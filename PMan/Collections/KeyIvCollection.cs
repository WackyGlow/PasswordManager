using MongoDB.Driver;
using PMan.Entity;

namespace PMan;

public class KeyIvCollection
{
    private readonly IMongoCollection<KeyEntity> _passwords;
    public KeyIvCollection()
    {
        // Connect to the MongoDB database
        var connectionString = "mongodb+srv://rasm92i3:S0Ao9vrnzC9pWthQ@cluster0.kmn6x0b.mongodb.net/";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("PasswordDB");


        // Initialize the collection for users
        _passwords = database.GetCollection<KeyEntity>("KeyIvCollection");
    }


    public KeyEntity GetKeys()
    {
       return _passwords.Find(k => true).FirstOrDefault();
    }


    public void InsertValues(string key, string iv)
    {

        var checksum = GetKeys();

        if (checksum != null)
        {
            //do nothing :)
        }
        else
        {
            var obj = new KeyEntity()
            {
                Id = 1,
                HashKey = key,
                InitVect = iv
            };
            _passwords.InsertOne(obj);
        }
    }
}