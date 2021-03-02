using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlignerVerification.CommandConvert
{
    public class CommandReturnMessage
    {
        public class ReturnType
        {
            public const string Excuted = "Excuted";
            public const string Finished = "Finished";
            public const string Error = "Error";
            public const string Event = "Event";
            public const string Information = "Information";
            public const string ReInformation = "ReInformation";
            public const string Abnormal = "Abnormal";
        }
        public string OrgMsg = "";
        public string Type;
        public string Command;
        public bool IsInterrupt = false;
        public string NodeAdr = "";
        public string Seq = "";
        public string Value = "";
    	public string FinCommand = "";
        public string CommandType = "";
    }
}
