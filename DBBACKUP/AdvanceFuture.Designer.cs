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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.btnExecuteDatabaseFileQuery = new System.Windows.Forms.Button();
            this.button2ExecuteTopIOQueries = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(731, 372);
            this.dataGridView1.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(761, 66);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(390, 385);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Lucida Calligraphy", 10F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(11, 22);
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
            this.btnExecuteDatabaseFileQuery.Location = new System.Drawing.Point(165, 22);
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
            this.button2ExecuteTopIOQueries.Location = new System.Drawing.Point(318, 22);
            this.button2ExecuteTopIOQueries.Name = "button2ExecuteTopIOQueries";
            this.button2ExecuteTopIOQueries.Size = new System.Drawing.Size(160, 27);
            this.button2ExecuteTopIOQueries.TabIndex = 4;
            this.button2ExecuteTopIOQueries.Text = "تكاليف موارد الاستعلامات";
            this.button2ExecuteTopIOQueries.UseVisualStyleBackColor = true;
            this.button2ExecuteTopIOQueries.Click += new System.EventHandler(this.button2ExecuteTopIOQueries_Click);
            // 
            // AdvanceFuture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 450);
            this.Controls.Add(this.button2ExecuteTopIOQueries);
            this.Controls.Add(this.btnExecuteDatabaseFileQuery);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "AdvanceFuture";
            this.Text = "AdvanceFuture";
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
    }
}