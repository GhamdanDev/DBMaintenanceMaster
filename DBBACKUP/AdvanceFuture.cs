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
        private void ShowCurrentLocks()
        {
            // النص الاستعلامي لجلب معلومات الأقفال
            string sqlQuery = @"
    SELECT
        resource_type AS [Resource Type],
        resource_database_id AS [Database ID],
        resource_associated_entity_id AS [Entity ID],
        request_mode AS [Request Mode],
        request_type AS [Request Type],
        request_status AS [Request Status],
        request_session_id AS [Session ID]
    FROM
        sys.dm_tran_locks
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
        private void ShowBlockingHierarchy()
        {
            // النص الاستعلامي لجلب التسلسل الهرمي للحظر
            string sqlQuery = @"
    WITH cteHead (session_id, request_id, wait_type, wait_resource, last_wait_type, is_user_process, request_cpu_time,
    request_logical_reads, request_reads, request_writes, wait_time, blocking_session_id, memory_usage,
    session_cpu_time, session_reads, session_writes, session_logical_reads,
    percent_complete, est_completion_time, request_start_time, request_status, command,
    plan_handle, sql_handle, statement_start_offset, statement_end_offset, most_recent_sql_handle,
    session_status, group_id, query_hash, query_plan_hash) 
    AS (
        SELECT sess.session_id, req.request_id, LEFT(ISNULL(req.wait_type, ''), 50) AS wait_type,
        LEFT(ISNULL(req.wait_resource, ''), 40) AS wait_resource, LEFT(req.last_wait_type, 50) AS last_wait_type,
        sess.is_user_process, req.cpu_time AS request_cpu_time, req.logical_reads AS request_logical_reads,
        req.reads AS request_reads, req.writes AS request_writes, req.wait_time, req.blocking_session_id, sess.memory_usage,
        sess.cpu_time AS session_cpu_time, sess.reads AS session_reads, sess.writes AS session_writes, sess.logical_reads AS session_logical_reads,
        CONVERT(decimal(5,2), req.percent_complete) AS percent_complete, req.estimated_completion_time AS est_completion_time,
        req.start_time AS request_start_time, LEFT(req.status, 15) AS request_status, req.command,
        req.plan_handle, req.sql_handle, req.statement_start_offset, req.statement_end_offset, conn.most_recent_sql_handle,
        LEFT(sess.status, 15) AS session_status, sess.group_id, req.query_hash, req.query_plan_hash
        FROM sys.dm_exec_sessions AS sess
        LEFT OUTER JOIN sys.dm_exec_requests AS req ON sess.session_id = req.session_id
        LEFT OUTER JOIN sys.dm_exec_connections AS conn on conn.session_id = sess.session_id 
    )
    , cteBlockingHierarchy (head_blocker_session_id, session_id, blocking_session_id, wait_type, wait_duration_ms,
    wait_resource, statement_start_offset, statement_end_offset, plan_handle, sql_handle, most_recent_sql_handle, [Level])
    AS (
        SELECT head.session_id AS head_blocker_session_id, head.session_id AS session_id, head.blocking_session_id,
        head.wait_type, head.wait_time, head.wait_resource, head.statement_start_offset, head.statement_end_offset,
        head.plan_handle, head.sql_handle, head.most_recent_sql_handle, 0 AS [Level]
        FROM cteHead AS head
        WHERE (head.blocking_session_id IS NULL OR head.blocking_session_id = 0)
        AND head.session_id IN (SELECT DISTINCT blocking_session_id FROM cteHead WHERE blocking_session_id != 0)
        UNION ALL
        SELECT h.head_blocker_session_id, blocked.session_id, blocked.blocking_session_id, blocked.wait_type,
        blocked.wait_time, blocked.wait_resource, h.statement_start_offset, h.statement_end_offset,
        h.plan_handle, h.sql_handle, h.most_recent_sql_handle, [Level] + 1
        FROM cteHead AS blocked
        INNER JOIN cteBlockingHierarchy AS h ON h.session_id = blocked.blocking_session_id and h.session_id != blocked.session_id
        WHERE h.wait_type COLLATE Latin1_General_BIN NOT IN ('EXCHANGE', 'CXPACKET') or h.wait_type is null
    )
    SELECT bh.*, txt.text AS blocker_query_or_most_recent_query 
    FROM cteBlockingHierarchy AS bh 
    OUTER APPLY sys.dm_exec_sql_text(ISNULL([sql_handle], most_recent_sql_handle)) AS txt;
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

        private void ShowQueryStats()
        {
            // النص الاستعلامي لجلب إحصائيات الاستعلامات
            string sqlQuery = @"
    SELECT 
        CONVERT(varchar(16), query_stats.query_hash, 1) AS Query_Hash,   
        SUM(query_stats.total_worker_time) / SUM(query_stats.execution_count) AS Avg_CPU_Time,  
        MIN(query_stats.statement_text) AS Sample_Statement_Text
    FROM   
        (SELECT QS.*,   
        SUBSTRING(ST.text, (QS.statement_start_offset / 2) + 1,  
        ((CASE statement_end_offset   
            WHEN -1 THEN DATALENGTH(ST.text)  
            ELSE QS.statement_end_offset END   
                - QS.statement_start_offset) / 2) + 1) AS statement_text  
         FROM sys.dm_exec_query_stats AS QS  
         CROSS APPLY sys.dm_exec_sql_text(QS.sql_handle) AS ST) AS query_stats  
    GROUP BY query_stats.query_hash  
    ORDER BY 2 DESC;
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
        private void ShowRowCountAggregates()
        {
            // النص الاستعلامي لجلب المعلومات التجميعية لعدد الصفوف
            string sqlQuery = @"
SELECT qs.execution_count,
    SUBSTRING(qt.text, qs.statement_start_offset/2 + 1,
                 (CASE 
                      WHEN qs.statement_end_offset = -1
                      THEN LEN(CONVERT(nvarchar(max), qt.text)) * 2
                      ELSE qs.statement_end_offset - qs.statement_start_offset
                 END) / 2
             ) AS query_text,
    qt.dbid,
    DB_NAME(qt.dbid) AS dbname,
    qt.objectid,
    qs.total_rows,
    qs.last_rows,
    qs.min_rows,
    qs.max_rows
FROM sys.dm_exec_query_stats AS qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS qt
WHERE qt.text LIKE '%SELECT%'
ORDER BY qs.execution_count DESC;
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
            ExecuteUserDefinedFunctions();
        }

        private void button2ShowCurrentLocks_Click(object sender, EventArgs e)
        {
            ShowCurrentLocks();
        }

        private void button2ShowQueryStats_Click(object sender, EventArgs e)
        {
            ShowQueryStats();
        }

        private void button2ShowRowCountAggregates_Click(object sender, EventArgs e)
        {
            ShowRowCountAggregates();
        }

        private void button2ShowBlockingHierarchy_Click(object sender, EventArgs e)
        {
            ShowBlockingHierarchy();
        }
    }
}
