using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.IO;

namespace AlignerVerification.CommandConvert
{
    public class EncoderRobot
    {
        private string Supplier;


        /// <summary>
        /// Robot Encoder
        /// </summary>
        /// <param name="supplier"> 設備供應商 </param>
        /// <param name="dtCommand"> Parameter List </param>
        public EncoderRobot(string supplier)
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

        /// <summary>
        /// Abssolute Encoder Offset
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 軸，(01~16) </param>
        /// <returns></returns>
        public string AbssoluteEncoderOffset(string Address, string Sequence, string axis)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:ENCOF:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, Convert.ToInt16(axis).ToString("00")) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr;
        }

        /// <summary>
        /// Abssolute Encoder Reset
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 軸，(01~16) </param>
        /// <returns></returns>
        public string AbssoluteEncoderReset(string Address, string Sequence, string axis)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:ENCCL:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, Convert.ToInt16(axis).ToString("00")) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr;
        }

        /// <summary>
        /// 取得  R  軸到  R1  軸目前位置
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Type">  Command/Encoder  選擇 </param>
        /// <param name="Unit"> 單位選擇 </param>
        /// <returns></returns>
        public string ArmLocation(string Address, string Sequence, string Type, string Unit)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}GET:POS__:{2},{3}";
                    commandStr = string.Format(commandStr, Address, Sequence, Type, Unit) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr;
        }

        /// <summary>
        /// Battery Alarm Clear
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 軸，(01~16) </param>
        /// <returns></returns>
        public string BatteryAlarmClear(string Address, string Sequence, string axis)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:BATCL:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, Convert.ToInt16(axis).ToString("00")) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr;
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:CONT_";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr;
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:PAUSE";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr;
        }

        /// <summary>
        /// 動作停止 [ SANWA, ATEL ]
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:STOP_";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr;
        }

        /// <summary>
        /// Error 履歷取得  [ SANWA, KAWASAKI, ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> 履歷號碼  </param>
        /// <returns></returns>
        public string ErrorMessage(string Address, string Sequence, string no)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}GET:ERR__:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, Convert.ToInt16(no).ToString("00")) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// Error 解除 [ SANWA, ATEL ]
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:RESET";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取片+放片  動作指令(Exchange)    : Carry
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm_form"> Arm 選擇 ( 1~2 ) </param>
        /// <param name="Point_form"> Point 編號 </param>
        /// <param name="Slot_form"> Slot 編號 </param>
        /// <param name="Arm_to"> Arm 選擇 ( 1~2 ) </param>
        /// <param name="Point_to"> Point 編號 </param>
        /// <param name="Slot_to"> Slot 編號 </param>
        /// <returns></returns>
        public string Exchange(string Address, string Sequence, string Arm_form, string Point_form, string Slot_form, string Arm_to, string Point_to, string Slot_to)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:CARRY,{2},{3},{4},{5},{6},{7}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point_form, Slot_form, Arm_form, Point_to, Slot_to, Arm_to) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:SERVO:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, sv) + EndCode();
                    break;
                case "KAWASAKI":
                    if (sv.Equals("1"))
                    {
                        commandStr = "{0},SERV,{1}";
                        commandStr = string.Format(commandStr, Convert.ToInt16(Sequence).ToString("000"), Address);
                        commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    }
                    else
                    {
                        commandStr = "{0},STOP,{1}";
                        commandStr = string.Format(commandStr, Convert.ToInt16(Sequence).ToString("000"), Address);
                        commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取片 [ SANWA, KAWASAKI, ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇  ( 1~3) </param>
        /// <param name="Point"> Point 編號，4 位數 10 進位 </param>
        /// <param name="Alignment"> Alignment 選擇 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string GetWafer(string Address, string Sequence, string Arm, string Point, string Alignment, string Slot)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                    commandStr = "${0}{1}CMD:GET__:{2},{3},{4},{5},{6}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm, Alignment, "0") + EndCode();
                    break;
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:GET__:{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm, "0") + EndCode();
                    break;
                case "KAWASAKI":

                    commandStr = "{0},GETS,{1},{2},{3},{4}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(Arm), Point, Slot);
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取片 (SANWA)- 繼續執行 1 or 2 後續的動作 (必須前一個指令有下過 1 或是 2 的 option)
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇  ( 1~3) </param>
        /// <param name="Point"> Point 編號，4 位數 10 進位 </param>
        /// <param name="Alignment"> Alignment 選擇 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string GetWaferToContinue(string Address, string Sequence, string Arm, string Point, string Alignment, string Slot)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":

                    commandStr = "${0}{1}CMD:GET__:{2},{3},{4},{5},{6}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm, Alignment, "3") + EndCode();
                    break;
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:GET__:{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm, "3") + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},MOVP,{1},{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(Arm), Point, Slot, "GAH");
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取片 SXZ  軸到位後停止: Get Wait
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇  ( 1~3) </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string GetWaferToReady(string Address, string Sequence, string Arm, string Point, string Slot)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:GETW_:{2},{3},{4}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm) + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},MOVP,{1},{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(Arm), Point, Slot,"GBH");
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取片 (SANWA)- Z  軸移動至  Panel  下點位，Arm 伸出後就停止動作
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇  ( 1~3) </param>
        /// <param name="Point"> Point 編號，4 位數 10 進位 </param>
        /// <param name="Alignment"> Alignment 選擇 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string GetWaferToStandBy(string Address, string Sequence, string Arm, string Point, string Alignment, string Slot)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                    commandStr = "${0}{1}CMD:GET__:{2},{3},{4},{5},{6}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm, Alignment, "1") + EndCode();
                    break;
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:GET__:{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm, "1") + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},MOVP,{1},{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(Arm), Point, Slot, "GBX");
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取片 (SANWA)- Arm 伸出後，Z  軸上升至取片位置後就停止動作
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇  ( 1~3) </param>
        /// <param name="Point"> Point 編號，4 位數 10 進位 </param>
        /// <param name="Alignment"> Alignment 選擇 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string GetWaferToUp(string Address, string Sequence, string Arm, string Point, string Alignment, string Slot)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                    commandStr = "${0}{1}CMD:GET__:{2},{3},{4},{5},{6}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm, Alignment, "2") + EndCode();
                    break;
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:GET__:{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm, "2") + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},MOVP,{1},{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(Arm), Point, Slot, "GAX");
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }
        public string GetPresence(string Address, string Sequence, string Arm)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "KAWASAKI":
                    commandStr = "{0},SENS,{1},{2}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(Arm));
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }
        /// <summary>
        /// 各軸移動至 HOME 位置:Normal Home [ SANWA, KAWASAKI ]
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:HOME_";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},HOMA,{1}";
                    commandStr = string.Format(commandStr, Sequence, Address);
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:RHOME";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 各軸安全回 HOME 位置: Safety Home
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string HomeSafety(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:SHOME";
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:LOGSV";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// Mapping 結果取得
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> Data 選擇  (1~3) </param>
        /// <returns></returns>
        public string MapList(string Address, string Sequence, string no)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}GET:MAP__:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, no) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// Mapping
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Point Data 編號。(0001 ~ 1999) </param>
        /// <param name="col"> Cassette  列編號 </param>
        /// <param name="slot"> Slot  編號。 </param>
        /// <returns></returns>
        public string Mapping(string Address, string Sequence, string pno, string col, string slot)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:MAP__:{2},{3},{4}";
                    commandStr = string.Format(commandStr, Address, Sequence, pno, col, slot) + EndCode();
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:MODE_:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, vl) + EndCode();
                    break;
                case "KAWASAKI":
                    switch (vl)
                    {
                        case "0":
                            vl = "Real";
                            break;
                        case "1":
                            vl = "Simu";
                            break;
                    }
                    commandStr = "{0},SMOD,{1}";
                    commandStr = string.Format(commandStr, Sequence, vl);
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}GET:MODE_";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},RMOD";
                    commandStr = string.Format(commandStr, Sequence);
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:MOVED:{2},{3},{4}";
                    commandStr = string.Format(commandStr, Address, Sequence, axis, type, pos) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 移動到指定的位置 - only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 指定上下手臂 </param>
        /// <param name="pos"> 點位 </param>
        /// <param name="Slot"> Slot </param>
        /// <param name="LocationCode"> Location Code </param>
        /// <returns></returns>
        //public string MovePosition(string Address, string Sequence, string axis, string Pos, string Slot, string LocationCode)
        //{
        //    return CommandAssembly(Supplier, Address, Sequence, "CMD", "MovePosition", Address.ToString(), axis, Pos, Slot, LocationCode);
        //}

        /// <summary>
        /// 移動到相對位置 - only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 指定上下手臂 </param>
        /// <param name="MoveData"> 移動數值 </param>
        /// <param name="MoveMode"> 移動模式 </param>
        /// <returns></returns>
        //public string MoveRelativePosition(string Address, string Sequence, string axis, string MoveData, string MoveMode)
        //{
        //    return CommandAssembly(Supplier, Address, Sequence, "CMD", "MoveRelativePosition", Address.ToString(), axis, MoveData, MoveMode);
        //}

        /// <summary>
        /// Multi Panel  選擇命令
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="arm"> 1 或 2，R 或Ｌ軸選擇 </param>
        /// <param name="no1"> 選擇指定的 Panel </param>
        /// <param name="no2"> 選擇指定的 Panel /param>
        /// <param name="no3"> 選擇指定的 Panel /param>
        /// <param name="no4"> 選擇指定的 Panel /param>
        /// <param name="no5"> 選擇指定的 Panel /param>
        /// <param name="no6"> 選擇指定的 Panel /param>
        /// <param name="no7"> 選擇指定的 Panel /param>
        /// <param name="no8"> 選擇指定的 Panel /param>
        /// <param name="no9"> 選擇指定的 Panel /param>
        /// <param name="no10"> 選擇指定的 Panel </param>
        /// <param name="no11"> 選擇指定的 Panel </param>
        /// <param name="no12"> 選擇指定的 Panel </param>
        /// <param name="no13"> 選擇指定的 Panel </param>
        /// <param name="no14"> 選擇指定的 Panel </param>
        /// <param name="no15"> 選擇指定的 Panel </param>
        /// <param name="no16"> 選擇指定的 Panel </param>
        /// <returns></returns>
        public string MultiPanel(string Address, string Sequence, string arm, string no1, string no2, string no3, string no4, string no5, string no6, string no7, string no8, string no9, string no10, string no11, string no12, string no13, string no14, string no15, string no16)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                    commandStr = "${0}{1}CMD:MOVED:{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}";
                    commandStr = string.Format(commandStr, Address, Sequence, arm, no1, no2, no3, no4, no5, no6, no7, no8, no9, no10, no11, no12, no13, no14, no15, no16) + EndCode();
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:ORG__";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 單軸 Orgin Search [ SANWA, ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="axis"> 軸, (01 ~ 16) </param>
        /// <returns></returns>
        public string OrginSearchByAxis(string Address, string Sequence, string axis)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:EORG_:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, axis) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取得 Robot 參數值
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:PARAM:{2},{3}";
                    commandStr = string.Format(commandStr, Address, Sequence, Type, No) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 設定 Robot 指定站點參數 - only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Point"> Station Code </param>
        /// <param name="No"> Parameter Name </param>
        /// <param name="Data"> Parameter Data </param>
        /// <returns></returns>
        //public string setParameterStation(string Address, string Sequence, string Point, string No, string Data)
        //{
        //    return CommandAssembly(Supplier, Address, Sequence, "SET", "ParameterStation", Address.ToString(), Point, No, Data);
        //}

        /// <summary>
        /// 取回 Robot 指定站點參數 - only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Point"> Station Code </param>
        /// <param name="No"> Parameter Name </param>
        /// <returns></returns>
        //public string ParameterStation(string Address, string Sequence, string Point, string No)
        //{
        //    return CommandAssembly(Supplier, Address, Sequence, "GET", "ParameterStationGet", Address.ToString(), Point, No);
        //}

        /// <summary>
        /// 取得 Robot 參數詳細資料
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}GET:PARSY:{2},{3}";
                    commandStr = string.Format(commandStr, Address, Sequence, Type, No) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 將 Point Data  從 CF 卡載入
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string PointLoad(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:LOADP";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 將 Point Data  存入  CF 卡
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        public string PointSave(string Address, string Sequence)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:SAVEP";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取回各軸位置 only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <returns></returns>
        //public string CurrentPosition(string Address, string Sequence)
        //{
        //    return CommandAssembly(Supplier, Address, Sequence, "GET", "CurrentPosition", Address.ToString());
        //}

        /// <summary>
        /// 取回座標位置 only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> 手臂 </param>
        /// <returns></returns>
        //public string CurrentCoordinatePosition(string Address, string Sequence, string Arm)
        //{
        //    return CommandAssembly(Supplier, Address, Sequence, "GET", "CurrentCoordinatePosition", Address.ToString(), Arm);
        //}

        /// <summary>
        /// 取回當前機器人最近的位置和Slot only Kawasaki
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> 手臂 </param>
        /// <returns></returns>
        //public string NearestStation(string Address, string Sequence, string Arm)
        //{
        //    return CommandAssembly(Supplier, Address, Sequence, "GET", "NearestStation", Address.ToString(), Arm);
        //}

        /// <summary>
        /// 放片 [ SANWA, KAWASAKI, ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇 </param>
        /// <param name="Point"> Point 編號 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string PutWafer(string Address, string Sequence, string Arm, string Point, string Slot)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:PUT__:{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm, "0") + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},PUTS,{1},{2},{3},{4}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(Arm), Point, Slot);
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 放片 (SANWA) - 繼續執行 1 or 2  後續的動作 (必須前一個指令有下過  1  或是  2 的 option)
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇 </param>
        /// <param name="Point"> Point 編號 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string PutWaferToContinue(string Address, string Sequence, string Arm, string Point, string Slot)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:PUT__:{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm, "3") + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},MOVP,{1},{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(Arm), Point, Slot, "PAH");
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 放片 (SANWA) - Z 中間位置停止(Teaching 位置  + Z 軸下降設定距離)
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇 </param>
        /// <param name="Point"> Point 編號 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string PutWaferToDown(string Address, string Sequence, string Arm, string Point, string Slot)
        {

            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:PUT__:{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm, "2") + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},MOVP,{1},{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(Arm), Point, Slot, "PAX");
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取片 SXZ  軸到位後停止: Get Wait
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇  ( 1~2 ) </param>
        /// <param name="Point"> Point 編號 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string PutWaferToReady(string Address, string Sequence, string Arm, string Point, string Slot)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:PUTW_:{2},{3},{4}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm) + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},MOVP,{1},{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(Arm), Point, Slot, "PBH");
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 放片 (SANWA) - Z  軸移動至  Panel  上點位，Arm 伸出後就停止動作
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Arm"> Arm  選擇 </param>
        /// <param name="Point"> Point 編號 </param>
        /// <param name="Slot"> Slot，3 位數 10 進位 </param>
        /// <returns></returns>
        public string PutWaferToStandBy(string Address, string Sequence, string Arm, string Point, string Slot)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:PUT__:{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Address, Sequence, Point, Slot, Arm, "1") + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},MOVP,{1},{2},{3},{4},{5}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(Arm), Point, Slot, "PBX");
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:RET__";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},HOMH,{1}";
                    commandStr = string.Format(commandStr, Sequence, Address);
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 儲存 Robot 參數
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:SAVE_";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 設定 Robot 參數值 
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="Type"> 參數類別(0~9) </param>
        /// <param name="No"> 參數號碼(000~999), to Kawasaki = ParameterName </param>
        /// <param name="Data"> 設定值(-99999999~+99999999), to Kawasaki = ParameterData </param>
        /// <returns></returns>
        public string setParameter(string Address, string Sequence, string Type, string No, string Data)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:PARAM:{2},{3},{4}";
                    commandStr = string.Format(commandStr, Address, Sequence, Type, No, Data) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 設定指定點位欄位的資訊
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Teach  點位(1~1999) </param>
        /// <param name="no"> 參數號碼(000~124) </param>
        /// <param name="data"> 參數值(-99999999~+99999999) </param>
        /// <returns></returns>
        public string setSettingPointParameter(string Address, string Sequence, string pno, string no, string data)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:PDATA:{2},{3},{4}";
                    commandStr = string.Format(commandStr, Address, Sequence, pno, no, data) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 電磁閥狀態設定 [ SANWA, ATEL ]
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:SV___:{2},{3}";
                    commandStr = string.Format(commandStr, Address, Sequence, no, vl) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 速度限制設定 [ SANWA, KAWASAKI, ATEL ]
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
                case "ATEL_NEW":
                    vl = Convert.ToInt16(vl).ToString();
                    if (vl.Equals("0"))
                    {
                        vl = "1";
                    }
                    else if (vl.Equals("100"))
                    {
                        vl = "0";
                    }
                    commandStr = "${0}{1}SET:SP___:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, vl) + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},SSPD,{1},{2}";
                    commandStr = string.Format(commandStr, Sequence, Address, vl);
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 各軸速度限制設定 [ ATEL ]
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="vl"> Speed 限制，2 位數 10 進位数 </param>
        /// <returns></returns>
        public string setSpeed(string Address, string Sequence, string axis, string vl)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:SP___:{2},{3}";
                    commandStr = string.Format(commandStr, Address, Sequence, axis, vl) + EndCode();
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:RIO__:{2},{3}";
                    commandStr = string.Format(commandStr, Address, Sequence, no, vl) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }
        /// <summary>
        /// 將目前各軸的位置寫入指定的 Point Data
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Sanwa : Teaching  點位(0001 ~ 1999)  Kawasaki (P1~P15) </param>
        /// <param name="arm"> Kawasaki 用 </param>
        /// <param name="slot"> Kawasaki 用 </param>
        /// <returns></returns>
        public string setTeachPoint(string Address, string Sequence, string pno)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:TEACH:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, pno) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }



        /// <summary>
        /// 取得指定點位欄位的資訊
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Teach  點位(1~1999) </param>
        /// <param name="no"> 參數號碼(000~124) </param>
        /// <returns></returns>
        public string SettingPointParameter(string Address, string Sequence, string pno, string no)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}GET:PDATA:{2},{3}";
                    commandStr = string.Format(commandStr, Address, Sequence, pno, no) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取得 Mapping  的厚度結果
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="no"> Data 選擇  (1~3)  </param>
        /// <returns></returns>
        public string SlotThickness(string Address, string Sequence, string no)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}GET:MAPT_:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, no) + EndCode();
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}GET:SV___:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, no) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// Speed 限制取得 [ SANWA, KAWASAKI ]
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}GET:SP___";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},RSPD,{1}";
                    commandStr = string.Format(commandStr, Sequence, Address);
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }



        /// <summary>
        /// Robot 狀態取得 [ SANWA, KAWASAKI, ATEL ]
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}GET:STS__";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},CSTA,{1}";
                    commandStr = string.Format(commandStr, Sequence, Address);
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
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
                case "ATEL_NEW":
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
                case "ATEL_NEW":
                    commandStr = "${0}{1}SET:STPDO";
                    commandStr = string.Format(commandStr, Address, Sequence) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 讀取  Point Data  裡的各軸位置(R~R1  六軸)
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="pno"> Teach  點位  </param>
        /// <returns></returns>
        public string TeachPoint(string Address, string Sequence, string pno)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}GET:TEACH:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, pno) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// Panel(Wafer)保持  : Panel(Wafer) Hold
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="arm"> Arm 選擇  ( 1~2 ) </param>
        /// <returns></returns>
        public string WaferHold(string Address, string Sequence, string arm)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:WHLD_:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, arm) + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},HOLD,{1},{2}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(arm));
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// Panel(Wafer)  解除  : Panel(Wafer) Release
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="arm"> Arm 選擇  ( 1~2 ) </param>
        /// <returns></returns>
        public string WaferReleaseHold(string Address, string Sequence, string arm)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}CMD:WRLS_:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, arm) + EndCode();
                    break;
                case "KAWASAKI":
                    commandStr = "{0},RELS,{1},{2}";
                    commandStr = string.Format(commandStr, Sequence, Address, KawasakiArm(arm));
                    commandStr = "<" + commandStr + ">" + KawasakiCheckSum(commandStr) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }

        /// <summary>
        /// 取得  Panel(Wafer)  的狀態 
        /// </summary>
        /// <param name="Address"> Equipment Address </param>
        /// <param name="Sequence"> Euuipment Sequence </param>
        /// <param name="arm"> Sanwa 用判 Arm  選擇 </param>
        /// <param name="pno"> Kawasaki 用 需輸入點位 </param>
        /// <returns></returns>
        public string WaferStatus(string Address, string Sequence, string arm)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "SANWA":
                case "ATEL_NEW":
                    commandStr = "${0}{1}GET:PNSTS:{2}";
                    commandStr = string.Format(commandStr, Address, Sequence, arm) + EndCode();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr;
        }


        public string KawasakiArm(string Arm)
        {
            string result = "";
            switch (Arm)
            {
                case "1":
                    result = "H2";
                    break;
                case "2":
                    result = "H1";
                    break;
                case "3":
                    result = "HA";
                    break;
            }
            return result;
        }
        public string KawasakiCheckSum(string Parameter)
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