using System.Security.Cryptography;

public class RsaKeyLoader
{
    public static RSA LoadPrivateKey(string privateKeyFilePath)
    {
        var privateKeyPem = File.ReadAllText(privateKeyFilePath);
        return CreateRsaFromPem(privateKeyPem);
    }

    public static RSA LoadPublicKey(string publicKeyFilePath)
    {
        var publicKeyPem = File.ReadAllText(publicKeyFilePath);
        return CreateRsaFromPublicPem(publicKeyPem);
    }

    private static RSA CreateRsaFromPem(string pem)
    {
        // Remove the first and last lines
        var rsa = RSA.Create();
        var pemFormatted = pem.Replace("-----BEGIN PRIVATE KEY-----", "")
                      .Replace("-----END PRIVATE KEY-----", "")
                      .Replace("-----BEGIN PUBLIC KEY-----", "")
                      .Replace("-----END PUBLIC KEY-----", "")
                      .Trim();
        var keyBytes = Convert.FromBase64String(pemFormatted);
        rsa.ImportPkcs8PrivateKey(keyBytes, out _); 
        return rsa;
    }

    private static RSA CreateRsaFromPublicPem(string pem)
    {
        var rsa = RSA.Create();
        var pemFormatted = pem.Replace("-----BEGIN PUBLIC KEY-----", "")
                              .Replace("-----END PUBLIC KEY-----", "")
                              .Replace("\n", "")
                              .Replace("\r", "");
        var keyBytes = Convert.FromBase64String(pemFormatted);
        rsa.ImportSubjectPublicKeyInfo(keyBytes, out _); // Correct for public key
        return rsa;
    }
}
