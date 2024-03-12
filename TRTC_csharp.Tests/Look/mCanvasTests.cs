using FluentAssertions.Equivalency;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
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
         var pixelCount = 0;

         // Assert
         canvas.Width.Should().Be(10);
         canvas.Height.Should().Be(20);
         foreach (var pixel in canvas.PixelGrid)
         {
            pixel.Should().Be(Colour.Black);
            pixelCount++;
         }
         pixelCount.Should().Be(200);
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
         canvas.PixelGrid[9, 9].Should().Be(Colour.Black);
      }

      [TestMethod]
      public void WriteAllPixelTest()
      {
         //Arrange
         var canvas = new mCanvas(10,10);
         //Act
         canvas.WriteAllPixels(Colour.Red);
         //Assert
         canvas.PixelGrid[0, 0].Should().Be(Colour.Red);
         canvas.PixelGrid[9, 9].Should().Be(Colour.Red);
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
      public void GenerateStringFromColourTest()
      {
         //Arrange
         var c1 = new Colour(1.5, 0.5, -1);
         var canvas = new mCanvas(2, 2);
         var expected = "255 128 0 ";
         //Act
         var r1 = canvas.GeneratePPMFromColours(c1);
         //Assert
         r1.Should().Be(expected);
      }
      [TestMethod]
      public void GenerateStringFromCanvasCoordinates()
      {
         //Arrange
         var canvas = new mCanvas(3, 3);
         var expectedPurple = "255 0 255 ";
         var expectedBlack = "0 0 0 ";
         canvas.WritePixel(1, 1, new Colour(1, 0, 1));
         //Act
         var r1 = canvas.GeneratePPMFromColours(canvas.PixelGrid[0, 0]);
         var r2 = canvas.GeneratePPMFromColours(canvas.PixelGrid[1, 1]);
         var r3 = canvas.GeneratePPMFromColours(canvas.PixelGrid[2, 2]);
         //Assert
         r1.Should().Be(expectedBlack);
         r2.Should().Be(expectedPurple);
         r3.Should().Be(expectedBlack);
      }
      [TestMethod]
      public void GenerateShortStringQueueFromGridRowTest()
      {
         //Arrange
         var canvas1 = new mCanvas(10, 1);
         var canvas2 = new mCanvas(10, 1);
         canvas2.WriteAllPixels(Colour.White);
         //Act
         var result1 = canvas1.GenerateStringQueueFromCanvas().Dequeue();
         var result2 = canvas2.GenerateStringQueueFromCanvas().Dequeue();
         //Assert
         result1.Length.Should().Be(59);//"0 " X 3 X 10 - last space        
         result2.Length.Should().Be(119);//"255 " X 3 X 10 - last space
      }

      [TestMethod]
      public void RowStringsShouldBeSplitIntoValidLinesOf70charOrLess()
      {
         //Arrange
         var canvas = new mCanvas(10, 2);
         canvas.WriteAllPixels(Colour.White);
         var rowStringQueue = canvas.GenerateStringQueueFromCanvas();
         //Act
         Queue<String> shortenedStringQueue = canvas.CreateQueueOfSpecifiedLineLength(rowStringQueue);
         //Assert
         shortenedStringQueue.Count.Should().Be(4);
         string firstString = shortenedStringQueue.Dequeue();
         firstString.Should().NotStartWith(" ");
         firstString.Should().NotEndWith(" ");

         firstString.Length.Should().Be(67);
         shortenedStringQueue.Dequeue().Length.Should().Be(51);
         shortenedStringQueue.Dequeue().Length.Should().Be(67);
         shortenedStringQueue.Dequeue().Length.Should().Be(51);
      }

      [TestMethod]
      public void MonsterRowStringsShouldBeSplitIntoValidLinesOf70charOrLess()
      {
         //Arrange
         var canvas = new mCanvas(20, 1);
         canvas.WriteAllPixels(Colour.Cyan);
         var rowStringQueue = canvas.GenerateStringQueueFromCanvas();
         //Act
         Queue<String> shortenedStringQueue = canvas.CreateQueueOfSpecifiedLineLength(rowStringQueue);
         //Assert
         shortenedStringQueue.Count.Should().Be(3);
         shortenedStringQueue.Dequeue().Length.Should().Be(69);
         shortenedStringQueue.Dequeue().Length.Should().Be(69);
         shortenedStringQueue.Dequeue().Length.Should().Be(59);
      }

         [TestMethod]
        public void ConstructPPMHeaderTest()
        {
            //Arrange
            var canvas = new mCanvas(5, 3);
            var expected =
            """
            P3
            5 3
            255

            """;

            //Act
            var header = canvas.ConstructHeader();
            //Assert
            header.Should().Be(expected);
        }

        [TestMethod]
        public void ConstructPPMBodyTest()
        {
            //Arrange
            var canvas = new mCanvas(5, 3);
            var c1 = new Colour(1.5, 0, 0);
            var c2 = new Colour(0, 0.5, 0);
            var c3 = new Colour(-0.5, 0, 1);
            var expected =
                """
                255 0 0 0 0 0 0 0 0 0 0 0 0 0 0
                0 0 0 0 0 0 0 128 0 0 0 0 0 0 0
                0 0 0 0 0 0 0 0 0 0 0 0 0 0 255

                """;

            //Act
            canvas.WritePixel(0, 0, c1);
            canvas.WritePixel(2, 1, c2);
            canvas.WritePixel(4, 2, c3);
            var result = canvas.ConstructBody();
            //Assert
            result.Should().Be(expected);
        }




        [TestMethod]
        public void ConstructPPMPixelDataTest()
        {
            //Arrange
            var canvas = new mCanvas(5, 3);
            var c1 = new Colour(1.5, 0, 0);
            var c2 = new Colour(0, 0.5, 0);
            var c3 = new Colour(-0.5, 0, 1);


         var expected =
         """
            P3
            5 3
            255
            255 0 0 0 0 0 0 0 0 0 0 0 0 0 0
            0 0 0 0 0 0 0 128 0 0 0 0 0 0 0
            0 0 0 0 0 0 0 0 0 0 0 0 0 0 255

            """;

            //Act
            canvas.WritePixel(0, 0, c1);
            canvas.WritePixel(2, 1, c2);
            canvas.WritePixel(4, 2, c3);
            var result = canvas.ConvertToPPMString();
         //Assert
         result.Should().Be(expected);
        }


    }
}
