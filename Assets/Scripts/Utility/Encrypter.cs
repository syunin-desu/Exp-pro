using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

/// <summary>
/// AES暗号化クラス
/// </summary>
public class Encrypter
{

    public byte[] EncryptData(byte[] binaryData, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (var encryptor = aes.CreateEncryptor())
            {
                // 暗号化処理を実行
                return encryptor.TransformFinalBlock(binaryData, 0, binaryData.Length);
            }
        }
    }
    public byte[] DecryptData(byte[] encryptedData, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            using (var decryptor = aes.CreateDecryptor())
            {
                // 復号化処理を実行
                return decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            }
        }
    }
}
