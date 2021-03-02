using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace AlignerVerification.CommandConvert
{
    public class CommandEncoder
    {
        public EncoderAligner Aligner;
        public EncoderRobot Robot;
        public EncoderOCR OCR;
        public EncoderLoadPort LoadPort;
        public Encoder_SmartTag SmartTag;

        private string Supplier;
     


        /// <summary>
        /// Encoder
        /// </summary>
        /// <param name="supplier"> Equipment supplier </param>
        public CommandEncoder(string supplier)
        {
     
          
            try
            {
                Supplier = supplier.ToUpper();

                Aligner = new EncoderAligner(Supplier);
                Robot = new EncoderRobot(Supplier);
                OCR = new EncoderOCR(Supplier);
                LoadPort = new EncoderLoadPort(Supplier, EncoderLoadPort.CommandMode.TDK_A);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
             
            }
        }

       
    }
}
