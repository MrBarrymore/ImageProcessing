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

namespace ImageProcessingLabs
{
    public partial class InterestingPointForm : Form
    {
        private static double[,] _pixels, outputPixel;
        private Bitmap image;

        // Задание Оператора Собеля для сепорабельных вычислений
        public static double[,] SubelSepX1 = new double[1, 3] { { 1, 2, 1 } };
        public static double[,] SubelSepX2 = new double[3, 1] { { 1 }, { 0 }, { -1 } };
        public static double[,] SubelSepY1 = new double[1, 3] { { 1, 0, -1 } };
        public static double[,] SubelSepY2 = new double[3, 1] { { 1 }, { 2 }, { 1 } };


        public InterestingPointForm()
        {
            InitializeComponent();
        }

        public InterestingPointForm(double[,] pixels)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            _pixels = (double[,])pixels.Clone();
            outputPixel = (double[,])pixels.Clone();
            image = Transformations.FromUInt32ToBitmap(_pixels);
            pictureBox1.Image = image;

        }

        private void FindPointButton_Click(object sender, EventArgs e)
        {
            if (RB_DoMoravec.Checked == true) DoMoravec();
            if (RB_DoHarris.Checked == true) DoHarris();
        }


        public void DoMoravec()
        {
            _pixels = (double[,])outputPixel.Clone();

            int minValue = Convert.ToInt32(textBox1.Text);
            int windowSize = Convert.ToInt32(textBox2.Text);
            int shiftSize = Convert.ToInt32(textBox3.Text);
            int locMaxRadius = 5;

            double[,] mistakeMatrix = new double[(int)_pixels.GetLength(0), (int)_pixels.GetLength(1)];
        //    double[,] newPixels = new double[(int)_pixels.GetLength(0), (int)_pixels.GetLength(1)];

            _pixels = DoSobelSeparable(_pixels);

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

            mistakeMatrix = GetLocalMax(mistakeMatrix, locMaxRadius); // Поиск локальных максимумов 

            DrawPoints(mistakeMatrix, minValue);

        }

        public void DoHarris()
        {
            _pixels = (double[,])outputPixel.Clone();

            int minValue = Convert.ToInt32(textBox1.Text);
            int windowSize = Convert.ToInt32(textBox2.Text);
            int shiftSize = Convert.ToInt32(textBox3.Text);
            int locMaxRadius = 5;

            double[,] mistakeMatrix = new double[(int)_pixels.GetLength(0), (int)_pixels.GetLength(1)];

            _pixels = DoSobelSeparable(_pixels);

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

            mistakeMatrix = GetLocalMax(mistakeMatrix, locMaxRadius); // Поиск локальных максимумов 

            DrawPoints(mistakeMatrix, minValue);

        }



        public double [,] DoSobelSeparable(double[,] pixel)
        {
            // Считаем частную производную по X (сепарабельно)
            double[,] pixelX = ConvolutionMatrixFactory.processNonSeparable(_pixels, SubelSepX1, 3);
            pixelX = ConvolutionMatrixFactory.processNonSeparable(pixelX, SubelSepX2, 3);
            // Считаем частную производную по Y (сепарабельно)
            double[,] pixelY = ConvolutionMatrixFactory.processNonSeparable(_pixels, SubelSepY1, 3);
            pixelY = ConvolutionMatrixFactory.processNonSeparable(pixelY, SubelSepY2, 3);

            // Вычисляем величину градиента
            for (int y = 0; y < _pixels.GetLength(0); y++)
            {
                for (int x = 0; x < _pixels.GetLength(1); x++)
                {
                    pixel[y, x] = Math.Sqrt(Math.Pow(pixelX[y, x], 2) + Math.Pow(pixelY[y, x], 2));
                }
            }
            return pixel;
        }

        public double [,] GetMainWindow(int windowSize, int y, int x)
        {
            double[,] mainWindow = new double[windowSize, windowSize];
            for (int wy = 0; wy < windowSize; wy++)
                for (int wx = 0; wx < windowSize; wx++)
                    mainWindow[wy, wx] = getPixel(y + wy, x + wx, 3);

            return mainWindow;
        }

