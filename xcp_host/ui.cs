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
        private TXCPChannel m_XcpChannel;
        private TXCPHandle m_XcpSession;
        private TXCPProtocolLayerConfig m_XcpProtocolConfig;
        private TXCPTransportLayerCAN m_XcpSlaveData;
        private TPCANHandle[] m_HandlesArray;
        private TPCANHandle m_PcanHandle;
        private TPCANBaudrate m_BaudRate;

        #region Method
        public ui()
        {
            InitializeComponent();

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
            // Protocol timouts configuration: 
            m_XcpProtocolConfig = new TXCPProtocolLayerConfig();
            m_XcpProtocolConfig.T1 = 25;
            m_XcpProtocolConfig.T2 = 25;
            m_XcpProtocolConfig.T3 = 25;
            m_XcpProtocolConfig.T4 = 25;
            m_XcpProtocolConfig.T5 = 25;
            m_XcpProtocolConfig.T6 = 25;
            m_XcpProtocolConfig.T7 = 25;

            m_XcpSession = 0;
            m_XcpChannel = 0;

            this.btn_HwRefresh_Click(this, new EventArgs());
        }

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
                if(btn_Connect.Text == "Connect") {
                    TXCPResult result;
                    StringBuilder strText = new StringBuilder(256);

                    this.m_XcpSlaveData.BroadcastID = UInt32.Parse(this.textBox_BroadcastID.Text, System.Globalization.NumberStyles.HexNumber);
                    this.m_XcpSlaveData.MasterID = UInt32.Parse(this.textBox_MasterID.Text, System.Globalization.NumberStyles.HexNumber);
                    this.m_XcpSlaveData.SlaveID = UInt32.Parse(this.textBox_SlaveID.Text, System.Globalization.NumberStyles.HexNumber);
                    result = XCPApi.InitializeCanChannel(out this.m_XcpChannel, this.m_PcanHandle, this.m_BaudRate);
                    if(result == TXCPResult.XCP_ERR_OK) {
                        // Associates a CAN channel with Slave data to communicate over CAN
                        // This operation retrieves the Session-handle (m_XcpSession) to be 
                        // used with all other XCP Command-Functions
                        result = XCPApi.AddSlaveOnCAN(this.m_XcpChannel, this.m_XcpSlaveData, this.m_XcpProtocolConfig, out this.m_XcpSession);
                        if (result == TXCPResult.XCP_ERR_OK) {
                            byte[] msg;
                            msg = new byte[Peak.Can.Xcp.XCPApi.CAN_MAX_LEN];
                            // Enables a session (connection) between this (master) application 
                            // and the slave described by the m_XcpSession handle
                            result = XCPApi.Connect(m_XcpSession, 0, msg, (ushort)msg.Length);
                            if(result == TXCPResult.XCP_ERR_OK) {
                                btn_Connect.Text = "Disconnect";
                            }
                        } else {
                            XCPApi.GetErrorText(result, strText);
                            MessageBox.Show("Error: " + strText.ToString());
                            XCPApi.UninitializeChannel(m_XcpChannel);
                            m_XcpChannel = 0;
                        }
                    } else {
                        XCPApi.GetErrorText(result, strText);
                        MessageBox.Show("Error: " + strText.ToString());
                    }
                } else {
                    TXCPResult result;
                    StringBuilder strText = new StringBuilder(256);
                    byte[] msg;
                    msg = new byte[Peak.Can.Xcp.XCPApi.CAN_MAX_LEN];
                    // Sets the ECU (slave) in a disconnected state
                    result = XCPApi.Disconnect(this.m_XcpSession, msg, (ushort)msg.Length);
                    if(result != TXCPResult.XCP_ERR_OK) {
                        XCPApi.GetErrorText(result, strText);
                        MessageBox.Show("Error: " + strText.ToString());
                    }
                    result = XCPApi.UninitializeChannel(this.m_XcpChannel);
                    if (result != TXCPResult.XCP_ERR_OK) {
                        XCPApi.GetErrorText(result, strText);
                        MessageBox.Show("Error: " + strText.ToString());
                    }
                    btn_Connect.Text = "Connect";
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
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

        #endregion // Method

        private void ui_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* Disconnect when closing */
            if(this.btn_Connect.Text != "Connect") {
                this.btn_Connect_Click(this, new EventArgs());
            }
        }
    }
}
