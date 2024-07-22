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
   
   public partial class QueryAnalyzer : Form
    {
        private string connectionString = "Data Source =.; Initial Catalog = infoDB; Integrated Security = True;";

        public QueryAnalyzer()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            string query = richTextBox1txtSqlQuery.Text;

            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Please enter a SQL query.");
                return;
            }

            try
            {
                // Execute the query and populate the DataGridView
                GetQueryStats(query);

                // Display the query plan
                string sqlHandle = GetSqlHandle(query);
                //if (!string.IsNullOrEmpty(sqlHandle){
                    DisplayQueryPlan(sqlHandle);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void GetQueryStats(string query)
        {

            MessageBox.Show($"from GetQueryStats  ");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
                    SELECT SUBSTRING(dest.text, (deqs.statement_start_offset / 2) + 1,
                        (CASE deqs.statement_end_offset
                            WHEN -1 THEN DATALENGTH(dest.text)
                            ELSE deqs.statement_end_offset
                                 - deqs.statement_start_offset
                        END) / 2 + 1) AS querystatement,
                        deqp.query_plan,
                        deqs.execution_count,
                        deqs.total_worker_time,
                        deqs.total_logical_reads,
                        deqs.total_elapsed_time
                    FROM sys.dm_exec_query_stats AS deqs
                    CROSS APPLY sys.dm_exec_sql_text(deqs.sql_handle) AS dest
                    CROSS APPLY sys.dm_exec_query_plan(deqs.plan_handle) AS deqp";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    dgvResults.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        /*     private string GetSqlHandle(string query)
             {
                 // Method to get SQL handle based on the query
                 // This method is a placeholder; you may need to adjust it based on actual requirements
                 return "your_sql_handle_here";
             }
     */
        private string GetSqlHandle(string query)
        {
            MessageBox.Show($"from GetSqlHandle");
            // تأكد من عدم وجود استعلامات فارغة
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query cannot be null or empty.");
            }

            // استعلام للعثور على sql_handle بناءً على نص الاستعلام
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
            SELECT TOP 1 deqs.sql_handle
            FROM sys.dm_exec_query_stats AS deqs
            CROSS APPLY sys.dm_exec_sql_text(deqs.sql_handle) AS dest
            WHERE dest.text LIKE @QueryText
            ORDER BY deqs.creation_time DESC;"; // ترتيب حسب وقت الإنشاء للحصول على أحدث المطابقات

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@QueryText", "%" + query.Replace("'", "''") + "%"); // تأكد من الهروب من علامات الاقتباس

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    return result?.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while retrieving SQL handle: {ex.Message}");
                    return null;
                }
            }
        }

        /*  private void DisplayQueryPlan(string sqlHandle)
          {
              MessageBox.Show($"from DisplayQueryPlan ");
              using (SqlConnection connection = new SqlConnection(connectionString))
              {
                  string sqlQuery = @"
                      SELECT deqp.query_plan
                      FROM sys.dm_exec_query_stats AS deqs
                      CROSS APPLY sys.dm_exec_query_plan(deqs.plan_handle) AS deqp
                      WHERE deqs.sql_handle = @SqlHandle";

                  SqlCommand command = new SqlCommand(sqlQuery, connection);
                  command.Parameters.AddWithValue("@SqlHandle", sqlHandle);

                  try
                  {
                      connection.Open();
                      string queryPlan = command.ExecuteScalar()?.ToString();
                      webBrowserPlan.DocumentText = queryPlan;
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show($"An error occurred: {ex.Message}");
                  }
              }
          }
  */
    private void DisplayQueryPlan(string sqlHandle)
{
    MessageBox.Show($"from DisplayQueryPlan ");
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        string sqlQuery = @"
            SELECT deqp.query_plan
            FROM sys.dm_exec_requests AS der
            CROSS APPLY sys.dm_exec_query_plan(der.plan_handle) AS deqp
            WHERE der.sql_handle = @SqlHandle";

        SqlCommand command = new SqlCommand(sqlQuery, connection);
        command.Parameters.AddWithValue("@SqlHandle", sqlHandle);

        try
        {
            connection.Open();
            string queryPlan = command.ExecuteScalar()?.ToString();

            // تأكد من أن خطة الاستعلام ليست فارغة
            if (string.IsNullOrEmpty(queryPlan))
            {
                MessageBox.Show("Query plan is empty.");
                return;
            }

            // عرض محتوى خطة الاستعلام في MessageBox
            MessageBox.Show(queryPlan);

            // حفظ خطة الاستعلام إلى ملف للتحقق منها
            System.IO.File.WriteAllText("queryPlan.xml", queryPlan);

            // تحقق من أن خطة الاستعلام بصيغة XML
            if (!queryPlan.StartsWith("<ShowPlanXML"))
            {
                MessageBox.Show("Query plan is not in XML format.");
                return;
            }

            // عرض خطة الاستعلام في مستعرض الويب
            webBrowserPlan.DocumentText = queryPlan;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}");
        }
    }
}

        private void dgvResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
