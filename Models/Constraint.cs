using Guify.Models.Components;

namespace Guify.Models {
	class Constraint {

	}

	class Constraint<T> : Constraint {
		public Constraint(T value, ValueComponent<T> component) {
			Set2Value = value;
			Component = component;
		}
		public readonly T Set2Value;
		public readonly ValueComponent<T> Component;
	}
}