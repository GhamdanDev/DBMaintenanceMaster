

 

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



    public partial class DataBase_Info_Fix : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private DatabaseFileChecker fileChecker;

        public DataBase_Info_Fix()
        {
            InitializeComponent();

        }

        private void btnCheckFiles_Click(object sender, EventArgs e)
        {
            fileChecker = new DatabaseFileChecker("Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;");
            DataTable result = fileChecker.CheckDatabaseFileLogicalNames();
            dataGridView1.DataSource = result;
        }

        private void btnFixFiles_Click(object sender, EventArgs e)
        {
            DataTable result = (DataTable)dataGridView1.DataSource;
            if (result != null)
            {
                fileChecker.FixLogicalNames(result);
                MessageBox.Show("Logical names corrected successfully!");
            }
            else
            {
                MessageBox.Show("No data to fix.");
            }
        }

        private void LoadDatabaseExtendedProperties()
        {
  
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True; "; // استبدل بقيمة سلسلة الاتصال الخاصة بك
            string query = @"
                IF OBJECT_ID('tempdb..#results') IS NOT NULL DROP TABLE #results;
                CREATE TABLE #results
                (
                    DatabaseName sysname NOT NULL,
                    ApplicationName sysname NULL,
                    Owner sysname NULL
                );

                INSERT INTO #results
                EXEC sp_ineachdb '
                IF OBJECT_ID(''tempdb..#dbs'') IS NOT NULL DROP TABLE #dbs;
                IF OBJECT_ID(''tempdb..#props'') IS NOT NULL DROP TABLE #props;

                CREATE TABLE #dbs
                (
                    DatabaseName sysname NOT NULL
                );

                INSERT INTO #dbs
                (
                    DatabaseName
                )
                SELECT DB_NAME() AS DatabaseName;

                CREATE TABLE #props
                (
                    DatabaseName sysname NOT NULL,
                    PropertyName sysname NULL,
                    PropertyValue sysname NULL
                );

                INSERT INTO #props
                (
                    DatabaseName,
                    PropertyName,
                    PropertyValue
                )
                SELECT DB_NAME() AS DatabaseName,
                       name AS PropertyName,
                       CAST(value AS sysname) AS PropertyValue
                FROM sys.extended_properties
                WHERE class_desc = ''DATABASE''
                      AND name IN ( ''Application Name'', ''Owner'' );

                INSERT INTO #results
                (
                    DatabaseName,
                    ApplicationName,
                    Owner
                )
                SELECT pivot_table.DatabaseName,
                       pivot_table.[Application Name],
                       pivot_table.Owner
                FROM
                (
                    SELECT d.DatabaseName,
                           p.PropertyName,
                           p.PropertyValue
                    FROM #dbs AS d
                        LEFT OUTER JOIN #props AS p
                            ON d.DatabaseName = p.DatabaseName
                ) t
                PIVOT
                (
                    MIN(PropertyValue)
                    FOR PropertyName IN ([Application Name], Owner)
                ) AS pivot_table;
                ',
                @user_only = 1;

                SELECT DatabaseName,
                       ApplicationName,
                       Owner
                FROM #results
                ORDER BY DatabaseName;
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    
    private void btnCheckPhysicalFiles_Click(object sender, EventArgs e)
        {

            fileChecker = new DatabaseFileChecker("Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;");
            DataTable result = fileChecker.GetPhysicalFileRenameCommands();
            dataGridView1.DataSource = result;
        }

        private void btnFixPhysicalFiles_Click(object sender, EventArgs e)
        {
            DataTable result = (DataTable)dataGridView1.DataSource;
            if (result != null)
            {
                fileChecker.RenamePhysicalFiles(result);
                MessageBox.Show("Physical file names corrected successfully!");
            }
            else
            {
                MessageBox.Show("No data to fix.");
            }
        }

        public void serverName(string str)
        {
            con = new SqlConnection("Data Source=" + str + "; Integrated Security=True;");
            con.Open();
            cmd = new SqlCommand("SELECT * FROM sys.servers WHERE product = 'SQL Server'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ComboBoxserverName.Items.Add(dr["name"]);
            }
            dr.Close();
        }

        public void Createconnection()
        {
            con = new SqlConnection("Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;");
            con.Open();
            ComboBoxDatabaseName.Items.Clear();
            cmd = new SqlCommand("SELECT name FROM sys.databases", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ComboBoxDatabaseName.Items.Add(dr["name"]);
            }
            dr.Close();
        }

        private void DataBase_Info_Fix_Load(object sender, EventArgs e)
        {
            serverName(".");
            LoadCollations();
        }

        private void ComboBoxserverName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Createconnection();
        }

        private void ComboBoxDatabaseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ComboBoxDatabaseName.Text))
            {
                LoadData(ComboBoxDatabaseName.Text);
            }
        }




        private void LoadData(string databaseName)
        {
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand useDbCmd = new SqlCommand("USE MySimpleDB", conn))
                    {
                        useDbCmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_GetDatabaseInfo", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DatabaseName", databaseName);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        // دمج جميع الجداول في جدول واحد
                        DataTable mergedTable = new DataTable();
                        foreach (DataTable table in ds.Tables)
                        {
                            mergedTable.Merge(table);
                        }

                        // عرض النتائج في DataGridView
                        dataGridView1.DataSource = mergedTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        private void LoadDataCountInfoDB(string databaseName)

        {
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand useDbCmd = new SqlCommand("USE MySimpleDB", conn))
                    {
                        useDbCmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_GetDatabaseCountInfo", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DatabaseName", databaseName);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        // دمج جميع الجداول في جدول واحد
                        DataTable mergedTable = new DataTable();
                        foreach (DataTable table in ds.Tables)
                        {
                            mergedTable.Merge(table);
                        }

                        // عرض النتائج في DataGridView
                        dataGridView2.DataSource = mergedTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        private void LoadDataCountInfoDB2(string databaseName)
        {
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand useDbCmd = new SqlCommand("USE MySimpleDB", conn))
                    {
                        useDbCmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_GetDatabaseCountInfoV5", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DatabaseName", databaseName);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        // دمج جميع الجداول في جدول واحد
                        DataTable mergedTable = new DataTable();
                        mergedTable.Columns.Add("الاسم", typeof(string));
                        mergedTable.Columns.Add("العدد", typeof(int));

                        foreach (DataTable table in ds.Tables)
                        {

                            //foreach (DataRow row in table.Rows)
                            //{
                            //    mergedTable.Rows.Add(table.TableName, row[0]);
                            //}

                            string tableName = GetFriendlyTableName(table.TableName);
                            foreach (DataRow row in table.Rows)
                            {
                                mergedTable.Rows.Add(tableName, row[0]);
                            }
                        }

                        // عرض النتائج في DataGridView
                        dataGridView2.DataSource = mergedTable;
                        dataGridView2.Columns["الاسم"].Width = 200;
                        dataGridView2.Columns["العدد"].Width = 100;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        private string GetFriendlyTableName(string tableName)
        {
            switch (tableName)
            {

                case "Table1":
                    return "عدد الفهارس";
                case "Table2":
                    return "عدد المفاتيح الفرعيه";
                case "Table3":
                    return "عدد القوادح";
                case "Table4":
                    return "عدد القوادح المفعله";
                case "Table5":
                    return "عدد القوادح المعطله";
                case "Table6":
                    return "عدد العروض";

                // Add more cases as needed
                default:
                    return "عدد الجداول";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ComboBoxDatabaseName.Text))
            {
                LoadData(ComboBoxDatabaseName.Text);
            }

            MessageBox.Show("select Database name");

        }

        private void ComboBoxDatabaseName_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ComboBoxDatabaseName.Text))
            {
                LoadDataCountInfoDB(ComboBoxDatabaseName.Text);
                LoadDataCountInfoDB2(ComboBoxDatabaseName.Text);
                LoadData(ComboBoxDatabaseName.Text);
            }
            else
                MessageBox.Show("select Database name");

        }

        private void button1CreateIndexes_Click(object sender, EventArgs e)
        {
            // CreateIndexes(ComboBoxDatabaseName.Text);
            AddIndexes(ComboBoxDatabaseName.Text);

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void AnalyzeIndexes(string databaseName)
        {
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand useDbCmd = new SqlCommand($"USE {databaseName}", conn))
                    {
                        useDbCmd.ExecuteNonQuery();
                    }

                    string query = @"
               SELECT 
                   OBJECT_NAME(i.object_id) AS TableName,
                   i.name AS IndexName,
                   i.type_desc AS IndexType,
                   dm_usage.user_seeks,
                   dm_usage.user_scans,
                   dm_usage.user_lookups,
                   dm_usage.user_updates
               FROM 
                   sys.indexes AS i
               LEFT OUTER JOIN 
                   sys.dm_db_index_usage_stats AS dm_usage
                   ON i.object_id = dm_usage.object_id AND i.index_id = dm_usage.index_id
               WHERE 
                   OBJECTPROPERTY(i.object_id, 'IsUserTable') = 1
               ORDER BY 
                   OBJECT_NAME(i.object_id), i.name;";

                    // تهيئة RichTextBox للعرض
                    RichTextBox rtb = new RichTextBox();
                    rtb.Dock = DockStyle.Fill;
                    rtb.ReadOnly = true;
                    rtb.ScrollBars = RichTextBoxScrollBars.Both;

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tableName = reader["TableName"].ToString();
                                string indexName = reader["IndexName"].ToString();
                                string indexType = reader["IndexType"].ToString();
                                int userSeeks = reader["user_seeks"] != DBNull.Value ? Convert.ToInt32(reader["user_seeks"]) : 0;
                                int userScans = reader["user_scans"] != DBNull.Value ? Convert.ToInt32(reader["user_scans"]) : 0;
                                int userLookups = reader["user_lookups"] != DBNull.Value ? Convert.ToInt32(reader["user_lookups"]) : 0;
                                int userUpdates = reader["user_updates"] != DBNull.Value ? Convert.ToInt32(reader["user_updates"]) : 0;

                                rtb.AppendText($"جدول: {tableName}\n");
                                rtb.AppendText($"فهرس: {indexName} ({indexType})\n");
                                rtb.AppendText($"عمليات البحث من قبل المستخدم: {userSeeks}\n");
                                rtb.AppendText($"عمليات المسح من قبل المستخدم: {userScans}\n");
                                rtb.AppendText($"عمليات البحث المباشر من قبل المستخدم: {userLookups}\n");
                                rtb.AppendText($"عمليات التحديث من قبل المستخدم: {userUpdates}\n");
                                rtb.AppendText("-----------------------------------\n");
                            }
                        }
                    }

                    // عرض نتائج التحليل في نافذة منبثقة
                    Form resultsForm = new Form();
                    resultsForm.Text = "تحليل الفهارس";
                    resultsForm.Controls.Add(rtb);
                    resultsForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ أثناء تحليل الفهارس: " + ex.Message);
                }
            }
        }
        private void RebuildIndexes(string databaseName)
        {
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand useDbCmd = new SqlCommand($"USE {databaseName}", conn))
                    {
                        useDbCmd.ExecuteNonQuery();
                    }

                    string query = "EXEC sp_MSforeachtable @command1 = 'ALTER INDEX ALL ON ? REBUILD';";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Indexes rebuilt successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while rebuilding indexes: " + ex.Message);
                }
            }
        }
        private void ReorganizeIndexes(string databaseName)
        {
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand useDbCmd = new SqlCommand($"USE {databaseName}", conn))
                    {
                        useDbCmd.ExecuteNonQuery();
                    }

                    string query = "EXEC sp_MSforeachtable @command1 = 'ALTER INDEX ALL ON ? REORGANIZE';";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Indexes reorganized successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while reorganizing indexes: " + ex.Message);
                }
            }
        }
        private void AddIndexes(string databaseName)
        {
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand useDbCmd = new SqlCommand($"USE {databaseName}", conn))
                    {
                        useDbCmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = @"
                   DECLARE @tableName NVARCHAR(128);
                   DECLARE @columnName NVARCHAR(128);
                   DECLARE @indexName NVARCHAR(128);
                   DECLARE @sql NVARCHAR(MAX);

                   DECLARE table_cursor CURSOR FOR
                   SELECT name FROM sys.tables;

                   OPEN table_cursor;
                   FETCH NEXT FROM table_cursor INTO @tableName;

                   WHILE @@FETCH_STATUS = 0
                   BEGIN
                       DECLARE column_cursor CURSOR FOR
                       SELECT name FROM sys.columns WHERE object_id = OBJECT_ID(@tableName);

                       OPEN column_cursor;
                       FETCH NEXT FROM column_cursor INTO @columnName;

                       WHILE @@FETCH_STATUS = 0
                       BEGIN
                           SET @indexName = 'IX_' + @tableName + '_' + @columnName;

                           -- تحقق من عدم وجود الفهرس
                           IF NOT EXISTS (
                               SELECT 1 
                               FROM sys.indexes 
                               WHERE name = @indexName 
                               AND object_id = OBJECT_ID(@tableName)
                           )
                           BEGIN
                               SET @sql = 'CREATE INDEX ' + @indexName + ' ON ' + @tableName + ' (' + @columnName + ');';
                               EXEC sp_executesql @sql;
                           END

                           FETCH NEXT FROM column_cursor INTO @columnName;
                       END

                       CLOSE column_cursor;
                       DEALLOCATE column_cursor;

                       FETCH NEXT FROM table_cursor INTO @tableName;
                   END

                   CLOSE table_cursor;
                   DEALLOCATE table_cursor;";
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Indexes added successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while adding indexes: " + ex.Message);
                }
            }
        }
        /*  private void CreateDatabaseAudit(string databaseName, string auditPath)
          {
              string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";
              using (SqlConnection conn = new SqlConnection(connectionString))
              {
                  try
                  {
                      conn.Open();
                      using (SqlCommand useDbCmd = new SqlCommand($"USE {databaseName}", conn))
                      {
                          useDbCmd.ExecuteNonQuery();
                      }

                      string createDirSql = "EXEC master.sys.xp_create_subdir @path";
                      using (SqlCommand cmd = new SqlCommand(createDirSql, conn))
                      {
                          cmd.Parameters.AddWithValue("@path", auditPath);
                          cmd.ExecuteNonQuery();
                      }

                      string auditSql = @"
  USE master;
  CREATE SERVER AUDIT [" + databaseName + @"] 
  TO FILE ( FILEPATH = @AuditPath, MAXSIZE = 1GB, MAX_ROLLOVER_FILES = 15, RESERVE_DISK_SPACE = OFF ) 
  WITH ( QUEUE_DELAY = 1000 );
  ALTER SERVER AUDIT [" + databaseName + @"] WITH (STATE = ON);
  USE [" + databaseName + @"];
  CREATE DATABASE AUDIT SPECIFICATION [" + databaseName + @"]
  FOR SERVER AUDIT [" + databaseName + @"]
  ADD (SCHEMA_OBJECT_ACCESS_GROUP) WITH (STATE = ON);
  ";

                      using (SqlCommand cmd = new SqlCommand(auditSql, conn))
                      {
                          cmd.Parameters.AddWithValue("@AuditPath", auditPath);
                          cmd.ExecuteNonQuery();
                      }

                      MessageBox.Show("Database audit created successfully!");
                  }
                  catch (Exception ex)
                  {
                      //MessageBox.Show("An error occurred while creating database audit: " + ex.Message);
                      ShowErrorMessage("An error occurred while creating database audit: " + ex.Message);

                  }
              }
          }
  */


        private void CreateDatabaseAudit(string databaseName, string auditPath)
        {
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // إنشاء مجلد مسار التدقيق
                    string createDirSql = "EXEC master.sys.xp_create_subdir @path";
                    using (SqlCommand cmd = new SqlCommand(createDirSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@path", auditPath);
                        cmd.ExecuteNonQuery();
                    }

                    // إنشاء تدقيق على مستوى الخادم
                    string createServerAuditSql = @"
CREATE SERVER AUDIT [" + databaseName + @"] 
TO FILE ( FILEPATH = '" + auditPath + @"', MAXSIZE = 1GB, MAX_ROLLOVER_FILES = 15, RESERVE_DISK_SPACE = OFF ) 
WITH ( QUEUE_DELAY = 1000, ON_FAILURE = SHUTDOWN );
ALTER SERVER AUDIT [" + databaseName + @"] WITH (STATE = ON);";
                    using (SqlCommand cmd = new SqlCommand(createServerAuditSql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // استخدام قاعدة البيانات المستهدفة
                    using (SqlCommand useDbCmd = new SqlCommand($"USE {databaseName}", conn))
                    {
                        useDbCmd.ExecuteNonQuery();
                    }

                    // إنشاء تدقيق على مستوى قاعدة البيانات
                    string createDatabaseAuditSql = @"
CREATE DATABASE AUDIT SPECIFICATION [" + databaseName + @"]
FOR SERVER AUDIT [" + databaseName + @"]
ADD (SCHEMA_OBJECT_ACCESS_GROUP) WITH (STATE = ON);";
                    using (SqlCommand cmd = new SqlCommand(createDatabaseAuditSql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Database audit created successfully!");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("An error occurred while creating database audit: " + ex.Message);
                }
            }
        }

        /*    private void DisplayAuditData(string auditFilePath)
            {
                string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string query = @"
    SELECT *
    FROM sys.fn_get_audit_file(@auditFilePath, DEFAULT, DEFAULT);";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@auditFilePath", auditFilePath);
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable dataTable = new DataTable();

                            try
                            {
                                adapter.Fill(dataTable);

                                // Check if dataTable contains any rows
                                if (dataTable.Rows.Count == 0)
                                {
                                    // Display message if no data is found
                                    MessageBox.Show("No data found in the audit file.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dataGridView1.DataSource = null; // Clear DataGridView if no data
                                }
                                else
                                {
                                    // Display data if available
                                    dataGridView1.DataSource = dataTable;
                                }
                            }
                            catch (Exception ex)
                            {
                                // If there is an error while filling the DataTable
                                ShowErrorMessage("An error occurred while processing audit data: " + ex.Message);
                                dataGridView1.DataSource = null; // Clear DataGridView in case of error
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("An error occurred while fetching audit data: " + ex.Message);
                        dataGridView1.DataSource = null; // Clear DataGridView in case of error
                    }
                }
            }
    */


        private void DisplayAuditData(string auditFilePath)
        {
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"
SELECT *
FROM sys.fn_get_audit_file(@auditFilePath, DEFAULT, DEFAULT);";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@auditFilePath", auditFilePath);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();

                        try
                        {
                            adapter.Fill(dataTable);

                            // Check if dataTable contains any rows
                            if (dataTable.Rows.Count == 0)
                            {
                                // Display message if no data is found
                                MessageBox.Show("No data found in the audit file.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dataGridView1.DataSource = null; // Clear DataGridView if no data
                            }
                            else
                            {
                                // Create a new DataTable to hold the displayable data
                                DataTable displayTable = dataTable.Clone();

                                // Convert byte array columns to string for display
                                foreach (DataColumn column in dataTable.Columns)
                                {
                                    if (column.DataType == typeof(byte[]))
                                    {
                                        displayTable.Columns[column.ColumnName].DataType = typeof(string);
                                    }
                                }

                                // Copy and convert data from original table to display table
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    DataRow newRow = displayTable.NewRow();
                                    foreach (DataColumn column in dataTable.Columns)
                                    {
                                        if (column.DataType == typeof(byte[]))
                                        {
                                            // Check for DBNull before conversion
                                            if (row[column] == DBNull.Value)
                                            {
                                                newRow[column.ColumnName] = DBNull.Value;
                                            }
                                            else
                                            {
                                                newRow[column.ColumnName] = BitConverter.ToString((byte[])row[column]);
                                            }
                                        }
                                        else
                                        {
                                            newRow[column.ColumnName] = row[column];
                                        }
                                    }
                                    displayTable.Rows.Add(newRow);
                                }

                                // Display the data in the DataGridView
                                dataGridView1.DataSource = displayTable;
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            // If there is an SQL specific error
                            ShowErrorMessage("An SQL error occurred while processing audit data: " + sqlEx.Message);
                            dataGridView1.DataSource = null; // Clear DataGridView in case of error
                        }
                        catch (Exception ex)
                        {
                            // If there is a general error while filling the DataTable
                            ShowErrorMessage("An error occurred while processing audit data: " + ex.Message);
                            dataGridView1.DataSource = null; // Clear DataGridView in case of error
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    // If there is an SQL specific error
                    ShowErrorMessage("An SQL error occurred while fetching audit data: " + sqlEx.Message);
                    dataGridView1.DataSource = null; // Clear DataGridView in case of error
                }
                catch (Exception ex)
                {
                    // If there is a general error while connecting to the database
                    ShowErrorMessage("An error occurred while fetching audit data: " + ex.Message);
                    dataGridView1.DataSource = null; // Clear DataGridView in case of error
                }
            }
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

       
        private void SHRINKFILE(string databaseName)
        {
            // استبدل قيمة connectionString بقيمة الاتصال بقاعدة البيانات الخاصة بك
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";

            // تضمين أمر USE database وأوامر DBCC SHRINKFILE ضمن النص الخاص بالأمر
            string query = $@"
           USE {databaseName};
           DBCC SHRINKFILE ('{databaseName}', 0);
           DBCC SHRINKFILE ('{databaseName}_log', 0);
       ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                // لا حاجة لإضافة المعلمات هنا لأننا ندمج قيم المعلمات مباشرة في نص الاستعلام

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("تم تنفيذ الأمر بنجاح");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"حدث خطأ: {ex.Message}");
                }
            }
        }
        //Changing Database Collation
        /*private void LoadCollations()
        {
            string query = "SELECT name FROM sys.fn_helpcollations();";
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable collationsTable = new DataTable();
                adapter.Fill(collationsTable);

                cbNewCollation.DataSource = collationsTable;
                cbNewCollation.DisplayMember = "name";
                cbNewCollation.ValueMember = "name";
            }
        }

        private string GenerateChangeCollationScript(string newCollation)
        {
            // Combine the script parts into a single script
            string script = @"
        DECLARE @NewCollation VARCHAR(128) = '" + newCollation + @"';

        SELECT IDENTITY(INT, 1, 1) AS ID, 0 AS ExecutionOrder,
               CAST('--Suite of commands to change collation of all columns that are not currently ' + QUOTENAME(@NewCollation) AS VARCHAR(MAX)) AS Command
        INTO #Results;

        SELECT objz.object_id, SCHEMA_NAME(objz.schema_id) AS SchemaName, objz.name AS TableName, colz.name AS ColumnName,
               colz.collation_name, colz.column_id
        INTO #MyAffectedTables
        FROM sys.columns colz
            INNER JOIN sys.tables objz ON colz.object_id = objz.object_id
        WHERE colz.collation_name IS NOT NULL
              AND objz.is_ms_shipped = 0
              AND colz.is_computed = 0
              AND colz.collation_name <> @NewCollation;

        INSERT INTO #Results (ExecutionOrder, Command)
        SELECT 10, CONVERT(VARCHAR(8000), 'ALTER TABLE ' + QUOTENAME(tabz.SchemaName) + '.' + QUOTENAME(tabz.TableName) + ' DROP CONSTRAINT ' + QUOTENAME(conz.name) + ';')
        FROM sys.check_constraints conz
            INNER JOIN #MyAffectedTables tabz ON conz.parent_object_id = tabz.object_id AND conz.parent_column_id = tabz.column_id;

        INSERT INTO #Results (ExecutionOrder, Command)
        SELECT 100, CONVERT(VARCHAR(8000), 'ALTER TABLE ' + QUOTENAME(tabz.SchemaName) + '.' + QUOTENAME(tabz.TableName) + ' ADD CONSTRAINT ' + QUOTENAME(conz.name) + ' CHECK ' + conz.definition + ';')
        FROM sys.check_constraints conz
            INNER JOIN #MyAffectedTables tabz ON conz.parent_object_id = tabz.object_id AND conz.parent_column_id = tabz.column_id;

        INSERT INTO #Results (ExecutionOrder, Command)
        SELECT 20, CONVERT(VARCHAR(8000), 'ALTER TABLE ' + QUOTENAME(tabz.SchemaName) + '.' + QUOTENAME(tabz.TableName) + ' DROP CONSTRAINT ' + QUOTENAME(conz.name) + ';')
        FROM sys.default_constraints conz
            INNER JOIN #MyAffectedTables tabz ON conz.parent_object_id = tabz.object_id AND conz.parent_column_id = tabz.column_id;

        INSERT INTO #Results (ExecutionOrder, Command)
        SELECT 200, CONVERT(VARCHAR(8000), 'ALTER TABLE ' + QUOTENAME(tabz.SchemaName) + '.' + QUOTENAME(tabz.TableName) + ' ADD CONSTRAINT ' + QUOTENAME(conz.name) + ' DEFAULT ' + conz.definition + ' FOR ' + QUOTENAME(tabz.ColumnName) + ';')
        FROM sys.default_constraints conz
            INNER JOIN #MyAffectedTables tabz ON conz.parent_object_id = tabz.object_id AND conz.parent_column_id = tabz.column_id;

        INSERT INTO #Results (ExecutionOrder, Command)
        SELECT DISTINCT 30, 'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(objz.schema_id)) + '.' + QUOTENAME(objz.name) + ' DROP COLUMN ' + QUOTENAME(colz.name) + ';'
        FROM sys.columns colz
            LEFT OUTER JOIN sys.tables objz ON colz.[object_id] = objz.[object_id]
            LEFT OUTER JOIN sys.computed_columns CALC ON colz.[object_id] = CALC.[object_id] AND colz.[column_id] = CALC.[column_id]
            LEFT OUTER JOIN sys.sql_expression_dependencies depz ON colz.object_id = depz.referenced_id AND colz.column_id = depz.referencing_minor_id
            INNER JOIN #MyAffectedTables tabz ON depz.referenced_id = tabz.object_id AND depz.referenced_minor_id = tabz.column_id
        WHERE colz.is_computed = 1;

        INSERT INTO #Results (ExecutionOrder, Command)
        SELECT DISTINCT 300, 'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(objz.schema_id)) + '.' + QUOTENAME(objz.name) + ' ADD ' + QUOTENAME(colz.name) + ' AS ' + ISNULL(CALC.definition, '') + CASE WHEN CALC.is_persisted = 1 THEN ' PERSISTED' ELSE '' END + ';'
        FROM sys.columns colz
            LEFT OUTER JOIN sys.tables objz ON colz.[object_id] = objz.[object_id]
            LEFT OUTER JOIN sys.computed_columns CALC ON colz.[object_id] = CALC.[object_id] AND colz.[column_id] = CALC.[column_id]
            LEFT OUTER JOIN sys.sql_expression_dependencies depz ON colz.object_id = depz.referenced_id AND colz.column_id = depz.referencing_minor_id
            INNER JOIN #MyAffectedTables tabz ON depz.referenced_id = tabz.object_id AND depz.referenced_minor_id = tabz.column_id
        WHERE colz.is_computed = 1;

        INSERT INTO #Results (ExecutionOrder, Command)
        SELECT DISTINCT 40, CONVERT(VARCHAR(8000), 'ALTER TABLE ' + QUOTENAME(tabz.SchemaName) + '.' + QUOTENAME(tabz.TableName) + ' DROP CONSTRAINT ' + QUOTENAME(conz.name) + ';')
        FROM sys.foreign_keys conz
            INNER JOIN sys.foreign_key_columns colz ON conz.object_id = colz.constraint_object_id
            INNER JOIN #MyAffectedTables tabz ON conz.parent_object_id = tabz.object_id
        WHERE tabz.object_id = colz.parent_object_id AND tabz.column_id = colz.parent_column_id;

        INSERT INTO #Results (ExecutionOrder, Command)
        SELECT DISTINCT 850, 'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(conz.schema_id)) + '.' + QUOTENAME(OBJECT_NAME(conz.parent_object_id)) + ' ADD CONSTRAINT ' + QUOTENAME(conz.name) + ' FOREIGN KEY (' + STUFF((SELECT ', ' + QUOTENAME(colz2.name)
              FROM sys.foreign_key_columns colz2
              WHERE colz2.parent_object_id = colz.parent_object_id
                AND colz2.parent_column_id = colz.parent_column_id
                AND colz2.constraint_object_id = colz.constraint_object_id
              ORDER BY colz2.constraint_column_id
              FOR XML PATH ('')), 1, 1, '') + ') REFERENCES ' + QUOTENAME(SCHEMA_NAME(objz.schema_id)) + '.' + QUOTENAME(objz.name) + ' (' + STUFF((SELECT ', ' + QUOTENAME(colz2.name)
              FROM sys.foreign_key_columns colz2
              WHERE colz2.referenced_object_id = colz.referenced_object_id
                AND colz2.referenced_column_id = colz.referenced_column_id
                AND colz2.constraint_object_id = colz.constraint_object_id
              ORDER BY colz2.constraint_column_id
              FOR XML PATH ('')), 1, 1, '') + ');'
        FROM sys.foreign_keys conz
            INNER JOIN sys.foreign_key_columns colz ON conz.object_id = colz.constraint_object_id
            INNER JOIN sys.columns colz2 ON colz.referenced_object_id = colz2.object_id
                AND colz.referenced_column_id = colz2.column_id
            INNER JOIN sys.tables objz ON colz2.object_id = objz.object_id
            INNER JOIN #MyAffectedTables tabz ON conz.parent_object_id = tabz.object_id
        WHERE tabz.object_id = colz.parent_object_id AND tabz.column_id = colz.parent_column_id;

        INSERT INTO #Results (ExecutionOrder, Command)
        SELECT DISTINCT 50, CONVERT(VARCHAR(8000), 'ALTER INDEX ' + QUOTENAME(indz.name) + ' ON ' + QUOTENAME(SCHEMA_NAME(objz.schema_id)) + '.' + QUOTENAME(objz.name) + ' DISABLE;')
        FROM sys.indexes indz
            INNER JOIN sys.index_columns colz ON indz.object_id = colz.object_id AND indz.index_id = colz.index_id
            INNER JOIN #MyAffectedTables tabz ON colz.object_id = tabz.object_id
        WHERE tabz.object_id = colz.object_id AND tabz.column_id = colz.column_id AND indz.type_desc = 'NONCLUSTERED';

        INSERT INTO #Results (ExecutionOrder, Command)
        SELECT DISTINCT 600, CONVERT(VARCHAR(8000), 'ALTER INDEX ' + QUOTENAME(indz.name) + ' ON ' + QUOTENAME(SCHEMA_NAME(objz.schema_id)) + '.' + QUOTENAME(objz.name) + ' REBUILD;')
        FROM sys.indexes indz
            INNER JOIN sys.index_columns colz ON indz.object_id = colz.object_id AND indz.index_id = colz.index_id
            INNER JOIN #MyAffectedTables tabz ON colz.object_id = tabz.object_id
        WHERE tabz.object_id = colz.object_id AND tabz.column_id = colz.column_id AND indz.type_desc = 'NONCLUSTERED';

        INSERT INTO #Results (ExecutionOrder, Command)
        SELECT 900, CONVERT(VARCHAR(8000), 'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(objz.schema_id)) + '.' + QUOTENAME(objz.name) + ' ALTER COLUMN ' + QUOTENAME(colz.name) + ' ' + typz.name + CASE typz.name
            WHEN 'char' THEN '(' + CONVERT(VARCHAR, colz.max_length) + ')'
            WHEN 'varchar' THEN CASE colz.max_length
                WHEN -1 THEN '(MAX)'
                ELSE '(' + CONVERT(VARCHAR, colz.max_length) + ')'
                END
            WHEN 'nchar' THEN '(' + CONVERT(VARCHAR, colz.max_length / 2) + ')'
            WHEN 'nvarchar' THEN CASE colz.max_length
                WHEN -1 THEN '(MAX)'
                ELSE '(' + CONVERT(VARCHAR, colz.max_length / 2) + ')'
                END
            WHEN 'binary' THEN '(' + CONVERT(VARCHAR, colz.max_length) + ')'
            WHEN 'varbinary' THEN CASE colz.max_length
                WHEN -1 THEN '(MAX)'
                ELSE '(' + CONVERT(VARCHAR, colz.max_length) + ')'
                END
            ELSE ''
            END + ' COLLATE ' + @NewCollation + CASE colz.is_nullable
            WHEN 1 THEN ' NULL'
            ELSE ' NOT NULL'
            END + ';')
        FROM sys.columns colz
            LEFT OUTER JOIN sys.tables objz ON colz.[object_id] = objz.[object_id]
            LEFT OUTER JOIN sys.computed_columns CALC ON colz.[object_id] = CALC.[object_id] AND colz.[column_id] = CALC.[column_id]
            LEFT OUTER JOIN sys.sql_expression_dependencies depz ON colz.object_id = depz.referenced_id AND colz.column_id = depz.referencing_minor_id
            INNER JOIN sys.types typz ON colz.system_type_id = typz.system_type_id AND colz.user_type_id = typz.user_type_id
            INNER JOIN #MyAffectedTables tabz ON colz.[object_id] = tabz.[object_id] AND colz.[column_id] = tabz.[column_id]
        WHERE objz.is_ms_shipped = 0 AND colz.is_computed = 0;

        SELECT Command
        FROM #Results
        ORDER BY ExecutionOrder, ID;
    ";

            return script;
        }

        
        private void btnApplyCollation_Click_1(object sender, EventArgs e)
        {
            string newCollation = cbNewCollation.SelectedValue.ToString();

            if (string.IsNullOrEmpty(newCollation))
            {
                MessageBox.Show("Please select a new collation.");
                return;
            }

            string changeCollationScript = GenerateChangeCollationScript(newCollation);
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand(changeCollationScript, conn, transaction);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("Collation change applied successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //MessageBox.Show("Error applying collation change: " + ex.Message);

                    ShowErrorMessage("Error applying collation change: " + ex.Message);
                }
            }
        }
     */


        private void LoadCollations()
        {
            string query = "SELECT name FROM sys.fn_helpcollations()";  // إزالة الفاصلة المنقوطة الزائدة
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable collationsTable = new DataTable();
                adapter.Fill(collationsTable);

                cbNewCollation.DataSource = collationsTable;
                cbNewCollation.DisplayMember = "name";
                cbNewCollation.ValueMember = "name";
            }
        }

        private void btnApplyCollation_Click_1(object sender, EventArgs e)
        {
            //string newCollation = cbNewCollation.SelectedValue?.ToString();
            string newCollation = cbNewCollation.Text;

            if (string.IsNullOrEmpty(newCollation))
            {
                MessageBox.Show("Please select a new collation.");
                return;
            }

            string changeCollationScript = GenerateChangeCollationScript(newCollation);
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand(changeCollationScript, conn, transaction);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("Collation change applied successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ShowErrorMessage("Error applying collation change: " + ex.Message);
                }
            }
        }


        private string GenerateChangeCollationScript(string newCollation)
        {
            string script = $@"
    DECLARE @NewCollation VARCHAR(128) = '{cbNewCollation.Text}'; -- change this to the collation that you need
--WAS 'SQL_Latin1_General_CP1_CI_AS'
--toggling 'Latin1_General_CI_AS' 


--create table on the fly, add the DROP CONSTRAINTS
SELECT IDENTITY(INT, 1, 1) AS ID,
       0 AS ExecutionOrder,
       CAST('--Suite of commands to change collation of all columns that are not currently ' + QUOTENAME(@NewCollation) AS VARCHAR(MAX)) AS Command
INTO #Results;
--#################################################################################################
--Start a transaction? might cause huge bloating of the transaction log, but too bad.
--#################################################################################################  
----INSERT INTO #Results
----            (ExecutionOrder,Command)
----SELECT 1,'SET XACT_ABORT ON' UNION ALL
----SELECT 2,'BEGIN TRAN' UNION ALL
----SELECT 1000, 'COMMIT TRAN'           
SELECT objz.object_id,
       SCHEMA_NAME(objz.schema_id) AS SchemaName,
       objz.name AS TableName,
       colz.name AS ColumnName,
       colz.collation_name,
       colz.column_id
INTO #MyAffectedTables
FROM sys.columns colz
    INNER JOIN sys.tables objz
        ON colz.object_id = objz.object_id
WHERE colz.collation_name IS NOT NULL
      AND objz.is_ms_shipped = 0
      AND colz.is_computed = 0
      AND colz.collation_name <> @NewCollation;

--AND colz.collation_name <> @NewCollation
--################################################################################################# 
--STEP_001 check constriants
--#################################################################################################

INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT 10 AS ExecutionOrder,
       CONVERT(
                  VARCHAR(8000),
                  'ALTER TABLE ' + QUOTENAME(tabz.SchemaName) + '.' + QUOTENAME(tabz.TableName) + ' DROP CONSTRAINT '
                  + QUOTENAME(conz.name) + ';'
              ) AS Command
FROM sys.check_constraints conz
    INNER JOIN #MyAffectedTables tabz
        ON conz.parent_object_id = tabz.object_id
           AND conz.parent_column_id = tabz.column_id;

--add the recreation of the constraints.
INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT 100 AS ExecutionOrder,
       CONVERT(
                  VARCHAR(8000),
                  'ALTER TABLE ' + QUOTENAME(tabz.SchemaName) + '.' + QUOTENAME(tabz.TableName) + ' ADD CONSTRAINT '
                  + QUOTENAME(conz.name) + ' CHECK ' + conz.definition + ';'
              ) AS Command
FROM sys.check_constraints conz
    INNER JOIN #MyAffectedTables tabz
        ON conz.parent_object_id = tabz.object_id
           AND conz.parent_column_id = tabz.column_id;

--################################################################################################# 
--STEP_002 default constriants
--################################################################################################# 
/*--visualize the data
SELECT * 
FROM sys.default_constraints conz
  INNER JOIN #MyAffectedTables tabz
    ON  conz.parent_object_id = tabz.object_id
    AND conz.parent_column_id = tabz.column_id
*/
INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT 20 AS ExecutionOrder,
       CONVERT(
                  VARCHAR(8000),
                  'ALTER TABLE ' + QUOTENAME(tabz.SchemaName) + '.' + QUOTENAME(tabz.TableName) + ' DROP CONSTRAINT '
                  + QUOTENAME(conz.name) + ';'
              ) AS Command
FROM sys.default_constraints conz
    INNER JOIN #MyAffectedTables tabz
        ON conz.parent_object_id = tabz.object_id
           AND conz.parent_column_id = tabz.column_id;

INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT 200 AS ExecutionOrder,
       CONVERT(
                  VARCHAR(8000),
                  'ALTER TABLE ' + QUOTENAME(tabz.SchemaName) + '.' + QUOTENAME(tabz.TableName) + ' ADD CONSTRAINT '
                  + QUOTENAME(conz.name) + ' DEFAULT ' + conz.definition + ' FOR ' + QUOTENAME(tabz.ColumnName) + ';'
              ) AS Command
FROM sys.default_constraints conz
    INNER JOIN #MyAffectedTables tabz
        ON conz.parent_object_id = tabz.object_id
           AND conz.parent_column_id = tabz.column_id;

--################################################################################################# 
--STEP_003 calculated columns : refering internal columns to the table
--################################################################################################# 
--need distinct in case of a calculated columns appending two or more columns together: we need the definition only once.
INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT DISTINCT
       30 AS ExecutionOrder,
       'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(objz.schema_id)) + '.' + QUOTENAME(objz.name) + ' DROP COLUMN '
       + QUOTENAME(colz.name) + ';' AS Command
FROM sys.columns colz
    LEFT OUTER JOIN sys.tables objz
        ON colz.[object_id] = objz.[object_id]
    LEFT OUTER JOIN sys.computed_columns CALC
        ON colz.[object_id] = CALC.[object_id]
           AND colz.[column_id] = CALC.[column_id]
    --only calculations referencing columns
    LEFT OUTER JOIN sys.sql_expression_dependencies depz
        ON colz.object_id = depz.referenced_id
           AND colz.column_id = depz.referencing_minor_id
    INNER JOIN #MyAffectedTables tabz
        ON depz.referenced_id = tabz.object_id
           AND depz.referenced_minor_id = tabz.column_id
WHERE colz.is_computed = 1;

INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT DISTINCT
       300 AS ExecutionOrder,
       'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(objz.schema_id)) + '.' + QUOTENAME(objz.name) + ' ADD '
       + QUOTENAME(colz.name) + 'AS ' + ISNULL(CALC.definition, '') + CASE
                                                                          WHEN CALC.is_persisted = 1 THEN
                                                                              ' PERSISTED'
                                                                          ELSE
                                                                              ''
                                                                      END + ';' AS Command
FROM sys.columns colz
    LEFT OUTER JOIN sys.tables objz
        ON colz.[object_id] = objz.[object_id]
    LEFT OUTER JOIN sys.computed_columns CALC
        ON colz.[object_id] = CALC.[object_id]
           AND colz.[column_id] = CALC.[column_id]
    --only calculations referencing columns
    LEFT OUTER JOIN sys.sql_expression_dependencies depz
        ON colz.object_id = depz.referenced_id
           AND colz.column_id = depz.referencing_minor_id
    INNER JOIN #MyAffectedTables tabz
        ON depz.referenced_id = tabz.object_id
           AND depz.referenced_minor_id = tabz.column_id
WHERE colz.is_computed = 1;

--################################################################################################# 
--STEP_004 foreign key constriants :child references
--################################################################################################# 
/*--visualize the data
--at least in my case, it is very rare to have a char column as the value for a FK; my FK's are all int/bigint
--I had to create a fake pair of tables to test this.
*/
INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT DISTINCT
       40 AS ExecutionOrder,
       CONVERT(
                  VARCHAR(8000),
                  'ALTER TABLE ' + QUOTENAME(tabz.SchemaName) + '.' + QUOTENAME(tabz.TableName) + ' DROP CONSTRAINT '
                  + QUOTENAME(conz.name) + ';'
              ) AS Command
FROM sys.foreign_keys conz
    INNER JOIN sys.foreign_key_columns colz
        ON conz.object_id = colz.constraint_object_id
    INNER JOIN #MyAffectedTables tabz
        ON conz.parent_object_id = tabz.object_id
WHERE tabz.object_id = colz.parent_object_id
      AND tabz.column_id = colz.parent_column_id;

--foreign keys, potentially, can span multiple keys;  
--'scriptlet to do all FK's for reference.
INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT DISTINCT
    --FK must be added AFTER the PK/unique constraints are added back.
       850 AS ExecutionOrder,
       'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(conz.schema_id)) + '.' + QUOTENAME(OBJECT_NAME(conz.parent_object_id))
       + ' ADD CONSTRAINT ' + QUOTENAME(conz.name) + ' FOREIGN KEY (' + ChildCollection.ChildColumns + ') REFERENCES '
       + QUOTENAME(SCHEMA_NAME(conz.schema_id)) + '.' + QUOTENAME(OBJECT_NAME(conz.referenced_object_id)) + ' ('
       + ParentCollection.ParentColumns + ') ' + ';' AS Command
FROM sys.foreign_keys conz
    INNER JOIN sys.foreign_key_columns colz
        ON conz.object_id = colz.constraint_object_id
    INNER JOIN #MyAffectedTables tabz
        ON conz.parent_object_id = tabz.object_id
           AND tabz.column_id = colz.parent_column_id
    INNER JOIN
    ( --gets my child tables column names   
        SELECT name,
               ChildColumns = STUFF(
                              (
                                  SELECT ',' + REFZ.name
                                  FROM sys.foreign_key_columns fkcolz
                                      INNER JOIN sys.columns REFZ
                                          ON fkcolz.parent_object_id = REFZ.object_id
                                             AND fkcolz.parent_column_id = REFZ.column_id
                                  WHERE fkcolz.parent_object_id = conz.parent_object_id
                                        AND fkcolz.constraint_object_id = conz.object_id
                                  ORDER BY fkcolz.constraint_column_id
                                  FOR XML PATH('')
                              ),
                              1,
                              1,
                              ''
                                   )
        FROM sys.foreign_keys conz
            INNER JOIN sys.foreign_key_columns colz
                ON conz.object_id = colz.constraint_object_id
        GROUP BY conz.name,
                 conz.parent_object_id, --- without GROUP BY multiple rows are returned
                 conz.object_id
    ) ChildCollection
        ON conz.name = ChildCollection.name
    INNER JOIN
    ( --gets the parent tables column names for the FK reference
        SELECT name,
               ParentColumns = STUFF(
                               (
                                   SELECT ',' + REFZ.name
                                   FROM sys.foreign_key_columns fkcolz
                                       INNER JOIN sys.columns REFZ
                                           ON fkcolz.referenced_object_id = REFZ.object_id
                                              AND fkcolz.referenced_column_id = REFZ.column_id
                                   WHERE fkcolz.referenced_object_id = conz.referenced_object_id
                                         AND fkcolz.constraint_object_id = conz.object_id
                                   ORDER BY fkcolz.constraint_column_id
                                   FOR XML PATH('')
                               ),
                               1,
                               1,
                               ''
                                    )
        FROM sys.foreign_keys conz
            INNER JOIN sys.foreign_key_columns colz
                ON conz.object_id = colz.constraint_object_id
        -- AND colz.parent_column_id 
        GROUP BY conz.name,
                 conz.referenced_object_id, --- without GROUP BY multiple rows are returned
                 conz.object_id
    ) ParentCollection
        ON conz.name = ParentCollection.name;

--################################################################################################# 
--STEP_005, 006 and 007  primary keys,unique indexes,regular indexes
--################################################################################################# 
/*pre-quel sequel to gather the data:*/
SELECT CASE
           WHEN is_primary_key = 1 THEN
               'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME) + '.' + QUOTENAME(OBJECT_NAME) + ' DROP CONSTRAINT '
               + QUOTENAME(index_name) + ';'
           WHEN is_unique_constraint = 1 THEN
               'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME) + '.' + QUOTENAME(OBJECT_NAME) + ' DROP CONSTRAINT '
               + QUOTENAME(index_name) + ';'
           ELSE
               'DROP INDEX ' + +QUOTENAME(index_name) + ' ON ' + QUOTENAME(SCHEMA_NAME) + '.' + QUOTENAME(OBJECT_NAME)
               + ';'
       END COLLATE DATABASE_DEFAULT AS c1,
       CASE
           WHEN is_primary_key = 1 THEN
               'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME) + '.' + QUOTENAME(OBJECT_NAME) + ' ADD CONSTRAINT '
               + QUOTENAME(index_name) + ' PRIMARY KEY ' + CASE
                                                               WHEN type_desc = 'CLUSTERED' THEN
                                                                   type_desc
                                                               ELSE
                                                                   ''
                                                           END + ' (' + index_columns_key + ')' + ';'
           WHEN is_unique_constraint = 1 THEN
               'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME) + '.' + QUOTENAME(OBJECT_NAME) + ' ADD CONSTRAINT '
               + QUOTENAME(index_name) + 'UNIQUE (' + index_columns_key + ')' + ';'
           --ELSE 'DROP INDEX IX_ProductVendor_BusinessEntityID  ON Purchasing.ProductVendor WITH (ONLINE = ON, MAXDOP = 2)'
           ELSE
               'CREATE INDEX ' + +QUOTENAME(index_name) + ' ON ' + +QUOTENAME(SCHEMA_NAME) + '.'
               + QUOTENAME(OBJECT_NAME) + ' (' + index_columns_key + ')'
               + CASE
                     WHEN index_columns_include = '---' THEN
                         ''
                     ELSE
                         ' INCLUDE (' + index_columns_include + ')'
                 END + CASE
                           WHEN has_filter = 0 THEN
                               ''
                           ELSE
                               ' WHERE ' + filter_definition + ' '
                       END + ';'
       END COLLATE DATABASE_DEFAULT AS c2,
       *
INTO #Indexes
FROM
(
    SELECT SCH.schema_id,
           SCH.[name] COLLATE DATABASE_DEFAULT AS SCHEMA_NAME,
           OBJS.[object_id],
           OBJS.[name] COLLATE DATABASE_DEFAULT AS OBJECT_NAME,
           IDX.index_id,
           ISNULL(IDX.[name], '---')COLLATE DATABASE_DEFAULT AS index_name,
           partitions.Rows,
           partitions.SizeMB,
           INDEXPROPERTY(OBJS.[object_id], IDX.[name], 'IndexDepth') AS IndexDepth,
           IDX.type,
           IDX.type_desc COLLATE DATABASE_DEFAULT AS type_desc,
           IDX.fill_factor,
           IDX.is_unique,
           IDX.is_primary_key,
           IDX.is_unique_constraint,
           IDX.has_filter,
           IDX.filter_definition,
           ISNULL(Index_Columns.index_columns_key, '---')COLLATE DATABASE_DEFAULT AS index_columns_key,
           ISNULL(Index_Columns.index_columns_include, '---')COLLATE DATABASE_DEFAULT AS index_columns_include
    FROM sys.objects OBJS
        INNER JOIN sys.schemas SCH
            ON OBJS.schema_id = SCH.schema_id
        INNER JOIN sys.indexes IDX
            ON OBJS.[object_id] = IDX.[object_id]
        INNER JOIN
        (
            SELECT [object_id],
                   index_id,
                   SUM(row_count) AS Rows,
                   CONVERT(
                              NUMERIC(19, 3),
                              CONVERT(
                                         NUMERIC(19, 3),
                                         SUM(in_row_reserved_page_count + lob_reserved_page_count
                                             + row_overflow_reserved_page_count
                                            )
                                     ) / CONVERT(NUMERIC(19, 3), 128)
                          ) AS SizeMB
            FROM sys.dm_db_partition_stats STATS
            GROUP BY [object_id],
                     index_id
        ) AS partitions
            ON IDX.[object_id] = partitions.[object_id]
               AND IDX.index_id = partitions.index_id
        CROSS APPLY
    (
        SELECT LEFT(index_columns_key, LEN(index_columns_key) - 1)COLLATE DATABASE_DEFAULT AS index_columns_key,
               LEFT(index_columns_include, LEN(index_columns_include) - 1)COLLATE DATABASE_DEFAULT AS index_columns_include
        FROM
        (
            SELECT
                (
                    SELECT colz.[name] + ',' + ' ' COLLATE DATABASE_DEFAULT
                    FROM sys.index_columns IXCOLS
                        INNER JOIN sys.columns colz
                            ON IXCOLS.column_id = colz.column_id
                               AND IXCOLS.[object_id] = colz.[object_id]
                    WHERE IXCOLS.is_included_column = 0
                          AND IDX.[object_id] = IXCOLS.[object_id]
                          AND IDX.index_id = IXCOLS.index_id
                    ORDER BY key_ordinal
                    FOR XML PATH('')
                ) AS index_columns_key,
                (
                    SELECT colz.[name] + ',' + ' ' COLLATE DATABASE_DEFAULT
                    FROM sys.index_columns IXCOLS
                        INNER JOIN sys.columns colz
                            ON IXCOLS.column_id = colz.column_id
                               AND IXCOLS.[object_id] = colz.[object_id]
                    WHERE IXCOLS.is_included_column = 1
                          AND IDX.[object_id] = IXCOLS.[object_id]
                          AND IDX.index_id = IXCOLS.index_id
                    ORDER BY index_column_id
                    FOR XML PATH('')
                ) AS index_columns_include
        ) AS Index_Columns
    ) AS Index_Columns
) AllIndexes;

--################################################################################################# 
--STEP_005 primary keys
--################################################################################################# 
INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT DISTINCT
       50 AS ExecutionOrder,
       IDXZ.c1 AS Command
FROM #Indexes IDXZ
    LEFT OUTER JOIN #MyAffectedTables TBLZ
        ON IDXZ.OBJECT_NAME = TBLZ.TableName
WHERE is_primary_key = 1
      AND
      (
          CHARINDEX(TBLZ.ColumnName + ',', IDXZ.index_columns_key + ',') > 0
          OR CHARINDEX(TBLZ.ColumnName + ',', IDXZ.index_columns_include + ',') > 0
      );

INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT DISTINCT
       500 AS ExecutionOrder,
       IDXZ.c2 AS Command
FROM #Indexes IDXZ
    LEFT OUTER JOIN #MyAffectedTables TBLZ
        ON IDXZ.OBJECT_NAME = TBLZ.TableName
WHERE is_primary_key = 1
      AND
      (
          CHARINDEX(TBLZ.ColumnName + ',', IDXZ.index_columns_key + ',') > 0
          OR CHARINDEX(TBLZ.ColumnName + ',', IDXZ.index_columns_include + ',') > 0
      );

--################################################################################################# 
--STEP_006 unique indexes
--################################################################################################# 
INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT DISTINCT
       60 AS ExecutionOrder,
       IDXZ.c1 AS Command
FROM #Indexes IDXZ
    LEFT OUTER JOIN #MyAffectedTables TBLZ
        ON IDXZ.OBJECT_NAME = TBLZ.TableName
WHERE IDXZ.is_primary_key = 0
      AND IDXZ.is_unique_constraint = 0
      AND
      (
          CHARINDEX(TBLZ.ColumnName + ',', IDXZ.index_columns_key + ',') > 0
          OR CHARINDEX(TBLZ.ColumnName + ',', IDXZ.index_columns_include + ',') > 0
      );

INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT DISTINCT
       600 AS ExecutionOrder,
       IDXZ.c2 AS Command
FROM #Indexes IDXZ
    LEFT OUTER JOIN #MyAffectedTables TBLZ
        ON IDXZ.OBJECT_NAME = TBLZ.TableName
WHERE IDXZ.is_primary_key = 0
      AND IDXZ.is_unique_constraint = 0
      AND
      (
          CHARINDEX(TBLZ.ColumnName + ',', IDXZ.index_columns_key + ',') > 0
          OR CHARINDEX(TBLZ.ColumnName + ',', IDXZ.index_columns_include + ',') > 0
      );

--################################################################################################# 
--STEP_007 regular indexes(also featuring includes or filtered indexes
--################################################################################################# 
INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT DISTINCT
       70 AS ExecutionOrder,
       IDXZ.c1 AS Command
FROM #Indexes IDXZ
    LEFT OUTER JOIN #MyAffectedTables TBLZ
        ON IDXZ.OBJECT_NAME = TBLZ.TableName
WHERE IDXZ.is_primary_key = 0
      AND IDXZ.is_unique_constraint = 1
      AND
      (
          CHARINDEX(TBLZ.ColumnName + ',', IDXZ.index_columns_key + ',') > 0
          OR CHARINDEX(TBLZ.ColumnName + ',', IDXZ.index_columns_include + ',') > 0
      );

INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT DISTINCT
       700 AS ExecutionOrder,
       IDXZ.c2 AS Command
FROM #Indexes IDXZ
    LEFT OUTER JOIN #MyAffectedTables TBLZ
        ON IDXZ.OBJECT_NAME = TBLZ.TableName
WHERE IDXZ.is_primary_key = 0
      AND IDXZ.is_unique_constraint = 1
      AND
      (
          CHARINDEX(TBLZ.ColumnName + ',', IDXZ.index_columns_key + ',') > 0
          OR CHARINDEX(TBLZ.ColumnName + ',', IDXZ.index_columns_include + ',') > 0
      );

--################################################################################################# 
--STEP_008 Column Collation definitions
--################################################################################################# 

INSERT INTO #Results
(
    ExecutionOrder,
    Command
)
SELECT DISTINCT
       80 AS ExecutionOrder,
       'ALTER TABLE ' + QUOTENAME(SCHEMA_NAME(objz.schema_id)) + '.' + QUOTENAME(objz.name) + ' ALTER COLUMN '
       + CASE
             WHEN colz.[is_computed] = 0 THEN
                 QUOTENAME(colz.[name]) + ' ' + (TYPE_NAME(colz.[user_type_id]))
                 + CASE
                       WHEN TYPE_NAME(colz.[user_type_id]) IN ( 'char', 'varchar' ) THEN
                           CASE
                               WHEN colz.[max_length] = -1 THEN
                                   '(max)' + SPACE(6 - LEN(CONVERT(VARCHAR, colz.[max_length]))) + SPACE(7)
                                   + SPACE(16 - LEN(TYPE_NAME(colz.[user_type_id])))
                                   ----collate to comment out when not desired
                                   + CASE
                                         WHEN colz.collation_name IS NULL THEN
                                             ''
                                         ELSE
                                             ' COLLATE ' + @NewCollation -- this was the old collation: colz.collation_name
                                     END + CASE
                                               WHEN colz.[is_nullable] = 0 THEN
                                                   ' NOT NULL'
                                               ELSE
                                                   '     NULL'
                                           END
                               ELSE
                                   '(' + CONVERT(VARCHAR, colz.[max_length]) + ') '
                                   + SPACE(6 - LEN(CONVERT(VARCHAR, colz.[max_length]))) + SPACE(7)
                                   + SPACE(16 - LEN(TYPE_NAME(colz.[user_type_id])))
                                   ----collate to comment out when not desired
                                   + CASE
                                         WHEN colz.collation_name IS NULL THEN
                                             ''
                                         ELSE
                                             ' COLLATE ' + @NewCollation
                                     END + CASE
                                               WHEN colz.[is_nullable] = 0 THEN
                                                   ' NOT NULL'
                                               ELSE
                                                   '     NULL'
                                           END
                           END
                       WHEN TYPE_NAME(colz.[user_type_id]) IN ( 'nchar', 'nvarchar' ) THEN
                           CASE
                               WHEN colz.[max_length] = -1 THEN
                                   '(max)' + SPACE(6 - LEN(CONVERT(VARCHAR, (colz.[max_length])))) + SPACE(7)
                                   + SPACE(16 - LEN(TYPE_NAME(colz.[user_type_id])))
                                   ----collate to comment out when not desired
                                   --+ CASE
                                   --     WHEN colz.collation_name IS NULL
                                   --     THEN ''
                                   --     ELSE ' COLLATE ' + colz.collation_name
                                   --   END
                                   + CASE
                                         WHEN colz.[is_nullable] = 0 THEN
                                             ' NOT NULL'
                                         ELSE
                                             '     NULL'
                                     END
                               ELSE
                                   '(' + CONVERT(VARCHAR, (colz.[max_length])) + ') '
                                   + SPACE(6 - LEN(CONVERT(VARCHAR, (colz.[max_length])))) + SPACE(7)
                                   + SPACE(16 - LEN(TYPE_NAME(colz.[user_type_id])))
                                   ----collate to comment out when not desired
                                   --+ CASE
                                   --     WHEN colz.collation_name IS NULL
                                   --     THEN ''
                                   --     ELSE ' COLLATE ' + colz.collation_name
                                   --   END
                                   + CASE
                                         WHEN colz.[is_nullable] = 0 THEN
                                             ' NOT NULL'
                                         ELSE
                                             '     NULL'
                                     END
                           END
                   END
         END --iscomputed = 0
       + ';' AS Command
FROM sys.columns colz
    LEFT OUTER JOIN sys.tables objz
        ON colz.object_id = objz.object_id
    INNER JOIN #MyAffectedTables tabz
        ON tabz.object_id = colz.object_id
           AND tabz.column_id = colz.column_id
WHERE objz.type = 'U'
      AND TYPE_NAME(colz.[user_type_id]) IN ( 'char', 'varchar', 'nchar', 'nvarchar' );

--################################################################################################# 
--STEP_009 refresh dependent views, procs and functions
-- refresh them in dependancy order in a single pass.
--################################################################################################# 
--if there was nothing with the wrong collation, there's no need to refresh:
IF EXISTS (SELECT * FROM #Results WHERE ExecutionOrder > 0)
BEGIN
    CREATE TABLE #MyObjectHierarchy
    (
        HID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        ObjectId INT,
        TYPE INT,
        OBJECTTYPE AS CASE
                          WHEN TYPE = 1 THEN
                              'FUNCTION'
                          WHEN TYPE = 4 THEN
                              'VIEW'
                          WHEN TYPE = 8 THEN
                              'TABLE'
                          WHEN TYPE = 16 THEN
                              'PROCEDURE'
                          WHEN TYPE = 128 THEN
                              'RULE'
                          ELSE
                              ''
                      END,
        ONAME VARCHAR(255),
        OOWNER VARCHAR(255),
        SEQ INT
    );

    --our list of objects in dependancy order
    INSERT #MyObjectHierarchy
    (
        TYPE,
        ONAME,
        OOWNER,
        SEQ
    )
    -- use this if inside a stored procedure
    -- EXEC sp_msdependencies @intrans = 1 
    --else
    EXEC sp_MSdependencies;

    INSERT INTO #Results
    (
        ExecutionOrder,
        Command
    )
    SELECT 900 + HID AS ExecutionOrder,
           CASE
               WHEN OBJECTTYPE = 'VIEW' THEN
                   'EXEC sp_refreshview ''' + QUOTENAME(OOWNER) + '.' + QUOTENAME(ONAME) + ''';'
               WHEN OBJECTTYPE IN ( 'FUNCTION', 'PROCEDURE' ) THEN
                   'EXEC sp_recompile  ''' + QUOTENAME(OOWNER) + '.' + QUOTENAME(ONAME) + ''';'
           END
    FROM #MyObjectHierarchy
    WHERE OBJECTTYPE IN ( 'FUNCTION', 'VIEW', 'PROCEDURE' )
    ORDER BY HID;
END; --Exists 


--################################################################################################# 
--Final Presentation
--#################################################################################################     
SELECT *
FROM #Results
ORDER BY ExecutionOrder,
         ID;
--#################################################################################################     
--optional cursor to go ahead and run all these scripts
--don't run this cursor unless you are 100% sure of the scripts.
--TEST TEST TEST!
--#################################################################################################     
/*
declare
  @isql varchar(max)
  
  declare c1 cursor LOCAL FORWARD_ONLY STATIC READ_ONLY for
  --###############################################################################################
  --cursor definition
  --###############################################################################################
  SELECT
    Command
  FROM   #Results
  ORDER  BY
    ExecutionOrder,
    ID
  --###############################################################################################
  open c1
  fetch next from c1 into @isql
  While @@fetch_status <> -1
    begin
    print @isql
    exec(@isql)
    fetch next from c1 into @isql
    end
  close c1
  deallocate c1
*/
--#################################################################################################    
--clean up our temp objects
--#################################################################################################
IF
(
    SELECT OBJECT_ID('Tempdb.dbo.#MyAffectedTables')
) IS NOT NULL
    DROP TABLE #MyAffectedTables;

IF
(
    SELECT OBJECT_ID('Tempdb.dbo.#Indexes')
) IS NOT NULL
    DROP TABLE #Indexes;

IF
(
    SELECT OBJECT_ID('Tempdb.dbo.#Results')
) IS NOT NULL
    DROP TABLE #Results;

IF
(
    SELECT OBJECT_ID('Tempdb.dbo.#MyObjectHierarchy')
) IS NOT NULL
    DROP TABLE #MyObjectHierarchy;

    ";

            return script;
        }

        private void LoadDatabaseUsageStats()
        {
            string connectionString = "Data Source=" + ComboBoxserverName.Text + "; Integrated Security=True;";

            string query = @"
        SELECT d.name AS DBname,
               LastUserAccess =
               (
                   SELECT LastUserAccess = MAX(a.xx)
                   FROM
                   (
                       SELECT xx = MAX(last_user_seek)
                       WHERE MAX(last_user_seek) IS NOT NULL
                       UNION
                       SELECT xx = MAX(last_user_scan)
                       WHERE MAX(last_user_scan) IS NOT NULL
                       UNION
                       SELECT xx = MAX(last_user_lookup)
                       WHERE MAX(last_user_lookup) IS NOT NULL
                       UNION
                       SELECT xx = MAX(last_user_update)
                       WHERE MAX(last_user_update) IS NOT NULL
                   ) a
               )
        FROM master.dbo.sysdatabases d
            LEFT OUTER JOIN sys.dm_db_index_usage_stats s
                ON d.dbid = s.database_id
        WHERE d.dbid > 4
        GROUP BY d.name
        ORDER BY LastUserAccess;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
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
                SqlCommand command = new SqlCommand(query, connection);
                // لا حاجة لإضافة المعلمات هنا لأننا ندمج قيم المعلمات مباشرة في نص الاستعلام

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("تم تنفيذ الأمر بنجاح");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"حدث خطأ: {ex.Message}");
                }
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            AnalyzeIndexes(ComboBoxDatabaseName.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RebuildIndexes(ComboBoxDatabaseName.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReorganizeIndexes(ComboBoxDatabaseName.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SHRINKFILE(ComboBoxDatabaseName.Text);

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void BtnCreateAudit_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string auditPath = folderBrowserDialog.SelectedPath;
                    string databaseName = ComboBoxDatabaseName.Text; // أو الحصول على اسم قاعدة البيانات من واجهة المستخدم
                    CreateDatabaseAudit(databaseName, auditPath);
                }
            }
        }

        private void btnDisplayAuditData_Click(object sender, EventArgs e)
        {
            

        }

        private void button5Lastuseraccesstodatabase_Click(object sender, EventArgs e)
        {
            LoadDatabaseUsageStats();
        }

        private void btnDisplayAuditData_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "اختيار ملف تدقيق";
                openFileDialog.Filter = "ملفات التدقيق (*.sqlaudit)|*.sqlaudit|جميع الملفات (*.*)|*.*"; // يمكنك تعديل الفلتر حسب نوع الملفات المطلوبة

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string auditFilePath = openFileDialog.FileName; // الحصول على مسار الملف الذي تم اختياره
                    string databaseName = ComboBoxDatabaseName.Text; // أو الحصول على اسم قاعدة البيانات من واجهة المستخدم
                    DisplayAuditData(auditFilePath); // تمرير مسار الملف إلى الدالة
                }
            }
        }

        private void btnDatabaseExtendedProperties_Click(object sender, EventArgs e)
        {
            LoadDatabaseExtendedProperties();
        }
    }


    public class DatabaseFileChecker
    {
        private string connectionString;

        public DatabaseFileChecker(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable CheckDatabaseFileLogicalNames()
        {
            string query = @"
            WITH Files AS (
                SELECT 
                    DB_NAME(database_id) AS DatabaseName,
                    type_desc AS FileType,
                    name AS LogicalName,
                    physical_name AS PhysicalName
                FROM sys.master_files
                WHERE database_id > 4
                AND file_id IN (1,2)
            )
            SELECT 
                Files.DatabaseName,
                Files.FileType,
                Files.LogicalName,
                Files.PhysicalName,
                CASE
                    WHEN Files.FileType = 'ROWS' THEN
                        'USE [' + DatabaseName + ']; ALTER DATABASE [' + DatabaseName + '] MODIFY FILE (NAME=N''' + LogicalName
                        + ''', NEWNAME=N''' + DatabaseName + ''')'
                    ELSE
                        'USE [' + DatabaseName + ']; ALTER DATABASE [' + DatabaseName + '] MODIFY FILE (NAME=N''' + LogicalName
                        + ''', NEWNAME=N''' + DatabaseName + '_log'')'
                END AS Command
            FROM Files
            WHERE Files.DatabaseName <> 'SSISDB'
                  AND Files.DatabaseName NOT LIKE 'ReportServer%'
                  AND CHARINDEX(Files.DatabaseName, Files.LogicalName) = 0;
        ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public void FixLogicalNames(DataTable dataTable)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataRow row in dataTable.Rows)
                {
                    string commandText = row["Command"].ToString();
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public DataTable GetPhysicalFileRenameCommands()
        {
            string query = @"
            WITH Files AS (
                SELECT 
                    DB_NAME(database_id) AS DatabaseName,
                    type_desc AS FileType,
                    name AS LogicalName,
                    physical_name AS PhysicalName
                FROM sys.master_files
                WHERE database_id > 4
                AND file_id IN (1,2)
            )
            SELECT 
                Files.DatabaseName,
                Files.FileType,
                Files.LogicalName,
                Files.PhysicalName,
                CASE
                    WHEN Files.FileType = 'ROWS' THEN
                        'ALTER DATABASE [' + Files.DatabaseName + '] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; ALTER DATABASE ['
                        + Files.DatabaseName + '] SET OFFLINE; --Now rename ' + Files.PhysicalName + ' to ' + Files.DatabaseName
                        + '.mdf; ALTER DATABASE [' + Files.DatabaseName + '] MODIFY FILE ( NAME = [' + Files.LogicalName
                        + '], FILENAME = ''' + Files.PhysicalName + ''' ) --change to ' + Files.DatabaseName
                        + '.mdf; ALTER DATABASE [' + Files.DatabaseName + '] SET ONLINE; ALTER DATABASE [' + Files.DatabaseName
                        + '] SET MULTI_USER;'
                    ELSE
                        'ALTER DATABASE [' + Files.DatabaseName + '] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; ALTER DATABASE ['
                        + Files.DatabaseName + '] SET OFFLINE; --Now rename ' + Files.PhysicalName + ' to ' + Files.DatabaseName
                        + '_log.ldf; ALTER DATABASE [' + Files.DatabaseName + '] MODIFY FILE ( NAME = [' + Files.LogicalName
                        + '], FILENAME = ''' + Files.PhysicalName + ''' ) --change to ' + Files.DatabaseName
                        + '_log.ldf; ALTER DATABASE [' + Files.DatabaseName + '] SET ONLINE; ALTER DATABASE ['
                        + Files.DatabaseName + '] SET MULTI_USER;'
                END AS Command
            FROM Files
            WHERE Files.DatabaseName NOT LIKE 'ReportServer%'
                  AND CHARINDEX(Files.DatabaseName, Files.PhysicalName) = 0;
        ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public void RenamePhysicalFiles(DataTable dataTable)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataRow row in dataTable.Rows)
                {
                    string commandText = row["Command"].ToString();
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }











}

