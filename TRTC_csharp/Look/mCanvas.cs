using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRTC_csharp.Look
{
    public class mCanvas
    {
        public int Width { get; set; }
        public int Height { get; set; }


        public Colour[,] PixelGrid;  //I have put it in its own variable, to keep methods seperate.
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

        public void WritePixel(int x, int y, Colour colour) => this.PixelGrid[x, y] = colour;
        
        public string ConvertDoubleToClampedString(Double value, int max = 255)
        {

            Double scaledToMax = value * max;
            if (scaledToMax < 0) { scaledToMax = 0; }
            if (scaledToMax > max) { scaledToMax = max; }

            return scaledToMax.ToString("0");
        }

        public string GeneratePPMFromColours(Colour colour, int max = 255) =>
            ConvertDoubleToClampedString(colour.red, max) + "\n" +
            ConvertDoubleToClampedString(colour.green, max) + "\n" +
            ConvertDoubleToClampedString(colour.blue, max) +"\n";



        public string GenerateListOfColourInfo(int max = 255)
        {
            StringBuilder list = new StringBuilder();

            for (int row = 0; row < Height -1; row++)
            {
                {
                    for (int column = 0; column < Width -1; column++)
                    {
                        list.AppendLine(GeneratePPMFromColours(this.PixelGrid[row, column], max));
                        //Debug.WriteLine(GeneratePPMFromColours(this.PixelGrid[row, column], max));
                    }
                    list.AppendLine();

                }
            }
            return list.ToString();
        }






    //    public string GeneratePPMFromColours(Colour colour, string line, int max = 255) =>
    //line +=  ((line + ConvertDoubleToClampedString(colour.red, max))  < 70) + " " +
    // ConvertDoubleToClampedString(colour.green, max) + " " +
    //ConvertDoubleToClampedString(colour.blue, max);



    //    public string makeLinesFromColours(Colour colour, string line, int max =255)
    //    {
    //        if (line.Length < 59) { line += GeneratePPMFromColours(colour); }

        


        public string ConvertToPPM()
        {            
            StringBuilder export = new("P3");
            export.Append($"\n{this.Width} {this.Height}");
            export.Append("\n255");
            
            StringBuilder line = new StringBuilder();

            using (StreamReader reader = new StreamReader(GenerateListOfColourInfo()))
            { 
            while (reader.ReadLine() != null)
                {
                    while ((line.Length + reader.ReadLine().Length <= 70) 
                            && (reader.ReadLine() != String.Empty))
                        {
                            line.Append(reader.ReadLine() );
                            if ( line.Length != 70) line.Append(" ");
                        }
                    export.AppendLine(line.ToString());
                    line = new StringBuilder();
                    }
    }
                return export.ToString();
            }




            }




        }


    
    /* Notes, I wanted to add an optional init paramater for Colour, but unfortunately it isn't the necessary "compile time constant"
     Too lazy for additional methods 
    Maybe I should convert the doubles Colour by Colour? 
    Line length matters more than keeping colours together, I should do it by individual subcolour
     */



