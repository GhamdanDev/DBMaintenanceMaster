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
using System.Data.SqlClient;
namespace DBBACKUP
{
    /*public partial class EncryptionForm : Form
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
*/


    /*  public partial class EncryptionForm : Form
      {
          private static byte[] Key;
          private static byte[] IV;
          string connectionString = "Server=.;Database=infoDB;Integrated Security=True;";
          public EncryptionForm()
          {
              InitializeComponent();
              GenerateKeyAndIV();
          }
          private void EncryptionForm_Load(object sender, EventArgs e)
          {

          }

          private void GenerateKeyAndIV()
          {
              using (var aes = Aes.Create())
              {
                  aes.GenerateKey();
                  aes.GenerateIV();
                  Key = aes.Key;
                  IV = aes.IV;

                  txtKey.Text = Convert.ToBase64String(Key);
                  txtIV.Text = Convert.ToBase64String(IV);
              }
          }

          private void btnSaveKey_Click(object sender, EventArgs e)
          {
              string fileName = txtInputFile.Text;
              SaveKeyToDatabase(fileName, Key, IV);
          }

          private void SaveKeyToDatabase(string fileName, byte[] key, byte[] iv)
          {
              using (SqlConnection connection = new SqlConnection(connectionString))
              {
                  connection.Open();
                  string query = "INSERT INTO Keys (FileName, Key, IV) VALUES (@FileName, @Key, @IV)";
                  using (SqlCommand command = new SqlCommand(query, connection))
                  {
                      command.Parameters.AddWithValue("@FileName", fileName);
                      command.Parameters.AddWithValue("@Key", key);
                      command.Parameters.AddWithValue("@IV", iv);
                      command.ExecuteNonQuery();
                  }
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
                      LoadKeyFromDatabase(openFileDialog.FileName);
                  }
              }
          }

          private void LoadKeyFromDatabase(string fileName)
          {
              using (SqlConnection connection = new SqlConnection(connectionString))
              {
                  connection.Open();
                  string query = "SELECT Key, IV FROM Keys WHERE FileName = @FileName";
                  using (SqlCommand command = new SqlCommand(query, connection))
                  {
                      command.Parameters.AddWithValue("@FileName", fileName);
                      using (SqlDataReader reader = command.ExecuteReader())
                      {
                          if (reader.Read())
                          {
                              Key = (byte[])reader["Key"];
                              IV = (byte[])reader["IV"];
                              txtKey.Text = Convert.ToBase64String(Key);
                              txtIV.Text = Convert.ToBase64String(IV);
                          }
                      }
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

          private void btnDecrypt_Click(object sender, EventArgs e)
          {
              string inputFilePath = txtInputFile.Text;
              string outputDirectory = Path.GetDirectoryName(inputFilePath);
              string outputFileName = Path.GetFileNameWithoutExtension(inputFilePath) + "_decrypted_DB.bak";
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
      }
  */










