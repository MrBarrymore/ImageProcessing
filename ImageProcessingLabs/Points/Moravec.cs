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
using  ImageProcessingLabs.Wrapped;

namespace ImageProcessingLabs.Points
{
  
    public class Moravec
    {
        private static WrappedImage _image;

        public static List<InterestingPoint> DoMoravec(double minValue, int windowSize, int shiftSize, int locMaxRadius, WrappedImage image)
        {
            _image = (WrappedImage)image.Clone();

            _image = CommonMath.DoSobelSeparable(image); // Считаем градиент в каждой точке 

            WrappedImage mistakeImage= GetMinimums(_image, windowSize, shiftSize);

         //   mistakeImage = Normalization(mistakeImage, 0, 1);

            List<InterestingPoint> candidates =
               CommonMath.getCandidates(mistakeImage, mistakeImage.height, mistakeImage.width, locMaxRadius, minValue);
          
            //  candidates = candidates.Where(x => x.probability > minValue).ToList();

            return candidates;
        }

        private static WrappedImage Normalization(WrappedImage source, double newMin, double newMax)
        {
            var result = (WrappedImage)source.Clone();

            double min = source.buffer[0, 0], max = source.buffer[0, 0];
            foreach (var value in source.buffer)
            {
                if (double.IsNaN(value))
                    continue;

                min = Math.Min(min, value);
                max = Math.Max(max, value);
            }

            for (var i = 0; i < source.height; i++)
            for (var j = 0; j < source.width; j++)
            {
                result.buffer[i, j] = (source.buffer[i, j] - min) * (newMax - newMin) / (max - min) + newMin;
            }

            return result;
        }

        public static WrappedImage GetMinimums(WrappedImage inputImage, int windowSize, int shiftSize)
        {
            WrappedImage mistakeImage = (WrappedImage)inputImage.Clone();

            for (int y = 0; y < mistakeImage.height; y++)
            {
                for (int x = 0; x < mistakeImage.width; x++)
                {
                    double[] mistake = new double[8];
                    double[,] mainWindow = GetMainWindow(windowSize, y, x); // Формируем исходное окно

                    for (int shiftMode = 0; shiftMode < 8; shiftMode++) // Формируем сдвинутое окно
                    {
                        double[,] shiftWindow = GetShiftWindow(windowSize, y, x, shiftSize, shiftMode);
                        mistake[shiftMode] = GetMistake(mainWindow, shiftWindow);
                    }

                    mistakeImage.buffer[y, x] = mistake.Min(); // Получаем значение оператора Моравика в каждой точке изображения  
                }
            }

            return mistakeImage;
        }


            public static double[,] GetMainWindow(int windowSize, int y, int x)
        {
            double[,] mainWindow = new double[windowSize, windowSize];
            for (int wy = 0; wy < windowSize; wy++)
                for (int wx = 0; wx < windowSize; wx++)
                    mainWindow[wy, wx] = WrappedImage.getPixel(_image, y + wy, x + wx, BorderHandling.Mirror);

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
                            shiftWindow[wy, wx] = WrappedImage.getPixel(_image, y + wy - shiftSize, x + wx, BorderHandling.Mirror);
                            continue;
                        case 1: // Сдвиг Вправо-Вверх
                            shiftWindow[wy, wx] = WrappedImage.getPixel(_image, y + wy - shiftSize, x + wx + shiftSize, BorderHandling.Mirror);
                            continue;
                        case 2: // Сдвиг Вправо
                            shiftWindow[wy, wx] = WrappedImage.getPixel(_image, y + wy, x + wx + shiftSize, BorderHandling.Mirror);
                            continue;
                        case 3: // Сдвиг Вправо-Вниз
                            shiftWindow[wy, wx] = WrappedImage.getPixel(_image, y + wy + shiftSize, x + wx + shiftSize, BorderHandling.Mirror);
                            continue;
                        case 4: // Сдвиг Вниз
                            shiftWindow[wy, wx] = WrappedImage.getPixel(_image, y + wy + shiftSize, x + wx, BorderHandling.Mirror);
                            continue;
                        case 5: // Сдвиг Влево-Вниз
                            shiftWindow[wy, wx] = WrappedImage.getPixel(_image, y + wy + shiftSize, x + wx - shiftSize, BorderHandling.Mirror);
                            continue;
                        case 6: // Сдвиг Влево
                            shiftWindow[wy, wx] = WrappedImage.getPixel(_image, y + wy, x + wy - shiftSize, BorderHandling.Mirror);
                            continue;
                        case 7: // Сдвиг Влево-Вверх
                            shiftWindow[wy, wx] = WrappedImage.getPixel(_image, y + wy - shiftSize, x + wy - shiftSize, BorderHandling.Mirror);
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