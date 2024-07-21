namespace DBBACKUP
{
    partial class QueryAnalyzer
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
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.btnExecute = new System.Windows.Forms.Button();
            this.webBrowserPlan = new System.Windows.Forms.WebBrowser();
            this.richTextBox1txtSqlQuery = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvResults
            // 
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(22, 47);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.Size = new System.Drawing.Size(624, 205);
            this.dgvResults.TabIndex = 0;
            this.dgvResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellContentClick);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(33, 279);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 70);
            this.btnExecute.TabIndex = 2;
            this.btnExecute.Text = "btnExecute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // webBrowserPlan
            // 
            this.webBrowserPlan.Location = new System.Drawing.Point(662, 12);
            this.webBrowserPlan.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPlan.Name = "webBrowserPlan";
            this.webBrowserPlan.Size = new System.Drawing.Size(403, 250);
            this.webBrowserPlan.TabIndex = 3;
            // 
            // richTextBox1txtSqlQuery
            // 
            this.richTextBox1txtSqlQuery.Location = new System.Drawing.Point(148, 267);
            this.richTextBox1txtSqlQuery.Name = "richTextBox1txtSqlQuery";
            this.richTextBox1txtSqlQuery.Size = new System.Drawing.Size(476, 96);
            this.richTextBox1txtSqlQuery.TabIndex = 4;
            this.richTextBox1txtSqlQuery.Text = "";
            // 
            // QueryAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 450);
            this.Controls.Add(this.richTextBox1txtSqlQuery);
            this.Controls.Add(this.webBrowserPlan);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.dgvResults);
            this.Name = "QueryAnalyzer";
            this.Text = "QueryAnalyzer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.WebBrowser webBrowserPlan;
        private System.Windows.Forms.RichTextBox richTextBox1txtSqlQuery;
    }
}