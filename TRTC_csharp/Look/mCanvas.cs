using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
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

      public void WriteAllPixels(Colour colour)
      {
         for (int column = 0; column < Width; column++)
         {
            for (int row = 0; row < Height; row++)
            {
               PixelGrid[column, row] = colour;
            }

         }
      }
        
        public string ConvertDoubleToClampedString(Double value, int max = 255)
        {

            Double scaledToMax = value * max;
            if (scaledToMax < 0) { scaledToMax = 0; }
            if (scaledToMax > max) { scaledToMax = max; }

            return scaledToMax.ToString("0");
        }

        public string GeneratePPMFromColours(Colour colour, int max = 255) =>
            ConvertDoubleToClampedString(colour.red, max) + " " +
            ConvertDoubleToClampedString(colour.green, max) + " " +
            ConvertDoubleToClampedString(colour.blue, max) + " ";

      

      public Queue<string> GenerateStringQueueFromCanvas()
      {
         Queue<string> queue = new Queue<string>();
         for (int y = 0; y < Height; y++)
         {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < Width; x++)
            {
               sb.Append (GeneratePPMFromColours(PixelGrid[x, y]));
            }
            queue.Enqueue(sb.ToString().TrimEnd());
         }
         return queue;
      }

      public Queue<string> CreateQueueOfSpecifiedLineLength(Queue<string> inputedSQ, int maxLength = 70, int atomMaxLength = 3) 
      {
         var result = new Queue<string>();
         while (inputedSQ.Count > 0)
         {
            string rowString = inputedSQ.Dequeue();
            while (rowString.Length > maxLength)
            {
               int potentialMax = maxLength - atomMaxLength;
               for (int i = potentialMax; i < rowString.Length && i < maxLength; i++)
               {
                  if (rowString[i] == ' ')
                  {
                     result.Enqueue(rowString.Substring(0, i));
                     rowString = rowString.Substring(i + 1);
                     break;
                  }
               }
            }
            result.Enqueue(rowString);
         }
         return result;

      }
    public string ConstructHeader(int max = 255)
        {
            StringBuilder export = new("P3\r\n");
            export.AppendLine($"{this.Width} {this.Height}");
            export.AppendLine($"{max}");

            return export.ToString();
        }
        public string ConstructBody(int max = 255)
        {
         StringBuilder export = new();
         Queue<string> validLines = CreateQueueOfSpecifiedLineLength(GenerateStringQueueFromCanvas());
         foreach (string line in validLines)
         {
            export.AppendLine(line);
         }

            return export.ToString();
        }

        public string ConvertToPPMString()=>
            ConstructHeader() +  ConstructBody();

      public void ExportToPPMFile()
      {
         using (StreamWriter output = new (@"D://myFirst.PPM"))
         {
            output.Write(ConvertToPPMString());
         }
      }
            }

        }


    
    /* Notes, I wanted to add an optional init paramater for Colour, but unfortunately it isn't the necessary "compile time constant"
     Too lazy for additional methods 
    Maybe I should convert the doubles Colour by Colour? 
    Line length matters more than keeping colours together, I should do it by individual subcolour
    It seems the list of colourinfo needs to completed before it can be read by streamreader. WRONG! It's reading it as a path not a string.
    Would a queue work better than a string?  Or an array for line strings. */



