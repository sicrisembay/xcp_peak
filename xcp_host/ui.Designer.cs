
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox_Connection.SuspendLayout();
            this.tabControl_main.SuspendLayout();
            this.tabPage_upload_download.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbb_channel
            // 
            this.cbb_channel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_channel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_channel.Location = new System.Drawing.Point(22, 43);
            this.cbb_channel.Name = "cbb_channel";
            this.cbb_channel.Size = new System.Drawing.Size(119, 21);
            this.cbb_channel.TabIndex = 37;
            this.cbb_channel.SelectedIndexChanged += new System.EventHandler(this.cbb_channel_SelectedIndexChanged);
            // 
            // cbb_baudrates
            // 
            this.cbb_baudrates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_baudrates.Location = new System.Drawing.Point(150, 43);
            this.cbb_baudrates.Name = "cbb_baudrates";
            this.cbb_baudrates.Size = new System.Drawing.Size(152, 21);
            this.cbb_baudrates.TabIndex = 38;
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
            this.btn_Connect.TabIndex = 47;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // textBox_BroadcastID
            // 
            this.textBox_BroadcastID.Location = new System.Drawing.Point(77, 80);
            this.textBox_BroadcastID.Name = "textBox_BroadcastID";
            this.textBox_BroadcastID.Size = new System.Drawing.Size(42, 20);
            this.textBox_BroadcastID.TabIndex = 48;
            this.textBox_BroadcastID.Text = "300";
            this.textBox_BroadcastID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_BroadcastID_KeyPress);
            // 
            // textBox_MasterID
            // 
            this.textBox_MasterID.Location = new System.Drawing.Point(172, 80);
            this.textBox_MasterID.Name = "textBox_MasterID";
            this.textBox_MasterID.Size = new System.Drawing.Size(42, 20);
            this.textBox_MasterID.TabIndex = 48;
            this.textBox_MasterID.Text = "555";
            this.textBox_MasterID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_MasterID_KeyPress);
            // 
            // textBox_SlaveID
            // 
            this.textBox_SlaveID.Location = new System.Drawing.Point(258, 80);
            this.textBox_SlaveID.Name = "textBox_SlaveID";
            this.textBox_SlaveID.Size = new System.Drawing.Size(42, 20);
            this.textBox_SlaveID.TabIndex = 48;
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
            this.tabControl_main.Controls.Add(this.tabPage2);
            this.tabControl_main.Enabled = false;
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
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(670, 490);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
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
        private System.Windows.Forms.TabPage tabPage2;
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
    }
}

