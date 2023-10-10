using System.Security.Cryptography;

namespace PMan;

public class AesKeyGenerator
{

    public AesKeyGenerator()
    {

    }

    public Byte[] GenerateKey()
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.GenerateKey();
            var _key = aesAlg.Key;
            return _key;
        }
    }

    public Byte[] GenerateIV()
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.GenerateIV();
            var _iv = aesAlg.IV;
            return _iv;
        }
    }


}