    /*  public partial class EncryptionForm : Form
      {
          private static byte[] Key;
          private static byte[] IV;
          private string connectionString = "Server=.;Database=infoDB;Integrated Security=True;";

          public EncryptionForm()
          {
              InitializeComponent();
              LoadOrGenerateKeyAndIV();
          }

          private void LoadOrGenerateKeyAndIV()
          {
              string defaultFileName = "default";
              if (!LoadKeyFromDatabase(defaultFileName))
              {
                  GenerateKeyAndIV();
                  SaveKeyToDatabase(defaultFileName, Key, IV);
              }
          }
          private void EncryptionForm_Load(object sender, EventArgs e)
          {

          }
          private void GenerateKeyAndIV()
          {
              using (var aes = Aes.Create())
              {
                  aes.GenerateKey();
                  aes.GenerateIV();
                  Key = aes.Key;
                  IV = aes.IV;

                  txtKey.Text = Convert.ToBase64String(Key);
                  txtIV.Text = Convert.ToBase64String(IV);
              }
          }

          private bool LoadKeyFromDatabase(string fileName)
          {
              using (SqlConnection connection = new SqlConnection(connectionString))
              {
                  connection.Open();
                  string query = "SELECT [Key], IV FROM Keys WHERE FileName = @FileName";
                  using (SqlCommand command = new SqlCommand(query, connection))
                  {
                      command.Parameters.AddWithValue("@FileName", fileName);
                      using (SqlDataReader reader = command.ExecuteReader())
                      {
                          if (reader.Read())
                          {
                              Key = (byte[])reader["Key"];
                              IV = (byte[])reader["IV"];
                              txtKey.Text = Convert.ToBase64String(Key);
                              txtIV.Text = Convert.ToBase64String(IV);
                              return true;
                          }
                      }
                  }
              }
              return false;
          }

          private void SaveKeyToDatabase(string fileName, byte[] key, byte[] iv)
          {
              using (SqlConnection connection = new SqlConnection(connectionString))
              {
                  connection.Open();
                  string query = "INSERT INTO Keys (FileName, [Key], IV) VALUES (@FileName, @Key, @IV)";
                  using (SqlCommand command = new SqlCommand(query, connection))
                  {
                      command.Parameters.AddWithValue("@FileName", fileName);
                      command.Parameters.AddWithValue("@Key", key);
                      command.Parameters.AddWithValue("@IV", iv);
                      command.ExecuteNonQuery();
                  }
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
                      LoadKeyFromDatabase(openFileDialog.FileName);
                  }
              }
          }

          private void btnSaveKey_Click(object sender, EventArgs e)
          {
              string fileName = txtInputFile.Text;
              SaveKeyToDatabase(fileName, Key, IV);
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

          private void btnDecrypt_Click(object sender, EventArgs e)
          {
              string inputFilePath = txtInputFile.Text;
              string outputDirectory = Path.GetDirectoryName(inputFilePath);
              string outputFileName = Path.GetFileNameWithoutExtension(inputFilePath) + "_decrypted_DB.bak";
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

          private void btnSaveKey_Click_1(object sender, EventArgs e)
          {
              string fileName = txtInputFile.Text;
              SaveKeyToDatabase(fileName, Key, IV);
          }
      }

  */


