using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public class EncryptedJsonFileStorage<T> : ISaveLoadRepository<T>
    {
        private readonly string _filePath;
        private readonly string _encryptionPassword;

        public EncryptedJsonFileStorage(string filePath, string encryptionPassword)
        {
            _filePath = filePath;
            _encryptionPassword = encryptionPassword;
        }

        public void Save(T data)
        {
            string json = JsonUtility.ToJson(data);
            string encrypted = AesEncryptor.Encrypt(json, _encryptionPassword);
            File.WriteAllText(_filePath, encrypted);
        }

        public T Load()
        {
            if (!File.Exists(_filePath))
            {
                return default;
            }
            
            string encrypted = File.ReadAllText(_filePath);
            string json = AesEncryptor.Decrypt(encrypted, _encryptionPassword);
            return JsonUtility.FromJson<T>(json);
        }
    }
}