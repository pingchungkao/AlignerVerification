﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

using log4net;
using AlignerVerification.Class;
using AlignerVerification.Comm;
using AlignerVerification.Controller;
using AlignerVerification.AOI;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;

namespace AlignerVerification.UIUpdate
{
    class FormMainUpdate
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(FormMainUpdate));

        delegate void UpdateConnectReport(DeviceConfig dc, EConnectionReport eConnectionReport);
        public static void ConnectReportUpdate(DeviceConfig dc, EConnectionReport eConnectionReport)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];

                if (form.InvokeRequired)
                {
                    UpdateConnectReport ph = new UpdateConnectReport(ConnectReportUpdate);
                    form.BeginInvoke(ph, dc, eConnectionReport);
                }
                else
                {
                    string DeviceName = "lb" + dc.DeviceName + "Status";

                    if (!(form.Controls.Find(DeviceName, true).FirstOrDefault() is Label lb)) return;

                    switch (eConnectionReport)
                    {
                        case EConnectionReport.eMessage:
                            //lb.BackColor
                            break;
                        case EConnectionReport.eConnecting:
                            lb.BackColor = Color.Red;
                            lb.Text = "未連線";
                            break;
                        case EConnectionReport.eConnected:
                            lb.BackColor = Color.Lime;
                            lb.Text = "連線";
                            break;
                        case EConnectionReport.eDisconnected:
                            lb.BackColor = Color.Red;
                            lb.Text = "斷線";
                            break;
                        case EConnectionReport.eError:
                            lb.BackColor = Color.Red;
                            lb.Text = "異常";
                            break;
                    }  
                    
                    //if(!eConnectionReport.Equals(EConnectionReport.eMessage))
                    //{
                    //    if(eConnectionReport.Equals(EConnectionReport.eConnected))
                    //    {
                    //        logger.Info(dc.DeviceName + " 連線狀況: " + lb.Text);
                    //    }
                    //    else
                    //    {
                    //        logger.Debug(dc.DeviceName + " 連線狀況: " + lb.Text);
                    //    }
                    //}
                
                }

            }
            catch (Exception e)
            {
                logger.Debug(e.StackTrace);
            }
        }
        delegate void UpdateAlignerTackTime(double TackTime);
        public static void AlignerTackTimeUpdate(double TackTime)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];

                if (form.InvokeRequired)
                {
                    UpdateAlignerTackTime ph = new UpdateAlignerTackTime(AlignerTackTimeUpdate);
                    form.BeginInvoke(ph, TackTime);
                }
                else
                {
                    Label lb = null;
                    lb = form.Controls.Find("lbTaskTime", true).FirstOrDefault() as Label;

                    if(lb != null)
                        lb.Text = Math.Round(TackTime, 5).ToString();
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.StackTrace);
            }
        }
        delegate void UpdateAlignerTackTimeInfo();
        public static void AlignerTackTimeInfoUpdate()
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];

                if (form.InvokeRequired)
                {
                    UpdateAlignerTackTimeInfo ph = new UpdateAlignerTackTimeInfo(AlignerTackTimeInfoUpdate);
                    form.BeginInvoke(ph);
                }
                else
                {
                    Label lb = null;
                    lb = form.Controls.Find("lbTaskTime", true).FirstOrDefault() as Label;

                    if (lb != null)
                        lb.Text = Math.Round(Statistics.NowTackTime, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbAvgTaskTime", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = Math.Round(Statistics.AvgTackTime, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbMaxTaskTime", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = Math.Round(Statistics.MaxTackTime, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbMinTaskTime", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = Math.Round(Statistics.MinTackTime, 5).ToString();

                }
            }
            catch (Exception e)
            {
                logger.Debug(e.StackTrace);
            }
        }
        delegate void UpdateMessageLog(string type, string msg);
        public static void MessageLogUpdate(string type, string msg)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];

                if (form.InvokeRequired)
                {
                    UpdateMessageLog ph = new UpdateMessageLog(MessageLogUpdate);
                    form.BeginInvoke(ph, type, msg);
                }
                else
                {
                    RichTextBox rtb = null;
                    rtb = form.Controls.Find("RichTextBox1", true).FirstOrDefault() as RichTextBox;

                    rtb.AppendText(type +" : "+msg + "\n");
                    //logger.Debug(type + " : " + msg + "\n");
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.StackTrace);
            }
        }
        delegate void UpdateDisplayImage(string RootDirectory, Mat mat, Tool AOITool, float ZoomRadioX, float ZoomRadioY, int Cnt, bool saveImg);
        public static void DisplayImageUpdate(string RootDirectory, Mat mat, Tool AOITool, float ZoomRadioX, float ZoomRadioY, int Cnt ,bool saveImg)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                if (form.InvokeRequired)
                {
                    UpdateDisplayImage ph = new UpdateDisplayImage(DisplayImageUpdate);
                    form.BeginInvoke(ph, RootDirectory, mat, AOITool, ZoomRadioX, ZoomRadioY, Cnt, saveImg);
                }
                else
                {
                    ImageBox imgBox = null;
                    imgBox = form.Controls.Find("DisplayImageBox", true).FirstOrDefault() as ImageBox;

                    Mat DrawMat = mat.Clone();

                    int r = (int)(Math.Sqrt(Math.Pow(AOITool.TopPt.X - AOITool.CenterPt.X, 2) + Math.Pow(AOITool.TopPt.Y - AOITool.CenterPt.Y, 2)) + 0.5);
                    CvInvoke.Circle(DrawMat, AOITool.CenterPt, r, new MCvScalar(0, 255, 0),10);

                    if (MachineParas.WaferType == 1 && !AOITool.NotchMark)
                    {
                        CvInvoke.Line(DrawMat, AOITool.EndPt, AOITool.NotchPt, new MCvScalar(255, 0, 255), 10);
                    }

                    Point topPoint = new Point(AOITool.TopPt.X, AOITool.TopPt.Y);
                    DrawCrossPoint(DrawMat, topPoint, new MCvScalar(255, 0, 0), 25, 10);

                    Point NotchPoint = new Point(AOITool.NotchPt.X, AOITool.NotchPt.Y);
                    DrawCrossPoint(DrawMat, NotchPoint, new MCvScalar(0, 0, 255), 25, 10);

                    imgBox.Image = DrawMat;
                    imgBox.Refresh();

                    form.Refresh();

                    //圓心與Notch位置
                    Label lb = null;
                    lb = form.Controls.Find("lbShowCurrentCnt", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = "n = " + Cnt.ToString();
                    
                    lb = form.Controls.Find("lbTopPosX", true).FirstOrDefault() as Label;
                    if(lb!= null)
                        lb.Text = Math.Round(Statistics.NowTop.X, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbTopPosY", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = Math.Round(Statistics.NowTop.Y, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbAvgTopPosX", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = Math.Round(Statistics.AvgTop.X, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbAvgTopPosY", true).FirstOrDefault() as Label;
                    if (lb != null)
                        //lb.Text = Math.Round(Statistics.AvgOrigin.Y, 5).ToString();
                        lb.Text = Math.Round(Statistics.AvgTop.Y, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbNotchPosX", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = Math.Round(Statistics.NowNotch.X, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbNotchPosY", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = Math.Round(Statistics.NowNotch.Y, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbAvgNotchPosX", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = Math.Round(Statistics.AvgNotch.X, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbAvgNotchPosY", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = Math.Round(Statistics.AvgNotch.Y, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbToffset", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = Math.Round(Statistics.TOffset, 5).ToString();
                    lb = null;
                    lb = form.Controls.Find("lbNoffset", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = Math.Round(Statistics.NOffset, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbNDegoffset", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = Math.Round(Statistics.NOffsetDeg, 3).ToString();

                    if(Statistics.TOffset > MachineParas.dOOffsetUpLimit)
                    {
                        lb = null;
                        lb = form.Controls.Find("ShowToffsetLabel", true).FirstOrDefault() as Label;
                        if (lb != null)
                            lb.ForeColor = Color.Red;

                        lb = null;
                        lb = form.Controls.Find("lbToffset", true).FirstOrDefault() as Label;
                        if (lb != null)
                            lb.ForeColor = Color.Red;

                    }

                    if (Statistics.NOffsetDeg > MachineParas.dNOffsetUpLimit)
                    {
                        lb = null;
                        lb = form.Controls.Find("ShowNDegoffsetLabel", true).FirstOrDefault() as Label;
                        if (lb != null)
                            lb.ForeColor = Color.Red;

                        lb = null;
                        lb = form.Controls.Find("lbNDegoffset", true).FirstOrDefault() as Label;
                        if (lb != null)
                            lb.ForeColor = Color.Red;

                    }



                    lb = null;
                    lb = form.Controls.Find("lbCpkMMOffset", true).FirstOrDefault() as Label;
                    if (lb != null)
                    {
                        lb.Visible = false;
                        lb.Text = string.Format("Cpk(mm)_{0:F3}", Statistics.CpkOffsetT);
                    }


                    lb = null;
                    lb = form.Controls.Find("lbCpkDegOffset", true).FirstOrDefault() as Label;
                    if (lb != null)
                    {
                        lb.Visible = false;
                        lb.Text = string.Format("Cpk(Deg)_{0:F3}", Statistics.CpkOffsetDeg);
                    }

                    lb = null;
                    lb = form.Controls.Find("lbCpkMMXOffset", true).FirstOrDefault() as Label;
                    if (lb != null)
                    {
                        lb.Visible = false;
                        lb.Text = string.Format("Cpk(X_mm)_{0:F3}", Statistics.CpkXOffsetT);
                    }

                    lb = null;
                    lb = form.Controls.Find("lbCpkMMYOffset", true).FirstOrDefault() as Label;
                    if (lb != null)
                    {
                        lb.Visible = false;
                        lb.Text = string.Format("Cpk(Y_mm)_{0:F3}", Statistics.CpkYOffsetT);
                    }

                    if(Cnt > 2)
                    {
                        lb = null;
                        lb = form.Controls.Find("lbRPi", true).FirstOrDefault() as Label;
                        if (lb != null)
                        {
                            lb.Text = string.Format("RPi_{0:F3}", Statistics.RPi);
                        }

                        lb = null;
                        lb = form.Controls.Find("lbRPa", true).FirstOrDefault() as Label;
                        if (lb != null)
                        {
                            lb.Text = string.Format("RPa_{0:F3}", Statistics.RPa);
                        }
                    }


                    form.Refresh();

                    if (saveImg)
                    {
                        string strFileName = RootDirectory + @"\n" + Cnt.ToString() + @".jpg";
                        Bitmap bit = new Bitmap(form.Width, form.Height);//例項化一個和窗體一樣大的bitmap
                        form.DrawToBitmap(bit, new Rectangle(0, 0, form.Width, form.Height));
                        bit.Save(strFileName);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.StackTrace);
            }
        }

        delegate void UpdateRepeaTestInfo(string RootDirectory, Mat mat, Tool AOITool, float ZoomRadioX, float ZoomRadioY, int Cnt);
        public static void RepeaTestInfoUpdate(string RootDirectory, Mat mat, Tool AOITool, float ZoomRadioX, float ZoomRadioY, int Cnt)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                if (form.InvokeRequired)
                {
                    UpdateRepeaTestInfo ph = new UpdateRepeaTestInfo(RepeaTestInfoUpdate);
                    form.BeginInvoke(ph, RootDirectory, mat, AOITool, ZoomRadioX, ZoomRadioY, Cnt);                
                }
                else
                {
                    ImageBox imgBox = null;
                    imgBox = form.Controls.Find("DisplayImageBox", true).FirstOrDefault() as ImageBox;

                    Mat DrawMat = mat.Clone();
                    //重複精度
                    Point Pt1 = new Point(AOITool.RepeatPt1.X, AOITool.RepeatPt1.Y);
                    Point Pt2 = new Point(AOITool.RepeatPt2.X, AOITool.RepeatPt2.Y);
                    Point Pt3 = new Point(AOITool.RepeatPt3.X, AOITool.RepeatPt3.Y);
                    Point PtO = new Point(AOITool.CenterPt.X, AOITool.CenterPt.Y);

                    DrawCrossPoint(DrawMat, Pt1, new MCvScalar(255, 0, 0), 30, 15);
                    DrawCrossPoint(DrawMat, Pt2, new MCvScalar(255, 0, 0), 30, 15);
                    DrawCrossPoint(DrawMat, Pt3, new MCvScalar(255, 0, 0), 30, 15);

                    imgBox.Image = DrawMat;
                    imgBox.Refresh();

                    Label lb = null;
                    lb = form.Controls.Find("lbShowCurrentCnt", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = "n = " + Cnt.ToString();

                    lb = form.Controls.Find("lbRepeatP1Pox", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = string.Format("P1 x = {0:F3}, y = {1:F3}", Pt1.X / AOITool.PixelPerMM, Pt1.Y / AOITool.PixelPerMM);

                    lb = null;
                    lb = form.Controls.Find("lbRepeatP2Pox", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = string.Format("P2 x = {0:F3}, y = {1:F3}", Pt2.X / AOITool.PixelPerMM, Pt2.Y / AOITool.PixelPerMM);

                    lb = null;
                    lb = form.Controls.Find("lbRepeatP3Pox", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = string.Format("P3 x = {0:F3}, y = {1:F3}", Pt3.X / AOITool.PixelPerMM, Pt3.Y / AOITool.PixelPerMM);

                    lb = null;
                    lb = form.Controls.Find("lbRepeatPOPox", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = string.Format("PO x = {0:F3}, y = {1:F3}", PtO.X / AOITool.PixelPerMM, PtO.Y / AOITool.PixelPerMM);
            
                    lb = null;
                    lb = form.Controls.Find("lbRepeatP1P2FullRange", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = string.Format("P1P2 Angle FullRange {0:F3}", Statistics.RepeatPT1PT2AngleOffset);

                    lb = null;
                    lb = form.Controls.Find("lbRepeatP1P3FullRange", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = string.Format("P1P3 Angle FullRange {0:F3}", Statistics.RepeatPT1PT3AngleOffset);

                    lb = null;
                    lb = form.Controls.Find("lbRepeatP1POFullRange", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = string.Format("P1PO Angle FullRange {0:F3}", Statistics.RepeatPT1PTOAngleOffset);

                    lb = null;
                    lb = form.Controls.Find("lbRepeatP2POFullRange", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = string.Format("P2PO Angle FullRange {0:F3}", Statistics.RepeatPT2PTOAngleOffset);

                    lb = null;
                    lb = form.Controls.Find("lbRepeatP3POFullRange", true).FirstOrDefault() as Label;
                    if (lb != null)
                        lb.Text = string.Format("P3PO Angle FullRange {0:F3}", Statistics.RepeatPT3PTOAngleOffset);

                    form.Refresh();
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.StackTrace);
            }
        }


        delegate void UpdateShowImage(Mat mat);
        public static void ShowImageUpdate(Mat mat)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                if (form.InvokeRequired)
                {
                    UpdateShowImage ph = new UpdateShowImage(ShowImageUpdate);
                    form.BeginInvoke(ph, mat);
                }
                else
                {
                    ImageBox imgbox = null;
                    imgbox = form.Controls.Find("FilterImageBox", true).FirstOrDefault() as ImageBox;
                    if(imgbox != null)
                    {
                        imgbox.Visible = false;
                        imgbox.Refresh();
                    }

                    imgbox = form.Controls.Find("DisplayImageBox", true).FirstOrDefault() as ImageBox;
                    if(imgbox != null)
                    {
                        imgbox.Image = mat;
                        imgbox.Refresh();

                        using (Pen pen = new Pen(Color.Red, 1))
                        {
                            pen.DashStyle = DashStyle.Dash;
                            Graphics eGraphics = imgbox.CreateGraphics();

                            Point HSPT = new Point(0, imgbox.Height / 2);
                            Point HEPT = new Point(imgbox.Width, imgbox.Height / 2);
                            Point VSPT = new Point(imgbox.Width / 2, 0);
                            Point VEPT = new Point(imgbox.Width / 2, imgbox.Height);

                            eGraphics.DrawLine(pen, HSPT, HEPT);
                            eGraphics.DrawLine(pen, VSPT, VEPT);

                            pen.DashStyle = DashStyle.Dash;
                            pen.Color = Color.Blue;
                            for (int i = 1; i < 5; i++)
                            {
                                Point a = new Point(imgbox.Width / 5 * i, 0);
                                Point b = new Point(imgbox.Width / 5 * i, imgbox.Height);

                                eGraphics.DrawLine(pen, a, b);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.StackTrace);
            }

        }
        private static void DrawCrossPoint(Mat mat, Point point, MCvScalar color, int length, int thickness)
        {
            Point crossPt1 = new Point(point.X - length, point.Y - length);
            Point crossPt2 = new Point(point.X + length, point.Y - length);
            Point crossPt3 = new Point(point.X - length, point.Y + length);
            Point crossPt4 = new Point(point.X + length, point.Y + length);
            CvInvoke.Line(mat, crossPt1, crossPt4, color, thickness);
            CvInvoke.Line(mat, crossPt2, crossPt3, color, thickness);
        }
        delegate void CopyScreen(string FileName);
        public static void ScreenCopy(string FileName)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                if (form.InvokeRequired)
                {
                    CopyScreen ph = new CopyScreen(ScreenCopy);
                    form.BeginInvoke(ph, FileName);
                }
                else
                {
                    Bitmap bit = new Bitmap(form.Width, form.Height);//例項化一個和窗體一樣大的bitmap
                    //form.DrawToBitmap(bit, form.ClientRectangle);
                    Graphics g = Graphics.FromImage(bit);
                    g.CompositingQuality = CompositingQuality.HighQuality;//質量設為最高
                    g.CopyFromScreen(form.Left, form.Top, 0, 0, new Size(form.Width, form.Height));//儲存整個窗體為圖片

                    //IntPtr dc1 = g.GetHdc();
                    //g.ReleaseHdc(dc1);

                    bit.Save(FileName);
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.StackTrace);
            }
        }
        delegate void UpdatePresentMonitorTakeTime(TimeSpan ts);
        public static void PresentMonitorTakeTimeUpdate(TimeSpan ts)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                if (form.InvokeRequired)
                {
                    UpdatePresentMonitorTakeTime ph = new UpdatePresentMonitorTakeTime(PresentMonitorTakeTimeUpdate);
                    form.BeginInvoke(ph, ts);
                }
                else
                {
                    Label lb = null;
                    lb = form.Controls.Find("lbPresentTakeTime", true).FirstOrDefault() as Label;
                    if(lb != null)
                    {
                        lb.Text = ts.Hours.ToString().PadLeft(2,'0') + ":" + 
                            ts.Minutes.ToString().PadLeft(2, '0') + ":" + 
                            ts.Seconds.ToString().PadLeft(2, '0');
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.StackTrace);
            }
        }
        delegate void UpdateCommTestStatus(string devicename);
        public static void CommTestStatusUpdate(string devicename)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                if (form.InvokeRequired)
                {
                    UpdateCommTestStatus ph = new UpdateCommTestStatus(CommTestStatusUpdate);
                    form.BeginInvoke(ph, devicename);
                }
                else
                {
                    Label lb = null;
                    lb = form.Controls.Find("lb" + devicename + "CommTest", true).FirstOrDefault() as Label;

                    if(lb != null)
                        lb.BackColor = Color.LimeGreen;
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.StackTrace);
            }
        }
    }
}
