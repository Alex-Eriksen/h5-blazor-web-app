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

    public enum ReturnType
    {
        _string,
        _byteArray,
        _utf,
        _hex,
        _byte
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
