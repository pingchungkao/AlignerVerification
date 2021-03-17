using log4net;
using AlignerVerification.Comm;
using AlignerVerification.CommandConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AlignerVerification.Class;

namespace AlignerVerification.Controller
{
    public class DeviceMsg
    {
        public DeviceConfig config;
        public string MsgObject;
    }
    public enum EConnectionReport { eMessage = 0, eConnecting, eConnected, eDisconnected, eError }
    public class DeviceController : IConnectionReport, IDevice
    {
        public const string PROCESS_STATE_NOT_CONNECT = "NOT_CONNECT";
        public const string PROCESS_STATE_CONNECT_ERROR = "CONNECT_ERROR";
        public const string PROCESS_STATE_INIT = "INIT";
        public const string PROCESS_STATE_IDLE = "IDLE";
        public const string PROCESS_STATE_PROCESS = "PROCESS";
        public const string PROCESS_STATE_ERROR = "ERROR";
        public const string PROCESS_STATE_UNKNOWN = "UNKNOWN";

        private static readonly ILog logger = LogManager.GetLogger(typeof(DeviceController));
        IConnection conn;
        IConnectionReport ConnReport;

        public DeviceConfig _Config;
        public string Name { get; set; }
        public string Status = "Disconnected";
        public string processState = PROCESS_STATE_NOT_CONNECT;
        public string errorCode = "";
        CommandDecoder _Decoder;

        public string FINReturnCode = "";
        public string RawData = "";
        public string DataReceived = "";
        /// <summary>
        /// 接收到$1EVT
        /// </summary>
        public EventHandler<string> ReceivedEventMessage;


        public bool _IsConnected { get; set; }
        public int TrxNo = 1;
        bool WaitingForSync = false;
        string ReturnForSync = "";
        string ReturnTypeForSync = "";

        public DeviceController(DeviceConfig Config, IConnectionReport _ConnReport = null)
        {
            _Config = Config;
            _Decoder = new CommandConvert.CommandDecoder(_Config.Vendor);
            ConnReport = _ConnReport;

            switch (Config.ConnectionType)
            {
                case "Socket":
                    conn = new TcpCommClient(Config, this);
                    break;
                case "ComPort":
                    conn = new ComPortClient(Config, this);
                    break;

            }

            this.Name = _Config.DeviceName;
            this.Status = "";
            this._IsConnected = false;
        }
        public void sendHexCommand(object Message)
        {
            logger.Info(_Config.DeviceName + " Send: " + Message);

            conn.SendHexData(Message);
        }

        public bool sendCommand(string msg)
        {
            errorCode = "";
            string info;
            bool result = false;
            try
            {
                if (!Status.Equals("Connected"))
                {
                    logger.Error(Name + " is not Connected.");
                    return false;
                }
                info = msg;

                if (_Config.Vendor.ToUpper().Equals("SANWA"))
                    msg = msg + "\r";

                logger.Info(_Config.DeviceName + " Send: " + info);

                conn.Send(msg);
                result = true;
            }
            catch (Exception e)
            {
                logger.Error(Name + " sendCommand error:" + msg + "\n" + e.Message + "\n" + e.StackTrace);
            }
            return result;
        }
        public bool start()
        {
            bool result = false;
            try
            {
                conn.Start();
                //this._IsConnected = true;
                result = true;
            }
            catch (Exception e)
            {
                logger.Error("連線失敗:" + e.Message + " " + e.StackTrace);
            }
            return result;
        }

        public void close()
        {
            conn.Close();
            this._IsConnected = false;

            logger.Info(_Config.DeviceName + " : Close ");
        }
        public void AssignedRecevicedEvent(EventHandler<string> EventMessage)
        {
            //EventMessage += ReceivedEventMessage;
            ReceivedEventMessage += EventMessage;
        }

        void IConnectionReport.On_Connection_Connected(object MsgObj)
        {
            this._IsConnected = true;
            this.Status = "Connected";
            //this.processState = DeviceController.PROCESS_STATE_INIT;
            this.processState = DeviceController.PROCESS_STATE_IDLE;

            if (ConnReport != null)
                ConnReport.On_Connection_Connected(_Config);
        }
        void IConnectionReport.On_Message_Log(string Type, string Message)
        {
            if (ConnReport != null)
                ConnReport.On_Message_Log(Type, Message);
        }
        void IConnectionReport.On_Connection_Connecting(object MsgObj)
        {
            this._IsConnected = false;
            this.Status = "Connecting";

            if (ConnReport != null)
                ConnReport.On_Connection_Connecting(_Config);
        }

        void IConnectionReport.On_Connection_Disconnected(object MsgObj)
        {
            this._IsConnected = false;
            this.Status = "Disconnected";
            this.processState = DeviceController.PROCESS_STATE_UNKNOWN;

            if (ConnReport != null)
                ConnReport.On_Connection_Disconnected(_Config);
        }

        void IConnectionReport.On_Connection_Error(object MsgObj)
        {
            this._IsConnected = false;
            this.processState = DeviceController.PROCESS_STATE_CONNECT_ERROR;

            if (ConnReport != null)
                ConnReport.On_Connection_Error(_Config);
        }

        void IConnectionReport.On_Connection_Message(object MsgObj)
        {
            string info = (string)MsgObj;

            if(_Config.DeviceName.Equals("Cylinder"))
                info = "\r";

            if (ConnReport != null)
            {
                DeviceMsg dmsg = new DeviceMsg
                {
                    config = _Config,
                    MsgObject = (string)MsgObj
                };

                logger.Info(_Config.DeviceName + " Recv: " + info);
                ConnReport.On_Connection_Message(dmsg);

            }
        }
    }

}
