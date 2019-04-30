using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ImageProcessingLabs.Convolution;
using ImageProcessingLabs.enums;

namespace ImageProcessingLabs.Points
{

    public class Moravec
    {
        private static Mat _image;

        public static List<InterestingPoint> DoMoravec(double minValue, int windowSize, int shiftSize, int locMaxRadius, Mat image)
        {
            _image = image.Clone();

            _image = SobelHelper.Sobel(_image, BorderWrapType.Mirror); // Считаем градиент в каждой точке 

            Mat mistakeImage = GetMinimums(_image, windowSize, shiftSize);


            List<InterestingPoint> candidates =
               CommonMath.getCandidates(mistakeImage, mistakeImage.Height, mistakeImage.Width, locMaxRadius, minValue);


            return candidates;
        }

        private static Mat Normalization(Mat source, double newMin, double newMax)
        {
            var result = source.Clone();

            double min = source.GetAt(0,0), max = source.GetAt(0,0);
            foreach (var value in source.GetData())
            {
                if (double.IsNaN(value))
                    continue;

                min = Math.Min(min, value);
                max = Math.Max(max, value);
            }

            for (var j = 0; j < source.Width; j++)
                for (var i = 0; i < source.Height; i++)
                {
                    result.Set(j, i, (source.GetAt(j, i) - min) * (newMax - newMin) / (max - min) + newMin);
                }

            return result;
        }

        public static Mat GetMinimums(Mat inputImage, int windowSize, int shiftSize)
        {
            Mat mistakeImage = (Mat)inputImage.Clone();
           
            for (int x = 0; x < mistakeImage.Width; x++)
                for (int y = 0; y < mistakeImage.Height; y++)
                {
                    double[] mistake = new double[8];
                    double[,] mainWindow = GetMainWindow(windowSize, y, x); // Формируем исходное окно

                    for (int shiftMode = 0; shiftMode < 8; shiftMode++) // Формируем сдвинутое окно
                    {
                        double[,] shiftWindow = GetShiftWindow(windowSize, y, x, shiftSize, shiftMode);
                        mistake[shiftMode] = GetMistake(mainWindow, shiftWindow);
                    }

                    mistakeImage.Set(x, y, mistake.Min()); // Получаем значение оператора Моравика в каждой точке изображения  
                }
            

            return mistakeImage;
        }


        public static double[,] GetMainWindow(int windowSize, int y, int x)
        {
            double[,] mainWindow = new double[windowSize, windowSize];
            for (int wy = 0; wy < windowSize; wy++)
                for (int wx = 0; wx < windowSize; wx++)
                    mainWindow[wy, wx] = _image.GetPixel(y + wy, x + wx, BorderWrapType.Mirror);

            return mainWindow;
        }

        public static double[,] GetShiftWindow(int windowSize, int y, int x, int shiftSize, int shiftMode)
        {

            double[,] shiftWindow = new double[windowSize, windowSize];
            for (int wy = 0; wy < windowSize; wy++)
                for (int wx = 0; wx < windowSize; wx++)
                {
                    switch (shiftMode)
                    {
                        case 0: // Сдвиг Вверх
                            shiftWindow[wy, wx] = _image.GetPixel(y + wy - shiftSize, x + wx, BorderWrapType.Mirror);
                            continue;
                        case 1: // Сдвиг Вправо-Вверх
                            shiftWindow[wy, wx] = _image.GetPixel(y + wy - shiftSize, x + wx + shiftSize, BorderWrapType.Mirror);
                            continue;
                        case 2: // Сдвиг Вправо
                            shiftWindow[wy, wx] = _image.GetPixel( y + wy, x + wx + shiftSize, BorderWrapType.Mirror);
                            continue;
                        case 3: // Сдвиг Вправо-Вниз
                            shiftWindow[wy, wx] = _image.GetPixel( y + wy + shiftSize, x + wx + shiftSize, BorderWrapType.Mirror);
                            continue;
                        case 4: // Сдвиг Вниз
                            shiftWindow[wy, wx] = _image.GetPixel(y + wy + shiftSize, x + wx, BorderWrapType.Mirror);
                            continue;
                        case 5: // Сдвиг Влево-Вниз
                            shiftWindow[wy, wx] = _image.GetPixel( y + wy + shiftSize, x + wx - shiftSize, BorderWrapType.Mirror);
                            continue;
                        case 6: // Сдвиг Влево
                            shiftWindow[wy, wx] = _image.GetPixel( y + wy, x + wy - shiftSize, BorderWrapType.Mirror);
                            continue;
                        case 7: // Сдвиг Влево-Вверх
                            shiftWindow[wy, wx] = _image.GetPixel( y + wy - shiftSize, x + wy - shiftSize, BorderWrapType.Mirror);
                            continue;
                    }
                }

            return shiftWindow;
        }

        public static double GetMistake(double[,] mainWindow, double[,] shiftWindow)
        {
            double mistake = 0;

            for (int y = 0; y < mainWindow.GetLength(0); y++)
                for (int x = 0; x < mainWindow.GetLength(0); x++)
                {
                    mistake += Math.Pow(mainWindow[y, x] - shiftWindow[y, x], 2);
                }
            return mistake;
        }

    }
}