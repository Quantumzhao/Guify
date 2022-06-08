namespace Guify.IO {
	class Printer
	{
		public void Print(char[,] buffer) {
			for (int j = 0; j < buffer.GetLength(1); j++)
			{
				for (int i = 0; i < buffer.GetLength(0); i++)
				{
					Console.Write(buffer[i, j]);
				}
				Console.WriteLine();
			}
		}
	}
}