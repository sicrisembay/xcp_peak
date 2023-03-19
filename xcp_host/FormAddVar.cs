using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xcp_host;

namespace xcp_host
{
    public partial class FormAddVar : Form
    {
        public xcp_host.VARTYPE varType;
        public string varName;
        public UInt32 varAddr;

        public FormAddVar()
        {
            InitializeComponent();

            this.button_accept.DialogResult = DialogResult.OK;

            this.comboBox_varType.Items.Clear();
            var varTypeName = Enum.GetNames(typeof(xcp_host.VARTYPE));
            for(int i = 0; i < varTypeName.Length; i++) {
                this.comboBox_varType.Items.Add(varTypeName[i]);
            }
            this.comboBox_varType.SelectedIndex = 3;
        }

        #region Helper

        private bool ValidateKeyPressForHex(char keypress)
        {
            char[] allowedChars = new char[] {
                '0', '1', '2', '3', '4', '5', '6', '7',
                '8', '9', 'A', 'B', 'C', 'D', 'E', 'F',
                'a', 'b', 'c', 'd', 'e', 'f', (char)0x08, (char)127
            };
            if (!allowedChars.Contains(keypress)) {
                return true;
            } else {
                return false;
            }
        }
        #endregion

        #region Button Event Handler

        private void button_accept_Click(object sender, EventArgs e)
        {
            this.varType = (xcp_host.VARTYPE)( this.comboBox_varType.SelectedIndex );
            this.varName = this.textBox_varName.Text;
            this.varAddr = UInt32.Parse(this.textBox_address.Text, System.Globalization.NumberStyles.HexNumber);
            this.Close();
        }

        #endregion

        #region TextBox Event Handler

        private void textBox_address_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = this.ValidateKeyPressForHex(e.KeyChar);
        }

        #endregion

    }
}
