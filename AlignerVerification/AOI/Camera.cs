using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;

namespace AlignerVerification.AOI
{
    public class Camera
    {
        public Mat MatFrame;
        public ImageBuffer FileBuffer;
        public ImageBuffer ImageBuffer;
        public Tool AOITool;

        public void SetImage(Mat mat)
        {
            MatFrame = mat;
            AOITool.SetImage(MatFrame);
        }
    }
}
