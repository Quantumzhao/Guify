namespace Guify.Models.Components {
	abstract class ValueComponent<T> : ComponentBase {
		public ValueComponent(T defaultValue, bool isRequired, string? longName, string? shortName)
			: base(longName, shortName) {
				
			DefaultValue = defaultValue;
			IsRequired = isRequired;
		}
		public abstract T Value { get; set; }
		public readonly T DefaultValue;
		public readonly bool IsRequired;
	}
}