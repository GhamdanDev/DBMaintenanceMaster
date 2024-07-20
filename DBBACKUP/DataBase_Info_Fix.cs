

 

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

        public DataBase_Info_Fix()
        {
            InitializeComponent();
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
    }








    /*

        public class DatabaseHelper
        {
            private string connectionString;


    public DatabaseHelper(string serverName)
            {
                connectionString = $"Data Source={serverName}; Integrated Security=True;";
            }

            private void OpenConnection(SqlConnection connection)
            {
                connection.ConnectionString = connectionString;
                connection.Open();
            }

            public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    OpenConnection(connection);
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }

            public void ExecuteNonQuery(string query, SqlParameter[] parameters = null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    OpenConnection(connection);
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }

            public DataSet ExecuteStoredProcedure(string storedProcedureName, SqlParameter[] parameters = null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    OpenConnection(connection);
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public partial class DataBase_Info_Fix : Form
        {
            private DatabaseHelper dbHelper;

    public DataBase_Info_Fix()
            {
                InitializeComponent();
            }

            private void DataBase_Info_Fix_Load(object sender, EventArgs e)
            {
                InitializeDatabaseHelper(".");
                LoadServerNames();
            }

            private void InitializeDatabaseHelper(string serverName)
            {
                dbHelper = new DatabaseHelper(serverName);
            }

            private void LoadServerNames()
            {
                var dt = dbHelper.ExecuteQuery("SELECT * FROM sys.servers WHERE product = 'SQL Server'");
                foreach (DataRow row in dt.Rows)
                {
                    ComboBoxserverName.Items.Add(row["name"]);
                }
            }

            private void LoadDatabaseNames()
            {
                var dt = dbHelper.ExecuteQuery("SELECT name FROM sys.databases");
                ComboBoxDatabaseName.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    ComboBoxDatabaseName.Items.Add(row["name"]);
                }
            }

            private void LoadData(string databaseName, string storedProcedure, DataGridView gridView)
            {
                var parameters = new SqlParameter[]
                {
            new SqlParameter("@DatabaseName", databaseName)
                };
                var ds = dbHelper.ExecuteStoredProcedure(storedProcedure, parameters);
                DataTable mergedTable = MergeTables(ds.Tables);
                gridView.DataSource = mergedTable;
            }

            private DataTable MergeTables(DataTableCollection tables)
            {
                DataTable mergedTable = new DataTable();
                foreach (DataTable table in tables)
                {
                    mergedTable.Merge(table);
                }
                return mergedTable;
            }

            private void AnalyzeIndexes(string databaseName)
            {
                string query = $@"
            USE {databaseName};
            SELECT 
                OBJECT_NAME(IX.object_id) AS TableName,
                IX.name AS IndexName,
                IX.type_desc AS IndexType,
                PS.index_id,
                SUM(PS.[used_page_count]) * 8 AS IndexSizeKB,
                SUM(PS.page_count) AS PageCount
            FROM 
                sys.dm_db_partition_stats PS
                INNER JOIN sys.indexes IX ON PS.object_id = IX.object_id AND PS.index_id = IX.index_id
            WHERE 
                OBJECTPROPERTY(PS.object_id,'IsUserTable') = 1
            GROUP BY 
                OBJECT_NAME(IX.object_id), IX.name, IX.type_desc, PS.index_id
            ORDER BY 
                OBJECT_NAME(IX.object_id), IX.name;
        ";
                var dt = dbHelper.ExecuteQuery(query);
                //richTextBox1.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    //richTextBox1.AppendText($"{row["TableName"]}, {row["IndexName"]}, {row["IndexType"]}, {row["IndexSizeKB"]} KB\n");
                }
            }

            private void RebuildIndexes(string databaseName)
            {
                string query = $"USE {databaseName}; EXEC sp_MSforeachtable @command1 = 'ALTER INDEX ALL ON ? REBUILD';";
                dbHelper.ExecuteNonQuery(query);
                MessageBox.Show("Indexes rebuilt successfully!");
            }

            private void ReorganizeIndexes(string databaseName)
            {
                string query = $"USE {databaseName}; EXEC sp_MSforeachtable @command1 = 'ALTER INDEX ALL ON ? REORGANIZE';";
                dbHelper.ExecuteNonQuery(query);
                MessageBox.Show("Indexes reorganized successfully!");
            }

            private void SHRINKFILE(string databaseName)
            {
                string query = $"USE {databaseName}; DBCC SHRINKFILE ('{databaseName}', 0); DBCC SHRINKFILE ('{databaseName}_log', 0);";
                dbHelper.ExecuteNonQuery(query);
                MessageBox.Show("تم تنفيذ الأمر بنجاح");
            }

            private void InfoOfDBTables(string databaseName)
            {
                string query = $@"
            USE {databaseName};
            SELECT 
                t.name AS TableName,
                i.name AS IndexName,
                i.index_id,
                sum(p.rows) AS RowCounts,
                sum(a.total_pages) AS TotalPages,
                sum(a.used_pages) AS UsedPages,
                sum(a.data_pages) AS DataPages,
                (sum(a.total_pages) * 8) / 1024 AS TotalSpaceMB,
                (sum(a.used_pages) * 8) / 1024 AS UsedSpaceMB, 
                (sum(a.data_pages) * 8) / 1024 AS DataSpaceMB
            FROM 
                sys.tables t
                INNER JOIN sys.indexes i ON t.object_id = i.object_id
                INNER JOIN sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
                INNER JOIN sys.allocation_units a ON p.partition_id = a.container_id
            WHERE 
                t.type = 'U'
            GROUP BY 
                t.name, i.object_id, i.index_id, i.name 
            ORDER BY 
                sum(a.total_pages) DESC, t.name;
        ";
                var dt = dbHelper.ExecuteQuery(query);
                //richTextBox1.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    //richTextBox1.AppendText($"{row["TableName"]}, {row["IndexName"]}, {row["TotalSpaceMB"]} MB\n");
                }
            }

            private void ComboBoxserverName_SelectedIndexChanged(object sender, EventArgs e)
            {
                InitializeDatabaseHelper(ComboBoxserverName.Text);
                LoadDatabaseNames();
            }

            private void ComboBoxDatabaseName_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (!string.IsNullOrWhiteSpace(ComboBoxDatabaseName.Text))
                {
                    LoadData(ComboBoxDatabaseName.Text, "sp_GetDatabaseInfo", dataGridView1);
                    LoadData(ComboBoxDatabaseName.Text, "sp_GetDatabaseCountInfo", dataGridView2);
                    LoadData(ComboBoxDatabaseName.Text, "sp_GetDatabaseCountInfoV5", dataGridView2);
                }
                else
                {
                    MessageBox.Show("Select Database name");
                }
            }

            private void button1_Click(object sender, EventArgs e)
            {
                if (!string.IsNullOrWhiteSpace(ComboBoxDatabaseName.Text))
                {
                    AnalyzeIndexes(ComboBoxDatabaseName.Text);
                }
                else
                {
                    MessageBox.Show("Select Database name");
                }
            }

            private void button2_Click(object sender, EventArgs e)
            {
                RebuildIndexes(ComboBoxDatabaseName.Text);
            }

            private void button3_Click(object sender, EventArgs e)
            {
                ReorganizeIndexes(ComboBoxDatabaseName.Text);
            }

            private void button4_Click(object sender, EventArgs e)
            {
                SHRINKFILE(ComboBoxDatabaseName.Text);
            }


            private void button1_Click_1(object sender, EventArgs e)
            {
                AnalyzeIndexes(ComboBoxDatabaseName.Text);
            }



            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }



            private void pictureBox4_Click(object sender, EventArgs e)
            {

            }


            private void ComboBoxDatabaseName_SelectedIndexChanged_1(object sender, EventArgs e)
            {
                if (!string.IsNullOrWhiteSpace(ComboBoxDatabaseName.Text))
                {
                    //LoadDataCountInfoDB(ComboBoxDatabaseName.Text);
                    //LoadDataCountInfoDB2(ComboBoxDatabaseName.Text);
                    //LoadData(ComboBoxDatabaseName.Text);
                }
                else
                    MessageBox.Show("select Database name");

            }

            private void button1CreateIndexes_Click(object sender, EventArgs e)
            {
                // CreateIndexes(ComboBoxDatabaseName.Text);
                //AddIndexes(ComboBoxDatabaseName.Text);

            }

        }
    */








}

