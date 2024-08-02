using System.Security.Cryptography;
using System.Text;

public class AllFunctions
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public AllFunctions(string key)
    {
        using (var sha256 = SHA256.Create())
        {
            // Generate a 256-bit key and IV
            _key = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            _iv = new byte[16]; // AES block size is 16 bytes
            Array.Copy(_key, _iv, 16);
        }
    }

    public string Encrypt(string plainText)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
                sw.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public string Decrypt(string cipherText)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
// usage
//key can be kept in the appsetting as it must be same for both encryt and decrypt
//AesEncryption aesEncryption = new AesEncryption(key);
//string encryptedText = aesEncryption.Encrypt(originalText);

//string decryptedText = aesEncryption.Decrypt(encryptedText);
