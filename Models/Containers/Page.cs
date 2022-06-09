namespace Guify.Models.Containers {
	class Page : ElementBase {

		public Page(string label, string comment, ElementBase[] items) {
			Label = label;
			Comment = comment;
			Items = items;
		}

		public readonly string Label;
		public readonly string Comment;
		public readonly ElementBase[] Items;
	}
}