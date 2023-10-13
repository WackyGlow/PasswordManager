using MongoDB.Driver;
using PMan.Entity;

namespace PMan;

public class PasswordCollection
{
    private readonly IMongoCollection<Password> _passwords;
    private PasswordHasher _passwordHasher;
    private AesEncryptionHelper _aes;
    private readonly KeyIvCollection _keys;

    public PasswordCollection()
    {
        // Connect to the MongoDB database
        var connectionString = "mongodb+srv://rasm92i3:S0Ao9vrnzC9pWthQ@cluster0.kmn6x0b.mongodb.net/";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("PasswordDB");


        // Initialize the collection for users
        _passwords = database.GetCollection<Password>("PasswordCollection");


        _passwordHasher = new PasswordHasher();
        _keys = new KeyIvCollection();
        var stateman = StateManager.Instance;  

        _aes = new AesEncryptionHelper(stateman.hashedPassword, _keys.GetKeys().InitVect);

    }


    public List<UnHashedPassword> GetAllPasswords()
    {
        var passwords = _passwords.Find<Password>(pw => true).ToList();
        var decryptedList = new List<UnHashedPassword>();
        try
        {
            foreach (var ent in passwords)
            {
                var decryptedlogin = new UnHashedPassword
                {
                    Id = ent.Id,
                    Website = ent.Website,
                    Login = _aes.Decrypt(ent.HashedLogin),
                    Password = _aes.Decrypt(ent.HashedPassword)
                };
                decryptedList.Add(decryptedlogin);
            }

            return decryptedList;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<UnHashedPassword>();
        }
    }

    public int GetPasswordsAmount()
    {
        var passwords = _passwords.Find<Password>(pw => true).ToList().Count;

        return passwords;
    }


    public void AddNewPass(string website, string loginname, string password)
    {
        var obj = new Password()
        {
            Id = GetPasswordsAmount(),
            Website = website,
            HashedLogin = loginname,
            HashedPassword = password
        };

        _passwords.InsertOne(obj);
    }

}