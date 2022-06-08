using Guify.Components;
using Guify.IO;

namespace Guify
{
	class Program
	{
		public static void Main(string[] argv) 
		{
			Console.WriteLine("Hello world");
			var text = new Label("labelxxxaiugbria");
			text.Render(4, 2);
			var printer = new Printer();
			printer.Print(text.Buffer);
		}
	}
}