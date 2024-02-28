namespace h5_blazor_web_app.Codes;
using System.Security.Cryptography;
using System.Text;

public class AsyncEncryptionHandler
{
    private string m_privateKey;
    private string m_publicKey;

    public AsyncEncryptionHandler()
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            m_privateKey = rsa.ToXmlString(true);
            m_publicKey = rsa.ToXmlString(false);
        }
    }
    public enum ReturnType
    {
        _string,
        _byteArray,
        _utf,
        _hex,
        _byte
    }

    public dynamic Encrypt(string plainText, ReturnType @return = ReturnType._string)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(m_publicKey);
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptBytes = rsa.Encrypt(bytes, true);
            return GetReturnType(encryptBytes, @return);
        }
    }

    public dynamic Decrypt(string encryptedText, ReturnType @return = ReturnType._string)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(m_privateKey);
            byte[] bytes = Encoding.UTF8.GetBytes(encryptedText);
            byte[] decryptBytes = rsa.Decrypt(bytes, true);
            return GetReturnType(decryptBytes, @return);
        }
    }

    private static dynamic GetReturnType(byte[] hashBytes, ReturnType @return)
    {
        switch (@return)
        {
            case ReturnType._string: return Convert.ToBase64String(hashBytes);
            case ReturnType._byteArray: return hashBytes;
            case ReturnType._utf: return Encoding.UTF8.GetString(hashBytes);
            case ReturnType._hex: return Convert.ToHexString(hashBytes);
            case ReturnType._byte: return Encoding.Default.GetString(hashBytes);
            default: return Convert.ToBase64String(hashBytes);
        }
    }
}
