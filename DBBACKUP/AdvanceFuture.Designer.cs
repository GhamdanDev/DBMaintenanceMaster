namespace DBBACKUP
{
    partial class AdvanceFuture
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.btnExecuteDatabaseFileQuery = new System.Windows.Forms.Button();
            this.button2ExecuteTopIOQueries = new System.Windows.Forms.Button();
            this.button2FindLastModiedStoredProcedures = new System.Windows.Forms.Button();
            this.button2ActiveTransactions = new System.Windows.Forms.Button();
            this.button2UserDefinedFunctions = new System.Windows.Forms.Button();
            this.button2ShowCurrentLocks = new System.Windows.Forms.Button();
            this.button2ShowQueryStats = new System.Windows.Forms.Button();
            this.button2ShowRowCountAggregates = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 132);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(731, 306);
            this.dataGridView1.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(761, 132);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(390, 319);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Lucida Calligraphy", 10F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(13, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = " تحليل أوقات الانتظار للعمليات";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnExecuteDatabaseFileQuery
            // 
            this.btnExecuteDatabaseFileQuery.Font = new System.Drawing.Font("Lucida Calligraphy", 10F, System.Drawing.FontStyle.Bold);
            this.btnExecuteDatabaseFileQuery.Location = new System.Drawing.Point(167, 22);
            this.btnExecuteDatabaseFileQuery.Name = "btnExecuteDatabaseFileQuery";
            this.btnExecuteDatabaseFileQuery.Size = new System.Drawing.Size(137, 27);
            this.btnExecuteDatabaseFileQuery.TabIndex = 3;
            this.btnExecuteDatabaseFileQuery.Text = "تحليلات المساحة";
            this.btnExecuteDatabaseFileQuery.UseVisualStyleBackColor = true;
            this.btnExecuteDatabaseFileQuery.Click += new System.EventHandler(this.btnExecuteDatabaseFileQuery_click);
            // 
            // button2ExecuteTopIOQueries
            // 
            this.button2ExecuteTopIOQueries.Font = new System.Drawing.Font("Lucida Calligraphy", 10F, System.Drawing.FontStyle.Bold);
            this.button2ExecuteTopIOQueries.Location = new System.Drawing.Point(320, 22);
            this.button2ExecuteTopIOQueries.Name = "button2ExecuteTopIOQueries";
            this.button2ExecuteTopIOQueries.Size = new System.Drawing.Size(160, 27);
            this.button2ExecuteTopIOQueries.TabIndex = 4;
            this.button2ExecuteTopIOQueries.Text = "تكاليف موارد الاستعلامات";
            this.button2ExecuteTopIOQueries.UseVisualStyleBackColor = true;
            this.button2ExecuteTopIOQueries.Click += new System.EventHandler(this.button2ExecuteTopIOQueries_Click);
            // 
            // button2FindLastModiedStoredProcedures
            // 
            this.button2FindLastModiedStoredProcedures.Font = new System.Drawing.Font("Lucida Calligraphy", 10F, System.Drawing.FontStyle.Bold);
            this.button2FindLastModiedStoredProcedures.Location = new System.Drawing.Point(490, 21);
            this.button2FindLastModiedStoredProcedures.Name = "button2FindLastModiedStoredProcedures";
            this.button2FindLastModiedStoredProcedures.Size = new System.Drawing.Size(160, 27);
            this.button2FindLastModiedStoredProcedures.TabIndex = 5;
            this.button2FindLastModiedStoredProcedures.Text = "اخر اجراءت";
            this.button2FindLastModiedStoredProcedures.UseVisualStyleBackColor = true;
            this.button2FindLastModiedStoredProcedures.Click += new System.EventHandler(this.button2FindLastModiedStoredProcedures_Click);
            // 
            // button2ActiveTransactions
            // 
            this.button2ActiveTransactions.Font = new System.Drawing.Font("Lucida Calligraphy", 10F, System.Drawing.FontStyle.Bold);
            this.button2ActiveTransactions.Location = new System.Drawing.Point(665, 22);
            this.button2ActiveTransactions.Name = "button2ActiveTransactions";
            this.button2ActiveTransactions.Size = new System.Drawing.Size(160, 27);
            this.button2ActiveTransactions.TabIndex = 6;
            this.button2ActiveTransactions.Text = "المعاملات النشطه";
            this.button2ActiveTransactions.UseVisualStyleBackColor = true;
            this.button2ActiveTransactions.Click += new System.EventHandler(this.ExecuteActiveTransactions);
            // 
            // button2UserDefinedFunctions
            // 
            this.button2UserDefinedFunctions.Font = new System.Drawing.Font("Lucida Calligraphy", 10F, System.Drawing.FontStyle.Bold);
            this.button2UserDefinedFunctions.Location = new System.Drawing.Point(831, 21);
            this.button2UserDefinedFunctions.Name = "button2UserDefinedFunctions";
            this.button2UserDefinedFunctions.Size = new System.Drawing.Size(160, 27);
            this.button2UserDefinedFunctions.TabIndex = 7;
            this.button2UserDefinedFunctions.Text = "دوال المستخدم ";
            this.button2UserDefinedFunctions.UseVisualStyleBackColor = true;
            this.button2UserDefinedFunctions.Click += new System.EventHandler(this.button2UserDefinedFunctions_Click);
            // 
            // button2ShowCurrentLocks
            // 
            this.button2ShowCurrentLocks.Font = new System.Drawing.Font("Lucida Calligraphy", 10F, System.Drawing.FontStyle.Bold);
            this.button2ShowCurrentLocks.Location = new System.Drawing.Point(1007, 22);
            this.button2ShowCurrentLocks.Name = "button2ShowCurrentLocks";
            this.button2ShowCurrentLocks.Size = new System.Drawing.Size(144, 27);
            this.button2ShowCurrentLocks.TabIndex = 8;
            this.button2ShowCurrentLocks.Text = "فحص تعارض الاقفال ";
            this.button2ShowCurrentLocks.UseVisualStyleBackColor = true;
            this.button2ShowCurrentLocks.Click += new System.EventHandler(this.button2ShowCurrentLocks_Click);
            // 
            // button2ShowQueryStats
            // 
            this.button2ShowQueryStats.Font = new System.Drawing.Font("Lucida Calligraphy", 10F, System.Drawing.FontStyle.Bold);
            this.button2ShowQueryStats.Location = new System.Drawing.Point(12, 81);
            this.button2ShowQueryStats.Name = "button2ShowQueryStats";
            this.button2ShowQueryStats.Size = new System.Drawing.Size(198, 27);
            this.button2ShowQueryStats.TabIndex = 9;
            this.button2ShowQueryStats.Text = "إحصائيات الاستعلامات في الاداء";
            this.button2ShowQueryStats.UseVisualStyleBackColor = true;
            this.button2ShowQueryStats.Click += new System.EventHandler(this.button2ShowQueryStats_Click);
            // 
            // button2ShowRowCountAggregates
            // 
            this.button2ShowRowCountAggregates.Font = new System.Drawing.Font("Lucida Calligraphy", 10F, System.Drawing.FontStyle.Bold);
            this.button2ShowRowCountAggregates.Location = new System.Drawing.Point(221, 81);
            this.button2ShowRowCountAggregates.Name = "button2ShowRowCountAggregates";
            this.button2ShowRowCountAggregates.Size = new System.Drawing.Size(137, 27);
            this.button2ShowRowCountAggregates.TabIndex = 10;
            this.button2ShowRowCountAggregates.Text = "إحصائيات الاستعلامات في الحجم";
            this.button2ShowRowCountAggregates.UseVisualStyleBackColor = true;
            this.button2ShowRowCountAggregates.Click += new System.EventHandler(this.button2ShowRowCountAggregates_Click);
            // 
            // AdvanceFuture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 450);
            this.Controls.Add(this.button2ShowRowCountAggregates);
            this.Controls.Add(this.button2ShowQueryStats);
            this.Controls.Add(this.button2ShowCurrentLocks);
            this.Controls.Add(this.button2UserDefinedFunctions);
            this.Controls.Add(this.button2ActiveTransactions);
            this.Controls.Add(this.button2FindLastModiedStoredProcedures);
            this.Controls.Add(this.button2ExecuteTopIOQueries);
            this.Controls.Add(this.btnExecuteDatabaseFileQuery);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "AdvanceFuture";
            this.Text = "AdvanceFuture";
            this.Load += new System.EventHandler(this.AdvanceFuture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnExecuteDatabaseFileQuery;
        private System.Windows.Forms.Button button2ExecuteTopIOQueries;
        private System.Windows.Forms.Button button2FindLastModiedStoredProcedures;
        private System.Windows.Forms.Button button2ActiveTransactions;
        private System.Windows.Forms.Button button2UserDefinedFunctions;
        private System.Windows.Forms.Button button2ShowCurrentLocks;
        private System.Windows.Forms.Button button2ShowQueryStats;
        private System.Windows.Forms.Button button2ShowRowCountAggregates;
    }
}