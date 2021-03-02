using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static List<double> TackTimeList = new List<double>();
        public static double NowTackTime;
        public static double MaxTackTime;
        public static double MinTackTime;
        public static double AvgTackTime;

        public static double CalibrateOffset;

        //Notch與原點之間的距離
        public static double AvgDistance;

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
        }
        public static void AddTackTime(double TackTime)
        {
            NowTackTime = TackTime;

            TackTimeList.Add(NowTackTime);

            MaxTackTime = TackTimeList.Max();
            MinTackTime = TackTimeList.Min();
            AvgTackTime = TackTimeList.Average();
        }
        public static void AddNotchDeg(double nowNotchDeg)
        {
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
                Temp = Math.Sqrt((nowPtD.X - AvgTop.X) * (nowPtD.X - AvgTop.X) + (nowPtD.Y - AvgTop.Y) * (nowPtD.Y - AvgTop.Y));

                if (Temp > TOffset) TOffset = Temp;
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

        public static double FindCalibrateOffset(PointD nowPtD)
        {
            ///CalibrateOffset  = Math.Sqrt((nowPtD.X - AvgOrigin.X) * (nowPtD.X - AvgOrigin.X) + (nowPtD.Y - AvgOrigin.Y) * (nowPtD.Y - AvgOrigin.Y));
            AvgDistance = Math.Sqrt((AvgNotch.X - AvgOrigin.X) * (AvgNotch.X - AvgOrigin.X) + (AvgNotch.Y - AvgOrigin.Y) * (AvgNotch.Y - AvgOrigin.Y));

            double Distance = Math.Sqrt((nowPtD.X - AvgOrigin.X) * (nowPtD.X - AvgOrigin.X) + (nowPtD.Y - AvgOrigin.Y) * (nowPtD.Y - AvgOrigin.Y));

            CalibrateOffset = Distance - AvgDistance;

            return CalibrateOffset;
        }
    }
}
