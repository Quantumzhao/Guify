namespace Guify.Models.Components {
	abstract class ComponentBase<T> : ControlBase {

		public ComponentBase(T defaultValue, bool isRequired, string? longName, string? shortName) {

			DefaultValue = defaultValue;
			IsRequired = isRequired;
			LongName = longName;
			ShortName = shortName;
		}

		public readonly string? LongName = null;
		public readonly string? ShortName = null;

		public bool IsChanged { get; set; } = false;

		public abstract T Value { get; set; }
		public readonly T DefaultValue;
		public readonly bool IsRequired;
	}
}