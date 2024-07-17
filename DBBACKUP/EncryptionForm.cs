using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBBACKUP
{
    public partial class EncryptionForm : Form
    {

        private static byte[] Key;
        private static byte[] IV;

        public EncryptionForm()
        {
            InitializeComponent();
            GenerateKeyAndIV();
        }

        private void GenerateKeyAndIV()
        {
            using (var aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                Key = aes.Key;
                IV = aes.IV;
            }
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtInputFile.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Encrypted Files (*.encrypted)|*.encrypted";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFile.Text = saveFileDialog.FileName;
                }
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string inputFilePath = txtInputFile.Text;
            string outputFilePath = txtOutputFile.Text;

            try
            {
                EncryptBackupFile(inputFilePath, outputFilePath);
                MessageBox.Show("Encryption successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void EncryptBackupFile(string inputFilePath, string outputFilePath)
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
        private void DecryptBackupFile(string inputFilePath, string outputFilePath)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (FileStream inputStream = new FileStream(inputFilePath, FileMode.Open))
                using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create))
                using (CryptoStream cryptoStream = new CryptoStream(inputStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(outputStream);
                }
            }
        }
        /*
                private void btnDecrypt_Click(object sender, EventArgs e)
                {
                    string inputFilePath = txtInputFile.Text;
                    //string outputFilePath = txtOutputFile.Text;
                    string outputFilePath = inputFilePath;

                    try
                    {
                        DecryptBackupFile(inputFilePath, outputFilePath);
                        MessageBox.Show("Decryption successful!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }*/



        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string inputFilePath = txtInputFile.Text;
            string outputDirectory = Path.GetDirectoryName(inputFilePath);
            string outputFileName = Path.GetFileNameWithoutExtension(inputFilePath) + "_decrypted_DB.bak" ;
            string outputFilePath = Path.Combine(outputDirectory, outputFileName);

            try
            {
                DecryptBackupFile(inputFilePath, outputFilePath);
                MessageBox.Show("Decryption successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        //يظهلر لديا خطا error : padding is invalied and can't be reoved
        private void EncryptionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
