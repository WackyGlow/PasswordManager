using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMan
{
    class PasswordHasher
    {   


        public string HashMasterPassword(string EncryptPassword)
        {
            // Convert the password string and master password to bytes
            byte[] passwordBytes = Encoding.UTF8.GetBytes(EncryptPassword);

            // Create an instance of Argon2id
            using (var hasher = new Argon2id(passwordBytes))
            {
                // Set the salt to the master password bytes
                hasher.Salt = passwordBytes;
                hasher.DegreeOfParallelism = 4; // Adjust this according to your system's capabilities
                hasher.MemorySize = 65536; // Adjust this according to your system's capabilities
                hasher.Iterations = 1;

                // Hash the password
                byte[] hashBytes = hasher.GetBytes(32); // You can adjust the output size as needed

                // Convert the hash to a hexadecimal string
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
