namespace Guify.Models.Components {
	abstract class ComponentBase : ElementBase {

		public ComponentBase(string? longName, string? shortName) {

			LongName = longName;
			ShortName = shortName;
		}
		public readonly string? LongName = null;
		public readonly string? ShortName = null;
	}
}