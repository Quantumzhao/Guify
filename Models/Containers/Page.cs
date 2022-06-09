namespace Guify.Models.Containers {
	class Page : ControlBase {

		public Page(string label, string comment, ControlBase[] items) {
			Label = label;
			Comment = comment;
			Items = items;
		}

		public readonly string Label;
		public readonly string Comment;
		public readonly ControlBase[] Items;

		public bool IsUsingThis { get; set; } = false;
	}
}