using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

			if (!InputDirectory.Exists || !GetFilesToProcess().Any())
			{
				throw new Exception("Please provide at least one input file");
			}

			if (!OutputDirectory.Exists)
			{
				OutputDirectory.Create();
			}

		}

		public void ProcessFiles()
		{
			foreach (var file in GetFilesToProcess())
			{
				ProcessFile(file);
			}
		}

		private void ProcessFile(FileInfo file)
		{
			var contents = ReadFile(file);
			var textImage = new TextImage(contents);

			var imageName = Path.Combine(OutputDirectory.FullName, string.Format("{0}.png", file.Name.Split('.').First()));
			textImage.Image.Save(imageName);
		}

		private IEnumerable<FileInfo> GetFilesToProcess()
		{
			return InputDirectory.GetFiles("*.txt");
		}

		private string ReadFile(FileSystemInfo file)
		{
			return File.ReadAllText(file.FullName);
		}

	}
}
