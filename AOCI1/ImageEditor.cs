using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;

namespace AOCI1
{

    class ImageEditor
    {
        private Image<Bgr, byte> sourceImage;
        private VideoCapture capture;

        public Image<Bgr, byte> OpenImage(string fileName)
        {
            sourceImage = new Image<Bgr, byte>(fileName);
            return sourceImage;
        }

        public void OpenVideo(string fileName)
        {
            capture = new VideoCapture(fileName);   
            
        }

        public Image<Bgr, byte> GetVideoFrame()
        {
            if (capture != null)
            {
                var frame = capture.QueryFrame();
                var image = frame.ToImage<Bgr, byte>();
                return image;
            }
            return null;
        }

        public void ShowImage(ImageBox imageBox, Image<Bgr, byte> image)
        {
            imageBox.Image = image.Resize(imageBox.Width, imageBox.Height, Inter.Linear);
        }

        public Image<Bgr, byte> EditImage(Image<Bgr, byte> image, double threshold, double linking, int colorCoeff, bool colorEffect, bool canny)
        {
            if (colorEffect == false && canny == false) return image;
            Image<Gray, byte> grayImage = image.Convert<Gray, byte>();
            grayImage = grayImage.PyrDown();
            grayImage = grayImage.PyrUp();
            double cannyThreshold = threshold;
            double cannyThresholdLinking = linking;
            var cannyEdges = grayImage.Canny(cannyThreshold, cannyThresholdLinking);
            var cannyEdgesBgr = cannyEdges.Convert<Bgr, byte>();
            if(canny == true)
            {
                return cannyEdgesBgr;
            }
            cannyEdgesBgr = cannyEdgesBgr.Resize(image.Width, image.Height, Inter.Linear);
            var resultImage = image.Sub(cannyEdgesBgr);
            if (colorEffect == true)
            {
                for (int channel = 0; channel < resultImage.NumberOfChannels; channel++)
                    for (int x = 0; x < resultImage.Width; x++)
                        for (int y = 0; y < resultImage.Height; y++)
                        {
                            byte color = resultImage.Data[y, x, channel];
                            color = Convert.ToByte((int)color / colorCoeff * colorCoeff);
                            resultImage.Data[y, x, channel] = color;
                        }
            }
            return resultImage;
        }

        public Image<Bgr, byte> ReturnColorChannel(Image<Bgr, byte> image, string channelName = "r")
        {
            int channelIndex;
            if (channelName == "r") channelIndex = 2;
            if (channelName == "g") channelIndex = 1;
            if (channelName == "b") channelIndex = 0;
            var channel = sourceImage.Split()[channelIndex];
            VectorOfMat vm = new VectorOfMat();
            vm.Push(channel[0]); vm.Push(channel[1]); vm.Push(channel[2]);
            CvInvoke.Merge(vm, destImage);


        }

        public Image<Bgr, byte> ReturnBW(Image<Bgr, byte> image)
        {

        }

    }
}
