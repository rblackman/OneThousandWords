using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
	public class Pixel
	{

		public byte Red { get; set; }
		public byte Green { get; set; }
		public byte Blue { get; set; }
		public string Hex
		{
			get { return string.Format("{0}{1}{2}", Red.ToString("X"), Green.ToString("X"), Blue.ToString("X")); }
		}

		public Pixel(byte red, byte green, byte blue)
		{
			Red = red;
			Green = green;
			Blue = blue;
		}

		public Pixel(IEnumerable<byte> bytes)
		{
			var byteArray = (bytes ?? Enumerable.Empty<byte>()).ToArray();
			if (byteArray.Length == 0 || byteArray.Length > 3)
			{
				throw new ArgumentException("Please provide a byte array with a lenght between 1-3");
			}
			Red = byteArray[0];
			if (byteArray.Length > 1)
			{
				Green = byteArray[1];
				if (byteArray.Length > 2)
				{
					Blue = byteArray[2];
				}
			}
		}
	}
}
