using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using Peak.Can.Xcp;
using Peak.Can.Basic;

namespace xcp_host
{
    using TXCPChannel = System.UInt16;
    using TXCPHandle = System.UInt32;
    using TPCANHandle = System.UInt16;

    public partial class ui : Form
    {
        private TXCPHandle m_XcpSession;
        private TPCANHandle[] m_HandlesArray;
        private TPCANHandle m_PcanHandle;
        private TPCANBaudrate m_BaudRate;
        private ApplXcp m_xcp;

        private OdtEntryPlotPoint[] plotList;

        #region Method
        public ui()
        {
            InitializeComponent();

            this.m_xcp = new ApplXcp();

            this.plotList = null;

            this.m_HandlesArray = new TPCANHandle[] 
            {
                PCANBasic.PCAN_USBBUS1,
                PCANBasic.PCAN_USBBUS2,
                PCANBasic.PCAN_USBBUS3,
                PCANBasic.PCAN_USBBUS4,
                PCANBasic.PCAN_USBBUS5,
                PCANBasic.PCAN_USBBUS6,
                PCANBasic.PCAN_USBBUS7,
                PCANBasic.PCAN_USBBUS8,
            };

            this.SetupDataGridView();

            this.btn_HwRefresh_Click(this, new EventArgs());

            this.comboBox_varType.Items.Clear();
            var varTypeName = Enum.GetNames(typeof(VARTYPE));
            for(int i = 0; i < varTypeName.Length; i++) {
                this.comboBox_varType.Items.Add(varTypeName[i]);
            }
            this.comboBox_varType.SelectedIndex = 0;
        }

        #region Helper
        private void EnableUiElement(bool enable)
        {
            textBox_BroadcastID.Enabled = enable;
            textBox_MasterID.Enabled = enable;
            textBox_SlaveID.Enabled = enable;
            cbb_channel.Enabled = enable;
            cbb_baudrates.Enabled = enable;
            btn_HwRefresh.Enabled = enable;

            tabControl_main.Enabled = !enable;
        }

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


        private bool SetValue(UInt32 address, Byte value)
        {
            UInt16[] buffer = new UInt16[1];
            buffer[0] = value;
            return ( this.m_xcp.Write(address, buffer) == TXCPResult.XCP_ERR_OK );
        }

        private bool SetValue(UInt32 address, SByte value)
        {
            UInt16[] buffer = new UInt16[1];
            buffer[0] = Convert.ToUInt16(value);
            return ( this.m_xcp.Write(address, buffer) == TXCPResult.XCP_ERR_OK );
        }

        private bool SetValue(UInt32 address, UInt16 value)
        {
            UInt16[] buffer = new UInt16[1];
            buffer[0] = value;
            return ( this.m_xcp.Write(address, buffer) == TXCPResult.XCP_ERR_OK );
        }

        private bool SetValue(UInt32 address, Int16 value)
        {
            UInt16[] buffer = new UInt16[1];
            buffer[0] = Convert.ToUInt16(value);
            return ( this.m_xcp.Write(address, buffer) == TXCPResult.XCP_ERR_OK );
        }

        private bool SetValue(UInt32 address, UInt32 value)
        {
            UInt16[] buffer = new UInt16[2];
            buffer[0] = Convert.ToUInt16(value & 0x0000FFFF);
            buffer[1] = Convert.ToUInt16(( value >> 16 ) & 0x0000FFFF);
            return ( this.m_xcp.Write(address, buffer) == TXCPResult.XCP_ERR_OK );
        }

        private bool SetValue(UInt32 address, Int32 value)
        {
            UInt16[] buffer = new UInt16[2];
            buffer[0] = Convert.ToUInt16(value & 0x0000FFFF);
            buffer[1] = Convert.ToUInt16(( value >> 16 ) & 0x0000FFFF);
            return ( this.m_xcp.Write(address, buffer) == TXCPResult.XCP_ERR_OK );
        }

        private bool SetValue(UInt32 address, float value)
        {
            UInt16[] buffer = new UInt16[2];
            Byte[] bitConvert = BitConverter.GetBytes(value);
            buffer[0] = (UInt16)(bitConvert[0] + (bitConvert[1] << 8));
            buffer[1] = (UInt16)( bitConvert[2] + ( bitConvert[3] << 8 ) );
            return ( this.m_xcp.Write(address, buffer) == TXCPResult.XCP_ERR_OK );
        }

        private Byte GetByteValue(UInt32 address)
        {
            UInt16[] buffer;
            this.m_xcp.Read(address, 1, out buffer);
            return ( (Byte)( buffer[0] & 0x00FF ) );
        }

        private SByte GetSByteValue(UInt32 address)
        {
            UInt16[] buffer;
            this.m_xcp.Read(address, 1, out buffer);
            return ( (SByte)( buffer[0] & 0x00FF ) );
        }


        private UInt16 GetWordValue(UInt32 address)
        {
            UInt16[] buffer;
            this.m_xcp.Read(address, 1, out buffer);
            return ( buffer[0] );
        }

        private Int16 GetSWordValue(UInt32 address)
        {
            UInt16[] buffer;
            byte[] bitConvertBuffer = new byte[2];
            this.m_xcp.Read(address, 1, out buffer);
            bitConvertBuffer[0] = (byte)( buffer[0] & 0x00FF );
            bitConvertBuffer[1] = (byte)( ( buffer[0] >> 8 ) & 0x00FF );
            return BitConverter.ToInt16(bitConvertBuffer, 0);
        }

        private UInt32 GetDwordValue(UInt32 address)
        {
            UInt16[] buffer;
            byte[] bitConvertBuffer = new byte[4];
            this.m_xcp.Read(address, 2, out buffer);
            bitConvertBuffer[0] = (byte)( buffer[0] & 0x00FF );
            bitConvertBuffer[1] = (byte)( ( buffer[0] >> 8 ) & 0x00FF );
            bitConvertBuffer[2] = (byte)( buffer[1] & 0x00FF );
            bitConvertBuffer[3] = (byte)( ( buffer[1] >> 8 ) & 0x00FF );
            return BitConverter.ToUInt32(bitConvertBuffer, 0);
        }

        private Int32 GetSDwordValue(UInt32 address)
        {
            UInt16[] buffer;
            byte[] bitConvertBuffer = new byte[4];
            this.m_xcp.Read(address, 2, out buffer);
            bitConvertBuffer[0] = (byte)( buffer[0] & 0x00FF );
            bitConvertBuffer[1] = (byte)( ( buffer[0] >> 8 ) & 0x00FF );
            bitConvertBuffer[2] = (byte)( buffer[1] & 0x00FF );
            bitConvertBuffer[3] = (byte)( ( buffer[1] >> 8 ) & 0x00FF );
            return BitConverter.ToInt32(bitConvertBuffer, 0);
        }

        private float GetFloatValue(UInt32 address)
        {
            UInt16[] buffer;
            byte[] bitConvertBuffer = new byte[4];
            this.m_xcp.Read(address, 2, out buffer);
            bitConvertBuffer[0] = (byte)( buffer[0] & 0x00FF );
            bitConvertBuffer[1] = (byte)( ( buffer[0] >> 8 ) & 0x00FF );
            bitConvertBuffer[2] = (byte)( buffer[1] & 0x00FF );
            bitConvertBuffer[3] = (byte)( ( buffer[1] >> 8 ) & 0x00FF );
            return BitConverter.ToSingle(bitConvertBuffer, 0);
        }

        private string GetFormattedValue(VARTYPE selectedType, UInt32 address)
        {
            UInt16[] buffer;
            string valueStr = "";
            switch (selectedType) {
                case VARTYPE.INT8: {
                    valueStr = GetSByteValue(address).ToString();
                    break;
                }
                case VARTYPE.UINT8: {
                    valueStr = GetByteValue(address).ToString();
                    break;
                }
                case VARTYPE.INT16: {
                    valueStr = GetSWordValue(address).ToString();
                    break;
                }
                case VARTYPE.UINT16: {
                    valueStr = GetWordValue(address).ToString();
                    break;
                }
                case VARTYPE.INT32: {
                    Int32 tmpVal = GetSDwordValue(address);
                    valueStr = tmpVal.ToString();
                    break;
                }
                case VARTYPE.UINT32: {
                    UInt32 tmpVal = GetDwordValue(address);
                    valueStr = tmpVal.ToString();
                    break;
                }
                case VARTYPE.FLOAT: {
                    float tmpVal = GetFloatValue(address);
                    valueStr = tmpVal.ToString();
                    break;
                }
                default: {
                    break;
                }
            }
            return valueStr;
        }

        private Int32 VarTypeIdx(string entry)
        {
            Int32 retval = -1;

            var EnumNames = Enum.GetNames(typeof(VARTYPE));
            for(int i = 0; i < EnumNames.Length; i++) {
                if(entry == EnumNames[i]) {
                    retval = i;
                    break;
                }
            }
            return retval;
        }
        #endregion

