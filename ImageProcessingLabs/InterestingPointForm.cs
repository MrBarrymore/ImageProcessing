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
using ImageProcessingLabs.Points;

namespace ImageProcessingLabs
{
    public partial class InterestingPointForm : Form
    {
        private static double[,] _pixels, outputPixel;
        private Bitmap image;

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
            if (RB_DoMoravec.Checked == true)
            {
                int minValue = Convert.ToInt32(textBox1.Text);
                int windowSize = Convert.ToInt32(textBox2.Text);
                int shiftSize = Convert.ToInt32(textBox3.Text);
                int locMaxRadius = Convert.ToInt32(textBox4.Text);

                List<InterestingPoint> MoravecMatrix = Moravec.DoMoravec(minValue, windowSize, shiftSize, locMaxRadius, _pixels);

                if (filter_checkBox.Checked == true) { 
                int maxCountPoints = Convert.ToInt32(textBox7.Text);
                List<InterestingPoint> subList = PointFilter.filterPoints(MoravecMatrix, maxCountPoints); // Фильтр точек
                    label9.Text = "Найдено интересных точек: " + MoravecMatrix.Count;
                    label10.Text = "Отображенно интересных точек: " + subList.Count;
                    DrawPoints(subList);
                }
                else
                { 
                    label10.Text = "Отображенно интересных точек: " + MoravecMatrix.Count;
                    label9.Text = "Найдено интересных точек: " + MoravecMatrix.Count;
                    DrawPoints(MoravecMatrix);
                }

            }

            if (RB_DoHarris.Checked == true)
            {
                double minValue = Convert.ToDouble(textBox5.Text);
                int windowSize = Convert.ToInt32(textBox6.Text);
                List<InterestingPoint> HarrisMatrix = Harris.DoHarris(minValue, windowSize,_pixels);
                
                ///////////
                if (checkBox2.Checked == true)
                {
                    List<InterestingPoint> SubHarrisMatrix = Harris.DoHarris(windowSize, _pixels, 2);
                    label9.Text = "Найдено откликов: " + SubHarrisMatrix.Count;
                    DrawMap(SubHarrisMatrix);
                }
                else
                if (checkBox1.Checked == true)
                {
                    List<InterestingPoint> SubHarrisMatrix = Harris.DoHarris(windowSize, _pixels, 1);
                    DrawMap(SubHarrisMatrix);
                    label9.Text = "Найдено локальных максимумов: " + SubHarrisMatrix.Count;
                }              

                else if (filter_checkBox.Checked == true)
                    {
                        int maxCountPoints = Convert.ToInt32(textBox7.Text);
                        List<InterestingPoint> subList = PointFilter.filterPoints(HarrisMatrix, maxCountPoints); // Фильтр точек
                        DrawPoints(subList);
                        label9.Text = "Найдено интересных точек: " + HarrisMatrix.Count;
                        label10.Text = "Отображенно интересных точек: " + subList.Count;
                    }
                    else
                    { 
                        label10.Text = "Отображенно интересных точек: " + HarrisMatrix.Count;
                        label9.Text = "Найдено интересных точек: " + HarrisMatrix.Count;
                        DrawPoints(HarrisMatrix);
                    }
                    
            }


        }

        public void DrawPoints(List<InterestingPoint> point)
        {
            image = Transformations.FromUInt32ToBitmap(outputPixel);
            Graphics graph = Graphics.FromImage(image);
            Color pen = Color.Blue;
            // ByteWriteFile(DrawMatrix);
            foreach (var interestingPoint in point)
            {
                graph.FillEllipse(new SolidBrush(pen), interestingPoint.y - 1, interestingPoint.x - 1, 3, 3);
            }

            pictureBox2.Image = image;

            image.Save("..\\..\\..\\..\\Output\\OutputPicture.png");
        }


        // Вспомагательные методы 

        public void DrawMap(List<InterestingPoint> point)
        {

            Graphics graph = Graphics.FromImage(image);
            Color pen = Color.White;

            double[,] blackMatrix = new double[_pixels.GetLength(0), _pixels.GetLength(1)];

            for (int y = 0; y < _pixels.GetLength(0); y++)
            for (int x = 0; x < _pixels.GetLength(1); x++)
            {
                blackMatrix[y, x] = 0;
            }

            foreach (var interestingPoint in point)
            {
                blackMatrix[interestingPoint.x, interestingPoint.y] = interestingPoint.probability * 255;
            }

            image = Transformations.FromUInt32ToBitmap(blackMatrix);
            pictureBox2.Image = image;
            image.Save("..\\..\\..\\..\\Output\\OutputPicture.png");
        }


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
