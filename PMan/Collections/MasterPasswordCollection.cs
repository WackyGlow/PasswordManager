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

    public int GetCurrentId()
    {
        var currentId = _passwords.Find(pw => true).ToList().Count - 1; return currentId;
    }



    public void NewMPassword(string MPassword)
    {
        var mPass_Obj = new HashedMPasswordEntity
        {
            Id = GetLastEntryId()
        };

        var objEntity = _passwordHasher.HashMasterPassword(MPassword,mPass_Obj);


        _passwords.InsertOne(objEntity);
    }

    public bool Login(string masterpassword)
    {
        var obj = _passwords.Find(pw => pw.Id == 1).FirstOrDefault();
        return _passwordHasher.VerifyMasterPassword(masterpassword, obj);
    }



}