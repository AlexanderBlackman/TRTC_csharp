using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TRTC_csharp.Maths;

namespace TRTC_csharp.Tests.Maths
{
    [TestClass]
    public class mTupleTests
    {
        readonly double d = 0.00001; //our tolerance
        [TestMethod]
        public void TestAsPoint()
        {
            // Arrange
            var p = new mTuple(4.3, -4.2, 3.1, 1.0);


            // Assert
            p.Should().BeOfType<mTuple>();
            p.x.Should().Be(4.3);
            p.y.Should().Be(-4.2);
            p.z.Should().Be(3.1);
            p.w.Should().Be(1.0);
            p.IsPoint().Should().BeTrue();
            p.IsVector().Should().BeFalse();
        }

        [TestMethod]
        public void TestAsVector()
        {
            // Arrange
            var v = new mTuple(4.3, -4.2, 3.1, 0.0);
            v.Should().BeOfType<mTuple>();
            v.x.Should().Be(4.3);
            v.y.Should().Be(-4.2);
            v.z.Should().Be(3.1);
            v.w.Should().Be(0.0);
            v.IsVector().Should().BeTrue();
            v.IsPoint().Should().BeFalse();
        }

        [TestMethod]
        public void TestIfVectorSubclassEqualsVectorTuple()
        {
            // Arrange
            var v = new mVector(4.3, -4.2, 3.1);
            var t = new mTuple(4.3, -4.2, 3.1, 0.0);

            // Assert
            t.Should().BeOfType<mTuple>();
            v.Should().BeOfType<mVector>();
            v.x.Should().Be(t.x);
            v.y.Should().Be(t.y);
            v.z.Should().Be(t.z);
            v.w.Should().Be(t.w);
        }


        [TestMethod]
        public void TestIfTuplesCanBeAdded()
        {
            //Arrange 
            var a1 = new mTuple(3, -2, 5, 1);
            var a2 = new mTuple(-2, 3, 1, 0);
            //Act
            mTuple result = a1 + a2;
            //Assert
            result.x.Should().Be(1.0);
            result.y.Should().Be(1.0);
            result.z.Should().Be(6.0);
            result.w.Should().Be(1.0);
        }

        [TestMethod]
        public void TestIfPointsCanBeSubtracted()
        {
            //Arrange
            var p1 = new mPoint(3, 2, 1);
            var p2 = new mPoint(5, 6, 7);
            //Act
            var result = p1 - p2;
            //Arrange
            result.Should().BeOfType<mVector>();
            result.x.Should().Be(-2.0);
            result.y.Should().Be(-4.0);
            result.z.Should().Be(-6.0);
            result.w.Should().Be(0.0);
        }
        [TestMethod]
        public void TestIfVectorsCanBeSubtractedFromPoints()
        {
            //Arrange
            var p1 = new mPoint(3, 2, 1);
            var v1 = new mVector(5, 6, 7);
            //Act
            var result = p1 - v1;
            result.Should().BeOfType<mPoint>();
            result.x.Should().Be(-2.0);
            result.y.Should().Be(-4.0);
            result.z.Should().Be(-6.0);
            result.w.Should().Be(1.0);
        }

        [TestMethod]
        public void TestIfVectorsCanBeSubtractedFromVectors()
        {
            //Arrange
            var v1 = new mVector(3, 2, 1);
            var v2 = new mVector(5, 6, 7);
            //Act
            var result = v1 - v2;
            //Assert
            result.Should().BeOfType<mVector>();
            result.x.Should().Be(-2.0);
            result.y.Should().Be(-4.0);
            result.z.Should().Be(-6.0);
            result.w.Should().Be(0.0);
        }

        [TestMethod]
        public void NegatingATupleTest()
        {
            //Arrange
            var t1 = new mTuple(1, -2, 3, -4);
            //Act
            var result = -t1;
            //Assert
            result.Should().BeAssignableTo<mTuple>();
            result.x.Should().Be(-1);
            result.y.Should().Be(2);
            result.z.Should().Be(-3);
            result.w.Should().Be(4);
        }

