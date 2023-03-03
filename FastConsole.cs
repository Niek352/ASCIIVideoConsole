using System.Text;

namespace ASCIIVideoConsole;

public static class FastConsole
{
	static readonly BufferedStream str;

	static FastConsole()
	{
		Console.OutputEncoding = Encoding.Unicode;
		str = new BufferedStream(Console.OpenStandardOutput(), 0x15000);
	}

	public static void WriteLine(String s) => Write(s + "\r\n");

	public static void Write(String s)
	{
		var rgb = new byte[s.Length << 1];
		Encoding.Unicode.GetBytes(s, 0, s.Length, rgb, 0);
		lock (str)
			str.Write(rgb, 0, rgb.Length);
	}

	public static void Flush() { lock (str) str.Flush(); }
};