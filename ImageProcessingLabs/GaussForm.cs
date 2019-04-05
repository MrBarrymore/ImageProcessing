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
    public partial class GaussForm : Form
    {
        public GaussForm()
        {
            InitializeComponent();
        }

        // Глобальные переменные 
        Bitmap image, bufImage;

        static List<double[,]> picturePiramid = new List<double[,]>();

        static double[,] _pixels;
        double[,] sigmas;
        int Edgemode = 0;

        public GaussForm(double[,] pixels)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            _pixels = pixels;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (RB_Zero.Checked == true) Edgemode = 0;
            if (RB_EdgeCoppy.Checked == true) Edgemode = 1;
            if (RB_EdgeReflection.Checked == true) Edgemode = 2;
            if (RB_WrapImage.Checked == true) Edgemode = 3;

            int numOctaves = CountOctava(_pixels);
            double sigmaA = Convert.ToDouble(textBox2.Text);
            double sigma0 = Convert.ToDouble(textBox3.Text);
            int levels = Convert.ToInt32(comboBox2.Text);
            sigmas = new double[numOctaves, levels + 1]; // Массив значений сигм на каждом этапе преобразований

            image = Transformations.FromUInt32ToBitmap(_pixels);
            pictureBox1.Image = image;

            DeletePictures();
            image.Save("..\\..\\..\\..\\Output\\" + "Исходное изображение.png");

            double[,] GaussMatrix0 = CountGaussMatrix(sigmaA);
            _pixels = ImageMethods.processNonSeparable(_pixels, GaussMatrix0, 1);
            image = Transformations.FromUInt32ToBitmap(_pixels);
            pictureBox2.Image = image;
            picturePiramid.Add(_pixels);


            // Строим пирамиду изображения
            for (int i = 0; i < numOctaves; i++)
            {
                double[,] Pyramidpixels;
                if (i == 0) Pyramidpixels = picturePiramid[0];
                else Pyramidpixels = BuildNewLevel(picturePiramid[i]);

                double sigma = sigma0;

                for (int j = 0; j <= levels; j++)
                {
                    sigma = Math.Pow(Math.Pow(2d, 1d / levels), j) * sigma0; // Пересчитываем сигму

                    double[,] GaussMatrix;
                    GaussMatrix = CountGaussMatrix(sigma);
                    Pyramidpixels = ImageMethods.processNonSeparable(Pyramidpixels, GaussMatrix, 1);


                    bufImage = Transformations.FromUInt32ToBitmap(Pyramidpixels);
                    bufImage.Save("..\\..\\..\\..\\Output\\" + "Октава " + i + " Уровень " + j + " Знач.сигма " + sigma + ".png");
                    sigmas[i, j] = sigma; // Записываем значения сигм на каждом этапе преобразований
                }

                picturePiramid.Add(Pyramidpixels);
            }

            // Задаем новые значения для элементов управления выводом изображений
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox3.Text = Convert.ToString(0);
            comboBox4.Text = Convert.ToString(0);
            for (int i = 0; i < numOctaves; i++) comboBox3.Items.Add(i);
            for (int i = 0; i < levels; i++) comboBox4.Items.Add(i);

            // Заполняем выходные данные
            lbl_CountOctavas.Text = "Кол-во октав: " + Convert.ToString(numOctaves);
            lbl_LevelsInOctava.Text = "Уровней в октаве: " + Convert.ToString(levels);
        }

        private void btn_showImage_Click(object sender, EventArgs e)
        {
            showImage();
        }

        private void ShowNewImage(object sender, EventArgs e)
        {
            showImage();
        }

        double[,] CountGaussMatrix(int radius, double sigma)
        {
            double[,] gaussMatrix = new double[2 * radius + 1, 2 * radius + 1];

            double coef = 1 / (2 * Math.PI * sigma * sigma);
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    gaussMatrix[x + radius, y + radius] = coef * Math.Exp(-(x * x + y * y) / (2 * sigma * sigma));
                }
            }
            return gaussMatrix;
        }

        double[,] CountGaussMatrix(double sigma)
        {
            return CountGaussMatrix((int)Math.Ceiling(sigma) * 3, sigma);
        }

        void DeletePictures()
        {
            DirectoryInfo dirInfo = new DirectoryInfo("..\\..\\..\\..\\Output\\");
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }

        void showImage()
        {
            if (checkBoxRealSize.Checked == true) pictureBox2.SizeMode = PictureBoxSizeMode.Normal;
            else pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            try
            {
                int i = Convert.ToInt32(comboBox3.Text);
                int j = Convert.ToInt32(comboBox4.Text);

                image = new Bitmap("..\\..\\..\\..\\Output\\" + "Октава " + (i) + " Уровень " + (j) + " Знач.сигма " + sigmas[i, j] + ".png");
                pictureBox2.Image = image;
                lbl_SigmaValue.Text = "Значение сигмы: " + Convert.ToString(sigmas[i, j]);
                lbl_EffectiveSigmaValue.Text = "Эффективное значение сигмы: " + sigmas[0, 0] * Math.Pow(2, i);
            }
            catch
            {
                MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int CountOctava(double[,] _pixels)
        {
            int octava = 0;

            while ((_pixels.GetLength(0) / Math.Pow(2, octava)) > 50 && (_pixels.GetLength(1) / Math.Pow(2, octava)) > 50)
            {
                octava++;
            }
            return octava;
        }

        static double[,] BuildNewLevel(double[,] Pyramidpixels)
        {
            double[,] thisLevel = new double[(int)(Pyramidpixels.GetLength(0) / 2), (int)(Pyramidpixels.GetLength(1) / 2)];

            for (int y = 0; y < thisLevel.GetLength(0); y++)
            {
                for (int x = 0; x < thisLevel.GetLength(1); x++)
                {
                    thisLevel[y, x] = Pyramidpixels[(int)(y * 2), (int)(x * 2)];
                }
            }

            return thisLevel;
        }

    }
}
