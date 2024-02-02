using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRTC_csharp.Maths;

namespace TRTC_csharp.Look
{
    public class Colour
    {
        public double red { get; set; }
        public double green { get; set; }
        public double blue { get; set; }

        public Colour(double red, double green, double blue) 
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        #region operator overloads
        public static Colour operator +(Colour a, Colour b)=>
            new Colour ((a.red + b.red), (a.green + b.green), (a.blue + b.blue));
        public static Colour operator -(Colour a, Colour b)=>
            new Colour ((a.red - b.red), (a.green - b.green), (a.blue - b.blue));
        public static Colour operator * (Colour c, double scalar) =>
            new Colour ((c.red * scalar),(c.green * scalar), (c.blue * scalar));
        public static Colour operator *(double scalar, Colour c) =>
            new Colour((c.red * scalar), (c.green * scalar), (c.blue * scalar));
        public static Colour operator *(Colour a, Colour b)=>
            new Colour((a.red * b.red), (a.green * b.green), (a.blue * b.blue));
        #endregion
        #region PresetColours
        public static readonly Colour Black = new Colour(0, 0, 0);
        public static readonly Colour White = new Colour(1, 1, 1);
        public static readonly Colour Red = new Colour(1, 0, 0);
        public static readonly Colour Green = new Colour(0, 1, 0);
        public static readonly Colour Blue = new Colour(0, 0, 1);
        public static readonly Colour Cyan = new Colour(0, 1, 1);
        public static readonly Colour Yellow = new Colour(1, 1, 0);
        public static readonly Colour Purple = new Colour(1, 0, 1);
        #endregion



    }
}
