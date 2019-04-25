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

using ImageProcessingLabs.Wrapped;

namespace ImageProcessingLabs
{
    public partial class GaussForm : Form
    {
        public GaussForm()
        {
            InitializeComponent();
        }

        // Глобальные переменные 
        Bitmap picture, bufImage;

        static List<double[,]> picturePiramid = new List<double[,]>();

        static WrappedImage _image;
        double[,] sigmas;

        BorderHandling borderHandling;

        public GaussForm(WrappedImage image)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            _image = new WrappedImage(image);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BorderHandling Edgemode = BorderHandling.Black;
            if (RB_Zero.Checked == true) Edgemode = BorderHandling.Black;
            if (RB_White.Checked == true) Edgemode = BorderHandling.White;
            if (RB_EdgeCoppy.Checked == true) Edgemode = BorderHandling.Copy;
            if (RB_EdgeReflection.Checked == true) Edgemode = BorderHandling.Wrap;
            if (RB_WrapImage.Checked == true) Edgemode = BorderHandling.Mirror;

            int numOctaves = BuildingPyramid.CountOctava(_image.buffer);
            double sigmaStart = Convert.ToDouble(textBox2.Text);
            double sigma0 = Convert.ToDouble(textBox3.Text);
            int levels = Convert.ToInt32(comboBox2.Text);
            sigmas = new double[numOctaves, levels + 1]; // Массив значений сигм на каждом этапе преобразований

            picture = Transformations.FromUInt32ToBitmap(_image.buffer);
            pictureBox1.Image = picture;

            DeletePictures();
            picture.Save("..\\..\\..\\..\\Output\\" + "Исходное изображение.png");


            buildPiramid(Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text), BuildingPyramid.CountOctava(_image.buffer), Convert.ToInt32(comboBox2.Text), Edgemode);

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

        public void buildPiramid(double sigmaA, double sigmaStart, int numOctaves, int levels, BorderHandling Edgemode)
        {
            double sigma0 = sigmaStart;
            double[,] GaussMatrix0 = ConvolutionMatrix.CountGaussMatrix(Math.Sqrt(sigmaA * sigmaA + sigma0 * sigma0));
            _image = ConvolutionMatrixFactory.processNonSeparable(_image, GaussMatrix0, Edgemode);
            picture = Transformations.FromUInt32ToBitmap(_image.buffer);
            pictureBox2.Image = picture;
            picturePiramid.Add(_image.buffer); // Добавляем 0 уровень пирамиды 

            // Строим пирамиду изображения
            for (int i = 0; i < numOctaves; i++)
            {
                double[,] Pyramidpixels;
                if (i == 0) Pyramidpixels = picturePiramid[0];
                else
                {
                    Pyramidpixels = BuildingPyramid.BuildNewLevel(picturePiramid[i]);
                    picturePiramid.Add(Pyramidpixels);
                }

                for (int j = 0; j <= levels; j++)
                {
                    double sigma;
                    if (j == 0)
                    {
                        sigma0 = sigmaStart;
                        bufImage = Transformations.FromUInt32ToBitmap(Pyramidpixels);
                        bufImage.Save("..\\..\\..\\..\\Output\\" + "Октава " + i + " Уровень " + j + " Знач.сигма " + sigma0 + ".png");
                        sigmas[i, j] = sigma0;
                    }
                    else
                    {
                        sigmaA = sigmas[i, j - 1];
                        sigma0 = Math.Pow(Math.Pow(2d, 1d / levels), j) * sigmaStart;

                        sigma = Math.Sqrt(sigmaA * sigmaA + sigma0 * sigma0);

                        double[,] GaussMatrix;
                        GaussMatrix = ConvolutionMatrix.CountGaussMatrix(sigma);
                        //    Pyramidpixels = ConvolutionMatrixFactory.processNonSeparable(Pyramidpixels, GaussMatrix, Edgemode);

                        // Cохранение картинки
                        bufImage = Transformations.FromUInt32ToBitmap(Pyramidpixels);
                        bufImage.Save("..\\..\\..\\..\\Output\\" + "Октава " + i + " Уровень " + j + " Знач.сигма " + sigma0 + ".png");
                        sigmas[i, j] = sigma0; // Записываем значения сигм на каждом этапе преобразований

                    }
                }
                picturePiramid.Add(Pyramidpixels);
            }

        }


        private void btn_showImage_Click(object sender, EventArgs e)
        {
            showImage();
        }

        private void ShowNewImage(object sender, EventArgs e)
        {
            showImage();
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

                picture = new Bitmap("..\\..\\..\\..\\Output\\" + "Октава " + (i) + " Уровень " + (j) + " Знач.сигма " + sigmas[i, j] + ".png");
                pictureBox2.Image = picture;
                lbl_SigmaValue.Text = "Значение сигмы: " + Convert.ToString(sigmas[i, j]);
                lbl_EffectiveSigmaValue.Text = "Эффективное значение сигмы: " + sigmas[i, j] * Math.Pow(2, i);
            }
            catch
            {
                MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
