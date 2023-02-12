using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xcp_host
{
    public partial class FormAddVar : Form
    {
        public string varType;
        public FormAddVar()
        {
            InitializeComponent();

            this.button_accept.DialogResult = DialogResult.OK;
        }

        private void button_accept_Click(object sender, EventArgs e)
        {
            this.varType = this.comboBox_varType.Text;
        }
    }
}
