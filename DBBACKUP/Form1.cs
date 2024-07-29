using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 
using System.Data.SqlClient;

namespace DBBACKUP
{
    

    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Visible = false;
            serverName(".");
        }
        private void UpdateNameLabel()
        {
            if (string.IsNullOrWhiteSpace(ctlFirstName.Text) || string.IsNullOrWhiteSpace(ctlLastName.Text))
                lblFullName.Text = "Please fill out both the first filed and the last filed.";
            else
                lblFullName.Text = $"This is {ctlFirstName.Text} and this {ctlLastName.Text}, implement User control concept.";
        }
        public void serverName(string str)
        {
            con = new SqlConnection("Data Source=" + str + ";Database=Master;data source=.; Integrated Security=True;;");
            con.Open();
            cmd = new SqlCommand("select *  from sysservers  where srvproduct='SQL Server'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ComboBoxserverName.Items.Add(dr[2]);
            }
            dr.Close();
        }

        private void ShowErrorMessage(string message)
        {
            Form errorForm = new Form
            {
                Text = "Error",
                Width = 600,
                Height = 400,
                StartPosition = FormStartPosition.CenterScreen
            };

            TextBox textBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Fill,
                Text = message,
                ScrollBars = ScrollBars.Vertical
            };

            errorForm.Controls.Add(textBox);
            errorForm.ShowDialog();
        }

        public void Createconnection()
        {
            con = new SqlConnection("Data Source=" + (ComboBoxserverName.Text) + ";Database=Master;data source=.; Integrated Security=True;;");
            con.Open();
            ComboBoxDatabaseName.Items.Clear();
            cmd = new SqlCommand("select * from sysdatabases", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ComboBoxDatabaseName.Items.Add(dr[0]);
            }
            dr.Close();
        }
        public void query(string que)
        {
            // ERROR: Not supported in C#: OnErrorStatement
         
            cmd = new SqlCommand(que, con);
            cmd.CommandTimeout = 60;
            cmd.ExecuteNonQuery();
        }
        //في الكود التالي اريد انى تعدله لكي  ينشاء نسخه من القاعده المحدد في ComboBoxDatabaseName با اسم جديد ويستعيد الملف الئ الجديده 
        public void blank(string str)
        {
            if (string.IsNullOrEmpty(ComboBoxserverName.Text) | string.IsNullOrEmpty(ComboBoxDatabaseName.Text))
            {
                // label3.Visible = true;
                MessageBox.Show("Server Name & Database can not be Blank");
                return;
            }
            else
            {
                if (str == "restore")
                {
                    OpenFileDialog1.ShowDialog();
                    // string a = ComboBoxDatabaseName.Text.ToString();
                    query("IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'" + ComboBoxDatabaseName.Text + "') DROP DATABASE " + ComboBoxDatabaseName.Text + " RESTORE DATABASE " + ComboBoxDatabaseName.Text + " FROM DISK = '" + OpenFileDialog1.FileName + "'");
                    label3.Visible = true;
                    label3.Text = "Database Backup file has been restore successfully";
                }
            }
        }

