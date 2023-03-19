
namespace xcp_host
{
    partial class FormAddVar
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
            this.button_accept = new System.Windows.Forms.Button();
            this.comboBox_varType = new System.Windows.Forms.ComboBox();
            this.textBox_varName = new System.Windows.Forms.TextBox();
            this.textBox_address = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_accept
            // 
            this.button_accept.Location = new System.Drawing.Point(182, 66);
            this.button_accept.Name = "button_accept";
            this.button_accept.Size = new System.Drawing.Size(92, 37);
            this.button_accept.TabIndex = 0;
            this.button_accept.Text = "OK";
            this.button_accept.UseVisualStyleBackColor = true;
            this.button_accept.Click += new System.EventHandler(this.button_accept_Click);
            // 
            // comboBox_varType
            // 
            this.comboBox_varType.FormattingEnabled = true;
            this.comboBox_varType.Location = new System.Drawing.Point(23, 23);
            this.comboBox_varType.Name = "comboBox_varType";
            this.comboBox_varType.Size = new System.Drawing.Size(121, 21);
            this.comboBox_varType.TabIndex = 1;
            // 
            // textBox_varName
            // 
            this.textBox_varName.Location = new System.Drawing.Point(150, 24);
            this.textBox_varName.Name = "textBox_varName";
            this.textBox_varName.Size = new System.Drawing.Size(165, 20);
            this.textBox_varName.TabIndex = 2;
            this.textBox_varName.Text = "VarName";
            // 
            // textBox_address
            // 
            this.textBox_address.Location = new System.Drawing.Point(321, 24);
            this.textBox_address.Name = "textBox_address";
            this.textBox_address.Size = new System.Drawing.Size(108, 20);
            this.textBox_address.TabIndex = 3;
            this.textBox_address.Text = "00000000";
            this.textBox_address.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_address_KeyPress);
            // 
            // FormAddVar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 121);
            this.Controls.Add(this.textBox_address);
            this.Controls.Add(this.textBox_varName);
            this.Controls.Add(this.comboBox_varType);
            this.Controls.Add(this.button_accept);
            this.Name = "FormAddVar";
            this.Text = "FormAddVar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_accept;
        private System.Windows.Forms.ComboBox comboBox_varType;
        private System.Windows.Forms.TextBox textBox_varName;
        private System.Windows.Forms.TextBox textBox_address;
    }
}