using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBBACKUP
{

    /*
        public partial class ScheduleDatabaseBackup : Form
        {
            private SqlCommand cmd;
            private SqlConnection sqlCon;
            private SqlDataReader dr;
            private string conString = "Data Source=.; Initial Catalog=DemoTest; Integrated Security=True;";

            private SqlConnection con;
            private SqlCommand cmd2;
            public ScheduleDatabaseBackup()
            {
                InitializeComponent();
                InitializeDatabaseConnection();
            }


            private void InitializeDatabaseConnection()
            {
                sqlCon = new SqlConnection(conString);
            }


            // دالة لإضافة المستخدم الجديد
            private void AddUser(string userName, string password)
            {
                if (con == null || con.State == System.Data.ConnectionState.Closed)
                {
                    MessageBox.Show("Please connect to a server first.");
                    return;
                }

                string query = $"CREATE LOGIN {userName} WITH PASSWORD = '{password}'; " +
                               $"CREATE USER {userName} FOR LOGIN {userName};";

                try
                {
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User added successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            private void ButtonAddUser_Click(object sender, EventArgs e)
            {
                string userName = textBox1Username.Text;
                string password = textBox1Password.Text;
                using (con = new SqlConnection("Data Source=" + ComboBoxserverName.Text + ";Database=Master;Integrated Security=True;"))
                {
                    con.Open();
                    using (cmd = new SqlCommand("select * from sysservers where srvproduct='SQL Server'", con))
                    {
                        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                        {
                            MessageBox.Show("Please enter both username and password.");
                            return;
                        }

                        AddUser(userName, password);
                    }
                }
            }


            private void ScheduleDatabaseBackup_Load(object sender, EventArgs e)
            {
                serverName(".");
                // Handle form load event if needed
            }
            //Dictionary<Object, Object> databasedetails= new Dictionary<Object, Object>();
            Dictionary<string, string> databasedetails= new Dictionary<string, string>();

            public void serverName(string str)
            {
                using (con = new SqlConnection("Data Source=" + str + ";Database=Master;Integrated Security=True;"))
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
                using (con = new SqlConnection("Data Source=" + ComboBoxserverName.Text + ";Database=Master;Integrated Security=True;"))
                {
                    con.Open();
                    checkedListDatabaseName.Items.Clear();
                    using (cmd = new SqlCommand("select * from sysdatabases", con))
                    {
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            checkedListDatabaseName.Items.Add(dr[0]);
                        databasedetails.Add(dr[0].ToString(),dr[1].ToString());
                        }
                        dr.Close();
                    }
                }
            }


            private void ComboBoxserverName_SelectedIndexChanged_1(object sender, EventArgs e)
            {
      Createconnection();
            }


            private void checkedListDatabaseName_SelectedIndexChanged(object sender, EventArgs e)
            {
                labelDBDetails.Text = checkedListDatabaseName.Text;
                string dbId = databasedetails[checkedListDatabaseName.Text];
                using (con = new SqlConnection("Data Source=" + ComboBoxserverName.Text + ";Database=Master;Integrated Security=True;"))
                {
                    con.Open(); 
                    using (cmd = new SqlCommand($"SELECT * FROM sys.master_files where database_id={dbId}", con))
                    {
                        int count = 0;
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            //labelDBDetails.Text = dr[6].ToString();

                            label1PhysicalName.Text= dr[7].ToString();
                            label1.Text = dr[9].ToString();
                            label2.Text = dr[4].ToString();
                            label3.Text = dr[12].ToString();

                            label5.Text = dr[2].ToString();
                            label6.Text = dr[6].ToString();

                            label7.Text = "log file";
                            label8.Text = "file guid";
                            //label9.Text = dr.GetName(9);
                            label10.Text = "growth";
                            label11.Text = "type desc";
                            label12.Text = "State desc";
                            label14.Text = "file location";





                        }
                        dr.Close();
                    }
                }
            }
        }
    */

    using System;
    using System.Data.SqlClient;
    using System.Windows.Forms;

    public partial class ScheduleDatabaseBackup : Form
    {
        private SqlCommand cmd;
        private SqlConnection sqlCon;
        private SqlDataReader dr;
        private string conString = "Data Source=.; Initial Catalog=DemoTest; Integrated Security=True;";
        private SqlConnection con;
        private SqlCommand cmd2;
        private Dictionary<string, string> databasedetails = new Dictionary<string, string>();

        public ScheduleDatabaseBackup()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            sqlCon = new SqlConnection(conString);
        }

        private SqlConnection OpenConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return connection;
        }


        private void InfoOfDBTables(string databaseName)
        {
            // استبدل قيمة connectionString بقيمة الاتصال بقاعدة البيانات الخاصة بك
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";

            // تضمين أمر USE database وأوامر DBCC SHRINKFILE ضمن النص الخاص بالأمر
            string query = $@"
    USE {databaseName};
    SELECT  
        t.NAME AS TableName,
        SUM(ps.row_count) AS RowCounts,
        SUM(ps.reserved_page_count) * 8 AS TotalSpaceKB,
        SUM(ps.used_page_count) * 8 AS UsedSpaceKB,
        (SUM(ps.reserved_page_count) - SUM(ps.used_page_count)) * 8 AS UnusedSpaceKB
    FROM 
        {databaseName}.sys.dm_db_partition_stats ps
    INNER JOIN 
        {databaseName}.sys.tables t ON ps.object_id = t.object_id
    GROUP BY 
        t.NAME
    ORDER BY 
        TotalSpaceKB DESC;
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"حدث خطأ: {ex.Message}");
                    }
                }
            }
        }
        private void AddUser(string userName, string password, string database)
        {
            string queryLogin = $"CREATE LOGIN {userName} WITH PASSWORD = '{password}'";
            string queryUser = $"USE {database}; CREATE USER {userName} FOR LOGIN {userName}";

            try
            {
                using (SqlCommand cmdLogin = new SqlCommand(queryLogin, con))
                {
                    cmdLogin.ExecuteNonQuery();
                }

                using (SqlCommand cmdUser = new SqlCommand(queryUser, con))
                {
                    cmdUser.ExecuteNonQuery();
                }

                MessageBox.Show("User added successfully.");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ButtonAddUser_Click(object sender, EventArgs e)
        {
            string userName = textBox1Username.Text;
            string password = textBox1Password.Text;
            string selectedDatabase = checkedListDatabaseName.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(selectedDatabase))
            {
                MessageBox.Show("Please enter both username, password and select a database.");
                return;
            }

            string connectionString = "Data Source=" + ComboBoxserverName.Text + ";Database=Master;Integrated Security=True;";
            using (con = OpenConnection(connectionString))
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    AddUser(userName, password, selectedDatabase);
                }
                else
                {
                    MessageBox.Show("Unable to connect to the server.");
                }
            }
        }

        private void ScheduleDatabaseBackup_Load(object sender, EventArgs e)
        {

            LoadUsers();
            LoadPermissions();
            serverName(".");
            // Handle form load event if needed
        }
        private void LoadUsers()
        {
            ComboBoxUsers.Items.Clear();
            string connectionString = "Data Source=" + ComboBoxserverName.Text + ";Database=Master;Integrated Security=True;";
            using (con = OpenConnection(connectionString))
            {
                using (cmd = new SqlCommand("select name from sys.sql_logins", con))
                {
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ComboBoxUsers.Items.Add(dr[0]);
                    }
                    dr.Close();
                }
            }
        }

        private void LoadPermissions()
        {
            // Add permissions to the checked list box
            checkedListBoxPermissions.Items.Add("SELECT");
            checkedListBoxPermissions.Items.Add("INSERT");
            checkedListBoxPermissions.Items.Add("UPDATE");
            checkedListBoxPermissions.Items.Add("DELETE");
            checkedListBoxPermissions.Items.Add("EXECUTE");
        }
        private void GrantPermissions(string userName, string[] permissions, string database)
        {
            try
            {
                using (SqlConnection con = OpenConnection($"Data Source={ComboBoxserverName.Text};Database={database};Integrated Security=True;"))
                {
                    foreach (string permission in permissions)
                    {
                        string queryGrant = $"GRANT {permission} TO {userName}";
                        using (SqlCommand cmdGrant = new SqlCommand(queryGrant, con))
                        {
                            cmdGrant.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Permissions granted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void ButtonGrantPermissions_Click(object sender, EventArgs e)
        {
            string selectedUser = ComboBoxUsers.SelectedItem?.ToString();
            string selectedDatabase = checkedListDatabaseName.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(selectedUser) || string.IsNullOrWhiteSpace(selectedDatabase))
            {
                MessageBox.Show("Please select a user and a database.");
                return;
            }

            var selectedPermissions = new System.Collections.Generic.List<string>();
            foreach (var item in checkedListBoxPermissions.CheckedItems)
            {
                selectedPermissions.Add(item.ToString());
            }

            GrantPermissions(selectedUser, selectedPermissions.ToArray(), selectedDatabase);
        }
        public void serverName(string str)
        {
            string connectionString = "Data Source=" + str + ";Database=Master;Integrated Security=True;";
            using (con = OpenConnection(connectionString))
            {
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
            string connectionString = "Data Source=" + ComboBoxserverName.Text + ";Database=Master;Integrated Security=True;";
            using (con = OpenConnection(connectionString))
            {
                checkedListDatabaseName.Items.Clear();
                using (cmd = new SqlCommand("select * from sysdatabases", con))
                {
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        checkedListDatabaseName.Items.Add(dr[0]);
                        databasedetails.Add(dr[0].ToString(), dr[1].ToString());
                    }
                    dr.Close();
                }
            }
        }

        private void ComboBoxserverName_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Createconnection();
        }

        private void checkedListDatabaseName_SelectedIndexChanged(object sender, EventArgs e)
        {

            InfoOfDBTables(checkedListDatabaseName.Text);

            labelDBDetails.Text = checkedListDatabaseName.Text;
            string dbId = databasedetails[checkedListDatabaseName.Text];
            string connectionString = "Data Source=" + ComboBoxserverName.Text + ";Database=Master;Integrated Security=True;";

            using (con = OpenConnection(connectionString))
            {
                using (cmd = new SqlCommand($"SELECT * FROM sys.master_files WHERE database_id={dbId}", con))
                {
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        label1PhysicalName.Text = dr[7].ToString();
                        label1.Text = dr[9].ToString();
                        label2.Text = dr[4].ToString();
                        label3.Text = dr[12].ToString();
                        label5.Text = dr[2].ToString();
                        label6.Text = dr[6].ToString();
                        label7.Text = "ملف الوق";
                        label8.Text = "دليل الملف";
                        label10.Text = "التوسع";
                        label11.Text = "نوع القرص";
                        label12.Text = "حالة القرص";
                        label14.Text = "موقع الملف";
                    }
                    dr.Close();
                }
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void labelDBDetails_Click(object sender, EventArgs e)
        {

        }
    }



}