public void RestoreDatabaseWithNewLocation()
    {
        if (string.IsNullOrEmpty(ComboBoxserverName.Text) || string.IsNullOrEmpty(ComboBoxDatabaseName.Text))
        {
            MessageBox.Show("Server Name & Database cannot be blank");
            return;
        }

        // Provide your SQL Server connection string
        string connectionString = "Data Source=" + ComboBoxserverName.Text + ";Initial Catalog=master;Integrated Security=True";

        // Show file dialog to select backup file
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        openFileDialog1.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
        openFileDialog1.FilterIndex = 1;

        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                // Get the selected backup file path
                string backupFilePath = openFileDialog1.FileName;

                // Generate a new database name for the restored database
                string newDatabaseName = ComboBoxDatabaseName.Text + "_Restored_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                // Specify the new location for the restored database files
                string dataFilePath = @"C:\db" + newDatabaseName + ".mdf";
                string logFilePath = @"C:\db" + newDatabaseName + "_log.ldf";

                // SQL query to restore database with new name and location
                string sqlQuery = "RESTORE DATABASE " + newDatabaseName +
                                  " FROM DISK = '" + backupFilePath + "'" +
                                  " WITH MOVE 'test' TO '" + dataFilePath + "'," +
                                  " MOVE 'test' TO '" + logFilePath + "'";

                // Establish connection and execute the query
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.ExecuteNonQuery();

                    label3.Visible = true;
                    label3.Text = "Database restored successfully as " + newDatabaseName + " at " + dataFilePath;
                }
            }
            catch (Exception ex)
            {
                    //MessageBox.Show("Error: " + ex.Message);
                    ShowErrorMessage($"{ex.Message}");
            }
        }
    }
        public void RestoreDatabaseWithNewName()
        {
            // Provide your SQL Server connection string
            string connectionString = "Data Source=" + ComboBoxserverName.Text + ";Initial Catalog=master;Integrated Security=True";

            // Show file dialog to select backup file
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Get the selected backup file path
                    string backupFilePath = openFileDialog1.FileName;

                    // Generate a new database name for the restored database
                    string newDatabaseName = ComboBoxDatabaseName.Text + "_Restored_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    // Define new paths for the database files
                    string newMdfFilePath = @"C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\" + newDatabaseName + ".mdf";
                    string newLdfFilePath = @"C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\" + newDatabaseName + "_log.ldf";

                    // SQL query to restore database with new name and file paths
                    string sqlQuery = "RESTORE DATABASE " + newDatabaseName +
                                      " FROM DISK = '" + backupFilePath + "'" +
                                      " WITH MOVE '" + ComboBoxDatabaseName.Text + "' TO '" + newMdfFilePath + "'," +
                                      " MOVE '" + ComboBoxDatabaseName.Text + "'_log TO '" + newLdfFilePath + "'";

                    // Establish connection and execute the query
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(sqlQuery, connection);
                        command.ExecuteNonQuery();

                        label3.Visible = true;
                        label3.Text = "Database restored successfully as " + newDatabaseName;
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"{ex.Message}");
                }
            }
        }
        public void RestoreDatabaseWithNewName3()
        {
            // Provide your SQL Server connection string
            string connectionString = "Data Source=" + ComboBoxserverName.Text + ";Initial Catalog=master;Integrated Security=True";

            // Show file dialog to select backup file
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Get the selected backup file path
                    string backupFilePath = openFileDialog1.FileName;

                    // Generate a new database name for the restored database
                    string newDatabaseName = ComboBoxDatabaseName.Text + "_Restored_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    // Get the file list from the backup
                    List<(string LogicalName, string Type)> fileList = new List<(string, string)>();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string fileListQuery = $"RESTORE FILELISTONLY FROM DISK = '{backupFilePath}'";
                        SqlCommand fileListCommand = new SqlCommand(fileListQuery, connection);
                        using (SqlDataReader reader = fileListCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string logicalName = reader["LogicalName"].ToString();
                                string type = reader["Type"].ToString(); // D for data file, L for log file
                                fileList.Add((logicalName, type));
                            }
                        }

                        // Generate the MOVE clauses for each file
                        string moveClauses = "";
                        foreach (var file in fileList)
                        {
                            string newFilePath = (file.Type == "D")
                                ? $@"C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\{newDatabaseName}_{file.LogicalName}.mdf"
                                : $@"C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\{newDatabaseName}_{file.LogicalName}.ldf";

                            moveClauses += $" MOVE '{file.LogicalName}' TO '{newFilePath}',";
                        }

                        // Remove the trailing comma
                        if (moveClauses.EndsWith(","))
                        {
                            moveClauses = moveClauses.Substring(0, moveClauses.Length - 1);
                        }

                        // SQL query to restore database with new name and file paths
                        string sqlQuery = $"RESTORE DATABASE {newDatabaseName} FROM DISK = '{backupFilePath}' WITH {moveClauses}";

                        // Execute the restore query
                        SqlCommand restoreCommand = new SqlCommand(sqlQuery, connection);
                        restoreCommand.ExecuteNonQuery();

                        label3.Visible = true;
                        label3.Text = "Database restored successfully as " + newDatabaseName;
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"{ex.Message}");
                }
            }
        }

        public void RestoreDatabaseWithNewName1()
        {
           

            // Provide your SQL Server connection string
            string connectionString = "Data Source=" + ComboBoxserverName.Text + ";Initial Catalog=master;Integrated Security=True";

            // Show file dialog to select backup file
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Get the selected backup file path
                    string backupFilePath = openFileDialog1.FileName;

                    // Generate a new database name for the restored database
                    string newDatabaseName = ComboBoxDatabaseName.Text + "_Restored_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    // SQL query to restore database with new name
                    string sqlQuery = "RESTORE DATABASE " + newDatabaseName + " FROM DISK = '" + backupFilePath + "'";

                    // Establish connection and execute the query
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(sqlQuery, connection);
                        command.ExecuteNonQuery();

                        label3.Visible = true;
                        label3.Text = "Database restored successfully as " + newDatabaseName;
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"{ex.Message}");
                }
            }
        }
        public void CreateNewDatabaseCopy()
        {
            if (string.IsNullOrEmpty(ComboBoxserverName.Text) || string.IsNullOrEmpty(ComboBoxDatabaseName.Text))
            {
                MessageBox.Show("Server Name & Database cannot be blank");
                return;
            }

            // Provide your SQL Server connection string
            string connectionString = "Data Source=" + ComboBoxserverName.Text + ";Initial Catalog=master;Integrated Security=True";

            try
            {
                // Generate a new database name
                string newDatabaseName = ComboBoxDatabaseName.Text + "_Copy_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                // SQL query to create new database copy
                string sqlQuery = "CREATE DATABASE " + newDatabaseName + " AS COPY OF " + ComboBoxDatabaseName.Text;

                // Establish connection and execute the query
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.ExecuteNonQuery();

                    label3.Visible = true;
                    label3.Text = "New database copy created successfully as " + newDatabaseName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void CreateDatabaseCopy(string str)
        {
            if (string.IsNullOrEmpty(ComboBoxserverName.Text) || string.IsNullOrEmpty(ComboBoxDatabaseName.Text))
            {
                MessageBox.Show("Server Name & Database cannot be blank");
                return;
            }

            // Provide your SQL Server connection string
            string connectionString = "Data Source=" + ComboBoxserverName.Text + ";Initial Catalog=master;Integrated Security=True";

            // Check if the operation is for restoring from a backup
            if (str == "restore")
            {
                // Show file dialog to select backup file
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Get the selected backup file path
                        string backupFilePath = openFileDialog1.FileName;

                        // Generate a unique name for the new database copy
                        string newDatabaseName = ComboBoxDatabaseName.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                        // SQL query to drop existing database and restore from backup
                        string sqlQuery = "IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'" + newDatabaseName + "') " +
                                          "DROP DATABASE " + newDatabaseName + "; " +
                                          "RESTORE DATABASE " + newDatabaseName + " FROM DISK = '" + backupFilePath + "'";

                        // Establish connection and execute the query
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand(sqlQuery, connection);
                            command.ExecuteNonQuery();

                            label3.Visible = true;
                            label3.Text = "Database backup has been restored successfully as " + newDatabaseName;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
        public void RestoreDatabase(string dbName, string backupFilePath)
        {
            if (string.IsNullOrEmpty(ComboBoxserverName.Text) || string.IsNullOrEmpty(ComboBoxDatabaseName.Text))
            {
                MessageBox.Show("Server Name & Database can not be Blank");
                return;
            }


            OpenFileDialog1.ShowDialog();

            // Ensure the database name is safe to use (prevent SQL injection)
            string safeDbName = dbName.Replace("'", "''");

            // Construct the SQL query to restore the database
            string q = @"
        IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'" + safeDbName + @"')
        BEGIN
            RAISERROR('Database with name %s already exists.', 16, 1, '" + safeDbName + @"');
        END
        ELSE
        BEGIN
            RESTORE DATABASE [" + safeDbName + @"]
            FROM DISK = '" + OpenFileDialog1.FileName + @"'
            WITH REPLACE;
        END
    ";

            // Execute the query
            query(q);

            label3.Visible = true;
            label3.Text = "Database backup file has been restored successfully";
        }
        public void CreateDatabaseFromBackup(string serverName, string databaseName, string backupFilePath)
        {
            using (SqlConnection connection = new SqlConnection($"Data Source={serverName};Integrated Security=True"))
            {
                connection.Open();
                OpenFileDialog1.ShowDialog();
                // Check if the database already exists
                string checkDatabaseQuery = $"IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'{databaseName}') BEGIN CREATE DATABASE {databaseName} END";
                SqlCommand checkCommand = new SqlCommand(checkDatabaseQuery, connection);
                checkCommand.ExecuteNonQuery();

                // Restore the database from the backup file
                string restoreDatabaseQuery = $"RESTORE DATABASE {databaseName} FROM DISK = '{OpenFileDialog1.FileName}'";
                SqlCommand restoreCommand = new SqlCommand(restoreDatabaseQuery, connection);
                restoreCommand.ExecuteNonQuery();

                MessageBox.Show($"Database '{databaseName}' created successfully from backup file.");
            }
        }


        private void ComboBoxserverName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Createconnection();
        }
        private void cmbrestore_Click(object sender, EventArgs e)
        {
            blank("restore");
        }

        private void ComboBoxserverName_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Createconnection();
        }

        private void cmbbackup_Click(object sender, EventArgs e)
        {
            blank("restore");
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //RestoreDatabase(DateTime.Now.ToString(),"");
            //RestoreDatabase("sbcfjkdsabfo","");
            //CreateDatabaseFromBackup(".","newdatabase","");
            //CreateDatabaseCopy("restore");
            //CreateNewDatabaseCopy();
            RestoreDatabaseWithNewName3();
            //RestoreDatabaseWithNewLocation();
        }

        private void ctlFirstName_TextChanged(object sender, EventArgs e)
        {
            UpdateNameLabel();

        }

        private void ctlLastName_TextChanged(object sender, EventArgs e)
        {
            UpdateNameLabel();
        }

        private void lblFullName_Click(object sender, EventArgs e)
        {

        }
    }
}
