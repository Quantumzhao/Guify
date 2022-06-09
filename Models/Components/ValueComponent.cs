namespace Guify.Models.Components {
	abstract class ValueComponent<T> : ComponentBase {
		public ValueComponent(T defaultValue) {
			DefaultValue = defaultValue;
		}
		public abstract T Value { get; set; }
		public readonly T DefaultValue;
	}
}