using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlignerVerification.Comm
{
    interface IDevice
    {
        bool start();

        void AssignedRecevicedEvent(EventHandler<string> ReceivedEventMessage);
    }
}