        [TestMethod]
        public void MultiplyTupleByScalar()
        {
            //Arrange
            var m = 3.5;
            var t = new mTuple(1.0, -2.0, 3.0, -4.0);
            //Act
            var result1 = m * t;
            var result2 = t * m;
            //Assert
            result1.Should().BeEquivalentTo(result2);
            result2.Should().BeEquivalentTo(result1);

            result1.x.Should().Be(3.5);
            result1.y.Should().Be(-7);
            result1.z.Should().Be(10.5);
            result1.w.Should().Be(-14);
        }

        [TestMethod]
        public void MultiplyVectorByScalar()
        {
            //Arrange
            var m = 3.5;
            var t = new mVector(1.0, -2.0, 3.0);
            //Act
            var result1 = m * t;
            var result2 = t * m;
            //Assert
            result1.Should().BeEquivalentTo(result2);
            result2.Should().BeEquivalentTo(result1);

            result1.x.Should().Be(3.5);
            result1.y.Should().Be(-7);
            result1.z.Should().Be(10.5);
            result1.w.Should().Be(0);
        }

        [TestMethod]
        public void DivideTupleByScalar()
        {
            //Arrange
            var d = 2;
            var t = new mTuple(1, -2, 3, -4);
            //Act
            var result = t / d;

            result.Should().BeAssignableTo<mTuple>();
            result.x.Should().Be(0.5);
            result.y.Should().Be(-1);
            result.z.Should().Be(1.5);
            result.w.Should().Be(-2);
        }

        [TestMethod]
        public void MagnitudeOfVector()
        {
            //Arrange
            var v1 = new mVector(1,0,0);
            var v2 = new mVector(0,1,0);
            var v3 = new mVector(0,0,1);
            var v4 = new mVector(1,2,3);
            var v5 = new mVector(-1,-2,-3);

            //Assert
            v1.Magnitude().Should().Be(1);
            v2.Magnitude().Should().Be(1);
            v3.Magnitude().Should().Be(1);
            v4.Magnitude().Should().Be(Math.Sqrt(14));
            v5.Magnitude().Should().Be(Math.Sqrt(14));  
        }

        [TestMethod]
        public void NormaliseVectorTest()
        {
            var v1 = new mVector(4,0,0);
            var target1 = new mVector(1, 0, 0);

            var v2 = new mVector(1, 2, 3);
            var target2 = new mVector(
               1/  Math.Sqrt(14), 2 /Math.Sqrt(14), 3 / Math.Sqrt(14));

            //Act
            var r1 = v1.Normalised();
            var r2 = v2.Normalised();

            //Assert
            r1.Should().BeOfType<mVector>();
            r2.Should().BeOfType<mVector>();

            r1.x.Should().BeApproximately(target1.x, d);
            r1.y.Should().BeApproximately(target1.y, d);
            r1.z.Should().BeApproximately(target1.z, d);
            
            r2.x.Should().BeApproximately(target2.x, d);
            r2.y.Should().BeApproximately(target2.y, d);
            r2.z.Should().BeApproximately(target2.z, d);
        }
        [TestMethod]
        public void GetMagnitudeOfNormalisedVectorTest()
        {
            //Arrange
            var v1 = new mVector(1,2,3);
            //Act
            var norm = v1.Normalised();
            var mag = norm.Magnitude();
            //Assert
            mag.Should().Be(1);
        }
        [TestMethod]
        public void GetDotOfTwoTuplesTest()
        {
            //Arrange
            var v1 = new mVector(1, 2, 3);
            var v2 = new mVector(2, 3, 4);
            //Act
            var result = mTuple.Dot(v1, v2);
            //Assert
            result.Should().Be(20);
        }
        [TestMethod]
        public void GetCrossOfTwoVectorsTest()
        {
            //Arrange
            var a = new mVector(1, 2, 3);
            var b = new mVector(2, 3, 4);
            //Act
            var AxB = mVector.Cross(a, b);
            var BxA = mVector.Cross(b, a);
            var Combined= AxB + BxA;
            //Assert
            AxB.x.Should().Be(-1);
            AxB.y.Should().Be(2);
            AxB.z.Should().Be(-1);

            BxA.x.Should().Be(1);
            BxA.y.Should().Be(-2);
            BxA.z.Should().Be(1);

            Combined.x.Should().Be(0);
            Combined.y.Should().Be(0);
            Combined.z.Should().Be(0);
        }

    }
}




