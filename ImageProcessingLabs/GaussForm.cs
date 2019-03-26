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
    public partial class GaussForm : Form
    {
        public GaussForm()
        {
            InitializeComponent();
        }

        Bitmap image, image2;
        static double[,] _pixels;

        public GaussForm(double [,] pixels)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            _pixels = pixels;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Edgemode = 0;
            if (RB_Zero.Checked == true) Edgemode = 0;
            if (RB_EdgeCoppy.Checked == true) Edgemode = 1;
            if (RB_EdgeReflection.Checked == true) Edgemode = 2;
            if (RB_WrapImage.Checked == true) Edgemode = 3;

            double[,] Gausspixels;
            image = Transformations.FromUInt32ToBitmap(_pixels);
            pictureBox1.Image = image;
            int sigma = Convert.ToInt32(textBox1.Text);
            double[,] GaussMatrix = new double[sigma, sigma];
            GaussMatrix = CountGaussMatrix(GaussMatrix, sigma);

            Gausspixels = Filters.matrix_filtration(_pixels, GaussMatrix, Edgemode);
            image = Transformations.FromUInt32ToBitmap(Gausspixels);
            pictureBox2.Image = image;
        }

        double [,] CountGaussMatrix(double [,] gaussMatrix, double sigma) 
        {
            for (int y = 0; y < sigma; y ++)
            {
                for (int x = 0; x < sigma; x++)
                {
                    int x1 = x - (int)(sigma / 2);
                    int y1 = y - (int)(sigma / 2);
                    gaussMatrix[y, x] = (1 / (Math.PI * 2 * Math.Pow(sigma, 2)) ) * Math.Exp( - (Math.Pow(x1, 2) + Math.Pow(y1, 2)) / (2 * Math.Pow(sigma, 2)));
                }
            }
            return gaussMatrix;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            int Edgemode = 0;
            if (RB_Zero.Checked == true) Edgemode = 0;
            if (RB_EdgeCoppy.Checked == true) Edgemode = 1;
            if (RB_EdgeReflection.Checked == true) Edgemode = 2;
            if (RB_WrapImage.Checked == true) Edgemode = 3;

            
            double sigma = Convert.ToInt32(textBox3.Text);
            int octava = Convert.ToInt32(comboBox1.Text);
            int levels = Convert.ToInt32(comboBox2.Text);

            image = Transformations.FromUInt32ToBitmap(_pixels);
            pictureBox1.Image = image;

            image.Save("..\\..\\..\\..\\Output\\" + "Исходное изображение.png");

            for (int i = 0; i < octava; i++)
            {
                double[,] Pyramidpixels = BuildNewLevel(_pixels.GetLength(0) / (int)(Math.Pow(2, i)), (int)(_pixels.GetLength(1) / (int)(Math.Pow(2, i))), i);

                for (int j = 0; j < levels; j++)
                {
                    double[,] GaussMatrix = new double[(int)sigma, (int)sigma];
                    GaussMatrix = CountGaussMatrix(GaussMatrix, sigma);
                    Pyramidpixels = Filters.matrix_filtration(Pyramidpixels, GaussMatrix, 1);

                    image2 = Transformations.FromUInt32ToBitmap(Pyramidpixels);

                    image2.Save("..\\..\\..\\..\\Output\\" + "Октава " + (i) + " Уровень " + (j + 1) + " Знач.сигма " + sigma + ".png");
                    sigma = (sigma * Math.Pow(Math.Pow(2, 1 / levels), j));
                }

                image = Transformations.FromUInt32ToBitmap(Pyramidpixels);
                pictureBox2.Image = image;
                pictureBox3.Image = image2;
            }
        }


        static double [,] BuildNewLevel( int Width, int Height, int octava)
        {
            double[,] Pyramidpixels = new double[Width, Height];

            for (int y = 0; y < Pyramidpixels.GetLength(0); y++)
            {
                for (int x = 0; x < Pyramidpixels.GetLength(1); x++)
                {
                    Pyramidpixels[y, x] = _pixels[(int)(y * (Math.Pow(2, octava) )), (int)(x * (Math.Pow(2, octava) ))];
                }
            }
            return Pyramidpixels;
        }

    }
}
