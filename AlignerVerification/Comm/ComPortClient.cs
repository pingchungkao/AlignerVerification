using log4net;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlignerVerification.Comm
{
    class ComPortClient : IConnection
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ComPortClient));
        private SerialPort port;
        IConnectionReport ConnReport;
        DeviceConfig cfg;
        public bool _WaitForData = false;
        public bool Connected = false;

        public ComPortClient(DeviceConfig _Config, IConnectionReport _ConnReport)
        {
            cfg = _Config;
            ConnReport = _ConnReport;
            Parity p = Parity.None;
            switch (_Config.ParityBit)
            {
                case "Even":
                    p = Parity.Even;
                    break;
                case "Mark":
                    p = Parity.Mark;
                    break;
                case "None":
                    p = Parity.None;
                    break;
                case "Odd":
                    p = Parity.Odd;
                    break;
                case "Space":
                    p = Parity.Space;
                    break;
            }
            StopBits s = StopBits.One;
            switch (_Config.StopBit)
            {
                case "None":
                    s = StopBits.None;
                    break;
                case "One":
                    s = StopBits.One;
                    break;
                case "OnePointFive":
                    s = StopBits.OnePointFive;
                    break;
                case "Two":
                    s = StopBits.Two;
                    break;
            }

            port = new SerialPort(_Config.PortName, _Config.BaudRate, p, _Config.DataBits, s);
            if (_Config.Vendor.Equals("SMARTTAG"))
            {
                port.Handshake = Handshake.None;
                port.RtsEnable = true;
                port.ReadTimeout = 5000;
                port.WriteTimeout = 5000;
            }
        }

        public void WaitForData(bool Enable)
        {
            _WaitForData = Enable;
        }


        public void Close()
        {
            port.Close();
            Connected = false;
            ConnReport.On_Connection_Disconnected("Close");
        }

        public void Start()
        {

            Thread ComTd = new Thread(ConnectServer);
            ComTd.IsBackground = true;
            ComTd.Start();
        }

        public bool Send(object Message)
        {
            try
            {
                port.Write(Message.ToString());
                return true;
            }
            catch (Exception e)
            {
                //logger.Error("(ConnectServer " + RmIp + ":" + SPort + ")" + e.Message + "\n" + e.StackTrace);
                ConnReport.On_Connection_Error("(ConnectServer )" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }

        public bool SendHexData(object Message)
        {
            try
            {
                byte[] buf = HexStringToByteArray(Message.ToString());

                port.Write(buf, 0, buf.Length);
                return true;
            }
            catch (Exception e)
            {
                //logger.Error("(ConnectServer " + RmIp + ":" + SPort + ")" + e.Message + "\n" + e.StackTrace);
                ConnReport.On_Connection_Error("(ConnectServer )" + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }

        private void ConnectServer()
        {
            try
            {
                ConnReport.On_Connection_Connecting("Connecting to ");
                port.Open();
                Connected = true;

                ConnReport.On_Connection_Connected("Connected! ");
                switch (cfg.Vendor.ToUpper())
                {
                    case "TDK":
                        port.DataReceived += new SerialDataReceivedEventHandler(TDK_DataReceived);
                        break;
                    case "ATEL_NEW":
                    case "SANWA":
                    case "SMC":
                        port.DataReceived += new SerialDataReceivedEventHandler(Sanwa_DataReceived);
                        break;
                    case "ASYST":
                        port.DataReceived += new SerialDataReceivedEventHandler(ASYST_DataReceived);
                        break;
                    case "SMARTTAG":
                        port.DataReceived += new SerialDataReceivedEventHandler(SMARTTAG_DataReceived);
                        break;
                }
            }
            catch (Exception e)
            {
                //logger.Error("(ConnectServer " + RmIp + ":" + SPort + ")" + e.Message + "\n" + e.StackTrace);
                //logger.Error("(ConnectServer )" + e.Message + "\n" + e.StackTrace);
                //ConnReport.On_Connection_Error("(ConnectServer )" + e.Message + "\n" + e.StackTrace);
            }
        }

        private void ASYST_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = "";

                data = port.ReadTo("\r\n");


                ThreadPool.QueueUserWorkItem(new WaitCallback(ConnReport.On_Connection_Message), data);
            }
            catch (Exception e1)
            {
                ConnReport.On_Connection_Error("(ASYST_DataReceived )" + e1.Message + "\n" + e1.StackTrace);
            }
        }

        private void SMARTTAG_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SpinWait.SpinUntil(() => !_WaitForData, 1000);

                byte[] buf = new byte[250];
                port.Read(buf, 0, buf.Length);
                string data = ByteArrayToString(buf);
                ConnReport.On_Connection_Message(data.Trim());

            }
            catch (Exception e1)
            {
                //logger.Error("(ConnectServer " + RmIp + ":" + SPort + ")" + e.Message + "\n" + e.StackTrace);
                ConnReport.On_Connection_Error("(ASYST_DataReceived )" + e1.Message + "\n" + e1.StackTrace);
            }
        }

        private void TDK_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = "";
                //switch (cfg.DeviceType)
                //{
                //    case "TDKController":
                //data = port.ReadTo("\r");

                //        break;
                //    case "SanwaController":
                data = port.ReadTo(((char)3).ToString());
                //        break;
                //}

                ThreadPool.QueueUserWorkItem(new WaitCallback(ConnReport.On_Connection_Message), data);
            }
            catch (Exception e1)
            {
                //logger.Error("(ConnectServer " + RmIp + ":" + SPort + ")" + e.Message + "\n" + e.StackTrace);
                ConnReport.On_Connection_Error("(TDK_DataReceived )" + e1.Message + "\n" + e1.StackTrace);
            }
        }

        string S = "";
        private void Sanwa_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (cfg.DeviceName.ToUpper().Equals("ALIGNER") || cfg.DeviceName.ToUpper().Equals("ALIGNER02"))
                {
                    S += port.ReadExisting();

                    if (S.Contains("$") && S.Contains("\r"))
                    {
                        if (S.Substring(0, 1).Equals("$"))
                        {
                            while (S.Substring(0, 1).Equals("$") && S.Contains("\r"))
                            {
                                string data = S.Substring(0, S.IndexOf("\r"));

                                S = S.Substring(S.IndexOf("\r") + 1);

                                ThreadPool.QueueUserWorkItem(new WaitCallback(ConnReport.On_Connection_Message), data);


                                if (S.Equals(""))
                                    break;
                            }
                        }
                        else if (S.Contains("ACK:ALIGN:9\r"))
                        {
                            ThreadPool.QueueUserWorkItem(new WaitCallback(ConnReport.On_Connection_Message), S);
                            S = "";
                        }
                    }
                }
                else if (cfg.DeviceName.ToUpper().Equals("CYLINDER"))
                {
                    string data = "";
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ConnReport.On_Connection_Message), data);
                }
                else
                {
                    string data = "";
                    data = port.ReadTo("\r");
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ConnReport.On_Connection_Message), data);
                }

            }
            catch (Exception e1)
            {
                ConnReport.On_Connection_Error("(Sanwa_DataReceived )" + e1.Message + "\n" + e1.StackTrace);
            }
        }

        private string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);


            foreach (byte b in ba)
            {
                if (b == 0)
                {
                    //continue;
                }
                hex.AppendFormat("{0:X2}", b);
                hex.Append(" ");
            }
            return hex.ToString();
        }


        private byte[] HexStringToByteArray(string s)
        {

            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        public void Dispose()
        {

        }
    }
}
