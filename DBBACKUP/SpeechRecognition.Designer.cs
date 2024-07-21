namespace DBBACKUP
{
    partial class SpeechRecognition
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
            this.button1generate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1generate
            // 
            this.button1generate.Location = new System.Drawing.Point(369, 127);
            this.button1generate.Name = "button1generate";
            this.button1generate.Size = new System.Drawing.Size(75, 23);
            this.button1generate.TabIndex = 3;
            this.button1generate.Text = "generate";
            this.button1generate.UseVisualStyleBackColor = true;
            this.button1generate.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // SpeechRecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1generate);
            this.Name = "SpeechRecognition";
            this.Text = "SpeechRecognition";
            this.Load += new System.EventHandler(this.SpeechRecognition_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1generate;
    }
}