namespace Guify {
	static class Utils {

		public static char[,] GetNewBlank(int width, int height) {
			var ret = new char[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					ret[i, j] = ' ';
				}
			}

			return ret;
		}
	}
}