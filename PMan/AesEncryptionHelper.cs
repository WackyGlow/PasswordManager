using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PMan
{
    internal class AesEncryptionHelper
    {
        private Byte[] _key;
        private Byte[] _iv;
        private readonly KeyIvCollection _ctx;

        public AesEncryptionHelper(string masterkey, Byte[] iv)
        {
            _key = Encoding.UTF8.GetBytes(masterkey);
            _ctx = new KeyIvCollection();
            var checksum = _ctx.GetKeys();
            if (checksum != null)
            {
                _key = Encoding.UTF8.GetBytes(masterkey);
                _iv = checksum.InitVect;
            }
            else
            {
                _key = Encoding.UTF8.GetBytes(masterkey);
                _iv = iv;
                
            }
        }

        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                //I have to use this work around because Argon2id gives a 512bit key, while AES only accepts 256bit. IE, I have Byte64 key, but i need Byte32 key.
                byte[] argon2idKey = _key;
                byte[] aesKey = new byte[32];

                using (SHA256 sha256 = SHA256.Create())
                {
                    aesKey = sha256.ComputeHash(argon2idKey);
                }

                aesAlg.Key = aesKey;
                aesAlg.IV = _iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            try
            {

            }
            catch (Exception a)
            {
                Console.WriteLine(a);
                throw;
            }
            using (Aes aesAlg = Aes.Create())
            {
                var stateman = StateManager.Instance;
                //I have to use this work around because Argon2id gives a 512bit key, while AES only accepts 256bit. IE, I have Byte64 key, but i need Byte32 key.
                byte[] argon2idKey = _key;
                byte[] aesKey = new byte[32];

                using (SHA256 sha256 = SHA256.Create())
                {
                    aesKey = sha256.ComputeHash(argon2idKey);
                }

                aesAlg.Key = aesKey;
                aesAlg.IV = _iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
