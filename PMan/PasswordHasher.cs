using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PMan
{
    class PasswordHasher
    {


        public  string HashMasterPassword(string masterPassword)
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

        public bool VerifyMasterPassword(string inputPassword, string storedHash, string masterPassword)
        {
            // Convert the input password string, master password, and stored hash to bytes
            byte[] inputPasswordBytes = Encoding.UTF8.GetBytes(inputPassword);
            byte[] masterPasswordBytes = Encoding.UTF8.GetBytes(masterPassword);
            byte[] storedHashBytes = StringToByteArray(storedHash); // Convert the stored hash string to bytes

            // Create an instance of Argon2id with the same parameters as during hashing
            using (var hasher = new Argon2id(inputPasswordBytes))
            {
                // Set the salt to the master password bytes
                hasher.Salt = masterPasswordBytes;
                hasher.DegreeOfParallelism = 4; // Adjust this according to your system's capabilities
                hasher.MemorySize = 65536; // Adjust this according to your system's capabilities
                hasher.Iterations = 1;

                // Hash the input password
                byte[] newHashBytes = hasher.GetBytes(32); // You can adjust the output size as needed

                // Compare the newly generated hash with the stored hash
                return ByteArrayToHexString(newHashBytes) == storedHash;
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
}
