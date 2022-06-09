namespace Guify.Models.Components {
	class Option : ValueComponent<bool> {
		public Option(bool defaultValue, string text) : base(defaultValue) {
			Text = text;
		}

		public override bool Value { get; set; }

		public readonly string Text;

		public readonly List<Constraint> Constraints = new List<Constraint>();
	}
}