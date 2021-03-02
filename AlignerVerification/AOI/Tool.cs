using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using log4net.Config;

using System.Drawing;

using Emgu.CV;
using Emgu.CV.Cvb;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

using MathWorks.MATLAB.NET.Arrays;
using AlignerVerification.Class;


namespace AlignerVerification.AOI
{

    public class Tool
    {
        private Mat MatImage = new Mat();
        private Rectangle ROI = new Rectangle();

        private Mat MatGray;
        public Mat MatBinary;

        public byte BinaryTHL = 0;
        public byte BinaryTHH = 255;

        public int Top = 0;
        public int Bottom = 12;

        public int ROITop = 0;
        public int ROIBottom = 2748;

        public bool OtsuInv = false;

        public bool SetImageReady = false;

        public int FilterMask = 20;

        //塗滿 Wafer
        public bool FillWafer = false;


        public double Notch_Theta = 0.0;

        public double PixelPerMM = 300.0 / 1.67;

        public Point NotchPt;
        public PointD NotchMMPt = new PointD();
        public Point CenterPt;
        public PointD CenterMMPt = new PointD();
        public Point TopPt;
        public PointD TopMMPt = new PointD();

        public Dictionary<int, Point> HighEageList = new Dictionary<int, Point>();
        public Dictionary<int, Point> LowEageList = new Dictionary<int, Point>();

        public int[] edge = new int[3840];
        public int[] lowedge = new int[3840];
        public bool ManualBinary = false;

        public string CalculateResult = "";

        private static readonly ILog logger = LogManager.GetLogger(typeof(Tool));

        public void SetImage(Mat image)
        {
            SetImageReady = true;

            MatImage = image.Clone();

            SetROI();
        }

        private void SetROI()
        {
            Rectangle roi = new Rectangle(0, ROITop, 3840, ROIBottom - ROITop);
            ROI = roi;

            MatBinary = new Mat(MatImage, ROI);
        }

        public void SetROITop(int top)
        {
            Top = top;
            ROITop = Top * 229;

            if (SetImageReady) SetROI();
        }

        public void SetROIBottom(int bottom)
        {
            Bottom = bottom;
            ROIBottom = Bottom * 229;

            if (SetImageReady) SetROI();
        }

        public Mat DoAutoBinary()
        {
            if (!SetImageReady) return null;

            //轉灰階
            MatGray = new Mat(MatImage, ROI);
            CvInvoke.CvtColor(MatGray, MatGray, ColorConversion.Bgr2Gray);

            Image<Gray, byte> img1 = MatGray.ToImage<Gray, byte>();

            CvInvoke.Threshold(img1, img1, 0, 255, ThresholdType.Otsu);

            img1 = NeedInv(img1);

            MatBinary = img1.Mat;

            return MatBinary;
        }

