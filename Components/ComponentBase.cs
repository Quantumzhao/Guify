namespace Guify.Components
{
	abstract class ComponentBase
	{
		public ComponentBase(int minWidth, int minHeight) {
			MinHeight = minHeight;
			MinWidth = minWidth;
		}

		public int MinHeight { get; }
		public int MinWidth { get; }
		public int Height { get; protected set; }
		public int Width { get; protected set; }

		public abstract char[,] Buffer { get; }
		public abstract void Render(int maxWidth, int maxHeight);
	}
}