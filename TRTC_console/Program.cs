using TRTC_csharp.Maths;
using TRTC_csharp.Look;


var canvas = new mCanvas(5, 3);
var c1 = new Colour(1.5, 0, 0);
var c2 = new Colour(0, 0.5, 0);
var c3 = new Colour(-0.5, 0, 1);

//Act
canvas.WritePixel(0, 0, c1);
canvas.WritePixel(2, 1, c2);
canvas.WritePixel(4, 2, c3);
var result = canvas.ConvertToPPM();

Console.WriteLine(result);

