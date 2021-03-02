using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace AlignerVerification.CommandConvert
{
    public class EncoderLoadPort
    {
        private string Supplier;

        private CommandMode cmdMode;

        /// <summary>
        /// Indicator Type
        /// </summary>
        public enum IndicatorType
        {
            Load,
            Unload,
            OpAccess,
            Presence,
            Placement,
            Status01,
            Status02
        }

        /// <summary>
        /// Indicator Status
        /// </summary>
        public enum IndicatorStatus
        {
            ON,
            OFF,
            Flashes
        }

        /// <summary>
        /// Load Port Runing Mode [TDK, ASYST]
        /// </summary>
        public enum ModeType
        {
            Online,
            Maintenance,
            Auto,
            Manual
        }

        /// <summary>
        /// Load Port Parameter state [TDK, ASYST]
        /// </summary>
        public enum ParamState
        {
            Enable,
            Disable
        }

        public enum CassrtteSize
        {
            Cassette_8_Inch = 0,
            Cassette_4_Or_6_Inch = 2,
            Disable_SlotSensor_INX2200 = 4,
            Disable_SlotSensor_INX2150 = 5
        }

        public enum TweekType
        {
            TweekDown,
            TweekUp
        }
        /// <summary>
        /// Wafer Sorting Type
        /// </summary>
        public enum MappingSortingType
        {
            Asc,
            Desc
        }

        /// <summary>
        /// Command Type [TDK, ASYST]
        /// </summary>
        public enum CommandType
        {
            Normal,
            Finish
        }

        public enum EventType
        {
            Complete,
            All
        }

        /// <summary>
        /// Command Type
        /// </summary>
        public enum CommandMode
        {
            TDK_A,
            TDK_B
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
                case "ASYST":
                case "KAWASAKI":
                    result = "\r\n";
                    break;
            }
            return result;
        }
        /// <summary>
        /// Load Port Encoder
        /// </summary>
        /// <param name="supplier"> 設備供應商 </param>
        public EncoderLoadPort(string supplier, CommandMode commandMode)
        {
            try
            {
                Supplier = supplier;

                cmdMode = commandMode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Reset [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Reset(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":

                    commandStr = "SET:RESET;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                case "ASYST":
                    commandStr = "HCS RESET";
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr + EndCode();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Initialization(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "SET:INITL;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr + EndCode();
        }

        /// <summary>
        /// Move to slot number
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <param name="Slot">Move to slot number</param>
        /// <returns></returns>
        public string Slot(CommandType commandType, string Slot)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "ASYST":
                    commandStr = "HCS SLOT";
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr + EndCode();
        }

        public string SetEvent(CommandType commandType, EventType evtType, ParamState state)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "ASYST":
                    switch (evtType)
                    {
                        case EventType.All:

                            commandStr = "EDER";
                            if (state == ParamState.Enable)
                            {
                                commandStr += " ON";
                            }
                            else if (state == ParamState.Disable)
                            {
                                commandStr += " OFF";
                            }
                            break;
                        case EventType.Complete:
                            commandStr = "ECS";
                            commandStr += " P38=4";
                            break;
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr + EndCode();

        }

        public string SetCassetteSizeOption(string size)
        {
            string Command = string.Empty;
            string param = string.Empty;
            try
            {
                switch (Supplier)
                {
                    case "ASYST":
                        Command = "ECS";

                        param = "P39=" + size;

                        break;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return Command + " " + param + "\r\n";
        }

        public string GetCassetteSizeOption()
        {
            string Command = string.Empty;
            string param = string.Empty;
            try
            {
                switch (Supplier)
                {
                    case "ASYST":
                        Command = "ECR";

                        param = "P39";

                        break;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return Command + " " + param + "\r\n";
        }

        public string Tweek(TweekType type)
        {
            string Command = string.Empty;
            string param = string.Empty;
            try
            {
                switch (Supplier)
                {
                    case "ASYST":
                        Command = "HCS";
                        switch (type)
                        {

                            case TweekType.TweekDown:

                                param = "TWEEKDN";
                                break;
                            case TweekType.TweekUp:

                                param = "TWEEKUP";
                                break;
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return Command + " " + param + "\r\n";
        }

        public string SetSlotOffset(string offset)
        {
            string Command = string.Empty;
            string parm = string.Empty;
            try
            {
                switch (Supplier)
                {
                    case "ASYST":
                        Command = "ECS";
                        parm = "P30=" + offset;

                        break;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return Command + " " + parm + "\r\n";
        }

        public string GetSlotOffset()
        {
            string Command = string.Empty;
            string parm = string.Empty;
            try
            {
                switch (Supplier)
                {
                    case "ASYST":
                        Command = "ECR";
                        parm = "P30";

                        break;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return Command + " " + parm + "\r\n";
        }

        public string SetWaferOffset(string offset)
        {
            string Command = string.Empty;
            string parm = string.Empty;
            try
            {
                switch (Supplier)
                {
                    case "ASYST":
                        Command = "ECS";
                        parm = "P31=" + offset;

                        break;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return Command + " " + parm + "\r\n";
        }

        public string GetWaferOffset()
        {
            string Command = string.Empty;
            string parm = string.Empty;
            try
            {
                switch (Supplier)
                {
                    case "ASYST":
                        Command = "ECR";
                        parm = "P31";

                        break;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return Command + " " + parm + "\r\n";
        }

        public string SetSlotPitch(string pitch)
        {
            string Command = string.Empty;
            string parm = string.Empty;
            try
            {
                switch (Supplier)
                {
                    case "ASYST":
                        Command = "ECS";
                        parm = "P35=" + pitch;

                        break;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return Command + " " + parm + "\r\n";
        }

        public string GetSlotPitch()
        {
            string Command = string.Empty;
            string parm = string.Empty;
            try
            {
                switch (Supplier)
                {
                    case "ASYST":
                        Command = "ECR";
                        parm = "P35";

                        break;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return Command + " " + parm + "\r\n";
        }

        public string SetTweekDistance(string distance)
        {
            string Command = string.Empty;
            string parm = string.Empty;
            try
            {
                switch (Supplier)
                {
                    case "ASYST":
                        Command = "ECS";
                        parm = "P36=" + distance;

                        break;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return Command + " " + parm + "\r\n";
        }

        public string GetTweekDistance()
        {
            string Command = string.Empty;
            string parm = string.Empty;
            try
            {
                switch (Supplier)
                {
                    case "ASYST":
                        Command = "ECR";
                        parm = "P36";

                        break;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return Command + " " + parm + "\r\n";
        }

        /// <summary>
        /// Status Indicator
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <param name="indicatorType"> Indicator Type </param>
        /// <param name="indicatorStatus"> Indicator Status</param>
        /// <returns></returns>
        public string Indicator(CommandType commandType, IndicatorType indicatorType, IndicatorStatus indicatorStatus)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    switch (indicatorType)
                    {
                        case IndicatorType.Load:
                            switch (indicatorStatus)
                            {
                                case IndicatorStatus.ON:
                                    commandStr = "SET:LON01;";
                                    break;
                                case IndicatorStatus.OFF:
                                    commandStr = "SET:LOF01;";
                                    break;
                                case IndicatorStatus.Flashes:
                                    commandStr = "SET:LBL01;";
                                    break;
                            }
                            break;
                        case IndicatorType.Unload:
                            switch (indicatorStatus)
                            {
                                case IndicatorStatus.ON:
                                    commandStr = "SET:LON02;";
                                    break;
                                case IndicatorStatus.OFF:
                                    commandStr = "SET:LOF02;";
                                    break;
                                case IndicatorStatus.Flashes:
                                    commandStr = "SET:LBL02;";
                                    break;
                            }
                            break;
                        case IndicatorType.OpAccess:
                            switch (indicatorStatus)
                            {
                                case IndicatorStatus.ON:
                                    commandStr = "SET:LON03;";
                                    break;
                                case IndicatorStatus.OFF:
                                    commandStr = "SET:LOF03;";
                                    break;
                                case IndicatorStatus.Flashes:
                                    commandStr = "SET:LBL03;";
                                    break;
                            }
                            break;
                        case IndicatorType.Presence:
                            switch (indicatorStatus)
                            {
                                case IndicatorStatus.ON:
                                    commandStr = "SET:LON04;";
                                    break;
                                case IndicatorStatus.OFF:
                                    commandStr = "SET:LOF04;";
                                    break;
                                case IndicatorStatus.Flashes:
                                    commandStr = "SET:LBL04;";
                                    break;
                            }
                            break;
                        case IndicatorType.Placement:
                            switch (indicatorStatus)
                            {
                                case IndicatorStatus.ON:
                                    commandStr = "SET:LON05;";
                                    break;
                                case IndicatorStatus.OFF:
                                    commandStr = "SET:LOF05;";
                                    break;
                                case IndicatorStatus.Flashes:
                                    commandStr = "SET:LBL05;";
                                    break;
                            }
                            break;
                        case IndicatorType.Status01:
                            switch (indicatorStatus)
                            {
                                case IndicatorStatus.ON:
                                    commandStr = "SET:LON07;";
                                    break;
                                case IndicatorStatus.OFF:
                                    commandStr = "SET:LOF07;";
                                    break;
                                case IndicatorStatus.Flashes:
                                    commandStr = "SET:LBL07;";
                                    break;
                            }
                            break;
                        case IndicatorType.Status02:
                            switch (indicatorStatus)
                            {
                                case IndicatorStatus.ON:
                                    commandStr = "SET:LON08;";
                                    break;
                                case IndicatorStatus.OFF:
                                    commandStr = "SET:LOF08;";
                                    break;
                                case IndicatorStatus.Flashes:
                                    commandStr = "SET:LBL08;";
                                    break;
                            }
                            break;
                    }
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr + EndCode();
        }

        /// <summary>
        /// Load Port Runing Mode [TDK, ASYST]
        /// </summary>
        /// <param name="modeType"> Runing Mode Type </param>
        /// <returns></returns>
        public string Mode(ModeType modeType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    switch (modeType)
                    {
                        case ModeType.Online:
                            commandStr = "MOD:ONMGV;";

                            break;
                        case ModeType.Maintenance:
                            commandStr = "MOD:MENTE;";
                            break;
                    }
                    commandStr = TDKAssembly(commandStr);
                    break;
                case "ASYST":
                    switch (modeType)
                    {
                        case ModeType.Auto:
                            commandStr = "HCS AUTO";

                            break;
                        case ModeType.Manual:
                            commandStr = "HCS MANUAL";
                            break;
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Status [TDK, ASYST]
        /// </summary>
        /// <returns></returns>
        public string Status()
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "GET:STATE;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                case "ASYST":
                    commandStr = "FSR FC=0";
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Version
        /// </summary>
        /// <returns></returns>
        public string Version()
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "GET:VERSN;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// LED Indicator Status
        /// </summary>
        /// <returns></returns>
        public string LEDIndicatorStatus()
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "GET:LEDST;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Wafer Sorting [TDK, ASYST]
        /// </summary>
        /// <param name="sortingType"> Sorting Type</param>
        /// <returns></returns>
        public string WaferSorting(MappingSortingType sortingType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    switch (sortingType)
                    {
                        case MappingSortingType.Asc:
                            commandStr = "GET:MAPRD;";
                            break;
                        case MappingSortingType.Desc:
                            commandStr = "GET:MAPDT;";
                            break;
                    }                   
                    commandStr = TDKAssembly(commandStr);
                    break;
                case "ASYST":
                    commandStr = "FSR FC=2";
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Wafer Quantity
        /// </summary>
        /// <returns></returns>
        public string WaferQuantity()
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "GET:WFCNT;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// FOUP Initial Position [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string InitialPosition(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:ORGSH;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                case "ASYST":
                    commandStr = "HCS CALIB";
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();            
        }

        /// <summary>
        /// FOUP Forced Initial Position
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string ForcedInitialPosition(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:ABORG;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Load FOUP [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Load(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CLOAD;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                case "ASYST":
                    commandStr = "HCS STAGE";
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Docking Position
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DockingPosition(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CLDDK;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Docking Position (No Vac)
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DockingPositionNoVac(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CLDYD;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Docking Position After Load
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DockingPositionAfterLoad(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CLDOP;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

     
        /// <summary>
        /// Docking Position After Mapping
        /// </summary>
        ///         /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DockingPositionAfterMapping(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CLMPO;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Unload FOUP [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Unload(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CULOD;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                case "ASYST":
                    commandStr = "HCS HOME";
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Until Door Close
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string UntilDoorClose(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CULDK;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Door Close After Undock
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DoorCloseAfterUndock(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CUDCL;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Door Close After Unload
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DoorCloseAfterUnload(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CUDNC;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Until Door Close Vac OFF
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string UntilDoorCloseVacOFF(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CULYD;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Until Undock
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string UntilUndock(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CULFC;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Map & Unload
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapAndUnload(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CUDMP;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Map & until door close
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapAndUntilDoorClose(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CUMDK;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Map & until undock
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapAndUntilUndock(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CUMFC;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }
        /// <summary>
        /// Mapping Load  [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MappingLoad(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:CLDMP;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                case "ASYST":
                    commandStr = "HCS STAGE";
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Mapping in Load
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MappingInLoad(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:MAPDO;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Retry Mapping
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string RetryMapping(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:REMAP;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  FOUP Clamp Release [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string FOUPClampRelease(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:PODOP;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                case "ASYST":
                    commandStr = "HCS UNLK";
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  FOUP Clamp Fix [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string FOUPClampFix(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:PODCL;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                case "ASYST":
                    commandStr = "HCS LOCK";
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Load Port Undock
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Undock(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:YWAIT;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Load Port Dock
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Dock(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:YDOOR;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Load Port Vacuum OFF
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string VacuumOFF(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:VACOF;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Load Port Vacuum ON
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string VacuumON(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:VACON;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Latch key Fix
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string LatchkeyFix(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:DORCL;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Latch key Release
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string LatchkeyRelease(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:DOROP;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Door Close
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DoorClose(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:DORFW;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Door Open
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DoorOpen(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:DORBK;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Door Up
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DoorUp(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:ZDRUP;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Door Down
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string DoorDown(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:ZDRDW;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Mapper Stopper ON
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapperStopperON(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:MSTON;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Mapper Stopper OFF
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapperStopperOFF(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:MSTOF;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Mapper Wait Position
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapperWaitPosition(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:ZMPED;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Mapper Start Position
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapperStartPosition(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:ZMPST;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Mapper Arm Close
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapperArmClose(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:MAPCL;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Mapper Arm Open
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MapperArmOpen(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:MAPOP;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Mapping Down
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string MappingDown(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:ZDRMP;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Retry
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Retry(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:RETRY;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Stop
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Stop(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:STOP_;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                case "ASYST":
                    commandStr = "HCS STOP";
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Pause
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Pause(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:PAUSE;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Abort [TDK, ASYST]
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Abort(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:ABORT;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                case "ASYST":
                    commandStr = "HCS STOP";
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        /// Load Port Advancing box sensing pads sensor [TDK, ASYST]
        /// </summary>
        /// <param name="modeType"> Runing Mode Type </param>
        /// <returns></returns>
        public string EQASP(ParamState state)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    switch (state)
                    {
                        case ParamState.Enable:
                            commandStr = "PRM:eqasp/ON;";
                            break;
                        case ParamState.Disable:
                            commandStr = "PRM:eqasp/OFF;";
                            break;
                    }                   
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }

        /// <summary>
        ///  Resum
        /// </summary>
        /// <param name="commandType"> Command Type </param>
        /// <returns></returns>
        public string Resum(CommandType commandType)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "TDK":
                    commandStr = "MOV:RESUM;";
                    commandStr = TDKAssembly(commandStr);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return commandStr + EndCode();
        }


        public string TDKAssembly(string Command)
        {
            string strCommandFormat = "";
            switch (cmdMode)
            {
                case CommandMode.TDK_A:
                    strCommandFormat = TDK_A(Command);
                    break;
                case CommandMode.TDK_B:
                    strCommandFormat = TDK_B(Command);
                    break;
            }
            return strCommandFormat;
        }

        public string TDK_A(string Command)
        {
            string strCommsnd = string.Empty;
            string strLen = string.Empty;
            string sCheckSum = string.Empty;
            int chrLH = 0;
            int chrLL = 0;

            try
            {
                strLen = Convert.ToString(Command.Length + 4, 16).PadLeft(2, '0');

                chrLH = 0;
                chrLL = Convert.ToInt32(strLen, 16);
                strLen = Convert.ToChar(chrLH).ToString() + Convert.ToChar(chrLL).ToString();
                sCheckSum = ProcCheckSum(strLen, Command);
                strCommsnd = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", Convert.ToChar(1), strLen, Convert.ToChar(48), string.Empty, Convert.ToChar(48), Command, sCheckSum, Convert.ToChar(3));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strCommsnd;
        }

        public string TDK_B(string Command)
        {
            string strCommsnd = string.Empty;
            string strLen = string.Empty;

            try
            {
                strLen = Convert.ToString(Command.Length + 4, 16).PadLeft(2, '0');
                strCommsnd = string.Format("{0}{1}{2}{3}", Convert.ToChar("s"), strLen, Command, Convert.ToChar(13));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strCommsnd;
        }

        public string ProcCheckSum(string Len, string Message)
        {
            string strCheckSum = string.Empty;
            string csHex = string.Empty;

            try
            {
                strCheckSum = string.Format("{0}{1}{2}{3}{4}", Len, Convert.ToChar(48), string.Empty, Convert.ToChar(48), Message.ToString());

                byte[] t = new byte[Encoding.ASCII.GetByteCount(strCheckSum)]; ;
                int ttt = Encoding.ASCII.GetBytes(strCheckSum, 0, Encoding.ASCII.GetByteCount(strCheckSum), t, 0);
                byte tt = 0;

                for (int i = 0; i < t.Length; i++)
                {
                    tt += t[i];
                }

                csHex = tt.ToString("X");
                if (csHex.Length == 1)
                {
                    csHex = "0" + csHex;
                }

                return csHex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
