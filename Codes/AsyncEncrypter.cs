using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace h5_blazor_web_app.Codes;

public class AsyncEncrypter
{
    public static string Encrypt(string plainText, string publicKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.FromXmlString(publicKey);
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptBytes = rsa.Encrypt(bytes, true);
            return Convert.ToBase64String(encryptBytes);
        }
    }
}
