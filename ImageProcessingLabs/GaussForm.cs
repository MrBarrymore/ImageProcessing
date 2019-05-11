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
using ImageProcessingLabs.enums;
using ImageProcessingLabs.Helper;
using ImageProcessingLabs.Properties;
using ImageProcessingLabs.Scale;
using ImageProcessingLabs.Transformation;

namespace ImageProcessingLabs
{
    public partial class GaussForm : Form
    {
        // Глобальные переменные 
        Bitmap picture, bufImage;

        static Mat _image;
        double[,] sigmas;

        public Pyramid pyramid;

        public GaussForm()
        {
            InitializeComponent();
        }

        public GaussForm(Mat image)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;


            _image = image.Clone();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BorderWrapType Edgemode = BorderWrapType.Mirror;
            if (RB_Zero.Checked == true) Edgemode = BorderWrapType.Black;
            if (RB_EdgeCoppy.Checked == true) Edgemode = BorderWrapType.Copy;
            if (RB_EdgeReflection.Checked == true) Edgemode = BorderWrapType.Wrap;
            if (RB_WrapImage.Checked == true) Edgemode = BorderWrapType.Mirror;


            double sigma0 = Convert.ToDouble(textBox2.Text);
            double sigma1 = Convert.ToDouble(textBox3.Text);
            int levels = Convert.ToInt32(comboBox2.Text);


            picture = Transformer.FromMatToBitmap(_image);
            pictureBox1.Image = picture;

            DeletePictures();

            picture.Save("..\\..\\..\\..\\Output\\" + "Исходное изображение.png");


            pyramid = Pyramid.Build(_image, levels, sigma0, sigma1);


            for (var octave = 0; octave < Pyramid.Depth; octave++)
            for (var k = 0; k <= pyramid.OctaveSize; k++)
            {
                var layer = pyramid.GetLayer(octave, k);

                var path = "..\\..\\..\\..\\Output\\" + layer;
                IOHelper.WriteMatToFile(layer.GetMat(), path);
            }


            // Задаем новые значения для элементов управления выводом изображений
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox3.Text = Convert.ToString(0);
            comboBox4.Text = Convert.ToString(0);
            for (int i = 0; i < Pyramid.Depth; i++) comboBox3.Items.Add(i);
            for (int i = 0; i < levels; i++) comboBox4.Items.Add(i);

            // Заполняем выходные данные
            lbl_CountOctavas.Text = "Кол-во октав: " + Convert.ToString(Pyramid.Depth);
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
