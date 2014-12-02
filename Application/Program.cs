namespace Application
{
	class Program
	{
		static void Main()
		{
			new Processor()
			   .ProcessTextFiles()
			   .ProcessImageFiles();
		}
	}
}