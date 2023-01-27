//  PXCP.cs
//
//  ~~~~~~~~~~~~
//
//  PCAN-XCP API
//
//  ~~~~~~~~~~~~
//
//  ------------------------------------------------------------------
//  Author : Keneth Wagner
//	Last change: 06.11.2018 K.Wagner
//
//  Language: C# 1.0
//  ------------------------------------------------------------------
//
//  Copyright (C) 2018  PEAK-System Technik GmbH, Darmstadt
//  more Info at http://www.peak-system.com 
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Peak.Can.Xcp
{
    using Peak.Can.Basic;
    using TPCANHandle = System.UInt16;
    using TPCANBitrateFD = System.String;
    using TPCANTimestampFD = System.UInt64;
    using TXCPChannel = System.UInt16;
    using TXCPHandle = System.UInt32;

    [Flags]
    public enum TXCPQueue : uint
    {
        /// <summary>
        /// Queue for packets of kind RES/ERR/EV/SERV
        /// </summary>
        XCP_CTO_QUEUE = 1,
        /// <summary>
        /// Queue for packets of kind DAQ
        /// </summary>
        XCP_DTO_QUEUE = 2,
    }

    public enum TXCPResult : uint
    {
        // Codes for not sucessfully executed XCP commands
        //
        /// <summary>
        /// Command processor synchronization
        /// </summary>
        XCP_ERR_CMD_SYNCH = 0,
        /// <summary>
        /// Command was not executed
        /// </summary>
        XCP_ERR_CMD_BUSY = 0x10,
        /// <summary>
        /// Command rejected because DAQ is running
        /// </summary>
        XCP_ERR_DAQ_ACTIVE = 0x11,
        /// <summary>
        /// Command rejected because PGM is running
        /// </summary>
        XCP_ERR_PGM_ACTIVE = 0x12,
        /// <summary>
        /// Unknown command or not implemented optional command
        /// </summary>
        XCP_ERR_CMD_UNKNOWN = 0x20,
        /// <summary>
        /// Command syntax invalid
        /// </summary>
        XCP_ERR_CMD_SYNTAX = 0x21,
        /// <summary>
        /// Command syntax valid but command parameter(s) out of range
        /// </summary>
        XCP_ERR_OUT_OF_RANGE = 0x22,
        /// <summary>
        /// The memory location is write protected
        /// </summary>
        XCP_ERR_WRITE_PROTECTED = 0x23,
        /// <summary>
        /// The memory location is not accessible
        /// </summary>
        XCP_ERR_ACCESS_DENIED = 0x24,
        /// <summary>
        /// Access denied,Seed & Key is required
        /// </summary>
        XCP_ERR_ACCESS_LOCKED = 0x25,
        /// <summary>
        /// Selected page not available
        /// </summary>
        XCP_ERR_PAGE_NOT_VALID = 0x26,
        /// <summary>
        /// Selected page mode not available
        /// </summary>
        XCP_RR_MODE_NOT_VALID = 0x27,
        /// <summary>
        /// Selected segment not valid
        /// </summary>
        XCP_ERR_SEGMENT_NOT_VALID = 0x28,
        /// <summary>
        /// Sequence error
        /// </summary>
        XCP_ERR_SEQUENCE = 0x29,
        /// <summary>
        /// DAQ configuration not valid
        /// </summary>
        XCP_ERR_DAQ_CONFIG = 0x2A,
        /// <summary>
        /// Memory overflow error
        /// </summary>
        XCP_ERR_MEMORY_OVERFLOW = 0x30,
        /// <summary>
        /// Generic error
        /// </summary>
        XCP_ERR_GENERIC = 0x31,
        /// <summary>
        /// The slave internal program verify routine detects an error
        /// </summary>
        XCP_ERR_VERIFY = 0x32,
        /// <summary>
        /// Access to the requested resource is temporary not possible
        /// </summary>
        XCP_ERR_RESOURCE_TEMPORARY_NOT_ACCESSIBLE = 0x33,

        // API return error codes
        //
        /// <summary>
        /// Acknowledge / no error
        /// </summary>
        XCP_ERR_OK = (1 << 8),
        /// <summary>
        /// Function not available / Operation not implemented
        /// </summary>
        XCP_ERR_NOT_IMPLEMENTED = (2 << 8),
        /// <summary>
        /// Invalid parameter value
        /// </summary>
        XCP_ERR_INVALID_PARAMETER = (3 << 8),
        /// <summary>
        /// The maximum amount of registered Slave channels was reached
        /// </summary>
        XCP_ERR_MAX_CHANNELS = (4 << 8),
        /// <summary>
        /// The given handle is invalid
        /// </summary>
        XCP_ERROR_INVALID_HANDLE = (5 << 8),
        /// <summary>
        /// A timeout was reached by calling a function synchronously
        /// </summary>
        XCP_ERR_INTERNAL_TIMEOUT = (6 << 8),
        /// <summary>
        /// The queue being referred is empty
        /// </summary>
        XCP_ERR_QUEUE_EMPTY = (7 << 8),
        /// <summary>
        /// The size of the given buffer, is not big enough
        /// </summary>
        XCP_ERR_INSUFFICIENT_BUFFER = (8 << 8),

        // Transport protocol error flags
        //
        /// <summary>
        /// Flag for a specific error within the underlying transport channel 
        /// </summary>
        XCP_ERR_TRANSPORT_CHANNEL = 0x80000000
    }

    [StructLayout(LayoutKind.Auto, Size = 4)]
    public struct XCPResult
    {
        [MarshalAs(UnmanagedType.U4)]
        private uint m_pxcpResult;

        public XCPResult(TXCPResult result)
        {
            m_pxcpResult = (uint)result;
        }

        private bool IsTransportLayerError()
        {
            return (TXCPResult)(m_pxcpResult & (uint)TXCPResult.XCP_ERR_TRANSPORT_CHANNEL) == TXCPResult.XCP_ERR_TRANSPORT_CHANNEL;
        }

        public TXCPResult XCP
        {
            get { return IsTransportLayerError() ? TXCPResult.XCP_ERR_TRANSPORT_CHANNEL : (TXCPResult)(m_pxcpResult & 0x7FFFFFFF); }
        }

        public uint TransportLayer
        {
            get { return (m_pxcpResult & 0x7FFFFFFF); }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TXCPCalibrationPage
    {
        [MarshalAs(UnmanagedType.U1)]
        public byte SegmentNumber;
        [MarshalAs(UnmanagedType.U1)]
        public byte PageNumber;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TXCPProgramFormat
    {
        [MarshalAs(UnmanagedType.U1)]
        public byte CompressionMethod;
        [MarshalAs(UnmanagedType.U1)]
        public byte EncryptionMethod;
        [MarshalAs(UnmanagedType.U1)]
        public byte ProgrammingMehtod;
        [MarshalAs(UnmanagedType.U1)]
        public byte AccessMethod;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TXCPODTEntry
    {
        [MarshalAs(UnmanagedType.U1)]
        public byte BitOffset;
        [MarshalAs(UnmanagedType.U1)]
        public byte DAQSize;
        [MarshalAs(UnmanagedType.U1)]
        public byte DAQAddressExtension;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 DAQAddress;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TXCPDAQListConfig
    {
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 DAQListNumber;
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 EventChannelNumber;
        [MarshalAs(UnmanagedType.U1)]
        public byte TransmissionRate;
        [MarshalAs(UnmanagedType.U1)]
        public byte DAQPriority;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TXCPTransportLayerCAN
    {
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 BroadcastID;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 MasterID;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 SlaveID;
        [MarshalAs(UnmanagedType.U1)]
        public bool IncrementalIdUsed;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TXCPProtocolLayerConfig
    {
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 T1;
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 T2;
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 T3;
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 T4;
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 T5;
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 T6;
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 T7;
        /// <summary>
        /// <remarks>DEPRECATED: Value will automatically adjusted on connect (XCP_Connect)</remarks>
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool MotorolaFormat;
        /// <summary>
        /// <remarks>DEPRECATED: Value will automatically adjusted on connect (XCP_Connect)</remarks>
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public byte AddressGranularity;
    }

    ////////////////////////////////////////////////////////////
    // PCAN-XCP API function declarations
    ////////////////////////////////////////////////////////////

    #region PCAN-XCP Class
    public static class XCPApi
    {
        /// <summary>
        /// Maximum count of asynchronous messages that a queue can retain
        /// </summary>
        public const int XCP_MAX_QUEUE_SIZE = 0x7FFF;
        
        /// <summary>
        /// Maximum length of a CTO packet on CAN
        /// </summary>
        public const int CAN_MAX_LEN = 8;
        /// <summary>
        /// Maximum length of a CTO packet on CAN-FD
        /// </summary>
        public const int CAN_MAX_LEN_FD = 64;

        /// <summary>
        /// Maximum length of a CTO packet on standard CAN
        /// </summary>
        public const int CAN_MAX_CTO = CAN_MAX_LEN;
        /// <summary>
        /// Maximum length of a DTO packet on standard CAN
        /// </summary>
        public const int CAN_MAX_DTO = CAN_MAX_LEN;
        /// <summary>
        /// Maximum length of a CTO packet on standard CAN, when programming
        /// </summary>
        public const int CAN_MAX_CTO_PGM = CAN_MAX_LEN;

        /// <summary>
        /// Allocates an XCP Channel for CAN communication using PCAN-Basic API
        /// </summary>
        /// <param name="XcpChannel">Buufer for the XcpChannel representing this CAN Channel</param>
        /// <param name="Channel">The handle of a PCAN Channel to be initialized</param>
        /// <param name="Btr0Btr1">The speed for the communication (BTR0BTR1 code)</param>
        /// <param name="HwType">NON PLUG&PLAY: The type of hardware and operation mode</param>
        /// <param name="IOPort">NON PLUG&PLAY: The I/O address for the parallel port</param>
        /// <param name="Interupt">NON PLUG&PLAY: Interrupt number of the parallel port</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_InitializeCanChannel")]
        public static extern TXCPResult InitializeCanChannel(
            out TXCPChannel XcpChannel,
            TPCANHandle Channel,
            TPCANBaudrate Btr0Btr1,
            TPCANType HwType,
            UInt32 IOPort,
            UInt16 Interrupt);

        /// <summary>
        /// Allocates an XCP Channel for CAN communication using PCAN-Basic API
        /// </summary>
        /// <param name="XcpChannel">Buufer for the XcpChannel representing this CAN Channel</param>
        /// <param name="Channel">The handle of a PCAN Channel to be initialized</param>
        /// <param name="Btr0Btr1">The speed for the communication (BTR0BTR1 code)</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult InitializeCanChannel(
            out TXCPChannel XcpChannel,
            TPCANHandle Channel,
            TPCANBaudrate Btr0Btr1)
        {
            return InitializeCanChannel(out XcpChannel, Channel, Btr0Btr1, (TPCANType)0, 0, 0);
        }

        /// <summary>
        /// Allocates an XCP Channel for CAN-FD communication using PCAN-Basic API
        /// </summary>
        /// <param name="XcpChannel">Buufer for the XcpChannel representing this CAN Channel</param>
        /// <param name="Channel">The handle of a PCAN Channel to be initialized</param>
        /// <param name="Btr0Btr1">The speed for the communication (FD parameters)</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_InitializeCanChannelFD")]
        public static extern TXCPResult InitializeCanChannelFD(
            out TXCPChannel XcpChannel,
            TPCANHandle Channel,
            TPCANBitrateFD Bitrate);

        /// <summary>
        /// Uninitializes a XCP Channel
        /// </summary>
        /// <param name="XcpChannel">The handle of a Xcp Channel</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_UninitializeChannel")]
        public static extern TXCPResult UninitializeChannel(
            TXCPChannel XcpChannel);

        /// <summary>
        /// Associates an ECU to a XCP Channel to communicate over CAN.
        /// </summary>
        /// <param name="XcpChannel">The handle of a Xcp Channel</param>
        /// <param name="SlaveData">The slave (ECU) data for CAN communication</param>
        /// <param name="Protocol">Protocol layer's configuration data </param>
        /// <param name="XcpHandle">Buffer for the handle representing the session (Channel + Slave)</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_AddSlaveOnCAN")]
        public static extern TXCPResult AddSlaveOnCAN(
            TXCPChannel XcpChannel,
            [MarshalAs(UnmanagedType.Struct)]
		    TXCPTransportLayerCAN SlaveData,
            TXCPProtocolLayerConfig Protocol,
            out TXCPHandle XcpHandle);

        /// <summary>
        /// Remove the assocuiation between an ECU and a XCP Channel.
        /// </summary>
        /// <param name="XcpHandle">The handle of the XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_RemoveSlave")]
        public static extern TXCPResult RemoveSlave(
            TXCPHandle XcpHandle);

        /// <summary>
        /// Retrieves an enqueued CTO or DTO packet from the XCP session represented
        /// by the given XcpHandle.
        /// </summary>
        /// <param name="XcpHandle">The handle of the XCP session</param>
        /// <param name="queueSelect">A TXCPQueue value that identifies the queue from 
        /// where the packet should be extracted.</param>
        /// <param name="dtoBuffer">Buffer for the CTO or DTO packet.</param>
        /// <param name="dtoBufferLength">Length of the packet buffer</param>
        /// <returns></returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_DequeuePacket")]
        public static extern TXCPResult DequeuePacket(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.U4)]
            TXCPQueue queueSelect,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] dtoBuffer,
            UInt16 dtoBufferLength);

        /// <summary>
        /// Resets the CTO/DTO queue(s) of a XCP session represented 
        /// by the given XcpHandle
        /// </summary>
        /// <param name="XcpHandle">The handle of the XCP session</param>
        /// <param name="QueueToClear">Value indicating the queue to be reset</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ResetQueue")]
        public static extern TXCPResult ResetQueue(
            TXCPHandle XcpHandle,
            TXCPQueue QueueToClear);

        //------------------------------
        // Commands
        //------------------------------

        /// <summary>
        /// Command CONNECT: establishes a continuous, logical, point-to-point 
        /// connection with a slave device.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">0: Normal, 1: User defined</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_Connect")]
        public static extern TXCPResult Connect(
            TXCPHandle XcpHandle,
            byte Mode,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command CONNECT: establishes a continuous, logical, point-to-point 
        /// connection with a slave device.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">0: Normal, 1: User defined</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult Connect(
            TXCPHandle XcpHandle,
            byte Mode)
        {
            return Connect(XcpHandle, Mode, null, 0);
        }

        /// <summary>
        /// Command DISCONNECT: brings the slave to the “DISCONNECTED” state.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_Disconnect")]
        public static extern TXCPResult Disconnect(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command DISCONNECT: brings the slave to the “DISCONNECTED” state.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult Disconnect(
            TXCPHandle XcpHandle)
        {
            return Disconnect(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command GET_STATUS: returns all current status information of the slave device.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetStatus")]
        public static extern TXCPResult GetStatus(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_STATUS: returns all current status information of the slave device.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetStatus(
            TXCPHandle XcpHandle)
        {
            return GetStatus(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command SYNCH: used to synchronize command execution after timeout conditions.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_Synchronize")]
        public static extern TXCPResult Synchronize(
             TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command SYNCH: used to synchronize command execution after timeout conditions.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult Synchronize(
             TXCPHandle XcpHandle)
        {
            return Synchronize(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command GET_COMM_MODE_INFO: returns optional information on different 
        /// Communication Modes supported by the slave.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetCommunicationMode")]
        public static extern TXCPResult GetCommunicationMode(
             TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_COMM_MODE_INFO: returns optional information on different 
        /// Communication Modes supported by the slave.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetCommunicationMode(
            TXCPHandle XcpHandle)
        {
            return GetCommunicationMode(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command GET_ID: used for automatic session configuration and for 
        /// slave device identification.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Type">The ideintification type (0..4)</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetIdentification")]
        public static extern TXCPResult GetIdentification(
            TXCPHandle XcpHandle,
            byte Type,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
	        byte[] ctoBuffer,
            byte ctoBufferLength);

        /// <summary>
        /// Command GET_ID: used for automatic session configuration and for 
        /// slave device identification.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Type">The ideintification type (0..4)</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetIdentification(
            TXCPHandle XcpHandle,
            byte Type)
        {
            return GetIdentification(XcpHandle, Type, null, 0);
        }

        /// <summary>
        /// Command SET_REQUEST: request to save to non-volatile memory.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Request mode especification</param>
        /// <param name="SessionId">Session Configuration identification</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_SetRequest")]
        public static extern TXCPResult SetRequest(
            TXCPHandle XcpHandle,
            byte Mode,
            UInt16 SessionId,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command SET_REQUEST: request to save to non-volatile memory.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Request mode especification</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult SetRequest(
            TXCPHandle XcpHandle,
            byte Mode,
            UInt16 SessionId)
        {
            return SetRequest(XcpHandle, Mode, SessionId, null, 0);
        }

        /// <summary>
        /// Command GET_SEED: Get seed for unlocking a protected resource.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Seed mode. 0: First part, 1: remaining part</param>
        /// <param name="Resource">Resource id, when Mode is 0. Otherwise don't care</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetSeed")]
        public static extern TXCPResult GetSeed(
            TXCPHandle XcpHandle,
            byte Mode,
            byte Resource,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_SEED: Get seed for unlocking a protected resource.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Seed mode. 0: First part, 1: remaining part</param>
        /// <param name="Resource">Resource id, when Mode is 0. Otherwise don't care</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetSeed(
            TXCPHandle XcpHandle,
            byte Mode,
            byte Resource)
        {
            return GetSeed(XcpHandle, Mode, Resource, null, 0);
        }

        /// <summary>
        /// Command UNLOCK: unlocks the slave device’s security protection using a 
        /// ´key´ computed from the ´seed´ obtained by a previous GET_SEED sequence.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="KeyLength">Indicates the (remaining) number of key bytes</param>
        /// <param name="Key">Buffer for the Key's data bytes</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_Unlock")]
        public static extern TXCPResult Unlock(
            TXCPHandle XcpHandle,
            byte KeyLength,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
            byte[] Key,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command UNLOCK: unlocks the slave device’s security protection using a 
        /// ´key´ computed from the ´seed´ obtained by a previous GET_SEED sequence.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Key">Buffer for the Key's data bytes</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult Unlock(
            TXCPHandle XcpHandle,
            byte[] Key)
        {
            return Unlock(XcpHandle, (byte)Key.Length, Key, null, 0);
        }

        /// <summary>
        /// Command SET_MTA: initializes a pointer (32Bit address + 8Bit extension) 
        /// for following memory transfer commands: BUILD_CHECKSUM, UPLOAD, DOWNLOAD,
        /// DOWNLOAD_NEXT, DOWNLOAD_MAX, MODIFY_BITS, PROGRAM_CLEAR, PROGRAM, 
        /// PROGRAM_NEXT and PROGRAM_MAX.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="AddrExtension">Address extension</param>
        /// <param name="Addr">Address</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_SetMemoryTransferAddress")]
        public static extern TXCPResult SetMemoryTransferAddress(
            TXCPHandle XcpHandle,
            byte AddrExtension,
            UInt32 Addr,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command SET_MTA: initializes a pointer (32Bit address + 8Bit extension) 
        /// for following memory transfer commands: BUILD_CHECKSUM, UPLOAD, DOWNLOAD,
        /// DOWNLOAD_NEXT, DOWNLOAD_MAX, MODIFY_BITS, PROGRAM_CLEAR, PROGRAM, 
        /// PROGRAM_NEXT and PROGRAM_MAX.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="AddrExtension">Address extension</param>
        /// <param name="Addr">Address</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult SetMemoryTransferAddress(
            TXCPHandle XcpHandle,
            byte AddrExtension,
            UInt32 Addr)
        {
            return SetMemoryTransferAddress(XcpHandle, AddrExtension, Addr, null, 0);
        }

        /// <summary>
        /// Command UPLOAD: Upload a data block from slave to master, starting at the 
        /// current MTA. The MTA will be post-incremented by the given number of data elements.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">The amount of elements to retrieve</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_Upload")]
        public static extern TXCPResult Upload(
            TXCPHandle XcpHandle,
            byte NumberOfElements,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command UPLOAD: Upload a data block from slave to master, starting at the 
        /// current MTA. The MTA will be post-incremented by the given number of data elements.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">The amount of elements to retrieve</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult Upload(
            TXCPHandle XcpHandle,
            byte NumberOfElements)
        {
            return Upload(XcpHandle, NumberOfElements, null, 0);
        }

        /// <summary>
        /// Command SHORT_UPLOAD: Upload a data block, of a maximum size of [1..MAX_CTO/AG], 
        /// from slave to master. 
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">The amount of elements to retrieve</param>
        /// <param name="AddrExtension">Address extension to set the MTA pointer</param>
        /// <param name="Addr">Address to set the MTA pointer</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ShortUpload")]
        public static extern TXCPResult ShortUpload(
            TXCPHandle XcpHandle,
            byte NumberOfElements,
            byte AddrExtension,
            UInt32 Addr,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command SHORT_UPLOAD: Upload a data block, of a maximum size of [1..MAX_CTO/AG], 
        /// from slave to master. 
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">The amount of elements to retrieve</param>
        /// <param name="AddrExtension">Address extension to set the MTA pointer</param>
        /// <param name="Addr">Address to set the MTA pointer</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult ShortUpload(
            TXCPHandle XcpHandle,
            byte NumberOfElements,
            byte AddrExtension,
            UInt32 Addr)
        {
            return ShortUpload(XcpHandle, NumberOfElements, AddrExtension, Addr, null, 0);
        }

        /// <summary>
        /// Command BUILD_CHECKSUM: Build a checksum over a memory range starting at the MTA.
        /// The MTA will be post-incremented by the block size.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="BlockSize">The number of elements used for the checksum</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_BuildChecksum")]
        public static extern TXCPResult BuildChecksum(
            TXCPHandle XcpHandle,
            UInt32 BlockSize,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command BUILD_CHECKSUM: Build a checksum over a memory range starting at the MTA.
        /// The MTA will be post-incremented by the block size.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="BlockSize">The number of elements used for the checksum</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult BuildChecksum(
            TXCPHandle XcpHandle,
            UInt32 BlockSize)
        {
            return BuildChecksum(XcpHandle, BlockSize, null, 0);
        }

        /// <summary>
        /// Command TRANSPORT_LAYER_CMD: sends a specific Transport Layer command.
        /// For CAN: Works only in blocking mode (ctoBuffer cannot be NULL)
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="SubCommand">Transport layer specific command</param>
        /// <param name="Paarameters">Command parameters buffer</param>
        /// <param name="PaarametersLength">Length of the Command parameters buffer</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_TransportLayerCommand")]
        public static extern TXCPResult TransportLayerCommand(
            TXCPHandle XcpHandle,
            byte SubCommand,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] Parameters,
            byte ParametersLength,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command TRANSPORT_LAYER_CMD: sends a specific Transport Layer command.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="SubCommand">Transport layer specific command</param>
        /// <param name="Paarameters">Command parameters buffer</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult TransportLayerCommand(
            TXCPHandle XcpHandle,
            byte SubCommand,
            byte[] Parameters)
        {
            return TransportLayerCommand(XcpHandle, SubCommand, Parameters, (byte)Parameters.Length, null, 0);
        }

        /// <summary>
        /// Command USER_CMD: sends a user specific command.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="SubCommand">User defined command</param>
        /// <param name="Parameters">Command parameters buffer</param>
        /// <param name="ParametersLength">Length of the Command parameters buffer</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_UserCommand")]
        public static extern TXCPResult UserCommand(
            TXCPHandle XcpHandle,
            byte SubCommand,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] Parameters,
            byte ParametersLength,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command USER_CMD: sends a user specific command.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="SubCommand">User defined command</param>
        /// <param name="Parameters">Command parameters buffer</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult UserCommand(
            TXCPHandle XcpHandle,
            byte SubCommand,
            byte[] Parameters)
        {
            return UserCommand(XcpHandle, SubCommand, Parameters, (byte)Parameters.Length, null, 0);
        }

        /// <summary>
        /// Command DOWNLOAD: Download a data block from master to slave, starting at the 
        /// current MTA. The MTA will be post-incremented by the number of data elements.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">The amount of elements to be transfered</param>
        /// <param name="Data">Data buffer with the elements to transfer</param>
        /// <param name="DataLength">Length of the data buffer</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_Download")]
        public static extern TXCPResult Download(
            TXCPHandle XcpHandle,
            byte NumberOfElements,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] Data,
            byte DataLength,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command DOWNLOAD: Download a data block from master to slave, starting at the 
        /// current MTA. The MTA will be post-incremented by the number of data elements.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">The amount of elements to be transfered</param>
        /// <param name="Data">Data buffer with the elements to transfer</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult Download(
            TXCPHandle XcpHandle,
            byte NumberOfElements,
            byte[] Data)
        {
            return Download(XcpHandle, NumberOfElements, Data, (byte)Data.Length, null, 0);
        }

        /// <summary>
        /// Command DOWNLOAD_NEXT: Download consecutive data blocks from master to 
        /// slave (Block Mode), starting at the current MTA. The MTA will be post-incremented 
        /// by the number of data elements.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">The amount of elements to be transfered</param>
        /// <param name="Data">Data buffer with the elements to transfer</param>
        /// <param name="DataLength">Length of the data buffer</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_DownloadNext")]
        public static extern TXCPResult DownloadNext(
            TXCPHandle XcpHandle,
            byte NumberOfElements,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] Data,
            byte DataLength,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command DOWNLOAD_NEXT: Download consecutive data blocks from master to 
        /// slave (Block Mode), starting at the current MTA. The MTA will be post-incremented 
        /// by the number of data elements.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">The amount of elements to be transfered</param>
        /// <param name="Data">Data buffer with the elements to transfer</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult DownloadNext(
            TXCPHandle XcpHandle,
            byte NumberOfElements,
            byte[] Data)
        {
            return DownloadNext(XcpHandle, NumberOfElements, Data, (byte)Data.Length, null, 0);
        }

        /// <summary>
        /// Command DOWNLOAD_MAX: Download a data block with fixed size (MAX_CTO/AG-1) from 
        /// master to slave, starting at the current MTA. The MTA will be post-incremented 
        /// by MAX_CTO/AG-1.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Data">Data buffer with the elements to transfer</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_DownloadMax")]
        public static extern TXCPResult DownloadMax(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray)]
            byte[] Data,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command DOWNLOAD_MAX: Download a data block with fixed size (MAX_CTO/AG-1) from 
        /// master to slave, starting at the current MTA. The MTA will be post-incremented 
        /// by MAX_CTO/AG-1.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Data">Data buffer with the elements to transfer</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult DownloadMax(
            TXCPHandle XcpHandle,
            byte[] Data)
        {
            return DownloadMax(XcpHandle, Data, null, 0);
        }

        /// <summary>
        /// Command MODIFY_BITS: modifies the bits of the 32 Bit memory 
        /// location referred by the MTA.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ShiftValue">Shift Value</param>
        /// <param name="ANDMask">AND Mask (MA)</param>
        /// <param name="XORMask">XOR Mask (MX)</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ModifyBits")]
        public static extern TXCPResult ModifyBits(
            TXCPHandle XcpHandle,
            byte ShiftValue,
            UInt16 ANDMask,
            UInt16 XORMask,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command MODIFY_BITS: modifies the bits of the 32 Bit memory 
        /// location referred by the MTA.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ShiftValue">Shift Value</param>
        /// <param name="ANDMask">AND Mask (MA)</param>
        /// <param name="XORMask">XOR Mask (MX)</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult ModifyBits(
            TXCPHandle XcpHandle,
            byte ShiftValue,
            UInt16 ANDMask,
            UInt16 XORMask)
        {
            return ModifyBits(XcpHandle, ShiftValue, ANDMask, XORMask, null, 0);
        }

        /// <summary>
        /// Command SET_CAL_PAGE: sets the access mode for a calibration data segment.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Access mode</param>
        /// <param name="Page">Represents a calibration data page (segment and page number)</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_SetCalibrationPage")]
        public static extern TXCPResult SetCalibrationPage(
            TXCPHandle XcpHandle,
            byte Mode,
            TXCPCalibrationPage Page,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command SET_CAL_PAGE: sets the access mode for a calibration data segment.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Access mode</param>
        /// <param name="Page">Represents a calibration data page (segment and page number)</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult SetCalibrationPage(
            TXCPHandle XcpHandle,
            byte Mode,
            TXCPCalibrationPage Page)
        {
            return SetCalibrationPage(XcpHandle, Mode, Page, null, 0);
        }

        /// <summary>
        /// Command GET_CAL_PAGE: returns the logical number for the calibration data page 
        /// that is currently activated for the specified access mode and data segment.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Access mode</param>
        /// <param name="DataSegmentNumber">Logical data segment number</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetCalibrationPage")]
        public static extern TXCPResult GetCalibrationPage(
            TXCPHandle XcpHandle,
            byte Mode,
            byte DataSegmentNumber,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_CAL_PAGE: returns the logical number for the calibration data page 
        /// that is currently activated for the specified access mode and data segment.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Access mode</param>
        /// <param name="DataSegmentNumber">Logical data segment number</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetCalibrationPage(
            TXCPHandle XcpHandle,
            byte Mode,
            byte DataSegmentNumber)
        {
            return GetCalibrationPage(XcpHandle, Mode, DataSegmentNumber, null, 0);
        }

        /// <summary>
        /// Command GET_PAG_PROCESSOR_INFO: returns general information on paging processor.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetPagingProcessorInformation")]
        public static extern TXCPResult GetPagingProcessorInformation(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_PAG_PROCESSOR_INFO: returns general information on paging processor.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetPagingProcessorInformation(
            TXCPHandle XcpHandle)
        {
            return GetPagingProcessorInformation(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command GET_SEGMENT_INFO: returns information on a specific SEGMENT.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Information mode (0..2)</param>
        /// <param name="SegmentNumber">Segment number, [0,1,..MAX_SEGMENT-1]</param>
        /// <param name="SegmentInfo">Segment information according to the 'information mode'</param>
        /// <param name="MappingIndex">Identifier for address mapping range</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetSegmentInformation")]
        public static extern TXCPResult GetSegmentInformation(
            TXCPHandle XcpHandle,
            byte Mode,
            byte SegmentNumber,
            byte SegmentInfo,
            byte MappingIndex,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_SEGMENT_INFO: returns information on a specific SEGMENT.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Information mode (0..2)</param>
        /// <param name="SegmentNumber">Segment number, [0,1,..MAX_SEGMENT-1]</param>
        /// <param name="SegmentInfo">Segment information according to the 'information mode'</param>
        /// <param name="MappingIndex">Identifier for address mapping range</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetSegmentInformation(
            TXCPHandle XcpHandle,
            byte Mode,
            byte SegmentNumber,
            byte SegmentInfo,
            byte MappingIndex)
        {
            return GetSegmentInformation(XcpHandle, Mode, SegmentNumber, SegmentInfo, MappingIndex, null, 0);
        }

        /// <summary>
        /// Command GET_PAGE_INFO: returns information on a specific PAGE.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Page">Represents a calibration data page (segment and page number)</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetPageInformation")]
        public static extern TXCPResult GetPageInformation(
            TXCPHandle XcpHandle,
            TXCPCalibrationPage Page,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_PAGE_INFO: returns information on a specific PAGE.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Page">Represents a calibration data page (segment and page number)</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetPageInformation(
            TXCPHandle XcpHandle,
            TXCPCalibrationPage Page)
        {
            return GetPageInformation(XcpHandle, Page, null, 0);
        }

        /// <summary>
        /// Command SET_SEGMENT_MODE: set the mode for a SEGMENT
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Segment mode (0,1)</param>
        /// <param name="segmentNumber">Segment number, [0,1,..MAX_SEGMENT-1]</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_SetSegmentMode")]
        public static extern TXCPResult SetSegmentMode(
            TXCPHandle XcpHandle,
            byte Mode,
            byte SegmentNumber,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command SET_SEGMENT_MODE: set the mode for a SEGMENT
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Segment mode (0,1)</param>
        /// <param name="segmentNumber">Segment number, [0,1,..MAX_SEGMENT-1]</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult SetSegmentMode(
            TXCPHandle XcpHandle,
            byte Mode,
            byte SegmentNumber)
        {
            return SetSegmentMode(XcpHandle, Mode, SegmentNumber, null, 0);
        }

        /// <summary>
        /// Command GET_SEGMENT_MODE: get the mode for a SEGMENT
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="SegmentNumber">Segment number, [0,1,..MAX_SEGMENT-1]</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetSegmentMode")]
        public static extern TXCPResult GetSegmentMode(
            TXCPHandle XcpHandle,
            byte SegmentNumber,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_SEGMENT_MODE: get the mode for a SEGMENT
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="SegmentNumber">Segment number, [0,1,..MAX_SEGMENT-1]</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetSegmentMode(
            TXCPHandle XcpHandle,
            byte SegmentNumber)
        {
            return GetSegmentMode(XcpHandle, SegmentNumber, null, 0);
        }

        /// <summary>
        /// Command COPY_CAL_PAGE: forces the slave to copy one calibration page to another.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Source">Represents the source calibration data page</param>
        /// <param name="Destination">Represents the destination calibration data page</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_CopyCalibrationPage")]
        public static extern TXCPResult CopyCalibrationPage(
            TXCPHandle XcpHandle,
            TXCPCalibrationPage Source,
            TXCPCalibrationPage Destination,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command COPY_CAL_PAGE: forces the slave to copy one calibration page to another.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Source">Represents the source calibration data page</param>
        /// <param name="Destination">Represents the destination calibration data page</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult CopyCalibrationPage(
            TXCPHandle XcpHandle,
            TXCPCalibrationPage Source,
            TXCPCalibrationPage Destination)
        {
            return CopyCalibrationPage(XcpHandle, Source, Destination, null, 0);
        }

        /// <summary>
        /// Command SET_DAQ_PTR: initializes the DAQ list pointer for a subsequent 
        /// operation with XCP_WriteDAQListEntry (WRITE_DAQ) or XCP_ReadDAQListEntry (READ_DAQ).
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqListNumber">DAQ List number, [0,1,..MAX_DAQ-1]</param>
        /// <param name="OdtNumber">Relative ODT number within this DAQ list</param>
        /// <param name="OdtEntry">Relative ODT entry number within this ODT</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_SetDAQListPointer")]
        public static extern TXCPResult SetDAQListPointer(
            TXCPHandle XcpHandle,
            UInt16 DaqListNumber,
            byte OdtNumber,
            byte OdtEntry,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command SET_DAQ_PTR: initializes the DAQ list pointer for a subsequent 
        /// operation with XCP_WriteDAQListEntry (WRITE_DAQ) or XCP_ReadDAQListEntry (READ_DAQ).
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqListNumber">DAQ List number, [0,1,..MAX_DAQ-1]</param>
        /// <param name="OdtNumber">Relative ODT number within this DAQ list</param>
        /// <param name="OdtEntry">Relative ODT entry number within this ODT</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult SetDAQListPointer(
            TXCPHandle XcpHandle,
            UInt16 DaqListNumber,
            byte OdtNumber,
            byte OdtEntry)
        {
            return SetDAQListPointer(XcpHandle, DaqListNumber, OdtNumber, OdtEntry, null, 0);
        }

        /// <summary>
        /// Command WRITE_DAQ: writes one ODT entry to a DAQ list defined by 
        /// the DAQ list pointer.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Entry">Represents an ODT entry for a DAQ List</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_WriteDAQListEntry")]
        public static extern TXCPResult WriteDAQListEntry(
            TXCPHandle XcpHandle,
            TXCPODTEntry Entry,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command WRITE_DAQ: writes one ODT entry to a DAQ list defined by 
        /// the DAQ list pointer.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Entry">Represents an ODT entry for a DAQ List</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult WriteDAQListEntry(
            TXCPHandle XcpHandle,
            TXCPODTEntry Entry)
        {
            return WriteDAQListEntry(XcpHandle, Entry, null, 0);
        }

        /// <summary>
        /// Command SET_DAQ_LIST_MODE: sets the mode for DAQ list.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">DAQ list mode bit</param>
        /// <param name="Configuration">Represents the configuration of a DAQ List</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_SetDAQListMode")]
        public static extern TXCPResult SetDAQListMode(
            TXCPHandle XcpHandle,
            byte Mode,
            TXCPDAQListConfig Configuration,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command SET_DAQ_LIST_MODE: sets the mode for DAQ list.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">DAQ list mode bit</param>
        /// <param name="Configuration">Represents the configuration of a DAQ List</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult SetDAQListMode(
            TXCPHandle XcpHandle,
            byte Mode,
            TXCPDAQListConfig Configuration)
        {
            return SetDAQListMode(XcpHandle, Mode, Configuration, null, 0);
        }

        /// <summary>
        /// Command START_STOP_DAQ_LIST: Starts, stops, or selects a DAQ list.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">0: Stop, 1: Start, 2: Select</param>
        /// <param name="DaqListNumber">DAQ list number, [0,1,..MAX_DAQ-1]</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_StartStopDAQList")]
        public static extern TXCPResult StartStopDAQList(
            TXCPHandle XcpHandle,
            byte Mode,
            UInt16 DaqListNumber,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command START_STOP_DAQ_LIST: Starts, stops, or selects a DAQ list.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">0: Stop, 1: Start, 2: Select</param>
        /// <param name="DaqListNumber">DAQ list number, [0,1,..MAX_DAQ-1]</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult StartStopDAQList(
            TXCPHandle XcpHandle,
            byte Mode,
            UInt16 DaqListNumber)
        {
            return StartStopDAQList(XcpHandle, Mode, DaqListNumber, null, 0);
        }

        /// <summary>
        /// Command START_STOP_SYNCH: Starts or stops several DAQ lists, synchronously.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">0: Stop all, 1: Start selected, 2: Stop selected</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_StartStopSynchronizedDAQList")]
        public static extern TXCPResult StartStopSynchronizedDAQList(
            TXCPHandle XcpHandle,
            byte Mode,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command START_STOP_SYNCH: Starts, stops, or selects several DAQ lists, synchronously.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">0: Stop all, 1: Start selected, 2: Stop selected</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult StartStopSynchronizedDAQList(
            TXCPHandle XcpHandle,
            byte Mode)
        {
            return StartStopSynchronizedDAQList(XcpHandle, Mode, null, 0);
        }

        /// <summary>
        /// Command WRITE_DAQ_MULTIPLE: writes consecutive ODT entries to a DAQ list 
        /// defined by the DAQ list pointer.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">Amount of ODT entries to be written</param>
        /// <param name="Elements">Buffer containing the ODT entries for a DAQ List</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_WriteDAQListEntries")]
        public static extern TXCPResult WriteDAQListEntries(
            TXCPHandle XcpHandle,
            byte NumberOfElements,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
            TXCPODTEntry[] Elements,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command WRITE_DAQ_MULTIPLE: writes consecutive ODT entries to a DAQ list 
        /// defined by the DAQ list pointer.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Elements">Buffer containing the ODT entries for a DAQ List</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult WriteDAQListEntries(
            TXCPHandle XcpHandle,
            TXCPODTEntry[] Elements)
        {
            return WriteDAQListEntries(XcpHandle, (byte)Elements.Length, Elements, null, 0);
        }

        /// <summary>
        /// Command READ_DAQ: Reads one ODT entry of a DAQ list defined by the DAQ 
        /// list pointer.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ReadDAQListEntry")]
        public static extern TXCPResult ReadDAQListEntry(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command READ_DAQ: Reads one ODT entry of a DAQ list defined by the DAQ 
        /// list pointer.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult ReadDAQListEntry(
            TXCPHandle XcpHandle)
        {
            return ReadDAQListEntry(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command GET_DAQ_CLOCK: Gets the DAQ clock from a slave.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetDAQClock")]
        public static extern TXCPResult GetDAQClock(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_DAQ_CLOCK: Gets the DAQ clock from a slave.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetDAQClock(
            TXCPHandle XcpHandle)
        {
            return GetDAQClock(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command GET_DAQ_PROCESSOR_INFO: returns general information on DAQ processor.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetDAQProcessorInformation")]
        public static extern TXCPResult GetDAQProcessorInformation(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_DAQ_PROCESSOR_INFO: returns general information on DAQ processor.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetDAQProcessorInformation(
            TXCPHandle XcpHandle)
        {
            return GetDAQProcessorInformation(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command GET_DAQ_RESOLUTION_INFO: returns information on DAQ 
        /// processing resolution.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetDAQResolutionInformation")]
        public static extern TXCPResult GetDAQResolutionInformation(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_DAQ_RESOLUTION_INFO: returns information on DAQ 
        /// processing resolution.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetDAQResolutionInformation(
            TXCPHandle XcpHandle)
        {
            return GetDAQResolutionInformation(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command GET_DAQ_LIST_MODE: returns information on the current mode 
        /// of a specified DAQ list.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqListNumber">DAQ list number, [0,1,..MAX_DAQ-1]</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetDAQListMode")]
        public static extern TXCPResult GetDAQListMode(
            TXCPHandle XcpHandle,
            UInt16 DaqListNumber,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_DAQ_LIST_MODE: returns information on the current mode 
        /// of a specified DAQ list.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqListNumber">DAQ list number, [0,1,..MAX_DAQ-1]</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetDAQListMode(
            TXCPHandle XcpHandle,
            UInt16 DaqListNumber)
        {
            return GetDAQListMode(XcpHandle, DaqListNumber, null, 0);
        }

        /// <summary>
        /// Command GET_DAQ_EVENT_INFO: returns information on a specific event channel.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="EventChannelNumber">Event channel number, [0,1,..MAX_EVENT_CHANNEL-1]</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetEventChannelInformation")]
        public static extern TXCPResult GetEventChannelInformation(
            TXCPHandle XcpHandle,
            UInt16 EventChannelNumber,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_DAQ_EVENT_INFO: returns information on a specific event channel.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="EventChannelNumber">Event channel number, [0,1,..MAX_EVENT_CHANNEL-1]</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetEventChannelInformation(
            TXCPHandle XcpHandle,
            UInt16 EventChannelNumber)
        {
            return GetEventChannelInformation(XcpHandle, EventChannelNumber, null, 0);
        }

        /// <summary>
        /// Command CLEAR_DAQ_LIST: clears an specific DAQ list configuration.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqListNumber">DAQ list number, [0,1..MAX_DAQ-1]</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ClearDAQList")]
        public static extern TXCPResult ClearDAQList(
            TXCPHandle XcpHandle,
            UInt16 DaqListNumber,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command CLEAR_DAQ_LIST: clears an specific DAQ list configuration.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqListNumber">DAQ list number, [0,1..MAX_DAQ-1]</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult ClearDAQList(
            TXCPHandle XcpHandle,
            UInt16 DaqListNumber)
        {
            return ClearDAQList(XcpHandle, DaqListNumber, null, 0);
        }

        /// <summary>
        /// Command GET_DAQ_LIST_INFO: returns information on a specific DAQ list.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqListNumber">DAQ list number, [0,1..MAX_DAQ-1]</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetDAQListInformation")]
        public static extern TXCPResult GetDAQListInformation(
            TXCPHandle XcpHandle,
            UInt16 DaqListNumber,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_DAQ_LIST_INFO: returns information on a specific DAQ list.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqListNumber">DAQ list number, [0,1..MAX_DAQ-1]</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetDAQListInformation(
            TXCPHandle XcpHandle,
            UInt16 DaqListNumber)
        {
            return GetDAQListInformation(XcpHandle, DaqListNumber, null, 0);
        }

        /// <summary>
        /// Command FREE_DAQ: clears all DAQ lists and frees all dynamically allocated 
        /// DAQ lists, ODTs and ODT entries.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_FreeDAQLists")]
        public static extern TXCPResult FreeDAQLists(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command FREE_DAQ: clears all DAQ lists and frees all dynamically allocated 
        /// DAQ lists, ODTs and ODT entries.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult FreeDAQLists(
            TXCPHandle XcpHandle)
        {
            return FreeDAQLists(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command ALLOC_DAQ: allocates a number of DAQ lists for a XCP slave device.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqCount">Number of DAQ lists to be allocated</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_AllocateDAQLists")]
        public static extern TXCPResult AllocateDAQLists(
            TXCPHandle XcpHandle,
            UInt16 DaqCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command ALLOC_DAQ: allocates a number of DAQ lists for a XCP slave device.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqCount">Number of DAQ lists to be allocated</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult AllocateDAQLists(
            TXCPHandle XcpHandle,
            UInt16 DaqCount)
        {
            return AllocateDAQLists(XcpHandle, DaqCount, null, 0);
        }

        /// <summary>
        /// Command ALLOC_ODT: allocates a number of ODTs and assigns them to the 
        /// specified DAQ list.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqListNumber">DAQ list number, [MIN_DAQ, MIN_DAQ+1,..MIN_DAQ+DAQ_COUNT-1]</param>
        /// <param name="OdtCount">Number of ODTs to be assigned to a DAQ list</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_AllocateODT")]
        public static extern TXCPResult AllocateODT(
            TXCPHandle XcpHandle,
            UInt16 DaqListNumber,
            byte OdtCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command ALLOC_ODT: allocates a number of ODTs and assigns them to the 
        /// specified DAQ list.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqListNumber">DAQ list number, [MIN_DAQ, MIN_DAQ+1,..MIN_DAQ+DAQ_COUNT-1]</param>
        /// <param name="OdtCount">Number of ODTs to be assigned to a DAQ list</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult AllocateODT(
            TXCPHandle XcpHandle,
            UInt16 DaqListNumber,
            byte OdtCount)
        {
            return AllocateODT(XcpHandle, DaqListNumber, OdtCount, null, 0);
        }

        /// <summary>
        /// Command ALLOC_ODT_ENTRY: allocates a number of ODT entries and assigns them to 
        /// the specific ODT in this specific DAQ list.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqListNumber">DAQ list number, [MIN_DAQ, MIN_DAQ+1,..MIN_DAQ+DAQ_COUNT-1]</param>
        /// <param name="OdtNumber">Relative ODT number, [0,1,..ODT_COUNT(DAQ list)-1]</param>
        /// <param name="EntriesCount">Number of ODT entries to be assigned to ODT</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_AllocateODTEntry")]
        public static extern TXCPResult AllocateODTEntry(
            TXCPHandle XcpHandle,
            UInt16 DaqListNumber,
            byte OdtNumber,
            byte EntriesCount,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command ALLOC_ODT_ENTRY: allocates a number of ODT entries and assigns them to 
        /// the specific ODT in this specific DAQ list.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="DaqListNumber">DAQ list number, [MIN_DAQ, MIN_DAQ+1,..MIN_DAQ+DAQ_COUNT-1]</param>
        /// <param name="OdtNumber">Relative ODT number, [0,1,..ODT_COUNT(DAQ list)-1]</param>
        /// <param name="EntriesCount">Number of ODT entries to be assigned to ODT</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult AllocateODTEntry(
            TXCPHandle XcpHandle,
            UInt16 DaqListNumber,
            byte OdtNumber,
            byte EntriesCount)
        {
            return AllocateODTEntry(XcpHandle, DaqListNumber, OdtNumber, EntriesCount, null, 0);
        }

        /// <summary>
        /// Command PROGRAM_START: indicates the begin of a non-volatile memory programming
        /// sequence.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ProgramStart")]
        public static extern TXCPResult ProgramStart(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command PROGRAM_START: indicates the begin of a non-volatile memory programming
        /// sequence.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult ProgramStart(
            TXCPHandle XcpHandle)
        {
            return ProgramStart(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command PROGRAM_CLEAR: clears a part of non-volatile memory prior to reprogramming.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Access mode, 0: absolute access, 1: functional access</param>
        /// <param name="ClearRange">Memory range to clear according to the access mode</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ProgramClear")]
        public static extern TXCPResult ProgramClear(
            TXCPHandle XcpHandle,
            byte Mode,
            UInt32 ClearRange,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command PROGRAM_CLEAR: clears a part of non-volatile memory prior to reprogramming.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Access mode, 0: absolute access, 1: functional access</param>
        /// <param name="ClearRange">Memory range to clear according to the access mode</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult ProgramClear(
            TXCPHandle XcpHandle,
            byte Mode,
            UInt32 ClearRange)
        {
            return ProgramClear(XcpHandle, Mode, ClearRange, null, 0);
        }

        /// <summary>
        /// Command PROGRAM: Programs a non-volatile memory segment inside the slave.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">Number of data elements to program, [1..(MAX_CTO_PGM-2)/AG]</param>
        /// <param name="Data">Buffer with the data elements to program</param>
        /// <param name="DataLength">Length of the data buffer</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_Program")]
        public static extern TXCPResult Program(
            TXCPHandle XcpHandle,
            byte NumberOfElements,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
	        byte[] Data,
            byte DataLength,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command PROGRAM: Programs a non-volatile memory segment inside the slave.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">Number of data elements to program, [1..(MAX_CTO_PGM-2)/AG]</param>
        /// <param name="Data">Buffer with the data elements to program</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult Program(
            TXCPHandle XcpHandle,
            byte NumberOfElements,
            byte[] Data)
        {
            return Program(XcpHandle, NumberOfElements, Data, (byte)Data.Length, null, 0);
        }

        /// <summary>
        /// Command PROGRAM_RESET: Indicates the end of a non-volatile memory programming sequence.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ProgramReset")]
        public static extern TXCPResult ProgramReset(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command PROGRAM_RESET: Indicates the end of a non-volatile memory programming sequence.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult ProgramReset(
            TXCPHandle XcpHandle)
        {
            return ProgramReset(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command GET_PGM_PROCESSOR_INFO: returns general information on programming processor.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetPGMProcessorInformation")]
        public static extern TXCPResult GetPGMProcessorInformation(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_PGM_PROCESSOR_INFO: returns general information on programming processor.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetPGMProcessorInformation(
            TXCPHandle XcpHandle)
        {
            return GetPGMProcessorInformation(XcpHandle, null, 0);
        }

        /// <summary>
        /// Command GET_SECTOR_INFO: returns information on a specific SECTOR.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Get mode, 0: start address, 1: length, 2: name length</param>
        /// <param name="SectorNumber">Sector number, [0,1,..MAX_SECTOR-1]</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetSectorInformation")]
        public static extern TXCPResult GetSectorInformation(
            TXCPHandle XcpHandle,
            byte Mode,
            byte SectorNumber,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command GET_SECTOR_INFO: returns information on a specific SECTOR.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Get mode, 0: start address, 1: length, 2: name length</param>
        /// <param name="SectorNumber">Sector number, [0,1,..MAX_SECTOR-1]</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult GetSectorInformation(
            TXCPHandle XcpHandle,
            byte Mode,
            byte SectorNumber)
        {
            return GetSectorInformation(XcpHandle, Mode, SectorNumber, null, 0);
        }

        /// <summary>
        /// Command PROGRAM_PREPARE: indicates the begin of a code download as a precondition
        /// for non-volatile memory programming.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="CodeSize">Size of the code that will be downloaded</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ProgramPrepare")]
        public static extern TXCPResult ProgramPrepare(
            TXCPHandle XcpHandle,
            UInt16 CodeSize,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command PROGRAM_PREPARE: indicates the begin of a code download as a precondition
        /// for non-volatile memory programming.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="CodeSize">Size of the code that will be downloaded</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult ProgramPrepare(
            TXCPHandle XcpHandle,
            UInt16 CodeSize)
        {
            return ProgramPrepare(XcpHandle, CodeSize, null, 0);
        }

        /// <summary>
        /// Command PROGRAM_FORMAT: describes the format of following, uninterrupted data
        /// transfer before programming.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Format">Represents the data format to be used</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ProgramFormat")]
        public static extern TXCPResult ProgramFormat(
            TXCPHandle XcpHandle,
            TXCPProgramFormat Format,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command PROGRAM_FORMAT: describes the format of following, uninterrupted data
        /// transfer before programming.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Format">Represents the data format to be used</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult ProgramFormat(
            TXCPHandle XcpHandle,
            TXCPProgramFormat Format)
        {
            return ProgramFormat(XcpHandle, Format, null, 0);
        }

        /// <summary>
        /// Command PROGRAM_NEXT: transmits consecutive data bytes for the PROGRAM command in
        /// block transfer mode.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">The amount of elements to be programed</param>
        /// <param name="Data">Data buffer with the elements to program</param>
        /// <param name="DataLength">Length of the data buffer</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ProgramNext")]
        public static extern TXCPResult ProgramNext(
            TXCPHandle XcpHandle,
            byte NumberOfElements,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
	        byte[] Data,
            byte DataLength,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command PROGRAM_NEXT: transmits consecutive data bytes for the PROGRAM command in
        /// block transfer mode.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="NumberOfElements">The amount of elements to be programed</param>
        /// <param name="Data">Data buffer with the elements to program</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult ProgramNext(
            TXCPHandle XcpHandle,
            byte NumberOfElements,
            byte[] Data)
        {
            return ProgramNext(XcpHandle, NumberOfElements, Data, (byte)Data.Length, null, 0);
        }

        /// <summary>
        /// Command PROGRAM_MAX: Programs a (fixed size) amount of elements into a 
        /// non-volatile memory segment, starting at the MTA. The MTA will be 
        /// postincremented by MAX_CTO_PGM-1.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Elements">Data buffer with the fixed length of MAX_CTO_PGM-1</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ProgramMax")]
        public static extern TXCPResult ProgramMax(
            TXCPHandle XcpHandle,
            [MarshalAs(UnmanagedType.LPArray)]
            byte[] Elements,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command PROGRAM_MAX: Programs a (fixed size) amount of elements into a 
        /// non-volatile memory segment, starting at the MTA. The MTA will be 
        /// postincremented by MAX_CTO_PGM-1.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Elements">Data buffer with the fixed length of MAX_CTO_PGM-1</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult ProgramMax(
            TXCPHandle XcpHandle,
            byte[] Elements)
        {
            return ProgramMax(XcpHandle, Elements, null, 0);
        }

        /// <summary>
        /// Command PROGRAM_VERIFY: Induces a verification process after a 
        /// memory programming session.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Mode, 0: internal routine, 1: Verification value</param>
        /// <param name="Type">Verification Type, i.e. area(s) to verify</param>
        /// <param name="Value">Verification value, when mode is 1</param>
        /// <param name="ctoBuffer">Buffer for the slave responce (CTO package)</param>
        /// <param name="ctoBufferLength">Length of the CTO buffer</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_ProgramVerify")]
        public static extern TXCPResult ProgramVerify(
            TXCPHandle XcpHandle,
            byte Mode,
            UInt16 Type,
            UInt32 Value,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)]
            byte[] ctoBuffer,
            UInt16 ctoBufferLength);

        /// <summary>
        /// Command PROGRAM_VERIFY: Induces a verification process after a 
        /// memory programming session.
        /// </summary>
        /// <param name="XcpHandle">The handle of a XCP session</param>
        /// <param name="Mode">Mode, 0: internal routine, 1: Verification value</param>
        /// <param name="Type">Verification Type, i.e. area(s) to verify</param>
        /// <param name="Value">Verification value, when mode is 1</param>
        /// <returns>A TXCPResult result code</returns>
        public static TXCPResult ProgramVerify(
            TXCPHandle XcpHandle,
            byte Mode,
            UInt16 Type,
            UInt32 Value)
        {
            return ProgramVerify(XcpHandle, Mode, Type, Value, null, 0);
        }

        /// <summary>
        /// Returns a descriptive text of a given TXCPResult error code
        /// </summary>
        /// <param name="errorCode">A TXCPResult result code</param>
        /// <param name="textBuffer">Buffer for the text (must be at least 256 in length)</param>
        /// <returns>A TXCPResult result code</returns>
        [DllImport("PXCP.dll", EntryPoint = "XCP_GetErrorText")]
        public static extern TXCPResult GetErrorText(
            TXCPResult errorCode,
            StringBuilder textBuffer);
    }
    #endregion
}