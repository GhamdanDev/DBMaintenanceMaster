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

    public partial class BackupInfoForm : Form
    {
        public BackupInfoForm()
        {
            InitializeComponent();
        }
/*
        private void btnSave_Click(object sender, EventArgs e)
        {
            // استبدل بـ بيانات الاتصال الخاصة بك
            string connectionString = "Server=myServerAddress;Database=infoDB;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO tblBackupInfWithIntervalType (DayInterval, NoOfFiles, DatabaseName, Location, SoftwareDate, LastEditDate, CreationDate, IntervalType) " +
                               "VALUES (@DayInterval, @NoOfFiles, @DatabaseName, @Location, @SoftwareDate, @LastEditDate, @CreationDate, @IntervalType)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // تعيين القيم لملف النسخ الاحتياطي
                    command.Parameters.AddWithValue("@DayInterval", int.Parse(txtDayInterval.Text));
                    command.Parameters.AddWithValue("@NoOfFiles", int.Parse(txtNoOfFiles.Text));
                    command.Parameters.AddWithValue("@DatabaseName", txtDatabaseName.Text);
                    command.Parameters.AddWithValue("@Location", txtLocation.Text);
                    command.Parameters.AddWithValue("@SoftwareDate", dtpSoftwareDate.Value);
                    command.Parameters.AddWithValue("@LastEditDate", dtpLastEditDate.Value);
                    command.Parameters.AddWithValue("@CreationDate", dtpCreationDate.Value);

                    // تعيين نوع الفاصل الزمني
                    int intervalType = radioButtonDaily.Checked ? 1 :
                                       radioButtonHourly.Checked ? 2 : 3;
                    command.Parameters.AddWithValue("@IntervalType", intervalType);

                    // فتح الاتصال و تنفيذ الاستعلام
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            MessageBox.Show("Data saved successfully!");
        }
  */  }
}
