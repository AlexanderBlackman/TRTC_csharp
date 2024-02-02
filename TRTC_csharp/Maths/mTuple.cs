using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TRTC_csharp.Maths
{
    public  class mTuple
    {
        public mTuple(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public double x { get;  set; }
        public double y { get;  set; }
        public double z { get;  set; }
        public double w { get;  set; }

        public bool IsPoint() => w.Equals(1.0);
        public bool IsVector() => w.Equals(0.0);

        #region operator overrides
        public static mTuple operator +(mTuple a, mTuple b) =>
            new mTuple( a.x + b.x, 
                        a.y + b.y, 
                        a.z + b.z, 
                        Math.Max(a.w, b.w));
        public static mTuple operator -(mTuple a)=>
            new mTuple (-a.x, -a.y, -a.z, -a.w);

        public static mTuple operator *(double a, mTuple b)=>
            new mTuple(a * b.x, a * b.y, a* b.z, a* b.w);
        public static mTuple operator *(mTuple b, double a) =>
            new mTuple(a * b.x, a * b.y, a * b.z, a * b.w);

        public static mTuple operator /(mTuple t, double d)=>
            new mTuple(t.x / d, t.y / d, t.z / d, t.w / d);
        

        public static double Dot(mTuple a, mTuple b)=>
            (a.x * b.x) + (a.y * b.y) + (a.z * b.z) + (a.w * b.w);


        #endregion

        public override string ToString() => $"({x},{y},{z},{w})";

    }
    public class mVector: mTuple
    {
        public mVector(double x, double y, double z)
            :base(x,y,z, 0.0) { }

        public mVector(mTuple t) : base(t.x, t.y, t.z, 0.0) { }

        public static mVector operator - (mVector a, mVector b)=>
             new mVector(a.x - b.x,
                 a.y - b.y,
                 a.z - b.z);

        public static mVector operator +(mVector a, mVector b) =>
                new mVector(a.x + b.x,
                a.y + b.y,
                a.z + b.z);

        public static mVector Cross(mVector a, mVector b)
        {
            return new mVector((a.y * b.z) - (a.z * b.y),
                               (a.z * b.x) - (a.x * b.z),
                               (a.x * b.y) - (a.y * b.x));
        }

        public double Magnitude()
        {
            return Math.Sqrt(
                    (this.x * this.x) +
                    (this.y * this.y) +
                    (this.z * this.z) +
                    (this.w * this.w));
        }
        //Why is this a mTuple?
        //public mTuple Normalised()
        //{
        //    return new mTuple(  (this.x / this.Magnitude()), 
        //                        (this.y / this.Magnitude()), 
        //                        (this.z / this.Magnitude()),
        //                        (this.w / this.Magnitude()));
        //}

        public mVector Normalised()
        {
            return new mVector( (this.x / this.Magnitude()),//This seems to work for mags above 1 but not below.
                                (this.y / this.Magnitude()),
                                (this.z / this.Magnitude()));
        }

        public override string ToString() => $"{x},{y},{z}";
    }

    public class mPoint: mTuple
    {
        public mPoint(double x, double y, double z)
            :base(x,y,z, 1.0) { }
        public mPoint(mTuple t) : base(t.x, t.y, t.z, 1.0) { }

        public static mVector operator -(mPoint a, mPoint b) =>
             new mVector(a.x - b.x,
                 a.y - b.y,
                 a.z - b.z);


        public static mPoint operator -(mPoint a, mVector b) =>
         new mPoint(a.x - b.x,
            a.y - b.y,
            a.z - b.z);

        public static mPoint operator +(mPoint a, mVector b) =>
            new mPoint(a.x + b.x,
                        a.y + b.y,
                        a.z + b.z);
        public override string ToString() => $"{x},{y},{z}";
    }
}
