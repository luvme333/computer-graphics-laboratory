using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory_work_1
{
    class Dilation: Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            kernel = kernel3;
            int MH = kernel.GetLength(0);
            int MW = kernel.GetLength(0);
            int maxR = 0, maxG = 0, maxB = 0;
            for (int i = -MW / 2; i <= MW / 2; i++)
                for (int j = -MH / 2; j <= MH / 2; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(x + i, y + j);
                    if (kernel[i + MW / 2, j + MH / 2] && sourceColor.R > maxR)
                    {
                        maxR = sourceColor.R;
                    }
                    if (kernel[i + MW / 2, j + MH / 2] && sourceColor.G > maxG)
                    {
                        maxG = sourceColor.G;
                    }
                    if (kernel[i + MW / 2, j + MH / 2] && sourceColor.B > maxB)
                    {
                        maxB = sourceColor.B;
                    }
                }
            return Color.FromArgb(maxR, maxG, maxB);
        }
        public Bitmap processImage(Bitmap sourceImage)
        {
            kernel = kernel3;
            int MH = kernel.GetLength(0);
            int MW = kernel.GetLength(0);
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int x = MW / 2; x < sourceImage.Width - MW / 2; x++)
            {
                for (int y = MH / 2; y < sourceImage.Height - MH / 2; y++)
                {
                    resultImage.SetPixel(x, y, calculateNewPixelColor(sourceImage, x, y));
                }
            }
            return (resultImage);
        }
    }

    class Erosion : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int MH = kernel.GetLength(0);
            int MW = kernel.GetLength(0);
            int minR = 255, minG = 255, minB = 255;
            for (int i = -MW / 2; i <= MW / 2; i++)
                for (int j = -MH / 2; j <= MH / 2; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(x + i, y + j);

                    if (kernel[i + MW / 2, j + MH / 2] && sourceColor.R < minR)
                    {
                        minR = sourceColor.R;
                    }
                    if (kernel[i + MW / 2, j + MH / 2] && sourceColor.G < minG)
                    {
                        minG = sourceColor.G;
                    }
                    if (kernel[i + MW / 2, j + MH / 2] && sourceColor.B < minB)
                    {
                        minB = sourceColor.B;
                    }
                }
            return Color.FromArgb(minR, minG, minB);
        }
        public Bitmap processImage(Bitmap sourceImage)
        {
            int MH = kernel.GetLength(0);
            int MW = kernel.GetLength(0);
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int x = MW / 2; x < sourceImage.Width - MW / 2; x++)
            {
                for (int y = MH / 2; y < sourceImage.Height - MH / 2; y++)
                {
                    resultImage.SetPixel(x, y, calculateNewPixelColor(sourceImage, x, y));
                }
            }
            return (resultImage);
        }
    }

    class Gradient : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int MH = kernel.GetLength(0);
            int MW = kernel.GetLength(0);
            int minR = 255, minG = 255, minB = 255, maxR = 0, maxG = 0, maxB = 0;
            for (int i = -MW / 2; i <= MW / 2; i++)
                for (int j = -MH / 2; j <= MH / 2; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(x + i, y + j);
                    if (kernel[i + MW / 2, j + MH / 2] && sourceColor.R > maxR)
                    {
                        maxR = sourceColor.R;
                    }
                    if (kernel[i + MW / 2, j + MH / 2] && sourceColor.G > maxG)
                    {
                        maxG = sourceColor.G;
                    }
                    if (kernel[i + MW / 2, j + MH / 2] && sourceColor.B > maxB)
                    {
                        maxB = sourceColor.B;
                    }
                    if (kernel[i + MW / 2, j + MH / 2] && sourceColor.R < minR)
                    {
                        minR = sourceColor.R;
                    }
                    if (kernel[i + MW / 2, j + MH / 2] && sourceColor.G < minG)
                    {
                        minG = sourceColor.G;
                    }
                    if (kernel[i + MW / 2, j + MH / 2] && sourceColor.B < minB)
                    {
                        minB = sourceColor.B;
                    }
                }
            return Color.FromArgb(Clamp(maxR - minR, 0, 255), Clamp(maxG - minG, 0, 255), Clamp(maxB - minB, 0, 255));
        }
        public Bitmap processImage(Bitmap sourceImage)
        {
            int MH = kernel.GetLength(0);
            int MW = kernel.GetLength(0);
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int x = MW / 2; x < sourceImage.Width - MW / 2; x++)

            {
                for (int y = MH / 2; y < sourceImage.Height - MH / 2; y++)
                {
                    resultImage.SetPixel(x, y, calculateNewPixelColor(sourceImage, x, y));
                }
            }
            return (resultImage);
        }
    }
}
