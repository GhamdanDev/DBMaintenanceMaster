using System;
 
using System.IO;
using System.Security.Cryptography;
using System.Text;
public class AESEncryption
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("your_secret_key_here"); // 256-bit key
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("your_initialization_vector_here"); // 128-bit IV

    public static void EncryptBackupFile(string inputFilePath, string outputFilePath, string password)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;

            using (FileStream inputStream = new FileStream(inputFilePath, FileMode.Open))
            using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create))
            using (CryptoStream cryptoStream = new CryptoStream(outputStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                inputStream.CopyTo(cryptoStream);
            }
        }
    }
   
}
