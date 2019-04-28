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
using System.Drawing.Imaging;
using ImageProcessingLabs.Helper;
using ImageProcessingLabs.Wrapped;

namespace ImageProcessingLabs
{
    public partial class MainForm : Form
    {
        public static Bitmap picture, pictureA, pictureB;
        public static string full_name_of_image = "\0";
        public static Mat imageMat;
        private static WrappedImage image, wrappedImage, wrappedImageA, wrappedImageB;

        public MainForm()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            picture = new Bitmap("..\\..\\..\\..\\..\\Pictures\\Cat.jpg");
          //  picture = new Bitmap("..\\..\\..\\..\\..\\Pictures\\Lenna.png");
          //   picture = new Bitmap("..\\..\\..\\..\\..\\Pictures\\cube.jpg");
            // picture = new Bitmap("..\\..\\..\\..\\..\\Pictures\\3d.png");
            //  picture = new Bitmap("..\\..\\..\\..\\..\\Pictures\\Star.jpg");

            //  picture = new Bitmap("..\\..\\..\\..\\..\\Pictures\\Simple.png");
            //  picture = new Bitmap("..\\..\\..\\..\\..\\Pictures\\Figures.jpg");

            pictureBox1.Image = picture;

            imageMat = new Mat(picture.Width, picture.Height);

            imageMat = IOHelper.ImageToMat(picture);

            wrappedImage = WrappedImage.of(picture);

            SobelForm _sobelForm = new SobelForm(imageMat);
            _sobelForm.Show();


            pictureA = new Bitmap("..\\..\\..\\..\\..\\Pictures\\LennaA.png");
            pictureB = new Bitmap("..\\..\\..\\..\\..\\Pictures\\LennaB.png");

            wrappedImageA = WrappedImage.of(pictureA);
            wrappedImageB = WrappedImage.of(pictureB);

            //   InterestingPointForm _interestingPointForm = new InterestingPointForm(wrappedImage);
            //    _interestingPointForm.ShowDialog();


          //  DescriptorForm descriptorForm = new DescriptorForm(wrappedImageA, wrappedImageB);
          //  descriptorForm.Show();

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    full_name_of_image = open_dialog.FileName;

                    picture = new Bitmap(open_dialog.FileName);

                    pictureBox1.Image = picture;

                    //получение матрицы с пикселями
                    image.buffer = new double[image.height, image.width];
                    for (int y = 0; y < image.height; y++)
                        for (int x = 0; x < image.width; x++)
                        {
                            Color color = picture.GetPixel(x, y);
                            image.buffer[y, x] = color.R * 0.299 + color.G * 0.587 + color.B * 0.114;
                        }
                }
                catch
                {
                    full_name_of_image = "\0";
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picture.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void операторСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SobelForm _sobelForm = new SobelForm(imageMat);
            _sobelForm.Show();
        }

        private void фильтрГауссаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GaussForm _gaussForm = new GaussForm(image);
            _gaussForm.Show();
        }

        private void интересныеТочкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InterestingPointForm _interestingPointForm = new InterestingPointForm(wrappedImage);
            _interestingPointForm.Show();
        }

        private void дескрипторыТочекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DescriptorForm descriptorForm = new DescriptorForm(wrappedImageA, wrappedImageB);
            descriptorForm.Show();
        }
    }
}
