using System;
using System.Collections.Generic;
using System.Text;

namespace AlignerVerification.CommandConvert
{
    public class Encoder_SmartTag
    {
        private string Supplier;
        

        /// <summary>
        /// Aligner Encoder
        /// </summary>
        /// <param name="supplier"> 設備供應商 </param>
        /// <param name="dtCommand"> Parameter List </param>
        public Encoder_SmartTag(string supplier)
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

        public string Hello()
        {
            return "FC FF B5 FF";
        }

        public string GetLCDData()
        {
            return "FF FF EF FF F7 FF FF 07 D0 FF 7F DB FF 7F FF";
        }

        public string SelectLCDData()
        {
            return "FF FF 1F FF 77 FF FF 07 B0 FF BB FF 7F FF";
        }

        public string SetLCDData(string Data)
        {
            Data += "                ";
            return "FF FF D0 FF F5 FF "+ parseWriteData(Data) + CalculateWriteChecksum(Data);
        }

        private string parseWriteData(string text)
        {
            int ttl_len = 240;
            int start_idx = 240 - text.Length;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < ttl_len; i++)
            {
                if (i < start_idx)
                {
                    result.Append("FF ");
                }
                else
                {
                    byte temp = Encoding.ASCII.GetBytes(text.Substring(i - start_idx, 1))[0];
                    string char_0 = temp.ToString("X2");
                    string char_1 = getWriteMappingChar(char_0.Substring(0, 1));
                    string char_2 = getWriteMappingChar(char_0.Substring(1, 1));
                    result.Append(char_2 + char_1 + " ");
                }
            }
            return result.ToString();
        }

        private string getWriteMappingChar(string tag)
        {
            Dictionary<string, string> charMap = new Dictionary<string, string>();
            charMap.Add("0", "F");
            charMap.Add("1", "7");
            charMap.Add("2", "B");
            charMap.Add("3", "3");
            charMap.Add("4", "D");
            charMap.Add("5", "5");
            charMap.Add("6", "9");
            charMap.Add("7", "1");
            charMap.Add("8", "E");
            charMap.Add("9", "6");
            charMap.Add("A", "A");
            charMap.Add("B", "2");
            charMap.Add("C", "C");
            charMap.Add("D", "4");
            charMap.Add("E", "8");
            charMap.Add("F", "0");
            return charMap[tag];
        }

        private string CalculateWriteChecksum(string dataToCalculate)
        {
            byte[] byteToCalculate = Encoding.ASCII.GetBytes(dataToCalculate);
            int checksum = 0;
            byte[] bdata = { 0x50 };
            foreach (byte b in bdata)
            {
                checksum += b;
            }
            foreach (byte chData in byteToCalculate)
            {
                checksum += chData;
            }
            //checksum &= 0xff;
            string temp = checksum.ToString("X4");
            string char1 = getWriteMappingChar(temp.Substring(0, 1));
            string char2 = getWriteMappingChar(temp.Substring(1, 1));
            string char3 = getWriteMappingChar(temp.Substring(2, 1));
            string char4 = getWriteMappingChar(temp.Substring(3, 1));
            string result = "FF " + char4 + char3 + " FF " + char2 + char1 + " FF ";
            return result;
        }

    }
}
