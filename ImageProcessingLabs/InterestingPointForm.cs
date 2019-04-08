using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingLabs
{
    public partial class InterestingPointForm : Form
    {
        private static double[,] _pixels;
        private Bitmap image;

        private static double [,] mistakeMatrix;

        public InterestingPointForm()
        {
            InitializeComponent();
        }

        public InterestingPointForm(double [,] pixels)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            _pixels = pixels;
            image = Transformations.FromUInt32ToBitmap(_pixels);
            pictureBox1.Image = image;

        }


        private void FindPointButton_Click(object sender, EventArgs e)
        {
            int maxValue = Convert.ToInt32(textBox1.Text);
            int windowSize = Convert.ToInt32(textBox2.Text);
            int shiftSize = Convert.ToInt32(textBox3.Text);
            int locMaxRadius = 5;

            mistakeMatrix = new double [(int)_pixels.GetLength(0), (int)_pixels.GetLength(1)];

            for (int y = 0; y < _pixels.GetLength(0); y++)
            {
                for (int x = 0; x < _pixels.GetLength(1); x++)
                {
                    double [] mistake = new double [8];
                    double[,] mainWindow = GetMainWindow(windowSize, y, x);
                    
                    for (int shiftMode = 0; shiftMode < 8; shiftMode++)
                    {
                        double[,] shiftWindow = GetShiftWindow(windowSize, y, x, shiftSize, shiftMode);
                        mistake[shiftMode] = GetMistake(mainWindow, shiftWindow);
                    }

                    mistakeMatrix[y, x] = mistake.Min(); // Получаем матрицу значений оператора Моравика в каждой точке изображения  
                 }
            }


            

        }

        public double [,] GetMainWindow(int windowSize, int y, int x)
        {
            double[,] mainWindow = new double[windowSize, windowSize];
            for (int wy = 0; wy < windowSize; wy++)
                for (int wx = 0; wx < windowSize; wx++)
                    mainWindow[wy, wx] = _pixels[y + wy, x + wx];
 
            return mainWindow;
        }

        public double[,] GetShiftWindow(int windowSize, int y, int x, int shiftSize, int shiftMode)
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




    }
}
