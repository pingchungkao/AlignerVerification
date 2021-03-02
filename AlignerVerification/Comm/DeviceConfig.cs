using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlignerVerification.Comm
{
    public class DeviceConfig
    {
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string Vendor { get; set; }
        public string IPAdress { get; set; }
        public int Port { get; set; }
        public string ConnectionType { get; set; }
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public string ParityBit { get; set; }
        public int DataBits { get; set; }
        public string StopBit { get; set; }
        public bool Enable { get; set; }
        public string File { get; set; }
    }
}
