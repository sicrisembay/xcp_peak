
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
            this.SuspendLayout();
            // 
            // cbb_channel
            // 
            this.cbb_channel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_channel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_channel.Location = new System.Drawing.Point(32, 42);
            this.cbb_channel.Name = "cbb_channel";
            this.cbb_channel.Size = new System.Drawing.Size(119, 21);
            this.cbb_channel.TabIndex = 37;
            this.cbb_channel.SelectedIndexChanged += new System.EventHandler(this.cbb_channel_SelectedIndexChanged);
            // 
            // cbb_baudrates
            // 
            this.cbb_baudrates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_baudrates.Location = new System.Drawing.Point(160, 42);
            this.cbb_baudrates.Name = "cbb_baudrates";
            this.cbb_baudrates.Size = new System.Drawing.Size(152, 21);
            this.cbb_baudrates.TabIndex = 38;
            this.cbb_baudrates.SelectedIndexChanged += new System.EventHandler(this.cbb_baudrates_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(29, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 42;
            this.label1.Text = "Channel:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(157, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 43;
            this.label2.Text = "Baudrate:";
            // 
            // btn_HwRefresh
            // 
            this.btn_HwRefresh.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_HwRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_HwRefresh.Location = new System.Drawing.Point(329, 31);
            this.btn_HwRefresh.Name = "btn_HwRefresh";
            this.btn_HwRefresh.Size = new System.Drawing.Size(65, 40);
            this.btn_HwRefresh.TabIndex = 46;
            this.btn_HwRefresh.Text = "Refresh";
            this.btn_HwRefresh.Click += new System.EventHandler(this.btn_HwRefresh_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(400, 33);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(102, 37);
            this.btn_Connect.TabIndex = 47;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // textBox_BroadcastID
            // 
            this.textBox_BroadcastID.Location = new System.Drawing.Point(119, 97);
            this.textBox_BroadcastID.Name = "textBox_BroadcastID";
            this.textBox_BroadcastID.Size = new System.Drawing.Size(75, 20);
            this.textBox_BroadcastID.TabIndex = 48;
            this.textBox_BroadcastID.Text = "300";
            // 
            // textBox_MasterID
            // 
            this.textBox_MasterID.Location = new System.Drawing.Point(119, 123);
            this.textBox_MasterID.Name = "textBox_MasterID";
            this.textBox_MasterID.Size = new System.Drawing.Size(75, 20);
            this.textBox_MasterID.TabIndex = 48;
            this.textBox_MasterID.Text = "555";
            // 
            // textBox_SlaveID
            // 
            this.textBox_SlaveID.Location = new System.Drawing.Point(119, 149);
            this.textBox_SlaveID.Name = "textBox_SlaveID";
            this.textBox_SlaveID.Size = new System.Drawing.Size(75, 20);
            this.textBox_SlaveID.TabIndex = 48;
            this.textBox_SlaveID.Text = "554";
            // 
            // ui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox_SlaveID);
            this.Controls.Add(this.textBox_MasterID);
            this.Controls.Add(this.textBox_BroadcastID);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.btn_HwRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbb_channel);
            this.Controls.Add(this.cbb_baudrates);
            this.Name = "ui";
            this.Text = "Test User Interface";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ui_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

