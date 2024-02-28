namespace h5_blazor_web_app.Codes;

using System.Security.Cryptography;
using System.Text;

public class AsyncEncryptionHandler
{
    private string m_privateKey;
    private string m_publicKey;
    public string PublicKey => m_publicKey;

    private readonly string m_privateKeyPath = "./Keys/privateKey.xml";
    private readonly string m_publicKeyPath = "./Keys/publicKey.xml";

    public AsyncEncryptionHandler()
    {
        if (File.Exists(m_privateKeyPath) && File.Exists(m_publicKeyPath))
        {
            m_privateKey = File.ReadAllText(m_privateKeyPath);
            m_publicKey = File.ReadAllText(m_publicKeyPath);
        }
        else
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                m_privateKey = rsa.ToXmlString(true);
                m_publicKey = rsa.ToXmlString(false);
                File.WriteAllText(m_privateKeyPath, m_privateKey);
                File.WriteAllText(m_publicKeyPath, m_publicKey);
            }
        }
    }

    public string Encrypt(string plainText) => AsyncEncrypter.Encrypt(plainText, PublicKey);

    public string Decrypt(string encryptedText)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.FromXmlString(m_privateKey);
            byte[] bytes = Convert.FromBase64String(encryptedText);
            byte[] decryptBytes = rsa.Decrypt(bytes, true);
            return Encoding.UTF8.GetString(decryptBytes);
        }
    }

    public string Decrypt(byte[] bytes)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.FromXmlString(m_privateKey);
            byte[] decryptBytes = rsa.Decrypt(bytes, true);
            return Encoding.UTF8.GetString(decryptBytes);
        }
    }
}
