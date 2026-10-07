namespace Modless
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        private void InitializeComponent()
        {
            btnRun = new System.Windows.Forms.Button();
            CreateWall = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            tabPage2 = new System.Windows.Forms.TabPage();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            panel2 = new System.Windows.Forms.Panel();
            CreateBeam = new System.Windows.Forms.Button();
            tabControl2 = new System.Windows.Forms.TabControl();
            tabPage3 = new System.Windows.Forms.TabPage();
            comboBox7 = new System.Windows.Forms.ComboBox();
            label13 = new System.Windows.Forms.Label();
            CreateDoor = new System.Windows.Forms.Button();
            label12 = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            comboBox6 = new System.Windows.Forms.ComboBox();
            label11 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            checkBox3 = new System.Windows.Forms.CheckBox();
            CreateRoom = new System.Windows.Forms.Button();
            checkBox2 = new System.Windows.Forms.CheckBox();
            checkBox1 = new System.Windows.Forms.CheckBox();
            Level_Top_Pick = new System.Windows.Forms.ComboBox();
            label10 = new System.Windows.Forms.Label();
            Level_Picker = new System.Windows.Forms.ComboBox();
            label9 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            comboBox4 = new System.Windows.Forms.ComboBox();
            textBox1 = new System.Windows.Forms.TextBox();
            CreateWallByCL = new System.Windows.Forms.Button();
            CreateCol = new System.Windows.Forms.Button();
            comboBox1 = new System.Windows.Forms.ComboBox();
            label6 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            comboBox2 = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            comboBox3 = new System.Windows.Forms.ComboBox();
            tabPage4 = new System.Windows.Forms.TabPage();
            tabPage5 = new System.Windows.Forms.TabPage();
            tabPage6 = new System.Windows.Forms.TabPage();
            CSV_INPUT = new System.Windows.Forms.Button();
            csv_output = new System.Windows.Forms.Button();
            label8 = new System.Windows.Forms.Label();
            comboBox5 = new System.Windows.Forms.ComboBox();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            label14 = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabControl2.SuspendLayout();
            tabPage3.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.BackColor = System.Drawing.Color.DarkSeaGreen;
            btnRun.Location = new System.Drawing.Point(6, 65);
            btnRun.Name = "btnRun";
            btnRun.Size = new System.Drawing.Size(121, 34);
            btnRun.TabIndex = 0;
            btnRun.Text = "슬라브 생성기";
            btnRun.UseVisualStyleBackColor = false;
            btnRun.UseWaitCursor = true;
            btnRun.Click += btnRun_Click;
            // 
            // CreateWall
            // 
            CreateWall.BackColor = System.Drawing.Color.DarkSeaGreen;
            CreateWall.Location = new System.Drawing.Point(516, 17);
            CreateWall.Name = "CreateWall";
            CreateWall.Size = new System.Drawing.Size(121, 34);
            CreateWall.TabIndex = 1;
            CreateWall.Text = "벽 생성기";
            CreateWall.UseVisualStyleBackColor = false;
            CreateWall.UseWaitCursor = true;
            CreateWall.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.DarkSeaGreen;
            panel1.Controls.Add(label14);
            panel1.Controls.Add(tabControl1);
            panel1.Controls.Add(label2);
            panel1.Dock = System.Windows.Forms.DockStyle.Left;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(87, 271);
            panel1.TabIndex = 6;
            panel1.UseWaitCursor = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new System.Drawing.Point(87, 40);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(289, 228);
            tabControl1.TabIndex = 7;
            tabControl1.UseWaitCursor = true;
            // 
            // tabPage1
            // 
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(281, 200);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.UseWaitCursor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(281, 200);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            tabPage2.UseWaitCursor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 12);
            label2.Name = "label2";
            label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            label2.Size = new System.Drawing.Size(66, 30);
            label2.TabIndex = 1;
            label2.Text = "COMPANY\r\nLOGO";
            label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            label2.UseWaitCursor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(4, 46);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(59, 15);
            label1.TabIndex = 6;
            label1.Text = "Company";
            label1.UseWaitCursor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(65, 43);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            pictureBox1.UseWaitCursor = true;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.DarkSeaGreen;
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(87, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(649, 10);
            panel2.TabIndex = 4;
            panel2.UseWaitCursor = true;
            // 
            // CreateBeam
            // 
            CreateBeam.BackColor = System.Drawing.Color.DarkSeaGreen;
            CreateBeam.Location = new System.Drawing.Point(133, 65);
            CreateBeam.Name = "CreateBeam";
            CreateBeam.Size = new System.Drawing.Size(121, 34);
            CreateBeam.TabIndex = 5;
            CreateBeam.Text = "보 생성기";
            CreateBeam.UseVisualStyleBackColor = false;
            CreateBeam.UseWaitCursor = true;
            CreateBeam.Click += CreateBeam_Click;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage3);
            tabControl2.Controls.Add(tabPage4);
            tabControl2.Controls.Add(tabPage5);
            tabControl2.Controls.Add(tabPage6);
            tabControl2.Location = new System.Drawing.Point(87, 12);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new System.Drawing.Size(657, 259);
            tabControl2.TabIndex = 7;
            tabControl2.UseWaitCursor = true;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = System.Drawing.Color.SeaGreen;
            tabPage3.Controls.Add(comboBox7);
            tabPage3.Controls.Add(label13);
            tabPage3.Controls.Add(CreateDoor);
            tabPage3.Controls.Add(label12);
            tabPage3.Controls.Add(textBox2);
            tabPage3.Controls.Add(comboBox6);
            tabPage3.Controls.Add(label11);
            tabPage3.Controls.Add(groupBox1);
            tabPage3.Controls.Add(Level_Top_Pick);
            tabPage3.Controls.Add(label10);
            tabPage3.Controls.Add(Level_Picker);
            tabPage3.Controls.Add(label9);
            tabPage3.Controls.Add(label7);
            tabPage3.Controls.Add(comboBox4);
            tabPage3.Controls.Add(textBox1);
            tabPage3.Controls.Add(btnRun);
            tabPage3.Controls.Add(CreateWall);
            tabPage3.Controls.Add(CreateWallByCL);
            tabPage3.Controls.Add(CreateBeam);
            tabPage3.Controls.Add(CreateCol);
            tabPage3.Controls.Add(comboBox1);
            tabPage3.Controls.Add(label6);
            tabPage3.Controls.Add(label3);
            tabPage3.Controls.Add(comboBox2);
            tabPage3.Controls.Add(label5);
            tabPage3.Controls.Add(label4);
            tabPage3.Controls.Add(comboBox3);
            tabPage3.Location = new System.Drawing.Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new System.Windows.Forms.Padding(3);
            tabPage3.Size = new System.Drawing.Size(649, 231);
            tabPage3.TabIndex = 0;
            tabPage3.Text = "Architecture";
            tabPage3.UseWaitCursor = true;
            // 
            // comboBox7
            // 
            comboBox7.FormattingEnabled = true;
            comboBox7.Location = new System.Drawing.Point(516, 133);
            comboBox7.Name = "comboBox7";
            comboBox7.Size = new System.Drawing.Size(121, 23);
            comboBox7.TabIndex = 31;
            comboBox7.UseWaitCursor = true;
            comboBox7.SelectedIndexChanged += comboBox7_SelectedIndexChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ForeColor = System.Drawing.Color.Transparent;
            label13.Location = new System.Drawing.Point(516, 116);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(47, 15);
            label13.TabIndex = 32;
            label13.Text = "문 타입";
            label13.UseWaitCursor = true;
            // 
            // CreateDoor
            // 
            CreateDoor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            CreateDoor.BackColor = System.Drawing.Color.DarkSeaGreen;
            CreateDoor.Location = new System.Drawing.Point(516, 171);
            CreateDoor.Name = "CreateDoor";
            CreateDoor.Size = new System.Drawing.Size(121, 34);
            CreateDoor.TabIndex = 30;
            CreateDoor.Text = "문 생성기";
            CreateDoor.UseVisualStyleBackColor = false;
            CreateDoor.UseWaitCursor = true;
            CreateDoor.Click += CreateDoor_Click;
            // 
            // label12
            // 
            label12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label12.AutoSize = true;
            label12.ForeColor = System.Drawing.Color.Transparent;
            label12.Location = new System.Drawing.Point(387, 165);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(87, 15);
            label12.TabIndex = 29;
            label12.Text = "천장 높이 지정";
            label12.UseWaitCursor = true;
            // 
            // textBox2
            // 
            textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            textBox2.Location = new System.Drawing.Point(387, 183);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(121, 23);
            textBox2.TabIndex = 28;
            textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            textBox2.UseWaitCursor = true;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // comboBox6
            // 
            comboBox6.FormattingEnabled = true;
            comboBox6.Location = new System.Drawing.Point(387, 134);
            comboBox6.Name = "comboBox6";
            comboBox6.Size = new System.Drawing.Size(121, 23);
            comboBox6.TabIndex = 26;
            comboBox6.UseWaitCursor = true;
            comboBox6.SelectedIndexChanged += comboBox6_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = System.Drawing.Color.Transparent;
            label11.Location = new System.Drawing.Point(387, 117);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(59, 15);
            label11.TabIndex = 27;
            label11.Text = "천장 타입";
            label11.UseWaitCursor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Controls.Add(CreateRoom);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            groupBox1.Location = new System.Drawing.Point(154, 124);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(205, 82);
            groupBox1.TabIndex = 25;
            groupBox1.TabStop = false;
            groupBox1.Text = "실내재료마감 생성기";
            groupBox1.UseWaitCursor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new System.Drawing.Point(147, 55);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new System.Drawing.Size(50, 19);
            checkBox3.TabIndex = 28;
            checkBox3.Text = "천장";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.UseWaitCursor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // CreateRoom
            // 
            CreateRoom.BackColor = System.Drawing.Color.DarkSeaGreen;
            CreateRoom.ForeColor = System.Drawing.SystemColors.ControlText;
            CreateRoom.Location = new System.Drawing.Point(11, 22);
            CreateRoom.Name = "CreateRoom";
            CreateRoom.Size = new System.Drawing.Size(121, 52);
            CreateRoom.TabIndex = 24;
            CreateRoom.Text = "룸마감 생성기";
            CreateRoom.UseVisualStyleBackColor = false;
            CreateRoom.UseWaitCursor = true;
            CreateRoom.Click += CreateRoom_Click;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new System.Drawing.Point(147, 35);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(38, 19);
            checkBox2.TabIndex = 27;
            checkBox2.Text = "벽";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.UseWaitCursor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(147, 16);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(50, 19);
            checkBox1.TabIndex = 26;
            checkBox1.Text = "바닥";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.UseWaitCursor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // Level_Top_Pick
            // 
            Level_Top_Pick.FormattingEnabled = true;
            Level_Top_Pick.Location = new System.Drawing.Point(6, 182);
            Level_Top_Pick.Name = "Level_Top_Pick";
            Level_Top_Pick.Size = new System.Drawing.Size(121, 23);
            Level_Top_Pick.TabIndex = 22;
            Level_Top_Pick.UseWaitCursor = true;
            Level_Top_Pick.SelectedIndexChanged += Level_Top_Pick_SelectedIndexChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = System.Drawing.Color.Transparent;
            label10.Location = new System.Drawing.Point(6, 165);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(111, 15);
            label10.TabIndex = 23;
            label10.Text = "상위 레벨 선택하기";
            label10.UseWaitCursor = true;
            // 
            // Level_Picker
            // 
            Level_Picker.FormattingEnabled = true;
            Level_Picker.Location = new System.Drawing.Point(6, 134);
            Level_Picker.Name = "Level_Picker";
            Level_Picker.Size = new System.Drawing.Size(121, 23);
            Level_Picker.TabIndex = 20;
            Level_Picker.UseWaitCursor = true;
            Level_Picker.SelectedIndexChanged += Level_Picker_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = System.Drawing.Color.Transparent;
            label9.Location = new System.Drawing.Point(6, 116);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(111, 15);
            label9.TabIndex = 21;
            label9.Text = "하위 레벨 선택하기";
            label9.UseWaitCursor = true;
            // 
            // label7
            // 
            label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label7.AutoSize = true;
            label7.ForeColor = System.Drawing.Color.Transparent;
            label7.Location = new System.Drawing.Point(387, 52);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(75, 15);
            label7.TabIndex = 19;
            label7.Text = "벽 높이 지정";
            label7.UseWaitCursor = true;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new System.Drawing.Point(387, 24);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new System.Drawing.Size(121, 23);
            comboBox4.TabIndex = 14;
            comboBox4.UseWaitCursor = true;
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            textBox1.Location = new System.Drawing.Point(387, 72);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(121, 23);
            textBox1.TabIndex = 18;
            textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            textBox1.UseWaitCursor = true;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // CreateWallByCL
            // 
            CreateWallByCL.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            CreateWallByCL.BackColor = System.Drawing.Color.DarkSeaGreen;
            CreateWallByCL.Location = new System.Drawing.Point(516, 65);
            CreateWallByCL.Name = "CreateWallByCL";
            CreateWallByCL.Size = new System.Drawing.Size(121, 34);
            CreateWallByCL.TabIndex = 17;
            CreateWallByCL.Text = "마감벽 생성기";
            CreateWallByCL.UseVisualStyleBackColor = false;
            CreateWallByCL.UseWaitCursor = true;
            CreateWallByCL.Click += CreateWallByCL_Click;
            // 
            // CreateCol
            // 
            CreateCol.BackColor = System.Drawing.Color.DarkSeaGreen;
            CreateCol.Location = new System.Drawing.Point(260, 65);
            CreateCol.Name = "CreateCol";
            CreateCol.Size = new System.Drawing.Size(121, 34);
            CreateCol.TabIndex = 16;
            CreateCol.Text = "기둥 생성기";
            CreateCol.UseVisualStyleBackColor = false;
            CreateCol.UseWaitCursor = true;
            CreateCol.Click += CreateCol_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(5, 24);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(121, 23);
            comboBox1.TabIndex = 8;
            comboBox1.UseWaitCursor = true;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = System.Drawing.Color.Transparent;
            label6.Location = new System.Drawing.Point(387, 6);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(47, 15);
            label6.TabIndex = 15;
            label6.Text = "벽 타입";
            label6.UseWaitCursor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = System.Drawing.Color.Transparent;
            label3.Location = new System.Drawing.Point(5, 6);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(71, 15);
            label3.TabIndex = 9;
            label3.Text = "슬라브 타입";
            label3.UseWaitCursor = true;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new System.Drawing.Point(133, 24);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new System.Drawing.Size(121, 23);
            comboBox2.TabIndex = 10;
            comboBox2.UseWaitCursor = true;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = System.Drawing.Color.Transparent;
            label5.Location = new System.Drawing.Point(260, 6);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(59, 15);
            label5.TabIndex = 13;
            label5.Text = "기둥 타입";
            label5.UseWaitCursor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = System.Drawing.Color.Transparent;
            label4.Location = new System.Drawing.Point(133, 6);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(47, 15);
            label4.TabIndex = 11;
            label4.Text = "보 타입";
            label4.UseWaitCursor = true;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new System.Drawing.Point(260, 24);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new System.Drawing.Size(121, 23);
            comboBox3.TabIndex = 12;
            comboBox3.UseWaitCursor = true;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // tabPage4
            // 
            tabPage4.BackColor = System.Drawing.Color.SeaGreen;
            tabPage4.Location = new System.Drawing.Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new System.Windows.Forms.Padding(3);
            tabPage4.Size = new System.Drawing.Size(649, 231);
            tabPage4.TabIndex = 1;
            tabPage4.Text = "Structure";
            tabPage4.UseWaitCursor = true;
            // 
            // tabPage5
            // 
            tabPage5.BackColor = System.Drawing.Color.SeaGreen;
            tabPage5.Location = new System.Drawing.Point(4, 24);
            tabPage5.Name = "tabPage5";
            tabPage5.Size = new System.Drawing.Size(649, 231);
            tabPage5.TabIndex = 2;
            tabPage5.Text = "MEP";
            tabPage5.UseWaitCursor = true;
            // 
            // tabPage6
            // 
            tabPage6.BackColor = System.Drawing.Color.SeaGreen;
            tabPage6.Controls.Add(CSV_INPUT);
            tabPage6.Controls.Add(csv_output);
            tabPage6.Controls.Add(label8);
            tabPage6.Controls.Add(comboBox5);
            tabPage6.Controls.Add(dataGridView1);
            tabPage6.Location = new System.Drawing.Point(4, 24);
            tabPage6.Name = "tabPage6";
            tabPage6.Size = new System.Drawing.Size(649, 231);
            tabPage6.TabIndex = 3;
            tabPage6.Text = "Data";
            tabPage6.UseWaitCursor = true;
            // 
            // CSV_INPUT
            // 
            CSV_INPUT.Location = new System.Drawing.Point(111, 197);
            CSV_INPUT.Name = "CSV_INPUT";
            CSV_INPUT.Size = new System.Drawing.Size(96, 23);
            CSV_INPUT.TabIndex = 4;
            CSV_INPUT.Text = "csv로 불러오기";
            CSV_INPUT.UseVisualStyleBackColor = true;
            CSV_INPUT.UseWaitCursor = true;
            CSV_INPUT.Click += CSV_INPUT_Click;
            // 
            // csv_output
            // 
            csv_output.Location = new System.Drawing.Point(7, 197);
            csv_output.Name = "csv_output";
            csv_output.Size = new System.Drawing.Size(96, 23);
            csv_output.TabIndex = 3;
            csv_output.Text = "csv로 내보내기";
            csv_output.UseVisualStyleBackColor = true;
            csv_output.UseWaitCursor = true;
            csv_output.Click += csv_output_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            label8.Location = new System.Drawing.Point(442, 201);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(60, 15);
            label8.TabIndex = 2;
            label8.Text = "Data 선택";
            label8.UseWaitCursor = true;
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Items.AddRange(new object[] { "Wall", "Floor", "Column", "Beam" });
            comboBox5.Location = new System.Drawing.Point(510, 196);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new System.Drawing.Size(121, 23);
            comboBox5.TabIndex = 1;
            comboBox5.UseWaitCursor = true;
            comboBox5.SelectedIndexChanged += comboBox5_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = System.Drawing.Color.SeaGreen;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            dataGridView1.GridColor = System.Drawing.Color.DarkSeaGreen;
            dataGridView1.Location = new System.Drawing.Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new System.Drawing.Size(649, 188);
            dataGridView1.TabIndex = 0;
            dataGridView1.UseWaitCursor = true;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(12, 53);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(66, 30);
            label14.TabIndex = 8;
            label14.Text = "COMPANY\r\nNAME";
            label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            label14.UseWaitCursor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Honeydew;
            ClientSize = new System.Drawing.Size(736, 271);
            Controls.Add(tabControl2);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Modless";
            UseWaitCursor = true;
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabControl2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button CreateWall;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button CreateBeam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Button CreateCol;
        private System.Windows.Forms.Button CreateWallByCL;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.Button csv_output;
        private System.Windows.Forms.Button CSV_INPUT;
        private System.Windows.Forms.ComboBox Level_Picker;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox Level_Top_Pick;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button CreateRoom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button CreateDoor;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}
