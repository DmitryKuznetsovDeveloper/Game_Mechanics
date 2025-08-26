using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SaveSystem
{
    public static class AesEncryptor
    {
        private const int KEY_SIZE = 32;
        
        public static string Encrypt(string plainText, string password)
        {
            byte[] key = GetKey(password);
            byte[] iv = GenerateIV();

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            ms.Write(iv, 0, iv.Length);
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);
            sw.Write(plainText);
            sw.Close();

            return Convert.ToBase64String(ms.ToArray());
        }
        
        public static string Decrypt(string cipherText, string password)
        {
            byte[] fullData = Convert.FromBase64String(cipherText);
            byte[] key = GetKey(password);

            byte[] iv = new byte[16];
            Array.Copy(fullData, 0, iv, 0, iv.Length);

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(fullData, iv.Length, fullData.Length - iv.Length);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        
        private static byte[] GetKey(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            Array.Resize(ref key, KEY_SIZE);
            return key;
        }
        
        private static byte[] GenerateIV()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] iv = new byte[16];
            rng.GetBytes(iv);
            return iv;
        }
    }
}
