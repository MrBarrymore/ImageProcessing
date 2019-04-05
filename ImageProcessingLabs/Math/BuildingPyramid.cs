using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageProcessingLabs
{
    class BuildingPyramid
    {

        public static int CountOctava(double[,] _pixels)
        {
            int octava = 0;

            while ((_pixels.GetLength(0) / Math.Pow(2, octava)) > 50 && (_pixels.GetLength(1) / Math.Pow(2, octava)) > 50)
            {
                octava++;
            }
            return octava;
        }

        public static double[,] BuildNewLevel(double[,] Pyramidpixels)
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
