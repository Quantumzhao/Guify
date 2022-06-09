namespace Guify.Models.Components {
	class Option : ValueComponent<bool> {
		public Option(bool defaultValue, string text, bool isRequired, string longName
			, string shortName) : base(defaultValue, isRequired, longName, shortName) {
				
			Text = text;
		}

		public override bool Value { get; set; }

		public readonly string Text;

		public readonly List<Constraint> Constraints = new List<Constraint>();
	}
}