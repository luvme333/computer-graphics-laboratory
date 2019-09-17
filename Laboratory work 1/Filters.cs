using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//using System.ComponentModel;

namespace Laboratory_work_1
{
    abstract class Filters
    {
        protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);
        public Bitmap processImage(Bitmap sourceImage)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i=0; i<sourceImage.Width; i++)
            {
                /*worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;*/
                for (int j=0; j<sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }
        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
    }
    class InvertFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
            return resultColor;
        }
    }

    class GrayWorldFilter : Filters
    {
        private int R_Sum = 0, G_Sum = 0, B_Sum = 0, R_Result =0, G_Result = 0, B_Result = 0, N;
        private int R_Avg = 0, G_Avg = 0, B_Avg = 0, Avg = 0;
        
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            N = sourceImage.Width * sourceImage.Height;
            Color sourceColor = sourceImage.GetPixel(x, y);
            if (x == 0 && y == 0)
            {
                for (int i = 0; i < sourceImage.Width; i++)
                {
                    for (int j = 0; j < sourceImage.Height; j++)
                    {
                        sourceColor = sourceImage.GetPixel(i, j);
                        R_Sum = R_Sum + sourceColor.R;
                        G_Sum = G_Sum + sourceColor.G;
                        B_Sum = B_Sum + sourceColor.B;
                    }
                }
                R_Avg = R_Sum / N;
                G_Avg = G_Sum / N;
                B_Avg = B_Sum / N;
                Avg = (R_Avg + G_Avg + B_Avg) / 3;
            }
            R_Result = sourceColor.R * Avg / R_Avg;
            G_Result = sourceColor.G * Avg / G_Avg;
            B_Result = sourceColor.B * Avg / B_Avg;
            R_Result = Clamp(R_Result, 0, 255);
            G_Result = Clamp(G_Result, 0, 255);
            B_Result = Clamp(B_Result, 0, 255);
            Color resultColor = Color.FromArgb((byte) R_Result, (byte) G_Result, (byte) B_Result);
            return resultColor;
        }
    }
    
    class LinearCorrection : Filters 
    {
        private double Brightness = 0, NewBrightness = 0;
        private double maxBrightness = 0, minBrightness = 255;
        protected double calculateNewBrightness(double max, double min, double value)
        {
            return (value - min) * 255 / (max - min);
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Brightness = 0.299 * sourceColor.R + 0.587 * sourceColor.G + sourceColor.B * 0.114;
            if (x == 0 && y == 0)
            {
                for (int i = 0; i < sourceImage.Width; i++)
                {
                    for (int j = 0; j < sourceImage.Height; j++)
                    {
                        sourceColor = sourceImage.GetPixel(i, j);
                        Brightness = 0.299 * sourceColor.R + 0.587 * sourceColor.G + sourceColor.B * 0.114;
                        if (minBrightness > Brightness)
                        {
                            minBrightness = Brightness;
                        }
                        if (maxBrightness < Brightness)
                        {
                           maxBrightness = Brightness;
                        }
                    }
                }
            }     
            NewBrightness = calculateNewBrightness(maxBrightness, minBrightness, Brightness);
            Color resultColor = Color.FromArgb((int)sourceColor.R * (int) (NewBrightness / Brightness), 
                sourceColor.G * (int) (NewBrightness / Brightness), 
                (int)sourceColor.B * (int) (NewBrightness / Brightness));
            return resultColor;
        }
    }
}
