using BC = BCrypt.Net.BCrypt;
using System.Security.Cryptography;
using System.Text;

namespace h5_blazor_web_app.Codes;
public class HashingHandler
{
    private byte[]? m_inputBytes = null;

    public HashingHandler(string textToHash)
    {
        m_inputBytes = Encoding.ASCII.GetBytes(textToHash);
    }

    public string GetMD5Hash()
    {
        if (m_inputBytes == null)
        {
            throw new NullReferenceException("Input bytes are null.");
        }

        var md5 = MD5.Create();
        var hashBytes = md5.ComputeHash(m_inputBytes);
        return Convert.ToBase64String(hashBytes);
    }

    public string GetSHAHash()
    {
        if (m_inputBytes == null)
        {
            throw new NullReferenceException("Input bytes are null.");
        }

        var sha = SHA256.Create();
        var hashBytes = sha.ComputeHash(m_inputBytes);
        return Convert.ToBase64String(hashBytes);
    }

    public string GetHMACHash(string key = "123456789")
    {
        if (m_inputBytes == null)
        {
            throw new NullReferenceException("Input bytes are null.");
        }

        var hmac = new HMACSHA256(Encoding.ASCII.GetBytes(key));
        var hashBytes = hmac.ComputeHash(m_inputBytes);
        return Convert.ToBase64String(hashBytes);
    }

    public string GetPBKDF2Hash(string salt, string hashAlgorithmName = "SHA256")
    {
        if (m_inputBytes == null)
        {
            throw new NullReferenceException("Input bytes are null.");
        }

        byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
        var hashAlgorithm = new HashAlgorithmName(hashAlgorithmName);

        byte[] hashBytes = Rfc2898DeriveBytes.Pbkdf2(m_inputBytes, saltBytes, 10, hashAlgorithm, 32);
        return Convert.ToBase64String(hashBytes);
    }

    public static string GetBCryptHash(string textToHash)
    {
        return BC.HashPassword(textToHash, 10, true);
    }

    public static bool VerifyBCrypt(string textToHash, string hashValue)
    {
        return BC.Verify(textToHash, hashValue, true);
    }
}
