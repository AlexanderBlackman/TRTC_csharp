using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using TRTC_csharp.Look;

namespace TRTC_csharp.Tests.Look
{
    [TestClass]
    public class ColourTests
    {
        readonly double d = 0.00001; //our tolerance
        [TestMethod]
        public void VerifyTheyAreTupleTest()
        {
            // Arrange
            var colour = new Colour(-0.5, 0.4, 1.7);

            // Assert
            colour.red.Should().Be(-0.5);
            colour.green.Should().Be(0.4);
            colour.blue.Should().Be(1.7);
        }

        [TestMethod]
        public void AddingTests()
        {
            //Arrange
            var c1 = new Colour(0.9, 0.6, 0.75);
            var c2 = new Colour(0.7, 0.1, 0.25);
            //Act
            var result = c1 + c2;
            //Assert
            result.red.Should().Be(1.6);
            result.green.Should().Be(0.7);
            result.blue.Should().Be(1.0);
        }

        [TestMethod]
        public void SubtractionTests() 
        {
            //Arrange
            var c1 = new Colour(0.9, 0.6, 0.75);
            var c2 = new Colour(0.7, 0.1, 0.25);
            //Act
            var result = c1 - c2;
            //Assert
            //result.Red.Should().Be(0.2); This has a minute difference even after double checking method, strange
            result.red.Should().BeApproximately(0.2, d);
            result.green.Should().Be(0.5);
            result.blue.Should().Be(0.5);
        }
        [TestMethod]
        public void ScalarMultiplicationTests()
        {
            //Arrange
            var c = new Colour(0.2, 0.3, 0.4);
            var expected = new Colour(0.4, 0.6, 0.8);
            //Act
            var result = 2 * c;
            //Assert
            //result.Should().Be(new Colour(0.4, 0.6, 0.8));
            result.Should().BeEquivalentTo(expected);
        }
        [TestMethod]
        public void MultiplyColourByColour()
        {
            //Arrange
            var c1 = new Colour(1.0, 0.2, 0.4);
            var c2 = new Colour(0.9, 1.0, 0.1);
//            var expected = new Colour(0.9, 0.2, 0.04);
            //Act
            var result = c1 * c2;
            //Assert
            result.red.Should().BeApproximately(0.9, d);
            result.green.Should().BeApproximately(0.2, d);
            result.blue.Should().BeApproximately(0.04, d);
        }
    }
}
