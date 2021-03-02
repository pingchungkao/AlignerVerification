using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.IO;

namespace AlignerVerification.CommandConvert
{
    public class EncoderAligner
    {
        private string Supplier;

        /// <summary>
        /// Aligner Encoder
        /// </summary>
        /// <param name="supplier"> 設備供應商 </param>
        /// <param name="dtCommand"> Parameter List </param>
        public EncoderAligner(string supplier)
        {
            try
            {
                Supplier = supplier;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        private string EndCode()
        {
            string result = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL":
                case "ATEL_NEW":
                    result = "\r";
                    break;

                case "KAWASAKI":
                    result = "\r\n";
                    break;
            }
            return result;
        }
        public string ArmLocation(string Address, string Sequence, string Type, string Unit)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                
                    commandStr = "${0}{1}GET:POS__:{2},{3}";
                    commandStr = string.Format(commandStr, Address, Sequence, Type, Unit);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr + EndCode();
        }
        /// <summary>
        /// 執行尋找晶圓(Wafer)缺口後移動至所需的角度位置
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="angle"> Align 後 Notch 所要移動的角度 </param>
        /// <returns></returns>
        public string Align(string Address, string Sequence, string angle)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                
                    commandStr = "${0}{1}CMD:ALIGN:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, angle.PadLeft(3,'0').PadRight(6,'0'));
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr + EndCode();
        }

        /// <summary>
        /// 執行尋找晶圓(Wafer)缺口後移動至所需的角度位置 [ SANWA ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="angle"> Align 後 Notch 所要移動的角度  </param>
        /// <param name="notch"> Align 尋邊動作模式 
        /// <para> 0 : 根據上一次align結果，不再做尋邊動作，直接到notch/flat指定角度，並補正偏心，不先回home </para>
        /// <para> 1 : normal mode </para>
        /// <para> 2 : 只做尋邊動作，aligner不做到notch/flat指定角度與補正偏心 </para> </param>
        /// <param name="ZAxis"> Z軸動作模式
        /// <para> 0 : 無Z軸 </para>
        /// <para> 1 : align完成Z軸下降 </para>
        /// <para> 2 : align完成Z軸上升 </para> </param>                     
        /// <param name="mode"> 執行模式          
        /// <para> 0 快速模式，尋邊1圈，並以最短路徑到notch/flat指定角度，並補正偏心 </para>
        /// <para> 1 normal mode  </para> </param>
        /// <returns></returns>
        public string Align(string Address, string Sequence, string angle, string notch, string ZAxis, string mode)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":

