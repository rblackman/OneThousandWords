using System;
using System.Configuration;
using System.IO;

namespace Application
{
	class Program
	{

		

		static void Main()
		{
			var processor = new Processor();
			processor.ProcessFiles();
		}
	}
}
