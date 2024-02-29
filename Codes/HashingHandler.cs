using BC = BCrypt.Net.BCrypt;
using System.Security.Cryptography;
using System.Text;
using h5_blazor_web_app.Helpers;
using static h5_blazor_web_app.Helpers.Helpers;

namespace h5_blazor_web_app.Codes;
public class HashingHandler
{
    private byte[]? m_inputBytes = null;

    public HashingHandler(string textToHash)
    {
        m_inputBytes = Encoding.ASCII.GetBytes(textToHash);
    }

    public dynamic GetMD5Hash(ReturnType @return = ReturnType._string)
    {
        if (m_inputBytes == null)
        {
            throw new NullReferenceException("Input bytes are null.");
        }

        var md5 = MD5.Create();
        var hashBytes = md5.ComputeHash(m_inputBytes);

        return GetReturnType(hashBytes, @return);
    }

    public dynamic GetSHAHash(ReturnType @return = ReturnType._string)
    {
        if (m_inputBytes == null)
        {
            throw new NullReferenceException("Input bytes are null.");
        }

        var sha = SHA256.Create();
        var hashBytes = sha.ComputeHash(m_inputBytes);

        return GetReturnType(hashBytes, @return);
    }

    public dynamic GetHMACHash(string key = "123456789", ReturnType @return = ReturnType._string)
    {
        if (m_inputBytes == null)
        {
            throw new NullReferenceException("Input bytes are null.");
        }

        var hmac = new HMACSHA256(Encoding.ASCII.GetBytes(key));
        var hashBytes = hmac.ComputeHash(m_inputBytes);
        return GetReturnType(hashBytes, @return);
    }

    public dynamic GetPBKDF2Hash(string salt, string hashAlgorithmName = "SHA256", ReturnType @return = ReturnType._string)
    {
        if (m_inputBytes == null)
        {
            throw new NullReferenceException("Input bytes are null.");
        }

        byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
        var hashAlgorithm = new HashAlgorithmName(hashAlgorithmName);

        byte[] hashBytes = Rfc2898DeriveBytes.Pbkdf2(m_inputBytes, saltBytes, 10, hashAlgorithm, 32);
        return GetReturnType(hashBytes, @return);
    }

    public static string GetBCryptHash(string textToHash, ReturnType @return = ReturnType._string)
    {
        byte[] hashBytes = Encoding.ASCII.GetBytes(BC.HashPassword(textToHash, 10, true));
        return GetReturnType(hashBytes, @return);
    }

    public static bool VerifyBCrypt(string textToHash, string hashValue)
    {
        return BC.Verify(textToHash, hashValue, true);
    }
}
