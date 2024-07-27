using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
 
namespace DBBACKUP
{
    
    public partial class FrmDbBackup : Form
    {
        private Timer backupTimer;
        private SqlCommand cmd;
        private SqlConnection sqlCon;
        private SqlDataReader dr;
        private string conString = "Data Source=.; Initial Catalog=master; Integrated Security=True;";
        private SqlConnection con;
        private SqlCommand cmd2;

        public FrmDbBackup()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = "Data Source=.;Integrated Security=True;";
            var databaseManager = new DatabaseManager(connectionString);
            databaseManager.CheckAndCreateDatabaseTables("infoDB");
            sqlCon = new SqlConnection("Data Source=.; Initial Catalog=master; Integrated Security=True;");
            
        }

        private void FrmDbBackup_Load(object sender, EventArgs e)
        {
            LoadBackinfo();

            if (LinkLabel1.Text == string.Empty)
            {
                LinkLabel1.Text = "Click To Set Directory Path";
            }

            serverName(".");
           
        }
        private string GetConnectionString(string serverName)
        {
            return $"Data Source={serverName};Database=Master;Integrated Security=True;";
        }





      
        public void serverName(string str)
        {
            using (con = new SqlConnection(GetConnectionString(str)))
            {
                con.Open();
                using (cmd2 = new SqlCommand("select * from sysservers where srvproduct='SQL Server'", con))
                {
                    dr = cmd2.ExecuteReader();
                    while (dr.Read())
                    {
                        ComboBoxserverName.Items.Add(dr[2]);
                    }
                    dr.Close();
                }
            }
        }

