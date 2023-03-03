using System.Diagnostics;
using System.Drawing;
using ASCIIVideoConsole;
using MediaFileProcessor.Models.Common;
using MediaFileProcessor.Models.Enums;
using MediaFileProcessor.Processors;

var pathToFile = Console.ReadLine().Replace('"', ' ').Trim();
Console.WriteLine(pathToFile);
var asci = new ASCIIConverter(40, 40);
var videoProcessor = new VideoFileProcessor();

await using var stream = new FileStream(pathToFile, FileMode.Open);
var resultMultiStream = await videoProcessor.ConvertVideoToImagesAsStreamAsync(new MediaFile(stream), FileFormatType.JPG);
var data = resultMultiStream.ReadAsStreamArray();


var stopWatch = new Stopwatch();
foreach (var t in data)
{
	stopWatch.Start();
	var bitmap = new Bitmap(t);
	asci.DrawBitmap(bitmap);
	Console.Title = stopWatch.ElapsedMilliseconds.ToString();
	stopWatch.Reset();
}