        public double [,] GetShiftWindow(int windowSize, int y, int x, int shiftSize, int shiftMode)
        {

            double[,] shiftWindow = new double[windowSize, windowSize];
            for (int wy = 0; wy < windowSize; wy++)
                for (int wx = 0; wx < windowSize; wx++)
                {
                    switch (shiftMode)
                    {
                        case 0: // Сдвиг Вверх
                            shiftWindow[wy, wx] = getPixel(y + wy - shiftSize, x + wx, 3);
                            continue;
                        case 1: // Сдвиг Вправо-Вверх
                            shiftWindow[wy, wx] = getPixel(y + wy - shiftSize, x + wx + shiftSize, 3);
                            continue;
                        case 2: // Сдвиг Вправо
                            shiftWindow[wy, wx] = getPixel(y + wy, x + wx + shiftSize, 3);
                            continue;
                        case 3: // Сдвиг Вправо-Вниз
                            shiftWindow[wy, wx] = getPixel(y + wy + shiftSize, x + wx + shiftSize, 3);
                            continue;
                        case 4: // Сдвиг Вниз
                            shiftWindow[wy, wx] = getPixel(y + wy + shiftSize, x + wx, 3);
                            continue;
                        case 5: // Сдвиг Влево-Вниз
                            shiftWindow[wy, wx] = getPixel(y + wy + shiftSize, x + wx - shiftSize, 3);
                            continue;
                        case 6: // Сдвиг Влево
                            shiftWindow[wy, wx] = getPixel(y + wy, x + wy - shiftSize, 3);
                            continue;
                        case 7: // Сдвиг Влево-Вверх
                            shiftWindow[wy, wx] = getPixel(y + wy - shiftSize, x + wy - shiftSize, 3);
                            continue;
                    }
                }

            return shiftWindow;
        }

        public double GetMistake(double[,] mainWindow, double [,] shiftWindow)
        {
            double mistake = 0;

            for (int y = 0; y < mainWindow.GetLength(0); y++)           
                for (int x = 0; x < mainWindow.GetLength(0); x++)
                {
                    mistake += Math.Pow(mainWindow[y, x] - shiftWindow[y, x], 2);
                }
            return mistake;
        }

        private static double getPixel(int y, int x, int borderHandling)
        {
            switch (borderHandling)
            {
                case 0:
                    if (x < 0 || x >= _pixels.GetLength(1) || y < 0 || y >= _pixels.GetLength(0))
                        return 0;
                    return _pixels[y, x];
                case 1:
                    if (x < 0 || x >= _pixels.GetLength(1) || y < 0 || y >= _pixels.GetLength(0))
                        return 255;
                    return _pixels[y, x];
                case 2:
                    x = border(0, x, _pixels.GetLength(1) - 1);
                    y = border(y, 0, _pixels.GetLength(0) - 1);
                    return _pixels[y, x];
                case 3:
                    x = (x + _pixels.GetLength(1)) % _pixels.GetLength(1);
                    y = (y + _pixels.GetLength(0)) % _pixels.GetLength(0);
                    return _pixels[y, x];
                case 4:
                    x = Math.Abs(x);
                    y = Math.Abs(y);
                    if (x >= _pixels.GetLength(1)) x = _pixels.GetLength(1) - (x - _pixels.GetLength(1) + 1);
                    if (y >= _pixels.GetLength(0)) y = _pixels.GetLength(0) - (y - _pixels.GetLength(0) + 1);
                    return _pixels[y, x];
                default:
                    return 255;
            }
        }

        private static int border(int y, int x, int Length)
        {
            int res = 0;

            if (x > Length) res = Length;
            else if (x < 0) res = 0;
            else if (y > Length) res = Length;
            else if (y < 0) res = 0;
            else
            {
                if (x != 0) res = x;
                if (y != 0) res = y;
            }
            return res;
        }


        public double[,] GetLocalMax(double[,] pixel, int locMaxRadius) // Написать метод вычисления локальных максимумов 
        {
            double [,] localMaxPixel = new double[pixel.GetLength(0), pixel.GetLength(1)];

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


        public void DrawPoints(double[,] mistakeMatrix, int minValue)
        {
            image = Transformations.FromUInt32ToBitmap(outputPixel);
            Graphics graph = Graphics.FromImage(image);
            Pen pen = new Pen(Color.Blue);

            ByteWriteFile(mistakeMatrix);

            for (int y = 0; y < mistakeMatrix.GetLength(0); y++)
                for (int x = 0; x < mistakeMatrix.GetLength(1); x++)
                {
                    if (mistakeMatrix[y, x] > minValue)
                    {
                        graph.DrawEllipse(pen, x, y, 2, 2);
                    }
                }

            pictureBox2.Image = image;
        }

        // Вспомагательные методы 

        public static void ByteWriteFile(double [,] pixel)
        {
            StreamWriter sw = new StreamWriter("..\\..\\..\\..\\pass.txt");
            string s1 = " ";
            for (int y = 0; y < pixel.GetLength(0); y++)
            {
                s1 = "";
                for (int x = 0; x < pixel.GetLength(1); x++)
                {
                    s1 += pixel[y, x] + " ";
                }
                s1 += "\n";
                sw.Write(s1);
            }
            s1 += "\n";
            sw.Close();
        }
    }
}