        #region Button Event Handlers
        private void btn_HwRefresh_Click(object sender, EventArgs e)
        {
            UInt32 iBuffer;
            TPCANStatus stsResult;

            // Clears the Channel combioBox and fill it again with 
            // the PCAN-Basic handles for no-Plug&Play hardware and
            // the detected Plug&Play hardware
            cbb_channel.Items.Clear();
            cbb_baudrates.Items.Clear();
            try {
                for (int i = 0; i < m_HandlesArray.Length; i++) {
                    // Includes all no-Plug&Play Handles
                    if (( m_HandlesArray[i] >= PCANBasic.PCAN_USBBUS1 ) &&
                        ( m_HandlesArray[i] <= PCANBasic.PCAN_USBBUS8 )) {
                        stsResult = PCANBasic.GetValue(m_HandlesArray[i], TPCANParameter.PCAN_CHANNEL_CONDITION, out iBuffer, sizeof(UInt32));
                        if (( stsResult == TPCANStatus.PCAN_ERROR_OK ) && ( iBuffer == PCANBasic.PCAN_CHANNEL_AVAILABLE )) {
                            cbb_channel.Items.Add(string.Format("PCAN-USB{0}", m_HandlesArray[i] & 0x0F));
                        }
                    }
                }
                cbb_channel.SelectedIndex = cbb_channel.Items.Count - 1;
            } catch (DllNotFoundException) {
                MessageBox.Show("Unable to find the library: PCANBasic.dll !", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
            cbb_baudrates.Items.Clear();
            string[] baudName = Enum.GetNames(typeof(TPCANBaudrate));
            for (int i = 0; i < baudName.Length; i++) {
                cbb_baudrates.Items.Add(baudName[i]);
            }
            cbb_baudrates.SelectedIndex = 0;
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try {
                if (btn_Connect.Text == "Connect") {
                    TXCPResult result;
                    StringBuilder strText = new StringBuilder(256);

                    UInt32 broadcastID = UInt32.Parse(this.textBox_BroadcastID.Text, System.Globalization.NumberStyles.HexNumber);
                    UInt32 masterID = UInt32.Parse(this.textBox_MasterID.Text, System.Globalization.NumberStyles.HexNumber);
                    UInt32 slaveID = UInt32.Parse(this.textBox_SlaveID.Text, System.Globalization.NumberStyles.HexNumber);
                    this.m_xcp.SetID(broadcastID, masterID, slaveID);
                    result = this.m_xcp.Connect(this.m_PcanHandle, this.m_BaudRate);
                    if(result == TXCPResult.XCP_ERR_OK) {
                        this.label_comm_resource.Text = "Resource: 0x" + this.m_xcp.resource.ToString("X2");
                        this.label_comm_mode.Text = "Mode: 0x" + this.m_xcp.mode.ToString("X2");
                        this.label_comm_max_cto.Text = "MAX_CTO: " + this.m_xcp.max_cto.ToString();
                        this.label_comm_max_dto.Text = "MAX_DTO: " + this.m_xcp.max_dto.ToString();
                        this.label_comm_proto_ver.Text = "Protocol Ver: " + this.m_xcp.protocol_version.ToString();
                        this.label_comm_transport_ver.Text = "Transport Ver: " + this.m_xcp.transport_version.ToString();
                        btn_Connect.Text = "Disconnect";
                        if(this.m_xcp.maxEventChannel > 0) {
                            comboBox_eventChannel.Items.Clear();
                            for(int i = 0; i < this.m_xcp.eventInfo.Length; i++) {
                                comboBox_eventChannel.Items.Add(this.m_xcp.eventInfo[i].name);
                            }
                            comboBox_eventChannel.SelectedIndex = 0;
                        }
                        this.EnableUiElement(false);
                    } else {
                        XCPApi.GetErrorText(result, strText);
                        MessageBox.Show("Error: " + strText.ToString());
                    }
                } else {
                    this.m_xcp.Disconnect();
                    this.label_comm_resource.Text = "Resource: --";
                    this.label_comm_mode.Text = "Mode: --";
                    this.label_comm_max_cto.Text = "MAX_CTO: --";
                    this.label_comm_max_dto.Text = "MAX_DTO: --";
                    this.label_comm_proto_ver.Text = "Protocol Ver: --";
                    this.label_comm_transport_ver.Text = "Transport Ver: --";
                    btn_Connect.Text = "Connect";
                    this.EnableUiElement(true);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_read_Click(object sender, EventArgs e)
        {
            try {
                UInt16[] buffer;
                UInt32 nElements = Convert.ToUInt32(textBox_upload_count.Text);
                UInt32 Addr = UInt32.Parse(this.textBox_upload_addr.Text, System.Globalization.NumberStyles.HexNumber);
                if (nElements <= 0) {
                    return;
                } else {
                    this.m_xcp.Read(Addr, nElements, out buffer);
                    rtb_upload_value.Text = String.Empty;
                    // Display
                    int count = 0;
                    while (count < nElements) {
                        rtb_upload_value.Text += buffer[count].ToString("X4") + " ";
                        count++;
                        if (( count % 16 ) == 0) {
                            rtb_upload_value.Text += Environment.NewLine;
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_write_Click(object sender, EventArgs e)
        {
            string[] stringArray = rtb_download_value.Text.Split(' ');
            UInt32 Addr = UInt32.Parse(this.textBox_download_addr.Text, System.Globalization.NumberStyles.HexNumber);
            Int32 nElements = stringArray.Length;
            UInt16[] buffer = new UInt16[nElements];

            for(int i = 0; i < nElements; i++) {
                UInt32 data = UInt32.Parse(stringArray[i], System.Globalization.NumberStyles.HexNumber);
                buffer[i] = (UInt16)( data & 0x0000FFFF );
            }
            this.m_xcp.Write(Addr, buffer);
        }

        private void button_add_var_Click(object sender, EventArgs e)
        {
            FormAddVar addVarEntryForm = new FormAddVar();
            if (addVarEntryForm.ShowDialog() == DialogResult.OK) {
                DataGridViewRow newRow = new DataGridViewRow();
                DataGridViewTextBoxCell varType = new DataGridViewTextBoxCell();
                var selectedType = addVarEntryForm.varType;
                varType.Value = selectedType.ToString();
                newRow.Cells.Add(varType);

                DataGridViewTextBoxCell varName = new DataGridViewTextBoxCell();
                varName.Value = addVarEntryForm.varName;
                newRow.Cells.Add(varName);

                DataGridViewTextBoxCell varAddr = new DataGridViewTextBoxCell();
                var addr = addVarEntryForm.varAddr;
                varAddr.Value = addr.ToString("X8");
                newRow.Cells.Add(varAddr);

                DataGridViewTextBoxCell varValue = new DataGridViewTextBoxCell();
                varValue.Value = this.GetFormattedValue(selectedType, addVarEntryForm.varAddr);
                newRow.Cells.Add(varValue);

                this.dataGridView_varRead.Rows.Add(newRow);
            }

        }

        private void button_refreshAllVar_Click(object sender, EventArgs e)
        {
            var rowCount = this.dataGridView_varRead.Rows.Count - 1;
            UInt32 address = 0;
            VARTYPE selectedType = VARTYPE.UINT16;
            bool validType = false;
            var EnumTypeNames = Enum.GetNames(typeof(VARTYPE));

            if (rowCount <= 0) {
                return;
            }

            for (int i = 0; i < rowCount; i++) {
                validType = false;
                string strType = this.dataGridView_varRead.Rows[i].Cells[0].Value.ToString();
                for (int j = 0; j < EnumTypeNames.Length; j++) {
                    if (strType == EnumTypeNames[j]) {
                        validType = true;
                        selectedType = (VARTYPE)j;
                        break;
                    }
                }
                if (validType) {
                    string strAddr = this.dataGridView_varRead.Rows[i].Cells[2].Value.ToString();
                    address = UInt32.Parse(strAddr, System.Globalization.NumberStyles.HexNumber);

                    string strValue = this.GetFormattedValue(selectedType, address);
                    this.dataGridView_varRead.Rows[i].Cells[3].Value = strValue;
                }
            }
        }

        private void button_writeAllVar_Click(object sender, EventArgs e)
        {
            var rowCount = this.dataGridView_varWrite.Rows.Count - 1;
            UInt32 address = 0;
            VARTYPE selectedType = VARTYPE.UINT16;
            var EnumTypeNames = Enum.GetNames(typeof(VARTYPE));

            if (rowCount <= 0) {
                return;
            }

            for (int i = 0; i < rowCount; i++) {
                string strType = this.dataGridView_varWrite.Rows[i].Cells[0].Value.ToString();
                var typeIdx = VarTypeIdx(strType);
                if(typeIdx >= 0) {
                    selectedType = (VARTYPE)typeIdx;
                    string strAddr = this.dataGridView_varWrite.Rows[i].Cells[2].Value.ToString();
                    address = UInt32.Parse(strAddr, System.Globalization.NumberStyles.HexNumber);

                    switch(selectedType) {
                        case VARTYPE.INT8: {
                            SByte value = Convert.ToSByte(dataGridView_varWrite.Rows[i].Cells[3].Value.ToString());
                            SetValue(address, value);
                            break;
                        }
                        case VARTYPE.UINT8: {
                            Byte value = Convert.ToByte(dataGridView_varWrite.Rows[i].Cells[3].Value.ToString());
                            SetValue(address, value);
                            break;
                        }
                        case VARTYPE.INT16: {
                            Int16 value = Convert.ToInt16(dataGridView_varWrite.Rows[i].Cells[3].Value.ToString());
                            SetValue(address, value);
                            break;
                        }
                        case VARTYPE.UINT16: {
                            UInt16 value = Convert.ToUInt16(dataGridView_varWrite.Rows[i].Cells[3].Value.ToString());
                            SetValue(address, value);
                            break;
                        }
                        case VARTYPE.INT32: {
                            Int32 value = Convert.ToInt32(dataGridView_varWrite.Rows[i].Cells[3].Value.ToString());
                            SetValue(address, value);
                            break;
                        }
                        case VARTYPE.UINT32: {
                            UInt32 value = Convert.ToUInt32(dataGridView_varWrite.Rows[i].Cells[3].Value.ToString());
                            SetValue(address, value);
                            break;
                        }
                        case VARTYPE.FLOAT: {
                            float value = Convert.ToSingle(dataGridView_varWrite.Rows[i].Cells[3].Value.ToString());
                            SetValue(address, value);
                            break;
                        }
                        default: {
                            break;
                        }
                    }
                }

            }
        }

        private void button_loadVarList_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV File (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string line;
                string[] values;
                this.dataGridView_varRead.Rows.Clear();
                var filereader = new StreamReader(openFileDialog.FileName);
                while (!filereader.EndOfStream) {
                    line = filereader.ReadLine();
                    values = line.Split(',');
                    if (values.Length != 4) {
                        continue;
                    }

                    DataGridViewRow newRow = new DataGridViewRow();
                    DataGridViewTextBoxCell varType = new DataGridViewTextBoxCell();
                    if (this.VarTypeIdx(values[0]) == -1) {
                        break;
                    }
                    varType.Value = ( (VARTYPE)this.VarTypeIdx(values[0]) ).ToString();
                    newRow.Cells.Add(varType);

                    DataGridViewTextBoxCell varName = new DataGridViewTextBoxCell();
                    varName.Value = values[1];
                    newRow.Cells.Add(varName);

                    DataGridViewTextBoxCell varAddr = new DataGridViewTextBoxCell();
                    var addr = UInt32.Parse(values[2], System.Globalization.NumberStyles.HexNumber);
                    varAddr.Value = addr.ToString("X8");
                    newRow.Cells.Add(varAddr);

                    DataGridViewTextBoxCell varValue = new DataGridViewTextBoxCell();
                    varValue.Value = this.GetFormattedValue((VARTYPE)this.VarTypeIdx(values[0]), addr);
                    newRow.Cells.Add(varValue);

                    this.dataGridView_varRead.Rows.Add(newRow);

                }
            }
        }

        private void button_loadVarWriteList_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV File (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string line;
                string[] values;
                this.dataGridView_varWrite.Rows.Clear();
                var filereader = new StreamReader(openFileDialog.FileName);
                while (!filereader.EndOfStream) {
                    line = filereader.ReadLine();
                    values = line.Split(',');
                    if (values.Length < 3) {
                        continue;
                    }

                    DataGridViewRow newRow = new DataGridViewRow();
                    DataGridViewTextBoxCell varType = new DataGridViewTextBoxCell();
                    if (this.VarTypeIdx(values[0]) == -1) {
                        break;
                    }
                    var typeIdx = this.VarTypeIdx(values[0]);
                    varType.Value = ( (VARTYPE)typeIdx ).ToString();
                    newRow.Cells.Add(varType);

                    DataGridViewTextBoxCell varName = new DataGridViewTextBoxCell();
                    varName.Value = values[1];
                    newRow.Cells.Add(varName);

                    DataGridViewTextBoxCell varAddr = new DataGridViewTextBoxCell();
                    var addr = UInt32.Parse(values[2], System.Globalization.NumberStyles.HexNumber);
                    varAddr.Value = addr.ToString("X8");
                    newRow.Cells.Add(varAddr);

                    DataGridViewTextBoxCell varValue = new DataGridViewTextBoxCell();
                    if (values.Length >= 4) {
                        switch ((VARTYPE)typeIdx) {
                            case VARTYPE.INT8: {
                                sbyte value = Convert.ToSByte(values[3]);
                                SetValue(addr, value);
                                varValue.Value = value.ToString();
                                break;
                            }
                            case VARTYPE.UINT8: {
                                byte value = Convert.ToByte(values[3]);
                                SetValue(addr, value);
                                varValue.Value = value.ToString();
                                break;
                            }
                            case VARTYPE.INT16: {
                                Int16 value = Convert.ToInt16(values[3]);
                                SetValue(addr, value);
                                varValue.Value = value.ToString();
                                break;
                            }
                            case VARTYPE.UINT16: {
                                UInt16 value = Convert.ToUInt16(values[3]);
                                SetValue(addr, value);
                                varValue.Value = value.ToString();
                                break;
                            }
                            case VARTYPE.INT32: {
                                Int32 value = Convert.ToInt32(values[3]);
                                SetValue(addr, value);
                                varValue.Value = value.ToString();
                                break;
                            }
                            case VARTYPE.UINT32: {
                                UInt32 value = Convert.ToUInt32(values[3]);
                                SetValue(addr, value);
                                varValue.Value = value.ToString();
                                break;
                            }
                            case VARTYPE.FLOAT: {
                                float value = Convert.ToSingle(values[3]);
                                SetValue(addr, value);
                                varValue.Value = value.ToString();
                                break;
                            }
                            default: {
                                break;
                            }
                        }
                    } else {
                        varValue.Value = this.GetFormattedValue((VARTYPE)typeIdx, addr);
                    }

                    newRow.Cells.Add(varValue);

                    this.dataGridView_varWrite.Rows.Add(newRow);

                }
            }
        }

        #endregion // Button Event Handlers

        #region ComboBox Event Handlers
        private void cbb_channel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strTemp = cbb_channel.Text;
            strTemp = strTemp.Substring(strTemp.IndexOf('B') + 1, 1);
            strTemp = "0x5" + strTemp;
            this.m_PcanHandle = Convert.ToByte(strTemp, 16);
        }

        private void cbb_baudrates_SelectedIndexChanged(object sender, EventArgs e)
        {
            TPCANBaudrate[] baudValue = (TPCANBaudrate[])Enum.GetValues(typeof(TPCANBaudrate));
            this.m_BaudRate = baudValue[cbb_baudrates.SelectedIndex];
        }
        #endregion // ComboBox Event Handlers

        #region TextBox Handlers
        private void textBox_upload_addr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = this.ValidateKeyPressForHex(e.KeyChar);
        }

        private void textBox_download_addr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = this.ValidateKeyPressForHex(e.KeyChar);
        }

        private void textBox_BroadcastID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = this.ValidateKeyPressForHex(e.KeyChar);
        }

        private void textBox_MasterID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = this.ValidateKeyPressForHex(e.KeyChar);
        }

        private void textBox_SlaveID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = this.ValidateKeyPressForHex(e.KeyChar);
        }
        #endregion

        #region RichTextBox Handlers
        private void rtb_download_value_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool handled = this.ValidateKeyPressForHex(e.KeyChar);
            if (e.KeyChar == ' ')
            {
                handled = false;
            }
            e.Handled = handled;
        }

        #endregion

        #region DataGridView Handlers

        private void SetupDataGridView()
        {
            DataGridViewTextBoxColumn varType;
            DataGridViewTextBoxColumn varName;
            DataGridViewTextBoxColumn varAddr;
            DataGridViewTextBoxColumn varValue;

            #region Variable Read
            varType = new DataGridViewTextBoxColumn();
            varType.Name = "TYPE";
            varType.Width = 100;
            varType.ReadOnly = true;

            varName = new DataGridViewTextBoxColumn();
            varName.Name = "NAME";
            varName.Width = 200;
            varName.ReadOnly = true;

            varAddr = new DataGridViewTextBoxColumn();
            varAddr.Name = "ADDRESS";
            varAddr.Width = 100;
            varAddr.ReadOnly = true;

            varValue = new DataGridViewTextBoxColumn();
            varValue.Name = "VALUE";
            varValue.Width = 100;

            this.dataGridView_varRead.Columns.Add(varType);
            this.dataGridView_varRead.Columns.Add(varName);
            this.dataGridView_varRead.Columns.Add(varAddr);
            this.dataGridView_varRead.Columns.Add(varValue);
            #endregion

            #region Variable Write
            varType = new DataGridViewTextBoxColumn();
            varType.Name = "TYPE";
            varType.Width = 100;
            varType.ReadOnly = true;

            varName = new DataGridViewTextBoxColumn();
            varName.Name = "NAME";
            varName.Width = 200;
            varName.ReadOnly = true;

            varAddr = new DataGridViewTextBoxColumn();
            varAddr.Name = "ADDRESS";
            varAddr.Width = 100;
            varAddr.ReadOnly = true;

            varValue = new DataGridViewTextBoxColumn();
            varValue.Name = "VALUE";
            varValue.Width = 100;

            this.dataGridView_varWrite.Columns.Add(varType);
            this.dataGridView_varWrite.Columns.Add(varName);
            this.dataGridView_varWrite.Columns.Add(varAddr);
            this.dataGridView_varWrite.Columns.Add(varValue);
            #endregion
        }

        #endregion

        #endregion // Method

        private void ui_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* Disconnect when closing */
            if(this.btn_Connect.Text != "Connect") {
                this.btn_Connect_Click(this, new EventArgs());
            }
        }

