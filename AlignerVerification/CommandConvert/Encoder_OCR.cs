using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace AlignerVerification.CommandConvert
{
    public class EncoderOCR
    {
        private string Supplier;
       

        /// <summary>
        /// OCR Encoder
        /// </summary>
        /// <param name="supplier"> 設備供應商 </param>
        public EncoderOCR(string supplier)
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

        public enum OnlineStatus
        {
            Offline,
            Online
        }

        public string Read()
        {
            return CommandAssembly("CMD", "Read", "-1");
        }

        public string SetOnline(OnlineStatus online)
        {
            return CommandAssembly("CMD", "OnlineStatus", ((int)online).ToString());
        }

        public string GetOnline()
        {
            return CommandAssembly("CMD", "GetOnline");
        }

        private string CommandAssembly(string CommandType, string Command, params string[] Parameter)
        {
            string strCommand = string.Empty;
            string strCommandFormat = string.Empty;
            string strCommandFormatParameter = string.Empty;



            try
            {

               

                //var query = (from a in dtRobotCommand.AsEnumerable()
                //             where a.Field<string>("Equipment_Type") == "ORC"
                //                && a.Field<string>("Equipment_Supplier") == Supplier
                //                && a.Field<string>("Command_Type") == CommandType
                //                && a.Field<string>("Action_Function") == Command
                //             select a).ToList();

                //if (query.Count == 0)
                //{
                //    throw new RowNotInTableException();
                //}

                //dtTemp = query.CopyToDataTable();
                //dtTemp.DefaultView.Sort = "Parameter_Order ASC";
                //dvTemp = dtTemp.DefaultView;

                switch (Supplier)
                {
                    case "COGNEX":

                        if (Command.Equals("Read"))
                        {
                            strCommandFormat = string.Format("READ({0})", Parameter) + Environment.NewLine;
                        }
                        else
                        {
                            throw new NotSupportedException();
                        }

                        break;

                    case "HST":

                        if (Command.Equals("Read"))
                        {
                            strCommandFormat = string.Format("{0}{1}{2}{3}{4}", "SM", ((char)34).ToString(), "READ", ((char)34).ToString(), "0");
                        }
                        else if (Command.Equals("OnlineStatus"))
                        {
                            strCommandFormat = string.Format("SO{0}", Parameter);
                        }
                        else if (Command.Equals("GetOnline"))
                        {
                            strCommandFormat = "GO";
                        }
                        else
                        {
                            throw new NotSupportedException();
                        }

                        break;
                    default:
                        throw new NotImplementedException();
                }

                strCommand = strCommandFormat + strCommandFormatParameter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strCommand;
        }

    }
}
