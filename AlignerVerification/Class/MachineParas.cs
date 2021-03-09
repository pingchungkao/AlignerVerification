using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlignerVerification.Class
{
    enum eWaferType { eNotch = 0, eFlat, eCircle};
    enum eTestMode { eFix = 0, eStep_Center, eStep_Notch };
    public static class MachineParas
    {
        //Wafer 半徑
        public static int WaferRadius = 150000;
        //測試速度
        public static int TestSpeed = 100;
        //Aligner後旋轉角度(mdeg)
        public static int NotchAngle = 90000;
        //Aligner後X方向位移(um)
        public static int AlignXOffset = 0;
        //輸出資料夾
        public static string ExportFolder = @"TestResult";
        //Aligner形式 
        public static int MachineType = 0;
        //Wafer形式(Notch、Flat、Circle) 
        public static int WaferType = 0;
        //Align模式
        public static int TestMode = (int)eTestMode.eStep_Notch;
        //Align測試後位移(X)
        public static int TestModeXOffset = 1500;
        //Align測試後位移(Y)
        public static int TestModeYOffset = 1500;
        //Align測試後位移(T)
        public static int TestModeTOffset = -21000;
        //輸出路徑
        public static string OutputFolder = @"D:\";

        //Aligner Wafer present monitot 監控
        public static double PresentMonitorHour = 0.0;
        public static double PresentMonitorMin = 0.0;
        public static double PresentMonitorSec = 0.0;

        //AutoRun時，是否確認 Wafer Present
        public static bool CheckWaferPresentInAutoRun = true;

        //異常時停機
        public static bool bAlarmStopEnabled = true;
        //異常時，Download Data
        public static bool bAlarmStopDownloadData = false;
        //O offset 規格上限
        public static double dOOffsetUpLimit = 0.2;
        //N offset 規格上限
        public static double dNOffsetUpLimit = 0.2;

    }
}
