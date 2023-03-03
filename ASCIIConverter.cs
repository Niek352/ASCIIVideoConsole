using System.Drawing;

namespace ASCIIVideoConsole;

public class ASCIIConverter
{
	private readonly int _height;
	private readonly int _width;
	private const string BLACK = "@";
	private const string CHARCOAL = "#";
	private const string DARKGRAY = "8";
	private const string MEDIUMGRAY = "&";
	private const string MEDIUM = "o";
	private const string GRAY = ":";
	private const string SLATEGRAY = "*";
	private const string LIGHTGRAY = ".";
	private const string WHITE = " ";
	
	public ASCIIConverter(int height, int width)
	{
		_height = height;
		_width = width;
	}
	
	public void DrawBitmap(Bitmap bitmap)
	{
		bitmap = GrayScaleFilter (new Bitmap(bitmap, _width, _height)) ;
		var width = bitmap.Width;
		var height = bitmap.Height;
		Console.SetCursorPosition(0,0);

		for (var i = 0; i < height; i++)
		{
			for (var j = 0; j < width; j++)
			{
				var pixel = bitmap.GetPixel(j, i);
				FastConsole.Write(GetGrayShade(pixel.R));
			}
			FastConsole.Write("\n");
		}
		FastConsole.Flush();
		Thread.Sleep(5);
	}
	
	public Bitmap GrayScaleFilter(Bitmap image)
	{
		Bitmap grayScale = new Bitmap(image.Width, image.Height);

		for (Int32 y = 0; y < grayScale.Height; y++)
			for (Int32 x = 0; x < grayScale.Width; x++)
			{
				Color c = image.GetPixel(x, y);

				Int32 gs = (Int32)(c.R * 0.3f + c.G * 0.59f + c.B * 0.11f);

				grayScale.SetPixel(x, y, Color.FromArgb(gs, gs, gs));
			}
		return grayScale;
	}
	
	private static string GetGrayShade(int redValue)
	{
		string asciival = " ";

		if (redValue >= 230)
		{
			asciival = WHITE;
		}
		else if (redValue >= 200)
		{
			asciival = LIGHTGRAY;
		}
		else if (redValue >= 180)
		{
			asciival = SLATEGRAY;
		}
		else if (redValue >= 160)
		{
			asciival = GRAY;
		}
		else if (redValue >= 130)
		{
			asciival = MEDIUM;
		}
		else if (redValue >= 100)
		{
			asciival = MEDIUMGRAY;
		}
		else if (redValue >= 70)
		{
			asciival = DARKGRAY;
		}
		else if (redValue >= 50)
		{
			asciival = CHARCOAL;
		}
		else
		{
			asciival = BLACK;
		}

		return asciival;
	}
}