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
        public int kernelSize;
        public bool[,] kernel;
        public bool[,] kernel3 = new bool[3, 3] { { true, false, true }, { false, true, false }, { true, false, true } };
        public bool[,] kernel5 = new bool[5, 5];
        public bool[,] kernel7 = new bool[7, 7];
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
        private int R_Sum = 0, G_Sum = 0, B_Sum = 0, R_Result = 0, G_Result = 0, B_Result = 0, N;
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
        private int R_Result = 0, G_Result = 0, B_Result = 0;
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
            if (Brightness > 0)
            {
                R_Result = (int) (sourceColor.R * NewBrightness / Brightness);
                G_Result = (int) (sourceColor.G *  NewBrightness / Brightness);
                B_Result = (int) (sourceColor.B * NewBrightness / Brightness);
                R_Result = Clamp(R_Result, 0, 255);
                G_Result = Clamp(G_Result, 0, 255);
                B_Result = Clamp(B_Result, 0, 255);
                Color resultColor = Color.FromArgb(R_Result, G_Result, B_Result);
                return resultColor;
            }
            else
            {
                Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);
                return resultColor;
            }
        }
    }

    class Waves 
    {
        private int newX = 0, newY = 0;
        public Bitmap processImage(Bitmap sourceImage)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i=0; i<sourceImage.Width - 1; i++)
            {
                for (int j=0; j<sourceImage.Height; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(i, j);
                    newX = i + (int) (20 * Math.Sin(2 * Math.PI * j / 30));
                    newY = j;
                    if (newX >= sourceImage.Width || newX < 0)
                    {
                        newX = i;
                    }
                    resultImage.SetPixel(newX, newY, sourceColor);
                }
            }
            return resultImage;
        }
    }

    class MatrixFilter : Filters
    {
        protected float[,] kernel = null;
        protected MatrixFilter() {}
        public MatrixFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            float resultR = 0, resultG = 0, resultB = 0;
            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
                    resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
                    resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
                }
            return Color.FromArgb(
                Clamp((int) resultR, 0, 255),
                Clamp((int) resultG, 0, 255),
                Clamp((int) resultB, 0, 255));
        }
    }

    class SharpnessFilter : MatrixFilter
    {
        public SharpnessFilter()
        {
            kernel = new float[3, 3];
            for (int i=0; i<3; i++)
                for (int j=0; j<3; j++)
                {
                    if (i==1 && j==1)
                    {
                        kernel[i, j] = 9;
                    }
                    else
                    {
                        kernel[i, j] = -1;
                    }
                }
        }
    }

    class MedianFilter : Filters
    {
        protected static int radius = 1, size = (radius*2 +1) * (radius*2 +1);
        protected int medianR, medianG, medianB;
        protected int R_Result = 0, G_Result = 0, B_Result = 0;
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int[] pixelColor_R = new int[size];
            int[] pixelColor_G = new int[size];
            int[] pixelColor_B = new int[size];
            int m = 0;
            Color sourceColor;
            for (int l = -radius; l <= radius; l++)
                for (int k = -radius; k <= radius; k++)
                {
                    sourceColor = sourceImage.GetPixel(x + l, y + k);
                    pixelColor_R[m] = sourceColor.R;
                    pixelColor_G[m] = sourceColor.G;
                    pixelColor_B[m] = sourceColor.B;
                    m++;
                }
            Array.Sort(pixelColor_R);
            Array.Sort(pixelColor_G);
            Array.Sort(pixelColor_B);
            medianR = pixelColor_R[size / 2];
            medianG = pixelColor_G[size / 2];
            medianB = pixelColor_B[size / 2];
            sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(medianR, medianG, medianB);
            return resultColor;
        }
        public Bitmap processImage(Bitmap sourceImage)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int x = radius; x < sourceImage.Width - radius; x++)
            {
                for (int y = radius; y < sourceImage.Height - radius; y++)
                {
                    resultImage.SetPixel(x, y, calculateNewPixelColor(sourceImage, x, y));
                }
            }
            return (resultImage);
        }
    }
}
