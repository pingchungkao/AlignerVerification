using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AlignerVerification.Class
{
    public class PointD
    {
        public double X;
        public double Y;
    }
    public static class Statistics
    {
        public static int OffsetType = 0;
        //原點
        public static PointD NowOrigin = new PointD();
        public static PointD AvgOrigin = new PointD();
        public static double OOffset;

        //最高點
        public static PointD NowTop = new PointD();
        public static PointD AvgTop = new PointD();
        public static double TOffset;
        //Notch
        public static PointD NowNotch = new PointD();
        public static PointD AvgNotch = new PointD();
        public static double NOffset;   

        //NotchDeg
        public static double NowNotchDeg;
        public static double MaxNotchDeg;
        public static double MinNotchDeg;
        public static double NOffsetDeg;

        public static List<PointD> OriginList = new List<PointD>();
        public static List<PointD> NotchList = new List<PointD>();
        public static List<PointD> TopList = new List<PointD>();
        public static List<double> NotchDegList = new List<double>();

        public static List<Point> OPTList = new List<Point>();
        public static List<Point> NPTList = new List<Point>();
        public static List<Point> TPTList = new List<Point>();
        public static Point NowOPT = new Point();
        public static Point NowNPT = new Point();
        public static Point NowTPT = new Point();

        public static List<double> TackTimeList = new List<double>();
        public static double NowTackTime;
        public static double MaxTackTime;
        public static double MinTackTime;
        public static double AvgTackTime;

        public static double CalibrateOffset;
        public static double CalibrateDegOffset;

        //Notch與原點之間的距離
        public static double AvgDistance;
        #region CPK
        /// <summary>
        /// 規格寬度（規格上限-規格下限）
        /// </summary>
        public static double CpkTSpcFullRange = 0.2;
        public static double CpkDegSpcFullRange = 0.2;
        public static double CpkOffsetT = 0;
        public static double CpkXOffsetT = 0;
        public static double CpkYOffsetT = 0;
        public static double CpkOffsetDeg = 0;
        #endregion
        #region 重複性
        public static List<Point> RepeatPT1List = new List<Point>();
        public static List<double> RepeatPT1OffsetList = new List<double>();
        public static List<Point> RepeatPT2List = new List<Point>();
        public static List<double> RepeatPT2OffsetList = new List<double>();
        public static List<Point> RepeatPT3List = new List<Point>();
        public static List<double> RepeatPT3OffsetList = new List<double>();
        public static List<Point> RepeatPTOList = new List<Point>();
        public static List<double> RepeatPTOOffsetList = new List<double>();

        public static List<double> PT1PT2AngleList = new List<double>();
        public static List<double> PT1PT3AngleList = new List<double>();
        public static List<double> PT1PTOAngleList = new List<double>();
        public static List<double> PT2PTOAngleList = new List<double>();
        public static List<double> PT3PTOAngleList = new List<double>();

        public static Point NowRepeatPT1 = new Point();
        public static Point NowRepeatPT2 = new Point();
        public static Point NowRepeatPT3 = new Point();
        public static Point NowRepeatPTO = new Point();

        public static double RepeatPT1Offset;
        public static double RepeatPT1Sigma;
        public static double RepeatPT2Offset;
        public static double RepeatPT2Sigma;
        public static double RepeatPT3Offset;
        public static double RepeatPT3Sigma;
        public static double RepeatPTOOffset;
        public static double RepeatPTOSigma;

        public static double RepeatPT1PT2AngleOffset;
        public static double RepeatPT1PT2AngleMax;
        public static double RepeatPT1PT2AngleMin;
        public static double PT1PT2AngleSigma;

        public static double RepeatPT1PT3AngleOffset;
        public static double RepeatPT1PT3AngleMax;
        public static double RepeatPT1PT3AngleMin;
        public static double PT1PT3AngleSigma;

        public static double RepeatPT1PTOAngleOffset;
        public static double RepeatPT1PTOAngleMax;
        public static double RepeatPT1PTOAngleMin;
        public static double PT1PTOAngleSigma;

        public static double RepeatPT2PTOAngleOffset;
        public static double RepeatPT2PTOAngleMax;
        public static double RepeatPT2PTOAngleMin;
        public static double PT2PTOAngleSigma;

        public static double RepeatPT3PTOAngleOffset;
        public static double RepeatPT3PTOAngleMax;
        public static double RepeatPT3PTOAngleMin;
        public static double PT3PTOAngleSigma;

        #endregion

        public static double RPi;
        public static double RPa;

        public static void Reset()
        {
            NowTop.X = 0.0;
            NowTop.Y = 0.0;
            AvgTop.X = 0.0;
            AvgTop.Y = 0.0;
            TOffset = 0.0;

            NowOrigin.X = 0;
            NowOrigin.Y = 0;

            AvgOrigin.X = 0;
            AvgOrigin.Y = 0;

            NOffset = 0.0;
            OOffset = 0.0;

            NowNotch.X = 0;
            NowNotch.Y = 0;

            AvgNotch.X = 0;
            AvgNotch.Y = 0;

            NowNotchDeg = 0.0;
            MaxNotchDeg = -360.0;
            MinNotchDeg = 360.0; 
            NOffsetDeg = 0.0;

            NowTackTime = 0.0;
            MaxTackTime = 0.0;
            MinTackTime = 0.0;
            AvgTackTime = 0.0;

            AvgDistance = 0.0;

            OriginList.Clear();
            NotchList.Clear();
            TackTimeList.Clear();
            TopList.Clear();
            NotchDegList.Clear();

            OPTList.Clear();
            NPTList.Clear();
            TPTList.Clear();

            NowOPT.X = 0;
            NowOPT.Y = 0;

            NowNPT.X = 0;
            NowNPT.Y = 0;

            NowTPT.X = 0;
            NowTPT.Y = 0;

            CpkOffsetT = 0.0;
            CpkOffsetDeg = 0.0;
            CpkXOffsetT = 0.0;
            CpkYOffsetT = 0.0;

#region 重複性
            NowRepeatPT1.X = NowRepeatPT1.Y = 0;
            NowRepeatPT2.X = NowRepeatPT2.Y = 0;
            NowRepeatPT3.X = NowRepeatPT3.Y = 0;
            NowRepeatPTO.X = NowRepeatPTO.Y = 0;

            RepeatPT1Offset = 0.0;
            RepeatPT2Offset = 0.0;
            RepeatPT3Offset = 0.0;
            RepeatPTOOffset = 0.0;

            RepeatPT1PT2AngleOffset = 0.0;
            RepeatPT1PT2AngleMax = -90;
            RepeatPT1PT2AngleMin = 90;
            PT1PT2AngleSigma = 0.0;

            RepeatPT1PT3AngleOffset = 0.0;
            RepeatPT1PT3AngleMax = -90;
            RepeatPT1PT3AngleMin = 90;
            PT1PT3AngleSigma = 0.0;

            RepeatPT1PTOAngleOffset = 0.0;
            RepeatPT1PTOAngleMax = -90;
            RepeatPT1PTOAngleMin = 90;
            PT1PTOAngleSigma = 0.0;

            RepeatPT2PTOAngleOffset = 0.0;
            RepeatPT2PTOAngleMax = -90;
            RepeatPT2PTOAngleMin = 90;
            PT2PTOAngleSigma = 0.0;

            RepeatPT3PTOAngleOffset = 0.0;
            RepeatPT3PTOAngleMax = -90;
            RepeatPT3PTOAngleMin = 90;
            PT3PTOAngleSigma = 0.0;

            RepeatPT1List.Clear();
            RepeatPT2List.Clear();
            RepeatPT3List.Clear();
            RepeatPTOList.Clear();

            PT1PT2AngleList.Clear();
            PT1PT3AngleList.Clear();
            PT1PTOAngleList.Clear();
            PT2PTOAngleList.Clear();
            PT3PTOAngleList.Clear();

            RepeatPT1OffsetList.Clear();
            RepeatPT2OffsetList.Clear();
            RepeatPT3OffsetList.Clear();
            RepeatPTOOffsetList.Clear();
#endregion
        }

        public static void AddRepeatPt(Point pt1, Point pt2, Point pt3, Point o)
        {
            RepeatPT1List.Add(pt1);
            RepeatPT2List.Add(pt2);
            RepeatPT3List.Add(pt3);
            RepeatPTOList.Add(o);

            NowRepeatPT1 = pt1;
            NowRepeatPT2 = pt2;
            NowRepeatPT3 = pt3;
            NowRepeatPTO = o;

            double Temp = 0.0;

            foreach (Point pt in RepeatPT1List)
            {
                Temp = Math.Sqrt(Math.Pow((NowRepeatPT1.X - pt.X), 2) + Math.Pow((NowRepeatPT1.Y - pt.Y), 2));

                if (Temp > RepeatPT1Offset) RepeatPT1Offset = Temp;
            }

            double avgX, avgY;

            avgX = RepeatPT1List.Average(x => x.X);
            avgY = RepeatPT1List.Average(x => x.Y);

            RepeatPT1OffsetList.Add(Math.Sqrt(Math.Pow((NowRepeatPT1.X - avgX),2) + Math.Pow((NowRepeatPT1.Y - avgY),2)));

            if (RepeatPT1List.Count > 1)
            {
                RepeatPT1Sigma = 0.0;

                foreach(double offset in RepeatPT1OffsetList)
                {
                    RepeatPT1Sigma += Math.Pow((offset - RepeatPT1OffsetList.Average(x=>x)), 2);
                }

                RepeatPT1Sigma = Math.Sqrt(RepeatPT1Sigma / (RepeatPT1OffsetList.Count - 1));
            }

           
            foreach (Point pt in RepeatPT2List)
            {
                Temp = Math.Sqrt(Math.Pow((NowRepeatPT2.X - pt.X),2) + Math.Pow((NowRepeatPT2.Y - pt.Y),2));

                if (Temp > RepeatPT2Offset) RepeatPT2Offset = Temp;
            }

            avgX = RepeatPT2List.Average(x => x.X);
            avgY = RepeatPT2List.Average(x => x.Y);

            RepeatPT2OffsetList.Add(Math.Sqrt((NowRepeatPT2.X - avgX) * (NowRepeatPT2.X - avgX) + (NowRepeatPT2.Y - avgY) * (NowRepeatPT2.Y - avgY)));

            if (RepeatPT2List.Count > 1)
            {
                RepeatPT2Sigma = 0.0;

                foreach (double offset in RepeatPT2OffsetList)
                {
                    RepeatPT2Sigma += Math.Pow((offset - RepeatPT2OffsetList.Average(x => x)), 2);
                }

                RepeatPT2Sigma = Math.Sqrt(RepeatPT2Sigma / (RepeatPT2OffsetList.Count - 1));
            }

            foreach (Point pt in RepeatPT3List)
            {
                Temp = Math.Sqrt(Math.Pow((NowRepeatPT3.X - pt.X), 2) + Math.Pow((NowRepeatPT3.Y - pt.Y), 2));

                if (Temp > RepeatPT3Offset) RepeatPT3Offset = Temp;
            }

            avgX = RepeatPT3List.Average(x => x.X);
            avgY = RepeatPT3List.Average(x => x.Y);

            RepeatPT3OffsetList.Add(Math.Sqrt(Math.Pow((NowRepeatPT3.X - avgX), 2) + Math.Pow((NowRepeatPT3.Y - avgY), 2)));

            if (RepeatPT3List.Count > 1)
            {
                RepeatPT3Sigma = 0.0;

                foreach (double offset in RepeatPT3OffsetList)
                {
                    RepeatPT3Sigma += Math.Pow((offset - RepeatPT3OffsetList.Average(x => x)), 2);
                }

                RepeatPT3Sigma = Math.Sqrt(RepeatPT3Sigma / (RepeatPT3OffsetList.Count - 1));
            }

            //原點偏移量
            foreach (Point pt in RepeatPTOList)
            {
                Temp = Math.Sqrt(Math.Pow((NowRepeatPTO.X - pt.X), 2) + Math.Pow((NowRepeatPTO.Y - pt.Y), 2));

                if (Temp > RepeatPTOOffset) RepeatPTOOffset = Temp;
            }

            avgX = RepeatPTOList.Average(x => x.X);
            avgY = RepeatPTOList.Average(x => x.Y);

            RepeatPTOOffsetList.Add(Math.Sqrt(Math.Pow((NowRepeatPTO.X - avgX), 2) + Math.Pow((NowRepeatPTO.Y - avgY), 2)));

            if (RepeatPTOList.Count > 1)
            {
                RepeatPTOSigma = 0.0;

                foreach (double offset in RepeatPTOOffsetList)
                {
                    RepeatPTOSigma += Math.Pow((offset - RepeatPTOOffsetList.Average(x => x)), 2);
                }

                RepeatPTOSigma = Math.Sqrt(RepeatPTOSigma / (RepeatPTOOffsetList.Count - 1));
            }


            /////////////////////////////////////////
            Temp = Math.Atan((double)(NowRepeatPT2.Y - NowRepeatPT1.Y) / (double)(NowRepeatPT2.X - NowRepeatPT1.X)) / Math.PI * 180;

            PT1PT2AngleList.Add(Temp);

            if (Temp > RepeatPT1PT2AngleMax)
                RepeatPT1PT2AngleMax = Temp;

            if (Temp < RepeatPT1PT2AngleMin)
                RepeatPT1PT2AngleMin = Temp;

            RepeatPT1PT2AngleOffset = RepeatPT1PT2AngleMax - RepeatPT1PT2AngleMin;

            if(PT1PT2AngleList.Count > 1)
            {
                PT1PT2AngleSigma = 0.0;
                foreach(double Angle in PT1PT2AngleList)
                    PT1PT2AngleSigma += Math.Pow((Angle - PT1PT2AngleList.Average(x => x)), 2);

                PT1PT2AngleSigma = Math.Sqrt(PT1PT2AngleSigma / (PT1PT2AngleList.Count - 1));
            }

            /////////////////////////////////////////
            Temp = Math.Atan((double)(NowRepeatPT3.Y - NowRepeatPT1.Y) / (double)(NowRepeatPT3.X - NowRepeatPT1.X)) / Math.PI * 180;
            PT1PT3AngleList.Add(Temp);

            if (Temp > RepeatPT1PT3AngleMax)
                RepeatPT1PT3AngleMax = Temp;

            if (Temp < RepeatPT1PT3AngleMin)
                RepeatPT1PT3AngleMin = Temp;

            RepeatPT1PT3AngleOffset = RepeatPT1PT3AngleMax - RepeatPT1PT3AngleMin;

            if (PT1PT3AngleList.Count > 1)
            {
                PT1PT3AngleSigma = 0.0;
                foreach (double Angle in PT1PT3AngleList)
                    PT1PT3AngleSigma += Math.Pow((Angle - PT1PT3AngleList.Average(x => x)), 2);

                PT1PT3AngleSigma = Math.Sqrt(PT1PT3AngleSigma / (PT1PT3AngleList.Count - 1));
            }

            ///////////
            //圓心偏移
            Temp = Math.Atan((double)(NowRepeatPTO.Y - NowRepeatPT1.Y) / (double)(NowRepeatPTO.X - NowRepeatPT1.X)) / Math.PI * 180;
            PT1PTOAngleList.Add(Temp);

            if (Temp > RepeatPT1PTOAngleMax)
                RepeatPT1PTOAngleMax = Temp;

            if (Temp < RepeatPT1PTOAngleMin)
                RepeatPT1PTOAngleMin = Temp;

            RepeatPT1PTOAngleOffset = RepeatPT1PTOAngleMax - RepeatPT1PTOAngleMin;

            if (PT1PTOAngleList.Count > 1)
            {
                PT1PTOAngleSigma = 0.0;
                foreach (double Angle in PT1PTOAngleList)
                    PT1PTOAngleSigma += Math.Pow((Angle - PT1PTOAngleList.Average(x => x)), 2);

                PT1PTOAngleSigma = Math.Sqrt(PT1PTOAngleSigma / (PT1PTOAngleList.Count - 1));
            }

            Temp = Math.Atan((double)(NowRepeatPTO.Y - NowRepeatPT2.Y) / (double)(NowRepeatPTO.X - NowRepeatPT2.X)) / Math.PI * 180;
            PT2PTOAngleList.Add(Temp);

            if (Temp > RepeatPT2PTOAngleMax)
                RepeatPT2PTOAngleMax = Temp;

            if (Temp < RepeatPT2PTOAngleMin)
                RepeatPT2PTOAngleMin = Temp;

            RepeatPT2PTOAngleOffset = RepeatPT2PTOAngleMax - RepeatPT2PTOAngleMin;

            if (PT2PTOAngleList.Count > 1)
            {
                PT2PTOAngleSigma = 0.0;
                foreach (double Angle in PT2PTOAngleList)
                    PT2PTOAngleSigma += Math.Pow((Angle - PT2PTOAngleList.Average(x => x)), 2);

                PT2PTOAngleSigma = Math.Sqrt(PT2PTOAngleSigma / (PT2PTOAngleList.Count - 1));
            }

            Temp = Math.Atan((double)(NowRepeatPTO.Y - NowRepeatPT3.Y) / (double)(NowRepeatPTO.X - NowRepeatPT3.X)) / Math.PI * 180;
            PT3PTOAngleList.Add(Temp);

            if (Temp > RepeatPT3PTOAngleMax)
                RepeatPT3PTOAngleMax = Temp;

            if (Temp < RepeatPT3PTOAngleMin)
                RepeatPT3PTOAngleMin = Temp;

            RepeatPT3PTOAngleOffset = RepeatPT3PTOAngleMax - RepeatPT3PTOAngleMin;

            if (PT3PTOAngleList.Count > 1)
            {
                PT3PTOAngleSigma = 0.0;
                foreach (double Angle in PT3PTOAngleList)
                    PT3PTOAngleSigma += Math.Pow((Angle - PT3PTOAngleList.Average(x => x)), 2);

                PT3PTOAngleSigma = Math.Sqrt(PT3PTOAngleSigma / (PT3PTOAngleList.Count - 1));
            }

        }
        
        public static void AddCalibrationPT(Point Opt, Point Npt, Point Tpt)
        {
            NowOPT = Opt;
            NowNPT = Npt;
            NowTPT = Tpt;

            OPTList.Add(NowOPT);
            NPTList.Add(NowNPT);
            TPTList.Add(NowTPT);
        }

        public static void AddTackTime(double TackTime)
        {
            NowTackTime = TackTime;

            TackTimeList.Add(NowTackTime);

            MaxTackTime = TackTimeList.Max();
            MinTackTime = TackTimeList.Min();
            AvgTackTime = TackTimeList.Average();
        }
        /// <summary>
        /// Cpk 計算
        /// </summary>
        /// <param name="CpkDeg">角度Cpk計算</param>
        /// <param name="CpkOffset">位移Cpk計算</param>
        public static void CaculateCpk()
        {
            if (TopList.Count <= 1) return;

            //平移 Ck(誤差量)
            //Ck = (M-X)/(T/2)
            //Cp = T/6Sigma
            //Cpk = (1 - Ck)*Cp
            List<double> errorList = new List<double>();
            foreach (PointD pt in TopList)
            {
                errorList.Add(Math.Sqrt(Math.Pow((AvgTop.X - pt.X), 2) + Math.Pow((AvgTop.Y - pt.Y), 2)));
            }

            double Sigma = 0.0;
            foreach(double err in errorList)
            {
                Sigma += Math.Pow((err - errorList.Average(x => x)), 2);
            }

            CpkOffsetT = (1 -(- (errorList.Average(x => x) / (CpkTSpcFullRange / 2.0)))) * (CpkTSpcFullRange / (6 * (Math.Sqrt(Sigma / (errorList.Count - 1)))));

            errorList.Clear();
            foreach (PointD pt in TopList)
            {
                errorList.Add(AvgTop.X - pt.X);
            }

            Sigma = 0.0;
            foreach (double err in errorList)
            {
                Sigma += Math.Pow((err - errorList.Average(x => x)), 2);
            }

            CpkXOffsetT = (1 - (-(errorList.Average(x => x) / (CpkTSpcFullRange / 2.0)))) * (CpkTSpcFullRange / (6 * (Math.Sqrt(Sigma / (errorList.Count - 1)))));


            errorList.Clear();
            foreach (PointD pt in TopList)
            {
                errorList.Add(AvgTop.Y - pt.Y);
            }

            Sigma = 0.0;
            foreach (double err in errorList)
            {
                Sigma += Math.Pow((err - errorList.Average(x => x)), 2);
            }

            CpkYOffsetT = (1 - (-(errorList.Average(x => x) / (CpkTSpcFullRange / 2.0)))) * (CpkTSpcFullRange / (6 * (Math.Sqrt(Sigma / (errorList.Count - 1)))));


            errorList.Clear();

            foreach (double deg in NotchDegList)
            {
                errorList.Add(NotchDegList.Average(x => x) - deg);
            }


            Sigma = 0.0;
            foreach (double err in errorList)
            {
                Sigma += Math.Pow((err - errorList.Average(x => x)), 2);
            }

            CpkOffsetDeg = (1 - ( -(errorList.Average(x => x)) / (CpkDegSpcFullRange / 2.0))) * (CpkDegSpcFullRange / (6 * (Math.Sqrt(Sigma / (errorList.Count - 1)))));

        }
        public static void AddNotchDeg(double nowNotchDeg)
        {
            NotchDegList.Add(nowNotchDeg);

            NowNotchDeg = nowNotchDeg;

            if (NowNotchDeg > MaxNotchDeg) MaxNotchDeg = nowNotchDeg;

            if (NowNotchDeg < MinNotchDeg) MinNotchDeg = nowNotchDeg;

            NOffsetDeg = MaxNotchDeg - MinNotchDeg;

        }
        public static void AddTop(PointD nowPtD)
        {
            double Temp = 0;

            NowTop = nowPtD;

            TopList.Add(nowPtD);

            AvgTop.X = TopList.Average(x => x.X);
            AvgTop.Y = TopList.Average(y => y.Y);

            if (OffsetType == 0)
            {
                foreach (PointD pt in TopList)
                {
                    Temp = Math.Sqrt((nowPtD.X - pt.X) * (nowPtD.X - pt.X) + (nowPtD.Y - pt.Y) * (nowPtD.Y - pt.Y));

                    if (Temp > TOffset) TOffset = Temp;
                }
            }
            else
            {
                //Temp = Math.Sqrt((nowPtD.X - AvgTop.X) * (nowPtD.X - AvgTop.X) + (nowPtD.Y - AvgTop.Y) * (nowPtD.Y - AvgTop.Y));
                //
                //if (Temp > TOffset) TOffset = Temp;


                foreach (PointD pt in TopList)
                {
                    Temp = Math.Sqrt((pt.X - AvgTop.X) * (pt.X - AvgTop.X) + (pt.Y - AvgTop.Y) * (pt.Y - AvgTop.Y));

                    if (Temp > TOffset) TOffset = Temp;
                }

            }

        }
        public static void AddOrigin(PointD nowPtD)
        {
            double Temp = 0;

            NowOrigin = nowPtD;

            OriginList.Add(nowPtD);

            AvgOrigin.X = OriginList.Average(x => x.X);
            AvgOrigin.Y = OriginList.Average(y => y.Y);

            if (OffsetType == 0)
            {
                foreach (PointD pt in OriginList)
                {
                    Temp = Math.Sqrt((nowPtD.X - pt.X) * (nowPtD.X - pt.X) + (nowPtD.Y - pt.Y) * (nowPtD.Y - pt.Y));

                    if (Temp > OOffset) OOffset = Temp;
                }
            }
            else
            {
                Temp = Math.Sqrt((nowPtD.X - AvgOrigin.X) * (nowPtD.X - AvgOrigin.X) + (nowPtD.Y - AvgOrigin.Y) * (nowPtD.Y - AvgOrigin.Y));

                if (Temp > OOffset) OOffset = Temp;
            }
        }
        public static void AddNotch(PointD nowPtD)
        {
            double Temp = 0;
            NowNotch = nowPtD;

            NotchList.Add(nowPtD);

            AvgNotch.X = NotchList.Average(x => x.X);
            AvgNotch.Y = NotchList.Average(y => y.Y);

            if (OffsetType == 0)
            {
                foreach (PointD pt in NotchList)
                {
                    Temp = Math.Sqrt((nowPtD.X - pt.X) * (nowPtD.X - pt.X) + (nowPtD.Y - pt.Y) * (nowPtD.Y - pt.Y));

                    if (Temp > NOffset) NOffset = Temp;
                }
            }
            else
            {
                Temp = Math.Sqrt((nowPtD.X - AvgNotch.X) * (nowPtD.X - AvgNotch.X) + (nowPtD.Y - AvgNotch.Y) * (nowPtD.Y - AvgNotch.Y));

                if (Temp > NOffset) NOffset = Temp;
            }
        }

        public static void FindCalibrateOffset()
        {
            //沿用舊的offset計算方式方式
            //1.計算平均Notch的位置

            //以下為以pixel方式計算-------------------------
            //double AvgNotchX = 0.0;
            //double AvgNotchY = 0.0;
            //double AvgOriginX = 0.0;
            //double AvgOriginY = 0.0;
            //double AvgTopX = 0.0;
            //double AvgTopY = 0.0;
            //double NowNotchX = (double)NowNPT.X;
            //double NowNotchY = (double)NowNPT.Y;
            //double NowOriginX = (double)NowOPT.X;
            //double NowOriginY = (double)NowOPT.Y;
            //double NowTopX = (double)NowTPT.X;
            //double NowTopY = (double)NowTPT.Y;

            //double sum1 = 0;
            //double sum2 = 0;
            //foreach (Point pt in OPTList)
            //{
            //    sum1 += pt.X;
            //    sum2 += pt.Y;
            //}

            //AvgOriginX = sum1 / OPTList.Count();
            //AvgOriginY = sum2 / OPTList.Count();

            //sum1 = 0;
            //sum2 = 0;
            //foreach (Point pt in NPTList)
            //{
            //    sum1 += pt.X;
            //    sum2 += pt.Y;
            //}

            //AvgNotchX = sum1 / NPTList.Count();
            //AvgNotchY = sum2 / NPTList.Count();

            //double d = Math.Sqrt(Math.Pow(AvgNotchX - AvgOriginX, 2) + Math.Pow(AvgNotchY - AvgOriginY, 2));

            //////1.2 計算 當下Notch到圓心的距離 - Notch到圓心的平均距離(不考慮角度)
            //CalibrateOffset = Math.Sqrt(Math.Pow(NowNotchX - AvgOriginX, 2) + Math.Pow(NowNotchY - AvgOriginY, 2)) - d;
            //CalibrateOffset = CalibrateOffset / (300.0 / 1.67);

            //if (NowNotchY - NowOriginY >= 0)
            //{
            //    CalibrateDegOffset = Math.Acos((NowNotchX - NowOriginX) / Math.Sqrt(Math.Pow(NowNotchX - NowOriginX, 2) + Math.Pow(NowNotchY - NowOriginY, 2))) * 180 / Math.PI;
            //}
            //else
            //{
            //    CalibrateDegOffset = -1 * Math.Acos((NowNotchX - NowOriginX) / Math.Sqrt(Math.Pow(NowNotchX - NowOriginX, 2) + Math.Pow(NowNotchY - NowOriginY, 2))) * 180 / Math.PI;
            //}
            //以上為以pixel方式計算-------------------------



            //以下為以mm方式計算-------------------------
            //1.1 計算 Notch到圓心的平均距離
            double d = Math.Sqrt(Math.Pow(AvgNotch.X - AvgOrigin.X, 2) + Math.Pow(AvgNotch.Y - AvgOrigin.Y, 2));

            ////1.2 計算 當下Notch到圓心的距離 - Notch到圓心的平均距離(不考慮角度)
            CalibrateOffset = Math.Sqrt(Math.Pow(NowNotch.X - AvgOrigin.X, 2) + Math.Pow(NowNotch.Y - AvgOrigin.Y, 2)) - d;


            //2.計算角度偏差量
            if (NowNotch.Y - NowOrigin.Y >= 0)
            {
                CalibrateDegOffset = Math.Acos((NowNotch.X - NowOrigin.X) / Math.Sqrt(Math.Pow(NowNotch.X - NowOrigin.X, 2) + Math.Pow(NowNotch.Y - NowOrigin.Y, 2))) * 180 / Math.PI;
            }
            else
            {
                CalibrateDegOffset = -1 * Math.Acos((NowNotch.X - NowOrigin.X) / Math.Sqrt(Math.Pow(NowNotch.X - NowOrigin.X, 2) + Math.Pow(NowNotch.Y - NowOrigin.Y, 2))) * 180 / Math.PI;
            }

            //以上為以mm方式計算-------------------------
        }

        public static void CaculatePoseRepeatability()
        {
            if (TopList.Count < 2) return; 

            List<double> l = new List<double>();

            for (int i = 0; i < TopList.Count; i++)
            {
                l.Add(Math.Sqrt(Math.Pow(TopList[i].X - TopList.Average(x => x.X), 2) +
                    Math.Pow(TopList[i].Y - TopList.Average(x => x.Y), 2)));
            }

            double Sigma = 0;

            for(int i = 0; i< l.Count; i++)
            {
                Sigma += Math.Pow(l[i] - l.Average(x => x), 2);
            }

            RPi = l.Average(x => x) + (3 * Math.Sqrt(Sigma / (l.Count - 1)));

            Sigma = 0;
            for (int i = 0; i< NotchDegList.Count; i++)
            {
                Sigma += Math.Pow(NotchDegList[i] - NotchDegList.Average(x => x), 2);
            }

            RPa = 3 * Math.Sqrt(Sigma/ (NotchDegList.Count-1));
        }
    }
}
