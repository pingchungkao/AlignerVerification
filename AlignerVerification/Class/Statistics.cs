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
        /// <summary>
        /// 規格寬度（規格上限-規格下限）
        /// </summary>
        public static double CpkTSpcFullRange = 0.2;
        public static double CpkDegSpcFullRange = 0.2;
        public static double CpkOffsetT = 0;
        public static double CpkXOffsetT = 0;
        public static double CpkYOffsetT = 0;
        public static double CpkOffsetDeg = 0;

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
    }
}
