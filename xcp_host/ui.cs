using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        #region Method
        public ui()
        {
            InitializeComponent();

            this.m_xcp = new ApplXcp();

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

            this.btn_HwRefresh_Click(this, new EventArgs());
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
            cbb_baudrates.SelectedIndex = 3;
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
                        if(result == TXCPResult.XCP_ERR_OK) {
                            this.label_comm_resource.Text = "Resource: 0x" + this.m_xcp.resource.ToString("X2");
                            this.label_comm_mode.Text = "Mode: 0x" + this.m_xcp.mode.ToString("X2");
                            this.label_comm_max_cto.Text = "MAX_CTO: " + this.m_xcp.max_cto.ToString();
                            this.label_comm_max_dto.Text = "MAX_DTO: " + this.m_xcp.max_dto.ToString();
                            this.label_comm_proto_ver.Text = "Protocol Ver: " + this.m_xcp.protocol_version.ToString();
                            this.label_comm_transport_ver.Text = "Transport Ver: " + this.m_xcp.transport_version.ToString();
                            btn_Connect.Text = "Disconnect";
                            this.EnableUiElement(false);
                        }
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

        #endregion // Method

        private void ui_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* Disconnect when closing */
            if(this.btn_Connect.Text != "Connect") {
                this.btn_Connect_Click(this, new EventArgs());
            }
        }

        private void rtb_download_value_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool handled = this.ValidateKeyPressForHex(e.KeyChar);
            if(e.KeyChar == ' ') {
                handled = false;
            }
            e.Handled = handled;
        }
    }

    public class ApplXcp
    {
        #region Members
        private TXCPChannel m_XcpChannel;
        private TXCPHandle m_XcpSession;
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
}
