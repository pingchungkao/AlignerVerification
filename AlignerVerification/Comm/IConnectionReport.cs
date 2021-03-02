using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlignerVerification.Comm
{
    public interface IConnectionReport
    {
        void On_Connection_Message(object Msg);
        void On_Connection_Connecting(object Msg);
        void On_Connection_Connected(object Msg);
        void On_Connection_Disconnected(object Msg);
        void On_Connection_Error(object Msg);
        void On_Message_Log(string Type, string Message);
    }
}
