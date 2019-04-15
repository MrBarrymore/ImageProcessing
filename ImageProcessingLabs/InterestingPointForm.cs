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
        private static double[,] MoravecMatrix;
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
                int locMaxRadius = 5;

                MoravecMatrix = null;
                MoravecMatrix = Moravec.DoMoravec(windowSize, shiftSize, locMaxRadius, _pixels);
                DrawPoints(MoravecMatrix, minValue);
            }

            if (RB_DoHarris.Checked == true) DoHarris();
        }

        private static double MIN_PROBABILITY = 0.01;

        public void DoHarris()
        {
            _pixels = (double[,])outputPixel.Clone();

           // int minValue = Convert.ToInt32(textBox1.Text);
            int windowSize = Convert.ToInt32(textBox2.Text);
            int shiftSize = Convert.ToInt32(textBox3.Text);

            double[,] harrisMat = new double[(int)_pixels.GetLength(0), (int)_pixels.GetLength(1)];
            _pixels = CommonMath.DoSobelSeparable(_pixels); // Считаем градиент в каждой точке 


            harrisMat = GetHarrisMatrix(windowSize, (int)_pixels.GetLength(0), (int)_pixels.GetLength(1));


            List<InterestingPoint> candidates =
                getCandidates(harrisMat, harrisMat.GetLength(0), harrisMat.GetLength(1));

            candidates = candidates.Where(x => x.probability > 0.01).ToList();

            DrawPoints(candidates);

        }


        double [,] GetHarrisMatrix(int windowSize, int width, int height)
        {
            double[,] harrisMat = new double[width, height];

            double[,] SobelX = CommonMath.GetSobelX(_pixels);
            double[,] SobelY = CommonMath.GetSobelY(_pixels);

            for (int y = 0; y < _pixels.GetLength(0); y++)
            {
                for (int x = 0; x < _pixels.GetLength(1); x++)
                {
                    double[,] mainWindow = GetMainWindow(windowSize, y, x); // Формируем исходное окно                  
                    double[,] gauss = ConvolutionMatrix.CountGaussMatrix(2);
                    
                    // Считаем матрицу H
                    double[,] currentMatrix = new double[2, 2];
                    for (int u = -(windowSize - 1); u <= (windowSize - 1); u++)
                    {
                        for (int v = -(windowSize - 1); v <= (windowSize - 1); v++)
                        {
                            double Ix = CommonMath.getPixel(SobelX, x + u, y + v, 3);
                            double Iy = CommonMath.getPixel(SobelY, x + u, y + v, 3);

                            double gaussPoint = CommonMath.getPixel(gauss, u + (windowSize - 1), 0 , 3) * CommonMath.getPixel(gauss, u + (windowSize - 1), v + (windowSize - 1), 3); 

                            currentMatrix[0, 0] += Math.Pow(Ix, 2) * gaussPoint;
                            currentMatrix[0, 1] += Ix * Iy * gaussPoint;
                            currentMatrix[1, 0] += Ix * Iy * gaussPoint;
                            currentMatrix[1, 1] += Math.Pow(Iy, 2) * gaussPoint;
                        }
                    }

                    double[] eigenvalues = getEigenvalues(currentMatrix);
                    harrisMat[y, x] = Math.Min(eigenvalues[0], eigenvalues[1]);

                }
            }

            return harrisMat;
        }
   
        static public double[] getEigenvalues(double[,] matrix) // Считаем собственные числа 
        {
            double[] eigenvalues = new double[2];

            double a = 1;
            double b = -matrix[0,0] - matrix[1,1];
            double c = matrix[0,0] * matrix[1,1] - matrix[0,1] * matrix[1,0];
            double d = Math.Pow(b,2) - 4 * a * c;
            if (Math.Abs(d) < 1e-4)
                d = 0;
            if (d < 0)
            {
                return eigenvalues;
            }

            eigenvalues[0] = (-b + Math.Sqrt(d)) / (2 * a);
            eigenvalues[1] = (-b - Math.Sqrt(d)) / (2 * a);

            return eigenvalues;
        }

        private static int[] dx = { -1, 0, 1, -1, 1, -1, 0, -1 };
        private static int[] dy = { -1, -1, -1, 0, 0, 1, 1, 1 };

        public static List<InterestingPoint> getCandidates(double[,] harrisMatrix, int width, int height)
        {
            List<InterestingPoint> candidates = new List<InterestingPoint>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bool ok = true;
                    double currentValue = harrisMatrix[i, j];
                    for (int k = 0; k < dx.GetLength(0) && ok; k++)
                    {
                        if (i + dx[k] < 0 ||
                            i + dx[k] >= width ||
                            j + dy[k] < 0 ||
                            j + dy[k] >= height) continue;
                        if (currentValue < CommonMath.getPixel(harrisMatrix, i + dx[k], j + dy[k],3))
                            ok = false;
                    }
                    if (ok)
                    {
                        candidates.Add(new InterestingPoint(i,j, currentValue));
                    }
                }
            }
            return candidates;
        }


        public double [,] GetMainWindow(int windowSize, int y, int x)
        {
            double[,] mainWindow = new double[windowSize, windowSize];
            for (int wy = 0; wy < windowSize; wy++)
                for (int wx = 0; wx < windowSize; wx++)
                    mainWindow[wy, wx] = CommonMath.getPixel(_pixels, y + wy, x + wx, 3);

            return mainWindow;
        }

        public void DrawPoints(double[,] DrawMatrix, int minValue)
        {
            image = Transformations.FromUInt32ToBitmap(outputPixel);
            Graphics graph = Graphics.FromImage(image);
            Pen pen = new Pen(Color.Blue);

            ByteWriteFile(DrawMatrix);

            for (int y = 0; y < DrawMatrix.GetLength(0); y++)
                for (int x = 0; x < DrawMatrix.GetLength(1); x++)
                {
                    if (DrawMatrix[y, x] > minValue)
                    {
                        graph.DrawEllipse(pen, x, y, 2, 2);
                    }
                }

            pictureBox2.Image = image;
        }

        public void DrawPoints(List<InterestingPoint> point)
        {
            image = Transformations.FromUInt32ToBitmap(outputPixel);
            Graphics graph = Graphics.FromImage(image);
            Pen pen = new Pen(Color.Blue);

           // ByteWriteFile(DrawMatrix);
           foreach (var interestingPoint in point)
           {
               graph.DrawEllipse(pen, interestingPoint.x - 1, interestingPoint.y - 1, 2, 2);
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
