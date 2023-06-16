
namespace xcp_host
{
    partial class ui
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
            if (disposing && ( components != null )) {
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
            this.cbb_channel = new System.Windows.Forms.ComboBox();
            this.cbb_baudrates = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_HwRefresh = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.textBox_BroadcastID = new System.Windows.Forms.TextBox();
            this.textBox_MasterID = new System.Windows.Forms.TextBox();
            this.textBox_SlaveID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox_Connection = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_comm_transport_ver = new System.Windows.Forms.Label();
            this.label_comm_proto_ver = new System.Windows.Forms.Label();
            this.label_comm_max_dto = new System.Windows.Forms.Label();
            this.label_comm_max_cto = new System.Windows.Forms.Label();
            this.label_comm_mode = new System.Windows.Forms.Label();
            this.label_comm_resource = new System.Windows.Forms.Label();
            this.tabControl_main = new System.Windows.Forms.TabControl();
            this.tabPage_upload_download = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtb_download_value = new System.Windows.Forms.RichTextBox();
            this.button_write = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_download_addr = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtb_upload_value = new System.Windows.Forms.RichTextBox();
            this.button_read = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_upload_addr = new System.Windows.Forms.TextBox();
            this.textBox_upload_count = new System.Windows.Forms.TextBox();
            this.tabPage_variable_test = new System.Windows.Forms.TabPage();
            this.button_writeAllVar = new System.Windows.Forms.Button();
            this.button_loadVarWriteList = new System.Windows.Forms.Button();
            this.button_loadVarList = new System.Windows.Forms.Button();
            this.button_refreshAllVar = new System.Windows.Forms.Button();
            this.button_add_var = new System.Windows.Forms.Button();
            this.dataGridView_varWrite = new System.Windows.Forms.DataGridView();
            this.dataGridView_varRead = new System.Windows.Forms.DataGridView();
            this.tabPage_daq_test = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox_varType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_odtEntryAddr = new System.Windows.Forms.TextBox();
            this.button_removeEntry = new System.Windows.Forms.Button();
            this.textBox_odtEntryName = new System.Windows.Forms.TextBox();
            this.button_addEntry = new System.Windows.Forms.Button();
            this.button_startDaq = new System.Windows.Forms.Button();
            this.button_daqClear = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button_configDaq = new System.Windows.Forms.Button();
            this.comboBox_eventChannel = new System.Windows.Forms.ComboBox();
            this.button_AddOdt = new System.Windows.Forms.Button();
            this.button_addDaq = new System.Windows.Forms.Button();
            this.button_saveDaq = new System.Windows.Forms.Button();
            this.treeView_Daq = new System.Windows.Forms.TreeView();
            this.tabPage_daqPlot_test = new System.Windows.Forms.TabPage();
            this.button_plotClear = new System.Windows.Forms.Button();
            this.checkBox_plotEnable = new System.Windows.Forms.CheckBox();
            this.button_addPlot = new System.Windows.Forms.Button();
            this.comboBox_odtEntry = new System.Windows.Forms.ComboBox();
            this.formsPlot_daq = new ScottPlot.FormsPlot();
            this.timer_dtoPoll = new System.Windows.Forms.Timer(this.components);
            this.timer_displayRefresh = new System.Windows.Forms.Timer(this.components);
            this.groupBox_Connection.SuspendLayout();
            this.tabControl_main.SuspendLayout();
            this.tabPage_upload_download.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage_variable_test.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_varWrite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_varRead)).BeginInit();
            this.tabPage_daq_test.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage_daqPlot_test.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbb_channel
            // 
            this.cbb_channel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_channel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_channel.Location = new System.Drawing.Point(22, 43);
            this.cbb_channel.Name = "cbb_channel";
            this.cbb_channel.Size = new System.Drawing.Size(119, 21);
            this.cbb_channel.TabIndex = 1;
            this.cbb_channel.SelectedIndexChanged += new System.EventHandler(this.cbb_channel_SelectedIndexChanged);
            // 
            // cbb_baudrates
            // 
            this.cbb_baudrates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_baudrates.Location = new System.Drawing.Point(150, 43);
            this.cbb_baudrates.Name = "cbb_baudrates";
            this.cbb_baudrates.Size = new System.Drawing.Size(152, 21);
            this.cbb_baudrates.TabIndex = 2;
            this.cbb_baudrates.SelectedIndexChanged += new System.EventHandler(this.cbb_baudrates_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 42;
            this.label1.Text = "Channel:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(147, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 43;
            this.label2.Text = "Baudrate:";
            // 
            // btn_HwRefresh
            // 
            this.btn_HwRefresh.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_HwRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_HwRefresh.Location = new System.Drawing.Point(310, 43);
            this.btn_HwRefresh.Name = "btn_HwRefresh";
            this.btn_HwRefresh.Size = new System.Drawing.Size(65, 102);
            this.btn_HwRefresh.TabIndex = 46;
            this.btn_HwRefresh.Text = "Refresh";
            this.btn_HwRefresh.Click += new System.EventHandler(this.btn_HwRefresh_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(381, 43);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(96, 102);
            this.btn_Connect.TabIndex = 6;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // textBox_BroadcastID
            // 
            this.textBox_BroadcastID.Location = new System.Drawing.Point(77, 80);
            this.textBox_BroadcastID.Name = "textBox_BroadcastID";
            this.textBox_BroadcastID.Size = new System.Drawing.Size(42, 20);
            this.textBox_BroadcastID.TabIndex = 3;
            this.textBox_BroadcastID.Text = "300";
            this.textBox_BroadcastID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_BroadcastID_KeyPress);
            // 
            // textBox_MasterID
            // 
            this.textBox_MasterID.Location = new System.Drawing.Point(172, 80);
            this.textBox_MasterID.Name = "textBox_MasterID";
            this.textBox_MasterID.Size = new System.Drawing.Size(42, 20);
            this.textBox_MasterID.TabIndex = 4;
            this.textBox_MasterID.Text = "555";
            this.textBox_MasterID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_MasterID_KeyPress);
            // 
            // textBox_SlaveID
            // 
            this.textBox_SlaveID.Location = new System.Drawing.Point(258, 80);
            this.textBox_SlaveID.Name = "textBox_SlaveID";
            this.textBox_SlaveID.Size = new System.Drawing.Size(42, 20);
            this.textBox_SlaveID.TabIndex = 5;
            this.textBox_SlaveID.Text = "554";
            this.textBox_SlaveID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_SlaveID_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "Broadcast:";
            // 
            // groupBox_Connection
            // 
            this.groupBox_Connection.Controls.Add(this.cbb_baudrates);
            this.groupBox_Connection.Controls.Add(this.textBox_SlaveID);
            this.groupBox_Connection.Controls.Add(this.label5);
            this.groupBox_Connection.Controls.Add(this.label4);
            this.groupBox_Connection.Controls.Add(this.textBox_MasterID);
            this.groupBox_Connection.Controls.Add(this.label_comm_transport_ver);
            this.groupBox_Connection.Controls.Add(this.label_comm_proto_ver);
            this.groupBox_Connection.Controls.Add(this.label_comm_max_dto);
            this.groupBox_Connection.Controls.Add(this.label_comm_max_cto);
            this.groupBox_Connection.Controls.Add(this.label_comm_mode);
            this.groupBox_Connection.Controls.Add(this.label_comm_resource);
            this.groupBox_Connection.Controls.Add(this.label3);
            this.groupBox_Connection.Controls.Add(this.cbb_channel);
            this.groupBox_Connection.Controls.Add(this.textBox_BroadcastID);
            this.groupBox_Connection.Controls.Add(this.label2);
            this.groupBox_Connection.Controls.Add(this.label1);
            this.groupBox_Connection.Controls.Add(this.btn_HwRefresh);
            this.groupBox_Connection.Controls.Add(this.btn_Connect);
            this.groupBox_Connection.Location = new System.Drawing.Point(12, 25);
            this.groupBox_Connection.Name = "groupBox_Connection";
            this.groupBox_Connection.Size = new System.Drawing.Size(496, 159);
            this.groupBox_Connection.TabIndex = 50;
            this.groupBox_Connection.TabStop = false;
            this.groupBox_Connection.Text = "Connection";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(220, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "Slave:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(129, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "Master:";
            // 
            // label_comm_transport_ver
            // 
            this.label_comm_transport_ver.AutoSize = true;
            this.label_comm_transport_ver.Location = new System.Drawing.Point(108, 132);
            this.label_comm_transport_ver.Name = "label_comm_transport_ver";
            this.label_comm_transport_ver.Size = new System.Drawing.Size(83, 13);
            this.label_comm_transport_ver.TabIndex = 49;
            this.label_comm_transport_ver.Text = "Transport Ver: --";
            // 
            // label_comm_proto_ver
            // 
            this.label_comm_proto_ver.AutoSize = true;
            this.label_comm_proto_ver.Location = new System.Drawing.Point(19, 132);
            this.label_comm_proto_ver.Name = "label_comm_proto_ver";
            this.label_comm_proto_ver.Size = new System.Drawing.Size(77, 13);
            this.label_comm_proto_ver.TabIndex = 49;
            this.label_comm_proto_ver.Text = "Protocol Ver: --";
            // 
            // label_comm_max_dto
            // 
            this.label_comm_max_dto.AutoSize = true;
            this.label_comm_max_dto.Location = new System.Drawing.Point(236, 106);
            this.label_comm_max_dto.Name = "label_comm_max_dto";
            this.label_comm_max_dto.Size = new System.Drawing.Size(71, 13);
            this.label_comm_max_dto.TabIndex = 49;
            this.label_comm_max_dto.Text = "MAX_DTO: --";
            // 
            // label_comm_max_cto
            // 
            this.label_comm_max_cto.AutoSize = true;
            this.label_comm_max_cto.Location = new System.Drawing.Point(163, 106);
            this.label_comm_max_cto.Name = "label_comm_max_cto";
            this.label_comm_max_cto.Size = new System.Drawing.Size(70, 13);
            this.label_comm_max_cto.TabIndex = 49;
            this.label_comm_max_cto.Text = "MAX_CTO: --";
            // 
            // label_comm_mode
            // 
            this.label_comm_mode.AutoSize = true;
            this.label_comm_mode.Location = new System.Drawing.Point(103, 106);
            this.label_comm_mode.Name = "label_comm_mode";
            this.label_comm_mode.Size = new System.Drawing.Size(46, 13);
            this.label_comm_mode.TabIndex = 49;
            this.label_comm_mode.Text = "Mode: --";
            // 
            // label_comm_resource
            // 
            this.label_comm_resource.AutoSize = true;
            this.label_comm_resource.Location = new System.Drawing.Point(19, 106);
            this.label_comm_resource.Name = "label_comm_resource";
            this.label_comm_resource.Size = new System.Drawing.Size(65, 13);
            this.label_comm_resource.TabIndex = 49;
            this.label_comm_resource.Text = "Resource: --";
            // 
            // tabControl_main
            // 
            this.tabControl_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_main.Controls.Add(this.tabPage_upload_download);
            this.tabControl_main.Controls.Add(this.tabPage_variable_test);
            this.tabControl_main.Controls.Add(this.tabPage_daq_test);
            this.tabControl_main.Controls.Add(this.tabPage_daqPlot_test);
            this.tabControl_main.Location = new System.Drawing.Point(12, 190);
            this.tabControl_main.Name = "tabControl_main";
            this.tabControl_main.SelectedIndex = 0;
            this.tabControl_main.Size = new System.Drawing.Size(678, 516);
            this.tabControl_main.TabIndex = 51;
            // 
            // tabPage_upload_download
            // 
            this.tabPage_upload_download.Controls.Add(this.groupBox2);
            this.tabPage_upload_download.Controls.Add(this.groupBox1);
            this.tabPage_upload_download.Location = new System.Drawing.Point(4, 22);
            this.tabPage_upload_download.Name = "tabPage_upload_download";
            this.tabPage_upload_download.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_upload_download.Size = new System.Drawing.Size(670, 490);
            this.tabPage_upload_download.TabIndex = 0;
            this.tabPage_upload_download.Text = "Upload/Download";
            this.tabPage_upload_download.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rtb_download_value);
            this.groupBox2.Controls.Add(this.button_write);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBox_download_addr);
            this.groupBox2.Location = new System.Drawing.Point(18, 242);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(632, 204);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Download";
            // 
            // rtb_download_value
            // 
            this.rtb_download_value.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_download_value.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_download_value.Location = new System.Drawing.Point(12, 66);
            this.rtb_download_value.Name = "rtb_download_value";
            this.rtb_download_value.Size = new System.Drawing.Size(600, 120);
            this.rtb_download_value.TabIndex = 51;
            this.rtb_download_value.Text = "0000 3F40";
            this.rtb_download_value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtb_download_value_KeyPress);
            // 
            // button_write
            // 
            this.button_write.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_write.Location = new System.Drawing.Point(536, 17);
            this.button_write.Name = "button_write";
            this.button_write.Size = new System.Drawing.Size(76, 38);
            this.button_write.TabIndex = 50;
            this.button_write.Text = "Write";
            this.button_write.UseVisualStyleBackColor = true;
            this.button_write.Click += new System.EventHandler(this.button_write_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 49;
            this.label9.Text = "Address:";
            // 
            // textBox_download_addr
            // 
            this.textBox_download_addr.Location = new System.Drawing.Point(58, 27);
            this.textBox_download_addr.Name = "textBox_download_addr";
            this.textBox_download_addr.Size = new System.Drawing.Size(64, 20);
            this.textBox_download_addr.TabIndex = 48;
            this.textBox_download_addr.Text = "0000C298";
            this.textBox_download_addr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_download_addr_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rtb_upload_value);
            this.groupBox1.Controls.Add(this.button_read);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox_upload_addr);
            this.groupBox1.Controls.Add(this.textBox_upload_count);
            this.groupBox1.Location = new System.Drawing.Point(18, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 204);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upload";
            // 
            // rtb_upload_value
            // 
            this.rtb_upload_value.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_upload_value.Enabled = false;
            this.rtb_upload_value.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_upload_value.Location = new System.Drawing.Point(12, 66);
            this.rtb_upload_value.Name = "rtb_upload_value";
            this.rtb_upload_value.Size = new System.Drawing.Size(600, 120);
            this.rtb_upload_value.TabIndex = 51;
            this.rtb_upload_value.Text = "";
            // 
            // button_read
            // 
            this.button_read.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_read.Location = new System.Drawing.Point(536, 17);
            this.button_read.Name = "button_read";
            this.button_read.Size = new System.Drawing.Size(76, 38);
            this.button_read.TabIndex = 50;
            this.button_read.Text = "Read";
            this.button_read.UseVisualStyleBackColor = true;
            this.button_read.Click += new System.EventHandler(this.button_read_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(133, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Count:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 49;
            this.label6.Text = "Address:";
            // 
            // textBox_upload_addr
            // 
            this.textBox_upload_addr.Location = new System.Drawing.Point(58, 27);
            this.textBox_upload_addr.Name = "textBox_upload_addr";
            this.textBox_upload_addr.Size = new System.Drawing.Size(64, 20);
            this.textBox_upload_addr.TabIndex = 48;
            this.textBox_upload_addr.Text = "0000C292";
            this.textBox_upload_addr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_upload_addr_KeyPress);
            // 
            // textBox_upload_count
            // 
            this.textBox_upload_count.Location = new System.Drawing.Point(171, 27);
            this.textBox_upload_count.Name = "textBox_upload_count";
            this.textBox_upload_count.Size = new System.Drawing.Size(53, 20);
            this.textBox_upload_count.TabIndex = 48;
            this.textBox_upload_count.Text = "14";
            // 
            // tabPage_variable_test
            // 
            this.tabPage_variable_test.Controls.Add(this.button_writeAllVar);
            this.tabPage_variable_test.Controls.Add(this.button_loadVarWriteList);
            this.tabPage_variable_test.Controls.Add(this.button_loadVarList);
            this.tabPage_variable_test.Controls.Add(this.button_refreshAllVar);
            this.tabPage_variable_test.Controls.Add(this.button_add_var);
            this.tabPage_variable_test.Controls.Add(this.dataGridView_varWrite);
            this.tabPage_variable_test.Controls.Add(this.dataGridView_varRead);
            this.tabPage_variable_test.Location = new System.Drawing.Point(4, 22);
            this.tabPage_variable_test.Name = "tabPage_variable_test";
            this.tabPage_variable_test.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_variable_test.Size = new System.Drawing.Size(670, 490);
            this.tabPage_variable_test.TabIndex = 1;
            this.tabPage_variable_test.Text = "Variable Test";
            this.tabPage_variable_test.UseVisualStyleBackColor = true;
            // 
            // button_writeAllVar
            // 
            this.button_writeAllVar.Location = new System.Drawing.Point(484, 250);
            this.button_writeAllVar.Name = "button_writeAllVar";
            this.button_writeAllVar.Size = new System.Drawing.Size(75, 23);
            this.button_writeAllVar.TabIndex = 5;
            this.button_writeAllVar.Text = "Write";
            this.button_writeAllVar.UseVisualStyleBackColor = true;
            this.button_writeAllVar.Click += new System.EventHandler(this.button_writeAllVar_Click);
            // 
            // button_loadVarWriteList
            // 
            this.button_loadVarWriteList.Location = new System.Drawing.Point(317, 250);
            this.button_loadVarWriteList.Name = "button_loadVarWriteList";
            this.button_loadVarWriteList.Size = new System.Drawing.Size(75, 23);
            this.button_loadVarWriteList.TabIndex = 4;
            this.button_loadVarWriteList.Text = "Load List";
            this.button_loadVarWriteList.UseVisualStyleBackColor = true;
            this.button_loadVarWriteList.Click += new System.EventHandler(this.button_loadVarWriteList_Click);
            // 
            // button_loadVarList
            // 
            this.button_loadVarList.Location = new System.Drawing.Point(317, 19);
            this.button_loadVarList.Name = "button_loadVarList";
            this.button_loadVarList.Size = new System.Drawing.Size(75, 23);
            this.button_loadVarList.TabIndex = 3;
            this.button_loadVarList.Text = "Load List";
            this.button_loadVarList.UseVisualStyleBackColor = true;
            this.button_loadVarList.Click += new System.EventHandler(this.button_loadVarList_Click);
            // 
            // button_refreshAllVar
            // 
            this.button_refreshAllVar.Location = new System.Drawing.Point(484, 19);
            this.button_refreshAllVar.Name = "button_refreshAllVar";
            this.button_refreshAllVar.Size = new System.Drawing.Size(75, 23);
            this.button_refreshAllVar.TabIndex = 2;
            this.button_refreshAllVar.Text = "Read";
            this.button_refreshAllVar.UseVisualStyleBackColor = true;
            this.button_refreshAllVar.Click += new System.EventHandler(this.button_refreshAllVar_Click);
            // 
            // button_add_var
            // 
            this.button_add_var.Location = new System.Drawing.Point(398, 19);
            this.button_add_var.Name = "button_add_var";
            this.button_add_var.Size = new System.Drawing.Size(75, 23);
            this.button_add_var.TabIndex = 1;
            this.button_add_var.Text = "Add Entry";
            this.button_add_var.UseVisualStyleBackColor = true;
            this.button_add_var.Click += new System.EventHandler(this.button_add_var_Click);
            // 
            // dataGridView_varWrite
            // 
            this.dataGridView_varWrite.AllowUserToDeleteRows = false;
            this.dataGridView_varWrite.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_varWrite.Location = new System.Drawing.Point(13, 295);
            this.dataGridView_varWrite.Name = "dataGridView_varWrite";
            this.dataGridView_varWrite.Size = new System.Drawing.Size(546, 175);
            this.dataGridView_varWrite.TabIndex = 0;
            // 
            // dataGridView_varRead
            // 
            this.dataGridView_varRead.AllowUserToDeleteRows = false;
            this.dataGridView_varRead.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_varRead.Location = new System.Drawing.Point(13, 53);
            this.dataGridView_varRead.Name = "dataGridView_varRead";
            this.dataGridView_varRead.Size = new System.Drawing.Size(546, 175);
            this.dataGridView_varRead.TabIndex = 0;
            // 
            // tabPage_daq_test
            // 
            this.tabPage_daq_test.Controls.Add(this.groupBox4);
            this.tabPage_daq_test.Controls.Add(this.button_saveDaq);
            this.tabPage_daq_test.Controls.Add(this.treeView_Daq);
            this.tabPage_daq_test.Location = new System.Drawing.Point(4, 22);
            this.tabPage_daq_test.Name = "tabPage_daq_test";
            this.tabPage_daq_test.Size = new System.Drawing.Size(670, 490);
            this.tabPage_daq_test.TabIndex = 2;
            this.tabPage_daq_test.Text = "DAQ Test";
            this.tabPage_daq_test.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.button_startDaq);
            this.groupBox4.Controls.Add(this.button_daqClear);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.button_configDaq);
            this.groupBox4.Controls.Add(this.comboBox_eventChannel);
            this.groupBox4.Controls.Add(this.button_AddOdt);
            this.groupBox4.Controls.Add(this.button_addDaq);
            this.groupBox4.Location = new System.Drawing.Point(333, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(319, 286);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox_varType);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.textBox_odtEntryAddr);
            this.groupBox3.Controls.Add(this.button_removeEntry);
            this.groupBox3.Controls.Add(this.textBox_odtEntryName);
            this.groupBox3.Controls.Add(this.button_addEntry);
            this.groupBox3.Location = new System.Drawing.Point(17, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(283, 115);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ODT Entry";
            // 
            // comboBox_varType
            // 
            this.comboBox_varType.FormattingEnabled = true;
            this.comboBox_varType.Location = new System.Drawing.Point(71, 71);
            this.comboBox_varType.Name = "comboBox_varType";
            this.comboBox_varType.Size = new System.Drawing.Size(77, 21);
            this.comboBox_varType.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(34, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Type:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Address:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Name:";
            // 
            // textBox_odtEntryAddr
            // 
            this.textBox_odtEntryAddr.Location = new System.Drawing.Point(71, 47);
            this.textBox_odtEntryAddr.Name = "textBox_odtEntryAddr";
            this.textBox_odtEntryAddr.Size = new System.Drawing.Size(77, 20);
            this.textBox_odtEntryAddr.TabIndex = 6;
            this.textBox_odtEntryAddr.Text = "00000000";
            // 
            // button_removeEntry
            // 
            this.button_removeEntry.Location = new System.Drawing.Point(166, 60);
            this.button_removeEntry.Name = "button_removeEntry";
            this.button_removeEntry.Size = new System.Drawing.Size(96, 35);
            this.button_removeEntry.TabIndex = 8;
            this.button_removeEntry.Text = "Remove Entry";
            this.button_removeEntry.UseVisualStyleBackColor = true;
            this.button_removeEntry.Click += new System.EventHandler(this.button_removeEntry_Click);
            // 
            // textBox_odtEntryName
            // 
            this.textBox_odtEntryName.Location = new System.Drawing.Point(71, 23);
            this.textBox_odtEntryName.Name = "textBox_odtEntryName";
            this.textBox_odtEntryName.Size = new System.Drawing.Size(77, 20);
            this.textBox_odtEntryName.TabIndex = 5;
            this.textBox_odtEntryName.Text = "VarName";
            // 
            // button_addEntry
            // 
            this.button_addEntry.Location = new System.Drawing.Point(166, 19);
            this.button_addEntry.Name = "button_addEntry";
            this.button_addEntry.Size = new System.Drawing.Size(96, 35);
            this.button_addEntry.TabIndex = 8;
            this.button_addEntry.Text = "Add Entry";
            this.button_addEntry.UseVisualStyleBackColor = true;
            this.button_addEntry.Click += new System.EventHandler(this.button_addEntry_Click);
            // 
            // button_startDaq
            // 
            this.button_startDaq.Location = new System.Drawing.Point(145, 232);
            this.button_startDaq.Name = "button_startDaq";
            this.button_startDaq.Size = new System.Drawing.Size(96, 38);
            this.button_startDaq.TabIndex = 10;
            this.button_startDaq.Text = "Start";
            this.button_startDaq.UseVisualStyleBackColor = true;
            this.button_startDaq.Click += new System.EventHandler(this.button_startDaq_Click);
            // 
            // button_daqClear
            // 
            this.button_daqClear.Location = new System.Drawing.Point(197, 55);
            this.button_daqClear.Name = "button_daqClear";
            this.button_daqClear.Size = new System.Drawing.Size(71, 35);
            this.button_daqClear.TabIndex = 4;
            this.button_daqClear.Text = "Clear All";
            this.button_daqClear.UseVisualStyleBackColor = true;
            this.button_daqClear.Click += new System.EventHandler(this.button_daqClear_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Event:";
            // 
            // button_configDaq
            // 
            this.button_configDaq.Location = new System.Drawing.Point(43, 232);
            this.button_configDaq.Name = "button_configDaq";
            this.button_configDaq.Size = new System.Drawing.Size(96, 38);
            this.button_configDaq.TabIndex = 6;
            this.button_configDaq.Text = "Configure Slave";
            this.button_configDaq.UseVisualStyleBackColor = true;
            this.button_configDaq.Click += new System.EventHandler(this.button_configDaq_Click);
            // 
            // comboBox_eventChannel
            // 
            this.comboBox_eventChannel.FormattingEnabled = true;
            this.comboBox_eventChannel.Location = new System.Drawing.Point(66, 28);
            this.comboBox_eventChannel.Name = "comboBox_eventChannel";
            this.comboBox_eventChannel.Size = new System.Drawing.Size(121, 21);
            this.comboBox_eventChannel.TabIndex = 1;
            // 
            // button_AddOdt
            // 
            this.button_AddOdt.Location = new System.Drawing.Point(120, 55);
            this.button_AddOdt.Name = "button_AddOdt";
            this.button_AddOdt.Size = new System.Drawing.Size(71, 35);
            this.button_AddOdt.TabIndex = 3;
            this.button_AddOdt.Text = "Add ODT";
            this.button_AddOdt.UseVisualStyleBackColor = true;
            this.button_AddOdt.Click += new System.EventHandler(this.button_AddOdt_Click);
            // 
            // button_addDaq
            // 
            this.button_addDaq.Location = new System.Drawing.Point(43, 55);
            this.button_addDaq.Name = "button_addDaq";
            this.button_addDaq.Size = new System.Drawing.Size(71, 35);
            this.button_addDaq.TabIndex = 2;
            this.button_addDaq.Text = "Add DAQ";
            this.button_addDaq.UseVisualStyleBackColor = true;
            this.button_addDaq.Click += new System.EventHandler(this.button_addDaq_Click);
            // 
            // button_saveDaq
            // 
            this.button_saveDaq.Location = new System.Drawing.Point(501, 375);
            this.button_saveDaq.Name = "button_saveDaq";
            this.button_saveDaq.Size = new System.Drawing.Size(89, 38);
            this.button_saveDaq.TabIndex = 7;
            this.button_saveDaq.Text = "Save";
            this.button_saveDaq.UseVisualStyleBackColor = true;
            this.button_saveDaq.Click += new System.EventHandler(this.button_saveDaq_Click);
            // 
            // treeView_Daq
            // 
            this.treeView_Daq.Location = new System.Drawing.Point(26, 21);
            this.treeView_Daq.Name = "treeView_Daq";
            this.treeView_Daq.Size = new System.Drawing.Size(289, 437);
            this.treeView_Daq.TabIndex = 0;
            // 
            // tabPage_daqPlot_test
            // 
            this.tabPage_daqPlot_test.Controls.Add(this.button_plotClear);
            this.tabPage_daqPlot_test.Controls.Add(this.checkBox_plotEnable);
            this.tabPage_daqPlot_test.Controls.Add(this.button_addPlot);
            this.tabPage_daqPlot_test.Controls.Add(this.comboBox_odtEntry);
            this.tabPage_daqPlot_test.Controls.Add(this.formsPlot_daq);
            this.tabPage_daqPlot_test.Location = new System.Drawing.Point(4, 22);
            this.tabPage_daqPlot_test.Name = "tabPage_daqPlot_test";
            this.tabPage_daqPlot_test.Size = new System.Drawing.Size(670, 490);
            this.tabPage_daqPlot_test.TabIndex = 3;
            this.tabPage_daqPlot_test.Text = "Plot Test";
            this.tabPage_daqPlot_test.UseVisualStyleBackColor = true;
            // 
            // button_plotClear
            // 
            this.button_plotClear.Location = new System.Drawing.Point(235, 407);
            this.button_plotClear.Name = "button_plotClear";
            this.button_plotClear.Size = new System.Drawing.Size(97, 32);
            this.button_plotClear.TabIndex = 4;
            this.button_plotClear.Text = "Clear";
            this.button_plotClear.UseVisualStyleBackColor = true;
            this.button_plotClear.Click += new System.EventHandler(this.button_plotClear_Click);
            // 
            // checkBox_plotEnable
            // 
            this.checkBox_plotEnable.AutoSize = true;
            this.checkBox_plotEnable.Location = new System.Drawing.Point(86, 411);
            this.checkBox_plotEnable.Name = "checkBox_plotEnable";
            this.checkBox_plotEnable.Size = new System.Drawing.Size(61, 17);
            this.checkBox_plotEnable.TabIndex = 3;
            this.checkBox_plotEnable.Text = "Update";
            this.checkBox_plotEnable.UseVisualStyleBackColor = true;
            // 
            // button_addPlot
            // 
            this.button_addPlot.Location = new System.Drawing.Point(235, 347);
            this.button_addPlot.Name = "button_addPlot";
            this.button_addPlot.Size = new System.Drawing.Size(97, 32);
            this.button_addPlot.TabIndex = 2;
            this.button_addPlot.Text = "Add";
            this.button_addPlot.UseVisualStyleBackColor = true;
            this.button_addPlot.Click += new System.EventHandler(this.button_addPlot_Click);
            // 
            // comboBox_odtEntry
            // 
            this.comboBox_odtEntry.FormattingEnabled = true;
            this.comboBox_odtEntry.Location = new System.Drawing.Point(66, 354);
            this.comboBox_odtEntry.Name = "comboBox_odtEntry";
            this.comboBox_odtEntry.Size = new System.Drawing.Size(144, 21);
            this.comboBox_odtEntry.TabIndex = 1;
            // 
            // formsPlot_daq
            // 
            this.formsPlot_daq.Location = new System.Drawing.Point(18, 28);
            this.formsPlot_daq.Name = "formsPlot_daq";
            this.formsPlot_daq.Size = new System.Drawing.Size(637, 300);
            this.formsPlot_daq.TabIndex = 0;
            // 
            // timer_dtoPoll
            // 
            this.timer_dtoPoll.Enabled = true;
            this.timer_dtoPoll.Tick += new System.EventHandler(this.timer_poll_Tick);
            // 
            // timer_displayRefresh
            // 
            this.timer_displayRefresh.Enabled = true;
            this.timer_displayRefresh.Interval = 1000;
            this.timer_displayRefresh.Tick += new System.EventHandler(this.timer_displayRefresh_Tick);
            // 
            // ui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 718);
            this.Controls.Add(this.tabControl_main);
            this.Controls.Add(this.groupBox_Connection);
            this.Name = "ui";
            this.Text = "Test User Interface";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ui_FormClosing);
            this.groupBox_Connection.ResumeLayout(false);
            this.groupBox_Connection.PerformLayout();
            this.tabControl_main.ResumeLayout(false);
            this.tabPage_upload_download.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage_variable_test.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_varWrite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_varRead)).EndInit();
            this.tabPage_daq_test.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage_daqPlot_test.ResumeLayout(false);
            this.tabPage_daqPlot_test.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbb_channel;
        private System.Windows.Forms.ComboBox cbb_baudrates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_HwRefresh;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TextBox textBox_BroadcastID;
        private System.Windows.Forms.TextBox textBox_MasterID;
        private System.Windows.Forms.TextBox textBox_SlaveID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox_Connection;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_comm_transport_ver;
        private System.Windows.Forms.Label label_comm_proto_ver;
        private System.Windows.Forms.Label label_comm_max_dto;
        private System.Windows.Forms.Label label_comm_max_cto;
        private System.Windows.Forms.Label label_comm_mode;
        private System.Windows.Forms.Label label_comm_resource;
        private System.Windows.Forms.TabControl tabControl_main;
        private System.Windows.Forms.TabPage tabPage_upload_download;
        private System.Windows.Forms.TabPage tabPage_variable_test;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_upload_addr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_upload_count;
        private System.Windows.Forms.Button button_read;
        private System.Windows.Forms.RichTextBox rtb_upload_value;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtb_download_value;
        private System.Windows.Forms.Button button_write;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_download_addr;
        private System.Windows.Forms.DataGridView dataGridView_varRead;
        private System.Windows.Forms.Button button_add_var;
        private System.Windows.Forms.Button button_refreshAllVar;
        private System.Windows.Forms.Button button_loadVarList;
        private System.Windows.Forms.DataGridView dataGridView_varWrite;
        private System.Windows.Forms.Button button_loadVarWriteList;
        private System.Windows.Forms.Button button_writeAllVar;
        private System.Windows.Forms.TabPage tabPage_daq_test;
        private System.Windows.Forms.TreeView treeView_Daq;
        private System.Windows.Forms.Button button_addDaq;
        private System.Windows.Forms.Button button_AddOdt;
        private System.Windows.Forms.Button button_addEntry;
        private System.Windows.Forms.TextBox textBox_odtEntryAddr;
        private System.Windows.Forms.TextBox textBox_odtEntryName;
        private System.Windows.Forms.ComboBox comboBox_varType;
        private System.Windows.Forms.Button button_configDaq;
        private System.Windows.Forms.Button button_saveDaq;
        private System.Windows.Forms.Button button_removeEntry;
        private System.Windows.Forms.ComboBox comboBox_eventChannel;
        private System.Windows.Forms.Button button_startDaq;
        private System.Windows.Forms.Timer timer_dtoPoll;
        private System.Windows.Forms.Timer timer_displayRefresh;
        private System.Windows.Forms.Button button_daqClear;
        private System.Windows.Forms.TabPage tabPage_daqPlot_test;
        private ScottPlot.FormsPlot formsPlot_daq;
        private System.Windows.Forms.ComboBox comboBox_odtEntry;
        private System.Windows.Forms.Button button_addPlot;
        private System.Windows.Forms.CheckBox checkBox_plotEnable;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_plotClear;
    }
}

