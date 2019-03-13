using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingLaba1
{
    public partial class SobelForm : Form
    {
        public SobelForm()
        {
            InitializeComponent();
        }

        UInt32[,] _pixels;
        Bitmap image;

        public SobelForm(UInt32[,] pixels)
        {
            InitializeComponent();

            _pixels = pixels;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[,] VelGr = new double[3, 3];
            double O = 0;
            UInt32[,] pixels1 = Filters.ConvertToGray(_pixels);
            UInt32[,] pixels2 = Filters.ConvertToGray(_pixels);
            UInt32[,] pixels3 = Filters.ConvertToGray(_pixels); 

            // Вычисляем величину градиента
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    VelGr[i, j] = Math.Sqrt(Math.Pow(Filters.SubelX[i, j], 2) + Math.Pow(Filters.SubelY[i, j], 2));
                }
            }

            pixels1 = Filters.matrix_filtration(_pixels.GetLength(1), _pixels.GetLength(0), _pixels, Filters.NSob, Filters.SubelX);
            image = Transformations.FromUInt32ToBitmap(pixels1, pixels1.GetLength(0), pixels1.GetLength(1));
            pictureBox1.Image = image;


            pixels2 = Filters.matrix_filtration(_pixels.GetLength(1), _pixels.GetLength(0), _pixels, Filters.NSob, Filters.SubelY);

            image = Transformations.FromUInt32ToBitmap(pixels2, pixels2.GetLength(0), pixels2.GetLength(1));
            pictureBox2.Image = image;

            for (int y = 0; y < _pixels.GetLength(0); y++)
            {
                for (int x = 0; x < _pixels.GetLength(1); x++)
                {
                    pixels3[y,x] = (UInt32)Math.Sqrt(Math.Pow(pixels1[y, x], 2) + Math.Pow(pixels2[y, x], 2));
                }
            }

            image = Transformations.FromUInt32ToBitmap(pixels3, pixels3.GetLength(0), pixels3.GetLength(1));
            pictureBox2.Image = image;
                     
        }

        static double [,] Addition(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя сложить");
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < b.GetLength(1); i++)
            {
                for (int j = 0; j < b.GetLength(0); j++)
                {
                    r[i, j] += a[i, j] + b[i, j];
                }
            }
            return r;
        }


        static double[,] Multiplication(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }
    }
}
