namespace DBBACKUP
{
    partial class Form1
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
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cmbbackup = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ComboBoxDatabaseName = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.ComboBoxserverName = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblFullName = new System.Windows.Forms.Label();
            this.ctlLastName = new DBBACKUP.ClearableTextBox();
            this.ctlFirstName = new DBBACKUP.ClearableTextBox();
            this.SuspendLayout();
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.Filter = "Bakup files (*.Bak)|*.Bak";
            // 
            // SaveFileDialog1
            // 
            this.SaveFileDialog1.Filter = "Bakup files (*.Bak)|*.Bak";
            // 
            // cmbbackup
            // 
            this.cmbbackup.Location = new System.Drawing.Point(42, 136);
            this.cmbbackup.Name = "cmbbackup";
            this.cmbbackup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbbackup.Size = new System.Drawing.Size(83, 23);
            this.cmbbackup.TabIndex = 36;
            this.cmbbackup.Text = "ابدا الاستعاده";
            this.cmbbackup.UseVisualStyleBackColor = true;
            this.cmbbackup.Click += new System.EventHandler(this.cmbbackup_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.SeaGreen;
            this.label3.Location = new System.Drawing.Point(131, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "label3";
            // 
            // ComboBoxDatabaseName
            // 
            this.ComboBoxDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBoxDatabaseName.FormattingEnabled = true;
            this.ComboBoxDatabaseName.Location = new System.Drawing.Point(42, 89);
            this.ComboBoxDatabaseName.Name = "ComboBoxDatabaseName";
            this.ComboBoxDatabaseName.Size = new System.Drawing.Size(223, 24);
            this.ComboBoxDatabaseName.TabIndex = 31;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(319, 38);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(86, 16);
            this.Label2.TabIndex = 34;
            this.Label2.Text = "اختر السيرفير";
            this.Label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // ComboBoxserverName
            // 
            this.ComboBoxserverName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBoxserverName.FormattingEnabled = true;
            this.ComboBoxserverName.Location = new System.Drawing.Point(42, 35);
            this.ComboBoxserverName.Name = "ComboBoxserverName";
            this.ComboBoxserverName.Size = new System.Drawing.Size(223, 23);
            this.ComboBoxserverName.TabIndex = 32;
            this.ComboBoxserverName.SelectedIndexChanged += new System.EventHandler(this.ComboBoxserverName_SelectedIndexChanged_1);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(281, 92);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(124, 16);
            this.Label1.TabIndex = 33;
            this.Label1.Text = "اختر قاعدة البيانات ";
            this.Label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(42, 165);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 37;
            this.button1.Text = "ابدا الاستعاده الئ جديده";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(753, 241);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(35, 13);
            this.lblFullName.TabIndex = 40;
            this.lblFullName.Text = "label4";
            this.lblFullName.Click += new System.EventHandler(this.lblFullName_Click);
            // 
            // ctlLastName
            // 
            this.ctlLastName.Location = new System.Drawing.Point(510, 241);
            this.ctlLastName.MinimumSize = new System.Drawing.Size(84, 53);
            this.ctlLastName.Name = "ctlLastName";
            this.ctlLastName.Size = new System.Drawing.Size(191, 53);
            this.ctlLastName.TabIndex = 39;
            this.ctlLastName.Title = "lblTitle";
            this.ctlLastName.TextChanged += new System.EventHandler(this.ctlLastName_TextChanged);
            // 
            // ctlFirstName
            // 
            this.ctlFirstName.Location = new System.Drawing.Point(510, 165);
            this.ctlFirstName.MinimumSize = new System.Drawing.Size(84, 53);
            this.ctlFirstName.Name = "ctlFirstName";
            this.ctlFirstName.Size = new System.Drawing.Size(191, 53);
            this.ctlFirstName.TabIndex = 38;
            this.ctlFirstName.Title = "lblTitle";
            this.ctlFirstName.TextChanged += new System.EventHandler(this.ctlFirstName_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 320);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.ctlLastName);
            this.Controls.Add(this.ctlFirstName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbbackup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ComboBoxDatabaseName);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.ComboBoxserverName);
            this.Controls.Add(this.Label1);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "استعادة قاعدة بيانات ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.SaveFileDialog SaveFileDialog1;
        private System.Windows.Forms.Button cmbbackup;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox ComboBoxDatabaseName;
        internal System.Windows.Forms.Label Label2;
        public System.Windows.Forms.ComboBox ComboBoxserverName;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button button1;
        private ClearableTextBox ctlFirstName;
        private ClearableTextBox ctlLastName;
        private System.Windows.Forms.Label lblFullName;
    }
}