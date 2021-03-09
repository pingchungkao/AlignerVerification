namespace AlignerVerification
{
    partial class FormMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.ShowResultGroupBox = new System.Windows.Forms.GroupBox();
            this.lbNDegoffset = new System.Windows.Forms.Label();
            this.lbNoffset = new System.Windows.Forms.Label();
            this.lbToffset = new System.Windows.Forms.Label();
            this.ShowNDegoffsetLabel = new System.Windows.Forms.Label();
            this.ShowNoffsetLabel = new System.Windows.Forms.Label();
            this.ShowToffsetLabel = new System.Windows.Forms.Label();
            this.NotchGroupBox = new System.Windows.Forms.GroupBox();
            this.lbAvgNotchPosY = new System.Windows.Forms.Label();
            this.lbAvgNotchPosX = new System.Windows.Forms.Label();
            this.lbNotchPosY = new System.Windows.Forms.Label();
            this.lbNotchPosX = new System.Windows.Forms.Label();
            this.ShowAvgNotchPosYLabel = new System.Windows.Forms.Label();
            this.ShowAvgNotchPosXLabel = new System.Windows.Forms.Label();
            this.ShowNotchPosYLabel = new System.Windows.Forms.Label();
            this.ShowNotchPosXLabel = new System.Windows.Forms.Label();
            this.gbOffSetPos = new System.Windows.Forms.GroupBox();
            this.lbAvgTopPosY = new System.Windows.Forms.Label();
            this.lbAvgTopPosX = new System.Windows.Forms.Label();
            this.lbTopPosY = new System.Windows.Forms.Label();
            this.lbTopPosX = new System.Windows.Forms.Label();
            this.ShowAvgCenterPosYLabel = new System.Windows.Forms.Label();
            this.ShowAvgCenterPosXLabel = new System.Windows.Forms.Label();
            this.ShowCenterPosYLabel = new System.Windows.Forms.Label();
            this.ShowCenterPosXLabel = new System.Windows.Forms.Label();
            this.TaskTimeGroupBox = new System.Windows.Forms.GroupBox();
            this.lbMinTaskTime = new System.Windows.Forms.Label();
            this.lbMaxTaskTime = new System.Windows.Forms.Label();
            this.lbAvgTaskTime = new System.Windows.Forms.Label();
            this.lbTaskTime = new System.Windows.Forms.Label();
            this.ShowMinTaskTimeLabel = new System.Windows.Forms.Label();
            this.ShowMaxTaskTimeLabel = new System.Windows.Forms.Label();
            this.ShowAvgTaskTimeLabel = new System.Windows.Forms.Label();
            this.ShowTaskTimeLabel = new System.Windows.Forms.Label();
            this.DisplayImageBox = new Emgu.CV.UI.ImageBox();
            this.FilterImageBox = new Emgu.CV.UI.ImageBox();
            this.lbShowCurrentCnt = new System.Windows.Forms.Label();
            this.RichTextBox1 = new System.Windows.Forms.RichTextBox();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.LeftTabControl = new System.Windows.Forms.TabControl();
            this.GeneralPage = new System.Windows.Forms.TabPage();
            this.pnGeneral = new System.Windows.Forms.Panel();
            this.WaferRadiusGroupBox = new System.Windows.Forms.GroupBox();
            this.WaferRadiusUpDown = new System.Windows.Forms.NumericUpDown();
            this.gbTestMode = new System.Windows.Forms.GroupBox();
            this.udTestModeTOffset = new System.Windows.Forms.NumericUpDown();
            this.udTestModeYOffset = new System.Windows.Forms.NumericUpDown();
            this.udTestModeXOffset = new System.Windows.Forms.NumericUpDown();
            this.lbTestModeTOffset = new System.Windows.Forms.Label();
            this.lbTestModeYOffset = new System.Windows.Forms.Label();
            this.lbTestModeXOffset = new System.Windows.Forms.Label();
            this.cmbTestMode = new System.Windows.Forms.ComboBox();
            this.TestSpeedGroupBox = new System.Windows.Forms.GroupBox();
            this.cmbTestSpeed = new System.Windows.Forms.ComboBox();
            this.gbCylinderMotion = new System.Windows.Forms.GroupBox();
            this.btnCylinderMoveUp = new System.Windows.Forms.Button();
            this.btnCylinderMoveDown = new System.Windows.Forms.Button();
            this.btnCylinderRelease = new System.Windows.Forms.Button();
            this.btnCylinderHold = new System.Windows.Forms.Button();
            this.NotchAngleGroupBox = new System.Windows.Forms.GroupBox();
            this.udNotchAngle = new System.Windows.Forms.NumericUpDown();
            this.WaferTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.cmbWaferType = new System.Windows.Forms.ComboBox();
            this.AlignXOffsetGroupBox = new System.Windows.Forms.GroupBox();
            this.udAlignXOffset = new System.Windows.Forms.NumericUpDown();
            this.gbMachineType = new System.Windows.Forms.GroupBox();
            this.cmbNotchType = new System.Windows.Forms.ComboBox();
            this.ExportFolderGroupBox = new System.Windows.Forms.GroupBox();
            this.tbExportFolder = new System.Windows.Forms.TextBox();
            this.TestCountGroupBox = new System.Windows.Forms.GroupBox();
            this.TestCountUpDown = new System.Windows.Forms.NumericUpDown();
            this.gbSingleMotion = new System.Windows.Forms.GroupBox();
            this.AlignerIniButton = new System.Windows.Forms.Button();
            this.CylinderIniButton = new System.Windows.Forms.Button();
            this.CalculateButton = new System.Windows.Forms.Button();
            this.AlignButton = new System.Windows.Forms.Button();
            this.GrabButton = new System.Windows.Forms.Button();
            this.RunButton = new System.Windows.Forms.Button();
            this.AOIPage = new System.Windows.Forms.TabPage();
            this.gbAOI = new System.Windows.Forms.GroupBox();
            this.btnReadConfigFile = new System.Windows.Forms.Button();
            this.gbAOISetup = new System.Windows.Forms.GroupBox();
            this.cbFillWafer = new System.Windows.Forms.CheckBox();
            this.cbManualBinary = new System.Windows.Forms.CheckBox();
            this.gbBinaryTHL = new System.Windows.Forms.GroupBox();
            this.tkbrBinaryTHL = new System.Windows.Forms.TrackBar();
            this.tbBinaryTHL = new System.Windows.Forms.TextBox();
            this.gbFilterMask = new System.Windows.Forms.GroupBox();
            this.udFilterMask = new System.Windows.Forms.NumericUpDown();
            this.btnShowBinary = new System.Windows.Forms.Button();
            this.btnShowROI = new System.Windows.Forms.Button();
            this.tkbrROIBottom = new System.Windows.Forms.TrackBar();
            this.tkbrROITop = new System.Windows.Forms.TrackBar();
            this.tbROIBottom = new System.Windows.Forms.TextBox();
            this.tbROITop = new System.Windows.Forms.TextBox();
            this.lbROIBottom = new System.Windows.Forms.Label();
            this.lbROITop = new System.Windows.Forms.Label();
            this.btnLoadBuffer = new System.Windows.Forms.Button();
            this.btnContinusTest = new System.Windows.Forms.Button();
            this.btnOpenImageFolder = new System.Windows.Forms.Button();
            this.PreTestButton = new System.Windows.Forms.Button();
            this.btnTestInfoBackup = new System.Windows.Forms.Button();
            this.TestButton = new System.Windows.Forms.Button();
            this.btnCalculateResutl = new System.Windows.Forms.Button();
            this.NextTestButton = new System.Windows.Forms.Button();
            this.AdvancePage = new System.Windows.Forms.TabPage();
            this.gbAdvance = new System.Windows.Forms.GroupBox();
            this.gbAlarmStop = new System.Windows.Forms.GroupBox();
            this.tbNOffsetUpLimit = new System.Windows.Forms.TextBox();
            this.tbOOffsetUpLimit = new System.Windows.Forms.TextBox();
            this.cbAlarmStopDownloadData = new System.Windows.Forms.CheckBox();
            this.cbAlarmStopEnabled = new System.Windows.Forms.CheckBox();
            this.lbNOffsetUpLimit = new System.Windows.Forms.Label();
            this.lbOOffsetUpLimit = new System.Windows.Forms.Label();
            this.gbCalibrate = new System.Windows.Forms.GroupBox();
            this.tbParam196 = new System.Windows.Forms.TextBox();
            this.tbParam195 = new System.Windows.Forms.TextBox();
            this.tbParam194 = new System.Windows.Forms.TextBox();
            this.tbParam193 = new System.Windows.Forms.TextBox();
            this.lbParam196 = new System.Windows.Forms.Label();
            this.lbParam195 = new System.Windows.Forms.Label();
            this.lbParam194 = new System.Windows.Forms.Label();
            this.lbParam193 = new System.Windows.Forms.Label();
            this.Calibrate = new System.Windows.Forms.Button();
            this.btnSaveParas = new System.Windows.Forms.Button();
            this.gbDownloadData = new System.Windows.Forms.GroupBox();
            this.btnDownloadData = new System.Windows.Forms.Button();
            this.cbDownloadData = new System.Windows.Forms.CheckBox();
            this.gbAdvanceParas = new System.Windows.Forms.GroupBox();
            this.cbOffsetType = new System.Windows.Forms.CheckBox();
            this.cbCheckWaferPresentInAutoRun = new System.Windows.Forms.CheckBox();
            this.gbOutputFolder = new System.Windows.Forms.GroupBox();
            this.lbShowOutputFolder = new System.Windows.Forms.Label();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.CylinderGroupBox = new System.Windows.Forms.GroupBox();
            this.cbCylinderEnabled = new System.Windows.Forms.CheckBox();
            this.lbCylinderStatus = new System.Windows.Forms.Label();
            this.btnCylinderConnect = new System.Windows.Forms.Button();
            this.cmbCylinderBaudRate = new System.Windows.Forms.ComboBox();
            this.lbCylinderBaudRate = new System.Windows.Forms.Label();
            this.cmbCylinderPort = new System.Windows.Forms.ComboBox();
            this.lbCylinderPort = new System.Windows.Forms.Label();
            this.gbAligner = new System.Windows.Forms.GroupBox();
            this.cbAlignerEnabled = new System.Windows.Forms.CheckBox();
            this.lbAlignerStatus = new System.Windows.Forms.Label();
            this.btnAlignerConnect = new System.Windows.Forms.Button();
            this.cmbAlignerBaudRate = new System.Windows.Forms.ComboBox();
            this.lbAlignerBaudRate = new System.Windows.Forms.Label();
            this.cmbAlignerPort = new System.Windows.Forms.ComboBox();
            this.lbAlignerPort = new System.Windows.Forms.Label();
            this.gbCamera = new System.Windows.Forms.GroupBox();
            this.cbCameraEnabled = new System.Windows.Forms.CheckBox();
            this.lbCameraStatus = new System.Windows.Forms.Label();
            this.btnCameraConnect = new System.Windows.Forms.Button();
            this.cmbCameraBaudRate = new System.Windows.Forms.ComboBox();
            this.lbCameraBaudRate = new System.Windows.Forms.Label();
            this.cmbCameraPort = new System.Windows.Forms.ComboBox();
            this.lbCameraPort = new System.Windows.Forms.Label();
            this.TestTabControl = new System.Windows.Forms.TabPage();
            this.gbTest = new System.Windows.Forms.GroupBox();
            this.gbPresentTest = new System.Windows.Forms.GroupBox();
            this.lbEndMonitorTime = new System.Windows.Forms.Label();
            this.lbStartMonitorTime = new System.Windows.Forms.Label();
            this.lbEndMonitorTimeLabel = new System.Windows.Forms.Label();
            this.lbStartMonitorTimeLabel = new System.Windows.Forms.Label();
            this.lbPresentStatus = new System.Windows.Forms.Label();
            this.lbPresentMonitorTime = new System.Windows.Forms.Label();
            this.udPresentMonitorSec = new System.Windows.Forms.NumericUpDown();
            this.udPresentMonitorMin = new System.Windows.Forms.NumericUpDown();
            this.udPresentMonitorHour = new System.Windows.Forms.NumericUpDown();
            this.lbPresentMonitorSec = new System.Windows.Forms.Label();
            this.lbPresentMonitorMin = new System.Windows.Forms.Label();
            this.lbPresentMonitorHour = new System.Windows.Forms.Label();
            this.btnStartMonitorPresent = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BottomPanel.SuspendLayout();
            this.ShowResultGroupBox.SuspendLayout();
            this.NotchGroupBox.SuspendLayout();
            this.gbOffSetPos.SuspendLayout();
            this.TaskTimeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayImageBox)).BeginInit();
            this.DisplayImageBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilterImageBox)).BeginInit();
            this.RightPanel.SuspendLayout();
            this.LeftTabControl.SuspendLayout();
            this.GeneralPage.SuspendLayout();
            this.pnGeneral.SuspendLayout();
            this.WaferRadiusGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaferRadiusUpDown)).BeginInit();
            this.gbTestMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTestModeTOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTestModeYOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTestModeXOffset)).BeginInit();
            this.TestSpeedGroupBox.SuspendLayout();
            this.gbCylinderMotion.SuspendLayout();
            this.NotchAngleGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udNotchAngle)).BeginInit();
            this.WaferTypeGroupBox.SuspendLayout();
            this.AlignXOffsetGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAlignXOffset)).BeginInit();
            this.gbMachineType.SuspendLayout();
            this.ExportFolderGroupBox.SuspendLayout();
            this.TestCountGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TestCountUpDown)).BeginInit();
            this.gbSingleMotion.SuspendLayout();
            this.AOIPage.SuspendLayout();
            this.gbAOI.SuspendLayout();
            this.gbAOISetup.SuspendLayout();
            this.gbBinaryTHL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbrBinaryTHL)).BeginInit();
            this.gbFilterMask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFilterMask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkbrROIBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkbrROITop)).BeginInit();
            this.AdvancePage.SuspendLayout();
            this.gbAdvance.SuspendLayout();
            this.gbAlarmStop.SuspendLayout();
            this.gbCalibrate.SuspendLayout();
            this.gbDownloadData.SuspendLayout();
            this.gbAdvanceParas.SuspendLayout();
            this.gbOutputFolder.SuspendLayout();
            this.CylinderGroupBox.SuspendLayout();
            this.gbAligner.SuspendLayout();
            this.gbCamera.SuspendLayout();
            this.TestTabControl.SuspendLayout();
            this.gbTest.SuspendLayout();
            this.gbPresentTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPresentMonitorSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPresentMonitorMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPresentMonitorHour)).BeginInit();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.ShowResultGroupBox);
            this.BottomPanel.Controls.Add(this.NotchGroupBox);
            this.BottomPanel.Controls.Add(this.gbOffSetPos);
            this.BottomPanel.Controls.Add(this.TaskTimeGroupBox);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BottomPanel.Location = new System.Drawing.Point(0, 484);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(645, 244);
            this.BottomPanel.TabIndex = 4;
            // 
            // ShowResultGroupBox
            // 
            this.ShowResultGroupBox.Controls.Add(this.lbNDegoffset);
            this.ShowResultGroupBox.Controls.Add(this.lbNoffset);
            this.ShowResultGroupBox.Controls.Add(this.lbToffset);
            this.ShowResultGroupBox.Controls.Add(this.ShowNDegoffsetLabel);
            this.ShowResultGroupBox.Controls.Add(this.ShowNoffsetLabel);
            this.ShowResultGroupBox.Controls.Add(this.ShowToffsetLabel);
            this.ShowResultGroupBox.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ShowResultGroupBox.Location = new System.Drawing.Point(12, 138);
            this.ShowResultGroupBox.Name = "ShowResultGroupBox";
            this.ShowResultGroupBox.Size = new System.Drawing.Size(541, 90);
            this.ShowResultGroupBox.TabIndex = 4;
            this.ShowResultGroupBox.TabStop = false;
            this.ShowResultGroupBox.Text = "Result";
            // 
            // lbNDegoffset
            // 
            this.lbNDegoffset.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbNDegoffset.Location = new System.Drawing.Point(360, 59);
            this.lbNDegoffset.Name = "lbNDegoffset";
            this.lbNDegoffset.Size = new System.Drawing.Size(126, 19);
            this.lbNDegoffset.TabIndex = 6;
            this.lbNDegoffset.Text = "0.0000";
            this.lbNDegoffset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbNoffset
            // 
            this.lbNoffset.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbNoffset.Location = new System.Drawing.Point(199, 60);
            this.lbNoffset.Name = "lbNoffset";
            this.lbNoffset.Size = new System.Drawing.Size(126, 19);
            this.lbNoffset.TabIndex = 5;
            this.lbNoffset.Text = "0.0000";
            this.lbNoffset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbNoffset.Visible = false;
            // 
            // lbToffset
            // 
            this.lbToffset.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbToffset.Location = new System.Drawing.Point(33, 59);
            this.lbToffset.Name = "lbToffset";
            this.lbToffset.Size = new System.Drawing.Size(126, 19);
            this.lbToffset.TabIndex = 4;
            this.lbToffset.Text = "0.0000";
            this.lbToffset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ShowNDegoffsetLabel
            // 
            this.ShowNDegoffsetLabel.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ShowNDegoffsetLabel.Location = new System.Drawing.Point(334, 32);
            this.ShowNDegoffsetLabel.Name = "ShowNDegoffsetLabel";
            this.ShowNDegoffsetLabel.Size = new System.Drawing.Size(126, 26);
            this.ShowNDegoffsetLabel.TabIndex = 3;
            this.ShowNDegoffsetLabel.Text = "N Offset(deg):";
            // 
            // ShowNoffsetLabel
            // 
            this.ShowNoffsetLabel.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ShowNoffsetLabel.Location = new System.Drawing.Point(168, 32);
            this.ShowNoffsetLabel.Name = "ShowNoffsetLabel";
            this.ShowNoffsetLabel.Size = new System.Drawing.Size(126, 26);
            this.ShowNoffsetLabel.TabIndex = 2;
            this.ShowNoffsetLabel.Text = "N Offset(mm):";
            this.ShowNoffsetLabel.Visible = false;
            // 
            // ShowToffsetLabel
            // 
            this.ShowToffsetLabel.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ShowToffsetLabel.Location = new System.Drawing.Point(6, 32);
            this.ShowToffsetLabel.Name = "ShowToffsetLabel";
            this.ShowToffsetLabel.Size = new System.Drawing.Size(126, 19);
            this.ShowToffsetLabel.TabIndex = 1;
            this.ShowToffsetLabel.Text = "O Offset(mm):";
            // 
            // NotchGroupBox
            // 
            this.NotchGroupBox.Controls.Add(this.lbAvgNotchPosY);
            this.NotchGroupBox.Controls.Add(this.lbAvgNotchPosX);
            this.NotchGroupBox.Controls.Add(this.lbNotchPosY);
            this.NotchGroupBox.Controls.Add(this.lbNotchPosX);
            this.NotchGroupBox.Controls.Add(this.ShowAvgNotchPosYLabel);
            this.NotchGroupBox.Controls.Add(this.ShowAvgNotchPosXLabel);
            this.NotchGroupBox.Controls.Add(this.ShowNotchPosYLabel);
            this.NotchGroupBox.Controls.Add(this.ShowNotchPosXLabel);
            this.NotchGroupBox.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.NotchGroupBox.Location = new System.Drawing.Point(215, 3);
            this.NotchGroupBox.Name = "NotchGroupBox";
            this.NotchGroupBox.Size = new System.Drawing.Size(213, 127);
            this.NotchGroupBox.TabIndex = 3;
            this.NotchGroupBox.TabStop = false;
            this.NotchGroupBox.Text = "Notch Position";
            // 
            // lbAvgNotchPosY
            // 
            this.lbAvgNotchPosY.Location = new System.Drawing.Point(128, 95);
            this.lbAvgNotchPosY.Name = "lbAvgNotchPosY";
            this.lbAvgNotchPosY.Size = new System.Drawing.Size(79, 19);
            this.lbAvgNotchPosY.TabIndex = 8;
            this.lbAvgNotchPosY.Text = "0.00";
            this.lbAvgNotchPosY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbAvgNotchPosX
            // 
            this.lbAvgNotchPosX.Location = new System.Drawing.Point(128, 71);
            this.lbAvgNotchPosX.Name = "lbAvgNotchPosX";
            this.lbAvgNotchPosX.Size = new System.Drawing.Size(79, 19);
            this.lbAvgNotchPosX.TabIndex = 7;
            this.lbAvgNotchPosX.Text = "0.00";
            this.lbAvgNotchPosX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbNotchPosY
            // 
            this.lbNotchPosY.Location = new System.Drawing.Point(124, 47);
            this.lbNotchPosY.Name = "lbNotchPosY";
            this.lbNotchPosY.Size = new System.Drawing.Size(83, 19);
            this.lbNotchPosY.TabIndex = 6;
            this.lbNotchPosY.Text = "0.00";
            this.lbNotchPosY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbNotchPosX
            // 
            this.lbNotchPosX.Location = new System.Drawing.Point(124, 23);
            this.lbNotchPosX.Name = "lbNotchPosX";
            this.lbNotchPosX.Size = new System.Drawing.Size(83, 19);
            this.lbNotchPosX.TabIndex = 5;
            this.lbNotchPosX.Text = "0.00";
            this.lbNotchPosX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ShowAvgNotchPosYLabel
            // 
            this.ShowAvgNotchPosYLabel.Location = new System.Drawing.Point(6, 95);
            this.ShowAvgNotchPosYLabel.Name = "ShowAvgNotchPosYLabel";
            this.ShowAvgNotchPosYLabel.Size = new System.Drawing.Size(97, 19);
            this.ShowAvgNotchPosYLabel.TabIndex = 3;
            this.ShowAvgNotchPosYLabel.Text = "Avg Y(mm):";
            // 
            // ShowAvgNotchPosXLabel
            // 
            this.ShowAvgNotchPosXLabel.Location = new System.Drawing.Point(6, 71);
            this.ShowAvgNotchPosXLabel.Name = "ShowAvgNotchPosXLabel";
            this.ShowAvgNotchPosXLabel.Size = new System.Drawing.Size(97, 19);
            this.ShowAvgNotchPosXLabel.TabIndex = 2;
            this.ShowAvgNotchPosXLabel.Text = "Avg X(mm):";
            // 
            // ShowNotchPosYLabel
            // 
            this.ShowNotchPosYLabel.Location = new System.Drawing.Point(6, 47);
            this.ShowNotchPosYLabel.Name = "ShowNotchPosYLabel";
            this.ShowNotchPosYLabel.Size = new System.Drawing.Size(72, 19);
            this.ShowNotchPosYLabel.TabIndex = 1;
            this.ShowNotchPosYLabel.Text = "Y(mm):";
            // 
            // ShowNotchPosXLabel
            // 
            this.ShowNotchPosXLabel.Location = new System.Drawing.Point(6, 23);
            this.ShowNotchPosXLabel.Name = "ShowNotchPosXLabel";
            this.ShowNotchPosXLabel.Size = new System.Drawing.Size(72, 19);
            this.ShowNotchPosXLabel.TabIndex = 0;
            this.ShowNotchPosXLabel.Text = "X(mm):";
            // 
            // gbOffSetPos
            // 
            this.gbOffSetPos.Controls.Add(this.lbAvgTopPosY);
            this.gbOffSetPos.Controls.Add(this.lbAvgTopPosX);
            this.gbOffSetPos.Controls.Add(this.lbTopPosY);
            this.gbOffSetPos.Controls.Add(this.lbTopPosX);
            this.gbOffSetPos.Controls.Add(this.ShowAvgCenterPosYLabel);
            this.gbOffSetPos.Controls.Add(this.ShowAvgCenterPosXLabel);
            this.gbOffSetPos.Controls.Add(this.ShowCenterPosYLabel);
            this.gbOffSetPos.Controls.Add(this.ShowCenterPosXLabel);
            this.gbOffSetPos.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gbOffSetPos.Location = new System.Drawing.Point(12, 3);
            this.gbOffSetPos.Name = "gbOffSetPos";
            this.gbOffSetPos.Size = new System.Drawing.Size(197, 127);
            this.gbOffSetPos.TabIndex = 2;
            this.gbOffSetPos.TabStop = false;
            this.gbOffSetPos.Text = "Offset Position";
            // 
            // lbAvgTopPosY
            // 
            this.lbAvgTopPosY.Location = new System.Drawing.Point(98, 95);
            this.lbAvgTopPosY.Name = "lbAvgTopPosY";
            this.lbAvgTopPosY.Size = new System.Drawing.Size(93, 19);
            this.lbAvgTopPosY.TabIndex = 7;
            this.lbAvgTopPosY.Text = "0.00";
            this.lbAvgTopPosY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbAvgTopPosX
            // 
            this.lbAvgTopPosX.Location = new System.Drawing.Point(98, 71);
            this.lbAvgTopPosX.Name = "lbAvgTopPosX";
            this.lbAvgTopPosX.Size = new System.Drawing.Size(93, 19);
            this.lbAvgTopPosX.TabIndex = 6;
            this.lbAvgTopPosX.Text = "0.00";
            this.lbAvgTopPosX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTopPosY
            // 
            this.lbTopPosY.Location = new System.Drawing.Point(74, 47);
            this.lbTopPosY.Name = "lbTopPosY";
            this.lbTopPosY.Size = new System.Drawing.Size(117, 19);
            this.lbTopPosY.TabIndex = 5;
            this.lbTopPosY.Text = "0.00";
            this.lbTopPosY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTopPosX
            // 
            this.lbTopPosX.Location = new System.Drawing.Point(74, 23);
            this.lbTopPosX.Name = "lbTopPosX";
            this.lbTopPosX.Size = new System.Drawing.Size(117, 19);
            this.lbTopPosX.TabIndex = 4;
            this.lbTopPosX.Text = "0.00";
            this.lbTopPosX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ShowAvgCenterPosYLabel
            // 
            this.ShowAvgCenterPosYLabel.Location = new System.Drawing.Point(6, 95);
            this.ShowAvgCenterPosYLabel.Name = "ShowAvgCenterPosYLabel";
            this.ShowAvgCenterPosYLabel.Size = new System.Drawing.Size(97, 19);
            this.ShowAvgCenterPosYLabel.TabIndex = 3;
            this.ShowAvgCenterPosYLabel.Text = "Avg Y(mm):";
            // 
            // ShowAvgCenterPosXLabel
            // 
            this.ShowAvgCenterPosXLabel.Location = new System.Drawing.Point(6, 71);
            this.ShowAvgCenterPosXLabel.Name = "ShowAvgCenterPosXLabel";
            this.ShowAvgCenterPosXLabel.Size = new System.Drawing.Size(97, 19);
            this.ShowAvgCenterPosXLabel.TabIndex = 2;
            this.ShowAvgCenterPosXLabel.Text = "Avg X(mm):";
            // 
            // ShowCenterPosYLabel
            // 
            this.ShowCenterPosYLabel.Location = new System.Drawing.Point(6, 47);
            this.ShowCenterPosYLabel.Name = "ShowCenterPosYLabel";
            this.ShowCenterPosYLabel.Size = new System.Drawing.Size(72, 19);
            this.ShowCenterPosYLabel.TabIndex = 1;
            this.ShowCenterPosYLabel.Text = "Y(mm):";
            // 
            // ShowCenterPosXLabel
            // 
            this.ShowCenterPosXLabel.Location = new System.Drawing.Point(6, 23);
            this.ShowCenterPosXLabel.Name = "ShowCenterPosXLabel";
            this.ShowCenterPosXLabel.Size = new System.Drawing.Size(72, 19);
            this.ShowCenterPosXLabel.TabIndex = 0;
            this.ShowCenterPosXLabel.Text = "X(mm):";
            // 
            // TaskTimeGroupBox
            // 
            this.TaskTimeGroupBox.Controls.Add(this.lbMinTaskTime);
            this.TaskTimeGroupBox.Controls.Add(this.lbMaxTaskTime);
            this.TaskTimeGroupBox.Controls.Add(this.lbAvgTaskTime);
            this.TaskTimeGroupBox.Controls.Add(this.lbTaskTime);
            this.TaskTimeGroupBox.Controls.Add(this.ShowMinTaskTimeLabel);
            this.TaskTimeGroupBox.Controls.Add(this.ShowMaxTaskTimeLabel);
            this.TaskTimeGroupBox.Controls.Add(this.ShowAvgTaskTimeLabel);
            this.TaskTimeGroupBox.Controls.Add(this.ShowTaskTimeLabel);
            this.TaskTimeGroupBox.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TaskTimeGroupBox.Location = new System.Drawing.Point(434, 3);
            this.TaskTimeGroupBox.Name = "TaskTimeGroupBox";
            this.TaskTimeGroupBox.Size = new System.Drawing.Size(203, 127);
            this.TaskTimeGroupBox.TabIndex = 0;
            this.TaskTimeGroupBox.TabStop = false;
            this.TaskTimeGroupBox.Text = "Task Time";
            // 
            // lbMinTaskTime
            // 
            this.lbMinTaskTime.Location = new System.Drawing.Point(114, 95);
            this.lbMinTaskTime.Name = "lbMinTaskTime";
            this.lbMinTaskTime.Size = new System.Drawing.Size(83, 19);
            this.lbMinTaskTime.TabIndex = 9;
            this.lbMinTaskTime.Text = "0.00";
            this.lbMinTaskTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbMaxTaskTime
            // 
            this.lbMaxTaskTime.Location = new System.Drawing.Point(114, 71);
            this.lbMaxTaskTime.Name = "lbMaxTaskTime";
            this.lbMaxTaskTime.Size = new System.Drawing.Size(83, 19);
            this.lbMaxTaskTime.TabIndex = 8;
            this.lbMaxTaskTime.Text = "0.00";
            this.lbMaxTaskTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbAvgTaskTime
            // 
            this.lbAvgTaskTime.Location = new System.Drawing.Point(114, 47);
            this.lbAvgTaskTime.Name = "lbAvgTaskTime";
            this.lbAvgTaskTime.Size = new System.Drawing.Size(83, 19);
            this.lbAvgTaskTime.TabIndex = 7;
            this.lbAvgTaskTime.Text = "0.00";
            this.lbAvgTaskTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTaskTime
            // 
            this.lbTaskTime.Location = new System.Drawing.Point(114, 23);
            this.lbTaskTime.Name = "lbTaskTime";
            this.lbTaskTime.Size = new System.Drawing.Size(83, 19);
            this.lbTaskTime.TabIndex = 6;
            this.lbTaskTime.Text = "0.00";
            this.lbTaskTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ShowMinTaskTimeLabel
            // 
            this.ShowMinTaskTimeLabel.Location = new System.Drawing.Point(6, 95);
            this.ShowMinTaskTimeLabel.Name = "ShowMinTaskTimeLabel";
            this.ShowMinTaskTimeLabel.Size = new System.Drawing.Size(80, 19);
            this.ShowMinTaskTimeLabel.TabIndex = 3;
            this.ShowMinTaskTimeLabel.Text = "Min T(s):";
            // 
            // ShowMaxTaskTimeLabel
            // 
            this.ShowMaxTaskTimeLabel.Location = new System.Drawing.Point(6, 71);
            this.ShowMaxTaskTimeLabel.Name = "ShowMaxTaskTimeLabel";
            this.ShowMaxTaskTimeLabel.Size = new System.Drawing.Size(80, 19);
            this.ShowMaxTaskTimeLabel.TabIndex = 2;
            this.ShowMaxTaskTimeLabel.Text = "Max T(s):";
            // 
            // ShowAvgTaskTimeLabel
            // 
            this.ShowAvgTaskTimeLabel.Location = new System.Drawing.Point(6, 47);
            this.ShowAvgTaskTimeLabel.Name = "ShowAvgTaskTimeLabel";
            this.ShowAvgTaskTimeLabel.Size = new System.Drawing.Size(80, 19);
            this.ShowAvgTaskTimeLabel.TabIndex = 1;
            this.ShowAvgTaskTimeLabel.Text = "Avg T(s):";
            // 
            // ShowTaskTimeLabel
            // 
            this.ShowTaskTimeLabel.Location = new System.Drawing.Point(6, 23);
            this.ShowTaskTimeLabel.Name = "ShowTaskTimeLabel";
            this.ShowTaskTimeLabel.Size = new System.Drawing.Size(72, 19);
            this.ShowTaskTimeLabel.TabIndex = 0;
            this.ShowTaskTimeLabel.Text = "T(s):";
            this.ShowTaskTimeLabel.Click += new System.EventHandler(this.ShowTaskTimeLabel_Click);
            // 
            // DisplayImageBox
            // 
            this.DisplayImageBox.BackColor = System.Drawing.Color.Transparent;
            this.DisplayImageBox.Controls.Add(this.FilterImageBox);
            this.DisplayImageBox.Controls.Add(this.lbShowCurrentCnt);
            this.DisplayImageBox.Location = new System.Drawing.Point(3, 3);
            this.DisplayImageBox.Name = "DisplayImageBox";
            this.DisplayImageBox.Size = new System.Drawing.Size(640, 480);
            this.DisplayImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DisplayImageBox.TabIndex = 2;
            this.DisplayImageBox.TabStop = false;
            this.DisplayImageBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DisplayImageBox_MouseMove);
            // 
            // FilterImageBox
            // 
            this.FilterImageBox.BackColor = System.Drawing.Color.Transparent;
            this.FilterImageBox.Location = new System.Drawing.Point(117, 137);
            this.FilterImageBox.Name = "FilterImageBox";
            this.FilterImageBox.Size = new System.Drawing.Size(274, 223);
            this.FilterImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FilterImageBox.TabIndex = 5;
            this.FilterImageBox.TabStop = false;
            this.FilterImageBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FilterImageBox_MouseMove);
            // 
            // lbShowCurrentCnt
            // 
            this.lbShowCurrentCnt.AutoSize = true;
            this.lbShowCurrentCnt.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbShowCurrentCnt.ForeColor = System.Drawing.Color.Red;
            this.lbShowCurrentCnt.Location = new System.Drawing.Point(30, 30);
            this.lbShowCurrentCnt.Name = "lbShowCurrentCnt";
            this.lbShowCurrentCnt.Size = new System.Drawing.Size(0, 30);
            this.lbShowCurrentCnt.TabIndex = 5;
            // 
            // RichTextBox1
            // 
            this.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.RichTextBox1.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RichTextBox1.Location = new System.Drawing.Point(0, 0);
            this.RichTextBox1.Name = "RichTextBox1";
            this.RichTextBox1.Size = new System.Drawing.Size(324, 728);
            this.RichTextBox1.TabIndex = 0;
            this.RichTextBox1.Text = "";
            this.RichTextBox1.TextChanged += new System.EventHandler(this.RichTextBox_TextChanged);
            // 
            // RightPanel
            // 
            this.RightPanel.Controls.Add(this.LeftTabControl);
            this.RightPanel.Controls.Add(this.RichTextBox1);
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Location = new System.Drawing.Point(645, 0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(683, 728);
            this.RightPanel.TabIndex = 5;
            // 
            // LeftTabControl
            // 
            this.LeftTabControl.Controls.Add(this.GeneralPage);
            this.LeftTabControl.Controls.Add(this.AOIPage);
            this.LeftTabControl.Controls.Add(this.AdvancePage);
            this.LeftTabControl.Controls.Add(this.TestTabControl);
            this.LeftTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftTabControl.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LeftTabControl.Location = new System.Drawing.Point(324, 0);
            this.LeftTabControl.Name = "LeftTabControl";
            this.LeftTabControl.SelectedIndex = 0;
            this.LeftTabControl.Size = new System.Drawing.Size(359, 728);
            this.LeftTabControl.TabIndex = 4;
            // 
            // GeneralPage
            // 
            this.GeneralPage.Controls.Add(this.pnGeneral);
            this.GeneralPage.Controls.Add(this.TestCountGroupBox);
            this.GeneralPage.Controls.Add(this.gbSingleMotion);
            this.GeneralPage.Controls.Add(this.RunButton);
            this.GeneralPage.Location = new System.Drawing.Point(4, 25);
            this.GeneralPage.Name = "GeneralPage";
            this.GeneralPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralPage.Size = new System.Drawing.Size(351, 699);
            this.GeneralPage.TabIndex = 0;
            this.GeneralPage.Text = "基本設定";
            this.GeneralPage.UseVisualStyleBackColor = true;
            // 
            // pnGeneral
            // 
            this.pnGeneral.Controls.Add(this.WaferRadiusGroupBox);
            this.pnGeneral.Controls.Add(this.gbTestMode);
            this.pnGeneral.Controls.Add(this.TestSpeedGroupBox);
            this.pnGeneral.Controls.Add(this.gbCylinderMotion);
            this.pnGeneral.Controls.Add(this.NotchAngleGroupBox);
            this.pnGeneral.Controls.Add(this.WaferTypeGroupBox);
            this.pnGeneral.Controls.Add(this.AlignXOffsetGroupBox);
            this.pnGeneral.Controls.Add(this.gbMachineType);
            this.pnGeneral.Controls.Add(this.ExportFolderGroupBox);
            this.pnGeneral.Location = new System.Drawing.Point(3, 3);
            this.pnGeneral.Name = "pnGeneral";
            this.pnGeneral.Size = new System.Drawing.Size(345, 503);
            this.pnGeneral.TabIndex = 17;
            // 
            // WaferRadiusGroupBox
            // 
            this.WaferRadiusGroupBox.Controls.Add(this.WaferRadiusUpDown);
            this.WaferRadiusGroupBox.Location = new System.Drawing.Point(3, 3);
            this.WaferRadiusGroupBox.Name = "WaferRadiusGroupBox";
            this.WaferRadiusGroupBox.Size = new System.Drawing.Size(164, 53);
            this.WaferRadiusGroupBox.TabIndex = 7;
            this.WaferRadiusGroupBox.TabStop = false;
            this.WaferRadiusGroupBox.Text = "Wafer 半徑(um)";
            // 
            // WaferRadiusUpDown
            // 
            this.WaferRadiusUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.WaferRadiusUpDown.Location = new System.Drawing.Point(61, 22);
            this.WaferRadiusUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.WaferRadiusUpDown.Name = "WaferRadiusUpDown";
            this.WaferRadiusUpDown.Size = new System.Drawing.Size(97, 23);
            this.WaferRadiusUpDown.TabIndex = 1;
            this.WaferRadiusUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.WaferRadiusUpDown.Value = new decimal(new int[] {
            150000,
            0,
            0,
            0});
            this.WaferRadiusUpDown.ValueChanged += new System.EventHandler(this.WaferRadiusUpDown_ValueChanged);
            // 
            // gbTestMode
            // 
            this.gbTestMode.Controls.Add(this.udTestModeTOffset);
            this.gbTestMode.Controls.Add(this.udTestModeYOffset);
            this.gbTestMode.Controls.Add(this.udTestModeXOffset);
            this.gbTestMode.Controls.Add(this.lbTestModeTOffset);
            this.gbTestMode.Controls.Add(this.lbTestModeYOffset);
            this.gbTestMode.Controls.Add(this.lbTestModeXOffset);
            this.gbTestMode.Controls.Add(this.cmbTestMode);
            this.gbTestMode.Location = new System.Drawing.Point(3, 362);
            this.gbTestMode.Name = "gbTestMode";
            this.gbTestMode.Size = new System.Drawing.Size(336, 139);
            this.gbTestMode.TabIndex = 16;
            this.gbTestMode.TabStop = false;
            this.gbTestMode.Text = "測試模式";
            // 
            // udTestModeTOffset
            // 
            this.udTestModeTOffset.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udTestModeTOffset.Location = new System.Drawing.Point(178, 108);
            this.udTestModeTOffset.Maximum = new decimal(new int[] {
            360000,
            0,
            0,
            0});
            this.udTestModeTOffset.Minimum = new decimal(new int[] {
            360000,
            0,
            0,
            -2147483648});
            this.udTestModeTOffset.Name = "udTestModeTOffset";
            this.udTestModeTOffset.Size = new System.Drawing.Size(85, 23);
            this.udTestModeTOffset.TabIndex = 6;
            this.udTestModeTOffset.Value = new decimal(new int[] {
            21000,
            0,
            0,
            -2147483648});
            this.udTestModeTOffset.ValueChanged += new System.EventHandler(this.udTestModeTOffset_ValueChanged);
            // 
            // udTestModeYOffset
            // 
            this.udTestModeYOffset.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.udTestModeYOffset.Location = new System.Drawing.Point(178, 79);
            this.udTestModeYOffset.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.udTestModeYOffset.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.udTestModeYOffset.Name = "udTestModeYOffset";
            this.udTestModeYOffset.Size = new System.Drawing.Size(85, 23);
            this.udTestModeYOffset.TabIndex = 5;
            this.udTestModeYOffset.Value = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.udTestModeYOffset.ValueChanged += new System.EventHandler(this.udTestModeYOffset_ValueChanged);
            // 
            // udTestModeXOffset
            // 
            this.udTestModeXOffset.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.udTestModeXOffset.Location = new System.Drawing.Point(178, 50);
            this.udTestModeXOffset.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.udTestModeXOffset.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.udTestModeXOffset.Name = "udTestModeXOffset";
            this.udTestModeXOffset.Size = new System.Drawing.Size(85, 23);
            this.udTestModeXOffset.TabIndex = 4;
            this.udTestModeXOffset.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.udTestModeXOffset.ValueChanged += new System.EventHandler(this.udTestModeXOffset_ValueChanged);
            // 
            // lbTestModeTOffset
            // 
            this.lbTestModeTOffset.Location = new System.Drawing.Point(9, 108);
            this.lbTestModeTOffset.Name = "lbTestModeTOffset";
            this.lbTestModeTOffset.Size = new System.Drawing.Size(163, 21);
            this.lbTestModeTOffset.TabIndex = 3;
            this.lbTestModeTOffset.Text = "T Offset(mdeg)";
            // 
            // lbTestModeYOffset
            // 
            this.lbTestModeYOffset.Location = new System.Drawing.Point(9, 80);
            this.lbTestModeYOffset.Name = "lbTestModeYOffset";
            this.lbTestModeYOffset.Size = new System.Drawing.Size(163, 21);
            this.lbTestModeYOffset.TabIndex = 2;
            this.lbTestModeYOffset.Text = "Y Offset(um)";
            // 
            // lbTestModeXOffset
            // 
            this.lbTestModeXOffset.Location = new System.Drawing.Point(9, 52);
            this.lbTestModeXOffset.Name = "lbTestModeXOffset";
            this.lbTestModeXOffset.Size = new System.Drawing.Size(163, 21);
            this.lbTestModeXOffset.TabIndex = 1;
            this.lbTestModeXOffset.Text = "X Offset(um)";
            // 
            // cmbTestMode
            // 
            this.cmbTestMode.FormattingEnabled = true;
            this.cmbTestMode.Items.AddRange(new object[] {
            "Fix",
            "Step(Center)",
            "Step(Notch)"});
            this.cmbTestMode.Location = new System.Drawing.Point(9, 22);
            this.cmbTestMode.Name = "cmbTestMode";
            this.cmbTestMode.Size = new System.Drawing.Size(121, 24);
            this.cmbTestMode.TabIndex = 0;
            this.cmbTestMode.Text = "Step(Notch)";
            this.cmbTestMode.SelectedIndexChanged += new System.EventHandler(this.cmbTestMode_SelectedIndexChanged);
            // 
            // TestSpeedGroupBox
            // 
            this.TestSpeedGroupBox.Controls.Add(this.cmbTestSpeed);
            this.TestSpeedGroupBox.Location = new System.Drawing.Point(173, 3);
            this.TestSpeedGroupBox.Name = "TestSpeedGroupBox";
            this.TestSpeedGroupBox.Size = new System.Drawing.Size(164, 53);
            this.TestSpeedGroupBox.TabIndex = 8;
            this.TestSpeedGroupBox.TabStop = false;
            this.TestSpeedGroupBox.Text = "測試速度(%)";
            // 
            // cmbTestSpeed
            // 
            this.cmbTestSpeed.FormattingEnabled = true;
            this.cmbTestSpeed.Items.AddRange(new object[] {
            "100",
            "90",
            "80",
            "70",
            "60",
            "50",
            "40",
            "30",
            "20",
            "10"});
            this.cmbTestSpeed.Location = new System.Drawing.Point(76, 21);
            this.cmbTestSpeed.Name = "cmbTestSpeed";
            this.cmbTestSpeed.Size = new System.Drawing.Size(82, 24);
            this.cmbTestSpeed.TabIndex = 0;
            this.cmbTestSpeed.Text = "100";
            this.cmbTestSpeed.SelectedIndexChanged += new System.EventHandler(this.cmbTestSpeed_SelectedIndexChanged);
            // 
            // gbCylinderMotion
            // 
            this.gbCylinderMotion.Controls.Add(this.btnCylinderMoveUp);
            this.gbCylinderMotion.Controls.Add(this.btnCylinderMoveDown);
            this.gbCylinderMotion.Controls.Add(this.btnCylinderRelease);
            this.gbCylinderMotion.Controls.Add(this.btnCylinderHold);
            this.gbCylinderMotion.Location = new System.Drawing.Point(3, 229);
            this.gbCylinderMotion.Name = "gbCylinderMotion";
            this.gbCylinderMotion.Size = new System.Drawing.Size(336, 132);
            this.gbCylinderMotion.TabIndex = 15;
            this.gbCylinderMotion.TabStop = false;
            this.gbCylinderMotion.Text = "汽缸動作";
            // 
            // btnCylinderMoveUp
            // 
            this.btnCylinderMoveUp.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCylinderMoveUp.Location = new System.Drawing.Point(115, 78);
            this.btnCylinderMoveUp.Name = "btnCylinderMoveUp";
            this.btnCylinderMoveUp.Size = new System.Drawing.Size(100, 50);
            this.btnCylinderMoveUp.TabIndex = 19;
            this.btnCylinderMoveUp.Text = "Move Up";
            this.btnCylinderMoveUp.UseVisualStyleBackColor = true;
            this.btnCylinderMoveUp.Click += new System.EventHandler(this.btnCylinderMoveUp_Click);
            // 
            // btnCylinderMoveDown
            // 
            this.btnCylinderMoveDown.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCylinderMoveDown.Location = new System.Drawing.Point(9, 78);
            this.btnCylinderMoveDown.Name = "btnCylinderMoveDown";
            this.btnCylinderMoveDown.Size = new System.Drawing.Size(100, 50);
            this.btnCylinderMoveDown.TabIndex = 18;
            this.btnCylinderMoveDown.Text = "Move Down";
            this.btnCylinderMoveDown.UseVisualStyleBackColor = true;
            this.btnCylinderMoveDown.Click += new System.EventHandler(this.btnCylinderMoveDown_Click);
            // 
            // btnCylinderRelease
            // 
            this.btnCylinderRelease.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCylinderRelease.Location = new System.Drawing.Point(115, 22);
            this.btnCylinderRelease.Name = "btnCylinderRelease";
            this.btnCylinderRelease.Size = new System.Drawing.Size(100, 50);
            this.btnCylinderRelease.TabIndex = 17;
            this.btnCylinderRelease.Text = "Release";
            this.btnCylinderRelease.UseVisualStyleBackColor = true;
            this.btnCylinderRelease.Click += new System.EventHandler(this.btnCylinderRelease_Click);
            // 
            // btnCylinderHold
            // 
            this.btnCylinderHold.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCylinderHold.Location = new System.Drawing.Point(9, 22);
            this.btnCylinderHold.Name = "btnCylinderHold";
            this.btnCylinderHold.Size = new System.Drawing.Size(100, 50);
            this.btnCylinderHold.TabIndex = 16;
            this.btnCylinderHold.Text = "Hold";
            this.btnCylinderHold.UseVisualStyleBackColor = true;
            this.btnCylinderHold.Click += new System.EventHandler(this.btnCylinderHold_Click);
            // 
            // NotchAngleGroupBox
            // 
            this.NotchAngleGroupBox.Controls.Add(this.udNotchAngle);
            this.NotchAngleGroupBox.Location = new System.Drawing.Point(3, 59);
            this.NotchAngleGroupBox.Name = "NotchAngleGroupBox";
            this.NotchAngleGroupBox.Size = new System.Drawing.Size(164, 53);
            this.NotchAngleGroupBox.TabIndex = 9;
            this.NotchAngleGroupBox.TabStop = false;
            this.NotchAngleGroupBox.Text = "Align 後旋轉角度(mdeg)";
            // 
            // udNotchAngle
            // 
            this.udNotchAngle.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udNotchAngle.Location = new System.Drawing.Point(61, 22);
            this.udNotchAngle.Maximum = new decimal(new int[] {
            360000,
            0,
            0,
            0});
            this.udNotchAngle.Minimum = new decimal(new int[] {
            360000,
            0,
            0,
            -2147483648});
            this.udNotchAngle.Name = "udNotchAngle";
            this.udNotchAngle.Size = new System.Drawing.Size(97, 23);
            this.udNotchAngle.TabIndex = 10;
            this.udNotchAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udNotchAngle.Value = new decimal(new int[] {
            90000,
            0,
            0,
            0});
            this.udNotchAngle.ValueChanged += new System.EventHandler(this.udNotchAngle_ValueChanged);
            // 
            // WaferTypeGroupBox
            // 
            this.WaferTypeGroupBox.Controls.Add(this.cmbWaferType);
            this.WaferTypeGroupBox.Location = new System.Drawing.Point(175, 177);
            this.WaferTypeGroupBox.Name = "WaferTypeGroupBox";
            this.WaferTypeGroupBox.Size = new System.Drawing.Size(164, 53);
            this.WaferTypeGroupBox.TabIndex = 14;
            this.WaferTypeGroupBox.TabStop = false;
            this.WaferTypeGroupBox.Text = "Wafer 形式";
            // 
            // cmbWaferType
            // 
            this.cmbWaferType.FormattingEnabled = true;
            this.cmbWaferType.Items.AddRange(new object[] {
            "Notch Type",
            "Flat Type",
            "Circle Type"});
            this.cmbWaferType.Location = new System.Drawing.Point(6, 22);
            this.cmbWaferType.Name = "cmbWaferType";
            this.cmbWaferType.Size = new System.Drawing.Size(152, 24);
            this.cmbWaferType.TabIndex = 0;
            this.cmbWaferType.Text = "Notch Type";
            this.cmbWaferType.SelectedIndexChanged += new System.EventHandler(this.cmbWaferType_SelectedIndexChanged);
            // 
            // AlignXOffsetGroupBox
            // 
            this.AlignXOffsetGroupBox.Controls.Add(this.udAlignXOffset);
            this.AlignXOffsetGroupBox.Location = new System.Drawing.Point(173, 59);
            this.AlignXOffsetGroupBox.Name = "AlignXOffsetGroupBox";
            this.AlignXOffsetGroupBox.Size = new System.Drawing.Size(164, 53);
            this.AlignXOffsetGroupBox.TabIndex = 12;
            this.AlignXOffsetGroupBox.TabStop = false;
            this.AlignXOffsetGroupBox.Text = "Align後X方向位移(um)";
            // 
            // udAlignXOffset
            // 
            this.udAlignXOffset.Location = new System.Drawing.Point(61, 22);
            this.udAlignXOffset.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.udAlignXOffset.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.udAlignXOffset.Name = "udAlignXOffset";
            this.udAlignXOffset.Size = new System.Drawing.Size(97, 23);
            this.udAlignXOffset.TabIndex = 11;
            this.udAlignXOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udAlignXOffset.ValueChanged += new System.EventHandler(this.udAlignXOffset_ValueChanged);
            // 
            // gbMachineType
            // 
            this.gbMachineType.Controls.Add(this.cmbNotchType);
            this.gbMachineType.Location = new System.Drawing.Point(3, 177);
            this.gbMachineType.Name = "gbMachineType";
            this.gbMachineType.Size = new System.Drawing.Size(164, 53);
            this.gbMachineType.TabIndex = 13;
            this.gbMachineType.TabStop = false;
            this.gbMachineType.Text = "Align 形式";
            // 
            // cmbNotchType
            // 
            this.cmbNotchType.FormattingEnabled = true;
            this.cmbNotchType.Items.AddRange(new object[] {
            "Vacuum Type",
            "Clamp Type"});
            this.cmbNotchType.Location = new System.Drawing.Point(6, 22);
            this.cmbNotchType.Name = "cmbNotchType";
            this.cmbNotchType.Size = new System.Drawing.Size(152, 24);
            this.cmbNotchType.TabIndex = 0;
            this.cmbNotchType.Text = "Vacuum Type";
            this.cmbNotchType.SelectedIndexChanged += new System.EventHandler(this.NotchTypeComboBox_SelectedIndexChanged);
            // 
            // ExportFolderGroupBox
            // 
            this.ExportFolderGroupBox.Controls.Add(this.tbExportFolder);
            this.ExportFolderGroupBox.Location = new System.Drawing.Point(3, 118);
            this.ExportFolderGroupBox.Name = "ExportFolderGroupBox";
            this.ExportFolderGroupBox.Size = new System.Drawing.Size(164, 53);
            this.ExportFolderGroupBox.TabIndex = 10;
            this.ExportFolderGroupBox.TabStop = false;
            this.ExportFolderGroupBox.Text = "輸出資料夾";
            // 
            // tbExportFolder
            // 
            this.tbExportFolder.Location = new System.Drawing.Point(6, 21);
            this.tbExportFolder.Name = "tbExportFolder";
            this.tbExportFolder.Size = new System.Drawing.Size(152, 23);
            this.tbExportFolder.TabIndex = 0;
            this.tbExportFolder.Text = "20200108";
            this.tbExportFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbExportFolder.TextChanged += new System.EventHandler(this.tbExportFolder_TextChanged);
            // 
            // TestCountGroupBox
            // 
            this.TestCountGroupBox.Controls.Add(this.TestCountUpDown);
            this.TestCountGroupBox.Location = new System.Drawing.Point(243, 569);
            this.TestCountGroupBox.Name = "TestCountGroupBox";
            this.TestCountGroupBox.Size = new System.Drawing.Size(99, 59);
            this.TestCountGroupBox.TabIndex = 11;
            this.TestCountGroupBox.TabStop = false;
            this.TestCountGroupBox.Text = "測試次數";
            // 
            // TestCountUpDown
            // 
            this.TestCountUpDown.Location = new System.Drawing.Point(9, 22);
            this.TestCountUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.TestCountUpDown.Name = "TestCountUpDown";
            this.TestCountUpDown.Size = new System.Drawing.Size(81, 23);
            this.TestCountUpDown.TabIndex = 11;
            this.TestCountUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TestCountUpDown.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // gbSingleMotion
            // 
            this.gbSingleMotion.Controls.Add(this.AlignerIniButton);
            this.gbSingleMotion.Controls.Add(this.CylinderIniButton);
            this.gbSingleMotion.Controls.Add(this.CalculateButton);
            this.gbSingleMotion.Controls.Add(this.AlignButton);
            this.gbSingleMotion.Controls.Add(this.GrabButton);
            this.gbSingleMotion.Location = new System.Drawing.Point(7, 512);
            this.gbSingleMotion.Name = "gbSingleMotion";
            this.gbSingleMotion.Size = new System.Drawing.Size(230, 180);
            this.gbSingleMotion.TabIndex = 6;
            this.gbSingleMotion.TabStop = false;
            // 
            // AlignerIniButton
            // 
            this.AlignerIniButton.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AlignerIniButton.Location = new System.Drawing.Point(115, 13);
            this.AlignerIniButton.Name = "AlignerIniButton";
            this.AlignerIniButton.Size = new System.Drawing.Size(98, 49);
            this.AlignerIniButton.TabIndex = 1;
            this.AlignerIniButton.Text = "Aligner初始化";
            this.AlignerIniButton.UseVisualStyleBackColor = true;
            this.AlignerIniButton.Click += new System.EventHandler(this.AlignerIniButton_Click);
            // 
            // CylinderIniButton
            // 
            this.CylinderIniButton.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CylinderIniButton.Location = new System.Drawing.Point(9, 13);
            this.CylinderIniButton.Name = "CylinderIniButton";
            this.CylinderIniButton.Size = new System.Drawing.Size(100, 49);
            this.CylinderIniButton.TabIndex = 0;
            this.CylinderIniButton.Text = "汽缸初始化";
            this.CylinderIniButton.UseVisualStyleBackColor = true;
            this.CylinderIniButton.Click += new System.EventHandler(this.CylinderIniButton_Click);
            // 
            // CalculateButton
            // 
            this.CalculateButton.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CalculateButton.Location = new System.Drawing.Point(115, 122);
            this.CalculateButton.Name = "CalculateButton";
            this.CalculateButton.Size = new System.Drawing.Size(100, 49);
            this.CalculateButton.TabIndex = 4;
            this.CalculateButton.Text = "計算";
            this.CalculateButton.UseVisualStyleBackColor = true;
            this.CalculateButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // AlignButton
            // 
            this.AlignButton.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AlignButton.Location = new System.Drawing.Point(9, 68);
            this.AlignButton.Name = "AlignButton";
            this.AlignButton.Size = new System.Drawing.Size(100, 49);
            this.AlignButton.TabIndex = 2;
            this.AlignButton.Text = "Align";
            this.AlignButton.UseVisualStyleBackColor = true;
            this.AlignButton.Click += new System.EventHandler(this.AlignButton_Click);
            // 
            // GrabButton
            // 
            this.GrabButton.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GrabButton.Location = new System.Drawing.Point(9, 122);
            this.GrabButton.Name = "GrabButton";
            this.GrabButton.Size = new System.Drawing.Size(100, 49);
            this.GrabButton.TabIndex = 3;
            this.GrabButton.Text = "取像";
            this.GrabButton.UseVisualStyleBackColor = true;
            this.GrabButton.Click += new System.EventHandler(this.GrabButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.BackColor = System.Drawing.Color.NavajoWhite;
            this.RunButton.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RunButton.Location = new System.Drawing.Point(245, 638);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(100, 49);
            this.RunButton.TabIndex = 5;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = false;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // AOIPage
            // 
            this.AOIPage.Controls.Add(this.gbAOI);
            this.AOIPage.Location = new System.Drawing.Point(4, 25);
            this.AOIPage.Name = "AOIPage";
            this.AOIPage.Padding = new System.Windows.Forms.Padding(3);
            this.AOIPage.Size = new System.Drawing.Size(351, 699);
            this.AOIPage.TabIndex = 1;
            this.AOIPage.Text = "影像設定";
            this.AOIPage.UseVisualStyleBackColor = true;
            // 
            // gbAOI
            // 
            this.gbAOI.Controls.Add(this.btnReadConfigFile);
            this.gbAOI.Controls.Add(this.gbAOISetup);
            this.gbAOI.Controls.Add(this.btnLoadBuffer);
            this.gbAOI.Controls.Add(this.btnContinusTest);
            this.gbAOI.Controls.Add(this.btnOpenImageFolder);
            this.gbAOI.Controls.Add(this.PreTestButton);
            this.gbAOI.Controls.Add(this.btnTestInfoBackup);
            this.gbAOI.Controls.Add(this.TestButton);
            this.gbAOI.Controls.Add(this.btnCalculateResutl);
            this.gbAOI.Controls.Add(this.NextTestButton);
            this.gbAOI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAOI.Location = new System.Drawing.Point(3, 3);
            this.gbAOI.Name = "gbAOI";
            this.gbAOI.Size = new System.Drawing.Size(345, 693);
            this.gbAOI.TabIndex = 29;
            this.gbAOI.TabStop = false;
            // 
            // btnReadConfigFile
            // 
            this.btnReadConfigFile.Location = new System.Drawing.Point(242, 398);
            this.btnReadConfigFile.Name = "btnReadConfigFile";
            this.btnReadConfigFile.Size = new System.Drawing.Size(98, 57);
            this.btnReadConfigFile.TabIndex = 29;
            this.btnReadConfigFile.Text = "讀取參數檔";
            this.btnReadConfigFile.UseVisualStyleBackColor = true;
            this.btnReadConfigFile.Visible = false;
            this.btnReadConfigFile.Click += new System.EventHandler(this.btnReadConfigFile_Click);
            // 
            // gbAOISetup
            // 
            this.gbAOISetup.Controls.Add(this.cbFillWafer);
            this.gbAOISetup.Controls.Add(this.cbManualBinary);
            this.gbAOISetup.Controls.Add(this.gbBinaryTHL);
            this.gbAOISetup.Controls.Add(this.gbFilterMask);
            this.gbAOISetup.Controls.Add(this.btnShowBinary);
            this.gbAOISetup.Controls.Add(this.btnShowROI);
            this.gbAOISetup.Controls.Add(this.tkbrROIBottom);
            this.gbAOISetup.Controls.Add(this.tkbrROITop);
            this.gbAOISetup.Controls.Add(this.tbROIBottom);
            this.gbAOISetup.Controls.Add(this.tbROITop);
            this.gbAOISetup.Controls.Add(this.lbROIBottom);
            this.gbAOISetup.Controls.Add(this.lbROITop);
            this.gbAOISetup.Location = new System.Drawing.Point(3, 22);
            this.gbAOISetup.Name = "gbAOISetup";
            this.gbAOISetup.Size = new System.Drawing.Size(342, 247);
            this.gbAOISetup.TabIndex = 18;
            this.gbAOISetup.TabStop = false;
            this.gbAOISetup.Text = "影像參數設定";
            // 
            // cbFillWafer
            // 
            this.cbFillWafer.AutoSize = true;
            this.cbFillWafer.Location = new System.Drawing.Point(7, 193);
            this.cbFillWafer.Name = "cbFillWafer";
            this.cbFillWafer.Size = new System.Drawing.Size(86, 20);
            this.cbFillWafer.TabIndex = 20;
            this.cbFillWafer.Text = "塗滿Wafer";
            this.cbFillWafer.UseVisualStyleBackColor = true;
            this.cbFillWafer.CheckedChanged += new System.EventHandler(this.cbFillWafer_CheckedChanged);
            // 
            // cbManualBinary
            // 
            this.cbManualBinary.AutoSize = true;
            this.cbManualBinary.Location = new System.Drawing.Point(10, 79);
            this.cbManualBinary.Name = "cbManualBinary";
            this.cbManualBinary.Size = new System.Drawing.Size(87, 20);
            this.cbManualBinary.TabIndex = 19;
            this.cbManualBinary.Text = "手動二值化";
            this.cbManualBinary.UseVisualStyleBackColor = true;
            this.cbManualBinary.CheckedChanged += new System.EventHandler(this.cbManualBinary_CheckedChanged);
            // 
            // gbBinaryTHL
            // 
            this.gbBinaryTHL.Controls.Add(this.tkbrBinaryTHL);
            this.gbBinaryTHL.Controls.Add(this.tbBinaryTHL);
            this.gbBinaryTHL.Enabled = false;
            this.gbBinaryTHL.Location = new System.Drawing.Point(6, 105);
            this.gbBinaryTHL.Name = "gbBinaryTHL";
            this.gbBinaryTHL.Size = new System.Drawing.Size(208, 78);
            this.gbBinaryTHL.TabIndex = 18;
            this.gbBinaryTHL.TabStop = false;
            // 
            // tkbrBinaryTHL
            // 
            this.tkbrBinaryTHL.Location = new System.Drawing.Point(55, 22);
            this.tkbrBinaryTHL.Maximum = 255;
            this.tkbrBinaryTHL.Name = "tkbrBinaryTHL";
            this.tkbrBinaryTHL.Size = new System.Drawing.Size(147, 45);
            this.tkbrBinaryTHL.TabIndex = 17;
            this.tkbrBinaryTHL.ValueChanged += new System.EventHandler(this.tkbrBinaryTHL_ValueChanged);
            // 
            // tbBinaryTHL
            // 
            this.tbBinaryTHL.Location = new System.Drawing.Point(4, 22);
            this.tbBinaryTHL.Name = "tbBinaryTHL";
            this.tbBinaryTHL.Size = new System.Drawing.Size(45, 23);
            this.tbBinaryTHL.TabIndex = 16;
            this.tbBinaryTHL.Text = "0";
            // 
            // gbFilterMask
            // 
            this.gbFilterMask.Controls.Add(this.udFilterMask);
            this.gbFilterMask.Location = new System.Drawing.Point(232, 119);
            this.gbFilterMask.Name = "gbFilterMask";
            this.gbFilterMask.Size = new System.Drawing.Size(74, 68);
            this.gbFilterMask.TabIndex = 14;
            this.gbFilterMask.TabStop = false;
            this.gbFilterMask.Text = "濾波遮罩";
            // 
            // udFilterMask
            // 
            this.udFilterMask.Location = new System.Drawing.Point(8, 22);
            this.udFilterMask.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udFilterMask.Name = "udFilterMask";
            this.udFilterMask.Size = new System.Drawing.Size(60, 23);
            this.udFilterMask.TabIndex = 0;
            this.udFilterMask.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udFilterMask.ValueChanged += new System.EventHandler(this.udFilterMask_ValueChanged);
            // 
            // btnShowBinary
            // 
            this.btnShowBinary.Location = new System.Drawing.Point(193, 193);
            this.btnShowBinary.Name = "btnShowBinary";
            this.btnShowBinary.Size = new System.Drawing.Size(137, 45);
            this.btnShowBinary.TabIndex = 13;
            this.btnShowBinary.Text = "顯示二值化";
            this.btnShowBinary.UseVisualStyleBackColor = true;
            this.btnShowBinary.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnShowROI
            // 
            this.btnShowROI.Location = new System.Drawing.Point(232, 73);
            this.btnShowROI.Name = "btnShowROI";
            this.btnShowROI.Size = new System.Drawing.Size(98, 30);
            this.btnShowROI.TabIndex = 6;
            this.btnShowROI.Text = "顯示邊界";
            this.btnShowROI.UseVisualStyleBackColor = true;
            this.btnShowROI.Click += new System.EventHandler(this.btnShowROI_Click);
            // 
            // tkbrROIBottom
            // 
            this.tkbrROIBottom.Location = new System.Drawing.Point(220, 22);
            this.tkbrROIBottom.Maximum = 12;
            this.tkbrROIBottom.Name = "tkbrROIBottom";
            this.tkbrROIBottom.Size = new System.Drawing.Size(110, 45);
            this.tkbrROIBottom.TabIndex = 5;
            this.tkbrROIBottom.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tkbrROIBottom.Value = 12;
            this.tkbrROIBottom.ValueChanged += new System.EventHandler(this.tkbrImageTop_ValueChanged);
            // 
            // tkbrROITop
            // 
            this.tkbrROITop.Location = new System.Drawing.Point(54, 22);
            this.tkbrROITop.Maximum = 12;
            this.tkbrROITop.Name = "tkbrROITop";
            this.tkbrROITop.Size = new System.Drawing.Size(110, 45);
            this.tkbrROITop.TabIndex = 4;
            this.tkbrROITop.ValueChanged += new System.EventHandler(this.tkbrImageTop_ValueChanged);
            // 
            // tbROIBottom
            // 
            this.tbROIBottom.Location = new System.Drawing.Point(173, 44);
            this.tbROIBottom.Name = "tbROIBottom";
            this.tbROIBottom.Size = new System.Drawing.Size(26, 23);
            this.tbROIBottom.TabIndex = 3;
            this.tbROIBottom.Text = "12";
            // 
            // tbROITop
            // 
            this.tbROITop.Location = new System.Drawing.Point(10, 44);
            this.tbROITop.Name = "tbROITop";
            this.tbROITop.Size = new System.Drawing.Size(26, 23);
            this.tbROITop.TabIndex = 2;
            this.tbROITop.Text = "0";
            // 
            // lbROIBottom
            // 
            this.lbROIBottom.AutoSize = true;
            this.lbROIBottom.Location = new System.Drawing.Point(170, 22);
            this.lbROIBottom.Name = "lbROIBottom";
            this.lbROIBottom.Size = new System.Drawing.Size(44, 16);
            this.lbROIBottom.TabIndex = 1;
            this.lbROIBottom.Text = "下邊界";
            // 
            // lbROITop
            // 
            this.lbROITop.AutoSize = true;
            this.lbROITop.Location = new System.Drawing.Point(7, 22);
            this.lbROITop.Name = "lbROITop";
            this.lbROITop.Size = new System.Drawing.Size(44, 16);
            this.lbROITop.TabIndex = 0;
            this.lbROITop.Text = "上邊界";
            // 
            // btnLoadBuffer
            // 
            this.btnLoadBuffer.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnLoadBuffer.Location = new System.Drawing.Point(6, 634);
            this.btnLoadBuffer.Name = "btnLoadBuffer";
            this.btnLoadBuffer.Size = new System.Drawing.Size(100, 50);
            this.btnLoadBuffer.TabIndex = 17;
            this.btnLoadBuffer.Text = "暫存區影像";
            this.btnLoadBuffer.UseVisualStyleBackColor = true;
            this.btnLoadBuffer.Click += new System.EventHandler(this.btnLoadBuffer_Click);
            // 
            // btnContinusTest
            // 
            this.btnContinusTest.Location = new System.Drawing.Point(9, 321);
            this.btnContinusTest.Name = "btnContinusTest";
            this.btnContinusTest.Size = new System.Drawing.Size(100, 50);
            this.btnContinusTest.TabIndex = 28;
            this.btnContinusTest.Text = "連續測試";
            this.btnContinusTest.UseVisualStyleBackColor = true;
            this.btnContinusTest.Click += new System.EventHandler(this.btnContinusTest_Click);
            // 
            // btnOpenImageFolder
            // 
            this.btnOpenImageFolder.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnOpenImageFolder.Location = new System.Drawing.Point(6, 578);
            this.btnOpenImageFolder.Name = "btnOpenImageFolder";
            this.btnOpenImageFolder.Size = new System.Drawing.Size(100, 50);
            this.btnOpenImageFolder.TabIndex = 16;
            this.btnOpenImageFolder.Text = "資料夾影像";
            this.btnOpenImageFolder.UseVisualStyleBackColor = true;
            this.btnOpenImageFolder.Click += new System.EventHandler(this.btnOpenImageFolder_Click);
            // 
            // PreTestButton
            // 
            this.PreTestButton.BackColor = System.Drawing.SystemColors.Control;
            this.PreTestButton.Image = ((System.Drawing.Image)(resources.GetObject("PreTestButton.Image")));
            this.PreTestButton.Location = new System.Drawing.Point(9, 275);
            this.PreTestButton.Name = "PreTestButton";
            this.PreTestButton.Size = new System.Drawing.Size(40, 40);
            this.PreTestButton.TabIndex = 24;
            this.PreTestButton.UseVisualStyleBackColor = false;
            this.PreTestButton.Click += new System.EventHandler(this.NextTestButton_Click);
            // 
            // btnTestInfoBackup
            // 
            this.btnTestInfoBackup.Location = new System.Drawing.Point(242, 338);
            this.btnTestInfoBackup.Name = "btnTestInfoBackup";
            this.btnTestInfoBackup.Size = new System.Drawing.Size(98, 57);
            this.btnTestInfoBackup.TabIndex = 27;
            this.btnTestInfoBackup.Text = "資料備份";
            this.btnTestInfoBackup.UseVisualStyleBackColor = true;
            this.btnTestInfoBackup.Click += new System.EventHandler(this.btnTestInfoBackup_Click);
            // 
            // TestButton
            // 
            this.TestButton.BackColor = System.Drawing.SystemColors.Control;
            this.TestButton.Image = ((System.Drawing.Image)(resources.GetObject("TestButton.Image")));
            this.TestButton.Location = new System.Drawing.Point(55, 275);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(40, 40);
            this.TestButton.TabIndex = 26;
            this.TestButton.UseVisualStyleBackColor = false;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // btnCalculateResutl
            // 
            this.btnCalculateResutl.Location = new System.Drawing.Point(242, 275);
            this.btnCalculateResutl.Name = "btnCalculateResutl";
            this.btnCalculateResutl.Size = new System.Drawing.Size(98, 57);
            this.btnCalculateResutl.TabIndex = 19;
            this.btnCalculateResutl.Text = "計算";
            this.btnCalculateResutl.UseVisualStyleBackColor = true;
            this.btnCalculateResutl.Visible = false;
            this.btnCalculateResutl.Click += new System.EventHandler(this.btnCalculateResutl_Click);
            // 
            // NextTestButton
            // 
            this.NextTestButton.BackColor = System.Drawing.SystemColors.Control;
            this.NextTestButton.Image = ((System.Drawing.Image)(resources.GetObject("NextTestButton.Image")));
            this.NextTestButton.Location = new System.Drawing.Point(101, 275);
            this.NextTestButton.Name = "NextTestButton";
            this.NextTestButton.Size = new System.Drawing.Size(40, 40);
            this.NextTestButton.TabIndex = 25;
            this.NextTestButton.UseVisualStyleBackColor = false;
            this.NextTestButton.Click += new System.EventHandler(this.NextTestButton_Click);
            // 
            // AdvancePage
            // 
            this.AdvancePage.Controls.Add(this.gbAdvance);
            this.AdvancePage.Location = new System.Drawing.Point(4, 25);
            this.AdvancePage.Name = "AdvancePage";
            this.AdvancePage.Padding = new System.Windows.Forms.Padding(3);
            this.AdvancePage.Size = new System.Drawing.Size(351, 699);
            this.AdvancePage.TabIndex = 2;
            this.AdvancePage.Text = "進階設定";
            this.AdvancePage.UseVisualStyleBackColor = true;
            // 
            // gbAdvance
            // 
            this.gbAdvance.Controls.Add(this.gbAlarmStop);
            this.gbAdvance.Controls.Add(this.gbCalibrate);
            this.gbAdvance.Controls.Add(this.btnSaveParas);
            this.gbAdvance.Controls.Add(this.gbDownloadData);
            this.gbAdvance.Controls.Add(this.gbAdvanceParas);
            this.gbAdvance.Controls.Add(this.CylinderGroupBox);
            this.gbAdvance.Controls.Add(this.gbAligner);
            this.gbAdvance.Controls.Add(this.gbCamera);
            this.gbAdvance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAdvance.Location = new System.Drawing.Point(3, 3);
            this.gbAdvance.Name = "gbAdvance";
            this.gbAdvance.Size = new System.Drawing.Size(345, 693);
            this.gbAdvance.TabIndex = 6;
            this.gbAdvance.TabStop = false;
            // 
            // gbAlarmStop
            // 
            this.gbAlarmStop.Controls.Add(this.tbNOffsetUpLimit);
            this.gbAlarmStop.Controls.Add(this.tbOOffsetUpLimit);
            this.gbAlarmStop.Controls.Add(this.cbAlarmStopDownloadData);
            this.gbAlarmStop.Controls.Add(this.cbAlarmStopEnabled);
            this.gbAlarmStop.Controls.Add(this.lbNOffsetUpLimit);
            this.gbAlarmStop.Controls.Add(this.lbOOffsetUpLimit);
            this.gbAlarmStop.Location = new System.Drawing.Point(9, 579);
            this.gbAlarmStop.Name = "gbAlarmStop";
            this.gbAlarmStop.Size = new System.Drawing.Size(243, 108);
            this.gbAlarmStop.TabIndex = 7;
            this.gbAlarmStop.TabStop = false;
            this.gbAlarmStop.Text = "規格設定";
            // 
            // tbNOffsetUpLimit
            // 
            this.tbNOffsetUpLimit.Location = new System.Drawing.Point(154, 79);
            this.tbNOffsetUpLimit.Name = "tbNOffsetUpLimit";
            this.tbNOffsetUpLimit.Size = new System.Drawing.Size(80, 23);
            this.tbNOffsetUpLimit.TabIndex = 6;
            this.tbNOffsetUpLimit.Text = "0.2";
            this.tbNOffsetUpLimit.TextChanged += new System.EventHandler(this.tbNOffsetUpLimit_TextChanged);
            // 
            // tbOOffsetUpLimit
            // 
            this.tbOOffsetUpLimit.Location = new System.Drawing.Point(154, 47);
            this.tbOOffsetUpLimit.Name = "tbOOffsetUpLimit";
            this.tbOOffsetUpLimit.Size = new System.Drawing.Size(80, 23);
            this.tbOOffsetUpLimit.TabIndex = 5;
            this.tbOOffsetUpLimit.Text = "0.2";
            this.tbOOffsetUpLimit.TextChanged += new System.EventHandler(this.tbOOffsetUpLimit_TextChanged);
            // 
            // cbAlarmStopDownloadData
            // 
            this.cbAlarmStopDownloadData.AutoSize = true;
            this.cbAlarmStopDownloadData.Location = new System.Drawing.Point(111, 22);
            this.cbAlarmStopDownloadData.Name = "cbAlarmStopDownloadData";
            this.cbAlarmStopDownloadData.Size = new System.Drawing.Size(123, 20);
            this.cbAlarmStopDownloadData.TabIndex = 4;
            this.cbAlarmStopDownloadData.Text = "超過規格下載資料";
            this.cbAlarmStopDownloadData.UseVisualStyleBackColor = true;
            this.cbAlarmStopDownloadData.CheckedChanged += new System.EventHandler(this.cbAlarmStopDownloadData_CheckedChanged);
            // 
            // cbAlarmStopEnabled
            // 
            this.cbAlarmStopEnabled.AutoSize = true;
            this.cbAlarmStopEnabled.Location = new System.Drawing.Point(6, 22);
            this.cbAlarmStopEnabled.Name = "cbAlarmStopEnabled";
            this.cbAlarmStopEnabled.Size = new System.Drawing.Size(99, 20);
            this.cbAlarmStopEnabled.TabIndex = 3;
            this.cbAlarmStopEnabled.Text = "超過規格停機";
            this.cbAlarmStopEnabled.UseVisualStyleBackColor = true;
            this.cbAlarmStopEnabled.CheckedChanged += new System.EventHandler(this.cbAlarmStopEnabled_CheckedChanged);
            // 
            // lbNOffsetUpLimit
            // 
            this.lbNOffsetUpLimit.AutoSize = true;
            this.lbNOffsetUpLimit.Location = new System.Drawing.Point(11, 82);
            this.lbNOffsetUpLimit.Name = "lbNOffsetUpLimit";
            this.lbNOffsetUpLimit.Size = new System.Drawing.Size(113, 16);
            this.lbNOffsetUpLimit.TabIndex = 2;
            this.lbNOffsetUpLimit.Text = "N offset 上限(deg)";
            // 
            // lbOOffsetUpLimit
            // 
            this.lbOOffsetUpLimit.AutoSize = true;
            this.lbOOffsetUpLimit.Location = new System.Drawing.Point(11, 51);
            this.lbOOffsetUpLimit.Name = "lbOOffsetUpLimit";
            this.lbOOffsetUpLimit.Size = new System.Drawing.Size(112, 16);
            this.lbOOffsetUpLimit.TabIndex = 0;
            this.lbOOffsetUpLimit.Text = "O offset 上限(mm)";
            // 
            // gbCalibrate
            // 
            this.gbCalibrate.Controls.Add(this.tbParam196);
            this.gbCalibrate.Controls.Add(this.tbParam195);
            this.gbCalibrate.Controls.Add(this.tbParam194);
            this.gbCalibrate.Controls.Add(this.tbParam193);
            this.gbCalibrate.Controls.Add(this.lbParam196);
            this.gbCalibrate.Controls.Add(this.lbParam195);
            this.gbCalibrate.Controls.Add(this.lbParam194);
            this.gbCalibrate.Controls.Add(this.lbParam193);
            this.gbCalibrate.Controls.Add(this.Calibrate);
            this.gbCalibrate.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCalibrate.Location = new System.Drawing.Point(3, 400);
            this.gbCalibrate.Name = "gbCalibrate";
            this.gbCalibrate.Size = new System.Drawing.Size(339, 173);
            this.gbCalibrate.TabIndex = 6;
            this.gbCalibrate.TabStop = false;
            this.gbCalibrate.Text = "校正";
            // 
            // tbParam196
            // 
            this.tbParam196.Location = new System.Drawing.Point(90, 130);
            this.tbParam196.Name = "tbParam196";
            this.tbParam196.Size = new System.Drawing.Size(100, 23);
            this.tbParam196.TabIndex = 8;
            // 
            // tbParam195
            // 
            this.tbParam195.Location = new System.Drawing.Point(90, 92);
            this.tbParam195.Name = "tbParam195";
            this.tbParam195.Size = new System.Drawing.Size(100, 23);
            this.tbParam195.TabIndex = 7;
            // 
            // tbParam194
            // 
            this.tbParam194.Location = new System.Drawing.Point(90, 56);
            this.tbParam194.Name = "tbParam194";
            this.tbParam194.Size = new System.Drawing.Size(100, 23);
            this.tbParam194.TabIndex = 6;
            // 
            // tbParam193
            // 
            this.tbParam193.Location = new System.Drawing.Point(90, 19);
            this.tbParam193.Name = "tbParam193";
            this.tbParam193.Size = new System.Drawing.Size(100, 23);
            this.tbParam193.TabIndex = 5;
            // 
            // lbParam196
            // 
            this.lbParam196.AutoSize = true;
            this.lbParam196.Location = new System.Drawing.Point(17, 130);
            this.lbParam196.Name = "lbParam196";
            this.lbParam196.Size = new System.Drawing.Size(67, 16);
            this.lbParam196.TabIndex = 4;
            this.lbParam196.Text = "Param196";
            // 
            // lbParam195
            // 
            this.lbParam195.AutoSize = true;
            this.lbParam195.Location = new System.Drawing.Point(17, 95);
            this.lbParam195.Name = "lbParam195";
            this.lbParam195.Size = new System.Drawing.Size(67, 16);
            this.lbParam195.TabIndex = 3;
            this.lbParam195.Text = "Param195";
            // 
            // lbParam194
            // 
            this.lbParam194.AutoSize = true;
            this.lbParam194.Location = new System.Drawing.Point(17, 59);
            this.lbParam194.Name = "lbParam194";
            this.lbParam194.Size = new System.Drawing.Size(67, 16);
            this.lbParam194.TabIndex = 2;
            this.lbParam194.Text = "Param194";
            // 
            // lbParam193
            // 
            this.lbParam193.AutoSize = true;
            this.lbParam193.Location = new System.Drawing.Point(17, 22);
            this.lbParam193.Name = "lbParam193";
            this.lbParam193.Size = new System.Drawing.Size(67, 16);
            this.lbParam193.TabIndex = 1;
            this.lbParam193.Text = "Param193";
            // 
            // Calibrate
            // 
            this.Calibrate.AccessibleDescription = "btnCalibration";
            this.Calibrate.Location = new System.Drawing.Point(255, 22);
            this.Calibrate.Name = "Calibrate";
            this.Calibrate.Size = new System.Drawing.Size(75, 33);
            this.Calibrate.TabIndex = 0;
            this.Calibrate.Text = "校正";
            this.Calibrate.UseVisualStyleBackColor = true;
            this.Calibrate.Click += new System.EventHandler(this.Calibrate_Click);
            // 
            // btnSaveParas
            // 
            this.btnSaveParas.Location = new System.Drawing.Point(258, 639);
            this.btnSaveParas.Name = "btnSaveParas";
            this.btnSaveParas.Size = new System.Drawing.Size(81, 45);
            this.btnSaveParas.TabIndex = 4;
            this.btnSaveParas.Text = "儲存參數";
            this.btnSaveParas.UseVisualStyleBackColor = true;
            this.btnSaveParas.Click += new System.EventHandler(this.btnSaveParas_Click);
            // 
            // gbDownloadData
            // 
            this.gbDownloadData.Controls.Add(this.btnDownloadData);
            this.gbDownloadData.Controls.Add(this.cbDownloadData);
            this.gbDownloadData.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDownloadData.Location = new System.Drawing.Point(3, 346);
            this.gbDownloadData.Name = "gbDownloadData";
            this.gbDownloadData.Size = new System.Drawing.Size(339, 54);
            this.gbDownloadData.TabIndex = 3;
            this.gbDownloadData.TabStop = false;
            this.gbDownloadData.Text = "CCD and Encoder";
            // 
            // btnDownloadData
            // 
            this.btnDownloadData.Location = new System.Drawing.Point(255, 17);
            this.btnDownloadData.Name = "btnDownloadData";
            this.btnDownloadData.Size = new System.Drawing.Size(80, 29);
            this.btnDownloadData.TabIndex = 1;
            this.btnDownloadData.Text = "下載資料";
            this.btnDownloadData.UseVisualStyleBackColor = true;
            this.btnDownloadData.Click += new System.EventHandler(this.btnDownloadData_Click);
            // 
            // cbDownloadData
            // 
            this.cbDownloadData.AutoSize = true;
            this.cbDownloadData.Location = new System.Drawing.Point(9, 22);
            this.cbDownloadData.Name = "cbDownloadData";
            this.cbDownloadData.Size = new System.Drawing.Size(119, 20);
            this.cbDownloadData.TabIndex = 0;
            this.cbDownloadData.Text = "Download Data";
            this.cbDownloadData.UseVisualStyleBackColor = true;
            this.cbDownloadData.CheckedChanged += new System.EventHandler(this.cbDownloadData_CheckedChanged);
            // 
            // gbAdvanceParas
            // 
            this.gbAdvanceParas.Controls.Add(this.cbOffsetType);
            this.gbAdvanceParas.Controls.Add(this.cbCheckWaferPresentInAutoRun);
            this.gbAdvanceParas.Controls.Add(this.gbOutputFolder);
            this.gbAdvanceParas.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbAdvanceParas.Location = new System.Drawing.Point(3, 265);
            this.gbAdvanceParas.Name = "gbAdvanceParas";
            this.gbAdvanceParas.Size = new System.Drawing.Size(339, 81);
            this.gbAdvanceParas.TabIndex = 5;
            this.gbAdvanceParas.TabStop = false;
            this.gbAdvanceParas.Text = "進階參數";
            // 
            // cbOffsetType
            // 
            this.cbOffsetType.AutoSize = true;
            this.cbOffsetType.Location = new System.Drawing.Point(183, 48);
            this.cbOffsetType.Name = "cbOffsetType";
            this.cbOffsetType.Size = new System.Drawing.Size(159, 20);
            this.cbOffsetType.TabIndex = 5;
            this.cbOffsetType.Text = "根據平均位置計算偏移量";
            this.cbOffsetType.UseVisualStyleBackColor = true;
            this.cbOffsetType.CheckedChanged += new System.EventHandler(this.cbOffsetType_CheckedChanged);
            // 
            // cbCheckWaferPresentInAutoRun
            // 
            this.cbCheckWaferPresentInAutoRun.AutoSize = true;
            this.cbCheckWaferPresentInAutoRun.Location = new System.Drawing.Point(183, 19);
            this.cbCheckWaferPresentInAutoRun.Name = "cbCheckWaferPresentInAutoRun";
            this.cbCheckWaferPresentInAutoRun.Size = new System.Drawing.Size(143, 20);
            this.cbCheckWaferPresentInAutoRun.TabIndex = 4;
            this.cbCheckWaferPresentInAutoRun.Text = "Present sensor 啟用";
            this.cbCheckWaferPresentInAutoRun.UseVisualStyleBackColor = true;
            this.cbCheckWaferPresentInAutoRun.CheckedChanged += new System.EventHandler(this.cbCheckWaferPresentInAutoRun_CheckedChanged);
            // 
            // gbOutputFolder
            // 
            this.gbOutputFolder.Controls.Add(this.lbShowOutputFolder);
            this.gbOutputFolder.Controls.Add(this.btnOutputFolder);
            this.gbOutputFolder.Location = new System.Drawing.Point(6, 19);
            this.gbOutputFolder.Name = "gbOutputFolder";
            this.gbOutputFolder.Size = new System.Drawing.Size(160, 56);
            this.gbOutputFolder.TabIndex = 3;
            this.gbOutputFolder.TabStop = false;
            this.gbOutputFolder.Text = "輸出資料夾";
            // 
            // lbShowOutputFolder
            // 
            this.lbShowOutputFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbShowOutputFolder.Location = new System.Drawing.Point(6, 25);
            this.lbShowOutputFolder.Name = "lbShowOutputFolder";
            this.lbShowOutputFolder.Size = new System.Drawing.Size(77, 26);
            this.lbShowOutputFolder.TabIndex = 1;
            this.lbShowOutputFolder.Text = "D:\\";
            this.lbShowOutputFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.Location = new System.Drawing.Point(100, 11);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(54, 40);
            this.btnOutputFolder.TabIndex = 2;
            this.btnOutputFolder.Text = "點選";
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);
            // 
            // CylinderGroupBox
            // 
            this.CylinderGroupBox.Controls.Add(this.cbCylinderEnabled);
            this.CylinderGroupBox.Controls.Add(this.lbCylinderStatus);
            this.CylinderGroupBox.Controls.Add(this.btnCylinderConnect);
            this.CylinderGroupBox.Controls.Add(this.cmbCylinderBaudRate);
            this.CylinderGroupBox.Controls.Add(this.lbCylinderBaudRate);
            this.CylinderGroupBox.Controls.Add(this.cmbCylinderPort);
            this.CylinderGroupBox.Controls.Add(this.lbCylinderPort);
            this.CylinderGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.CylinderGroupBox.Location = new System.Drawing.Point(3, 179);
            this.CylinderGroupBox.Name = "CylinderGroupBox";
            this.CylinderGroupBox.Size = new System.Drawing.Size(339, 86);
            this.CylinderGroupBox.TabIndex = 0;
            this.CylinderGroupBox.TabStop = false;
            this.CylinderGroupBox.Text = "Cylinder";
            // 
            // cbCylinderEnabled
            // 
            this.cbCylinderEnabled.AutoSize = true;
            this.cbCylinderEnabled.Location = new System.Drawing.Point(9, 25);
            this.cbCylinderEnabled.Name = "cbCylinderEnabled";
            this.cbCylinderEnabled.Size = new System.Drawing.Size(51, 20);
            this.cbCylinderEnabled.TabIndex = 7;
            this.cbCylinderEnabled.Text = "啟用";
            this.cbCylinderEnabled.UseVisualStyleBackColor = true;
            this.cbCylinderEnabled.CheckedChanged += new System.EventHandler(this.cbCylinderEnabled_CheckedChanged);
            // 
            // lbCylinderStatus
            // 
            this.lbCylinderStatus.BackColor = System.Drawing.Color.Gray;
            this.lbCylinderStatus.Location = new System.Drawing.Point(66, 19);
            this.lbCylinderStatus.Name = "lbCylinderStatus";
            this.lbCylinderStatus.Size = new System.Drawing.Size(80, 25);
            this.lbCylinderStatus.TabIndex = 6;
            this.lbCylinderStatus.Text = "未啟用";
            this.lbCylinderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCylinderConnect
            // 
            this.btnCylinderConnect.Location = new System.Drawing.Point(65, 52);
            this.btnCylinderConnect.Name = "btnCylinderConnect";
            this.btnCylinderConnect.Size = new System.Drawing.Size(81, 29);
            this.btnCylinderConnect.TabIndex = 5;
            this.btnCylinderConnect.Text = "連線";
            this.btnCylinderConnect.UseVisualStyleBackColor = true;
            this.btnCylinderConnect.Click += new System.EventHandler(this.btnCylinderConnect_Click);
            // 
            // cmbCylinderBaudRate
            // 
            this.cmbCylinderBaudRate.FormattingEnabled = true;
            this.cmbCylinderBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.cmbCylinderBaudRate.Location = new System.Drawing.Point(255, 49);
            this.cmbCylinderBaudRate.Name = "cmbCylinderBaudRate";
            this.cmbCylinderBaudRate.Size = new System.Drawing.Size(66, 24);
            this.cmbCylinderBaudRate.TabIndex = 4;
            this.cmbCylinderBaudRate.Text = "38400";
            this.cmbCylinderBaudRate.SelectedIndexChanged += new System.EventHandler(this.cmbCylinderBaudRate_SelectedIndexChanged);
            // 
            // lbCylinderBaudRate
            // 
            this.lbCylinderBaudRate.AutoSize = true;
            this.lbCylinderBaudRate.Location = new System.Drawing.Point(180, 52);
            this.lbCylinderBaudRate.Name = "lbCylinderBaudRate";
            this.lbCylinderBaudRate.Size = new System.Drawing.Size(69, 16);
            this.lbCylinderBaudRate.TabIndex = 3;
            this.lbCylinderBaudRate.Text = "Baud Rate";
            // 
            // cmbCylinderPort
            // 
            this.cmbCylinderPort.FormattingEnabled = true;
            this.cmbCylinderPort.Location = new System.Drawing.Point(255, 19);
            this.cmbCylinderPort.Name = "cmbCylinderPort";
            this.cmbCylinderPort.Size = new System.Drawing.Size(66, 24);
            this.cmbCylinderPort.TabIndex = 2;
            this.cmbCylinderPort.Text = "COM24";
            this.cmbCylinderPort.SelectedIndexChanged += new System.EventHandler(this.cmbCylinderPort_SelectedIndexChanged);
            // 
            // lbCylinderPort
            // 
            this.lbCylinderPort.AutoSize = true;
            this.lbCylinderPort.Location = new System.Drawing.Point(185, 23);
            this.lbCylinderPort.Name = "lbCylinderPort";
            this.lbCylinderPort.Size = new System.Drawing.Size(64, 16);
            this.lbCylinderPort.TabIndex = 1;
            this.lbCylinderPort.Text = "Com Port";
            // 
            // gbAligner
            // 
            this.gbAligner.Controls.Add(this.cbAlignerEnabled);
            this.gbAligner.Controls.Add(this.lbAlignerStatus);
            this.gbAligner.Controls.Add(this.btnAlignerConnect);
            this.gbAligner.Controls.Add(this.cmbAlignerBaudRate);
            this.gbAligner.Controls.Add(this.lbAlignerBaudRate);
            this.gbAligner.Controls.Add(this.cmbAlignerPort);
            this.gbAligner.Controls.Add(this.lbAlignerPort);
            this.gbAligner.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbAligner.Location = new System.Drawing.Point(3, 102);
            this.gbAligner.Name = "gbAligner";
            this.gbAligner.Size = new System.Drawing.Size(339, 77);
            this.gbAligner.TabIndex = 1;
            this.gbAligner.TabStop = false;
            this.gbAligner.Text = "Aligner";
            // 
            // cbAlignerEnabled
            // 
            this.cbAlignerEnabled.AutoSize = true;
            this.cbAlignerEnabled.Location = new System.Drawing.Point(9, 22);
            this.cbAlignerEnabled.Name = "cbAlignerEnabled";
            this.cbAlignerEnabled.Size = new System.Drawing.Size(51, 20);
            this.cbAlignerEnabled.TabIndex = 8;
            this.cbAlignerEnabled.Text = "啟用";
            this.cbAlignerEnabled.UseVisualStyleBackColor = true;
            this.cbAlignerEnabled.CheckedChanged += new System.EventHandler(this.cbAlignerEnabled_CheckedChanged);
            // 
            // lbAlignerStatus
            // 
            this.lbAlignerStatus.BackColor = System.Drawing.Color.Gray;
            this.lbAlignerStatus.Location = new System.Drawing.Point(66, 13);
            this.lbAlignerStatus.Name = "lbAlignerStatus";
            this.lbAlignerStatus.Size = new System.Drawing.Size(80, 25);
            this.lbAlignerStatus.TabIndex = 6;
            this.lbAlignerStatus.Text = "未啟用";
            this.lbAlignerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAlignerConnect
            // 
            this.btnAlignerConnect.Location = new System.Drawing.Point(65, 41);
            this.btnAlignerConnect.Name = "btnAlignerConnect";
            this.btnAlignerConnect.Size = new System.Drawing.Size(81, 29);
            this.btnAlignerConnect.TabIndex = 5;
            this.btnAlignerConnect.Text = "連線";
            this.btnAlignerConnect.UseVisualStyleBackColor = true;
            this.btnAlignerConnect.Click += new System.EventHandler(this.btnCylinderConnect_Click);
            // 
            // cmbAlignerBaudRate
            // 
            this.cmbAlignerBaudRate.FormattingEnabled = true;
            this.cmbAlignerBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.cmbAlignerBaudRate.Location = new System.Drawing.Point(255, 44);
            this.cmbAlignerBaudRate.Name = "cmbAlignerBaudRate";
            this.cmbAlignerBaudRate.Size = new System.Drawing.Size(66, 24);
            this.cmbAlignerBaudRate.TabIndex = 4;
            this.cmbAlignerBaudRate.Text = "38400";
            this.cmbAlignerBaudRate.SelectedIndexChanged += new System.EventHandler(this.cmbAlignerBaudRate_SelectedIndexChanged);
            // 
            // lbAlignerBaudRate
            // 
            this.lbAlignerBaudRate.AutoSize = true;
            this.lbAlignerBaudRate.Location = new System.Drawing.Point(180, 47);
            this.lbAlignerBaudRate.Name = "lbAlignerBaudRate";
            this.lbAlignerBaudRate.Size = new System.Drawing.Size(69, 16);
            this.lbAlignerBaudRate.TabIndex = 3;
            this.lbAlignerBaudRate.Text = "Baud Rate";
            // 
            // cmbAlignerPort
            // 
            this.cmbAlignerPort.FormattingEnabled = true;
            this.cmbAlignerPort.Location = new System.Drawing.Point(255, 13);
            this.cmbAlignerPort.Name = "cmbAlignerPort";
            this.cmbAlignerPort.Size = new System.Drawing.Size(66, 24);
            this.cmbAlignerPort.TabIndex = 2;
            this.cmbAlignerPort.Text = "COM17";
            this.cmbAlignerPort.SelectedIndexChanged += new System.EventHandler(this.cmbAlignerPort_SelectedIndexChanged);
            // 
            // lbAlignerPort
            // 
            this.lbAlignerPort.AutoSize = true;
            this.lbAlignerPort.Location = new System.Drawing.Point(185, 17);
            this.lbAlignerPort.Name = "lbAlignerPort";
            this.lbAlignerPort.Size = new System.Drawing.Size(64, 16);
            this.lbAlignerPort.TabIndex = 1;
            this.lbAlignerPort.Text = "Com Port";
            // 
            // gbCamera
            // 
            this.gbCamera.Controls.Add(this.cbCameraEnabled);
            this.gbCamera.Controls.Add(this.lbCameraStatus);
            this.gbCamera.Controls.Add(this.btnCameraConnect);
            this.gbCamera.Controls.Add(this.cmbCameraBaudRate);
            this.gbCamera.Controls.Add(this.lbCameraBaudRate);
            this.gbCamera.Controls.Add(this.cmbCameraPort);
            this.gbCamera.Controls.Add(this.lbCameraPort);
            this.gbCamera.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCamera.Location = new System.Drawing.Point(3, 19);
            this.gbCamera.Name = "gbCamera";
            this.gbCamera.Size = new System.Drawing.Size(339, 83);
            this.gbCamera.TabIndex = 2;
            this.gbCamera.TabStop = false;
            this.gbCamera.Text = "Camera";
            // 
            // cbCameraEnabled
            // 
            this.cbCameraEnabled.AutoSize = true;
            this.cbCameraEnabled.Location = new System.Drawing.Point(9, 22);
            this.cbCameraEnabled.Name = "cbCameraEnabled";
            this.cbCameraEnabled.Size = new System.Drawing.Size(51, 20);
            this.cbCameraEnabled.TabIndex = 8;
            this.cbCameraEnabled.Text = "啟用";
            this.cbCameraEnabled.UseVisualStyleBackColor = true;
            this.cbCameraEnabled.CheckedChanged += new System.EventHandler(this.cbCameraEnabled_CheckedChanged);
            // 
            // lbCameraStatus
            // 
            this.lbCameraStatus.BackColor = System.Drawing.Color.Gray;
            this.lbCameraStatus.Location = new System.Drawing.Point(66, 17);
            this.lbCameraStatus.Name = "lbCameraStatus";
            this.lbCameraStatus.Size = new System.Drawing.Size(80, 25);
            this.lbCameraStatus.TabIndex = 6;
            this.lbCameraStatus.Text = "未啟用";
            this.lbCameraStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCameraConnect
            // 
            this.btnCameraConnect.Location = new System.Drawing.Point(65, 46);
            this.btnCameraConnect.Name = "btnCameraConnect";
            this.btnCameraConnect.Size = new System.Drawing.Size(81, 29);
            this.btnCameraConnect.TabIndex = 5;
            this.btnCameraConnect.Text = "連線";
            this.btnCameraConnect.UseVisualStyleBackColor = true;
            this.btnCameraConnect.Click += new System.EventHandler(this.btnCylinderConnect_Click);
            // 
            // cmbCameraBaudRate
            // 
            this.cmbCameraBaudRate.FormattingEnabled = true;
            this.cmbCameraBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.cmbCameraBaudRate.Location = new System.Drawing.Point(255, 51);
            this.cmbCameraBaudRate.Name = "cmbCameraBaudRate";
            this.cmbCameraBaudRate.Size = new System.Drawing.Size(66, 24);
            this.cmbCameraBaudRate.TabIndex = 4;
            this.cmbCameraBaudRate.Text = "38400";
            this.cmbCameraBaudRate.SelectedIndexChanged += new System.EventHandler(this.cmbCameraBaudRate_SelectedIndexChanged);
            // 
            // lbCameraBaudRate
            // 
            this.lbCameraBaudRate.AutoSize = true;
            this.lbCameraBaudRate.Location = new System.Drawing.Point(180, 54);
            this.lbCameraBaudRate.Name = "lbCameraBaudRate";
            this.lbCameraBaudRate.Size = new System.Drawing.Size(69, 16);
            this.lbCameraBaudRate.TabIndex = 3;
            this.lbCameraBaudRate.Text = "Baud Rate";
            // 
            // cmbCameraPort
            // 
            this.cmbCameraPort.FormattingEnabled = true;
            this.cmbCameraPort.Location = new System.Drawing.Point(255, 17);
            this.cmbCameraPort.Name = "cmbCameraPort";
            this.cmbCameraPort.Size = new System.Drawing.Size(66, 24);
            this.cmbCameraPort.TabIndex = 2;
            this.cmbCameraPort.Text = "COM71";
            this.cmbCameraPort.SelectedIndexChanged += new System.EventHandler(this.cmbCameraPort_SelectedIndexChanged);
            // 
            // lbCameraPort
            // 
            this.lbCameraPort.AutoSize = true;
            this.lbCameraPort.Location = new System.Drawing.Point(185, 19);
            this.lbCameraPort.Name = "lbCameraPort";
            this.lbCameraPort.Size = new System.Drawing.Size(64, 16);
            this.lbCameraPort.TabIndex = 1;
            this.lbCameraPort.Text = "Com Port";
            // 
            // TestTabControl
            // 
            this.TestTabControl.Controls.Add(this.gbTest);
            this.TestTabControl.Location = new System.Drawing.Point(4, 25);
            this.TestTabControl.Name = "TestTabControl";
            this.TestTabControl.Padding = new System.Windows.Forms.Padding(3);
            this.TestTabControl.Size = new System.Drawing.Size(351, 699);
            this.TestTabControl.TabIndex = 3;
            this.TestTabControl.Text = "單點測試";
            this.TestTabControl.UseVisualStyleBackColor = true;
            // 
            // gbTest
            // 
            this.gbTest.Controls.Add(this.gbPresentTest);
            this.gbTest.Controls.Add(this.btnStartMonitorPresent);
            this.gbTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTest.Location = new System.Drawing.Point(3, 3);
            this.gbTest.Name = "gbTest";
            this.gbTest.Size = new System.Drawing.Size(345, 693);
            this.gbTest.TabIndex = 0;
            this.gbTest.TabStop = false;
            // 
            // gbPresentTest
            // 
            this.gbPresentTest.Controls.Add(this.lbEndMonitorTime);
            this.gbPresentTest.Controls.Add(this.lbStartMonitorTime);
            this.gbPresentTest.Controls.Add(this.lbEndMonitorTimeLabel);
            this.gbPresentTest.Controls.Add(this.lbStartMonitorTimeLabel);
            this.gbPresentTest.Controls.Add(this.lbPresentStatus);
            this.gbPresentTest.Controls.Add(this.lbPresentMonitorTime);
            this.gbPresentTest.Controls.Add(this.udPresentMonitorSec);
            this.gbPresentTest.Controls.Add(this.udPresentMonitorMin);
            this.gbPresentTest.Controls.Add(this.udPresentMonitorHour);
            this.gbPresentTest.Controls.Add(this.lbPresentMonitorSec);
            this.gbPresentTest.Controls.Add(this.lbPresentMonitorMin);
            this.gbPresentTest.Controls.Add(this.lbPresentMonitorHour);
            this.gbPresentTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPresentTest.Location = new System.Drawing.Point(3, 19);
            this.gbPresentTest.Name = "gbPresentTest";
            this.gbPresentTest.Size = new System.Drawing.Size(339, 160);
            this.gbPresentTest.TabIndex = 0;
            this.gbPresentTest.TabStop = false;
            this.gbPresentTest.Text = "Present 監控";
            // 
            // lbEndMonitorTime
            // 
            this.lbEndMonitorTime.Location = new System.Drawing.Point(88, 129);
            this.lbEndMonitorTime.Name = "lbEndMonitorTime";
            this.lbEndMonitorTime.Size = new System.Drawing.Size(68, 16);
            this.lbEndMonitorTime.TabIndex = 12;
            this.lbEndMonitorTime.Text = "00:00:00";
            // 
            // lbStartMonitorTime
            // 
            this.lbStartMonitorTime.Location = new System.Drawing.Point(88, 91);
            this.lbStartMonitorTime.Name = "lbStartMonitorTime";
            this.lbStartMonitorTime.Size = new System.Drawing.Size(68, 16);
            this.lbStartMonitorTime.TabIndex = 11;
            this.lbStartMonitorTime.Text = "00:00:00";
            // 
            // lbEndMonitorTimeLabel
            // 
            this.lbEndMonitorTimeLabel.AutoSize = true;
            this.lbEndMonitorTimeLabel.Location = new System.Drawing.Point(14, 129);
            this.lbEndMonitorTimeLabel.Name = "lbEndMonitorTimeLabel";
            this.lbEndMonitorTimeLabel.Size = new System.Drawing.Size(68, 16);
            this.lbEndMonitorTimeLabel.TabIndex = 10;
            this.lbEndMonitorTimeLabel.Text = "結束時間：";
            // 
            // lbStartMonitorTimeLabel
            // 
            this.lbStartMonitorTimeLabel.AutoSize = true;
            this.lbStartMonitorTimeLabel.Location = new System.Drawing.Point(14, 91);
            this.lbStartMonitorTimeLabel.Name = "lbStartMonitorTimeLabel";
            this.lbStartMonitorTimeLabel.Size = new System.Drawing.Size(68, 16);
            this.lbStartMonitorTimeLabel.TabIndex = 9;
            this.lbStartMonitorTimeLabel.Text = "開始時間：";
            // 
            // lbPresentStatus
            // 
            this.lbPresentStatus.BackColor = System.Drawing.Color.Lime;
            this.lbPresentStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPresentStatus.Location = new System.Drawing.Point(243, 26);
            this.lbPresentStatus.Name = "lbPresentStatus";
            this.lbPresentStatus.Size = new System.Drawing.Size(90, 35);
            this.lbPresentStatus.TabIndex = 8;
            this.lbPresentStatus.Text = "狀態正常";
            this.lbPresentStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPresentMonitorTime
            // 
            this.lbPresentMonitorTime.AutoSize = true;
            this.lbPresentMonitorTime.Location = new System.Drawing.Point(14, 19);
            this.lbPresentMonitorTime.Name = "lbPresentMonitorTime";
            this.lbPresentMonitorTime.Size = new System.Drawing.Size(56, 16);
            this.lbPresentMonitorTime.TabIndex = 7;
            this.lbPresentMonitorTime.Text = "監控時間";
            // 
            // udPresentMonitorSec
            // 
            this.udPresentMonitorSec.Location = new System.Drawing.Point(159, 38);
            this.udPresentMonitorSec.Name = "udPresentMonitorSec";
            this.udPresentMonitorSec.Size = new System.Drawing.Size(39, 23);
            this.udPresentMonitorSec.TabIndex = 5;
            this.udPresentMonitorSec.ValueChanged += new System.EventHandler(this.udPresentMonitorSec_ValueChanged);
            // 
            // udPresentMonitorMin
            // 
            this.udPresentMonitorMin.Location = new System.Drawing.Point(88, 38);
            this.udPresentMonitorMin.Name = "udPresentMonitorMin";
            this.udPresentMonitorMin.Size = new System.Drawing.Size(39, 23);
            this.udPresentMonitorMin.TabIndex = 4;
            this.udPresentMonitorMin.ValueChanged += new System.EventHandler(this.udPresentMonitorMin_ValueChanged);
            // 
            // udPresentMonitorHour
            // 
            this.udPresentMonitorHour.Location = new System.Drawing.Point(17, 38);
            this.udPresentMonitorHour.Name = "udPresentMonitorHour";
            this.udPresentMonitorHour.Size = new System.Drawing.Size(39, 23);
            this.udPresentMonitorHour.TabIndex = 3;
            this.udPresentMonitorHour.ValueChanged += new System.EventHandler(this.udPresentMonitorHour_ValueChanged);
            // 
            // lbPresentMonitorSec
            // 
            this.lbPresentMonitorSec.AutoSize = true;
            this.lbPresentMonitorSec.Location = new System.Drawing.Point(206, 40);
            this.lbPresentMonitorSec.Name = "lbPresentMonitorSec";
            this.lbPresentMonitorSec.Size = new System.Drawing.Size(20, 16);
            this.lbPresentMonitorSec.TabIndex = 2;
            this.lbPresentMonitorSec.Text = "秒";
            // 
            // lbPresentMonitorMin
            // 
            this.lbPresentMonitorMin.AutoSize = true;
            this.lbPresentMonitorMin.Location = new System.Drawing.Point(133, 40);
            this.lbPresentMonitorMin.Name = "lbPresentMonitorMin";
            this.lbPresentMonitorMin.Size = new System.Drawing.Size(20, 16);
            this.lbPresentMonitorMin.TabIndex = 1;
            this.lbPresentMonitorMin.Text = "分";
            // 
            // lbPresentMonitorHour
            // 
            this.lbPresentMonitorHour.AutoSize = true;
            this.lbPresentMonitorHour.Location = new System.Drawing.Point(62, 40);
            this.lbPresentMonitorHour.Name = "lbPresentMonitorHour";
            this.lbPresentMonitorHour.Size = new System.Drawing.Size(20, 16);
            this.lbPresentMonitorHour.TabIndex = 0;
            this.lbPresentMonitorHour.Text = "時";
            // 
            // btnStartMonitorPresent
            // 
            this.btnStartMonitorPresent.Location = new System.Drawing.Point(246, 185);
            this.btnStartMonitorPresent.Name = "btnStartMonitorPresent";
            this.btnStartMonitorPresent.Size = new System.Drawing.Size(90, 34);
            this.btnStartMonitorPresent.TabIndex = 6;
            this.btnStartMonitorPresent.Text = "開始";
            this.btnStartMonitorPresent.UseVisualStyleBackColor = true;
            this.btnStartMonitorPresent.Click += new System.EventHandler(this.btnStartMonitorPresent_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DisplayImageBox);
            this.panel1.Controls.Add(this.BottomPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 728);
            this.panel1.TabIndex = 6;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 728);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1328, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(300, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(200, 17);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.AutoSize = false;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(800, 17);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 750);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.statusStrip1);
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "FormMain";
            this.Text = "Aligner Verification - Ver.1.02.2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.BottomPanel.ResumeLayout(false);
            this.ShowResultGroupBox.ResumeLayout(false);
            this.NotchGroupBox.ResumeLayout(false);
            this.gbOffSetPos.ResumeLayout(false);
            this.TaskTimeGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DisplayImageBox)).EndInit();
            this.DisplayImageBox.ResumeLayout(false);
            this.DisplayImageBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FilterImageBox)).EndInit();
            this.RightPanel.ResumeLayout(false);
            this.LeftTabControl.ResumeLayout(false);
            this.GeneralPage.ResumeLayout(false);
            this.pnGeneral.ResumeLayout(false);
            this.WaferRadiusGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WaferRadiusUpDown)).EndInit();
            this.gbTestMode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udTestModeTOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTestModeYOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTestModeXOffset)).EndInit();
            this.TestSpeedGroupBox.ResumeLayout(false);
            this.gbCylinderMotion.ResumeLayout(false);
            this.NotchAngleGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udNotchAngle)).EndInit();
            this.WaferTypeGroupBox.ResumeLayout(false);
            this.AlignXOffsetGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udAlignXOffset)).EndInit();
            this.gbMachineType.ResumeLayout(false);
            this.ExportFolderGroupBox.ResumeLayout(false);
            this.ExportFolderGroupBox.PerformLayout();
            this.TestCountGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TestCountUpDown)).EndInit();
            this.gbSingleMotion.ResumeLayout(false);
            this.AOIPage.ResumeLayout(false);
            this.gbAOI.ResumeLayout(false);
            this.gbAOISetup.ResumeLayout(false);
            this.gbAOISetup.PerformLayout();
            this.gbBinaryTHL.ResumeLayout(false);
            this.gbBinaryTHL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbrBinaryTHL)).EndInit();
            this.gbFilterMask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udFilterMask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkbrROIBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkbrROITop)).EndInit();
            this.AdvancePage.ResumeLayout(false);
            this.gbAdvance.ResumeLayout(false);
            this.gbAlarmStop.ResumeLayout(false);
            this.gbAlarmStop.PerformLayout();
            this.gbCalibrate.ResumeLayout(false);
            this.gbCalibrate.PerformLayout();
            this.gbDownloadData.ResumeLayout(false);
            this.gbDownloadData.PerformLayout();
            this.gbAdvanceParas.ResumeLayout(false);
            this.gbAdvanceParas.PerformLayout();
            this.gbOutputFolder.ResumeLayout(false);
            this.CylinderGroupBox.ResumeLayout(false);
            this.CylinderGroupBox.PerformLayout();
            this.gbAligner.ResumeLayout(false);
            this.gbAligner.PerformLayout();
            this.gbCamera.ResumeLayout(false);
            this.gbCamera.PerformLayout();
            this.TestTabControl.ResumeLayout(false);
            this.gbTest.ResumeLayout(false);
            this.gbPresentTest.ResumeLayout(false);
            this.gbPresentTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPresentMonitorSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPresentMonitorMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPresentMonitorHour)).EndInit();
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel BottomPanel;
        private Emgu.CV.UI.ImageBox DisplayImageBox;
        private System.Windows.Forms.RichTextBox RichTextBox1;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.TabControl LeftTabControl;
        private System.Windows.Forms.TabPage GeneralPage;
        private System.Windows.Forms.GroupBox gbSingleMotion;
        private System.Windows.Forms.Button AlignerIniButton;
        private System.Windows.Forms.Button CylinderIniButton;
        private System.Windows.Forms.Button AlignButton;
        private System.Windows.Forms.Button GrabButton;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.TabPage AOIPage;
        private System.Windows.Forms.GroupBox TaskTimeGroupBox;
        private System.Windows.Forms.Label ShowMinTaskTimeLabel;
        private System.Windows.Forms.Label ShowMaxTaskTimeLabel;
        private System.Windows.Forms.Label ShowAvgTaskTimeLabel;
        private System.Windows.Forms.Label ShowTaskTimeLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox ShowResultGroupBox;
        private System.Windows.Forms.Label ShowNDegoffsetLabel;
        private System.Windows.Forms.Label ShowNoffsetLabel;
        private System.Windows.Forms.Label ShowToffsetLabel;
        private System.Windows.Forms.GroupBox NotchGroupBox;
        private System.Windows.Forms.Label ShowAvgNotchPosYLabel;
        private System.Windows.Forms.Label ShowAvgNotchPosXLabel;
        private System.Windows.Forms.Label ShowNotchPosYLabel;
        private System.Windows.Forms.Label ShowNotchPosXLabel;
        private System.Windows.Forms.GroupBox gbOffSetPos;
        private System.Windows.Forms.Label ShowAvgCenterPosYLabel;
        private System.Windows.Forms.Label ShowAvgCenterPosXLabel;
        private System.Windows.Forms.Label ShowCenterPosYLabel;
        private System.Windows.Forms.Label ShowCenterPosXLabel;
        private System.Windows.Forms.GroupBox WaferTypeGroupBox;
        private System.Windows.Forms.ComboBox cmbWaferType;
        private System.Windows.Forms.GroupBox gbMachineType;
        private System.Windows.Forms.ComboBox cmbNotchType;
        private System.Windows.Forms.GroupBox AlignXOffsetGroupBox;
        private System.Windows.Forms.NumericUpDown udAlignXOffset;
        private System.Windows.Forms.GroupBox TestCountGroupBox;
        private System.Windows.Forms.NumericUpDown TestCountUpDown;
        private System.Windows.Forms.GroupBox ExportFolderGroupBox;
        private System.Windows.Forms.TextBox tbExportFolder;
        private System.Windows.Forms.GroupBox NotchAngleGroupBox;
        private System.Windows.Forms.NumericUpDown udNotchAngle;
        private System.Windows.Forms.GroupBox TestSpeedGroupBox;
        private System.Windows.Forms.ComboBox cmbTestSpeed;
        private System.Windows.Forms.GroupBox WaferRadiusGroupBox;
        private System.Windows.Forms.NumericUpDown WaferRadiusUpDown;
        private System.Windows.Forms.TabPage AdvancePage;
        private System.Windows.Forms.GroupBox CylinderGroupBox;
        private System.Windows.Forms.ComboBox cmbCylinderBaudRate;
        private System.Windows.Forms.Label lbCylinderBaudRate;
        private System.Windows.Forms.ComboBox cmbCylinderPort;
        private System.Windows.Forms.Label lbCylinderPort;
        private System.Windows.Forms.CheckBox cbCylinderEnabled;
        private System.Windows.Forms.Label lbCylinderStatus;
        private System.Windows.Forms.Button btnCylinderConnect;
        private System.Windows.Forms.GroupBox gbAligner;
        private System.Windows.Forms.CheckBox cbAlignerEnabled;
        private System.Windows.Forms.Label lbAlignerStatus;
        private System.Windows.Forms.Button btnAlignerConnect;
        private System.Windows.Forms.ComboBox cmbAlignerBaudRate;
        private System.Windows.Forms.Label lbAlignerBaudRate;
        private System.Windows.Forms.ComboBox cmbAlignerPort;
        private System.Windows.Forms.Label lbAlignerPort;
        private System.Windows.Forms.GroupBox gbTestMode;
        private System.Windows.Forms.GroupBox gbCylinderMotion;
        private System.Windows.Forms.Button btnCylinderMoveUp;
        private System.Windows.Forms.Button btnCylinderMoveDown;
        private System.Windows.Forms.Button btnCylinderRelease;
        private System.Windows.Forms.Button btnCylinderHold;
        private System.Windows.Forms.GroupBox gbCamera;
        private System.Windows.Forms.CheckBox cbCameraEnabled;
        private System.Windows.Forms.Label lbCameraStatus;
        private System.Windows.Forms.Button btnCameraConnect;
        private System.Windows.Forms.ComboBox cmbCameraBaudRate;
        private System.Windows.Forms.Label lbCameraBaudRate;
        private System.Windows.Forms.ComboBox cmbCameraPort;
        private System.Windows.Forms.Label lbCameraPort;
        private System.Windows.Forms.NumericUpDown udTestModeTOffset;
        private System.Windows.Forms.NumericUpDown udTestModeYOffset;
        private System.Windows.Forms.NumericUpDown udTestModeXOffset;
        private System.Windows.Forms.Label lbTestModeTOffset;
        private System.Windows.Forms.Label lbTestModeYOffset;
        private System.Windows.Forms.Label lbTestModeXOffset;
        private System.Windows.Forms.ComboBox cmbTestMode;
        private System.Windows.Forms.GroupBox gbDownloadData;
        private System.Windows.Forms.CheckBox cbDownloadData;
        private System.Windows.Forms.Button btnSaveParas;
        private System.Windows.Forms.Button btnLoadBuffer;
        private System.Windows.Forms.Button btnOpenImageFolder;
        private System.Windows.Forms.GroupBox gbAOISetup;
        private System.Windows.Forms.TrackBar tkbrROITop;
        private System.Windows.Forms.TextBox tbROIBottom;
        private System.Windows.Forms.TextBox tbROITop;
        private System.Windows.Forms.Label lbROIBottom;
        private System.Windows.Forms.Label lbROITop;
        private System.Windows.Forms.TrackBar tkbrROIBottom;
        private Emgu.CV.UI.ImageBox FilterImageBox;
        private System.Windows.Forms.Button btnShowROI;
        private System.Windows.Forms.Button btnShowBinary;
        private System.Windows.Forms.GroupBox gbFilterMask;
        private System.Windows.Forms.NumericUpDown udFilterMask;
        private System.Windows.Forms.CheckBox cbManualBinary;
        private System.Windows.Forms.GroupBox gbBinaryTHL;
        private System.Windows.Forms.TrackBar tkbrBinaryTHL;
        private System.Windows.Forms.TextBox tbBinaryTHL;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.Button btnCalculateResutl;
        private System.Windows.Forms.Label lbNotchPosY;
        private System.Windows.Forms.Label lbNotchPosX;
        private System.Windows.Forms.Label lbTopPosY;
        private System.Windows.Forms.Label lbTopPosX;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Button NextTestButton;
        private System.Windows.Forms.Button PreTestButton;
        private System.Windows.Forms.Button btnTestInfoBackup;
        private System.Windows.Forms.Button btnContinusTest;
        private System.Windows.Forms.Label lbNDegoffset;
        private System.Windows.Forms.Label lbNoffset;
        private System.Windows.Forms.Label lbToffset;
        private System.Windows.Forms.Label lbAvgNotchPosY;
        private System.Windows.Forms.Label lbAvgNotchPosX;
        private System.Windows.Forms.Label lbAvgTopPosY;
        private System.Windows.Forms.Label lbAvgTopPosX;
        private System.Windows.Forms.Label lbTaskTime;
        private System.Windows.Forms.Button CalculateButton;
        private System.Windows.Forms.Label lbAvgTaskTime;
        private System.Windows.Forms.Label lbMinTaskTime;
        private System.Windows.Forms.Label lbMaxTaskTime;
        private System.Windows.Forms.Label lbShowCurrentCnt;
        private System.Windows.Forms.GroupBox gbAdvanceParas;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.Label lbShowOutputFolder;
        private System.Windows.Forms.Button btnDownloadData;
        private System.Windows.Forms.Panel pnGeneral;
        private System.Windows.Forms.GroupBox gbAOI;
        private System.Windows.Forms.GroupBox gbAdvance;
        private System.Windows.Forms.GroupBox gbCalibrate;
        private System.Windows.Forms.Button Calibrate;
        private System.Windows.Forms.TextBox tbParam196;
        private System.Windows.Forms.TextBox tbParam195;
        private System.Windows.Forms.TextBox tbParam194;
        private System.Windows.Forms.TextBox tbParam193;
        private System.Windows.Forms.Label lbParam196;
        private System.Windows.Forms.Label lbParam195;
        private System.Windows.Forms.Label lbParam194;
        private System.Windows.Forms.Label lbParam193;
        private System.Windows.Forms.GroupBox gbOutputFolder;
        private System.Windows.Forms.TabPage TestTabControl;
        private System.Windows.Forms.GroupBox gbTest;
        private System.Windows.Forms.GroupBox gbPresentTest;
        private System.Windows.Forms.Label lbEndMonitorTime;
        private System.Windows.Forms.Label lbStartMonitorTime;
        private System.Windows.Forms.Label lbEndMonitorTimeLabel;
        private System.Windows.Forms.Label lbStartMonitorTimeLabel;
        private System.Windows.Forms.Label lbPresentStatus;
        private System.Windows.Forms.Label lbPresentMonitorTime;
        private System.Windows.Forms.Button btnStartMonitorPresent;
        private System.Windows.Forms.NumericUpDown udPresentMonitorSec;
        private System.Windows.Forms.NumericUpDown udPresentMonitorMin;
        private System.Windows.Forms.NumericUpDown udPresentMonitorHour;
        private System.Windows.Forms.Label lbPresentMonitorSec;
        private System.Windows.Forms.Label lbPresentMonitorMin;
        private System.Windows.Forms.Label lbPresentMonitorHour;
        private System.Windows.Forms.CheckBox cbCheckWaferPresentInAutoRun;
        private System.Windows.Forms.Button btnReadConfigFile;
        private System.Windows.Forms.CheckBox cbOffsetType;
        private System.Windows.Forms.CheckBox cbFillWafer;
        private System.Windows.Forms.GroupBox gbAlarmStop;
        private System.Windows.Forms.TextBox tbNOffsetUpLimit;
        private System.Windows.Forms.TextBox tbOOffsetUpLimit;
        private System.Windows.Forms.CheckBox cbAlarmStopDownloadData;
        private System.Windows.Forms.CheckBox cbAlarmStopEnabled;
        private System.Windows.Forms.Label lbNOffsetUpLimit;
        private System.Windows.Forms.Label lbOOffsetUpLimit;
    }
}