        #region Test
        private void button_addDaq_Click(object sender, EventArgs e)
        {
            var daqCnt = this.treeView_Daq.Nodes.Count;
            DaqTreeNode node = new DaqTreeNode((UInt16)daqCnt, (UInt16)comboBox_eventChannel.SelectedIndex);
            this.treeView_Daq.Nodes.Add(node);
        }

        private void button_AddOdt_Click(object sender, EventArgs e)
        {
            var node = this.treeView_Daq.SelectedNode;
            if (node == null) {
                return;
            }
            if (node.Level != 0) {
                return;
            }
            var odtCnt = node.Nodes.Count;
            OdtTreeNode nodeOdt = new OdtTreeNode((Byte)odtCnt);
            node.Nodes.Add(nodeOdt);

            if (( odtCnt == 0 ) && ( this.m_xcp.timestampSize != 0 )) {
                VARTYPE type = VARTYPE.UINT8;
                switch (this.m_xcp.timestampSize) {
                    case 1: {
                        type = VARTYPE.UINT8;
                        break;
                    }
                    case 2: {
                        type = VARTYPE.UINT16;
                        break;
                    }
                    case 4: {
                        type = VARTYPE.UINT32;
                        break;
                    }
                    default: {
                        break;
                    }
                }

                OdtEntryTimestampNode timestampNode = new OdtEntryTimestampNode(this.m_xcp.timestampUnit, type);
                nodeOdt.Add(timestampNode);
            }
        }

