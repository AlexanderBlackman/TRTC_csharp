using FluentAssertions.Equivalency;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using TRTC_csharp.Look;

namespace TRTC_csharp.Tests.Look
{
    [TestClass]
    public class mCanvasTests
    {




        [TestMethod]
        public void CreateCanvasTest()
        {
            // Arrange
            var canvas = new mCanvas(10, 20);           
            


            // Assert
            canvas.Width.Should().Be(10);
            canvas.Height.Should().Be(20);
            foreach (var pixel in canvas.PixelGrid)
            {
                pixel.Should().Be(Colour.Black);
            }
        }

        [TestMethod]
        public void WritePixelTest()
        {
            // Arrange
            var canvas = new mCanvas(10, 20);
            // Act
            canvas.WritePixel(2, 3, Colour.Red);
            // Assert
            canvas.PixelGrid[2, 3].Should().Be(Colour.Red);
            canvas.PixelGrid[9,9].Should().Be(Colour.Black);
        }


        [TestMethod]
        public void GenerateStringFromDoubleTest()
        {
            //Arrange
            var c1 = new Colour(1.5, 0.5, -1);
            var canvas = new mCanvas(2, 2);
            //Act
            var r1 = canvas.ConvertDoubleToClampedString(c1.red);
            var g1 = canvas.ConvertDoubleToClampedString(c1.green);
            var b1 = canvas.ConvertDoubleToClampedString(c1.blue);
            //Assert
            r1.Should().Be("255");
            g1.Should().Be("128");
            b1.Should().Be("0");

        }

        [TestMethod]
        public void GenerateThreeLineStringFromColourTest()
        {
            //Arrange
            var c1 = new Colour(1.5, 0.5, -1);
            var canvas = new mCanvas(2, 2);
            var expected = "255\n128\n0\n";
            //Act
            var r1 = canvas.GeneratePPMFromColours(c1);
            //Assert
            r1.Should().Be(expected);
        }


        [TestMethod]
        public void ConstructPPMHeaderTest()
        {
            //Arrange
            var canvas = new mCanvas(5, 3);
            var expected = "P3\n5 3\n255";
            //Act
            var header = canvas.ConvertToPPM();
            //Assert
            header.Should().Be(expected);

        }



        [TestMethod]
        public void ConstructPPMPixelDataTest()
        {
            //Arrange
            var canvas = new mCanvas(5, 3);
            var c1 = new Colour(1.5, 0, 0);
            var c2 = new Colour(0, 0.5, 0);
            var c3 = new Colour(-0.5, 0, 1);

            //Act
            canvas.WritePixel(0, 0, c1);
            canvas.WritePixel(2, 1, c2);
            canvas.WritePixel(4, 2, c3);
            var result = canvas.ConvertToPPM();

            //Assert
            



        }
        [TestMethod]
        public void ColourInfoListGenerationTest()
        {
            //Arrange
            var canvas = new mCanvas(3, 3);
            //Act
            var myList = canvas.GenerateListOfColourInfo();
            //Assert

        }

    }
}