                    commandStr = "${0}{1}CMD:ALIGN:{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Address, Sequence, angle.PadLeft(3, '0').PadRight(6, '0'), notch, ZAxis, mode);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr + EndCode();          
        }

        /// <summary>
        /// 設定晶圓(Wafer)大小
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="vl"> vl：Wafer Size 的半徑(um)，150000 代表 12 吋 Wafer，100000 代表 8 吋 Wafer </param>
        /// <returns></returns>
        public string SetSize(string Address, string Sequence, string vl)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":

                    commandStr = "${0}{1}SET:ALIGN:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, vl);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr + EndCode();
        }

        /// <summary>
        /// 暫停解除
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string DeviceContinue(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
               
                    commandStr = "${0}{1}SET:CONT_";
                    commandStr = string.Format(commandStr, Address, Sequence);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr + EndCode();
        }

        /// <summary>
        /// 動作暫停
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string DevicePause(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
               
                    commandStr = "${0}{1}SET:PAUSE";
                    commandStr = string.Format(commandStr, Address, Sequence);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr + EndCode();
        }

        /// <summary>
        /// 動作停止
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="m1"></param>
        /// <returns></returns>
        public string DeviceStop(string Address, string Sequence, string m1)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
               
                    commandStr = "${0}{1}SET:STOP_";
                    commandStr = string.Format(commandStr, Address, Sequence);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr + EndCode();
        }

        /// <summary>
        /// Error 履歷取得 
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> 履歷號碼  2 位數  10 進位 </param>
        /// <returns></returns>
        public string ErrorMessage(string Address, string Sequence, string no)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                    commandStr = "${0}{1}GET:ERR__:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, Convert.ToInt16(no).ToString("00")) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// Error 解除
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string ErrorReset(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
               
                    commandStr = "${0}{1}SET:RESET";
                    commandStr= string.Format(commandStr, Address, Sequence);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Servo On
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="sv"> Servo ON / OFF 選擇  (0~1) </param>
        /// <returns></returns>
        public string ServoOn(string Address, string Sequence, string sv)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
              
                    commandStr = "${0}{1}SET:SERVO:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, sv) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 各軸移動至 HOME 位置:Normal Home
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Home(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
               
                    commandStr = "${0}{1}CMD:HOME_";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 各軸回 home 的速度回 ORG 的位置，並確認 ORG Sensor
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string HomeOrgin(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
              
                    commandStr = "${0}{1}CMD:RHOME";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 設定 Log file  儲存
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string LogSave(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
             
                    commandStr = "${0}{1}SET:LOGSV";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 動作模式選擇設定
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="vl"> 動作模式選擇  </param>
        /// <returns></returns>
        public string Mode(string Address, string Sequence, string vl)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
           
                    commandStr = "${0}{1}SET:MODE_:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence,vl) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取得動作模式選擇設定
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string GetMode(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
         
                    commandStr = "${0}{1}GET:MODE_";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 移動指定軸到指定點位
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 軸 (01 ~ 16) </param>
        /// <param name="type"> 移動方式選擇 </param>
        /// <param name="pos"> 移動數量, 9  位數  10 進位(-99999999 ~ +99999999) </param>
        /// <returns></returns>
        public string MoveDirect(string Address, string Sequence, string axis, string type, string pos)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
           
                    commandStr = "${0}{1}CMD:MOVED:{2},{3},{4}";
                    commandStr = string.Format(commandStr, Address, Sequence, axis, type, pos) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

      

        /// <summary>
        /// 原點復歸
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string OrginSearch(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                
                    commandStr = "${0}{1}CMD:ORG__";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取得 Aligner 參數值
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Type">  參數類別 </param>
        /// <param name="No"> 參數號碼 </param>
        /// <returns></returns>
        public string Parameter(string Address, string Sequence, string Type, string No)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
        
                    commandStr = "${0}{1}SET:PARAM:{2},{3}";
                    commandStr = string.Format(commandStr, Address, Sequence, Type, No) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取得 Aligner 參數詳細資料
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Type">  參數類別(0~9) </param>
        /// <param name="No">  參數號碼(000~999) </param>
        /// <returns></returns>
        public string PARSY(string Address, string Sequence, string Type, string No)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
              
                    commandStr = "${0}{1}GET:PARSY:{2},{3}";
                    commandStr = string.Format(commandStr, Address, Sequence, Type, No) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// Arm Retract
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Retract(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
               
                    commandStr = "${0}{1}CMD:RET__";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 儲存 Aligner 參數
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Save(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                
                    commandStr = "${0}{1}SET:SAVE_";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 設定 Aligner 參數值 
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Type"> 參數類別(0~9) </param>
        /// <param name="No"> 參數號碼(000~999) </param>
        /// <param name="Data"> 設定值(-99999999~+99999999) </param>
        /// <returns></returns>
        public string setParameter(string Address, string Sequence, string Type, string No, string Data)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
             
                    commandStr = "${0}{1}SET:PARAM:{2},{3},{4}";
                    commandStr = string.Format(commandStr, Address, Sequence, Type, No, Data) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 電磁閥狀態設定
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> 電磁閥號碼 </param>
        /// <param name="vl"> 狀態 </param>
        /// <returns></returns>
        public string setSolenoidValve(string Address, string Sequence, string no, string vl)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
             
                    commandStr = "${0}{1}SET:SV___:{2},{3}";
                    commandStr = string.Format(commandStr, Address, Sequence, no, vl) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 速度限制設定
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="vl"> Speed 限制，2 位數 10 進位数 </param>
        /// <returns></returns>
        public string setSpeed(string Address, string Sequence, string vl)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                    vl = Convert.ToInt16(vl).ToString();
                    if (vl.Equals("0"))
                    {
                        vl = "1";
                    }else if (vl.Equals("100"))
                    {
                        vl = "0";
                    }
                    commandStr = "${0}{1}SET:SP___:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, vl) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// IO 狀態設定
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> IO 號碼 </param>
        /// <param name="vl"> 狀態 </param>
        /// <returns></returns>
        public string setStatusIO(string Address, string Sequence, string no, string vl)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                
                    commandStr = "${0}{1}SET:RIO__:{2},{3}";
                    commandStr = string.Format(commandStr, Address, Sequence, no, vl) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 電磁閥狀態取得
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> 電磁閥號碼  (01~10) </param>
        /// <returns></returns>
        public string SolenoidValve(string Address, string Sequence, string no)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
              
                    commandStr = "${0}{1}GET:SV___:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, no) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// Speed 限制取得
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Speed(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
               
                    commandStr = "${0}{1}GET:SP___";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// Aligner狀態取得
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string Status(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                
                    commandStr = "${0}{1}GET:STS__";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

      

        /// <summary>
        /// IO 狀態取得
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="nol"> IO 號碼 </param>
        /// <returns></returns>
        public string StatusIO(string Address, string Sequence, string nol)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
               
                    commandStr = "${0}{1}GET:RIO__:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, nol) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// Step 動作等待解除
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string STPDO(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
               
                    commandStr = "${0}{1}SET:STPDO";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 保持晶圓控制
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string WaferHold(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
               
                    commandStr = "${0}{1}CMD:WHLD_:1";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 釋放晶圓控制
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string WaferReleaseHold(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
               
                    commandStr = "${0}{1}CMD:WRLS_:1";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

      

        private string KawasakiCheckSum(string Parameter)
        {
            string strCheckSum = string.Empty;
            int value = 0;
            int sum = 0;
            int remainder = 0;
            char[] charValues;

            try
            {
                charValues = Parameter.ToCharArray();

                foreach (char _eachChar in charValues)
                {
                    value = Convert.ToInt32(_eachChar);
                    sum = sum + Convert.ToInt32(_eachChar);
                }

                remainder = sum % 256;

                strCheckSum = String.Format("{0:X}", remainder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            if (strCheckSum.Length == 1)
            {
                strCheckSum = "0" + strCheckSum;
            }
            return strCheckSum;
        }
    }
}
