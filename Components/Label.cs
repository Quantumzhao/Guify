namespace Guify.Components {
	class Label : ComponentBase {
		public Label(string text) : base(MIN_WIDTH, MIN_HEIGHT) {

			Text = text;			
		}

		public const int MIN_HEIGHT = 1;
		public const int MIN_WIDTH = 4;

		public string Text { get; private set; } = string.Empty;

		private char[,] _Buffer = new char[0, 0];
		public override char[,] Buffer => _Buffer;
		
		public override void Render(int maxWidth, int maxHeight) {
			maxWidth = Math.Max(maxWidth, MinWidth);
			maxHeight = Math.Max(maxHeight, MinHeight);

			var chunks = Text.Chunk(maxWidth).ToArray();
			var chunksHeight = chunks.Count();

			if (Text.Length <= maxWidth) {
				_Buffer = Utils.GetNewBlank(Text.Length, MinHeight);
				for (int i = 0; i < Text.Length; i++)
				{
					_Buffer[i, 0] = Text[i];
				}
			} else if (chunksHeight <= maxHeight) {
				_Buffer = Utils.GetNewBlank(maxWidth, chunksHeight);
				for (int j = 0; j < chunks.Length; j++)
				{
					for (int i = 0; i < chunks[j].Length; i++)
					{
						_Buffer[i, j] = chunks[j][i];
					}
				}
			} else {
				_Buffer = Utils.GetNewBlank(maxWidth, maxHeight);
				for (int j = 0; j < Buffer.GetLength(1); j++)
				{
					for (int i = 0; i < Buffer.GetLength(0); i++)
					{
						if (j == Buffer.GetLength(1) - 1 && i >= Buffer.GetLength(0) - 3) {
							// draw "..."
							_Buffer[i, j] = '.';
						} else {
							_Buffer[i, j] = chunks[j][i];
						}
					}
				}
			}
		}
	}
}