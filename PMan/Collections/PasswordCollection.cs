using MongoDB.Driver;
using PMan.Entity;

namespace PMan;

public class PasswordCollection
{
    private readonly IMongoCollection<Password> _passwords;

    public PasswordCollection()
    {
        // Connect to the MongoDB database
        var connectionString = "mongodb+srv://rasm92i3:S0Ao9vrnzC9pWthQ@cluster0.kmn6x0b.mongodb.net/";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("PasswordDB");


        // Initialize the collection for users
        _passwords = database.GetCollection<Password>("PasswordCollection");
    }


    public List<Password> GetAllPasswords()
    {
            var passwords = _passwords.Find<Password>(pw => true).ToList();
            return passwords;
    }


}