    /*   public partial class EncryptionForm : Form
       {
           private static byte[] Key;
           private static byte[] IV;
           private string connectionString = "Server=.;Database=infoDB;Integrated Security=True;";

           public EncryptionForm()
           {
               InitializeComponent();
               LoadKeysFromDatabase();
           }

           private void LoadKeysFromDatabase()
           {
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                   connection.Open();
                   string query = "SELECT FileName FROM Keys";
                   using (SqlCommand command = new SqlCommand(query, connection))
                   using (SqlDataReader reader = command.ExecuteReader())
                   {
                       while (reader.Read())
                       {
                           cmbKeys.Items.Add(reader["FileName"].ToString());
                       }
                   }
               }
           }
           private void btnSaveKey_Click_1(object sender, EventArgs e)
           {

           }
           private void GenerateKeyAndIV()
           {
               using (var aes = Aes.Create())
               {
                   aes.GenerateKey();
                   aes.GenerateIV();
                   Key = aes.Key;
                   IV = aes.IV;

                   txtKey.Text = Convert.ToBase64String(Key);
                   txtIV.Text = Convert.ToBase64String(IV);
               }
           }

           private void SaveKeyToDatabase(string fileName, byte[] key, byte[] iv)
           {
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                   connection.Open();
                   string query = "INSERT INTO Keys (FileName, [Key], IV) VALUES (@FileName, @Key, @IV)";
                   using (SqlCommand command = new SqlCommand(query, connection))
                   {
                       command.Parameters.AddWithValue("@FileName", fileName);
                       command.Parameters.AddWithValue("@Key", key);
                       command.Parameters.AddWithValue("@IV", iv);
                       command.ExecuteNonQuery();
                   }
               }
           }

           private bool LoadKeyFromDatabase(string fileName)
           {
               using (SqlConnection connection = new SqlConnection(connectionString))
               {
                   connection.Open();
                   string query = "SELECT [Key], IV FROM Keys WHERE FileName = @FileName";
                   using (SqlCommand command = new SqlCommand(query, connection))
                   {
                       command.Parameters.AddWithValue("@FileName", fileName);
                       using (SqlDataReader reader = command.ExecuteReader())
                       {
                           if (reader.Read())
                           {
                               Key = (byte[])reader["Key"];
                               IV = (byte[])reader["IV"];
                               txtKey.Text = Convert.ToBase64String(Key);
                               txtIV.Text = Convert.ToBase64String(IV);
                               return true;
                           }
                       }
                   }
               }
               return false;
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

           private void btnSaveKey_Click(object sender, EventArgs e)
           {
               if (cmbKeys.SelectedIndex >= 0)
               {
                   string selectedKey = cmbKeys.SelectedItem.ToString();
                   if (LoadKeyFromDatabase(selectedKey))
                   {
                       SaveKeyToDatabase(txtInputFile.Text, Key, IV);
                       MessageBox.Show("Key saved successfully!");
                   }
                   else
                   {
                       MessageBox.Show("Key not found in database.");
                   }
               }
               else
               {
                   GenerateKeyAndIV();
                   SaveKeyToDatabase(txtInputFile.Text, Key, IV);
                   MessageBox.Show("New key generated and saved successfully!");
               }
           }

           private void btnEncrypt_Click(object sender, EventArgs e)
           {
               string inputFilePath = txtInputFile.Text;
               string outputFilePath = inputFilePath + ".encrypted";

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

           private void btnDecrypt_Click(object sender, EventArgs e)
           {
               string inputFilePath = txtInputFile.Text;
               string outputFilePath = inputFilePath.Replace(".encrypted", "_decrypted.bak");

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

           private void EncryptionForm_Load(object sender, EventArgs e)
           {
               LoadKeysFromDatabase();
           }
       }

   */


    public partial class EncryptionForm : Form
    {
        private byte[] Key;
        private byte[] IV;
        private string connectionString = "Server=.;Database=infoDB;Integrated Security=True;";

        public EncryptionForm()
        {
            InitializeComponent();
            LoadKeysIntoComboBox();
        }

        private void LoadKeysIntoComboBox()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT FileName FROM Keys";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBoxKeys.Items.Add(reader["FileName"].ToString());
                    }
                }
            }
        }
                private void EncryptionForm_Load(object sender, EventArgs e)
        {

        }
        private void btnSaveKey_Click(object sender, EventArgs e)
        {
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

        private void SaveKeyToDatabase(string fileName, byte[] key, byte[] iv)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Keys (FileName, [Key], IV) VALUES (@FileName, @Key, @IV)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@Key", key);
                    command.Parameters.AddWithValue("@IV", iv);
                    command.ExecuteNonQuery();
                }
            }
        }

        private bool LoadKeyFromDatabase(string fileName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT [Key], IV FROM Keys WHERE FileName = @FileName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FileName", fileName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Key = (byte[])reader["Key"];
                            IV = (byte[])reader["IV"];
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtInputFile.Text = openFileDialog.FileName;
                    if (!LoadKeyFromDatabase(openFileDialog.FileName))
                    {
                        GenerateKeyAndIV();
                        SaveKeyToDatabase(openFileDialog.FileName, Key, IV);
                    }
                }
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string inputFilePath = txtInputFile.Text;
            string outputFilePath = inputFilePath + ".encrypted";

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

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string inputFilePath = txtInputFile.Text;
            string outputDirectory = Path.GetDirectoryName(inputFilePath);
            string outputFileName = Path.GetFileNameWithoutExtension(inputFilePath).Replace(".encrypted", "") + "_decrypted.bak";
            string outputFilePath = Path.Combine(outputDirectory, outputFileName);

            if (!LoadKeyFromDatabase(inputFilePath.Replace(".encrypted", "")))
            {
                MessageBox.Show("The key for this file could not be found in the database.");
                return;
            }

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
    }

}
