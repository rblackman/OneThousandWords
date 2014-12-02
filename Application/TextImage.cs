using System;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Application
{
	public class TextImage
	{

		private static Pixel PadPixel = new Pixel(0, 0, 0);

		private readonly string _originalText;

		public string OriginalText
		{
			get { return _originalText; }
		}

		public Bitmap Image { get; set; }

		public TextImage(string text)
		{
			_originalText = text;
			if (string.IsNullOrWhiteSpace(OriginalText))
			{
				throw new ArgumentException("Please provide some text to make an image out of");
			}

			CreateImage();

		}

		private void CreateImage()
		{
			var utf8Bytes = Encoding.UTF8.GetBytes(OriginalText);

			var pixels = utf8Bytes
				.Select((b, i) => new { b, i })
				.ToLookup(x => x.i / 3)
				.Select(group => new Pixel(group.Select(pair => pair.b)))
				.ToArray();


			var size = GetImageSize(pixels);
			var width = size[0];
			var height = size[1];

			Image = new Bitmap(width, height);

			for (var i = 0; i < (width * height); i++)
			{		
				var pixel = i < pixels.Length ? pixels[i] : PadPixel;
				var y = i / width;
				var x = i - (y * width);

				Image.SetPixel(x, y, Color.FromArgb(pixel.Red, pixel.Green, pixel.Blue));
			}

		}

		private int[] GetImageSize(Pixel[] pixels)
		{
			var pixelCount = pixels.Length;

			var x = 1;
			var y = 1;
			var imageArea = 2;

			while (imageArea < pixelCount)
			{
				if (x > y)
				{
					y++;
				}
				else
				{
					x++;
				}
				imageArea = x * y;
			}
			return new[] { x, y };
		}

	}
}
