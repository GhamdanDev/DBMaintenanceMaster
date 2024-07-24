namespace DBBACKUP
{
    partial class ErrorMessagesChatGPT
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
            this.txtInput = new System.Windows.Forms.TextBox();
            this.rtbChat = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(22, 279);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(478, 20);
            this.txtInput.TabIndex = 0;
            // 
            // rtbChat
            // 
            this.rtbChat.Location = new System.Drawing.Point(22, 73);
            this.rtbChat.Name = "rtbChat";
            this.rtbChat.Size = new System.Drawing.Size(552, 188);
            this.rtbChat.TabIndex = 1;
            this.rtbChat.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(506, 276);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(58, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ErrorMessagesChatGPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 450);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtbChat);
            this.Controls.Add(this.txtInput);
            this.Name = "ErrorMessagesChatGPT";
            this.Text = "ErrorMessagesChatGPT";
            this.Load += new System.EventHandler(this.ErrorMessagesChatGPT_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.Button btnSend;
    }
}