        private Int32 GetTypeSize(VARTYPE type)
        {
            Int32 typeSize = 0;
            switch(type) {
                case VARTYPE.INT8:
                case VARTYPE.UINT8: {
                    typeSize = 1;
                    break;
                }
                case VARTYPE.INT16:
                case VARTYPE.UINT16: {
                    typeSize = 2;
                    break;
                }
                case VARTYPE.INT32:
                case VARTYPE.UINT32:
                case VARTYPE.FLOAT: {
                    typeSize = 4;
                    break;
                }
                default: {
                    break;
                }
            }
            return typeSize;
        }

        private Int32 GetTypeSize(string type)
        {
            string[] strTypesArray = Enum.GetNames(typeof(VARTYPE));
            var idx = Array.FindIndex(strTypesArray, s => s.Equals(type));
            return GetTypeSize((VARTYPE)idx);
        }

        private Int32 GetAvailableByteOdt(TreeNode nodeOdt)
        {
            Int32 freeByte = this.m_xcp.DaqMaxOdtEntrySize;
            var nodeCnt = nodeOdt.Nodes.Count;
            if(nodeCnt > 0) {
                for(int i = 0; i < nodeCnt; i++) {
                    /* Traverse Odt Entry */
                    TreeNode node = nodeOdt.Nodes[i];
                    if(node.Nodes.Count > 0) {
                        for(int j = 0; j < node.Nodes.Count; j++) {
                            TreeNode childNode = node.Nodes[j];
                            if(childNode.Name == "Type") {
                                string[] strArray = childNode.Text.Split(':');
                                freeByte -= this.GetTypeSize(strArray[1]);
                            }
                        }
                    }
                }
            }
            return freeByte;
        }

        private void button_addEntry_Click(object sender, EventArgs e)
        {
            if (this.treeView_Daq.SelectedNode == null) {
                /* none selected */
                return;
            }
            if (this.treeView_Daq.SelectedNode.Level != 1) {
                /* Not an ODT node */
                return;
            }

            OdtTreeNode node = (OdtTreeNode)(this.treeView_Daq.SelectedNode);
            var OdtEntryCnt = node.OdtEntryCount;
            var freeOdtEntyBytes = GetAvailableByteOdt(node);
            UInt32 Addr = UInt32.Parse(textBox_odtEntryAddr.Text, System.Globalization.NumberStyles.HexNumber);
            OdtEntryTreeNode newNode = new OdtEntryTreeNode(OdtEntryCnt,
                                                textBox_odtEntryName.Text,
                                                Addr,
                                                (VARTYPE)comboBox_varType.SelectedIndex
                                                );
            if((freeOdtEntyBytes - GetTypeSize(comboBox_varType.Text)) >= 0) {
                node.Add(newNode);
            } else {
                MessageBox.Show("New ODT Entry does not fit!");
            }            
        }

        private void button_removeEntry_Click(object sender, EventArgs e)
        {
            if (this.treeView_Daq.SelectedNode == null) {
                /* none selected */
                return;
            }
            if (this.treeView_Daq.SelectedNode.Level != 2) {
                /* Not an ODTEntry node */
                return;
            }

            OdtTreeNode parent = (OdtTreeNode)( this.treeView_Daq.SelectedNode.Parent);
            if(typeof(OdtEntryTreeNode) == this.treeView_Daq.SelectedNode.GetType()) {
                OdtEntryTreeNode node = (OdtEntryTreeNode)this.treeView_Daq.SelectedNode;
                parent.Remove(node);
            }
        }