        public Mat DoManualBinary()
        {
            if (!SetImageReady) return null;

            //轉灰階
            MatGray = new Mat(MatImage, ROI);
            CvInvoke.CvtColor(MatGray, MatGray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

            Image<Gray, byte> img1 = MatGray.ToImage<Gray, byte>();

            CvInvoke.Threshold(img1, img1, BinaryTHL, 255, ThresholdType.Binary);

            img1 = NeedInv(img1);

            MatBinary = img1.Mat;

            return MatBinary;
        }

        public Image<Gray, byte> NeedInv(Image<Gray, byte> img1)
        {
            int wsum = 0;
            int bsum = 0;
            for (int x = 0; x < ROI.Width; x++)
            {
                if (img1.Data[0, x, 0] == 0x00)
                    bsum++;
                else
                    wsum++;
            }

            if (wsum > bsum)
            {
                for (int y = 0; y < ROI.Height; y++)
                {
                    for (int x = 0; x < ROI.Width; x++)
                    {
                        if (img1.Data[y, x, 0] == 0x00)
                            img1.Data[y, x, 0] = 0xFF;
                        else
                            img1.Data[y, x, 0] = 0x00;
                    }
                }
            }

            return img1;
        }

        public Mat DoBinary()
        {
            if (!SetImageReady) return null;
            
            if(!ManualBinary)
                DoAutoBinary();
            else
                DoManualBinary();

            if (FilterMask != 0)
            {
                Mat structElement = CvInvoke.GetStructuringElement(ElementShape.Ellipse,
                    new Size(FilterMask, FilterMask), new Point(-1, -1));

                CvInvoke.MorphologyEx(MatBinary, MatBinary, MorphOp.Close, structElement, new Point(-1, -1), 1,
                                     BorderType.Default, new MCvScalar(0, 0, 0));

                CvInvoke.MorphologyEx(MatBinary, MatBinary, MorphOp.Open, structElement, new Point(-1, -1), 1,
                                     BorderType.Default, new MCvScalar(0, 0, 0));
            }

            CvBlobs blobs = new CvBlobs();
            CvBlobDetector _blobDetector = new CvBlobDetector();
            _blobDetector.Detect(MatBinary.ToImage<Gray, byte>(), blobs);

            int maxwidth = 0;
            int width = 0;
            foreach (CvBlob item in blobs.Values)
            {
                width = item.BoundingBox.Right - item.BoundingBox.Left;
                if (width > maxwidth)   maxwidth = width;
            }

            Image<Gray, byte> img1 = MatBinary.ToImage<Gray, byte>();

            img1 = MatBinary.ToImage<Gray, byte>();

            foreach (CvBlob item in blobs.Values)
            {
                //Blob過小的時候視為雜訊
                if (item.BoundingBox.Right - item.BoundingBox.Left < maxwidth / 3)
                {
                    for (int y = item.BoundingBox.Top; y < item.BoundingBox.Bottom; y++)
                        for (int x = item.BoundingBox.Left; x < item.BoundingBox.Right; x++)
                            img1.Data[y, x, 0] = 0x00;
                }
            }

            MatBinary = img1.Mat;

            if (FillWafer)
            {
                Image<Gray, byte> img = MatBinary.ToImage<Gray, byte>();

                for (int x = 0; x < ROI.Width; x++)
                {
                    for (int y = 0; y < ROI.Height; y++)
                    {
                        if(0xFF == img.Data[y, x, 0] )
                        {
                            for (int k = y; k < ROI.Height; k++)
                                img.Data[k, x, 0] = 0xFF;

                            break;
                        }
                    }
                }

                MatBinary = img.Mat;
            }

            return MatBinary;
        }
        //最小二乘法直线拟合
        bool CalculateLineKB(List<Point> m_FoldList, ref double k, ref double b)
        {
            //最小二乘法直线拟合
            //m_FoldList为关键点(x,y)的链表
            //拟合直线方程(Y=kX+b)，k和b为返回值

            long lCount = m_FoldList.Count;
            if (lCount < 2) return false;

            double mX, mY, mXX, mXY;
            mX = mY = mXX = mXY = 0;

            foreach(Point pt in m_FoldList)
            {
                mX += pt.X;
                mY += pt.Y;
                mXX += pt.X*pt.X;
                mXY += pt.X*pt.Y;
            }


            if (mX * mX - mXX * lCount == 0) return false;

            k = (double)(mY * mX - mXY * lCount) / (double)(mX * mX - mXX * lCount);
            b = (double)(mY - mX * k) / (double)lCount;

            return true;
        }
        public void LeastSquaresFit(List<PointF> pts, ref double Ox, ref double Oy, ref double R)
        {
            if (pts.Count < 3)  return;
            
            double cent_x = 0.0, cent_y = 0.0, radius = 0.0;
            double sum_x = 0.0f, sum_y = 0.0f;
            double sum_x2 = 0.0f, sum_y2 = 0.0f;
            double sum_x3 = 0.0f, sum_y3 = 0.0f;
            double sum_xy = 0.0f, sum_x1y2 = 0.0f, sum_x2y1 = 0.0f;
            int N = pts.Count;
            double x, y, x2, y2;
            for (int i = 0; i < N; i++)
            {
                PointF Cpts = pts[i];
                x = (double)Cpts.X;
                y = (double)Cpts.Y;
                x2 = x * x;
                y2 = y * y;
                sum_x += x;
                sum_y += y;
                sum_x2 += x2;
                sum_y2 += y2;
                sum_x3 += x2 * x;
                sum_y3 += y2 * y;
                sum_xy += x * y;
                sum_x1y2 += x * y2;
                sum_x2y1 += x2 * y;
            }
            double C, D, E, G, H;
            double a, b, c;
            C = N * sum_x2 - sum_x * sum_x;
            D = N * sum_xy - sum_x * sum_y;
            E = N * sum_x3 + N * sum_x1y2 - (sum_x2 + sum_y2) * sum_x;
            G = N * sum_y2 - sum_y * sum_y;
            H = N * sum_x2y1 + N * sum_y3 - (sum_x2 + sum_y2) * sum_y;
            a = (H * D - E * G) / (C * G - D * D);
            b = (H * C - E * D) / (D * D - G * C);
            c = -(a * sum_x + b * sum_y + sum_x2 + sum_y2) / N;
            cent_x = a / (-2);
            cent_y = b / (-2);
            radius = Math.Sqrt(a * a + b * b - 4 * c) / 2;

            Ox = cent_x;
            Oy = cent_y;
            R = radius;
        }
        public bool FindFlatWaferNotch()
        {
            bool bReturn = true;
            //計算左側圓方程式
            List<PointF> LeftEage = new List<PointF>();
            for (int x =0 ; x < (int)(2 * (3840.0 / 5.0) + 0.5); x++)
            {
                PointF pt = new PointF
                {
                    X = (float)x,
                    Y = (float)edge[x]
                };
                LeftEage.Add(pt);
            }
            double Ox = 0.0;
            double Oy = 0.0;
            double R = 0.0;
            LeastSquaresFit(LeftEage, ref Ox, ref Oy, ref R);

            //計算右側邊界角度變化
            List<Point> RightEage = new List<Point>();
            for (int x = (int)(4 * (3840.0 / 5.0) + 0.5); x < 3840; x++)
            {
                Point pt = new Point
                {
                    X = x,
                    Y = edge[x]
                };
                RightEage.Add(pt);
            }

            double da1 = 0.0;
            double db1 = 0.0;
            CalculateLineKB(RightEage, ref da1, ref db1);
            List<Point> List1 = new List<Point>();
            List<Point> List2 = new List<Point>();

            for (int i = 0; i<10; i++)
            {
                List1.Add(RightEage[i]);
                List2.Add(RightEage[RightEage.Count - 1 - i]);
            }
            
            Point pt1 = new Point((int)(List1.Average(x => x.X) + 0.5), (int)(List1.Average(y => y.Y) + 0.5));
            Point pt2 = new Point((int)(List2.Average(x => x.X) + 0.5), (int)(List2.Average(y => y.Y) + 0.5));

            //計算Notch角度
            if(pt1.X - pt2.X == 0)
            {
                if(pt1.Y - pt2.Y > 0)
                {
                    Notch_Theta = 90.0;
                }
                else
                {
                    Notch_Theta = -90.0;
                }
            }
            else
                Notch_Theta = Math.Atan((double)(pt1.Y - pt2.Y) / (double)(pt1.X - pt2.X)) / Math.PI * 180;

            //計算直線與圓的交點(計算Notch點)
            double a = 1.0 + da1 * da1;
            double b = -2 * Ox + 2 * da1 * (db1 - Oy);
            double c = Ox * Ox + (db1 - Oy) * (db1 - Oy) - R * R;

            double dNotchX = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
            if (dNotchX > 3840.0)   dNotchX = (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);

            double dNotchY = da1 * dNotchX + db1;

            NotchPt.X = (int)(dNotchX + 0.5);
            NotchPt.Y = (int)(dNotchY + 0.5) + ROITop;

            NotchMMPt.X = (double)NotchPt.X / PixelPerMM;
            NotchMMPt.Y = (double)NotchPt.Y / PixelPerMM;

            R = (double)MachineParas.WaferRadius/ 1000.0 * PixelPerMM;
            FindRelateCircle(0, NotchPt.X, ref Ox, ref Oy, R);

            CenterPt.X = (int)Ox;
            CenterPt.Y = (int)Oy + ROITop;

            CenterMMPt.X = (double)CenterPt.X / PixelPerMM;
            CenterMMPt.Y = (double)CenterPt.Y / PixelPerMM;

            //------------------------------------------------------------------------------
            //計算圓方程式
            List<PointF> TopEage = new List<PointF>();
            for (int x = 0; x < NotchPt.X; x++)
            {
                PointF pt = new PointF
                {
                    X = (float)x,
                    Y = (float)edge[x]
                };
                TopEage.Add(pt);
            }

            LeastSquaresFit(TopEage, ref Ox, ref Oy, ref R);

            ////尋找最高的點
            double MaxTop = 99999999.0;
            double tempY = 0;
            int MaxX = 0;
            for (int x = 0; x < 3840; x++)
            {
                tempY = Oy - Math.Sqrt(R * R - (Ox - x) * (Ox - x));
                if (tempY < MaxTop)
                {
                    MaxTop = tempY;
                    MaxX = x;
                }
            }

            //----------------------------------------
            //紀錄最高點
            TopPt.X = MaxX;
            TopPt.Y = (int)(MaxTop + 0.5) + ROITop;

            TopMMPt.X = (double)TopPt.X / PixelPerMM;
            TopMMPt.Y = (double)TopPt.Y / PixelPerMM;
            //----------------------------------------


            return bReturn;
        }
        public bool FindCircleWaferInfo()
        {
            bool bReturn = true;

            double Ox = 0.0;
            double Oy = 0.0;
            double R = 0.0;

            ////計算相對圓心
            R = (double)MachineParas.WaferRadius / 1000.0 * PixelPerMM;
            FindRelateCircle(0, edge.Count() - 1, ref Ox, ref Oy, R);

            CenterPt.X = (int)Ox;
            CenterPt.Y = (int)Oy + ROITop;
            CenterMMPt.X = (double)CenterPt.X / PixelPerMM;
            CenterMMPt.Y = (double)CenterPt.Y / PixelPerMM;

            List<PointF> TopEage = new List<PointF>();
            int MinValue = edge.Min();

            for(int i = 0; i< 3840; i++)
            {
                if(edge[i] == MinValue)
                {
                    PointF pt = new PointF
                    {
                        X = (float)i,
                        Y = (float)edge[i]
                    };
                    TopEage.Add(pt);
                }
            }

            if(TopEage.Count == 1)
            {
                TopPt.X = (int)TopEage[0].X;
                TopPt.Y = (int)TopEage[0].Y + ROITop;
                TopMMPt.X = (double)TopPt.X / PixelPerMM;
                TopMMPt.Y = (double)TopPt.Y / PixelPerMM;
            }
            else
            {
                TopPt.X = (int)TopEage.Average(x => x.X);
                TopPt.Y = MinValue + ROITop;
                TopMMPt.X = (double)TopPt.X / PixelPerMM;
                TopMMPt.Y = (double)TopPt.Y / PixelPerMM;
            }

            NotchPt.X = 0;
            NotchPt.Y = 0;
            NotchMMPt.X = (double)NotchPt.X / PixelPerMM;
            NotchMMPt.Y = (double)NotchPt.Y / PixelPerMM;

            Notch_Theta = 0.0;

            return bReturn;
        }

        void FindRelateCircle(int index1, int index2, ref double Ox, ref double Oy, double R)
        {
            int x1 = index1;
            int x2 = index2;
            int y1 = edge[x1];
            int y2 = edge[x2];
            double r = R;

            if (y1 != y2)
            {
                double k1, k2;
                k1 = (double)(x1 - x2) / (double)(y1 - y2);
                k2 = (double)((x1*x1 - x2*x2) + (y1*y1 - y2*y2)) / (double)(2 * (y1 - y2));

                double a = 1 + k1* k1;
                double b = -2 * x1 - 2 * k1 * k2 + 2 * k1 * y1;
                double c = x1* x1 + k2* k2 - 2 * k2 * y1 + y1* y1 - r*r;

                Ox = (-b - Math.Sqrt(b* b - 4 * a * c)) / (2 * a);
                Oy = -k1 * Ox + k2;

                if(Oy < 0)
                {
                    Ox = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
                    Oy = -k1 * Ox + k2;
                }
            }
            else
            {
                Ox = (x1 + x2) / 2;
                Oy = y1 + Math.Sqrt(r*r - (Ox - x1)* (Ox - x1));
            }
        }
        public bool FindCircleWaferNotch()
        {
            bool bReturn = true;

            double Ox = 0.0;
            double Oy = 0.0;
            double R = 0.0;

            ////計算相對圓心
            R = (double)MachineParas.WaferRadius / 1000.0 * PixelPerMM;
            FindRelateCircle(0, edge.Count() - 1, ref Ox, ref Oy, R);

            CenterPt.X = (int)Ox;
            CenterPt.Y = (int)Oy + ROITop;
            CenterMMPt.X = (double)CenterPt.X / PixelPerMM;
            CenterMMPt.Y = (double)CenterPt.Y / PixelPerMM;

            List<PointF> Eage = new List<PointF>();
            for (int x = 0; x < (int)(2 * (3840.0 / 5.0) + 0.5); x++)
            {
                PointF pt = new PointF
                {
                    X = (float)x,
                    Y = (float)edge[x]
                };
                Eage.Add(pt);
            }

            for (int x = (int)(4 * (3840.0 / 5.0) + 0.5); x < 3840; x++)
            {
                Point pt = new Point
                {
                    X = x,
                    Y = edge[x]
                };
                Eage.Add(pt);
            }

            LeastSquaresFit(Eage, ref Ox, ref Oy, ref R);

            //尋找最高的點
            double MaxTop = 99999999.0;
            double tempY = 0;
            int MaxX = 0;
            for (int x = 0; x < 3840; x++)
            {
                tempY = Oy - Math.Sqrt(R * R - (Ox - x) * (Ox - x));
                if (tempY < MaxTop)
                {
                    MaxTop = tempY;
                    MaxX = x;
                }
            }

            //----------------------------------------
            //紀錄最高點
            TopPt.X = MaxX;
            TopPt.Y = (int)(MaxTop + 0.5) + ROITop;

            TopMMPt.X = (double)TopPt.X / PixelPerMM;
            TopMMPt.Y = (double)TopPt.Y / PixelPerMM;
            //----------------------------------------

            double minR = 999999999.0;
            double r;
            for (int x = 0; x < lowedge.Count(); x++)
            {
                r = Math.Sqrt((x - Ox) * (x - Ox) + (lowedge[x] - Oy) * (lowedge[x] - Oy));
                if (r < minR)
                    minR = r;
            }

            List<Point> RGroup = new List<Point>();

            for (int x = 0; x < edge.Count(); x++)
            {
                if (Math.Abs(Math.Sqrt((x - Ox) * (x - Ox) + (lowedge[x] - Oy) * (lowedge[x] - Oy)) - minR) < 0.001)
                {
                    Point NotchPt = new Point(x, lowedge[x]);
                    RGroup.Add(NotchPt);
                }
            }

            if (RGroup.Count() == 1)
            {
                NotchPt.X = RGroup[0].X;
                NotchPt.Y = RGroup[0].Y + ROITop;
            }
            else
            {
                double sumX = 0.0;
                double sumY = 0.0;

                for (int x = 0; x < RGroup.Count; x++)
                {
                    sumX += (double)RGroup[x].X;
                    sumY += (double)RGroup[x].Y;
                }

                NotchPt.X = (int)(sumX / (double)RGroup.Count + 0.5);
                NotchPt.Y = (int)(sumY / (double)RGroup.Count + 0.5) + ROITop;
            }

            NotchMMPt.X = (double)NotchPt.X / PixelPerMM;
            NotchMMPt.Y = (double)NotchPt.Y / PixelPerMM;


            //計算Notch角度
            if(NotchPt.X - CenterPt.X == 0)
            {
                if(NotchPt.Y - CenterPt.Y >= 0)
                {
                    Notch_Theta = 90.0;
                }
                else
                {
                    Notch_Theta = -90.0;
                }
            }
            else
                Notch_Theta = Math.Atan((double)(NotchPt.Y - CenterPt.Y) / (double)(NotchPt.X - CenterPt.X)) / Math.PI * 180;

            return bReturn;
        }

        public bool Calculate()
        {
            bool bResult = true;

            CalculateResult = "";

            try
            {
                if (null == DoBinary())
                {
                    bResult = false;
                    CalculateResult = "二值化異常";
                }
                else
                {
                    if (!FindEage())
                    {
                        bResult = false;
                        CalculateResult = "尋邊界異常";
                    }
                    else
                    {
                        //Wafer形式(Notch、Flat、Circle) 
                        if (MachineParas.WaferType == 0) //不得於平邊的時候
                            FindCircleWaferNotch();
                        else if(MachineParas.WaferType == 1)
                            FindFlatWaferNotch();
                        else
                            FindCircleWaferInfo();
                    }
                }
            }
            catch(Exception e)
            {
                logger.Debug(e.StackTrace);
            }


            return bResult;
        }
        private bool FindEage()
        {
            bool bResult = true;
            Image<Gray, byte> img1 = MatBinary.ToImage<Gray, byte>();
            HighEageList.Clear();
            LowEageList.Clear();

            for (int x = 0; x < ROI.Width; x++)
            {
                for(int y = 0; y < ROI.Height; y++)
                {
                    if(img1.Data[y, x, 0] == 0xFF)
                    {
                        Point pt = new Point
                        {
                            X = x,
                            Y = y
                        };
                        HighEageList.Add(x, pt);
                        break;
                    }
                }

                for(int y = ROI.Height-1; y >= 0; y--)
                {
                    if (img1.Data[y, x, 0] == 0xFF)
                    {
                        Point pt = new Point
                        {
                            X = x,
                            Y = y
                        };
                        LowEageList.Add(x,pt);
                        break;
                    }
                }
            }

            if(LowEageList.Count != HighEageList.Count)
            {
                bResult = false;
            }
            else if(LowEageList.Count != 3840)
            {
                bResult = false;
            }
            else if (HighEageList.Count != 3840)
            {
                bResult = false;
            }

            if(bResult)
            {
                if (LowEageList[0].Y == ROI.Height-1)
                {
                    for (int x = 0; x < 3840; x++)
                    {
                        Point highpt = HighEageList[x];
                        LowEageList[x] = highpt;
                    }
                }

                for(int x=0; x< 3840; x++)
                {
                    edge[x] = HighEageList[x].Y;
                    lowedge[x] = LowEageList[x].Y;
                }

            }

            return bResult;
        }

    }
}
