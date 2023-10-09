using MongoDB.Driver;
using PMan.Entity;

namespace PMan;

public class MasterPasswordCollection
{
    private readonly IMongoCollection<HashedMPasswordEntity> _passwords;
    private PasswordHasher _passwordHasher;

    public MasterPasswordCollection()
    {
        // Connect to the MongoDB database
        var connectionString = "mongodb+srv://rasm92i3:S0Ao9vrnzC9pWthQ@cluster0.kmn6x0b.mongodb.net/";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("PasswordDB");


        // Initialize the collection for users
        _passwords = database.GetCollection<HashedMPasswordEntity>("MasterPasswordCollection");

        // Initialize the Hasher helper class
        _passwordHasher = new PasswordHasher();


    }


    public int GetLastEntryId()
    {
        var highestIdEntry = _passwords.Find(pw => true).ToList().Count;
        return highestIdEntry;
    }

    public void NewMPassword(string MPassword)
    {
        var mPass_Obj = new HashedMPasswordEntity
        {
            Id = GetLastEntryId(),
            HashedMasterPassword = MPassword
        };

        _passwords.InsertOne(mPass_Obj);
    }
}