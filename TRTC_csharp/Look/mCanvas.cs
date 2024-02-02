using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRTC_csharp.Look
{
    public class mCanvas
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Colour[,] PixelGrid;
        public mCanvas(int width, int height)
        {
            Width = width;
            Height = height;
            PixelGrid = new Colour[Width, Height];
            for (int column = 0; column < Width; column++)
            {
                for (int row = 0; row < Height; row++)
                {
                    PixelGrid[column, row] = Colour.Black;
                }
            }
        }

        public void WritePixel(mCanvas canvas, int x, int y, Colour colour)
        {
            canvas.PixelGrid[x, y] = colour;
        }
    }



}
