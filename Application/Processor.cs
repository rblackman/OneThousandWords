using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Application
{
	public class Processor
	{

		private DirectoryInfo InputDirectory { get; set; }
		private DirectoryInfo OutputDirectory { get; set; }

		public Processor()
		{
			var basePath = Environment.CurrentDirectory;
			InputDirectory = new DirectoryInfo(Path.Combine(basePath, "input"));
			OutputDirectory = new DirectoryInfo(Path.Combine(basePath, "output"));

			if (!InputDirectory.Exists || !InputDirectory.GetFiles().Any())
			{
				throw new Exception("Please provide at least one input file");
			}

			if (!OutputDirectory.Exists)
			{
				OutputDirectory.Create();
			}

		}

		#region text to image

		public Processor ProcessTextFiles()
		{
			foreach (var file in GetTextToProcess())
			{
				ProcessTextFile(file);
			}
			return this;
		}

		private void ProcessTextFile(FileInfo file)
		{
			var contents = ReadTextFile(file);
			var textImage = new TextImage(contents);

			var fileName = Path.Combine(OutputDirectory.FullName, string.Format("{0}.png", file.Name.Split('.').First()));
			textImage.Image.Save(fileName);
		}

		private IEnumerable<FileInfo> GetTextToProcess()
		{
			return InputDirectory.GetFiles("*.txt");
		}

		private string ReadTextFile(FileSystemInfo file)
		{
			return File.ReadAllText(file.FullName);
		}

		#endregion

		#region image to text

		public Processor ProcessImageFiles()
		{
			foreach (var imageFile in GetImagesToProcess())
			{
				ProcessImageFile(imageFile);
			}
			return this;
		}

		private void ProcessImageFile(FileInfo file)
		{
			var image = Image.FromFile(file.FullName);
			var text = TextImage.FromImage(new Bitmap(image));

			var fileName = Path.Combine(OutputDirectory.FullName, string.Format("{0}.txt", file.Name.Split('.').First()));
			using (var writer = new StreamWriter(fileName))
			{
				writer.Write(text);
				writer.Close();
			}
		}

		private IEnumerable<FileInfo> GetImagesToProcess()
		{
			return InputDirectory.GetFiles("*.png");
		}

		#endregion

	}

}
