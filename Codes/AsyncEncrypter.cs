using System.Security.Cryptography;
using System.Text;
using static h5_blazor_web_app.Helpers.Helpers;

namespace h5_blazor_web_app.Codes;

public class AsyncEncrypter
{
    public static dynamic Encrypt(string plainText, string publicKey, ReturnType @return = ReturnType._string)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.FromXmlString(publicKey);
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptBytes = rsa.Encrypt(bytes, true);
            return GetReturnType(encryptBytes, @return);
        }
    }
}
