


/*    private void CreateIndexes(string databaseName)
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
                                SET @sql = 'CREATE INDEX ' + @indexName + ' ON ' + @tableName + ' (' + @columnName + ');';
                                EXEC sp_executesql @sql;
                                
                                FETCH NEXT FROM column_cursor INTO @columnName;
                            END
                            
                            CLOSE column_cursor;
                            DEALLOCATE column_cursor;
                            
                            FETCH NEXT FROM table_cursor INTO @tableName;
                        END
                        
                        CLOSE table_cursor;
                        DEALLOCATE table_cursor;
                    ";
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Indexes created successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while creating indexes: " + ex.Message);
                }
            }
        }*/

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
            }else
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
    }
}

