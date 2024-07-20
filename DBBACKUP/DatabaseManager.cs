using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBBACKUP
{
    class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
        }




        public void CheckAndCreateDatabaseTables(String databaseName)
        {
            try
            {
                // Connection string to your SQL Server instance
                string connectionString = "Data Source=.;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the database exists
                    bool databaseExists = CheckDatabaseExists(connection, databaseName);
                    if (!databaseExists)
                    {
                        // Create the database
                        CreateDatabase(connection, databaseName);
                        Console.WriteLine("Database created successfully.");
                    }

                    // Check if the tblBackupInfo table exists
                    bool tblBackupInfoExists = CheckTableExists(connection, "tblBackupInfo");
                    if (!tblBackupInfoExists)
                    {
                        // Create the tblBackupInfo table
                        CreatetblBackupInfoTable(connection);
                        Console.WriteLine("tblBackupInfo table created successfully.");
                    }

                    // Check if the tblBackupDetails table exists
                    bool tblBackupDetailsExists = CheckTableExists(connection, "tblBackupDetails");
                    if (!tblBackupDetailsExists)
                    {
                        // Create the tblBackupDetails table
                        CreatetblBackupDetailsTable(connection);
                        Console.WriteLine("tblBackupDetails table created successfully.");
                    }

                    // Check if the DATABASE_BACKUP stored procedure exists
                    bool databaseBackupProcExists = CheckStoredProcedureExists(connection, "DATABASE_BACKUP");
                    if (!databaseBackupProcExists)
                    {
                        // Create the DATABASE_BACKUP stored procedure
                        CreateDatabaseBackupStoredProcedure(connection);
                        Console.WriteLine("DATABASE_BACKUP stored procedure created successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private bool CheckDatabaseExists(SqlConnection connection, string databaseName)
        {
            bool databaseExists = false;

            using (SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM sys.databases WHERE name = '{databaseName}'", connection))
            {
                int count = (int)command.ExecuteScalar();
                databaseExists = count > 0;
            }

            return databaseExists;
        }

        /*    private void CreateDatabase(SqlConnection connection, string databaseName)
            {
                string createDatabaseSql = $@"CREATE DATABASE [{databaseName}];


       CREATE TABLE [dbo].[tblBackupInfo](
                [IID] [int] IDENTITY(1,1) NOT NULL,
                [DayInterval] [int] NULL,
                [NoOfFiles] [int] NULL,
                [DatabaseName] [nvarchar](500) NULL,
                [Location] [nvarchar](max) NULL,
                [SoftwareDate] [datetime] NULL,
                [LastEditDate] [datetime] NULL,
                [CreationDate] [datetime] NOT NULL
            );



     CREATE PROCEDURE [dbo].[DATABASE_BACKUP]
            (
                @DatabaseName VARCHAR(1000) = NULL,
                @Location VARCHAR(1000) = NULL,
                @Type VARCHAR(25) = NULL,
                @BackupName VARCHAR(500) = NULL,
                @FILEPATH VARCHAR(2000) = NULL,
                @DATABASE VARCHAR(1000) = NULL,
                @DayInterval INT = NULL,
                @NoOfFiles INT = NULL,
                @SoftwareDate DATE = NULL,
                @ACTIONTYPE VARCHAR(50)
            )
            AS
            BEGIN
                -- Procedure code goes here
      IF @ACTIONTYPE = 'BACKUP_INFO'  
        BEGIN  
            SELECT DATABASENAME,ISNULL(NoOfFiles,0) AS NoOfFiles,LOCATION,DayInterval FROM tblBackupInfo  
            SELECT TOP 1 BackupType,BackupDate,Location FROM tblBackupDetails ORDER BY IID DESC  
        END  

        IF @ACTIONTYPE = 'INSERT_BACKUP_INFO'  
        BEGIN  
            IF NOT EXISTS (SELECT * FROM tblBackupInfo)  
            BEGIN  
                INSERT INTO tblBackupInfo (DayInterval,NoOfFiles,DatabaseName,Location,SoftwareDate,CreationDate)  
                VALUES (@DayInterval,@NoOfFiles,@DatabaseName,@Location,@SoftwareDate,GETDATE())  
            END  
            ELSE  
            BEGIN  
                UPDATE tblBackupInfo SET DayInterval=@DayInterval,NoOfFiles=@NoOfFiles,DatabaseName=@DatabaseName,  
                Location=@Location,SoftwareDate=@SoftwareDate,LastEditDate=GETDATE()  
            END  
        END  

        IF @ACTIONTYPE = 'DB_BACKUP'  
        BEGIN  
            BEGIN TRY  
                BACKUP DATABASE @DATABASE  
                TO DISK = @FILEPATH;  

                INSERT INTO tblBackupDetails VALUES(@BackupName,@FILEPATH,@SoftwareDate,@Type,GETDATE())              
            END TRY  
            BEGIN CATCH  
                SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_SEVERITY() AS ErrorSeverity,ERROR_STATE() AS ErrorState,  
                       ERROR_PROCEDURE() AS ErrorProcedure,ERROR_LINE() AS ErrorLine,ERROR_MESSAGE() AS ErrorMessage;  
            END CATCH  
        END  

        IF @ACTIONTYPE = 'REMOVE_LOCATION'  
        BEGIN  
            SELECT Location FROM tblBackupDetails WHERE IID NOT IN (  
                SELECT TOP (SELECT NoOfFiles FROM tblBackupInfo) IID FROM tblBackupDetails ORDER BY IID DESC)  
        END  
            END


    ";

                using (SqlCommand command = new SqlCommand(createDatabaseSql, connection))
                {
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("create dabatbas successfl ");

                CreatetblBackupDetailsTable(connection);
                CreateDatabaseBackupStoredProcedure(connection);
            }
    */


        private void CreateDatabase(SqlConnection connection, string databaseName)
        {
            // Create the database
            string createDatabaseSql = $"CREATE DATABASE [{databaseName}]";

            using (SqlCommand command = new SqlCommand(createDatabaseSql, connection))
            {
                command.ExecuteNonQuery();
            }

            // Message box to indicate database creation success
            MessageBox.Show("Database created successfully.");

            // Now connect to the new database to create tables and stored procedures
            string newConnectionString = $"Data Source=.;Integrated Security=True;Initial Catalog={databaseName};";
            using (SqlConnection newConnection = new SqlConnection(newConnectionString))
            {
                newConnection.Open();

                // Create tables and stored procedures
                CreatetblBackupInfoTable(newConnection);
                CreatetblBackupDetailsTable(newConnection);
                CreateDatabaseBackupStoredProcedure(newConnection);
            }
        }

        private bool CheckTableExists(SqlConnection connection, string tableName)
        {
            bool tableExists = false;

            using (SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'", connection))
            {
                int count = (int)command.ExecuteScalar();
                tableExists = count > 0;
            }

            return tableExists;
        }

        private bool CheckStoredProcedureExists(SqlConnection connection, string procedureName)
        {
            bool procedureExists = false;

            using (SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = '{procedureName}'", connection))
            {
                int count = (int)command.ExecuteScalar();
                procedureExists = count > 0;
            }

            return procedureExists;
        }

        private void CreatetblBackupInfoTable(SqlConnection connection)
        {
            
            string createTableSql = @"
        CREATE TABLE [dbo].[tblBackupInfo](
            [IID] [int] IDENTITY(1,1) NOT NULL,
            [DayInterval] [int] NULL,
            [NoOfFiles] [int] NULL,
            [DatabaseName] [nvarchar](500) NULL,
            [Location] [nvarchar](max) NULL,
            [SoftwareDate] [datetime] NULL,
            [LastEditDate] [datetime] NULL,
            [CreationDate] [datetime] NOT NULL
        )";

            using (SqlCommand command = new SqlCommand(createTableSql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private void CreatetblBackupDetailsTable(SqlConnection connection)
        {

            MessageBox.Show("from table creator ");
            string createTableSql = @"
        CREATE TABLE [dbo].[tblBackupDetails](
            [IID] [int] IDENTITY(1,1) NOT NULL,
            [BackupName] [varchar](50) NULL,
            [Location] [varchar](500) NULL,
            [BackupDate] [datetime] NULL,
            [BackupType] [varchar](50) NULL,
            [CreationDate] [datetime] NOT NULL
        )";

            using (SqlCommand command = new SqlCommand(createTableSql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private void CreateDatabaseBackupStoredProcedure(SqlConnection connection)
        {

            MessageBox.Show("from StoredProcedure creator");
            string createProcedureSql = @"
 

        CREATE PROCEDURE [dbo].[DATABASE_BACKUP]
        (
            @DatabaseName VARCHAR(1000) = NULL,
            @Location VARCHAR(1000) = NULL,
            @Type VARCHAR(25) = NULL,
            @BackupName VARCHAR(500) = NULL,
            @FILEPATH VARCHAR(2000) = NULL,
            @DATABASE VARCHAR(1000) = NULL,
            @DayInterval INT = NULL,
            @NoOfFiles INT = NULL,
            @SoftwareDate DATE = NULL,
            @ACTIONTYPE VARCHAR(50)
        )
        AS
        BEGIN
            -- Procedure code goes here
  IF @ACTIONTYPE = 'BACKUP_INFO'  
    BEGIN  
        SELECT DATABASENAME,ISNULL(NoOfFiles,0) AS NoOfFiles,LOCATION,DayInterval FROM tblBackupInfo  
        SELECT TOP 1 BackupType,BackupDate,Location FROM tblBackupDetails ORDER BY IID DESC  
    END  
  
    IF @ACTIONTYPE = 'INSERT_BACKUP_INFO'  
    BEGIN  
        IF NOT EXISTS (SELECT * FROM tblBackupInfo)  
        BEGIN  
            INSERT INTO tblBackupInfo (DayInterval,NoOfFiles,DatabaseName,Location,SoftwareDate,CreationDate)  
            VALUES (@DayInterval,@NoOfFiles,@DatabaseName,@Location,@SoftwareDate,GETDATE())  
        END  
        ELSE  
        BEGIN  
            UPDATE tblBackupInfo SET DayInterval=@DayInterval,NoOfFiles=@NoOfFiles,DatabaseName=@DatabaseName,  
            Location=@Location,SoftwareDate=@SoftwareDate,LastEditDate=GETDATE()  
        END  
    END  
  
    IF @ACTIONTYPE = 'DB_BACKUP'  
    BEGIN  
        BEGIN TRY  
            BACKUP DATABASE @DATABASE  
            TO DISK = @FILEPATH;  
  
            INSERT INTO tblBackupDetails VALUES(@BackupName,@FILEPATH,@SoftwareDate,@Type,GETDATE())              
        END TRY  
        BEGIN CATCH  
            SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_SEVERITY() AS ErrorSeverity,ERROR_STATE() AS ErrorState,  
                   ERROR_PROCEDURE() AS ErrorProcedure,ERROR_LINE() AS ErrorLine,ERROR_MESSAGE() AS ErrorMessage;  
        END CATCH  
    END  
  
    IF @ACTIONTYPE = 'REMOVE_LOCATION'  
    BEGIN  
        SELECT Location FROM tblBackupDetails WHERE IID NOT IN (  
            SELECT TOP (SELECT NoOfFiles FROM tblBackupInfo) IID FROM tblBackupDetails ORDER BY IID DESC)  
    END  
        END";

            using (SqlCommand command = new SqlCommand(createProcedureSql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

    }
}
