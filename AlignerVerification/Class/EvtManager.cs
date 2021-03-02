using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlignerVerification.Class
{

    public class EvtManager
    {
        public static EventWaitHandle CylinderFinishEvt = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static EventWaitHandle CameraGrabFinishEvt = new EventWaitHandle(false, EventResetMode.AutoReset);

        public static EventWaitHandle AlignerResetFinishEvt = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static EventWaitHandle AlignerSetAlignACKEvt = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static EventWaitHandle AlignerSetAlignFinishEvt = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static EventWaitHandle AlignerSetSpeedFinishEvt = new EventWaitHandle(false, EventResetMode.AutoReset);

        public static EventWaitHandle AlignerWRLSFinishEvt = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static EventWaitHandle AlignerWHLDFinishEvt = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static EventWaitHandle AlignerORGFinishEvt = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static EventWaitHandle AlignerHOMEFinishEvt = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static EventWaitHandle AlignerMoveFinishEvt = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static EventWaitHandle AlignerMovdpFinishEvt = new EventWaitHandle(false, EventResetMode.AutoReset);
        public static EventWaitHandle AlignerGetRIOEvt = new EventWaitHandle(false, EventResetMode.AutoReset);

    }
}
