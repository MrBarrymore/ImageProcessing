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

        Bitmap image;
        double[,] _pixels;

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
    }
}
