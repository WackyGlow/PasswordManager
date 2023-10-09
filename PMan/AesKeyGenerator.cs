using System.Security.Cryptography;

namespace PMan;

public class AesKeyGenerator
{

    public AesKeyGenerator()
    {

    }

    public string GenerateKey()
    {
        using (AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider())
        {
            aesCrypto.GenerateKey();
            return Convert.ToBase64String(aesCrypto.Key);
        }
    }

    public string GenerateIV()
    {
        using (AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider())
        {
            aesCrypto.GenerateIV();
            return Convert.ToBase64String(aesCrypto.IV);
        }
    }
}