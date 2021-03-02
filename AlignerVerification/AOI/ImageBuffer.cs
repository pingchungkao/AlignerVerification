using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Emgu.CV;
using Emgu.CV.Cvb;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;



namespace AlignerVerification.AOI
{
    public class ImageInfo
    {
        public Mat MatImage = null;
        public string FileName = "";
        public int ID = 0;
    }
    public class ImageBuffer
    {
        //最大儲存上限
        public int BufferMaxCount = 100;
        private int ImageIndex = 0;

        /// <summary>
        /// 目前容量(上限等同於BufferMaxCount)
        /// </summary>
        public int ImageCount = 0;
        private List<ImageInfo> StoreImageInfo = new List<ImageInfo>();
        /// <summary>
        /// Review Index
        /// </summary>
        private int RunIndex = 0;


        public ImageBuffer()
        {
            SetBufferInfo(100, 480, 640, DepthType.Cv8U, 3);
        }
        public void SetBufferInfo(int count, int imagewidth, int imageheight, DepthType type, int channel)
        {
            StoreImageInfo.Clear();

            for(int i = 0; i< count; i++)
            {
                ImageInfo info = new ImageInfo
                {
                    ID = i
                };

                info.MatImage = new Mat(imageheight, imagewidth , type , channel);

                StoreImageInfo.Add(info);
            }

            BufferMaxCount = count;
            ImageIndex = 0;
            RunIndex = 0;
        }
        public void AddImage(Mat image)
        {
            AddImage(image, ImageIndex.ToString());
        }

        public void AddImage(Mat image, string filename)
        {
            ImageInfo info = StoreImageInfo[ImageIndex];

            info.MatImage = image.Clone();
            info.FileName = filename;

            ImageIndex++;
            if (ImageIndex >= BufferMaxCount) ImageIndex = 0;

            ImageCount++;
            if (ImageCount >= BufferMaxCount) ImageCount = BufferMaxCount;

            RunIndex = 0;
        }

        public ImageInfo GetImageInfo(int index)
        {
            ImageInfo info = null;
            if (ImageCount == 0) return info;

            if (index >= BufferMaxCount) return info;
            if (index >= ImageCount) return info;

            info = StoreImageInfo[index];

            RunIndex = index;

            return info;
        }

        public ImageInfo GetNextImageInfo()
        {
            ImageInfo info = null;
            if (ImageCount == 0) return info;

            int index = RunIndex+1;
            if (index >= BufferMaxCount) index = 0;
            if (index >= ImageCount) index = 0;

            info = StoreImageInfo[index];

            RunIndex = index;

            return info;
        }

        public ImageInfo GetPreviousImageInfo()
        {
            ImageInfo info = null;
            if (ImageCount == 0) return info;

            int index = RunIndex-1;
            if (index < 0) index = ImageCount -1;

            if(index < 0) return info;

            info = StoreImageInfo[index];

            RunIndex = index;

            return info;
        }

        public void Reset()
        {
            foreach(ImageInfo info in StoreImageInfo)
            {
                info.FileName = "";
            }

            ImageIndex = 0;
            RunIndex = 0;
            ImageCount = 0;
        }

        public void SaveImage(string strFolder)
        {
            if (!Directory.Exists(strFolder))
                Directory.CreateDirectory(strFolder);

            string strImageFile = strFolder + @"\Image\";

            if (!Directory.Exists(strImageFile))
                Directory.CreateDirectory(strImageFile);

            ImageInfo info = null;
            for (int i = 0; i< ImageCount; i++)
            {
                info = StoreImageInfo[i];
                info.MatImage.Save(strImageFile + i.ToString().PadLeft(3, '0') + @".bmp");
            }
        }
    }
}
