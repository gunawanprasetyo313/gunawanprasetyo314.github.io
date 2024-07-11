using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class AesEncryption
{

    // Fungsi untuk mengenkripsi teks menggunakan AES-128
    public static string Encrypt(string value, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Key = Encoding.UTF8.GetBytes(key.Substring(0, 16)); // Menggunakan 16 karakter pertama dari kunci
            aes.GenerateIV();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                // Simpan IV di awal stream
                ms.Write(aes.IV, 0, aes.IV.Length);
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(value);
                    }
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    // Fungsi untuk mendekripsi teks menggunakan AES-128
    public static string Decrypt(string ciphervalue, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Key = Encoding.UTF8.GetBytes(key.Substring(0, 16)); // Menggunakan 16 karakter pertama dari kunci
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] buffer = Convert.FromBase64String(ciphervalue);

            using (MemoryStream ms = new MemoryStream(buffer))
            {
                byte[] iv = new byte[aes.IV.Length];
                ms.Read(iv, 0, iv.Length);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, iv);
                
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}
