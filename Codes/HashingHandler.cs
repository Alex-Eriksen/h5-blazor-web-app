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

    /// <summary>
    /// Hashes the input bytes using MD5.
    /// </summary>
    /// <param name="return"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
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

    /// <summary>
    /// Hashes the input bytes using SHA1.
    /// </summary>
    /// <param name="return"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
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

    /// <summary>
    /// Hashes the input bytes using HMAC.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="return"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
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

    /// <summary>
    /// Hashes the input bytes using PBKDF2.
    /// </summary>
    /// <param name="salt"></param>
    /// <param name="hashAlgorithmName"></param>
    /// <param name="return"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
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

    /// <summary>
    /// Encrypts the <paramref name="textToHash"/> using BCrypt.
    /// </summary>
    /// <param name="textToHash"></param>
    /// <param name="return"></param>
    /// <returns><paramref name="textToHash"/> as <paramref name="return"/> type.</returns>
    public static dynamic GetBCryptHash(string textToHash, ReturnType @return = ReturnType._ascii)
    {
        byte[] hashBytes = Encoding.ASCII.GetBytes(BC.HashPassword(textToHash, 10, true));
        return GetReturnType(hashBytes, @return);
    }

    /// <summary>
    /// Verifies that <paramref name="textToHash"/> hash is equal to <paramref name="hashValue"/>.
    /// </summary>
    /// <param name="textToHash"></param>
    /// <param name="hashValue"></param>
    /// <returns>true or false depending on the result.</returns>
    public static bool VerifyBCrypt(string textToHash, string hashValue)
    {
        return BC.Verify(textToHash, hashValue, true);
    }
}
