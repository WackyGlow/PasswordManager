using Konscious.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace PMan;

public partial class NewUserPage : ContentPage
{
	public NewUserPage()
	{
		InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);
    }

    private async void OnCreateClicked(object sender, EventArgs e)
    {
        var passwordEntry = PasswordEntry.Text;
        try
        {
            if (passwordEntry != null)
            {
                string password = passwordEntry;
                Console.WriteLine(password);
                var _mpdb = new MasterPasswordCollection();
                _mpdb.NewMPassword(HashMasterPassword(password));
                await Navigation.PopAsync();
            }
            else
            {
                //do absolutely nothing... yet...
            }
        }
        catch (Exception ex)
        {
            // Handle the exception here
            Console.WriteLine($"An error occurred: {ex.Message}");
        }


        
    }


    public string HashMasterPassword(string masterPassword)
    {
        // Convert the master password string to bytes
        byte[] passwordBytes = Encoding.UTF8.GetBytes(masterPassword);

        // Generate a random salt (16 bytes is a common size)
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Create an Argon2id instance
        using (var hasher = new Argon2id(passwordBytes))
        {
            // Set the salt
            hasher.Salt = salt;
            hasher.DegreeOfParallelism = 4; // Adjust this according to your system's capabilities
            hasher.MemorySize = 65536; // Adjust this according to your system's capabilities
            hasher.Iterations = 1;

            // You can customize other parameters here (memory cost, time cost, parallelism)

            // Compute the hash
            byte[] hashBytes = hasher.GetBytes(32); // 32 bytes is a common hash size

            // Convert the hash bytes to a hexadecimal string
            string hashHex = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashHex;
        }
    }

    // Utility function to convert a hexadecimal string to byte array
    private byte[] StringToByteArray(string hex)
    {
        int numberChars = hex.Length;
        byte[] bytes = new byte[numberChars / 2];
        for (int i = 0; i < numberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }

    // Utility function to convert a byte array to a hexadecimal string
    private string ByteArrayToHexString(byte[] bytes)
    {
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }

}