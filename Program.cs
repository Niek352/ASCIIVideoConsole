using System.Drawing;
using ASCIIVideoConsole;
using MediaFileProcessor.Models.Common;
using MediaFileProcessor.Models.Enums;
using MediaFileProcessor.Processors;

Console.WriteLine();
var pathToFile = Console.ReadLine();
var asci = new ASCIIConverter();
var videoProcessor = new VideoFileProcessor();

await using var stream = new FileStream(pathToFile, FileMode.Open);
var resultMultiStream = await videoProcessor.ConvertVideoToImagesAsStreamAsync(new MediaFile(stream), FileFormatType.JPG);

var data = resultMultiStream.ReadAsStreamArray();


for (int i = 0; i < data.Length; i++)
{
	var bitmap = new Bitmap(data[i]);
	asci.DrawBitmap(bitmap);
}
