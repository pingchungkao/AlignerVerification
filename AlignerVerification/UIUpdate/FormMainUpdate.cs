using System;
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
                        //lb.Text = Math.Round(Statistics.NowOrigin.X, 5).ToString();
                        lb.Text = Math.Round(Statistics.NowTop.X, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbTopPosY", true).FirstOrDefault() as Label;
                    if (lb != null)
                        //lb.Text = Math.Round(Statistics.NowOrigin.Y, 5).ToString();
                        lb.Text = Math.Round(Statistics.NowTop.Y, 5).ToString();

                    lb = null;
                    lb = form.Controls.Find("lbAvgTopPosX", true).FirstOrDefault() as Label;
                    if (lb != null)
                        //lb.Text = Math.Round(Statistics.AvgOrigin.X, 5).ToString();
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
                        //lb.Text = Math.Round(Statistics.OOffset, 5).ToString();
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

    }
}
