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
    public partial class AdvanceFuture : Form
    {
        private string connectionString = "Data Source=.; Initial Catalog=infoDB; Integrated Security=True;";

        public AdvanceFuture()
        {
            InitializeComponent();
 
        }

        private void LoadData()
        {
            string query = @"
        WITH Waits AS
        (SELECT
            wait_type,
            wait_time_ms / 1000. AS wait_time_s,
            100. * wait_time_ms / SUM(wait_time_ms) OVER() AS pct,
            ROW_NUMBER() OVER(ORDER BY wait_time_ms DESC) AS rn
         FROM sys.dm_os_wait_stats
         WHERE wait_type NOT IN (
            'CLR_SEMAPHORE', 'LAZYWRITER_SLEEP', 'RESOURCE_QUEUE', 'SLEEP_TASK',
            'SLEEP_SYSTEMTASK', 'SQLTRACE_BUFFER_FLUSH', 'WAITFOR', 'LOGMGR_QUEUE',
            'CHECKPOINT_QUEUE', 'REQUEST_FOR_DEADLOCK_SEARCH', 'XE_TIMER_EVENT', 'BROKER_TO_FLUSH',
            'BROKER_TASK_STOP', 'CLR_MANUAL_EVENT', 'CLR_AUTO_EVENT', 'DISPATCHER_QUEUE_SEMAPHORE',
            'FT_IFTS_SCHEDULER_IDLE_WAIT', 'XE_DISPATCHER_WAIT', 'FT_IFTSHC_MUTEX', 'SQLTRACE_INCREMENTAL_FLUSH_SLEEP')
        )
        SELECT
            W1.wait_type,
            CAST(W1.wait_time_s AS DECIMAL(12, 2)) AS wait_time_s,
            CAST(W1.pct AS DECIMAL(12, 2)) AS pct,
            CAST(SUM(W2.pct) AS DECIMAL(12, 2)) AS running_pct
        FROM Waits AS W1
            INNER JOIN Waits AS W2 ON W2.rn <= W1.rn
        GROUP BY W1.rn, W1.wait_type, W1.wait_time_s, W1.pct
        HAVING SUM(W2.pct) - W1.pct < 95; -- percentage threshold
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                // تحديث الرسم البياني
                UpdateChart(dataTable);
            }
        }
        // Example method for updating a chart with data
        private void UpdateChart(DataTable dataTable)
        {
            // مسح البيانات القديمة
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();

            // إعداد منطقة الرسم البياني
            var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chart1.ChartAreas.Add(chartArea);

            // إعداد سلسلة جديدة للرسم البياني
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Wait Times",
                IsVisibleInLegend = true,
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar // نوع الرسم البياني: عمودي
            };

            // إعداد ألوان الأعمدة المختلفة
            Random rand = new Random();
            foreach (DataRow row in dataTable.Rows)
            {
                var point = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                point.AxisLabel = row["wait_type"].ToString();
                point.YValues = new double[] { Convert.ToDouble(row["wait_time_s"]) };
                point.Color = System.Drawing.Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)); // تعيين لون عشوائي للعمود
                series.Points.Add(point);
            }

            // إضافة السلسلة إلى الرسم البياني
            chart1.Series.Add(series);

            // تعيين عنوان الرسم البياني والمحاور
            chart1.Titles.Add("Wait Times by Wait Type");

            // تعيين عنوان المحور X
            chart1.ChartAreas[0].AxisX.Title = "Wait Type";
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // تدوير التسميات لتكون أكثر وضوحًا

            // تعيين عنوان المحور Y
            chart1.ChartAreas[0].AxisY.Title = "Wait Time (seconds)";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "N2"; // تنسيق الأرقام مع فاصلة عشرية

            // تخصيص مظهر الرسم البياني
            chart1.ChartAreas[0].BackColor = System.Drawing.Color.AliceBlue;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // إخفاء الخطوط الرئيسية على المحور X
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray; // تغيير لون الخطوط الرئيسية على المحور Y
        }

        private void ExecuteDatabaseFileQuery()
        {
            string sqlQuery = @"
    CREATE TABLE #db_files_info
    (
        DatabaseName NVARCHAR(128),
        file_id INT,
        FileType NVARCHAR(128),
        LogicalName NVARCHAR(128),
        PhysicalName NVARCHAR(260),
        SizeMB DECIMAL(18,2),
        SpaceUsedMB DECIMAL(18,2),
        FreeSpaceMB DECIMAL(18,2)
    );

    EXEC sp_MSforeachdb N'
    USE [?];
    INSERT INTO #db_files_info
    SELECT
        DB_NAME() AS [DatabaseName],
        file_id,
        type_desc AS [FileType],
        name AS [LogicalName],
        Physical_Name AS [PhysicalName],
        (size * 8.0 / 1024) AS [SizeMB],
        (FILEPROPERTY(name, ''SpaceUsed'') * 8.0 / 1024) AS [SpaceUsedMB],
        ((size - FILEPROPERTY(name, ''SpaceUsed'')) * 8.0 / 1024) AS [FreeSpaceMB]
    FROM sys.master_files
    WHERE
        DB_NAME(database_id) = DB_NAME()
    ORDER BY
        type, file_id;
    ';

    SELECT * FROM #db_files_info;
    DROP TABLE #db_files_info;
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }
        private void ExecuteTopIOQueries()
        {
            string sqlQuery = @"
    SELECT
         cpu_time,
        logical_reads,
        writes,
        reads,
        total_elapsed_time,
        statement_start_offset,
        statement_end_offset,
        plan_handle,
        sql_handle
    FROM
        sys.dm_exec_requests
    ORDER BY
        (logical_reads + writes) DESC;
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

     
        private void FindLastModiedStoredProcedures()
        {
            string sqlQuery = @"
   SELECT
    name AS [Stored Procedure],
    modify_date AS [Last Modified Date]
FROM
    sys.objects
WHERE
    type = 'P'
    AND DATEDIFF(D, modify_date, GETDATE()) < 30 -- Change 30 to the number of days you want to go back
ORDER BY
    modify_date DESC;
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
 
        }

        private void btnExecuteDatabaseFileQuery_click(object sender, EventArgs e)
        {
            ExecuteDatabaseFileQuery();
        }

        private void button2ExecuteTopIOQueries_Click(object sender, EventArgs e)
        {
            ExecuteTopIOQueries();
        }

        private void AdvanceFuture_Load(object sender, EventArgs e)
        {

        }

        private void button2FindLastModiedStoredProcedures_Click(object sender, EventArgs e)
        {
            FindLastModiedStoredProcedures();
        }

        private void ExecuteActiveTransactions(object sender, EventArgs e)
        {
            string sqlQuery = @"
    SELECT
        transaction_id,
        name,
        transaction_begin_time,
        transaction_type,
        transaction_state
    FROM
        sys.dm_tran_active_transactions;
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // Assuming you have another DataGridView named dataGridView2
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }
        private void ExecuteUserDefinedFunctions()
        {
            string sqlQuery = @"
    SELECT
        o.name AS FunctionName,
        o.type_desc AS FunctionType,
        (SELECT
             COUNT(*)
         FROM
             sys.sql_expression_dependencies sed
         WHERE
             sed.referencing_id = o.object_id) AS DependentObjectCount
    FROM
        sys.objects o
    WHERE
        o.type IN ('TF', 'IF', 'FN', 'FS', 'FT')
    ORDER BY
        DependentObjectCount DESC,
        o.name;
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // Assuming you have a DataGridView named dataGridView1 for displaying results
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }
        private void ExecuteUserDefinedFunctions2()
        {
            // نص الاستعلام لجلب قواعد البيانات
            string getDatabasesQuery = @"
    SELECT name
    FROM sys.databases
    WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')"; // استثناء قواعد البيانات النظامية

            DataTable databasesTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(getDatabasesQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                try
                {
                    connection.Open();
                    adapter.Fill(databasesTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while retrieving databases: {ex.Message}");
                    return;
                }
            }

            // إنشاء جدول لتخزين النتائج
            DataTable allFunctionsTable = new DataTable();
            allFunctionsTable.Columns.Add("DatabaseName", typeof(string));
            allFunctionsTable.Columns.Add("FunctionName", typeof(string));
            allFunctionsTable.Columns.Add("FunctionType", typeof(string));
            allFunctionsTable.Columns.Add("DependentObjectCount", typeof(int));

            // تنفيذ الاستعلام لكل قاعدة بيانات
            foreach (DataRow dbRow in databasesTable.Rows)
            {
                string databaseName = dbRow["name"].ToString();
                string sqlQuery = $@"
        USE [{databaseName}];
        SELECT
            o.name AS FunctionName,
            o.type_desc AS FunctionType,
            (SELECT
                 COUNT(*)
             FROM
                 sys.sql_expression_dependencies sed
             WHERE
                 sed.referencing_id = o.object_id) AS DependentObjectCount
        FROM
            sys.objects o
        WHERE
            o.type IN ('TF', 'IF', 'FN', 'FS', 'FT')
        ORDER BY
            DependentObjectCount DESC,
            o.name;
        ";

                using (SqlConnection dbConnection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(sqlQuery, dbConnection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dbFunctionsTable = new DataTable();

                    try
                    {
                        dbConnection.Open();
                        adapter.Fill(dbFunctionsTable);

                        // إضافة قاعدة البيانات إلى نتائج الاستعلام
                        foreach (DataRow row in dbFunctionsTable.Rows)
                        {
                            allFunctionsTable.Rows.Add(databaseName, row["FunctionName"], row["FunctionType"], row["DependentObjectCount"]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while retrieving functions from database {databaseName}: {ex.Message}");
                    }
                }
            }

            // عرض النتائج في DataGridView
            dataGridView1.DataSource = allFunctionsTable;
        }

        private void button2UserDefinedFunctions_Click(object sender, EventArgs e)
        {
            ExecuteUserDefinedFunctions2();
        }
    }
}
