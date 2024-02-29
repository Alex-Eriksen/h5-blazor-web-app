using System.Text;

namespace h5_blazor_web_app.Helpers;
public static class Helpers
{
    public static dynamic GetReturnType(byte[] bytes, ReturnType @return)
    {
        switch (@return)
        {
            case ReturnType._string: return Convert.ToBase64String(bytes);
            case ReturnType._byteArray: return bytes;
            case ReturnType._utf: return Encoding.UTF8.GetString(bytes);
            case ReturnType._hex: return Convert.ToHexString(bytes);
            case ReturnType._byte: return Encoding.Default.GetString(bytes);
            case ReturnType._ascii: return Encoding.ASCII.GetString(bytes);
            default: return Convert.ToBase64String(bytes);
        }
    }

    public enum ReturnType
    {
        _string,
        _byteArray,
        _utf,
        _hex,
        _byte,
        _ascii
    }
}
