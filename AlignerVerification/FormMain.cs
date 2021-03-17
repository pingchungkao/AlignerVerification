using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

using log4net;
using log4net.Config;
using AlignerVerification.Class;
using AlignerVerification.Comm;
using AlignerVerification.Controller;
using AlignerVerification.UIUpdate;
using AlignerVerification.AOI;

using Microsoft.Win32;  //Registry


using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;



namespace AlignerVerification
{

    public partial class FormMain : Form, IConnectionReport
    {
        private bool loadParas = false;

        //private MachineParas machineParas = new MachineParas();

        private static readonly ILog logger = LogManager.GetLogger(typeof(FormMain));

        public static Dictionary<string, DeviceConfig> deviceConfig = new Dictionary<string, DeviceConfig>();
        //Device
        public static Dictionary<string, DeviceController> deviceMap = new Dictionary<string, DeviceController>();

        //IO
        public static Dictionary<int, IODevice> rioMap = new Dictionary<int, IODevice>();

        public Camera cameraBasic;

        private double ZoomRadioX;
        private double ZoomRadioY;

        //防止連擊
        DateTime PreventMultiClick;

        DateTime TackSTime;
        double TackTime;

        BackgroundWorker bwAlignerIni = new BackgroundWorker();
        BackgroundWorker bwDoAlign = new BackgroundWorker();
        BackgroundWorker bwDoAutoRun = new BackgroundWorker();
        BackgroundWorker bwDownloadData = new BackgroundWorker();
        BackgroundWorker bwMonitorPresent = new BackgroundWorker();
        BackgroundWorker bwContinousTest = new BackgroundWorker();
        BackgroundWorker bwTest = new BackgroundWorker();

        //將Wafer置中
        BackgroundWorker bwDoWaferAlignment = new BackgroundWorker();

        //重複性測試
        BackgroundWorker bwDoRepeatMotionTest = new BackgroundWorker();

        public FormMain()
        {
            InitializeComponent();

            XmlConfigurator.Configure();//Log4N 需要

            bwAlignerIni.DoWork += new DoWorkEventHandler(DoAlignerIni);
            bwAlignerIni.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AlignerIniCompleted);

            bwDoAlign.DoWork += new DoWorkEventHandler(DoAlign);
            bwDoAlign.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AlignerIniCompleted);

