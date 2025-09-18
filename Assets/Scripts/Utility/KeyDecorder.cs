using System.Security.Cryptography;
using System.Text;
using System;
using System.IO;
using UnityEngine;

public class KeyDecorder
{
    public static (byte[] key, byte[] iv) DecryptKey(string filePath, byte[] decryptionKey, byte[] decryptionIV)
    {
        try
        {
            byte[] encryptedData = File.ReadAllBytes(filePath);

            using (Aes aes = Aes.Create())
            {
                aes.Key = decryptionKey;
                aes.IV = decryptionIV;

                using (var decryptor = aes.CreateDecryptor())
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                    string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                    string[] parts = decryptedText.Split('|');

                    Debug.Log("ÉLÅ[Ç∆IVÇïúå≥ÇµÇ‹ÇµÇΩÅI");
                    return (Convert.FromBase64String(parts[0]),
                        Convert.FromBase64String(parts[1]));
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"ïúçÜâªÇ…é∏îsÇµÇ‹ÇµÇΩ: {ex.Message}");
            return (null, null);
        }
    }
}