        private void button_configDaq_Click(object sender, EventArgs e)
        {
            TXCPResult result = TXCPResult.XCP_ERR_OK;
            List<OdtEntryTreeNode> odtEntry = new List<OdtEntryTreeNode>();

            if (!this.m_xcp.IsConnected) {
                return;
            }

            byte[] buffer;
            buffer = new byte[Peak.Can.Xcp.XCPApi.CAN_MAX_LEN];
            result = XCPApi.FreeDAQLists(this.m_xcp.m_XcpSession, buffer, Convert.ToUInt16(buffer.Length));

            UInt16 daqCount = Convert.ToUInt16(this.treeView_Daq.Nodes.Count);
            if (daqCount > 0) {
                /* Allocate DAQ */
                result = XCPApi.AllocateDAQLists(this.m_xcp.m_XcpSession, daqCount, buffer, Convert.ToUInt16(buffer.Length));
                /* Allocate ODT */
                #region Allocate ODT
                for (UInt16 i = 0; i < daqCount; i++) {
                    DaqTreeNode daqNode = (DaqTreeNode)this.treeView_Daq.Nodes[i];
                    TXCPDAQListConfig daqConfig;
                    daqConfig.DAQListNumber = daqNode.id;
                    daqConfig.EventChannelNumber = daqNode.eventChannel;
                    daqConfig.TransmissionRate = 1;
                    daqConfig.DAQPriority = 0;
                    result = XCPApi.SetDAQListMode(this.m_xcp.m_XcpSession, 0x10,
                                daqConfig, buffer, Convert.ToUInt16(buffer.Length));
                    var odtCount = daqNode.Nodes.Count;
                    if (odtCount > 0) {
                        result = XCPApi.AllocateODT(this.m_xcp.m_XcpSession, i, Convert.ToByte(odtCount), buffer, Convert.ToUInt16(buffer.Length));
                    }
                }
                #endregion

                /* Allocate ODT Entries*/
                #region Allocate ODT Entry
                Byte pid = 0;
                for (UInt16 i = 0; i < daqCount; i++) {
                    var daqNode = this.treeView_Daq.Nodes[i];
                    var odtCount = daqNode.Nodes.Count;
                    if (odtCount > 0) {
                        for(Byte j = 0; j < odtCount; j++) {
                            /* Assign DAQ ODT packet ID */
                            OdtTreeNode odtNode = (OdtTreeNode)(daqNode.Nodes[j]);
                            odtNode.pid = pid;
                            pid++;
                            if(odtNode.OdtEntryCount > 0) {
                                result = XCPApi.AllocateODTEntry(this.m_xcp.m_XcpSession, i, j, odtNode.OdtEntryCount, buffer, Convert.ToUInt16(buffer.Length));
                            } else {
                                MessageBox.Show("No ODT entry found in an ODT!");
                                return;
                            }
                        }
                    }
                }
                #endregion

                /* Configure ODT Entries */
                #region Write ODT Entries
                for (UInt16 i = 0; i < daqCount; i++) {
                    var daqNode = this.treeView_Daq.Nodes[i];
                    var odtCount = daqNode.Nodes.Count;
                    if (odtCount > 0) {
                        for (Byte j = 0; j < odtCount; j++) {
                            OdtTreeNode odtNode = (OdtTreeNode)( daqNode.Nodes[j] );
                            if (odtNode.Nodes.Count > 0) {
                                Byte odtEntryId = 0;
                                for(Byte k = 0; k < odtNode.Nodes.Count; k++) {
                                    if(typeof(OdtEntryTreeNode) == odtNode.Nodes[k].GetType()) {
                                        TXCPODTEntry odtEntryInstance;
                                        OdtEntryTreeNode odtEntryNode = (OdtEntryTreeNode)( odtNode.Nodes[k] );
                                        odtEntryInstance.BitOffset = 0;
                                        odtEntryInstance.DAQAddressExtension = 0;
                                        odtEntryInstance.DAQAddress = odtEntryNode.varAddress;
                                        odtEntryInstance.DAQSize = Convert.ToByte(GetTypeSize(odtEntryNode.varType));
                                        result = XCPApi.SetDAQListPointer(this.m_xcp.m_XcpSession,
                                                    i, j, odtEntryId, buffer, Convert.ToUInt16(buffer.Length));
                                        result = XCPApi.WriteDAQListEntry(this.m_xcp.m_XcpSession,
                                                    odtEntryInstance, buffer, Convert.ToUInt16(buffer.Length));
                                        odtEntryId++;

                                        odtEntry.Add(odtEntryNode);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                this.comboBox_odtEntry.Items.Clear();
                this.plotList = null;
                if(odtEntry.Count > 0) {
                    this.plotList = new OdtEntryPlotPoint[odtEntry.Count];
                    for(int i = 0; i < odtEntry.Count; i++) {
                        this.plotList[i] = new OdtEntryPlotPoint();
                        this.plotList[i].odtEntry = odtEntry[i];
                        this.comboBox_odtEntry.Items.Add(this.plotList[i].odtEntry.varName);
                    }
                    this.comboBox_odtEntry.SelectedIndex = 0;
                }
            }
        }

        private void button_saveDaq_Click(object sender, EventArgs e)
        {
            /// TODO
        }
        #endregion

        private void button_startDaq_Click(object sender, EventArgs e)
        {
            TXCPResult result = TXCPResult.XCP_ERR_OK;

            if (!this.m_xcp.IsConnected) {
                return;
            }

            byte[] buffer;
            buffer = new byte[Peak.Can.Xcp.XCPApi.CAN_MAX_LEN];

            if (button_startDaq.Text == "Start") {
                var daqCount = treeView_Daq.Nodes.Count;
                for(UInt16 i = 0; i < daqCount; i++) {
                    DaqTreeNode daqNode = (DaqTreeNode)( treeView_Daq.Nodes[i] );
                    result = XCPApi.StartStopDAQList(this.m_xcp.m_XcpSession, 0x02, i, buffer, Convert.ToUInt16(buffer.Length));
                }
                result = XCPApi.StartStopSynchronizedDAQList(this.m_xcp.m_XcpSession, 0x01, buffer, Convert.ToUInt16(buffer.Length));
                button_startDaq.Text = "Stop";
            } else {
                result = XCPApi.StartStopSynchronizedDAQList(this.m_xcp.m_XcpSession, 0x00, buffer, Convert.ToUInt16(buffer.Length));
                button_startDaq.Text = "Start";
            }
        }

        private void timer_poll_Tick(object sender, EventArgs e)
        {
            TXCPResult result = TXCPResult.XCP_ERR_OK;
            if (this.m_xcp.IsConnected) {
                byte[] buffer;
                buffer = new byte[Peak.Can.Xcp.XCPApi.CAN_MAX_LEN];
                while (result == TXCPResult.XCP_ERR_OK) {
                    result = XCPApi.DequeuePacket(this.m_xcp.m_XcpSession, TXCPQueue.XCP_DTO_QUEUE, buffer, Convert.ToUInt16(buffer.Length));
                    if (result == TXCPResult.XCP_ERR_OK) {
                        byte pid = buffer[0];
                        /* Find odt */
                        OdtTreeNode odtNode = null;
                        var daqCount = treeView_Daq.Nodes.Count;
                        if (daqCount > 0) {
                            for (UInt16 daq = 0; daq < daqCount; daq++) {
                                DaqTreeNode daqNode = (DaqTreeNode)( treeView_Daq.Nodes[daq] );
                                var odtCount = daqNode.Nodes.Count;
                                if (odtCount > 0) {
                                    for (UInt16 odt = 0; odt < odtCount; odt++) {
                                        OdtTreeNode node = (OdtTreeNode)( daqNode.Nodes[odt] );
                                        if (node.pid == pid) {
                                            odtNode = node;
                                        }
                                    }
                                }
                            }
                        }
                        if (odtNode != null) {
                            /* odt Found */
                            int idx = 1;
                            if (odtNode.Nodes.Count > 0) {
                                for (UInt16 i = 0; i < odtNode.Nodes.Count; i++) {
                                    if (typeof(OdtEntryTimestampNode) == odtNode.Nodes[i].GetType()) {
                                        /* Timestamp */
                                        OdtEntryTimestampNode node = (OdtEntryTimestampNode)odtNode.Nodes[i];
                                        UInt32 tick = 0;
                                        switch (GetTypeSize(node.varType)) {
                                            case 1: {
                                                tick = buffer[idx];
                                                break;
                                            }
                                            case 2: {
                                                tick = BitConverter.ToUInt16(buffer, idx);
                                                break;
                                            }
                                            case 4: {
                                                tick = BitConverter.ToUInt32(buffer, idx);
                                                break;
                                            }
                                            default: {
                                                break;
                                            }
                                        }
                                        idx += GetTypeSize(node.varType);
                                        node.UpdateTick(tick);
                                    } else if (typeof(OdtEntryTreeNode) == odtNode.Nodes[i].GetType()) {
                                        /* OdtEntry */
                                        OdtEntryTreeNode node = (OdtEntryTreeNode)odtNode.Nodes[i];
                                        node.UpdateValue(buffer, idx);
                                        idx += GetTypeSize(node.varType);
                                    } else {
                                        Console.WriteLine("Unknown Odt Entry type");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void timer_displayRefresh_Tick(object sender, EventArgs e)
        {
            /* update treeView */
            if(treeView_Daq.Nodes.Count > 0) {
                for(int i = 0; i < treeView_Daq.Nodes.Count; i++) {
                    DaqTreeNode daqNode = (DaqTreeNode)( treeView_Daq.Nodes[i] );
                    if(daqNode.Nodes.Count > 0) {
                        for(int j = 0; j < daqNode.Nodes.Count; j++) {
                            OdtTreeNode odtNode = (OdtTreeNode)( daqNode.Nodes[j] );
                            if(odtNode.Nodes.Count > 0) {
                                for(int k = 0; k < odtNode.Nodes.Count; k++) {
                                    if (typeof(OdtEntryTreeNode) == odtNode.Nodes[k].GetType()) {
                                        OdtEntryTreeNode node = (OdtEntryTreeNode)odtNode.Nodes[k];
                                        node.RefreshNode();
                                    } else if(typeof(OdtEntryTimestampNode) == odtNode.Nodes[k].GetType()) {
                                        OdtEntryTimestampNode node = (OdtEntryTimestampNode)odtNode.Nodes[k];
                                        node.RefreshNode();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if ((this.plotList != null) && (this.checkBox_plotEnable.Checked)) {
                if(this.plotList.Length > 0) {
                    this.formsPlot_daq.Plot.Clear();
                    for (int i = 0; i < this.plotList.Length; i++) {
                        var timestamp = this.plotList[i].GetX();
                        var value = this.plotList[i].GetY();
                        if ( timestamp.Length > 0 ) {
                            this.formsPlot_daq.Plot.AddScatter(timestamp, value);
                            this.formsPlot_daq.Refresh();
                        }
                    }
                }
            }
        }

        private void button_daqClear_Click(object sender, EventArgs e)
        {
            this.treeView_Daq.Nodes.Clear();
        }

        private void button_addPlot_Click(object sender, EventArgs e)
        {
            this.plotList[comboBox_odtEntry.SelectedIndex].odtEntry.UpdateValueEvent += this.OdtEntryUpdateValue;
        }

        private void OdtEntryUpdateValue(object sender, OdtEntryUpdateValueArgs e)
        {
            if(this.plotList.Length > 0) {
                for(int i = 0; i < this.plotList.Length; i++) {
                    if(this.plotList[i].odtEntry == sender) {
                        this.plotList[i].Add(e.timestamp, e.value);
                    }
                }
            }
        }
    }

    public enum VARTYPE
    {
        INT8 = 0,
        UINT8,
        INT16,
        UINT16,
        INT32,
        UINT32,
        FLOAT
    }

    public class ApplXcp
    {
#region Members
        private TXCPChannel m_XcpChannel;
        public TXCPHandle m_XcpSession;
        private TXCPProtocolLayerConfig m_XcpProtocolConfig;
        private TXCPTransportLayerCAN m_XcpSlaveData;
        private bool xcpSlaveDataIsValid;
        private TPCANHandle m_PcanHandle;
        private TPCANBaudrate m_BaudRate;
        public bool IsConnected { get; private set; }
        
        public byte addr_granularity { get; private set; }
        public byte resource { get; private set; }
        public byte mode { get; private set; }
        public byte max_cto { get; private set; }
        public UInt16 max_dto { get; private set; }
        public byte protocol_version { get; private set; }
        public byte transport_version { get; private set;  }
        public bool IsBigEndian { get; private set;  }
        public UInt16 maxEventChannel { get; private set; }
        public byte DaqMaxOdtEntrySize { get; private set; }
        public byte DaqOdtEntryGranularity { get; private set; }
        public double timestampUnit { get; private set; }
        public byte timestampSize { get; private set; }
        public UInt16 timestampTicks { get; private set; }

#region Event Info
        public XcpEventInfo[] eventInfo { get; private set; }
#endregion

        private const byte AG_BYTE = 1;
        private const byte AG_WORD = 2;
        private const byte AG_DWORD = 4;
#endregion


#region Methods
        public ApplXcp()
        {
            this.m_XcpSession = 0;
            this.m_XcpChannel = 0;

            this.m_XcpProtocolConfig = new TXCPProtocolLayerConfig();
            this.m_XcpProtocolConfig.T1 = 25;
            this.m_XcpProtocolConfig.T2 = 25;
            this.m_XcpProtocolConfig.T3 = 25;
            this.m_XcpProtocolConfig.T4 = 25;
            this.m_XcpProtocolConfig.T5 = 25;
            this.m_XcpProtocolConfig.T6 = 25;
            this.m_XcpProtocolConfig.T7 = 25;

            this.xcpSlaveDataIsValid = false;
            this.IsConnected = false;
        }

#region helper
        private double ConvertToUnitTime(byte value)
        {
            double unit = 0.0;
            switch (value) {
                case 0: {
                    unit = 1e-9;
                    break;
                }
                case 1: {
                    unit = 10e-9;
                    break;
                }
                case 2: {
                    unit = 100e-9;
                    break;
                }
                case 3: {
                    unit = 1e-6;
                    break;
                }
                case 4: {
                    unit = 10e-6;
                    break;
                }
                case 5: {
                    unit = 100e-6;
                    break;
                }
                case 6: {
                    unit = 1e-3;
                    break;
                }
                case 7: {
                    unit = 10e-3;
                    break;
                }
                case 8: {
                    unit = 100e-3;
                    break;
                }
                case 9: {
                    unit = 1;
                    break;
                }
                default: {
                    unit = 1e-3;
                    break;
                }
            }
            return unit;
        }
#endregion

        public void SetID(UInt32 broadcastId, UInt32 masterId, UInt32 slaveId)
        {
            this.m_XcpSlaveData.BroadcastID = broadcastId;
            this.m_XcpSlaveData.MasterID = masterId;
            this.m_XcpSlaveData.SlaveID = slaveId;
            this.xcpSlaveDataIsValid = true;
        }

        public TXCPResult Connect(TPCANHandle PcanHandle, TPCANBaudrate baudrate)
        {
            TXCPResult result;
            if (this.xcpSlaveDataIsValid != true) {
                throw new InvalidOperationException("XCP CAN ID not initialized");
            }
            result = XCPApi.InitializeCanChannel(out this.m_XcpChannel, PcanHandle, baudrate);
            if (result != TXCPResult.XCP_ERR_OK) {
                Console.WriteLine("XCPApi.InitializeCanChannel error!");
                return result;
            }
            this.m_PcanHandle = PcanHandle;
            this.m_BaudRate = baudrate;

            result = XCPApi.AddSlaveOnCAN(this.m_XcpChannel, this.m_XcpSlaveData, this.m_XcpProtocolConfig, out this.m_XcpSession);
            if (result != TXCPResult.XCP_ERR_OK) {
                Console.WriteLine("XCPApi.AddSlaveOnCAN error!");
                /* Clean-up */
                XCPApi.UninitializeChannel(this.m_XcpChannel);
                this.m_XcpChannel = 0;
                return result;
            }

            byte[] msg;
            msg = new byte[Peak.Can.Xcp.XCPApi.CAN_MAX_LEN];
            result = XCPApi.Connect(this.m_XcpSession, 0, msg, (ushort)msg.Length);
            if (result != TXCPResult.XCP_ERR_OK) {
                Console.WriteLine("XCPApi.Connect error!");
                /* Clean-up */
                XCPApi.RemoveSlave(this.m_XcpSession);
                this.m_XcpSession = 0;
                XCPApi.UninitializeChannel(this.m_XcpChannel);
                this.m_XcpChannel = 0;
                return result;
            }
            this.IsConnected = true;
            this.resource = msg[1];
            this.mode = msg[2];
            this.IsBigEndian = ( ( this.mode & 0x01 ) == 1 );
            this.m_XcpProtocolConfig.MotorolaFormat = this.IsBigEndian;

            switch( ( this.mode >> 1 ) & 0x03 ) {
                case 1: {
                    this.addr_granularity = AG_WORD;
                    this.m_XcpProtocolConfig.AddressGranularity = AG_WORD;
                    break;
                }
                case 2: {
                    this.addr_granularity = AG_DWORD;
                    this.m_XcpProtocolConfig.AddressGranularity = AG_DWORD;
                    break;
                }
                default: {
                    this.addr_granularity = AG_BYTE;  // Defualt is BYTE
                    this.m_XcpProtocolConfig.AddressGranularity = AG_BYTE; 
                    break;
                }
            }

            this.max_cto = msg[3];
            this.max_dto = (UInt16)( msg[4] + ( msg[5] << 8 ) );
            this.protocol_version = msg[6];
            this.transport_version = msg[7];

            /* 
             * Check slave configuration validity
             *      MAX_CTO modulo AG must be zero
             *      MAX_DTO modulo AG must be zero
             */
            if((this.max_cto % this.addr_granularity) != 0) {
                throw new InvalidConstraintException("MAX_CTO % AG != 0");
            }
            if((this.max_dto % this.addr_granularity) != 0) {
                throw new InvalidConstraintException("MAX_DTO % AG != 0");
            }

            if (true != this.GetDaqProcInfo()) {
                /* Clean-up */
                XCPApi.Disconnect(this.m_XcpSession);
                XCPApi.RemoveSlave(this.m_XcpSession);
                this.m_XcpSession = 0;
                XCPApi.UninitializeChannel(this.m_XcpChannel);
                this.m_XcpChannel = 0;
                return result;
            }

            if(this.maxEventChannel > 0) {
                this.eventInfo = new XcpEventInfo[this.maxEventChannel];

                for(UInt16 i = 0; i < this.maxEventChannel; i++) {
                    GetDaqEventInfo(i);
                }
            }

            if (true != this.GetDaqResInfo()) {
                /* Clean-up */
                XCPApi.Disconnect(this.m_XcpSession);
                XCPApi.RemoveSlave(this.m_XcpSession);
                this.m_XcpSession = 0;
                XCPApi.UninitializeChannel(this.m_XcpChannel);
                this.m_XcpChannel = 0;
                return result;
            }

            return TXCPResult.XCP_ERR_OK;
        }

        public void Disconnect()
        {
            if(IsConnected) {
                TXCPResult result;
                byte[] msg;
                msg = new byte[Peak.Can.Xcp.XCPApi.CAN_MAX_LEN];
                // Sets the ECU (slave) in a disconnected state
                if (this.m_XcpSession != 0) {
                    result = XCPApi.Disconnect(this.m_XcpSession, msg, (ushort)msg.Length);
                    if (result != TXCPResult.XCP_ERR_OK) {
                        Console.WriteLine("XCPApi.Disconnect failed");
                    }
                    result = XCPApi.RemoveSlave(this.m_XcpSession);
                    if (result != TXCPResult.XCP_ERR_OK) {
                        Console.WriteLine("XCPApi.RemoveSlave failed");
                    }
                    this.m_XcpSession = 0;
                }
                if (this.m_XcpChannel != 0) {
                    result = XCPApi.UninitializeChannel(this.m_XcpChannel);
                    if (result != TXCPResult.XCP_ERR_OK) {
                        Console.WriteLine("XCPApi.UninitializeChannel failed");
                    }
                    this.m_XcpChannel = 0;
                }
                this.IsConnected = false;
            }
        }

        private bool GetDaqProcInfo()
        {
            byte[] msg;
            TXCPResult result;

            if (this.IsConnected) {
                msg = new byte[XCPApi.CAN_MAX_LEN];
                result = XCPApi.GetDAQProcessorInformation(this.m_XcpSession, msg, (ushort)msg.Length);
                if (result != TXCPResult.XCP_ERR_OK) {
                    Console.WriteLine("XCPApi.GetDAQProcessorInformation error!");
                    return false;
                }
                this.maxEventChannel = BitConverter.ToUInt16(msg, 4);
                Console.WriteLine("DAQ Max Event Channel: " + this.maxEventChannel);
            } else {
                return false;
            }
            return true;
        }

        private bool GetDaqResInfo()
        {
            byte[] msg;
            TXCPResult result;

            if(this.IsConnected) {
                msg = new byte[XCPApi.CAN_MAX_LEN];
                result = XCPApi.GetDAQResolutionInformation(this.m_XcpSession, msg, (ushort)msg.Length);
                if (result != TXCPResult.XCP_ERR_OK) {
                    Console.WriteLine("XCPApi.GetDAQResolutionInformation error!");
                    return false;
                }
                this.DaqOdtEntryGranularity = msg[1];
                Console.WriteLine("DAQ ODT Entry Granularity: " + this.DaqOdtEntryGranularity);
                this.DaqMaxOdtEntrySize = msg[2];
                Console.WriteLine("DAQ ODT Entry Size: " + this.DaqMaxOdtEntrySize);
                this.timestampUnit = this.ConvertToUnitTime((byte)(msg[5] >> 4));
                Console.WriteLine("DAQ Timestamp Unit (second): " + this.timestampUnit);
                this.timestampSize = (byte)( msg[5] & 0x7 );
                Console.WriteLine("DAQ Timestamp Size: " + this.timestampSize + " bytes");
                this.timestampTicks = BitConverter.ToUInt16(msg, 6);
                Console.WriteLine("DAQ Timestamp Ticks: " + this.timestampTicks);
            } else {
                return false;
            }
            return true;
        }

        private bool GetDaqEventInfo(UInt16 eventChannel)
        {
            byte[] msg;
            TXCPResult result;

            if (this.IsConnected) {
                msg = new byte[XCPApi.CAN_MAX_LEN];
                result = XCPApi.GetEventChannelInformation(this.m_XcpSession, eventChannel, msg, (ushort)msg.Length);
                if (result != TXCPResult.XCP_ERR_OK) {
                    Console.WriteLine("XCPApi.GetEventChannelInformation error!");
                    return false;
                }
                var timeUnit = ConvertToUnitTime(msg[5]);
                this.eventInfo[eventChannel].period = timeUnit * msg[4];
                this.eventInfo[eventChannel].priority = msg[6];
                this.eventInfo[eventChannel].property = msg[1];
                Console.WriteLine("DAQ Event" + eventChannel + "=> Period: " + this.eventInfo[eventChannel].period +
                                   ", Property: " + this.eventInfo[eventChannel].property + 
                                   ", Priority: " + this.eventInfo[eventChannel].priority);
                byte nameLen = msg[3];
                if(nameLen > 0) {
                    char[] chars = new char[nameLen];
                    if (this.addr_granularity != AG_WORD) {
                        throw new InvalidOperationException("Method for WORD address granularity only");
                    }
                    for (int i = 0; i < nameLen; i++) {
                        result = XCPApi.Upload(this.m_XcpSession, 1, msg, (ushort)msg.Length);
                        if (result != TXCPResult.XCP_ERR_OK) {
                            Console.WriteLine("XCPApi.Upload error!");
                            return false;
                        }
                        chars[i] = (char)( msg[2] );
                    }
                    this.eventInfo[eventChannel].name = new string(chars);
                    Console.WriteLine("DAQ Event" + eventChannel + " Name: " + this.eventInfo[eventChannel].name);
                }
            } else {
                return false;
            }
            return true;
        }

        public TXCPResult Read(UInt32 address, UInt32 count, out UInt16[] buffer)
        {
            byte[] msg;
            TXCPResult result;

            if ((count == 0) || (!this.IsConnected)) {
                buffer = null;
            } else {
                msg = new byte[XCPApi.CAN_MAX_LEN];
                if(this.addr_granularity != AG_WORD) {
                    throw new InvalidOperationException("Method for WORD address granularity only");
                }
                buffer = new UInt16[count];
                if(count <= 3) {
                    /* Use short upload */
                    result = XCPApi.ShortUpload(this.m_XcpSession, (byte)count, 0, address, msg, (UInt16)msg.Length);
                    if(result != TXCPResult.XCP_ERR_OK) {
                        Console.WriteLine("XCPApi.ShortUpload failed");
                        return result;
                    }
                    for(int i = 0; i < count; i++) {
                        buffer[i] = (UInt16)( msg[( i + 1 ) * 2] + ( msg[( ( i + 1 ) * 2 ) + 1] << 8 ) );
                    }
                } else {
                    UInt32 idx = 0;
                    Int32 remaining = (Int32)(count & 0x7FFFFFFF);
                    /* Use upload */
                    result = XCPApi.SetMemoryTransferAddress(this.m_XcpSession, 0, address, msg, (UInt16)msg.Length);
                    if (result != TXCPResult.XCP_ERR_OK) {
                        Console.WriteLine("XCPApi.SetMemoryTransferAddress failed");
                        return result;
                    }
                    while(remaining > 0) {
                        byte nElem = (byte)( ( remaining > 3 ) ? 3 : remaining );
                        result = XCPApi.Upload(this.m_XcpSession, nElem, msg, (UInt16)msg.Length);
                        if (result != TXCPResult.XCP_ERR_OK) {
                            Console.WriteLine("XCPApi.Upload failed. " + remaining + " remains");
                            return result;
                        }
                        for (int i = 0; i < nElem; i++) {
                            buffer[idx] = (UInt16)( msg[( i + 1 ) * 2] + ( msg[( i + 1 ) * 2 + 1] << 8 ) );
                            idx++;
                        }
                        remaining -= nElem;
                    }
                }
            }
            return TXCPResult.XCP_ERR_OK;
        }

        public TXCPResult Write(UInt32 address, UInt16[] buffer)
        {
            TXCPResult result;
            Int32 count = buffer.Length;
            Int32 remaining = count;

            if (this.addr_granularity != AG_WORD) {
                throw new InvalidOperationException("Method for WORD address granularity only");
            }

            if ((count > 0) && (this.IsConnected)) {
                UInt32 idx = 0;
                byte[] msg = new byte[XCPApi.CAN_MAX_LEN];
                result = XCPApi.SetMemoryTransferAddress(this.m_XcpSession, 0, address, msg, (UInt16)msg.Length);
                if (result != TXCPResult.XCP_ERR_OK) {
                    Console.WriteLine("XCPApi.SetMemoryTransferAddress failed");
                    return result;
                }
                while(remaining > 0) {
                    byte nElem = (byte)( ( remaining > 3 ) ? 3 : remaining );
                    byte[] data = new byte[2 * nElem];
                    for(int i = 0; i < nElem; i++) {
                        data[i * 2] = (byte)( buffer[idx] & 0x00FF );
                        data[( i * 2 ) + 1] = (byte)( ( buffer[idx] >> 8 ) & 0x00FF );
                        idx++;
                    }
                    result = XCPApi.Download(this.m_XcpSession, nElem, data, (byte)data.Length, msg, (UInt16)msg.Length);
                    remaining -= nElem;
                }
            }
            return TXCPResult.XCP_ERR_OK;
        }
#endregion
    }

    public struct XcpEventInfo
    {
        public byte property;
        public string name;
        public double period;
        public byte priority;
    }

    public class DaqTreeNode : TreeNode
    {
        public UInt16 id;
        public UInt16 eventChannel;

        public DaqTreeNode(UInt16 daq, UInt16 eventChannel)
        {
            this.Name = "daq";
            this.Text = this.Name + daq;
            this.id = daq;
            this.eventChannel = eventChannel;
        }
    }

    public class OdtTreeNode : TreeNode
    {
        public Byte pid;
        public Byte OdtEntryCount;

        public OdtTreeNode(Byte entry)
        {
            this.pid = 0;
            this.OdtEntryCount = 0;
            this.Name = "Odt";
            this.Text = this.Name + entry;
        }

        public void Add(OdtEntryTreeNode node)
        {
            this.Nodes.Add(node);
            this.OdtEntryCount += 1;
        }

        public void Remove(OdtEntryTreeNode node)
        {
            this.Nodes.Remove(node);
            this.OdtEntryCount -= 1;
        }

        public void Add(OdtEntryTimestampNode node)
        {
            this.Nodes.Clear();
            this.OdtEntryCount = 0;
            this.Nodes.Add(node);
        }
    }

    public class OdtEntryPlotPoint
    {
        public OdtEntryTreeNode odtEntry;
        private List<double> x;
        private List<double> y;

        public OdtEntryPlotPoint()
        {
            this.x = new List<double>();
            this.y = new List<double>();
        }

        public void Add(double x, double y)
        {
            this.x.Add(x);
            this.y.Add(y);
        }

        public double[] GetX()
        {
            return this.x.ToArray();
        }

        public double[] GetY()
        {
            return this.y.ToArray();
        }

        public void Clear()
        {
            this.x.Clear();
            this.y.Clear();
        }
    }

    public delegate void OdtEntryUpdateValueHandler(object sender, OdtEntryUpdateValueArgs e);
    public class OdtEntryUpdateValueArgs : EventArgs
    {
        public readonly double timestamp;
        public readonly double value;

        public OdtEntryUpdateValueArgs(double timestamp, double value)
        {
            this.timestamp = timestamp;
            this.value = value;
        }
    }

    public class OdtEntryTreeNode : TreeNode
    {
        public string varName;
        public UInt32 varAddress;
        public VARTYPE varType;
        public object value;

        private TreeNode nameNode;
        private TreeNode addrNode;
        private TreeNode typeNode;
        private TreeNode valueNode;

        public event OdtEntryUpdateValueHandler UpdateValueEvent;

        public OdtEntryTreeNode(Int32 odtEntryId, string name, UInt32 address, VARTYPE type)
        {
            this.UpdateValueEvent = null;

            this.Name = "OdtEntry";
            this.Text = this.Name + ":" + odtEntryId;

            this.varName = name;
            this.varAddress = address;
            this.varType = type;

            this.nameNode = new TreeNode();
            this.nameNode.Name = "Name";
            this.nameNode.Text = this.nameNode.Name + ":" + this.varName;

            this.addrNode = new TreeNode();
            this.addrNode.Name = "Address";
            this.addrNode.Text = this.addrNode.Name + ":" + this.varAddress.ToString("X8");

            this.typeNode = new TreeNode();
            this.typeNode.Name = "Type";
            this.typeNode.Text = this.typeNode.Name + ":" + this.varType.ToString();

            this.valueNode = new TreeNode();
            this.valueNode.Name = "Value";
            this.valueNode.Text = this.valueNode.Name + ":";
            this.value = 0;
            switch(this.varType) {
                case VARTYPE.INT8: {
                    this.valueNode.Text += Convert.ToSByte(this.value).ToString();
                    break;
                }
                case VARTYPE.UINT8: {
                    this.valueNode.Text += Convert.ToByte(this.value).ToString();
                    break;
                }
                case VARTYPE.INT16: {
                    this.valueNode.Text += Convert.ToInt16(this.value).ToString();
                    break;
                }
                case VARTYPE.UINT16: {
                    this.valueNode.Text += Convert.ToUInt16(this.value).ToString();
                    break;
                }
                case VARTYPE.INT32: {
                    this.valueNode.Text += Convert.ToInt32(this.value).ToString();
                    break;
                }
                case VARTYPE.UINT32: {
                    this.valueNode.Text += Convert.ToUInt32(this.value).ToString();
                    break;
                }
                case VARTYPE.FLOAT: {
                    this.valueNode.Text += Convert.ToSingle(this.value).ToString();
                    break;
                }
                default: {
                    this.value = 0;
                    this.valueNode.Text += "---";
                    break;
                }
            }

            this.Nodes.Add(this.nameNode);
            this.Nodes.Add(this.addrNode);
            this.Nodes.Add(this.typeNode);
            this.Nodes.Add(this.valueNode);
        }

        public void UpdateValue(byte[] buffer, int idx)
        {
            OdtTreeNode parent = null;
            double timestamp = 0.0;

            if(typeof(OdtTreeNode) == this.Parent.GetType()) {
                parent = (OdtTreeNode)(this.Parent.Parent.Nodes[0]);
                if(typeof(OdtEntryTimestampNode) == parent.Nodes[0].GetType()) {
                    OdtEntryTimestampNode timeNode = (OdtEntryTimestampNode)parent.Nodes[0];
                    timestamp = timeNode.time;
                }
            }

            this.valueNode.Text = this.valueNode.Name + ":";

            switch (this.varType) {
                case VARTYPE.INT8: {
                    this.value = (SByte)(buffer[idx]);
                    this.valueNode.Text += Convert.ToSByte(this.value).ToString();
                    break;
                }
                case VARTYPE.UINT8: {
                    this.value = buffer[idx];
                    this.valueNode.Text += Convert.ToByte(this.value).ToString();
                    break;
                }
                case VARTYPE.INT16: {
                    this.value = BitConverter.ToInt16(buffer, idx);
                    this.valueNode.Text += Convert.ToInt16(this.value).ToString();
                    break;
                }
                case VARTYPE.UINT16: {
                    this.value = BitConverter.ToUInt16(buffer, idx);
                    this.valueNode.Text += Convert.ToUInt16(this.value).ToString();
                    break;
                }
                case VARTYPE.INT32: {
                    this.value = BitConverter.ToInt32(buffer, idx);
                    this.valueNode.Text += Convert.ToInt32(this.value).ToString();
                    break;
                }
                case VARTYPE.UINT32: {
                    this.value = BitConverter.ToUInt32(buffer, idx);
                    this.valueNode.Text += Convert.ToUInt32(this.value).ToString();
                    break;
                }
                case VARTYPE.FLOAT: {
                    this.value = BitConverter.ToSingle(buffer, idx);
                    this.valueNode.Text += Convert.ToSingle(this.value).ToString();
                    break;
                }
                default: {
                    this.value = 0;
                    this.valueNode.Text += "---";
                    break;
                }
            }

            if(this.UpdateValueEvent != null) {
                this.UpdateValueEvent(this, new OdtEntryUpdateValueArgs(timestamp, Convert.ToDouble(this.value)));
            }
        }

        public void UpdateValue(object val)
        {
            this.valueNode.Text = this.valueNode.Name + ":";

            switch (this.varType) {
                case VARTYPE.INT8: {
                    this.value = Convert.ToSByte(val);
                    this.valueNode.Text += Convert.ToSByte(this.value).ToString();
                    break;
                }
                case VARTYPE.UINT8: {
                    this.value = Convert.ToByte(val);
                    this.valueNode.Text += Convert.ToByte(this.value).ToString();
                    break;
                }
                case VARTYPE.INT16: {
                    this.value = Convert.ToInt16(val);
                    this.valueNode.Text += Convert.ToInt16(this.value).ToString();
                    break;
                }
                case VARTYPE.UINT16: {
                    this.value = Convert.ToUInt16(val);
                    this.valueNode.Text += Convert.ToUInt16(this.value).ToString();
                    break;
                }
                case VARTYPE.INT32: {
                    this.value = Convert.ToInt32(val);
                    this.valueNode.Text += Convert.ToInt32(this.value).ToString();
                    break;
                }
                case VARTYPE.UINT32: {
                    this.value = Convert.ToUInt32(val);
                    this.valueNode.Text += Convert.ToUInt32(this.value).ToString();
                    break;
                }
                case VARTYPE.FLOAT: {
                    this.value = Convert.ToSingle(val);
                    this.valueNode.Text += Convert.ToSingle(this.value).ToString();
                    break;
                }
                default: {
                    this.value = 0;
                    this.valueNode.Text += "---";
                    break;
                }
            }
        }

        public void RefreshNode()
        {
            this.nameNode.Text = this.nameNode.Name + ":" + this.varName;
            this.addrNode.Text = this.addrNode.Name + ":" + this.varAddress.ToString("X8");
            this.typeNode.Text = this.typeNode.Name + ":" + this.varType.ToString();
            this.valueNode.Text = this.valueNode.Name + ":" + this.value.ToString();
        }
    }

    public class OdtEntryTimestampNode : TreeNode
    {
        public double time { get; private set; }
        private double timeResolution;
        private UInt32 tick;
        public VARTYPE varType;

        private TreeNode typeNode;
        private TreeNode valueNode;

        public OdtEntryTimestampNode(double timeResolution, VARTYPE type)
        {
            this.Name = "Timestamp";
            this.Text = this.Name;

            this.timeResolution = timeResolution;
            this.varType = type;

            this.typeNode = new TreeNode();
            this.typeNode.Name = "Type";
            this.typeNode.Text = this.typeNode.Name + ":" + this.varType.ToString();

            this.valueNode = new TreeNode();
            this.valueNode.Name = "Time";
            this.valueNode.Text = this.valueNode.Name + ":" + this.time;

            this.Nodes.Add(this.typeNode);
            this.Nodes.Add(this.valueNode);
        }

        public void UpdateTick(UInt32 tick)
        {
            this.tick = tick;
            this.time = this.tick * this.timeResolution;
        }

        public void RefreshNode()
        {
            this.typeNode.Text = this.typeNode.Name + ":" + this.varType.ToString();
            this.valueNode.Text = this.valueNode.Name + ":" + this.time;
        }
    }
}
