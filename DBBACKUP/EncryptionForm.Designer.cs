namespace DBBACKUP
{
    partial class EncryptionForm
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
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.txtOutputFile = new System.Windows.Forms.MaskedTextBox();
            this.btnBrowseInput = new System.Windows.Forms.Button();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.btnEncryp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtInputFile
            // 
            this.txtInputFile.Location = new System.Drawing.Point(103, 57);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(299, 20);
            this.txtInputFile.TabIndex = 0;
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(103, 115);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(299, 20);
            this.txtOutputFile.TabIndex = 1;
            // 
            // btnBrowseInput
            // 
            this.btnBrowseInput.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnBrowseInput.Location = new System.Drawing.Point(22, 54);
            this.btnBrowseInput.Name = "btnBrowseInput";
            this.btnBrowseInput.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseInput.TabIndex = 2;
            this.btnBrowseInput.Text = "اختر ملف";
            this.btnBrowseInput.UseVisualStyleBackColor = true;
            this.btnBrowseInput.Click += new System.EventHandler(this.btnBrowseInput_Click);
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnBrowseOutput.Location = new System.Drawing.Point(22, 112);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseOutput.TabIndex = 3;
            this.btnBrowseOutput.Text = "اختر ملف";
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // btnEncryp
            // 
            this.btnEncryp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnEncryp.Location = new System.Drawing.Point(167, 157);
            this.btnEncryp.Name = "btnEncryp";
            this.btnEncryp.Size = new System.Drawing.Size(75, 23);
            this.btnEncryp.TabIndex = 4;
            this.btnEncryp.Text = "تشفير";
            this.btnEncryp.UseVisualStyleBackColor = true;
            this.btnEncryp.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(167, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "تشفير الملفات ";
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnDecrypt.Location = new System.Drawing.Point(263, 157);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 6;
            this.btnDecrypt.Text = "فك التشفير";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // EncryptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 198);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEncryp);
            this.Controls.Add(this.btnBrowseOutput);
            this.Controls.Add(this.btnBrowseInput);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.txtInputFile);
            this.Name = "EncryptionForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "EncryptionForm";
            this.Load += new System.EventHandler(this.EncryptionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.MaskedTextBox txtOutputFile;
        private System.Windows.Forms.Button btnBrowseInput;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Button btnEncryp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDecrypt;
    }
}