            bwDoAutoRun.DoWork += new DoWorkEventHandler(DoAutoRun);
            bwDoAutoRun.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DoAutoRunCompleted);

            bwDownloadData.DoWork += new DoWorkEventHandler(DoDownloadData);
            bwDownloadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AlignerIniCompleted);

            bwMonitorPresent.DoWork += new DoWorkEventHandler(DoMonitorPresent);
            bwMonitorPresent.RunWorkerCompleted += new RunWorkerCompletedEventHandler(MonitorCompleted);

            bwContinousTest.WorkerReportsProgress = true;
            bwContinousTest.DoWork += new DoWorkEventHandler(DoContinousTest);
            bwContinousTest.ProgressChanged += new ProgressChangedEventHandler(ContinousTestProgressChanged);
            bwContinousTest.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ContinousTestCompleted);

            bwDoWaferAlignment.DoWork += new DoWorkEventHandler(DoWaferAlignment);
            bwDoWaferAlignment.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AlignerIniCompleted);

            bwDoRepeatMotionTest.DoWork += new DoWorkEventHandler(DoRepeatMotionTest);
            bwDoRepeatMotionTest.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AlignerIniCompleted);

            IODevice ioDevice = null;
            ioDevice = new IODevice()
            {
                No = 0,
                IOName = "Hold Control",
                Type = "Output"
            };
            rioMap.Add(ioDevice.No, ioDevice);

            ioDevice = new IODevice()
            {
                No = 1,
                IOName = "Release Control",
                Type = "Output"
            };
            rioMap.Add(ioDevice.No, ioDevice);

            ioDevice = new IODevice()
            {
                No = 2,
                IOName = "Reserved",
                Type = "Undefined"
            };
            rioMap.Add(ioDevice.No, ioDevice);

            ioDevice = new IODevice()
            {
                No = 3,
                IOName = "Reserved",
                Type = "Undefined"
            };
            rioMap.Add(ioDevice.No, ioDevice);

            ioDevice = new IODevice()
            {
                No = 4,
                IOName = "Hold Status",
                Type = "Input"
            };
            rioMap.Add(ioDevice.No, ioDevice);

            ioDevice = new IODevice()
            {
                No = 5,
                IOName = "Reserved",
                Type = "Undefined"
            };
            rioMap.Add(ioDevice.No, ioDevice);

            ioDevice = new IODevice()
            {
                No = 6,
                IOName = "Reserved",
                Type = "Undefined"
            };
            rioMap.Add(ioDevice.No, ioDevice);

            ioDevice = new IODevice()
            {
                No = 7,
                IOName = "Reserved",
                Type = "Undefined"
            };
            rioMap.Add(ioDevice.No, ioDevice);

            ioDevice = new IODevice()
            {
                No = 8,
                IOName = "Present",
                Type = "Input"
            };
            rioMap.Add(ioDevice.No, ioDevice);

            ioDevice = new IODevice()
            {
                No = 9,
                IOName = "Reserved",
                Type = "Undefined"
            };
            rioMap.Add(ioDevice.No, ioDevice);

            ioDevice = new IODevice()
            {
                No = 10,
                IOName = "Controller Fan1",
                Type = "Input"
            };
            rioMap.Add(ioDevice.No, ioDevice);


            cameraBasic = new Camera
            {
                MatFrame = new Mat(),
                FileBuffer = new ImageBuffer(),
                ImageBuffer = new ImageBuffer(),
                AOITool = new Tool()
            };

            ZoomRadioX = (double)DisplayImageBox.Width / 3840.0;
            ZoomRadioY = (double)DisplayImageBox.Height / 2748.0;

            MachineParas.OutputFolder = Environment.CurrentDirectory;

        }
        void DoAlignerIni(object sender, DoWorkEventArgs e)
        {
            FormMainUpdate.MessageLogUpdate("DoAlignerIni", "$1SET:RESET");
            SetAlignerCommand("$1SET:RESET");

            if (!EvtManager.AlignerResetFinishEvt.WaitOne(5000))
            {
                FormMainUpdate.MessageLogUpdate("DoAlignerIni()", "$1SET:RESET Timeout");
                return;
            }

            FormMainUpdate.MessageLogUpdate("DoAlignerIni", "$1SET:RESET Finish");

            if (MachineParas.MachineType == 0)
            {
                string waferType = ",0";

                if (MachineParas.WaferType == 0)
                {
                    waferType = ",0";
                }
                else if (MachineParas.WaferType == 1)
                {
                    waferType = ",5";
                }
                else
                {
                    waferType = ",6";
                }


                FormMainUpdate.MessageLogUpdate("DoAlignerIni", "$1SET:ALIGN:"+ MachineParas.WaferRadius.ToString() + waferType);

                EvtManager.AlignerSetAlignACKEvt.Reset();
                //設定Wafer Radius
                SetAlignerCommand("$1SET:ALIGN:" + MachineParas.WaferRadius.ToString() + waferType);

                //設定WaferRadius
                if (!EvtManager.AlignerSetAlignACKEvt.WaitOne(10000))
                {
                    FormMainUpdate.MessageLogUpdate("DoAlignerIni", "$1SET:ALIGN:" + MachineParas.WaferRadius.ToString() + "Timeout");
                    return;
                }

                FormMainUpdate.MessageLogUpdate("DoAlignerIni", "$1SET:ALIGN:"+ MachineParas.WaferRadius.ToString() + "Finish");
            }

            //設定速度
            FormMainUpdate.MessageLogUpdate("DoAlignerIni", "SetAlignerSpeed");
            if (!SetAlignerSpeed()) return;
            FormMainUpdate.MessageLogUpdate("DoAlignerIni", "SetAlignerSpeed Finish");
            if (MachineParas.MachineType == 0)
            {
                //Move Up
                FormMainUpdate.MessageLogUpdate("DoAlignerIni", "DoAlignerRelease");
                if (!DoAlignerRelease()) return;
                FormMainUpdate.MessageLogUpdate("DoAlignerIni", "DoAlignerRelease Finish");
                DoCylinderJob("MoveUp");
            }

            //回原點
            FormMainUpdate.MessageLogUpdate("DoAlignerIni", "$1CMD:ORG__");
            SetAlignerCommand("$1CMD:ORG__");
            if (!EvtManager.AlignerORGFinishEvt.WaitOne(10000))
            {
                FormMainUpdate.MessageLogUpdate("DoAlignerIni()", "$1CMD:ORG__ Timeout");
                return;
            }
            FormMainUpdate.MessageLogUpdate("DoAlignerIni", "$1CMD:ORG__ Finish");

            //回Home點
            FormMainUpdate.MessageLogUpdate("DoAlignerIni", "DoAlignerHome()");
            if (!DoAlignerHome()) return;
            FormMainUpdate.MessageLogUpdate("DoAlignerIni", "DoAlignerHome()_Finish");
        }
        private void AlignerIniCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GeneralSetting(true);
        }
        private void DoAutoRunCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GeneralSetting(true);
            RunButton.Text = "Run";
        }
        private void MonitorCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GeneralSetting(true);

            lbEndMonitorTime.Text = DateTime.Now.ToString("HH:mm:ss");

            if(MonitorFinish)
            {
                lbPresentStatus.BackColor = Color.Lime;
                lbPresentStatus.Text = "狀態正常";
            }
            else
            {
                lbPresentStatus.BackColor = Color.Red;
                lbPresentStatus.Text = "狀態異常";
            }

            btnStartMonitorPresent.Text = "開始";

        }
        private void GeneralSetting(bool enabled)
        {
            string strEnabled = enabled ? "True" : "False";
            logger.Debug(@"GeneralSetting : GeneralSetting("+ strEnabled + ")");

            pnGeneral.Enabled = enabled;
            gbSingleMotion.Enabled = enabled;
            gbAOI.Enabled = enabled;
            gbAdvance.Enabled = enabled;
            gbPresentTest.Enabled = enabled;

            gbMotionTest.Enabled = enabled;
        }
        void IConnectionReport.On_Message_Log(string Type, string Message)
        {
            FormMainUpdate.MessageLogUpdate(Type, Message);
        }
        void IConnectionReport.On_Connection_Message(object Msg)
        {
            DeviceMsg dcmsg = (DeviceMsg)Msg;

            string returnMsg = dcmsg.MsgObject;

            switch (dcmsg.config.DeviceName.ToUpper())
            {
                case "CYLINDER":
                    EvtManager.CylinderFinishEvt.Set();
                    break;
                case "CAMERA":
                    EvtManager.CameraGrabFinishEvt.Set();
                    break;

                case "ALIGNER":
                case "ALIGNER02":
                    DeviceController dc = deviceMap[dcmsg.config.DeviceName];
                    string ReturnComand = returnMsg.Length >= 11 ? returnMsg.Substring(6, 5) : "";
                    string ReturnType = "";
                    if(returnMsg.Contains("$1ACK"))
                    {
                        ReturnType = "ACK";
                    }
                    else if(returnMsg.Contains("$1FIN"))
                    {
                        ReturnType = "FIN";
                    }
                    else if(returnMsg.Contains("NAK"))
                    {
                        ReturnType = "NAK";
                    }

                    if(returnMsg.Contains("ACK:ALIGN:9"))
                    {
                        ReturnComand = "ALIGN";
                    }

                    string ReturnCode;
                    string[] lines;

                    switch (ReturnType.ToUpper())
                    {
                        case "ACK":
                            switch (ReturnComand.ToUpper())
                            {
                                case "WHLD_":
                                case "WHLS_":
                                    break;
                                case "RESET":
                                    EvtManager.AlignerResetFinishEvt.Set();
                                    break;
                                case "ALIGN":
                                    dc.RawData = returnMsg;
                                    EvtManager.AlignerSetAlignACKEvt.Set();
                                    break;
                                case "SP___":
                                    EvtManager.AlignerSetSpeedFinishEvt.Set();
                                    break;

                                case "RIO__":
                                    lines = returnMsg.Split(':');
                                    lines[2] = lines[2].Trim();
                                    string[] ioDevice = lines[2].Split(',');
                                    //ioDevice[0] bit
                                    //ioDevice[1] status

                                    if(Int32.TryParse(ioDevice[0],out int ibit) && Int32.TryParse(ioDevice[1], out int istatus))
                                    {
                                        rioMap.TryGetValue(ibit, out IODevice IO);
                                        IO.Status = istatus;
                                        EvtManager.AlignerGetRIOEvt.Set();
                                    }

                                    break;
                                case "VER__":
                                    FormMainUpdate.CommTestStatusUpdate(dcmsg.config.DeviceName);

                                    lines = returnMsg.Split(':');
                                    FormMainUpdate.MessageLogUpdate(dcmsg.config.DeviceName, lines[2]);
                                    FormMainUpdate.MessageLogUpdate(dcmsg.config.DeviceName, "通訊測試結束");
                                    break;

                                case "LOGSV":
                                    EvtManager.AlignerLOGSVFinishEvt.Set();
                                    break;
                                
                                default:
                                    break;

                            }
                            //dc.DataReceived = "";
                            break;
                        case "FIN":
                            ReturnCode = returnMsg.Substring(12, 8);
                            dc.FINReturnCode = ReturnCode;
                            if(!dc.FINReturnCode.Equals("00000000"))
                                FormMainUpdate.MessageLogUpdate("Aligner", "Recev:" + returnMsg);

                            switch (ReturnComand.ToUpper())
                            {
                                case "WHLD_":
                                    EvtManager.AlignerWHLDFinishEvt.Set();
                                    break;

                                case "WRLS_":
                                    EvtManager.AlignerWRLSFinishEvt.Set();
                                    break;

                                case "ALIGN":
                                    EvtManager.AlignerSetAlignFinishEvt.Set();

                                    break;

                                case "ORG__":
                                    EvtManager.AlignerORGFinishEvt.Set();
                                    break;

                                case "HOME_":
                                    EvtManager.AlignerHOMEFinishEvt.Set();
                                    break;
                                case "MOVED":
                                    EvtManager.AlignerMoveFinishEvt.Set();
                                    break;

                                case "MOVDP":
                                    EvtManager.AlignerMovdpFinishEvt.Set();
                                    break;
                            }
                            dc.DataReceived = "";
                            break;
                        case "NAK":
                            ReturnCode = returnMsg.Substring(12, 8);

                            FormMainUpdate.MessageLogUpdate("Aligner", "Recev:" + returnMsg);
                            switch (ReturnComand.ToUpper())
                            {
                                case "WHLD_":
                                case "WHLS_":
                                    break;
                            }
                            //dc.DataReceived = "";
                            break;
                        case "":
                            //dc.DataReceived = returnMsg;
                            //if (dc.RawData != "")
                            //    dc.RawData += "\r";
                            //dc.RawData += dc.DataReceived;
                            break;
                        default:
                            break;
                    }

                    break;

                default:
                    break;
            }
        }
        void IConnectionReport.On_Connection_Connecting(object Msg)
        {
            DeviceConfig dc = (DeviceConfig)Msg;
            FormMainUpdate.ConnectReportUpdate(dc, EConnectionReport.eConnecting);
            FormMainUpdate.MessageLogUpdate(dc.DeviceName, "未連線");

        }
        void IConnectionReport.On_Connection_Connected(object Msg)
        {
            DeviceConfig dc = (DeviceConfig)Msg;
            FormMainUpdate.ConnectReportUpdate(dc, EConnectionReport.eConnected);
            FormMainUpdate.MessageLogUpdate(dc.DeviceName, "連線完成");
        }
        void IConnectionReport.On_Connection_Disconnected(object Msg)
        {
            DeviceConfig dc = (DeviceConfig)Msg;
            FormMainUpdate.ConnectReportUpdate(dc, EConnectionReport.eDisconnected);
            FormMainUpdate.MessageLogUpdate(dc.DeviceName, "斷線");
        }
        void IConnectionReport.On_Connection_Error(object Msg)
        {
            DeviceConfig dc = (DeviceConfig)Msg;
            FormMainUpdate.ConnectReportUpdate(dc, EConnectionReport.eError);
            FormMainUpdate.MessageLogUpdate(dc.DeviceName, "異常");
        }
        private void ShowTaskTimeLabel_Click(object sender, EventArgs e)
        {

        }
        private void NotchTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////Aligner形式
            MachineParas.MachineType = cmbNotchType.SelectedIndex;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string strCom;
            for (int i = 0; i<100; i++)
            {
                strCom = "COM" + i.ToString();
                cmbCylinderPort.Items.Add(strCom);
                cmbAlignerPort.Items.Add(strCom);
                cmbCameraPort.Items.Add(strCom);
                cmbAligner02Port.Items.Add(strCom);
            }
            DeviceConfig CylinderDC = new DeviceConfig
            {
                DeviceName = "Cylinder",
                Enable = true,
                PortName = "COM24",
                BaudRate = 38400,
                Vendor = "SMC",
                ConnectionType = "ComPort",
                DataBits = 8,
                ParityBit = "None",
                StopBit = "One",
                IPAdress = "192.168.0.135",
                Port = 23
            };
            deviceConfig.Add(CylinderDC.DeviceName, CylinderDC);

            DeviceConfig AlignerDC = new DeviceConfig
            {
                DeviceName = "Aligner",
                Enable = true,
                PortName = "COM24",
                BaudRate = 38400,
                Vendor = "Sanwa",
                ConnectionType = "Socket",
                DataBits = 8,
                ParityBit = "None",
                StopBit = "One",
                IPAdress = "192.168.0.135",
                Port = 23
            };
            deviceConfig.Add(AlignerDC.DeviceName, AlignerDC);

            DeviceConfig CameraDC = new DeviceConfig
            {
                DeviceName = "Camera",
                Enable = true,
                PortName = "COM24",
                BaudRate = 38400,
                Vendor = "Sanwa",
                ConnectionType = "ComPort",
                DataBits = 8,
                ParityBit = "None",
                StopBit = "One",
                IPAdress = "192.168.0.135",
                Port = 23
            };
            deviceConfig.Add(CameraDC.DeviceName, CameraDC);

            DeviceConfig Aligner02DC = new DeviceConfig
            {
                DeviceName = "Aligner02",
                Enable = true,
                PortName = "COM24",
                BaudRate = 38400,
                Vendor = "Sanwa",
                ConnectionType = "ComPort",
                DataBits = 8,
                ParityBit = "None",
                StopBit = "One",
                IPAdress = "192.168.0.135",
                Port = 23
            };
            deviceConfig.Add(Aligner02DC.DeviceName, Aligner02DC);

            //設定 Buffer
            cameraBasic.ImageBuffer.SetBufferInfo(360, 3840, 2748, DepthType.Cv8U, 3);
            cameraBasic.FileBuffer.SetBufferInfo(360, 3840, 2748, DepthType.Cv8U, 3);

            DisplayImageBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;

            FilterImageBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            FilterImageBox.Visible = false;
            FilterImageBox.Location = new Point(0, 0);
            FilterImageBox.Size = new Size(640, 480);

            //LoadINIFile();
            LoadRegFile();

            ConnectAllDevice();

            UpdateUI();

            PreventMultiClick = DateTime.Now;
        }
        private void cmbTestMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            ComboBox cmb = (ComboBox)sender;
            switch(cmb.SelectedIndex)
            {
                case 0: //Fix
                    lbTestModeXOffset.Text = "X Offset(um)";
                    lbTestModeYOffset.Text = "Y Offset(um)";
                    lbTestModeTOffset.Text = "T Offset(mdeg)";
                    break;
                case 1: //Step(Center)
                    lbTestModeXOffset.Text = "Center Mag(um)";
                    lbTestModeYOffset.Text = "Center Dir(mdeg)";
                    lbTestModeTOffset.Text = "T Offset(mdeg)";
                    break;
                case 2:
                    lbTestModeXOffset.Text = "X Offset(um)";
                    lbTestModeYOffset.Text = "Y Offset(um)";
                    lbTestModeTOffset.Text = "Notch Dir(mdeg)";
                    break;
                default:
                    break;
            }

            ////Align模式
            MachineParas.TestMode = cmbTestMode.SelectedIndex;

        }
        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            RichTextBox1.SelectionStart = RichTextBox1.TextLength;
            RichTextBox1.ScrollToCaret();
        }
        private void ReadDeviceRegConfig(string RegRoot, DeviceConfig dc)
        {
            string strValue = (string)Registry.GetValue(RegRoot, dc.DeviceName+"_Enable", dc.Enable.ToString());
            dc.Enable = strValue.ToUpper().Equals("TRUE") ? true : false;
            dc.ConnectionType = (string)Registry.GetValue(RegRoot, dc.DeviceName + "_ConnectionType", dc.ConnectionType);
            dc.PortName = (string)Registry.GetValue(RegRoot, dc.DeviceName + "_COMPort", "COM24");
            dc.BaudRate = (int)Registry.GetValue(RegRoot, dc.DeviceName + "_BaudRate", 38400);
            dc.IPAdress = (string)Registry.GetValue(RegRoot, dc.DeviceName + "_IPAdress", dc.IPAdress);
            //dc.Port = (int)Registry.GetValue(RegRoot, dc.DeviceName + "_Port", 23);
        }
        private void WriteDeviceRegConfig(string RegRoot, DeviceConfig dc)
        {
            Registry.SetValue(RegRoot, dc.DeviceName + "_Enable", dc.Enable.ToString());
            Registry.SetValue(RegRoot, dc.DeviceName + "_COMPort", dc.PortName);
            Registry.SetValue(RegRoot, dc.DeviceName + "_BaudRate", dc.BaudRate);
            Registry.SetValue(RegRoot, dc.DeviceName + "_ConnectionType", dc.ConnectionType);
            Registry.SetValue(RegRoot, dc.DeviceName + "_IPAdress", dc.IPAdress);
        }
        private void LoadRegFile()
        {
            string RegRoot = @"HKEY_CURRENT_USER\Software\AlignerVerification";
            RegistryKey Reg = Registry.CurrentUser.OpenSubKey("Software", true);
            string strValue;
            int iValue = 0;

            if (Reg.GetSubKeyNames().Contains("AlignerVerification") == false)
            {
                //Reg.CreateSubKey("AlignerVerification");
                Reg.Close();
                SaveRegFile();

                return;
            }


            MachineParas.WaferRadius = (int)Registry.GetValue(RegRoot, "WaferRadius", MachineParas.WaferRadius);

            //測試速度
            MachineParas.TestSpeed = (int)Registry.GetValue(RegRoot, "TestSpeed", MachineParas.TestSpeed);

            ////Aligner後旋轉角度(mdeg)
            MachineParas.NotchAngle = (int)Registry.GetValue(RegRoot, "NotchAngle", MachineParas.NotchAngle);

            ////Aligner後X方向位移(um)
            MachineParas.AlignXOffset = (int)Registry.GetValue(RegRoot, "AlignXOffset", MachineParas.AlignXOffset);

            ////輸出資料夾
            MachineParas.ExportFolder = (string)Registry.GetValue(RegRoot, "ExportFolder", MachineParas.ExportFolder);

            ////Aligner形式 
            MachineParas.MachineType = (int)Registry.GetValue(RegRoot, "MachineType", MachineParas.MachineType);

            ////Wafer形式(Notch、Flat、Circle)
            MachineParas.WaferType = (int)Registry.GetValue(RegRoot, "WaferType", MachineParas.WaferType);

            ////Align模式
            MachineParas.TestMode = (int)Registry.GetValue(RegRoot, "TestMode", MachineParas.TestMode);

            ////Align測試後位移(X)
            MachineParas.TestModeXOffset = (int)Registry.GetValue(RegRoot, "TestModeXOffset", MachineParas.TestModeXOffset);

            ////Align測試後位移(Y)
            MachineParas.TestModeYOffset = (int)Registry.GetValue(RegRoot, "TestModeYOffset", MachineParas.TestModeYOffset);

            ////Align測試後位移(T)
            MachineParas.TestModeTOffset = (int)Registry.GetValue(RegRoot, "TestModeTOffset", MachineParas.TestModeTOffset);

            MachineParas.OutputFolder = (string)Registry.GetValue(RegRoot, "OutputFolder", MachineParas.OutputFolder);

            MachineParas.PresentMonitorHour = Convert.ToDouble(Registry.GetValue(RegRoot, "PresentMonitorHour", MachineParas.PresentMonitorHour.ToString()));

            MachineParas.PresentMonitorMin = Convert.ToDouble(Registry.GetValue(RegRoot, "PresentMonitorMin", MachineParas.PresentMonitorMin.ToString()));

            MachineParas.PresentMonitorMin = Convert.ToDouble(Registry.GetValue(RegRoot, "PresentMonitorSec", MachineParas.PresentMonitorSec.ToString()));

            strValue = (string)Registry.GetValue(RegRoot, "CheckWaferPresentInAutoRun", MachineParas.CheckWaferPresentInAutoRun.ToString());
            MachineParas.CheckWaferPresentInAutoRun = strValue.ToUpper().Equals("TRUE") ? true : false;

            //Statistics 偏移量計算形式
            Statistics.OffsetType = (int)Registry.GetValue(RegRoot, "OffsetType", Statistics.OffsetType);

            foreach(DeviceConfig dc in deviceConfig.Values)
                ReadDeviceRegConfig(RegRoot, dc);

            iValue = (int)Registry.GetValue(RegRoot, "ROITop", 0);
            cameraBasic.AOITool.SetROITop(iValue);

            iValue = (int)Registry.GetValue(RegRoot, "ROIBottom", 12);
            cameraBasic.AOITool.SetROIBottom(iValue);

            strValue = (string)Registry.GetValue(RegRoot, "ManualBinary", cameraBasic.AOITool.ManualBinary.ToString());
            cameraBasic.AOITool.ManualBinary = strValue.ToUpper().Equals("TRUE") ? true : false;

            cameraBasic.AOITool.BinaryTHL = Convert.ToByte(Registry.GetValue(RegRoot, "BinaryTHL", cameraBasic.AOITool.BinaryTHL.ToString()));

            cameraBasic.AOITool.FilterMask = (int)Registry.GetValue(RegRoot, "FilterMask", cameraBasic.AOITool.FilterMask);

            strValue = (string)Registry.GetValue(RegRoot, "FillWafer", cameraBasic.AOITool.FillWafer.ToString());
            cameraBasic.AOITool.FillWafer = strValue.ToUpper().Equals("TRUE") ? true : false;

            //異常時停機
            strValue = (string)Registry.GetValue(RegRoot, "AlarmStopEnabled", MachineParas.bAlarmStopEnabled.ToString());
            MachineParas.bAlarmStopEnabled = strValue.ToUpper().Equals("TRUE") ? true : false;

            //異常時，Download Data
            strValue = (string)Registry.GetValue(RegRoot, "AlarmStopDownloadData", MachineParas.bAlarmStopDownloadData.ToString());
            MachineParas.bAlarmStopDownloadData = strValue.ToUpper().Equals("TRUE") ? true : false;

            //O offset 規格上限
            MachineParas.dOOffsetUpLimit = Convert.ToDouble(Registry.GetValue(RegRoot, "OOffsetUpLimit", MachineParas.dOOffsetUpLimit.ToString()));
            //N offset 規格上限
            MachineParas.dNOffsetUpLimit = Convert.ToDouble(Registry.GetValue(RegRoot, "NOffsetUpLimit", MachineParas.dNOffsetUpLimit.ToString()));


            Reg.Close();

        }
        private void SaveRegFile()
        {
            string RegRoot = @"HKEY_CURRENT_USER\Software\AlignerVerification";
            RegistryKey Reg = Registry.CurrentUser.OpenSubKey("Software", true);
            string strValue;
            int iValue = 0;

            if (!Reg.GetSubKeyNames().Contains("AlignerVerification"))
                Reg.CreateSubKey("AlignerVerification");

            //WaferRadius
            Registry.SetValue(RegRoot, "WaferRadius", MachineParas.WaferRadius);

            ////測試速度
            Registry.SetValue(RegRoot, "TestSpeed", MachineParas.TestSpeed);

            ////Aligner後旋轉角度(mdeg)
            Registry.SetValue(RegRoot, "NotchAngle", MachineParas.NotchAngle);

            ////Aligner後X方向位移(um)
            Registry.SetValue(RegRoot, "AlignXOffset", MachineParas.AlignXOffset);

            ////輸出資料夾
            Registry.SetValue(RegRoot, "ExportFolder", MachineParas.ExportFolder);

            ////Aligner形式 
            Registry.SetValue(RegRoot, "MachineType", MachineParas.MachineType);

            ////Wafer形式(Notch、Flat、Circle)
            Registry.SetValue(RegRoot, "WaferType", MachineParas.WaferType);

            ////Align模式
            Registry.SetValue(RegRoot, "TestMode", MachineParas.TestMode);

            ////Align測試後位移(X)
            Registry.SetValue(RegRoot, "TestModeXOffset", MachineParas.TestModeXOffset);

            ////Align測試後位移(Y)
            Registry.SetValue(RegRoot, "TestModeYOffset", MachineParas.TestModeYOffset);

            ////Align測試後位移(T)
            Registry.SetValue(RegRoot, "TestModeTOffset", MachineParas.TestModeTOffset);

            //輸出資料夾位置
            Registry.SetValue(RegRoot, "OutputFolder", MachineParas.OutputFolder);

            Registry.SetValue(RegRoot, "PresentMonitorHour", MachineParas.PresentMonitorHour.ToString());

            Registry.SetValue(RegRoot, "PresentMonitorMin", MachineParas.PresentMonitorMin.ToString());

            Registry.SetValue(RegRoot, "PresentMonitorSec", MachineParas.PresentMonitorSec.ToString());

            Registry.SetValue(RegRoot, "CheckWaferPresentInAutoRun", MachineParas.CheckWaferPresentInAutoRun.ToString());

            //Statistics 偏移量計算形式
            Registry.SetValue(RegRoot, "OffsetType", Statistics.OffsetType);

            foreach (DeviceConfig dc in deviceConfig.Values)
                WriteDeviceRegConfig(RegRoot, dc);

            Registry.SetValue(RegRoot, "ROITop", cameraBasic.AOITool.Top);
            Registry.SetValue(RegRoot, "ROIBottom", cameraBasic.AOITool.Bottom);
            Registry.SetValue(RegRoot, "ManualBinary", cameraBasic.AOITool.ManualBinary.ToString());
            Registry.SetValue(RegRoot, "BinaryTHL", cameraBasic.AOITool.BinaryTHL.ToString());
            Registry.SetValue(RegRoot, "FilterMask", cameraBasic.AOITool.FilterMask);
            Registry.SetValue(RegRoot, "FillWafer", cameraBasic.AOITool.FillWafer.ToString());

            //異常時停機
            Registry.SetValue(RegRoot, "AlarmStopEnabled", MachineParas.bAlarmStopEnabled.ToString());
            //異常時，Download Data
            Registry.SetValue(RegRoot, "AlarmStopDownloadData", MachineParas.bAlarmStopDownloadData.ToString());
            //O offset 規格上限
            Registry.SetValue(RegRoot, "OOffsetUpLimit", MachineParas.dOOffsetUpLimit.ToString());
            //N offset 規格上限
            Registry.SetValue(RegRoot, "NOffsetUpLimit", MachineParas.dNOffsetUpLimit.ToString());
        }
        private void UpdateUI()
        {
            loadParas = true;

            DeviceConfig dc;
            int iValue = 0;

            //Wafer 半徑
            WaferRadiusUpDown.Value = MachineParas.WaferRadius;
            //測試速度
            //if (!Int32.TryParse(cmbTestSpeed.Text, out MachineParas.TestSpeed))
            //    logger.Debug("SetParas() :" + "if (!Int32.TryParse(cmbTestSpeed.Text, out MachineParas.TestSpeed))");
            iValue = cmbTestSpeed.FindString(MachineParas.TestSpeed.ToString());
            if (iValue < 0)
                logger.Debug("UpdateUI() : cmbTestSpeed.FindString(MachineParas.TestSpeed.ToString()");
            else
                cmbTestSpeed.SelectedIndex = iValue;

            //Aligner後旋轉角度(mdeg)
            udNotchAngle.Value = MachineParas.NotchAngle;
            ////Aligner後X方向位移(um)
            udAlignXOffset.Value = MachineParas.AlignXOffset;
            ////輸出資料夾
            tbExportFolder.Text = MachineParas.ExportFolder;
            ////Aligner形式
            cmbNotchType.SelectedIndex = MachineParas.MachineType;
            ////Wafer形式(Notch、Flat、Circle) 
            cmbWaferType.SelectedIndex = MachineParas.WaferType;
            ////Align模式
            cmbTestMode.SelectedIndex = MachineParas.TestMode;
            ////Align測試後位移(X)
            udTestModeXOffset.Value = MachineParas.TestModeXOffset;
            ////Align測試後位移(Y)
            udTestModeYOffset.Value = MachineParas.TestModeYOffset;
            ////Align測試後位移(T)
            udTestModeTOffset.Value = MachineParas.TestModeTOffset;

            lbShowOutputFolder.Text = MachineParas.OutputFolder;

            udPresentMonitorHour.Value = (int)MachineParas.PresentMonitorHour;

            udPresentMonitorMin.Value = (int)MachineParas.PresentMonitorMin;

            udPresentMonitorSec.Value = (int)MachineParas.PresentMonitorSec;

            cbCheckWaferPresentInAutoRun.Checked = MachineParas.CheckWaferPresentInAutoRun;

            cbOffsetType.Checked = Statistics.OffsetType.Equals(1) ? true : false;

            //異常時停機
            cbAlarmStopEnabled.Checked = MachineParas.bAlarmStopEnabled;
            //異常時，Download Data
            cbAlarmStopDownloadData.Checked = MachineParas.bAlarmStopDownloadData;
            //O offset 規格上限
            tbOOffsetUpLimit.Text = MachineParas.dOOffsetUpLimit.ToString();
            //N offset 規格上限
            tbNOffsetUpLimit.Text = MachineParas.dNOffsetUpLimit.ToString();

            dc = deviceConfig["Cylinder"];
            cbCylinderEnabled.Checked = dc.Enable;
            cmbCylinderPort.Text = dc.PortName;
            cmbCylinderBaudRate.Text = dc.BaudRate.ToString();

            dc = deviceConfig["Aligner"];
            cbAlignerEnabled.Checked = dc.Enable;
            cmbAlignerPort.Text = dc.PortName;
            cmbAlignerBaudRate.Text = dc.BaudRate.ToString();

            cmbAlignerConnectionType.Text = dc.ConnectionType;
            tbAlignerIPAddress.Text = dc.IPAdress;

            dc = deviceConfig["Camera"];
            cbCameraEnabled.Checked = dc.Enable;
            cmbCameraPort.Text = dc.PortName;
            cmbCameraBaudRate.Text = dc.BaudRate.ToString();

            dc = deviceConfig["Aligner02"];
            cbAligner02Enabled.Checked = dc.Enable;
            cmbAligner02Port.Text = dc.PortName;
            cmbAligner02BaudRate.Text = dc.BaudRate.ToString();

            cmbAligner02ConnectionType.Text = dc.ConnectionType;
            tbAligner02IPAddress.Text = dc.IPAdress;


            //影像相關
            tkbrROITop.Value = cameraBasic.AOITool.Top;
            tbROITop.Text = tkbrROITop.Value.ToString();
            tkbrROIBottom.Value = cameraBasic.AOITool.Bottom;
            tbROIBottom.Text = tkbrROIBottom.Value.ToString();

            cbManualBinary.Checked = cameraBasic.AOITool.ManualBinary;

            tkbrBinaryTHL.Value = (int)cameraBasic.AOITool.BinaryTHL;
            tbBinaryTHL.Text = tkbrBinaryTHL.Value.ToString();

            udFilterMask.Value = cameraBasic.AOITool.FilterMask;

            cameraBasic.AOITool.SetROITop(tkbrROITop.Value);
            cameraBasic.AOITool.SetROIBottom(tkbrROIBottom.Value);

            //塗滿 Wafer
            cbFillWafer.Checked = cameraBasic.AOITool.FillWafer;

            int topY = (int)(cameraBasic.AOITool.ROITop * ZoomRadioY);
            int bottonY = (int)(cameraBasic.AOITool.ROIBottom * ZoomRadioY);
            Rectangle roi = new Rectangle
            {
                Location = new Point(0, topY),
                Size = new Size(640, bottonY - topY)
            };

            FilterImageBox.Location = new Point(roi.X, roi.Y);
            FilterImageBox.Size = new Size(roi.Width, roi.Height);

            loadParas = false;
        }
        private void SetParas()
        {
            int iValue;
            DeviceConfig dc;

            //Wafer 半徑
            MachineParas.WaferRadius = decimal.ToInt32(WaferRadiusUpDown.Value);
            //測試速度
            if (!Int32.TryParse(cmbTestSpeed.Text, out MachineParas.TestSpeed))
                logger.Debug("SetParas() :" + "if (!Int32.TryParse(cmbTestSpeed.Text, out MachineParas.TestSpeed))");
            //Aligner後旋轉角度(mdeg)
            MachineParas.NotchAngle = decimal.ToInt32(udNotchAngle.Value);
            ////Aligner後X方向位移(um)
            MachineParas.AlignXOffset = decimal.ToInt32(udAlignXOffset.Value);
            ////輸出資料夾
            MachineParas.ExportFolder = tbExportFolder.Text;
            ////Aligner形式
            MachineParas.MachineType = cmbNotchType.SelectedIndex;
            ////Wafer形式(Notch、Flat、Circle) 
            MachineParas.WaferType = cmbWaferType.SelectedIndex;
            ////Align模式
            MachineParas.TestMode = cmbTestMode.SelectedIndex;
            ////Align測試後位移(X)
            MachineParas.TestModeXOffset = decimal.ToInt32(udTestModeXOffset.Value);
            ////Align測試後位移(Y)
            MachineParas.TestModeYOffset = decimal.ToInt32(udTestModeYOffset.Value);
            ////Align測試後位移(T)
            MachineParas.TestModeTOffset = decimal.ToInt32(udTestModeTOffset.Value);

            MachineParas.OutputFolder = lbShowOutputFolder.Text;

            MachineParas.PresentMonitorHour = (double)udPresentMonitorHour.Value;

            MachineParas.PresentMonitorMin = (double)udPresentMonitorMin.Value;

            MachineParas.PresentMonitorSec = (int)udPresentMonitorSec.Value;

            MachineParas.CheckWaferPresentInAutoRun = cbCheckWaferPresentInAutoRun.Checked;

            Statistics.OffsetType = cbOffsetType.Checked ? 1 : 0;

            //異常時停機
            MachineParas.bAlarmStopEnabled = cbAlarmStopEnabled.Checked;
            //異常時，Download Data
            MachineParas.bAlarmStopDownloadData = cbAlarmStopDownloadData.Checked;
            //O offset 規格上限
            MachineParas.dOOffsetUpLimit = Convert.ToDouble(tbOOffsetUpLimit.Text);
            //N offset 規格上限
            MachineParas.dNOffsetUpLimit = Convert.ToDouble(tbNOffsetUpLimit.Text);

            dc = deviceConfig["Cylinder"];
            dc.Enable = cbCylinderEnabled.Checked;
            dc.PortName = cmbCylinderPort.Text;

            if (Int32.TryParse(cmbCylinderBaudRate.Text, out iValue))
                dc.BaudRate = iValue;
            else
                logger.Debug("SetParas() :" + "if (!Int32.TryParse(cmbCylinderBaudRate.Text, out iValue))");

            dc = deviceConfig["Aligner"];
            dc.Enable = cbAlignerEnabled.Checked;
            dc.PortName = cmbAlignerPort.Text;
            if (Int32.TryParse(cmbAlignerBaudRate.Text, out iValue))
                dc.BaudRate = iValue;
            else
                logger.Debug("SetParas() :" + "if (!Int32.TryParse(cmbAlignerBaudRate.Text, out iValue))");

            dc.ConnectionType = cmbAlignerConnectionType.Text;
            dc.IPAdress = tbAlignerIPAddress.Text;

            dc = deviceConfig["Camera"];
            dc.Enable = cbCameraEnabled.Checked;
            dc.PortName = cmbCameraPort.Text;
            if (Int32.TryParse(cmbCameraBaudRate.Text, out iValue))
                dc.BaudRate = iValue;
            else
                logger.Debug("SetParas() :" + "if (!Int32.TryParse(cmbCameraBaudRate.Text, out iValue))");


            dc = deviceConfig["Aligner02"];
            dc.Enable = cbAligner02Enabled.Checked;
            dc.PortName = cmbAligner02Port.Text;
            if (Int32.TryParse(cmbAligner02BaudRate.Text, out iValue))
                dc.BaudRate = iValue;
            else
                logger.Debug("SetParas() :" + "if (!Int32.TryParse(cmbAligner02BaudRate.Text, out iValue))");

            dc.ConnectionType = cmbAligner02ConnectionType.Text;
            dc.IPAdress = tbAligner02IPAddress.Text;

            //影像相關
            cameraBasic.AOITool.SetROITop(tkbrROITop.Value);
            tbROITop.Text = tkbrROITop.Value.ToString();
            cameraBasic.AOITool.SetROIBottom(tkbrROIBottom.Value);
            tbROIBottom.Text = tkbrROIBottom.Value.ToString();

            cameraBasic.AOITool.ManualBinary = cbManualBinary.Checked;

            cameraBasic.AOITool.BinaryTHL = (byte)tkbrBinaryTHL.Value;
            tbBinaryTHL.Text = tkbrBinaryTHL.Value.ToString();

            cameraBasic.AOITool.FilterMask = (int)udFilterMask.Value;

            //塗滿 Wafer
            cameraBasic.AOITool.FillWafer = cbFillWafer.Checked;
        }
        private void ConnectAllDevice()
        {
            foreach (DeviceConfig dc in deviceConfig.Values)
                ConnectDevice(dc);                            
        }

        private void DisconnectAllDevice()
        {
            foreach (DeviceConfig dc in deviceConfig.Values)
            {                
                deviceMap.TryGetValue(dc.DeviceName, out DeviceController Ctrl);

                if(Ctrl != null)
                {
                    if (dc.Enable) Ctrl.close();

                    deviceMap.Remove(Ctrl.Name);
                }
            }
        }
        private void ConnectDevice(DeviceConfig dc)
        {
            DeviceController dvcCtrl = new DeviceController(dc, this);
            deviceMap.TryGetValue(dvcCtrl.Name, out DeviceController Ctrl);

            if (Ctrl != null) deviceMap.Remove(Ctrl.Name);
           
            deviceMap.Add(dvcCtrl.Name, dvcCtrl);

            if (dc.Enable)  dvcCtrl.start();
        }

        private void btnCylinderConnect_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string deviceName = null;
            switch(btn.Name)
            {
                case "btnCylinderConnect":
                    deviceName = "Cylinder";
                    break;
                case "btnAlignerConnect":
                    deviceName = "Aligner";
                    break;
                case "btnCameraConnect":
                    deviceName = "Camera";
                    break;
                case "btnAligner02Connect":
                    deviceName = "Aligner02";
                    break;
            }

            deviceConfig.TryGetValue(deviceName, out DeviceConfig dc);

            if (dc != null) ConnectDevice(dc);
        }
        private void DoCylinderJob(string job)
        {
            string DeviceName = "Cylinder";
            DeviceController dctrl = deviceMap[DeviceName];
            if (dctrl._Config.Enable)
            {
                if (dctrl._IsConnected)
                {
                    switch(job)
                    {
                        case "Initialize":
                                FormMainUpdate.MessageLogUpdate("DoCylinderInitialize(dctrl)", "Start");
                                DoCylinderInitialize(dctrl);
                                FormMainUpdate.MessageLogUpdate("DoCylinderInitialize(dctrl)", "End");
                            break;
                        case "MoveDown":
                                FormMainUpdate.MessageLogUpdate("DoCylinderMoveDown(dctrl)", "Start");
                                DoCylinderMoveDown(dctrl);
                                FormMainUpdate.MessageLogUpdate("DoCylinderMoveDown(dctrl)", "End");
                            break;
                        case "MoveUp":
                                FormMainUpdate.MessageLogUpdate("DoCylinderMoveUp(dctrl)", "Start");
                                DoCylinderMoveUp(dctrl);
                                FormMainUpdate.MessageLogUpdate("DoCylinderMoveUp(dctrl)", "End");
                            break;
                        default:
                            break;

                    }
                }
                else
                    FormMainUpdate.MessageLogUpdate("DoCylinderJob(string job)", "未連線");
            }
            else
                FormMainUpdate.MessageLogUpdate("DoCylinderJob(string job)", "未啟用");
        }
        private void CylinderIniButton_Click(object sender, EventArgs e)
        {
            if (!StartPreventMultiClick(300)) return;
          
            DoCylinderJob("Initialize");

            StopPreventMultiClick();
        }
        private void btnCylinderMoveDown_Click(object sender, EventArgs e)
        {
            if (!StartPreventMultiClick(300)) return;

            DoCylinderJob("MoveDown");

            StopPreventMultiClick();
        }
        private void btnCylinderMoveUp_Click(object sender, EventArgs e)
        {
            if (!StartPreventMultiClick(300)) return;

            if(DoAlignerRelease())
            {
                Thread.Sleep(1000);
                DoCylinderJob("MoveUp");
            }


            StopPreventMultiClick();
        }
        private void DoCylinderInitialize(DeviceController dctrl)
        {
            if(dctrl != null)
            {
                logger.Info("Cylinder Initial");

                string strBuffer = "01 05 00 45 FF 00 9D EF";
                dctrl.sendHexCommand(strBuffer);
                Thread.Sleep(10);

                strBuffer = "01 05 00 30 FF 00 8C 35";
                dctrl.sendHexCommand(strBuffer);
                Thread.Sleep(10);

                strBuffer = "01 05 00 19 FF 00 5D FD";
                dctrl.sendHexCommand(strBuffer);
                Thread.Sleep(1000);

                strBuffer = "01 05 00 1C FF 00 4D FC";
                dctrl.sendHexCommand(strBuffer);
                Thread.Sleep(10);

                strBuffer = "01 05 00 1C 00 00 0C 0C";
                dctrl.sendHexCommand(strBuffer);
                logger.Info("Cylinder Initial Finish");
            }
            else
            {
                logger.Debug("DoCylinderInitialize(DeviceController dctrl) : if(dctrl == null)");
            }
        }
        private bool DoCylinderMoveDown(DeviceController dctrl)
        {
            bool bReturn = true;
            if (dctrl != null)
            {
                FormMainUpdate.MessageLogUpdate(dctrl._Config.DeviceName, "Move Down");

                string strBuffer = "01 0F 00 10 00 08 01 00 3F 56";
                dctrl.sendHexCommand(strBuffer);
                Thread.Sleep(100);

                strBuffer = "01 05 00 1A FF 00 AD FD";
                dctrl.sendHexCommand(strBuffer);
                Thread.Sleep(100);

                EvtManager.CylinderFinishEvt.Reset();

                strBuffer = "01 05 00 1A 00 00 EC 0D";
                dctrl.sendHexCommand(strBuffer);

                if(!EvtManager.CylinderFinishEvt.WaitOne(4000))
                {
                    logger.Debug("DoCylinderMoveDown(DeviceController dctrl) : Timeout");
                }
                else
                {
                    Thread.Sleep(2000);
                }


            }
            else
            {
                logger.Debug("DoCylinderMoveDown(DeviceController dctrl) : if(dctrl == null)");
                bReturn = false;
                 
            }

            return bReturn;
        }
        private bool DoCylinderMoveUp(DeviceController dctrl)
        {
            bool bReturn = true;
            if (dctrl != null)
            {
                FormMainUpdate.MessageLogUpdate(dctrl._Config.DeviceName, "Move Up");

                string strBuffer = "01 0F 00 10 00 08 01 01 FE 96";
                dctrl.sendHexCommand(strBuffer);
                Thread.Sleep(100);

                strBuffer = "01 05 00 1A FF 00 AD FD";
                dctrl.sendHexCommand(strBuffer);
                Thread.Sleep(100);

                EvtManager.CylinderFinishEvt.Reset();

                strBuffer = "01 05 00 1A 00 00 EC 0D";
                dctrl.sendHexCommand(strBuffer);

                if (!EvtManager.CylinderFinishEvt.WaitOne(4000))
                {
                    logger.Debug("DoCylinderMoveDown(DeviceController dctrl) : Timeout");
                }
                else
                {
                    Thread.Sleep(2000);
                }

            }
            else
            {
                logger.Debug("DoCylinderMoveDown(DeviceController dctrl) : if(dctrl == null)");
                bReturn = false;

            }

            return bReturn;
        }
        private bool DoCameraGrab(DeviceController dctrl)
        {
            bool bReturn = true;
            if (dctrl.Name.ToUpper().Equals("CAMERA"))
            {
                if(dctrl._Config.Enable)
                {
                    int RetryTime = 0;                   
                    while(RetryTime < 3)
                    {
                        dctrl.sendCommand("%CMD:CAP0");
                        if (EvtManager.CameraGrabFinishEvt.WaitOne(1000))   break;

                        RetryTime++;
                    }

                    if(RetryTime == 3)
                    {
                        FormMainUpdate.MessageLogUpdate("DoCameraGrab(DeviceController dctrl)", "RetryTime == 3");
                        bReturn = false;
                    }
                    
                }
                else
                {
                    FormMainUpdate.MessageLogUpdate("DoCameraGrab(DeviceController dctrl)", "!dctrl._Config.Enable");
                    bReturn = false;
                }

            }
            else
            {
                logger.Debug("DoCameraGrab(DeviceController dctrl) : if (!dctrl.Name.ToUpper().Equals(CAMERA)");
                bReturn = false;
            }

            return bReturn;
        }
        private bool DoGrab()
        {
            DeviceController dctrl = deviceMap["Camera"];
            if (dctrl._Config.Enable)
            {
                if (dctrl._IsConnected)
                {
                    //取像
                    if (!DoCameraGrab(dctrl))
                    {
                        logger.Debug("GrabButton_Click(object sender, EventArgs e) : 取像失敗");
                        return false;
                    }

                    string strFileName = Environment.CurrentDirectory;
                    strFileName = strFileName + @"\Output\img0\acc0.bmp";

                    if (!File.Exists(strFileName))
                    {
                        logger.Debug("GrabButton_Click(object sender, EventArgs e) : 檔案不存在(" + strFileName + ")");
                        return false;
                    }

                    cameraBasic.SetImage(CvInvoke.Imread(strFileName));

                    FormMainUpdate.ShowImageUpdate(cameraBasic.MatFrame);
                }
                else
                    logger.Debug("GrabButton_Click(object sender, EventArgs e) : 未連線");
            }
            else
                logger.Debug("GrabButton_Click(object sender, EventArgs e) : 未啟用");

            return true;
        }

        private void GrabButton_Click(object sender, EventArgs e)
        {
            if (!StartPreventMultiClick(300)) return;

            DoGrab();

            StopPreventMultiClick();
        }
        //private void ShowAlignment()
        //{
        //    using (Pen pen = new Pen(Color.Red, 1))
        //    {
        //        pen.DashStyle = DashStyle.Dash;
        //        Graphics eGraphics = DisplayImageBox.CreateGraphics();

        //        Point HSPT = new Point(0, DisplayImageBox.Height / 2);
        //        Point HEPT = new Point(DisplayImageBox.Width, DisplayImageBox.Height / 2);
        //        Point VSPT = new Point(DisplayImageBox.Width / 2, 0);
        //        Point VEPT = new Point(DisplayImageBox.Width / 2, DisplayImageBox.Height);

        //        eGraphics.DrawLine(pen, HSPT, HEPT);
        //        eGraphics.DrawLine(pen, VSPT, VEPT);

        //        pen.DashStyle = DashStyle.Dash;
        //        pen.Color = Color.Blue;
        //        for (int i = 1; i < 5; i++)
        //        {
        //            Point a = new Point(DisplayImageBox.Width / 5 * i, 0);
        //            Point b = new Point(DisplayImageBox.Width / 5 * i, DisplayImageBox.Height);

        //            eGraphics.DrawLine(pen, a, b);
        //        }
        //    }
        //}
        private void WaferRadiusUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loadParas) return;
            MachineParas.WaferRadius = decimal.ToInt32(WaferRadiusUpDown.Value);
        }

        private void btnSaveParas_Click(object sender, EventArgs e)
        {
            //SaveINIFile();
            SaveRegFile();
            logger.Info("儲存完畢");
            MessageBox.Show("儲存完畢");
        }

        private void cmbTestSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            if (!Int32.TryParse(cmbTestSpeed.Text, out MachineParas.TestSpeed))
                logger.Debug("cmbTestSpeed_SelectedIndexChanged(object sender, EventArgs e) : "+
                    "if(!Int32.TryParse(cmbTestSpeed.Text, out MachineParas.TestSpeed))");            
        }

        private void udNotchAngle_ValueChanged(object sender, EventArgs e)
        {
            if (loadParas) return;
            MachineParas.NotchAngle = decimal.ToInt32(udNotchAngle.Value);
        }

        private void udAlignXOffset_ValueChanged(object sender, EventArgs e)
        {
            if (loadParas) return;
            MachineParas.AlignXOffset = decimal.ToInt32(udAlignXOffset.Value);            
        }

        private void tbExportFolder_TextChanged(object sender, EventArgs e)
        {
            if (loadParas) return;
            MachineParas.ExportFolder = tbExportFolder.Text;
        }

        private void cmbWaferType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadParas) return;
            ////Wafer形式(Notch、Flat、Circle) 
            MachineParas.WaferType = cmbWaferType.SelectedIndex;
        }

        private void udTestModeXOffset_ValueChanged(object sender, EventArgs e)
        {
            if (loadParas) return;
            ////Align測試後位移(X)
            MachineParas.TestModeXOffset = decimal.ToInt32(udTestModeXOffset.Value);
        }

        private void udTestModeYOffset_ValueChanged(object sender, EventArgs e)
        {
            if (loadParas) return;
            ////Align測試後位移(Y)
            MachineParas.TestModeYOffset = decimal.ToInt32(udTestModeYOffset.Value);
        }

        private void udTestModeTOffset_ValueChanged(object sender, EventArgs e)
        {
            if (loadParas) return;
            ////Align測試後位移(T)
            MachineParas.TestModeTOffset = decimal.ToInt32(udTestModeTOffset.Value);
        }

        private void cbAlignerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            CheckBox cb = (CheckBox)sender;
            DeviceConfig dc = null;
            switch (cb.Name)
            {
                case "cbAlignerEnabled":
                    dc = deviceConfig["Aligner"];
                    break;
                case "cbAligner02Enabled":
                    dc = deviceConfig["Aligner02"];
                    break;
                case "cbCylinderEnabled":
                    dc = deviceConfig["Cylinder"];
                    break;
                case "cbCameraEnabled":
                    dc = deviceConfig["Camera"];
                    break;
                default:
                    break;
            }

            if(dc != null)
                dc.Enable = cb.Checked;
        }

        private void cmbAlignerPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            ComboBox cb = (ComboBox)sender;
            DeviceConfig dc = null;
            switch (cb.Name)
            {
                case "cmbAlignerPort":
                    dc = deviceConfig["Aligner"];
                    break;
                case "cmbAligner02Port":
                    dc = deviceConfig["Aligner02"];
                    break;
                case "cmbCylinderPort":
                    dc = deviceConfig["Cylinder"];
                    break;
                case "cmbCameraPort":
                    dc = deviceConfig["Camera"];
                    break;
            }

            if (dc != null)
                dc.PortName = cb.Text;
        }

        private void cmbAlignerBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            ComboBox cb = (ComboBox)sender;
            DeviceConfig dc = null;
            switch (cb.Name)
            {
                case "cmbAlignerBaudRate":
                    dc = deviceConfig["Aligner"];
                    break;
                case "cmbAligner02BaudRate":
                    dc = deviceConfig["Aligner02"];
                    break;
                case "cmbCylinderBaudRate":
                    dc = deviceConfig["Cylinder"];
                    break;
                case "cmbCameraBaudRate":
                    dc = deviceConfig["Camera"];
                    break;
            }

            if (Int32.TryParse(cb.Text, out int iValue))
                dc.BaudRate = iValue;
            else
                logger.Debug("cmbAlignerBaudRate_SelectedIndexChanged() :" +
                    "if (!Int32.TryParse(cb.Text, out int iValue)))");
        }

        private void btnOpenImageFolder_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            try
            {
                string strFileName = Environment.CurrentDirectory + @"\output\img0";

                FolderBrowserDialog path = new FolderBrowserDialog
                {
                    SelectedPath = strFileName
                };

                if(DialogResult.OK == path.ShowDialog())
                {
                    // 顯示進度條控制元件.
                    pbrOpenImageFolder.Visible = true;

                    string folderpath = path.SelectedPath;

                    string [] Files =  Directory.GetFiles(folderpath);

                    int iInageFileCount = Files.Length;

                    if(iInageFileCount > 0)
                    {
                        // 設定進度條最小值.
                        pbrOpenImageFolder.Minimum = 1;

                        // 設定進度條最大值.
                        pbrOpenImageFolder.Maximum = iInageFileCount*2;

                        // 設定進度條初始值
                        pbrOpenImageFolder.Value = 1;
                        // 設定每次增加的步長
                        pbrOpenImageFolder.Step = 1;
                    }

                    int count = 0;
                    Mat firstMat = null;
                    Mat tempMat = null;

                    Dictionary<string, Mat> matList = new Dictionary<string, Mat>();
                    foreach (string files in Directory.GetFiles(folderpath))
                    {
                        if (iInageFileCount > 0)
                            pbrOpenImageFolder.PerformStep();

                        if (files.Contains(".BMP") || files.Contains(".bmp"))
                        {
                            if (0 == count)
                            {
                                firstMat = CvInvoke.Imread(files);

                                matList.Add(files, firstMat);
                            }
                            else
                            {
                                tempMat = CvInvoke.Imread(files);

                                if (tempMat.Width == firstMat.Width &&
                                   tempMat.Height == firstMat.Height &&
                                   tempMat.NumberOfChannels == firstMat.NumberOfChannels &&
                                   tempMat.Depth == firstMat.Depth)
                                {
                                    matList.Add(files, tempMat);
                                }
                            }
                        }
                    }

                    if (matList.Count > 0)
                    {
                        cameraBasic.FileBuffer.SetBufferInfo(matList.Count,
                                                                firstMat.Width,
                                                                firstMat.Height,
                                                                firstMat.Depth,
                                                                firstMat.NumberOfChannels);

                        foreach (KeyValuePair<string, Mat> item in matList)
                        {
                            if (iInageFileCount > 0)
                                pbrOpenImageFolder.PerformStep();

                            cameraBasic.FileBuffer.AddImage(item.Value, item.Key);
                        }

                        ImageInfo Info = cameraBasic.FileBuffer.GetImageInfo(0);
                        cameraBasic.MatFrame = Info.MatImage;
                        cameraBasic.AOITool.SetImage(cameraBasic.MatFrame);

                        FormMainUpdate.ShowImageUpdate(cameraBasic.MatFrame);

                        //DisplayImageBox.Image = cameraBasic.MatFrame;
                        //DisplayImageBox.Refresh();

                        //ShowAlignment();

                        TestButton.BackColor = Color.White;
                        PreTestButton.BackColor = Color.White;
                        NextTestButton.BackColor = Color.White;

                        toolStripStatusLabel3.Text = Info.FileName;


                    }
                    matList.Clear();

                    pbrOpenImageFolder.Visible = false;
                }

            }
            catch (IOException err)
            {
                MessageBox.Show("btnOpenImageFolder_Click " + err.Message);
            }

            btn.Enabled = true;
        }

        private void tkbrImageTop_ValueChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            TrackBar bar = (TrackBar)sender;

            if(bar.Name.Equals("tkbrROITop"))
            {
                if (bar.Value >= tkbrROIBottom.Value)
                    bar.Value = tkbrROIBottom.Value - 1;

                tbROITop.Text = bar.Value.ToString();

            }
            else if (bar.Name.Equals("tkbrROIBottom"))
            {
                if (bar.Value <= tkbrROITop.Value)
                    bar.Value = tkbrROITop.Value + 1;

                tbROIBottom.Text = bar.Value.ToString();
            }
            else
            {
                return;
            }

            cameraBasic.AOITool.SetROITop(tkbrROITop.Value);
            cameraBasic.AOITool.SetROIBottom(tkbrROIBottom.Value);

            int topY = (int)(cameraBasic.AOITool.ROITop*ZoomRadioY);
            int bottonY = (int)(cameraBasic.AOITool.ROIBottom*ZoomRadioY);
            Rectangle roi = new Rectangle
            {
                Location = new Point(0, topY),
                Size = new Size(640, bottonY - topY)
            };

            FilterImageBox.Location = new Point(roi.X, roi.Y); 
            FilterImageBox.Size = new Size(roi.Width, roi.Height);

            DrawTopLineAndBottomLine();
        }
        private void DrawTopLineAndBottomLine()
        {
            FilterImageBox.Visible = false;
            FilterImageBox.Refresh();

            DisplayImageBox.Refresh();

            Point topStartPt = new Point(0, cameraBasic.AOITool.ROITop);
            Point topEndPt = new Point(3840, cameraBasic.AOITool.ROITop);
            Point bottomStartPt = new Point(0, cameraBasic.AOITool.ROIBottom);
            Point bottomEndPt = new Point(3840, cameraBasic.AOITool.ROIBottom);

            using (Pen pen = new Pen(Color.Blue, 2))
            {
                Graphics eGraphics = DisplayImageBox.CreateGraphics();

                eGraphics.DrawLine(pen, ZoomPoint(topStartPt), ZoomPoint(topEndPt));
                eGraphics.DrawLine(pen, ZoomPoint(bottomStartPt), ZoomPoint(bottomEndPt));
            }
        }

        private Point ZoomPoint(Point point)
        {
            return new Point((int)(point.X * ZoomRadioX), (int)(point.Y * ZoomRadioY));
        }

        private void btnShowROI_Click(object sender, EventArgs e)
        {
            DrawTopLineAndBottomLine();
        }

        private void sbrROITop_ValueChanged(object sender, ScrollEventArgs e)
        {

        }

        private void tkbrBinaryTHH_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!FilterImageBox.Visible) FilterImageBox.Visible = true;

            if (FilterImageBox.Visible)
            {
                FilterImageBox.Image = cameraBasic.AOITool.DoBinary();
                FilterImageBox.Refresh();
            }
        }

        private void udFilterMask_ValueChanged(object sender, EventArgs e)
        {
            cameraBasic.AOITool.FilterMask = (int)udFilterMask.Value;
            btnShowBinary.PerformClick();

        }

        private void cbManualBinary_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            cameraBasic.AOITool.ManualBinary = cb.Checked;

            UI_Enabled();
        }

        private void UI_Enabled()
        {
            gbBinaryTHL.Enabled = cbManualBinary.Checked;
        }

        private void tkbrBinaryTHL_ValueChanged(object sender, EventArgs e)
        {
            TrackBar bar = (TrackBar)sender;
            cameraBasic.AOITool.BinaryTHL = (byte)bar.Value;
            tbBinaryTHL.Text = bar.Value.ToString();

            if (!FilterImageBox.Visible) FilterImageBox.Visible = true;

            if (FilterImageBox.Visible)
            {
                FilterImageBox.Image = cameraBasic.AOITool.DoManualBinary();
                FilterImageBox.Refresh();
            }
        }

        private void DisplayImageBox_MouseMove(object sender, MouseEventArgs e)
        {
            //轉換為實際影像位置
            int RealX, RealY;
            RealX = (int)((double)e.X / ZoomRadioX + 0.5);
            RealY = (int)((double)e.Y / ZoomRadioY + 0.5);

            ShowImageInfo(RealX, RealY);

        }

        private void ShowImageInfo(int realX, int realY)
        {
            if (realX < 0) realX = 0;
            if (realY < 0) realY = 0;
            if (realX >= 3840) realX = 3840-1;
            if (realY >= 2748) realY = 2748-1;

            toolStripStatusLabel1.Text = "X:" + realX + ", Y:" + realY.ToString() + 
                "(" + ((int)(realX/cameraBasic.AOITool.PixelPerMM*1000+0.5)/1000.0).ToString() + 
                "," + ((int)(realY/cameraBasic.AOITool.PixelPerMM*1000+0.5)/1000.0).ToString() + ")";

            if (cameraBasic.AOITool.SetImageReady)
            {
                Image<Gray, byte> img1 = cameraBasic.MatFrame.ToImage<Gray, byte>();

                byte Gray = img1.Data[realY, realX, 0];
                toolStripStatusLabel2.Text = "Gray: " + Gray.ToString();
            }
        }

        private void FilterImageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!FilterImageBox.Visible) return;

            int RealX, RealY;
            RealX = (int)((double)(e.X + FilterImageBox.Left) / ZoomRadioX + 0.5);
            RealY = (int)((double)(e.Y + FilterImageBox.Top) / ZoomRadioY + 0.5);

            ShowImageInfo(RealX, RealY);
        }
        private void UIInitial()
        {
            lbNotchPosX.Text = "0.00";
            lbNotchPosY.Text = "0.00";

            lbTopPosX.Text = "0.00";
            lbTopPosY.Text = "0.00";

            lbAvgTopPosX.Text = "0.00";
            lbAvgTopPosY.Text = "0.00";

            lbAvgNotchPosX.Text = "0.00";
            lbAvgNotchPosY.Text = "0.00";

            lbToffset.Text = "0.00";
            lbNoffset.Text = "0.00";
            lbNDegoffset.Text = "0.00";

            lbShowCurrentCnt.Text = "";

            ShowToffsetLabel.ForeColor = Color.Black;
            lbToffset.ForeColor = Color.Black;

            ShowNDegoffsetLabel.ForeColor = Color.Black;
            lbNDegoffset.ForeColor = Color.Black;
        }
        private void btnCalculateResutl_Click(object sender, EventArgs e)
        {
            if (!StartPreventMultiClick(300)) return;

            UIInitial();

            if (FilterImageBox.Visible) FilterImageBox.Visible = false;

            DisplayImageBox.Image = cameraBasic.MatFrame;
            DisplayImageBox.Refresh();

            ShowResult(cameraBasic.AOITool.Calculate());

            StopPreventMultiClick();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            if (!StartPreventMultiClick(300)) return;

            UIInitial();

            if (FilterImageBox.Visible) FilterImageBox.Visible = false;

            DisplayImageBox.Image = cameraBasic.MatFrame;
            DisplayImageBox.Refresh();

            ShowResult(cameraBasic.AOITool.Calculate());

            StopPreventMultiClick();

        }
        private void DrawCrossPoint(Mat mat, Point point, MCvScalar color, int length, int thickness)
        {
            Point crossPt1 = new Point(point.X - length, point.Y - length);
            Point crossPt2 = new Point(point.X + length, point.Y - length);
            Point crossPt3 = new Point(point.X - length, point.Y + length);
            Point crossPt4 = new Point(point.X + length, point.Y + length);
            CvInvoke.Line(mat, crossPt1, crossPt4, color, thickness);
            CvInvoke.Line(mat, crossPt2, crossPt3, color, thickness);
        }
        private void ShowResult(bool bRet)
        {
            if(!bRet)
            {
                logger.Debug(cameraBasic.AOITool.CalculateResult);
            }
            else
            { 
                Point NotchPt = new Point(cameraBasic.AOITool.NotchPt.X, cameraBasic.AOITool.NotchPt.Y);
                Point TopPt = new Point(cameraBasic.AOITool.TopPt.X, cameraBasic.AOITool.TopPt.Y);

                Mat DrawMat = cameraBasic.MatFrame.Clone();
                DrawCrossPoint(DrawMat, NotchPt, new MCvScalar(0, 0, 255), 25, 10);
                DrawCrossPoint(DrawMat, TopPt, new MCvScalar(255, 0, 0), 25, 10);

                //圓心與Notch位置
                lbNotchPosX.Text = Math.Round(cameraBasic.AOITool.NotchMMPt.X, 3).ToString();
                lbNotchPosY.Text = Math.Round(cameraBasic.AOITool.NotchMMPt.Y, 3).ToString();

                lbTopPosX.Text = Math.Round(cameraBasic.AOITool.TopMMPt.X, 3).ToString();
                lbTopPosY.Text = Math.Round(cameraBasic.AOITool.TopMMPt.Y, 3).ToString();
                DisplayImageBox.Image = DrawMat;
                DisplayImageBox.Refresh();
            }
        }

        private bool StartPreventMultiClick(int ms)
        {
            if ((DateTime.Now - PreventMultiClick).TotalMilliseconds < ms)
                return false;

            return true;
        }

        private void StopPreventMultiClick()
        {
            PreventMultiClick = DateTime.Now;
        }

        private void NextTestButton_Click(object sender, EventArgs e)
        {
            if (!StartPreventMultiClick(300)) return;

            UIInitial();

            if (FilterImageBox.Visible) FilterImageBox.Visible = false;

            Button btnObj = (Button)sender;
            ImageBuffer imageBuffer;
            bool Next = false;
            switch (btnObj.Name)
            {
                case "PreTestButton":
                    Next = false;
                    break;

                case "NextTestButton":
                    Next = true;
                    break;

                default:
                    break;
            }

            if (btnObj.BackColor == Color.White)
            {
                imageBuffer = cameraBasic.FileBuffer;
            }
            else if (btnObj.BackColor == Color.Green)
            {
                imageBuffer = cameraBasic.ImageBuffer;
            }
            else
            {

                MessageBox.Show("未匯入Buffer");
                logger.Info("未匯入Buffer");
                return;
            }

            ImageInfo Info = Next ? imageBuffer.GetNextImageInfo() : imageBuffer.GetPreviousImageInfo();

            toolStripStatusLabel3.Text = Info.FileName;

            cameraBasic.SetImage(Info.MatImage);

            ShowResult(cameraBasic.AOITool.Calculate());

            StopPreventMultiClick();
        }
        private void btnTestInfoBackup_Click(object sender, EventArgs e)
        {
            //1.生成當下資料夾
            //2.儲存當下畫面
            //3.儲存當下參數檔
            //4.儲存當下那張影像
            string strDirectory = MachineParas.OutputFolder;
            string strSourceDirectory = Environment.CurrentDirectory;
            string strSubDirectory;
            DateTime dt = DateTime.Now;
            string dateString = dt.ToString("yyyyMMdd");
            string timeString = dt.ToString("HHmmss");
            dateString = @"\" + dateString + "_" + MachineParas.ExportFolder + @"_InfoBackup\";

            strSubDirectory = strDirectory + dateString;

            //1.生成當下資料夾
            if (!Directory.Exists(strSubDirectory))
                Directory.CreateDirectory(strSubDirectory);

            //2.儲存當下畫面
            FormMainUpdate.ScreenCopy(strSubDirectory + timeString + @"_Screen.jpg");

            //3.儲存當下參數檔
            //File.Copy(strSourceDirectory + @"\INI\Config.ini" , strSubDirectory + timeString + @"_Config.ini");
            RegExport(strSubDirectory + timeString + @"_Config.reg", @"HKEY_CURRENT_USER\Software\AlignerVerification");

            //4.儲存當下那張影像
            cameraBasic.MatFrame.Save(strSubDirectory + timeString + @"_Image.bmp");
            cameraBasic.ImageBuffer.SaveImage(strSubDirectory);

            logger.Info("備份完畢");
            MessageBox.Show("備份完畢");
        }

        private void RegExport(string exportPath, string registryPath)
        {
            string path = "\"" + exportPath + "\"";
            string key = "\"" + registryPath + "\"";
            using (Process proc = new Process())
            {
                try
                {
                    proc.StartInfo.FileName = "reg.exe";
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.RedirectStandardError = true;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.Arguments = "export \"" + key + "\" \"" + path + "\" /y";
                    proc.Start();
                    string stdout = proc.StandardOutput.ReadToEnd();
                    string stderr = proc.StandardError.ReadToEnd();
                    proc.WaitForExit();
                }
                catch (Exception)
                {
                    // handle exceptions
                }
            }
        }

        private void ContinousTestCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GeneralSetting(true);

            pbrContinusTest.Visible = false;
        }

        void DoContinousTest(object sender, DoWorkEventArgs e)
        {
            ImageBuffer imageBuffer = (ImageBuffer)e.Argument;

            string strImgBufDirectory = RootDirectory + @"Image\";

            if(Directory.Exists(strImgBufDirectory))
                DeleteFolder(strImgBufDirectory);

            Directory.CreateDirectory(strImgBufDirectory);

            for (int i = 0; i < imageBuffer.ImageCount; i++)
            {
                ImageInfo Info = null;

                Info = imageBuffer.GetImageInfo(i);

                cameraBasic.SetImage(Info.MatImage);

                cameraBasic.AOITool.Calculate();
                PointD O = new PointD
                {
                    X = cameraBasic.AOITool.CenterMMPt.X,
                    Y = cameraBasic.AOITool.CenterMMPt.Y
                };

                Statistics.AddOrigin(O);
                PointD N = new PointD
                {
                    X = cameraBasic.AOITool.NotchMMPt.X,
                    Y = cameraBasic.AOITool.NotchMMPt.Y
                };
                Statistics.AddNotch(N);
                Statistics.AddNotchDeg(cameraBasic.AOITool.Notch_Theta);


                PointD Top = new PointD
                {
                    X = cameraBasic.AOITool.TopMMPt.X,
                    Y = cameraBasic.AOITool.TopMMPt.Y
                };
                Statistics.AddTop(Top);

                Statistics.AddCalibrationPT(cameraBasic.AOITool.CenterPt, cameraBasic.AOITool.NotchPt, cameraBasic.AOITool.TopPt);

                Statistics.FindCalibrateOffset();

                FormMainUpdate.DisplayImageUpdate(strImgBufDirectory, cameraBasic.MatFrame, cameraBasic.AOITool, (float)ZoomRadioX, (float)ZoomRadioY, Info.ID + 1, bSaveBuffer);

                Thread.Sleep(300);

                bwContinousTest.ReportProgress((int)(((double)(i + 1) / (double)imageBuffer.ImageCount) * 100.0));

            }
        }
        private void ContinousTestProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbrContinusTest.Value = e.ProgressPercentage;
        }

        bool bSaveBuffer = false;
        private void btnContinusTest_Click(object sender, EventArgs e)
        {
            if(!bwContinousTest.IsBusy)
            {
                ImageBuffer imageBuffer;
                bSaveBuffer = false;

                UIInitial();
                Statistics.Reset();
                if (FilterImageBox.Visible) FilterImageBox.Visible = false;

                if (TestButton.BackColor == Color.White)
                {
                    imageBuffer = cameraBasic.FileBuffer;
                }
                else if (TestButton.BackColor == Color.Green)
                {
                    bSaveBuffer = true;
                    imageBuffer = cameraBasic.ImageBuffer;
                }
                else
                {
                    MessageBox.Show("未匯入Buffer");
                    logger.Info("未匯入Buffer");
                    return;
                }

                pbrContinusTest.Step = 1;

                pbrContinusTest.Visible = true;

                //關閉UI
                GeneralSetting(false);
                bwContinousTest.RunWorkerAsync(imageBuffer);
            }


        }

        private void btnCylinderHold_Click(object sender, EventArgs e)
        {
            if (!StartPreventMultiClick(300)) return;

            DoAlignerHold();

            StopPreventMultiClick();
        }
        private bool DoAlignerHold()
        {
            SetAlignerCommand("$1CMD:WHLD_:1");

            if (!EvtManager.AlignerWHLDFinishEvt.WaitOne(3000))
            {
                FormMainUpdate.MessageLogUpdate("DoAlignerHold()", "Timeout");
                return false;
            }

            return true;
        }
        private bool DoAlignerXOffset()
        {
            if (MachineParas.AlignXOffset.ToString().Equals("0"))
                return true;

            FormMainUpdate.MessageLogUpdate("DoAlignerXOffset()", MachineParas.AlignXOffset.ToString());

            SetAlignerCommand("$1CMD:MOVED:1,2,"+ MachineParas.AlignXOffset.ToString());

            if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000))
            {
                FormMainUpdate.MessageLogUpdate("DoAlignerXOffset()", "Timeout");
                return false;
            }
            return true;
        }
        private bool DoClampAlignerZMidDown()
        {
            SetAlignerCommand("$1CMD:MOVDP:1989,01000,00");
            
            if (!EvtManager.AlignerMovdpFinishEvt.WaitOne(3000))
            {
                FormMainUpdate.MessageLogUpdate("DoClampAlignerZMidDown()", "Timeout");
                return false;
            }
            return true;
        }
        private bool DoClampAlignerXClamp()
        {
            SetAlignerCommand("$1CMD:MOVDP:1989,10000,00");
            if (!EvtManager.AlignerMovdpFinishEvt.WaitOne(3000))
            {
                FormMainUpdate.MessageLogUpdate("DoClampAlignerXClamp()", "Timeout");
                return false;
            }
            return true;
        }
        private bool DoClampAlignerXUnClamp()
        {
            SetAlignerCommand("$1CMD:MOVED:5,2,+00002000");
            if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000))
            {
                FormMainUpdate.MessageLogUpdate("DoClampAlignerXUnClamp()", "Timeout");
                return false;
            }
            return true;
        }

        private bool DoClampAlignerZDown()
        {
            EvtManager.AlignerMoveFinishEvt.Reset();
            SetAlignerCommand("$1CMD:MOVED:4,1,+00000000");
            if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000))
            {
                FormMainUpdate.MessageLogUpdate("DoClampAlignerZDown()", "Timeout");
                return false;
            }
            return true;
        }
        private bool DoClampAlignerZUp()
        {
            SetAlignerCommand("$1CMD:MOVED:4,1,+00006000");
            if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000))
            {
                FormMainUpdate.MessageLogUpdate("DoClampAlignerZup()", "Timeout");
                return false;
            }
            return true;
        }

        private void GetAlignerRawData(int n)
        {
            DeviceController dc = deviceMap["Aligner"];
            FormMainUpdate.MessageLogUpdate("GetAlignerRawData", "Start");

            //2.生成輸出資料夾
            //string strDirectory = MachineParas.OutputFolder;
            //DateTime dt = DateTime.Now;
            //string dateString = dt.ToString("yyyyMMdd");
            //dateString = @"\" + dateString + "_" + MachineParas.ExportFolder + @"\";

            //string strSubDirectory = strDirectory + dateString;
            string strSubDirectory = RootDirectory;
            if (!Directory.Exists(strSubDirectory))
                Directory.CreateDirectory(strSubDirectory);

            dc.RawData = "";
            EvtManager.AlignerSetAlignACKEvt.Reset();
            SetAlignerCommand("$1GET:ALIGN:9");

            string FileName = strSubDirectory + "n" + n.ToString() + ".csv";

            if (EvtManager.AlignerSetAlignACKEvt.WaitOne(300000))
            {
                string data = dc.RawData;
                data = data.Replace("$1ACK:ALIGN:9\r","");

                string[] lines = data.Split('\r');
                logger.Info("2" + lines.Length.ToString());
                using (StreamWriter sw = new StreamWriter(FileName))
                {
                    foreach (string str in lines)
                    {
                        sw.WriteLine(str);
                    }

                    sw.Close();
                }
            }
            else
            {
                FormMainUpdate.MessageLogUpdate("GetAlignerRawData", "Timeout");
            }

            FormMainUpdate.MessageLogUpdate("GetAlignerRawData", "Finsih");
        }

        private void GetAlignerRawDataByFtp(int n)
        {

            FormMainUpdate.MessageLogUpdate("GetAlignerRawDataByFtp", "Start");

            string ftpURL = @"ftp://192.168.0.135:21/";
            string ftpFilePath = ftpURL + @"aligner_ccd_data.csv";

            string tempStoragePath = RootDirectory + "n" + n.ToString() + ".csv";


            EvtManager.AlignerLOGSVFinishEvt.Reset();
            SetAlignerCommand("$1SET:LOGSV");
            if (!EvtManager.AlignerLOGSVFinishEvt.WaitOne(120000))
            {
                FormMainUpdate.MessageLogUpdate("GetAlignerRawDataByFtp", "Timeout");
                return;
            }

            //FtpWebRequest
            FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(ftpFilePath);
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;

            //FtpWebResponse
            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            //Get Stream From FtpWebResponse
            Stream ftpStream = ftpResponse.GetResponseStream();
            using (FileStream fileStream = new FileStream(tempStoragePath, FileMode.Create))
            {
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    fileStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
            }
            ftpStream.Close();
            ftpResponse.Close();

            FormMainUpdate.MessageLogUpdate("GetAlignerRawDataByFtp", "End");
        }

        private void SetAlignerCommand(string cmd)
        {
            if(cmd.ToUpper().Contains("MOVED"))
            {
                EvtManager.AlignerMoveFinishEvt.Reset();
            }

            string DeviceName = "Aligner";
            DeviceController dctrl = deviceMap[DeviceName];
            if (dctrl._Config.Enable)   dctrl.sendCommand(cmd);
        }
        private void btnCylinderRelease_Click(object sender, EventArgs e)
        {
            if (!StartPreventMultiClick(300)) return;

            DoAlignerRelease();

            StopPreventMultiClick();
        }
        private bool DoAlignerRelease()
        {
            SetAlignerCommand("$1CMD:WRLS_:1");
            if (!EvtManager.AlignerWRLSFinishEvt.WaitOne(3000))
            {
                FormMainUpdate.MessageLogUpdate("DoAlignerRelease()", "Timeout");
                return false;
            }
            return true;
        }
        private void AlignerIniButton_Click(object sender, EventArgs e)
        {
            //避免重複執行
            if (!bwAlignerIni.IsBusy)
            {
                //關閉UI
                GeneralSetting(false);

                bwAlignerIni.RunWorkerAsync();
            }

        }
        private bool DoAlignerHome()
        {
            SetAlignerCommand("$1CMD:HOME_");
            if (!EvtManager.AlignerHOMEFinishEvt.WaitOne(10000))
            {
                FormMainUpdate.MessageLogUpdate("DoAlignerHome()", "SetAlignerCommand($1CMD: HOME_: Timeout");

                return false;
            }
            return true;
        }
        private bool SetAlignerSpeed()
        {
            int TestSpeed = MachineParas.TestSpeed;
            if (TestSpeed == 100) TestSpeed = 0;
            SetAlignerCommand("$1SET:SP___:" + TestSpeed.ToString());
            if (!EvtManager.AlignerSetSpeedFinishEvt.WaitOne(10000))
            {
                FormMainUpdate.MessageLogUpdate("SetAlignerSpeed()", "Timeout");
                return false;
            }
            return true;
        }
        private void AlignButton_Click(object sender, EventArgs e)
        {
            //避免重複執行
            if (!bwAlignerIni.IsBusy)
            {
                //關閉UI
                GeneralSetting(false);
                bwDoAlign.RunWorkerAsync();
            }
        }

        void DoAlign(object sender, DoWorkEventArgs e)
        {
            DoAlign();
        }

        void DoWaferAlignment(object sender, DoWorkEventArgs e)
        {
            if (DoAlign())
            {
                if (MachineParas.MachineType == 0)
                {
                    if (DoCylinderRaiseWafer())
                    {
                        Thread.Sleep(1000);

                        if (DoAlignerHome())
                        {
                            DoCylinderLowerWafer();
                        }
                    }
                    
                }
            }
        }
        void DoRepeatMotionTest(object sender, DoWorkEventArgs e)
        {
            int n = 5;

            //歸零
            //SetAlignerCommand("$1CMD:MOVED:3,1,0");
            //if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000)) return;

            //取像
            FormMainUpdate.MessageLogUpdate("DoRepeatMotionTest()", "Initial_DoGrab()");

            if (!DoGrab()) return;
            Thread.Sleep(iCrabDelayTime);
            for(int i = 0; i< n; i++)
            {
                FormMainUpdate.MessageLogUpdate("DoRepeatMotionTest()", "(" + (i+1).ToString() + ")");

                SetAlignerCommand("$1CMD:MOVED:3,1," + (-360000*(i+1)).ToString());


                if (!EvtManager.AlignerMoveFinishEvt.WaitOne(5000)) return;

                Thread.Sleep(iCrabDelayTime);

                //取像
                FormMainUpdate.MessageLogUpdate("DoRepeatMotionTest()", "DoGrab()");
                if (!DoGrab()) return;
                Thread.Sleep(2000);
            }

            //歸零
            //EvtManager.AlignerMoveFinishEvt.Reset();
            //SetAlignerCommand("$1CMD:MOVED:3,1,00000000");
            //if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000)) return;

            //取像
            FormMainUpdate.MessageLogUpdate("DoRepeatMotionTest()", "Final_DoGrab()");
            if (!DoGrab()) return;

        }

        private bool DoAlign()
        {
            bool result = true;
            if (MachineParas.MachineType == 0)
            {
                string waferType = ",0";

                if (MachineParas.WaferType == 0)
                {
                    waferType = ",0";
                }
                else if (MachineParas.WaferType == 1)
                {
                    waferType = ",5";
                }
                else
                {
                    waferType = ",6";
                }

                FormMainUpdate.MessageLogUpdate("DoAlign()", "$1SET:ALIGN:" + MachineParas.WaferRadius.ToString() + waferType);

                EvtManager.AlignerSetAlignACKEvt.Reset();
                SetAlignerCommand("$1SET:ALIGN:" + MachineParas.WaferRadius.ToString() + waferType);
                //設定WaferRadius
                if (!EvtManager.AlignerSetAlignACKEvt.WaitOne(10000))
                {
                    FormMainUpdate.MessageLogUpdate("DoAlign()", "SetAlignerCommand($1SET:ALIGN) Timeout");
                    return false;
                }

                FormMainUpdate.MessageLogUpdate("DoAlign()", "$1SET:ALIGN:" + MachineParas.WaferRadius.ToString() + waferType + "Finish");

            }
            FormMainUpdate.MessageLogUpdate("DoAlign()", "SetAlignerSpeed()");
            if (!SetAlignerSpeed()) return false;
            FormMainUpdate.MessageLogUpdate("DoAlign()", "SetAlignerSpeed() Finish");

            FormMainUpdate.MessageLogUpdate("DoAlign()", "DoAlignerHome()");
            if (!DoAlignerHome()) return false;
            FormMainUpdate.MessageLogUpdate("DoAlign()", "DoAlignerHome() Finish");

            if (MachineParas.MachineType == 0)
            {
                FormMainUpdate.MessageLogUpdate("DoAlign()", "DoCylinderLowerWafer()");
                if (!DoCylinderLowerWafer()) return false;
                FormMainUpdate.MessageLogUpdate("DoAlign()", "DoCylinderLowerWafer() Finish");
            }

            TackSTime = DateTime.Now;

            if (MachineParas.MachineType == 0)
            {
                FormMainUpdate.MessageLogUpdate("DoAlign()", "$1CMD:ALIGN:" + MachineParas.NotchAngle.ToString());
                SetAlignerCommand("$1CMD:ALIGN:" + MachineParas.NotchAngle.ToString());
            }
            else
            {
                FormMainUpdate.MessageLogUpdate("DoAlign()", "$1CMD:ALIGN:" + MachineParas.NotchAngle.ToString() + ",1,2,1");
                SetAlignerCommand("$1CMD:ALIGN:" + MachineParas.NotchAngle.ToString()+",1,2,1");
            }

            if(!EvtManager.AlignerSetAlignFinishEvt.WaitOne(30000))
            {
                FormMainUpdate.MessageLogUpdate("DoAlign()", "$1CMD:ALIGN:" + MachineParas.NotchAngle.ToString()+ "Timeout");
                result = false;
            }


            TackTime = (DateTime.Now - TackSTime).TotalSeconds;

            ////顯示Aligner時間
            FormMainUpdate.AlignerTackTimeUpdate(TackTime);

            //確認Present是否ON
            if (MachineParas.CheckWaferPresentInAutoRun)
            {
                if (!CheckAlignerIO(8))
                {
                    result = false;
                }
            }


            //Align後X方向位移
            if (MachineParas.MachineType == 0)
            {
                FormMainUpdate.MessageLogUpdate("DoAlign()", "DoAlignerXOffset()");
                if (!DoAlignerXOffset())
                {
                    result = false;
                }
                FormMainUpdate.MessageLogUpdate("DoAlign()", "DoAlignerXOffset() Finish");
            }

             return result;
        }
        bool IsRun = false;
        int RepeatCnt;

        private bool DoCylinderRaiseWafer()
        {
            bool IsRun = true;
            FormMainUpdate.MessageLogUpdate("DoCylinderRaiseWafer()", "Aligner Release");
            if (!DoAlignerRelease())    IsRun = false;

            if(IsRun)
            {
                FormMainUpdate.MessageLogUpdate("DoCylinderRaiseWafer()", "Cylinder Move Up");
                DoCylinderJob("MoveUp");
            }


            return IsRun;
        }

        private bool DoCylinderLowerWafer()
        {
            bool IsRun = true;
            FormMainUpdate.MessageLogUpdate("DoCylinderLowerWafer()", "Cylinder Move Down");
            DoCylinderJob("MoveDown");

            FormMainUpdate.MessageLogUpdate("DoCylinderLowerWafer()", "Aligner Hold");
            if (!DoAlignerHold())
            {
                IsRun = false;
            }
            return IsRun;
        }

        string RootDirectory;
        private void RunButton_Click(object sender, EventArgs e)
        {
            if (!StartPreventMultiClick(1000)) return;

            logger.Debug("RunButton_Click : IsRun = !IsRun");
            IsRun = !IsRun;

            if(IsRun)
            {
                UIInitial();

                if (FilterImageBox.Visible) FilterImageBox.Visible = false;
                //1.確認硬體通訊
                foreach(DeviceController dc in deviceMap.Values)
                {
                    if(dc._Config.Enable)
                    {
                        if(!dc._IsConnected)
                        {
                            FormMainUpdate.MessageLogUpdate(dc._Config.DeviceName, "未連線");
                            IsRun = false;
                            return;
                        }
                    }
                }

                //2.生成輸出資料夾
                string strDirectory = MachineParas.OutputFolder;
                DateTime dt = DateTime.Now;
                string dateString = dt.ToString("yyyyMMdd");
                dateString = @"\" + dateString + "_" + MachineParas.ExportFolder + @"\";
                RootDirectory = strDirectory + dateString;

                logger.Debug(@"RunButton_Click : if (Directory.Exists("+ RootDirectory + "))");
                if (Directory.Exists(RootDirectory))
                {
                    Thread.Sleep(3000);
                    logger.Debug(@"RunButton_Click : DeleteFolder("+ RootDirectory + ")");
                    DeleteFolder(RootDirectory);
                }

                logger.Debug(@"RunButton_Click : Directory.CreateDirectory("+ RootDirectory + ")");
                Directory.CreateDirectory(RootDirectory);

                logger.Debug(@"RunButton_Click : cameraBasic.FileBuffer.Reset()");
                cameraBasic.FileBuffer.Reset();
                logger.Debug(@"RunButton_Click : cameraBasic.ImageBuffer.Reset()");
                cameraBasic.ImageBuffer.Reset();
                logger.Debug(@"RunButton_Click : Statistics.Reset()");
                Statistics.Reset();
                
                RepeatCnt = (int)TestCountUpDown.Value;

                //關閉UI
                GeneralSetting(false);

                logger.Debug(@"RunButton_Click : bwDoAutoRun.RunWorkerAsync()");
                bwDoAutoRun.RunWorkerAsync();

                RunButton.Text = "Stop";

                TestButton.BackColor = Color.DarkGray;
                PreTestButton.BackColor = Color.DarkGray;
                NextTestButton.BackColor = Color.DarkGray;
            }
            else
            {
                RunButton.Text = "Wait to Stop";
            }

            StopPreventMultiClick();
        }

        public static void DeleteFolder(string dir)
        {
            foreach (string d in Directory.GetFileSystemEntries(dir))
            {
                if (File.Exists(d))
                {
                    FileInfo fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d);//直接删除其中的文件   
                }
                else
                    DeleteFolder(d);//递归删除子文件夹   
            }
            Directory.Delete(dir);//删除已空文件夹   
        }
        private bool DoClampAlignerTestModeMotion()
        {
            bool IsRun = true;
            if ((((MachineParas.TestModeTOffset % 360000) % 120000) < 30000 &&
                ((MachineParas.TestModeTOffset % 360000) % 120000) >= 0) ||
                (((MachineParas.TestModeTOffset % 360000) % 120000) < -90000 &&
                ((MachineParas.TestModeTOffset % 360000) % 120000) < 120000))
            {
                SetAlignerCommand("$1CMD:MOVED:3,2," + ((MachineParas.TestModeTOffset % 360000) + 30000).ToString());
                if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000)) IsRun = false;

                if(IsRun)
                    if (!DoClampAlignerGoHome()) IsRun = false;

                if (IsRun)
                    if (!DoClampAlignerZMidDown()) IsRun = false;

                if (IsRun)
                    if (!DoClampAlignerXClamp()) IsRun = false;

                if (IsRun)
                    if (DoClampAlignerZDown()) IsRun = false;
            }
            else
            {
                SetAlignerCommand("$1CMD:MOVED:3,2," + (MachineParas.TestModeTOffset % 360000).ToString());
                if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000)) IsRun = false;
            }

            return IsRun;
        }

        private bool DoClampAlignerGoHome()
        {
            bool IsRun = true;

            if (!DoClampAlignerZMidDown()) IsRun = false;

            if (IsRun)
                if (!DoClampAlignerXUnClamp()) IsRun = false;

            if (IsRun)
                if (DoClampAlignerZUp()) IsRun = false;

            if (IsRun)
                if (!DoAlignerHome()) IsRun = false;

            return IsRun;
        }
        void DoAutoRun(object sender, DoWorkEventArgs e)
        {
            AutoRun();
        }
        private void AutoRun()
        {
            int CurrntCnt = 0;
            DeviceController Camera = deviceMap["Camera"];
            DeviceController Aligner = deviceMap["Aligner"];

            int XOffset = MachineParas.TestModeXOffset;
            int YOffset = MachineParas.TestModeYOffset;
            int TOffset = MachineParas.TestModeTOffset;

            if (!Directory.Exists(RootDirectory))
                Directory.CreateDirectory(RootDirectory);

            while (IsRun)
            {
                if(CurrntCnt == RepeatCnt)
                {
                    IsRun = false;
                    break;
                }

                //產生偏移
                if (CurrntCnt != 0)
                {
                    FormMainUpdate.MessageLogUpdate("AutoRun", "Move Wafer to Position");

                    //Vacuum type
                    if (MachineParas.MachineType == 0)
                    {
                        IsRun = DoCylinderRaiseWafer();
                        //if (!IsRun) break;
                    }

                    //Aligner回Home
                    FormMainUpdate.MessageLogUpdate("AutoRun", "Aligner Go Home");
                    if (!DoAlignerHome())
                    {
                        IsRun = false;
                        //break;
                    }

                    //Vacuum type
                    FormMainUpdate.MessageLogUpdate("AutoRun", "Go X Forward");
                    if (MachineParas.MachineType == 0)
                    {
                        if(!DoAlignerXOffset())
                        {
                            IsRun = false;
                            //break;
                        }

                        if(!DoCylinderLowerWafer())
                        {
                            IsRun = false;
                            //break;
                        }
                    }
                    else
                    {
                        //Z MID-DOWN
                        if(!DoClampAlignerZMidDown())
                        {
                            IsRun = false;
                            //break;
                        }

                        if(!DoClampAlignerXClamp())
                        {
                            IsRun = false;
                            //break;
                        }

                        if (!DoClampAlignerZDown())
                        {
                            IsRun = false;
                            //break;
                        }
                    }

                    //Do Offset
                    //enum eTestMode { eFix = 0, eStep_Center, eStep_Notch };
                    switch (MachineParas.TestMode)
                    {
                        case (int)eTestMode.eFix:
                            if(MachineParas.MachineType == 0)
                            {
                                SetAlignerCommand("$1CMD:MOVED:3,2," + (TOffset%360000).ToString());
                                if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000)) IsRun = false;

                                if(IsRun)
                                    if (!DoCylinderRaiseWafer()) IsRun = false;

                                if (IsRun)
                                {
                                    SetAlignerCommand("$1CMD:MOVED:1,2," + (XOffset% 8000).ToString());
                                    if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000))
                                    {
                                        FormMainUpdate.MessageLogUpdate("AutoRun", "$1CMD:MOVED:5,2,+00002000");
                                        IsRun = false;
                                    }
                                }

                                if (IsRun)
                                {
                                    SetAlignerCommand("$1CMD:MOVED:2,2," + (YOffset % 8000).ToString());
                                    if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000))
                                    {
                                        FormMainUpdate.MessageLogUpdate("AutoRun", "$1CMD:MOVED:5,2,+00002000");
                                        IsRun = false;
                                    }
                                }
                            }
                            else
                            {
                                IsRun = DoClampAlignerTestModeMotion();
                            }
                            break;
                        case (int)eTestMode.eStep_Center:
                            if (MachineParas.MachineType == 0)
                            {
                                SetAlignerCommand("$1CMD:MOVED:3,2," + (TOffset % 360000).ToString());
                                if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000)) IsRun = false;

                                if (IsRun)
                                    if (!DoCylinderRaiseWafer()) IsRun = false;

                                if (IsRun)
                                {
                                    double d1 = Math.Cos((double)(YOffset * CurrntCnt)* Math.PI / 180000.0);
                                    EvtManager.AlignerMoveFinishEvt.Reset();
                                    SetAlignerCommand("$1CMD:MOVED:1,2," +((int)((double)XOffset* d1) %8000).ToString());
                                    if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000))
                                    {
                                        FormMainUpdate.MessageLogUpdate("AutoRun", "$1CMD:MOVED:5,2,+00002000");
                                        IsRun = false;
                                    }
                                }

                                if (IsRun)
                                {
                                    double d2 = Math.Sin((double)(YOffset * CurrntCnt) * Math.PI / 180000.0);
                                    SetAlignerCommand("$1CMD:MOVED:2,2," + ((int)((double)XOffset * d2) % 8000).ToString());
                                    if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000))
                                    {
                                        FormMainUpdate.MessageLogUpdate("AutoRun", "$1CMD:MOVED:5,2,+00002000");
                                        IsRun = false;
                                    }
                                }
                            }
                            else
                            {
                                IsRun = DoClampAlignerTestModeMotion();
                            }
                            break;
                        case (int)eTestMode.eStep_Notch:
                            if (MachineParas.MachineType == 0)
                            {
                                SetAlignerCommand("$1CMD:MOVED:3,2," + (TOffset*CurrntCnt% 360000).ToString());
                                if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000)) IsRun = false;

                                if (IsRun)
                                    if (!DoCylinderRaiseWafer()) IsRun = false;

                                if (IsRun)
                                {
                                    SetAlignerCommand("$1CMD:MOVED:1,2," + (XOffset% 8000).ToString());
                                    if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000))
                                    {
                                        FormMainUpdate.MessageLogUpdate("AutoRun", "$1CMD:MOVED:5,2,+00002000");
                                        IsRun = false;
                                    }
                                }

                                if (IsRun)
                                {
                                    SetAlignerCommand("$1CMD:MOVED:2,2," + (YOffset % 8000).ToString());
                                    if (!EvtManager.AlignerMoveFinishEvt.WaitOne(3000))
                                    {
                                        FormMainUpdate.MessageLogUpdate("AutoRun", "$1CMD:MOVED:5,2,+00002000");
                                        IsRun = false;
                                    }
                                }
                            }
                            else
                            {
                                IsRun = DoClampAlignerTestModeMotion();
                            }
                            break;

                        default:
                            IsRun = false;
                            break;
                    }

                    //if (!IsRun) break;

                    if(MachineParas.MachineType == 0)
                    {
                        if(!DoCylinderLowerWafer())
                        {
                            IsRun = false;
                            //break;
                        }
                    }
                    else
                    {
                        if(!DoClampAlignerGoHome())
                        {
                            IsRun = false;
                            //break;
                        }
                    }
                }

                //Aligner
                bool AlignError = false;

                if(!DoAlign())
                {
                    IsRun = false;
                    //break;
                }
   
                Statistics.AddTackTime(TackTime);
                FormMainUpdate.AlignerTackTimeInfoUpdate();

                if (!Aligner.FINReturnCode.Equals("00000000"))
                    AlignError = true;

                //紀錄資料
                if (AlignError || DownloadData)
                {
                    FormMainUpdate.MessageLogUpdate("AutoRun", "Download Data");

                    DeviceConfig AlignerConfig = deviceConfig["Aligner"];
                    if (AlignerConfig.ConnectionType.Equals("ComPort"))
                    {
                        GetAlignerRawData(CurrntCnt + 1);
                    }
                    else
                    if (AlignerConfig.ConnectionType.Equals("Socket"))
                    {
                        GetAlignerRawDataByFtp(CurrntCnt + 1);
                    }

                    if (AlignError)
                    {
                        IsRun = false;
                        //break;
                    }
                }

                //Save Data
                FormMainUpdate.MessageLogUpdate("AutoRun", "Save Align Data");

                Aligner.DataReceived = "";
                EvtManager.AlignerSetAlignACKEvt.Reset();
                SetAlignerCommand("$1GET:ALIGN:0");
                if (!EvtManager.AlignerSetAlignACKEvt.WaitOne(5000))
                {
                    FormMainUpdate.MessageLogUpdate("AutoRun", "GET:ALIGN:0 Timeout");
                    IsRun = false;
                    //break;
                }

                //string DataFile = strDirectory + dateString + @"Data.csv";               //紀錄資料
                string DataFile = RootDirectory + @"Data.csv";
                using (StreamWriter sw = new StreamWriter(DataFile, true))
                {
                    string str;
                    if (CurrntCnt == 0)
                    { 

                        str = "encoder notch,encoder notch delta," +
                            "encoder offset,ccd offset,encoder 2nd flat," +
                            "encoder data abs,cpld count,ccd average," +
                            "ccd max,num ccd max,encoder ccd max," +
                            "ccd min,num ccd min,encoder ccd min," +
                            "ccd per encoder max,num ccd per encoder max," +
                            "encoder ccd per encoder max,count ccd per encoder max," +
                            "ccd per encoder min,num ccd per encoder min," +
                            "encoder ccd per encoder min,count ccd per encoder min," +
                            "ccd per encoder max,num ccd per encoder max," +
                            "encoder ccd per encoder max,count ccd per encoder max," +
                            "ccd per encoder min,num ccd per encoder min,encoder ccd per encoder min," +
                            "count ccd per encoder min,result,threshold,photo intensity," +
                            "photo switch,photo sense 0,photo sense 1";

                        sw.WriteLine(str);
                    }
                    str = Aligner.RawData;
                    str = str.Replace("$1ACK:ALIGN:", "");
                    str = str.Trim();
                    sw.WriteLine(str);

                    sw.Close();
                }

                //取像
                Thread.Sleep(1000);
                FormMainUpdate.MessageLogUpdate("AutoRun", "Capture");
                if (Camera._Config.Enable)
                {
                    if (Camera._IsConnected)
                    {
                        //取像
                        if (!DoCameraGrab(Camera))
                        {
                            IsRun = false;
                           // break;
                        }
                    }
                }

                string FileName = Environment.CurrentDirectory;
                FileName = FileName + @"\Output\img0\acc0.bmp";

                if (!File.Exists(FileName))
                {
                    IsRun = false;
                    //break;
                }
                FormMainUpdate.MessageLogUpdate("AutoRun", "SetImage");
                cameraBasic.SetImage(CvInvoke.Imread(FileName));
                FormMainUpdate.MessageLogUpdate("AutoRun", "Calculate");
                if (!cameraBasic.AOITool.Calculate())
                {
                    IsRun = false;
                    //break;
                }

                FormMainUpdate.MessageLogUpdate("AutoRun", "Calculate Finish");
                cameraBasic.ImageBuffer.AddImage(cameraBasic.MatFrame);

                PointD O = new PointD
                {
                    X = cameraBasic.AOITool.CenterMMPt.X,
                    Y = cameraBasic.AOITool.CenterMMPt.Y
                };
                Statistics.AddOrigin(O);


                PointD N = new PointD
                {
                    X = cameraBasic.AOITool.NotchMMPt.X,
                    Y = cameraBasic.AOITool.NotchMMPt.Y
                };

                Statistics.AddNotch(N);
                //Statistics.FindCalibrateOffset(N);

                Statistics.AddNotchDeg(cameraBasic.AOITool.Notch_Theta);

                PointD T = new PointD
                {
                    X = cameraBasic.AOITool.TopMMPt.X,
                    Y = cameraBasic.AOITool.TopMMPt.Y
                };

                Statistics.AddTop(T);

                Statistics.AddCalibrationPT(cameraBasic.AOITool.CenterPt, cameraBasic.AOITool.NotchPt, cameraBasic.AOITool.TopPt);
                Statistics.FindCalibrateOffset();

                FormMainUpdate.DisplayImageUpdate(RootDirectory, cameraBasic.MatFrame, cameraBasic.AOITool, (float)ZoomRadioX, (float)ZoomRadioY, CurrntCnt + 1, true);

                //string RawDataFile = strDirectory + dateString + @"RawData.csv";               //紀錄資料
                string RawDataFile = RootDirectory + @"RawData.csv";

                using (StreamWriter sw = new StreamWriter(RawDataFile, true))
                {
                    if (CurrntCnt == 0)
                        sw.WriteLine("Ox,Oy,Nx,Ny,Ndeg,Tack Time,Calibrate_offset_mm,Calibrate_deg");
                    string str;

                    str = Statistics.NowOrigin.X.ToString();
                    str += ",";
                    str += Statistics.NowOrigin.Y.ToString();
                    str += ",";
                    str += Statistics.NowNotch.X.ToString();
                    str += ",";
                    str += Statistics.NowNotch.Y.ToString();
                    str += ",";
                    str += Statistics.NowNotchDeg.ToString();
                    str += ",";
                    str += Statistics.NowTackTime.ToString();
                    str += ",";
                    str += Statistics.CalibrateOffset.ToString();
                    str += ",";
                    str += Statistics.CalibrateDegOffset.ToString();
                    sw.WriteLine(str);

                    sw.Close();
                }

                if(IsRun)
                {
                    if(MachineParas.bAlarmStopEnabled)
                    {
                        if(Statistics.TOffset > MachineParas.dOOffsetUpLimit || 
                            Statistics.NOffsetDeg > MachineParas.dNOffsetUpLimit)
                            IsRun = false;

                    }

                    if (MachineParas.bAlarmStopDownloadData)
                    {
                        if (Statistics.TOffset > MachineParas.dOOffsetUpLimit ||
                            Statistics.NOffsetDeg > MachineParas.dNOffsetUpLimit)
                        {
                            FormMainUpdate.MessageLogUpdate("AutoRun_", "Alarm Stop Download Data");

                            DeviceConfig AlignerConfig = deviceConfig["Aligner"];
                            if (AlignerConfig.ConnectionType.Equals("ComPort"))
                            {
                                GetAlignerRawData(CurrntCnt + 1);
                            }
                            else
                            if (AlignerConfig.ConnectionType.Equals("Socket"))
                            {
                                GetAlignerRawDataByFtp(CurrntCnt + 1);
                            }
                        }

                    }
                }

                CurrntCnt++;
            }

            IsRun = false;
        }
        bool DownloadData = false;
        private void cbDownloadData_CheckedChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            DownloadData = cbDownloadData.Checked;
        }

        private void btnLoadBuffer_Click(object sender, EventArgs e)
        {
            //Load Buffer 
            if (cameraBasic.ImageBuffer.ImageCount == 0)
                return;
            try
            {
                ImageInfo Info = cameraBasic.ImageBuffer.GetImageInfo(0);
                cameraBasic.MatFrame = Info.MatImage;
                cameraBasic.AOITool.SetImage(cameraBasic.MatFrame);

                DisplayImageBox.Image = cameraBasic.MatFrame;
                DisplayImageBox.Refresh();

                TestButton.BackColor = Color.Green;
                PreTestButton.BackColor = Color.Green;
                NextTestButton.BackColor = Color.Green;

                toolStripStatusLabel3.Text = Info.FileName;
            }
            catch (IOException err)
            {
                MessageBox.Show("btnLoadBuffer_Click " + err.Message);
            }
        }

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog
            {
                SelectedPath = MachineParas.OutputFolder
            };

            if (DialogResult.OK == path.ShowDialog())
            {
                MachineParas.OutputFolder = path.SelectedPath;

                lbShowOutputFolder.Text = MachineParas.OutputFolder;
            }
        }

        private void btnDownloadData_Click(object sender, EventArgs e)
        {
            //避免重複執行
            if (!bwDownloadData.IsBusy)
            {
                //關閉UI
                GeneralSetting(false);
                bwDownloadData.RunWorkerAsync();
            }
        }

        void DoDownloadData(object sender, DoWorkEventArgs e)
        {
            DeviceConfig  dc = deviceConfig["Aligner"];

            if(dc.ConnectionType.Equals("ComPort"))
            {
                GetAlignerRawData(-1);
            }
            else
            if(dc.ConnectionType.Equals("Socket"))
            {
                GetAlignerRawDataByFtp(-1);
            }
        }

        private void Calibrate_Click(object sender, EventArgs e)
        {
            string RawDataFile = RootDirectory + @"RawData.csv";               //紀錄資料

            string line;

            double deg_Encoder = 360.0 / 40000.0;
            double mm_CCD = 0.014;
            double sum = 0;

            double amplitude;
            double minDelta = 0;
            double Delta;
            int Angle_offset = 0;

            StreamReader file = new StreamReader(RawDataFile);
            List<double> data1 = new List<double>();
            List<double> data2 = new List<double>();
            List<double> data4 = new List<double>();
            List<double> data5 = new List<double>();
            List<double> data6 = new List<double>();
            List<double> data10 = new List<double>();
            List<double> data11 = new List<double>();
            int lineCnt = 0;
            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    string[] raw = line.Split(',');
                    if (lineCnt > 0)
                    {
                        //旋轉角度差異
                        if (Double.TryParse(raw[7], out double dData1))
                            data1.Add(dData1);

                        //平移距離差異
                        if (Double.TryParse(raw[6], out double dData2))
                            data2.Add(dData2);
                    }
                }
                catch (Exception ex)
                {
                    FormMainUpdate.MessageLogUpdate("Calibrate", "Read Raw Data File Error :" + ex.Message);
                }

                lineCnt++;
            }

            string DataFile = RootDirectory + @"Data.csv";               //紀錄資料
            lineCnt = 0;
            StreamReader Datafile = new StreamReader(DataFile);
            while ((line = Datafile.ReadLine()) != null)
            {
                try
                {
                    string[] raw = line.Split(',');
                    if (lineCnt > 0)
                    {
                        if (Double.TryParse(raw[0], out double dData4))
                            data4.Add(dData4 * deg_Encoder);

                        if (Double.TryParse(raw[2], out double dData5))
                            data5.Add(dData5 * deg_Encoder);

                        if (Double.TryParse(raw[3], out double dData6))
                            data6.Add(dData6 * mm_CCD);
                    }
                }
                catch (Exception ex)
                {
                    FormMainUpdate.MessageLogUpdate("Calibrate", "Read Data File Error :" + ex.Message);
                }

                lineCnt++;
            }

            for (int i = 0; i < data4.Count; i++)
            {
                data11.Add(data5[i] - data4[i]);
            }

            for (int i = 0; i < data1.Count; i++)
                data10.Add(data1[i] - data1.Average());

            sum = 0;
            foreach (double d in data2)
                sum = sum + d * d;

            amplitude = Math.Sqrt(sum / (double)data2.Count);


            for (int i = 0; i < 360; i++)
            {
                sum = 0;
                for (int j = 0; j < data11.Count; j++)
                    sum = sum + Math.Abs(amplitude * Math.Sin((data11[j] + i + 90) * Math.PI / 180) - data2[j]);

                if ( i == 0)
                {
                    minDelta = sum;
                }
                else
                {
                    Delta = sum;
                    if (Delta <= minDelta)
                    {
                        minDelta = Delta;
                        Angle_offset = i;
                    }
                }
            }

            tbParam193.Text = (Angle_offset * 1000).ToString();
            tbParam194.Text = ((int)(amplitude / data6.Average() * 5 / mm_CCD + 0.5)).ToString();

            sum = 0;
            foreach (double d in data10)
                sum = sum + d * d;

            amplitude = Math.Sqrt(sum / (double)data10.Count);

            for (int i = 0; i < 360; i++)
            {
                sum = 0;
                for (int j = 0; j < data11.Count; j++)
                    sum = sum + Math.Abs(amplitude * Math.Sin((data11[j] + i + 180) * Math.PI / 180) - data10[j]);

                if(i == 0)
                {
                    minDelta = sum;
                }
                else
                {
                    Delta = sum;
                    if (Delta <= minDelta)
                    {
                        minDelta = Delta;
                        Angle_offset = i;
                    }
                }
            }

            tbParam195.Text = (Angle_offset * 1000).ToString();
            tbParam196.Text = ((int)(amplitude / data6.Average() * 5 / deg_Encoder + 0.5)).ToString();
        }
        double MonitorTime = 0.0;
        bool MonitorFinish = false;
        bool IsMonitor = false;
        DateTime MonitorStartTime;
        void DoMonitorPresent(object sender, DoWorkEventArgs e)
        {
            while(IsMonitor)
            {
                //脫離監控
                TimeSpan ts = DateTime.Now - MonitorStartTime;
                if (ts.TotalMilliseconds > MonitorTime)
                {
                    IsMonitor = false;
                    MonitorFinish = true;
                    break;
                }

                if(!CheckAlignerIO(8))
                {
                    IsMonitor = false;
                    break;
                }

                FormMainUpdate.PresentMonitorTakeTimeUpdate(ts);

                if (IsMonitor)
                    Thread.Sleep(1000);
            }
        }

        private void udPresentMonitorHour_ValueChanged(object sender, EventArgs e)
        {
            MachineParas.PresentMonitorHour = (double)udPresentMonitorHour.Value;
        }

        private bool CheckAlignerIO(int input)
        {
            FormMainUpdate.MessageLogUpdate("CheckAlignerIO", "Read IO :" + input.ToString().PadLeft(3, '0'));
            SetAlignerCommand("$1GET:RIO__:" + input.ToString().PadLeft(3,'0') );
            if(!EvtManager.AlignerGetRIOEvt.WaitOne(1000))
            {
                FormMainUpdate.MessageLogUpdate("CheckAlignerIO", "Timeout");

                return false;
            }

            rioMap.TryGetValue(input, out IODevice iodc);

            FormMainUpdate.MessageLogUpdate("CheckAlignerIO", "Read IO :" + input.ToString().PadLeft(3, '0') + ", Result = " + iodc.Status);

            return iodc.Status == 1 ? true : false;
        }
        private void udPresentMonitorMin_ValueChanged(object sender, EventArgs e)
        {
            MachineParas.PresentMonitorMin = (double)udPresentMonitorMin.Value;
        }

        private void udPresentMonitorSec_ValueChanged(object sender, EventArgs e)
        {
            MachineParas.PresentMonitorSec = (double)udPresentMonitorSec.Value;
        }

        private void btnStartMonitorPresent_Click(object sender, EventArgs e)
        {
            if (!StartPreventMultiClick(300)) return;

            IsMonitor = !IsMonitor;
            if (IsMonitor)
            {
                GeneralSetting(false);

                lbPresentStatus.BackColor = Color.Lime;
                lbPresentStatus.Text = "監控中";
                lbStartMonitorTime.Text = DateTime.Now.ToString("HH:mm:ss");

                //監控時間
                MonitorTime = ((MachineParas.PresentMonitorHour * 60 + 
                                MachineParas.PresentMonitorMin) *60 + 
                                MachineParas.PresentMonitorSec) *1000;

                MonitorFinish = false;  //正常監控結束為true;
                MonitorStartTime = DateTime.Now;

                bwMonitorPresent.RunWorkerAsync();

                btnStartMonitorPresent.Text = "中斷";
            }
            else
            {
                MonitorFinish = true;
            }

            StopPreventMultiClick();
        }

        private void cbCheckWaferPresentInAutoRun_CheckedChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            MachineParas.CheckWaferPresentInAutoRun = cbCheckWaferPresentInAutoRun.Checked;
        }

        private void btnReadConfigFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "ini files (*.ini)|*.ini|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    //LoadINIFile(filePath);

                    UpdateUI();

                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bwTest.RunWorkerAsync();
        }

        private void cbOffsetType_CheckedChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            Statistics.OffsetType = cbOffsetType.Checked ? 1 : 0;
        }

        private void cbFillWafer_CheckedChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            cameraBasic.AOITool.FillWafer = cbFillWafer.Checked;
        }

        private void cbAlarmStopEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            MachineParas.bAlarmStopEnabled = cbAlarmStopEnabled.Checked;
        }

        private void cbAlarmStopDownloadData_CheckedChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            MachineParas.bAlarmStopDownloadData = cbAlarmStopDownloadData.Checked;
        }

        private void tbOOffsetUpLimit_TextChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            MachineParas.dOOffsetUpLimit = Convert.ToDouble(tbOOffsetUpLimit.Text);
        }

        private void tbNOffsetUpLimit_TextChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            MachineParas.dNOffsetUpLimit = Convert.ToDouble(tbNOffsetUpLimit.Text);
        }

        private void DisplayImageBox_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectAllDevice();
        }

        private void btnWaferAlignment_Click(object sender, EventArgs e)
        {
            if (!bwDoWaferAlignment.IsBusy)
            {
                //關閉UI
                GeneralSetting(false);
                bwDoWaferAlignment.RunWorkerAsync();
            }
        }

        private void btnRepeatMotionTest_Click(object sender, EventArgs e)
        {
            if (!bwDoRepeatMotionTest.IsBusy)
            {
                //關閉UI
                GeneralSetting(false);
                bwDoRepeatMotionTest.RunWorkerAsync();
            }
        }

        int iCrabDelayTime = 1000;

        private void udCrabDelayTime_ValueChanged(object sender, EventArgs e)
        {
            iCrabDelayTime = (int)udCrabDelayTime.Value;
        }

        private void cmbAlignerConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            ComboBox cmb = (ComboBox)sender;
            DeviceConfig dc = null;
            switch(cmb.Name)
            {
                case "cmbAlignerConnectionType":
                    dc = deviceConfig["Aligner"];
                    break;

                case "cmbAligner02ConnectionType":
                    dc = deviceConfig["Aligner02"];
                    break;

                default:
                    break;
            }

            if(dc != null)
                dc.ConnectionType = cmb.Text;

            //DeviceConfig dc = deviceConfig["Aligner"];
            //dc.ConnectionType = cmbAlignerConnectionType.Text;
        }

        private void tbAlignerIPAddress_TextChanged(object sender, EventArgs e)
        {
            if (loadParas) return;

            TextBox tb = (TextBox)sender;
            DeviceConfig dc = null;

            switch (tb.Name)
            {
                case "tbAlignerIPAddress":
                    dc = deviceConfig["Aligner"];
                    break;

                case "tbAligner02IPAddress":
                    dc = deviceConfig["Aligner02"];
                    break;

                default:
                    break;
            }

            if (dc != null)
                dc.IPAdress = tbAlignerIPAddress.Text;


            //DeviceConfig dc = deviceConfig["Aligner"];
            //dc.IPAdress = tbAlignerIPAddress.Text;
        }

        private void btnAlignerCommTest_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string DeviceName = "";
            switch (btn.Name)
            {
                case "btnAlignerCommTest":
                    DeviceName = "Aligner";
                    lbAlignerCommTest.BackColor = Color.Red;
                    break;

                case "btnAligner02CommTest":
                    DeviceName = "Aligner02";
                    lbAligner02CommTest.BackColor = Color.Red;
                    break;
                default:
                    break;
            }

            if(!DeviceName.Equals(""))
            {
                DeviceController dctrl = deviceMap[DeviceName];
                if (dctrl._Config.Enable) dctrl.sendCommand("$1GET:VER__");

                FormMainUpdate.MessageLogUpdate(DeviceName, "通訊測試開始");
                FormMainUpdate.MessageLogUpdate(DeviceName, "詢問版本號");
            }
        }
    }

    public class IODevice
    {
        public int No = -1;
        public string IOName = "Undefined";
        public string Type = "Undefined";
        public int Status = 0;
    }
}