        public void Createconnection()
        {
            using (con = new SqlConnection(GetConnectionString(".")))
            {
                con.Open();
                ComboBoxDatabaseName.Items.Clear();
                using (cmd = new SqlCommand("select * from sysdatabases", con))
                {
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ComboBoxDatabaseName.Items.Add(dr[0]);
                    }
                    dr.Close();
                }
            }
        }






        private void LoadBackinfo()
        {
            using (sqlCon = new SqlConnection(conString))
            {
                sqlCon.Open();
                DataSet dsData = new DataSet();
                using (cmd = new SqlCommand("DATABASE_BACKUP", sqlCon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACTIONTYPE", "BACKUP_INFO");
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dsData);
                    if (dsData.Tables.Count > 0)
                    {
                        if (dsData.Tables[0].Rows.Count > 0)
                        {
                            LinkLabel1.Text = dsData.Tables[0].Rows[0]["LOCATION"].ToString();
                            txtNoOfFiles.Text = dsData.Tables[0].Rows[0]["NoOfFiles"].ToString();
                            txtSpan.Text = dsData.Tables[0].Rows[0]["DayInterval"].ToString();
                            linkLabel2.Text = dsData.Tables[0].Rows[0]["LOCATION"].ToString();
                            linkLabel3.Text = dsData.Tables[0].Rows[0]["DATABASENAME"].ToString() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + ".bak";
                        }
                        if (dsData.Tables[1].Rows.Count > 0)
                        {
                            lblLastBackupInfo.Text = string.Format("اخر نسخه كانت {0} في تاريخ {1} مخزنه في {2}.",
                                dsData.Tables[1].Rows[0]["BackupDate"].ToString(), dsData.Tables[1].Rows[0]["Location"].ToString(), dsData.Tables[1].Rows[0]["BackupType"].ToString());
                        }
                        else
                        {
                            lblLastBackupInfo.Text = "No Backups !!!";
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (LinkLabel1.Text == "Click To Set Directory Path")
            {
                MessageBox.Show("Click To Set Directory Path", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtSpan.Text == string.Empty)
            {
                MessageBox.Show("Enter how many last backup files required ", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int numFlag;
                using (sqlCon = new SqlConnection(conString))
                {
                    sqlCon.Open();
                    using (cmd = new SqlCommand("DATABASE_BACKUP", sqlCon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTIONTYPE", "INSERT_BACKUP_INFO");
                        cmd.Parameters.AddWithValue("@DatabaseName", ComboBoxDatabaseName.Text);
                       cmd.Parameters.AddWithValue("@Location", LinkLabel1.Text);
                        cmd.Parameters.AddWithValue("@DayInterval", txtSpan.Text);
                        cmd.Parameters.AddWithValue("@SoftwareDate", DateTimePicker1.Text);
                        cmd.Parameters.AddWithValue("@NoOfFiles", txtNoOfFiles.Text);
                        numFlag = cmd.ExecuteNonQuery();

                        if (numFlag > 0)
                        {
                            MessageBox.Show("Data saved successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadBackinfo();
                        }
                        else
                        {
                            MessageBox.Show("Data not saved. Plaese Try Again.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    } }
                } 
            }

        private void Timer1_Tick(object sender, EventArgs e)
                {
                    ProgressBarEx5.Value += 1;
                    if (ProgressBarEx5.Value == 100)
                    {
                        ProgressBarEx5.Visible = false;
                        Timer1.Stop();
                        Panel1.Visible = false;
                        ProgressBarEx5.Text = "Finished";
                    }
                }

      private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
                {
                    FolderBrowserDialog1.ShowDialog();
                    LinkLabel1.Text = FolderBrowserDialog1.SelectedPath;
                }

                private void btnBackup_Click(object sender, EventArgs e)
                {
                    if (linkLabel2.Text == string.Empty)
                    {
                        MessageBox.Show("Please Set Backup Setting", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (linkLabel3.Text == string.Empty)
                    {
                        MessageBox.Show("Please Set Backup Setting", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string filaPath;
                        if (!linkLabel2.Text.EndsWith(@"\"))
                        {
                            filaPath = linkLabel2.Text + @"\" + linkLabel3.Text;
                        }
                        else
                        {
                            filaPath = linkLabel2.Text + linkLabel3.Text;
                        }
                        int numFlag;
                        if (sqlCon.State == ConnectionState.Closed)
                        {
                    sqlCon = new SqlConnection(conString);
                    sqlCon.Open();
                        }
                        cmd = new SqlCommand("DATABASE_BACKUP", sqlCon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTIONTYPE", "DB_BACKUP");
                        // cmd.Parameters.AddWithValue("@DATABASE", txtDbName.Text); // Your Database Name
                        cmd.Parameters.AddWithValue("@DATABASE", ComboBoxDatabaseName.Text); // Your Database Name
                        cmd.Parameters.AddWithValue("@FILEPATH", filaPath);
                        cmd.Parameters.AddWithValue("@BackupName", linkLabel3.Text);
                        cmd.Parameters.AddWithValue("@SoftwareDate", DateTimePicker1.Text);
                        cmd.Parameters.AddWithValue("@Type", "Manually");
                        numFlag = cmd.ExecuteNonQuery();
                        DataTable dtLoc = new DataTable();
                        cmd = new SqlCommand("DATABASE_BACKUP", sqlCon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ACTIONTYPE", "REMOVE_LOCATION");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtLoc);
                        for (int i = 0; i < dtLoc.Rows.Count; i++)
                        {
                            string delLoc = dtLoc.Rows[i][0].ToString();
                            string filepath = delLoc;
                            if (File.Exists(filepath))
                            {
                                File.Delete(filepath);

                            }
                        }
                        if (numFlag > 0)
                        {
                            Panel1.Visible = true;
                            Panel1.Location = new Point(58, 138);
                            Panel1.Height = 117;
                            Panel1.Width = 446;
                            ProgressBarEx5.Visible = true;
                            ProgressBarEx5.Value = 0;
                            Timer1.Start();
                            LoadBackinfo();
                        }
                        else
                        {
                            MessageBox.Show("Plaese Try Again.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                private void btnClose_Click(object sender, EventArgs e)
                {
                    this.Close();
                }

                private void txtSpan_KeyPress(object sender, KeyPressEventArgs e)
                {
                    if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }






        private void InitializeBackupTimer()
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(conString))
                {
                    sqlCon.Open();
                    string query = "SELECT TOP 1 [DayInterval] FROM [infoDB].[dbo].[tblBackupInfo] ORDER BY [LastEditDate] DESC";
                    using (SqlCommand command = new SqlCommand(query, sqlCon))
                    {
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            int intervalDays = Convert.ToInt32(result);
                            backupTimer = new Timer
                            {
                                //Interval = intervalDays * 24 * 60 * 60 * 1000 // Interval set to intervalDays days (converted to milliseconds)
                                
                                Interval = 20000 // Interval set to 20 seconds for testing purposes
                            };
                            backupTimer.Tick += BackupTimer_Tick;
                            backupTimer.Start();
                            MessageBox.Show($"InitializeBackupTimer successfully after {intervalDays} day(s).");
                        }
                        else
                        {
                            MessageBox.Show("No DayInterval found in tblBackupInfo.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing backup timer: {ex.Message}");
            }
        }

        private void BackupTimer_Tick(object sender, EventArgs e)
        {
            backupTimer.Stop(); // Stop the timer after the first tick
            PerformBackup();
        }

        private void PerformBackup()
        {
            string query = "SELECT TOP (1) [DatabaseName], [Location], [NoOfFiles] FROM [infoDB].[dbo].[tblBackupInfo] ORDER BY [LastEditDate] DESC";
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string dbName = reader["DatabaseName"].ToString();
                                string location = reader["Location"].ToString();
                                int noOfFiles = Convert.ToInt32(reader["NoOfFiles"]);

                                reader.Close(); // Ensure reader is closed before executing another command

                                // Perform backup operations (create backup files based on NoOfFiles)
                                for (int i = 1; i <= noOfFiles; i++)
                                {
                                    string backupFileName = $"{location}\\{dbName}_Backup_{DateTime.Now:yyyyMMddHHmmss}_{i}.bak";
                                    string backupQuery = $"BACKUP DATABASE [{dbName}] TO DISK = '{backupFileName}'";
                                    using (SqlCommand backupCommand = new SqlCommand(backupQuery, connection))
                                    {
                                        backupCommand.ExecuteNonQuery();
                                    }
                                }

                                // Update LastEditDate in tblBackupInfo
                                string updateQuery = $"UPDATE [infoDB].[dbo].[tblBackupInfo] SET [LastEditDate] = GETDATE() WHERE [DatabaseName] = '{dbName}'";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.ExecuteNonQuery();
                                }

                                MessageBox.Show("Backup completed successfully.");
                            }
                            else
                            {
                                MessageBox.Show("No backup information found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during backup: {ex.Message}");
            }
        }


        private void Label21_Click(object sender, EventArgs e)
                {
                }

                private void button1_Click(object sender, EventArgs e)
                {
                    Form1 check = new Form1();
                    check.Show();
                    Hide();
                }

                private void Panel1_Paint(object sender, PaintEventArgs e)
                {
                }

                private void txtNoOfFiles_TextChanged(object sender, EventArgs e)
                {
                }

                private void txtSpan_TextChanged(object sender, EventArgs e)
                {
                }

                private void label2_Click(object sender, EventArgs e)
                {
                }

                private void label5_Click(object sender, EventArgs e)
                {
                }

                private void label12_Click(object sender, EventArgs e)
                {
                }

                private void button2_Click(object sender, EventArgs e)
                {
                    Form1 form1 = new Form1();
                    form1.Show();
                    Hide();
                }

                private void label3_Click(object sender, EventArgs e)
                {
                }

                private void ComboBoxserverName_SelectedIndexChanged(object sender, EventArgs e)
                {
                    Createconnection();
                }

                private void button1_Click_1(object sender, EventArgs e)
                {
                    InitializeBackupTimer();
                }

        private void button3_Schedule_Click(object sender, EventArgs e)
        {
            ScheduleDatabaseBackup scheduleDatabaseBackup = new ScheduleDatabaseBackup();

            scheduleDatabaseBackup.Show();
            //Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ScheduleDatabaseBackup scheduleDatabaseBackup = new ScheduleDatabaseBackup();

            scheduleDatabaseBackup.Show();
            //Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EncryptionForm encryptionForm = new EncryptionForm();
            encryptionForm.Show();
            //Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataBase_Info_Fix dataBase_Info_Fix = new DataBase_Info_Fix();

            dataBase_Info_Fix.Show();
            //Hide();

        }

        private void lblLastBackupInfo_Click(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SpeechRecognition speechRecognition = new SpeechRecognition();
            speechRecognition.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            QueryAnalyzer queryAnalyzer = new QueryAnalyzer();
            queryAnalyzer.Show();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            AdvanceFuture advanceFuture = new AdvanceFuture();
            advanceFuture.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ErrorMessagesChatGPT errorMessagesChatGPT = new ErrorMessagesChatGPT();
            errorMessagesChatGPT.Show();
        }
    }






        }
