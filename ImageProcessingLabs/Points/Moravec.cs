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

namespace ImageProcessingLabs.Points
{
  
    public class Moravec
    {
        private static double[,] _pixels;

        public static List<InterestingPoint> DoMoravec(int minValue, int windowSize, int shiftSize, int locMaxRadius, double[,] pixels)
        {
            _pixels = (double[,])pixels.Clone();

            double[,] mistakeMatrix = new double[_pixels.GetLength(0), _pixels.GetLength(1)];
            _pixels = CommonMath.DoSobelSeparable(_pixels); // Считаем градиент в каждой точке 

            mistakeMatrix = GetMinimums(mistakeMatrix, windowSize, shiftSize);

            mistakeMatrix = GetLocalMax(mistakeMatrix, locMaxRadius); // Поиск локальных максимумов 

            List<InterestingPoint> candidates =
               CommonMath.getCandidates(mistakeMatrix, mistakeMatrix.GetLength(0), mistakeMatrix.GetLength(1));
            candidates = candidates.Where(x => x.probability > 150).ToList();

            return candidates;
        }

        public static double[,] GetMinimums(double[,] mistakeMatrix, int windowSize, int shiftSize)
        {
            for (int y = 0; y < _pixels.GetLength(0); y++)
            {
                for (int x = 0; x < _pixels.GetLength(1); x++)
                {
                    double[] mistake = new double[8];
                    double[,] mainWindow = GetMainWindow(windowSize, y, x); // Формируем исходное окно

                    for (int shiftMode = 0; shiftMode < 8; shiftMode++) // Формируем сдвинутое окно
                    {
                        double[,] shiftWindow = GetShiftWindow(windowSize, y, x, shiftSize, shiftMode);
                        mistake[shiftMode] = GetMistake(mainWindow, shiftWindow);
                    }

                    mistakeMatrix[y, x] = mistake.Min(); // Получаем значение оператора Моравика в каждой точке изображения  
                }
            }

            return mistakeMatrix;
        }


            public static double[,] GetMainWindow(int windowSize, int y, int x)
        {
            double[,] mainWindow = new double[windowSize, windowSize];
            for (int wy = 0; wy < windowSize; wy++)
                for (int wx = 0; wx < windowSize; wx++)
                    mainWindow[wy, wx] = CommonMath.getPixel(_pixels, y + wy, x + wx, 3);

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
                            shiftWindow[wy, wx] = CommonMath.getPixel(_pixels, y + wy - shiftSize, x + wx, 3);
                            continue;
                        case 1: // Сдвиг Вправо-Вверх
                            shiftWindow[wy, wx] = CommonMath.getPixel(_pixels, y + wy - shiftSize, x + wx + shiftSize, 3);
                            continue;
                        case 2: // Сдвиг Вправо
                            shiftWindow[wy, wx] = CommonMath.getPixel(_pixels, y + wy, x + wx + shiftSize, 3);
                            continue;
                        case 3: // Сдвиг Вправо-Вниз
                            shiftWindow[wy, wx] = CommonMath.getPixel(_pixels, y + wy + shiftSize, x + wx + shiftSize, 3);
                            continue;
                        case 4: // Сдвиг Вниз
                            shiftWindow[wy, wx] = CommonMath.getPixel(_pixels, y + wy + shiftSize, x + wx, 3);
                            continue;
                        case 5: // Сдвиг Влево-Вниз
                            shiftWindow[wy, wx] = CommonMath.getPixel(_pixels, y + wy + shiftSize, x + wx - shiftSize, 3);
                            continue;
                        case 6: // Сдвиг Влево
                            shiftWindow[wy, wx] = CommonMath.getPixel(_pixels, y + wy, x + wy - shiftSize, 3);
                            continue;
                        case 7: // Сдвиг Влево-Вверх
                            shiftWindow[wy, wx] = CommonMath.getPixel(_pixels, y + wy - shiftSize, x + wy - shiftSize, 3);
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

        public static double[,] GetLocalMax(double[,] pixel, int locMaxRadius) // Написать метод вычисления локальных максимумов 
        {
            double[,] localMaxPixel = new double[pixel.GetLength(0), pixel.GetLength(1)];

            for (int y = locMaxRadius; y < pixel.GetLength(0) - locMaxRadius; y = y + locMaxRadius)
            {

                for (int x = locMaxRadius; x < pixel.GetLength(1) - locMaxRadius; x = x + locMaxRadius)
                {
                    int ymax = 0, xmax = 0, buf = 0;

                    for (int ly = y; ly < y + locMaxRadius; ly++)
                        for (int lx = x; lx < x + locMaxRadius; lx++)
                        {
                            if (_pixels[ly, lx] > buf)
                            {
                                ymax = ly;
                                xmax = lx;
                            }
                        }
                    localMaxPixel[ymax, xmax] = _pixels[ymax, xmax];
                }
            }

            return localMaxPixel;
        }
    }
}