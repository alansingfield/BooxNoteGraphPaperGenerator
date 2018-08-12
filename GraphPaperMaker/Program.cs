using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPaperMaker
{
    public static class Program
    {
        const int width = 1404;
        const int height = 1872;
        const string outputFolder = @"c:\temp\grids\";
        static readonly Color grey = Color.FromArgb(192, 192, 192);
        static readonly Color darkGrey = Color.FromArgb(118, 118, 118);
        static readonly Color veryDarkGrey = Color.FromArgb(44, 44, 44);

        // These are the pixel lines which are removed when the template is shrunk to fit on the Boox Note screen
        // (not full screen). Avoid plotting a graph line which hits one of these.
        static readonly List<int> wolfX = new List<int>()
            {
                10, 25, 41, 58, 75, 91, 108, 125, 142, 158, 175, 192, 208, 225, 242, 259, 275, 292, 309, 325, 342,
                359, 376, 392, 409, 426, 442, 459, 476, 493, 509, 526, 543, 559, 576, 593, 610, 626, 643, 660, 676,
                693, 710, 727, 743, 760, 777, 793, 810, 827, 844, 860, 877, 894, 910, 927, 944, 961, 977, 994, 1011,
                1027, 1044, 1061, 1078, 1094, 1111, 1128, 1144, 1161, 1178, 1195, 1211, 1228, 1245, 1261, 1278, 1295,
                1312, 1328, 1345, 1362, 1378, 1395, 
            };

        static readonly List<int> wolfY = new List<int>()
            {
                8 , 25, 42, 59, 75, 92, 109, 126, 143, 160, 177, 193, 210, 227, 244, 261, 278, 295, 311, 328, 345,
                362, 379, 396, 413, 430, 446, 463, 480, 497, 514, 531, 548, 564, 581, 598, 615, 632, 649, 666, 683,
                699, 716, 733, 750, 767, 784, 801, 817, 834, 851, 868, 885, 902, 919, 935, 952, 969, 986, 1003, 1020,
                1037, 1054, 1070, 1087, 1104, 1121, 1138, 1155, 1172, 1188, 1205, 1222, 1239, 1256, 1273, 1290, 1307,
                1323, 1340, 1357, 1374, 1391, 1408, 1425, 1441, 1458, 1475, 1492, 1509, 1526, 1543, 1559, 1576, 1593,
                1610, 1627, 1644, 1661, 1678, 1694, 1711, 1728, 1745, 1762, 1779, 1796, 1812, 1829, 1846, 1863, 1880
            };

        public static void Main()
        {
            drawGraphPaper();
            drawFeintWithMargin();
            drawFeint();
            drawMediumWithMargin();
            drawMedium();
        }

        private static void drawGraphPaper()
        {
            var bitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bitmap))
            {

                drawVerticalLines(g, 16f, 0.0f, grey);
                drawHorizontalLines(g, 16f, 0.0f, grey);

                drawVerticalLines(g, 80f, 16.0f, darkGrey);
                drawHorizontalLines(g, 80f, 16.0f, darkGrey);

                drawVerticalLines(g, 160f, 96.0f, veryDarkGrey);
                drawHorizontalLines(g, 160f, 96.0f, veryDarkGrey);
            }

            bitmap.Save($"{outputFolder}GraphPaper.png");
        }


        private static void drawFeintWithMargin()
        {
            var bitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bitmap))
            {
                var grey = Color.FromArgb(118, 118, 118);

                drawHorizontalLines(g, 64f, 208.0f, darkGrey);

                drawVerticalLines(g, 9999f, 170f, veryDarkGrey);
            }

            bitmap.Save($"{outputFolder}FeintWithMargin.png");
        }

        private static void drawFeint()
        {
            var bitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bitmap))
            {
                var grey = Color.FromArgb(118, 118, 118);

                drawHorizontalLines(g, 64f, 208.0f, darkGrey);
            }

            bitmap.Save($"{outputFolder}Feint.png");
        }


        private static void drawMediumWithMargin()
        {
            var bitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bitmap))
            {
                drawHorizontalLines(g, 80f, 208.0f, darkGrey);

                drawVerticalLines(g, 9999f, 170f, veryDarkGrey);
            }

            bitmap.Save($"{outputFolder}MediumWithMargin.png");
        }

        private static void drawMedium()
        {
            var bitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bitmap))
            {
                var grey = Color.FromArgb(118, 118, 118);

                drawHorizontalLines(g, 80f, 208.0f, darkGrey);
            }

            bitmap.Save($"{outputFolder}Medium.png");
        }
        
        private static void drawHorizontalLines(Graphics g, float spacing, float offsetY, Color color)
        {
            var pen = new Pen(color);

            for (float y = offsetY; y < height; y += spacing)
            {
                int fudgedY = (int)Math.Round(y);
                if (wolfY.Contains(fudgedY))
                {
                    if (y > fudgedY)
                        fudgedY++;
                    else
                        fudgedY--;
                }

                g.DrawLine(pen, 0, fudgedY, width, fudgedY);
            }
        }

        private static void drawVerticalLines(Graphics g, float spacing, float offsetX, Color color)
        {
            var pen = new Pen(color);

            for (float x = offsetX; x < height; x += spacing)
            {
                int fudgedX = (int)Math.Round(x);
                if (wolfX.Contains(fudgedX))
                {
                    if (x > fudgedX)
                        fudgedX++;
                    else
                        fudgedX--;
                }

                g.DrawLine(pen, fudgedX, 0, fudgedX, height);
            }
        }

        private static void drawTroubleshooterX()
        {
            var bitmap = new Bitmap(width, height);
            var pen = new Pen(Color.FromArgb(166, 166, 166));

            using (var g = Graphics.FromImage(bitmap))
            {

                for (int x = 0; x < width; x += 16)
                {
                    for (int offs = 0; offs < 16; offs++)
                    {
                        int y1 = offs * 100;
                        int y2 = y1 + 100;

                        g.DrawLine(pen, x + offs, y1, x + offs, y2);
                    }
                }

            }

            bitmap.Save($"{outputFolder}troubleshooterX.png");
        }

        private static void drawTroubleshooterY()
        {
            var bitmap = new Bitmap(width, height);
            var pen = new Pen(Color.FromArgb(166, 166, 166));

            using (var g = Graphics.FromImage(bitmap))
            {

                for (int y = 0; y < height; y += 16)
                {
                    for (int offs = 0; offs < 16; offs++)
                    {
                        int x1 = offs * 85;
                        int x2 = x1 + 85;

                        //g.DrawLine(pen, x + offs, y1, x + offs, y2);
                        g.DrawLine(pen, x1, y + offs, x2, y + offs);
                    }
                }

            }

            bitmap.Save($"{outputFolder}troubleshooterY.png");
        }


        private static void drawTroubleshooterQ()
        {
            var bitmap = new Bitmap(width, height);
            var pen = new Pen(Color.FromArgb(166, 166, 166));

            using (var g = Graphics.FromImage(bitmap))
            {
                foreach(var x in wolfX)
                {
                    g.DrawLine(pen, x, 0, x, height);
                }
            }

            bitmap.Save($"{outputFolder}troubleshooterQ.png");
        }


        private static void drawTroubleshooterP()
        {
            var bitmap = new Bitmap(width, height);
            var pen = new Pen(Color.FromArgb(166, 166, 166));


            using (var g = Graphics.FromImage(bitmap))
            {
                foreach (var y in wolfY)
                {
                    g.DrawLine(pen, 0, y, width, y);
                }
            }

            bitmap.Save($"{outputFolder}troubleshooterP.png");
        }
    }
}
