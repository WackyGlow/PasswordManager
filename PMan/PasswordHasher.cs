using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PMan.Entity;

namespace PMan
{
    class PasswordHasher
    {


        public  HashedMPasswordEntity HashMasterPassword(string masterPassword, HashedMPasswordEntity obj)
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

                var stateman = StateManager.Instance;
                stateman.hashedPassword = hashHex;
                obj.salt = salt;

                return obj;
            }
        }

        /*
        public bool VerifyMasterPassword(string masterPassword, HashedMPasswordEntity storedHash)
        {
            // Convert the provided master password string to bytes
            byte[] passwordBytes = Encoding.UTF8.GetBytes(masterPassword);

            // Retrieve the stored salt
            byte[] salt = storedHash.salt;

            // Create an Argon2id instance with the same parameters
            using (var hasher = new Argon2id(passwordBytes))
            {
                // Set the salt
                hasher.Salt = salt;
                hasher.DegreeOfParallelism = 4; // Use the same parameters as during hashing
                hasher.MemorySize = 65536;
                hasher.Iterations = 1;

                // Compute the hash
                byte[] computedHashBytes = hasher.GetBytes(32);

                // Convert the computed hash bytes to a hexadecimal string
                string computedHashHex = BitConverter.ToString(computedHashBytes).Replace("-", "").ToLower();

                // Compare the computed hash with the stored hash
                return storedHash.HashedMasterPassword.Equals(computedHashHex);
            }
        }
        */


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
