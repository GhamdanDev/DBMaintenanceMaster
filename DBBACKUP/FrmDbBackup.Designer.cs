namespace DBBACKUP
{
    partial class FrmDbBackup
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
            this.components = new System.ComponentModel.Container();
            this.Label21 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label9 = new System.Windows.Forms.Label();
            this.ProgressBarEx5 = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.linkLabel2 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSpan = new System.Windows.Forms.TextBox();
            this.txtNoOfFiles = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblLastBackupInfo = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.ComboBoxserverName = new System.Windows.Forms.ComboBox();
            this.ComboBoxDatabaseName = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label21
            // 
            this.Label21.BackColor = System.Drawing.Color.SteelBlue;
            this.Label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label21.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label21.ForeColor = System.Drawing.Color.White;
            this.Label21.Location = new System.Drawing.Point(0, 0);
            this.Label21.Name = "Label21";
            this.Label21.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Label21.Size = new System.Drawing.Size(984, 25);
            this.Label21.TabIndex = 3;
            this.Label21.Text = "نظام ادارة  قواعد البيانات";
            this.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.Label21.Click += new System.EventHandler(this.Label21_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.SteelBlue;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MistyRose;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(925, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(63, 24);
            this.btnClose.TabIndex = 1284;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "خروج                  ";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Panel1
            // 
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.Label9);
            this.Panel1.Controls.Add(this.ProgressBarEx5);
            this.Panel1.Location = new System.Drawing.Point(4, 428);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(980, 62);
            this.Panel1.TabIndex = 1354;
            this.Panel1.Visible = false;
            this.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label9.Location = new System.Drawing.Point(7, 10);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(168, 17);
            this.Label9.TabIndex = 33;
            this.Label9.Text = "..... جار عمل النسخة الاحتياطية";
            // 
            // ProgressBarEx5
            // 
            this.ProgressBarEx5.Location = new System.Drawing.Point(-1, 35);
            this.ProgressBarEx5.Name = "ProgressBarEx5";
            this.ProgressBarEx5.Size = new System.Drawing.Size(980, 16);
            this.ProgressBarEx5.TabIndex = 33;
            this.ProgressBarEx5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(30, 368);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 17);
            this.label6.TabIndex = 1360;
            this.label6.Text = "اسم القاعدة";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(619, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 17);
            this.label5.TabIndex = 1358;
            this.label5.Text = "اسم قاعدة البيانات ";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(118, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 17);
            this.label3.TabIndex = 1355;
            this.label3.Text = "التاريخ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // DateTimePicker1
            // 
            this.DateTimePicker1.Enabled = false;
            this.DateTimePicker1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePicker1.Location = new System.Drawing.Point(12, 60);
            this.DateTimePicker1.Name = "DateTimePicker1";
            this.DateTimePicker1.Size = new System.Drawing.Size(100, 25);
            this.DateTimePicker1.TabIndex = 1348;
            // 
            // LinkLabel1
            // 
            this.LinkLabel1.AutoSize = true;
            this.LinkLabel1.LinkColor = System.Drawing.Color.Teal;
            this.LinkLabel1.Location = new System.Drawing.Point(266, 126);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(0, 17);
            this.LinkLabel1.TabIndex = 1352;
            this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Label7.Location = new System.Drawing.Point(262, 101);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(109, 17);
            this.Label7.TabIndex = 1351;
            this.Label7.Text = "موقع حفظ النسخة";
            this.Label7.Click += new System.EventHandler(this.Label7_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSave.Location = new System.Drawing.Point(115, 152);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 25);
            this.btnSave.TabIndex = 1349;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.SteelBlue;
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(982, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(2, 458);
            this.label13.TabIndex = 1362;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.SteelBlue;
            this.label22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label22.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(0, 481);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(982, 2);
            this.label22.TabIndex = 1363;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.SteelBlue;
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(0, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(2, 456);
            this.label20.TabIndex = 1364;
            // 
            // Timer1
            // 
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // FolderBrowserDialog1
            // 
            this.FolderBrowserDialog1.SelectedPath = "C:\\ProgramData\\PRM System\\Backup\\";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(93, 340);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(0, 17);
            this.linkLabel2.TabIndex = 1365;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(93, 398);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(0, 17);
            this.linkLabel3.TabIndex = 1366;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(345, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 1367;
            this.label1.Text = "وقت النسخ التلقائي";
            // 
            // txtSpan
            // 
            this.txtSpan.Location = new System.Drawing.Point(262, 152);
            this.txtSpan.MaxLength = 2;
            this.txtSpan.Name = "txtSpan";
            this.txtSpan.Size = new System.Drawing.Size(57, 25);
            this.txtSpan.TabIndex = 1368;
            this.txtSpan.TextChanged += new System.EventHandler(this.txtSpan_TextChanged);
            this.txtSpan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSpan_KeyPress);
            // 
            // txtNoOfFiles
            // 
            this.txtNoOfFiles.Location = new System.Drawing.Point(512, 150);
            this.txtNoOfFiles.MaxLength = 2;
            this.txtNoOfFiles.Name = "txtNoOfFiles";
            this.txtNoOfFiles.Size = new System.Drawing.Size(57, 25);
            this.txtNoOfFiles.TabIndex = 1370;
            this.txtNoOfFiles.TextChanged += new System.EventHandler(this.txtNoOfFiles_TextChanged);
            this.txtNoOfFiles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSpan_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(325, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 17);
            this.label2.TabIndex = 1369;
            this.label2.Text = "..";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.LightGray;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 190);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(984, 10);
            this.label11.TabIndex = 1372;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label12.Location = new System.Drawing.Point(768, 261);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(204, 25);
            this.label12.TabIndex = 1373;
            this.label12.Text = "معلومات اخر  عملية نسخ";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // lblLastBackupInfo
            // 
            this.lblLastBackupInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastBackupInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLastBackupInfo.Location = new System.Drawing.Point(469, 322);
            this.lblLastBackupInfo.Name = "lblLastBackupInfo";
            this.lblLastBackupInfo.Size = new System.Drawing.Size(506, 45);
            this.lblLastBackupInfo.TabIndex = 1374;
            this.lblLastBackupInfo.Text = "اخر نسخه كانت  {0} في  {1} مخزنه في{2}.";
            this.lblLastBackupInfo.Click += new System.EventHandler(this.lblLastBackupInfo_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.SteelBlue;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MistyRose;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(513, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 24);
            this.button2.TabIndex = 1376;
            this.button2.TabStop = false;
            this.button2.Text = "استعادة قاعدة بيانات";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ComboBoxserverName
            // 
            this.ComboBoxserverName.FormattingEnabled = true;
            this.ComboBoxserverName.Location = new System.Drawing.Point(805, 65);
            this.ComboBoxserverName.Name = "ComboBoxserverName";
            this.ComboBoxserverName.Size = new System.Drawing.Size(121, 25);
            this.ComboBoxserverName.TabIndex = 1378;
            this.ComboBoxserverName.SelectedIndexChanged += new System.EventHandler(this.ComboBoxserverName_SelectedIndexChanged);
            // 
            // ComboBoxDatabaseName
            // 
            this.ComboBoxDatabaseName.FormattingEnabled = true;
            this.ComboBoxDatabaseName.Location = new System.Drawing.Point(609, 65);
            this.ComboBoxDatabaseName.Name = "ComboBoxDatabaseName";
            this.ComboBoxDatabaseName.Size = new System.Drawing.Size(121, 25);
            this.ComboBoxDatabaseName.TabIndex = 1379;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 25);
            this.button1.TabIndex = 1380;
            this.button1.Text = "بدا المزامنه";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(854, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 17);
            this.label8.TabIndex = 1381;
            this.label8.Text = "اسم السيرفر";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.SteelBlue;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MistyRose;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(414, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 24);
            this.button3.TabIndex = 1382;
            this.button3.TabStop = false;
            this.button3.Text = "ادارة القواعد";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.SteelBlue;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MistyRose;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(304, 1);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 24);
            this.button4.TabIndex = 1383;
            this.button4.TabStop = false;
            this.button4.Text = "تشفير الملفات";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.SteelBlue;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MistyRose;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(208, 1);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(104, 24);
            this.button5.TabIndex = 1384;
            this.button5.TabStop = false;
            this.button5.Text = "صيانه القواعد";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(32, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 1356;
            this.label4.Text = "مسار التخزين";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.Location = new System.Drawing.Point(575, 153);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 17);
            this.label10.TabIndex = 1391;
            this.label10.Text = "عدد الملفات";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DBBACKUP.Properties.Resources.info_30px;
            this.pictureBox8.Location = new System.Drawing.Point(732, 261);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(37, 37);
            this.pictureBox8.TabIndex = 1394;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::DBBACKUP.Properties.Resources.search_database_30px;
            this.pictureBox6.Location = new System.Drawing.Point(736, 56);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(33, 50);
            this.pictureBox6.TabIndex = 1390;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DBBACKUP.Properties.Resources.search_in_cloud_30px3;
            this.pictureBox5.Location = new System.Drawing.Point(932, 56);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(33, 50);
            this.pictureBox5.TabIndex = 1389;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DBBACKUP.Properties.Resources.imac_clock_30px;
            this.pictureBox4.Location = new System.Drawing.Point(219, 152);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(37, 37);
            this.pictureBox4.TabIndex = 1388;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DBBACKUP.Properties.Resources.folder_30px5;
            this.pictureBox3.Location = new System.Drawing.Point(35, 333);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 34);
            this.pictureBox3.TabIndex = 1387;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DBBACKUP.Properties.Resources.folder_30px;
            this.pictureBox2.Location = new System.Drawing.Point(467, 144);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(37, 37);
            this.pictureBox2.TabIndex = 1386;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DBBACKUP.Properties.Resources.database_30px;
            this.pictureBox1.Location = new System.Drawing.Point(33, 388);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 34);
            this.pictureBox1.TabIndex = 1385;
            this.pictureBox1.TabStop = false;
            // 
            // btnBackup
            // 
            this.btnBackup.BackColor = System.Drawing.Color.White;
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnBackup.Image = global::DBBACKUP.Properties.Resources.data_backup_30px2;
            this.btnBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackup.Location = new System.Drawing.Point(805, 391);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(167, 33);
            this.btnBackup.TabIndex = 1350;
            this.btnBackup.Text = "ابدا النسخ الاحتياطي يدوياٍ";
            this.btnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBackup.UseVisualStyleBackColor = false;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.SteelBlue;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MistyRose;
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(658, 1);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(111, 24);
            this.button6.TabIndex = 1395;
            this.button6.TabStop = false;
            this.button6.Text = "التحكم الصوتي";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.SteelBlue;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MistyRose;
            this.button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(768, -2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(111, 24);
            this.button7.TabIndex = 1396;
            this.button7.TabStop = false;
            this.button7.Text = "فحص الاداء";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // FrmDbBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 483);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ComboBoxDatabaseName);
            this.Controls.Add(this.ComboBoxserverName);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblLastBackupInfo);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtNoOfFiles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.DateTimePicker1);
            this.Controls.Add(this.LinkLabel1);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.Label21);
            this.Controls.Add(this.txtSpan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmDbBackup";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDbBackup";
            this.Load += new System.EventHandler(this.FrmDbBackup_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label21;
        internal System.Windows.Forms.Label Label22;
        

        
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.ProgressBar ProgressBarEx5;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button btnBackup;
        internal System.Windows.Forms.LinkLabel LinkLabel1;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Label label22;
        internal System.Windows.Forms.Label label20;
        internal System.Windows.Forms.Timer Timer1;
        internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
        private System.Windows.Forms.Label linkLabel2;
        private System.Windows.Forms.Label linkLabel3;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSpan;
        internal System.Windows.Forms.DateTimePicker DateTimePicker1;
        private System.Windows.Forms.TextBox txtNoOfFiles;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label lblLastBackupInfo;
        internal System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox ComboBoxserverName;
        private System.Windows.Forms.ComboBox ComboBoxDatabaseName;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Button button3;
        internal System.Windows.Forms.Button button4;
        internal System.Windows.Forms.Button button5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox8;
        internal System.Windows.Forms.Button button6;
        internal System.Windows.Forms.Button button7;
    }
}