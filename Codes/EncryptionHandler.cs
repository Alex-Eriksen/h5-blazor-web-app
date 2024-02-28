using Microsoft.AspNetCore.DataProtection;

namespace h5_blazor_web_app.Codes;

public class EncryptionHandler
{
    private readonly IDataProtector m_protector;

    public EncryptionHandler(IDataProtectionProvider protectionProvider)
    {
        m_protector = protectionProvider.CreateProtector("123456789");
    }

    public string EncryptSymmetric(string plainText) => m_protector.Protect(plainText);
    public string DecryptSymmetric(string protectedText) => m_protector.Unprotect(protectedText);
    
}
