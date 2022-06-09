namespace Guify.Models.Components;

class OpenFolderBox : ValueComponent<string> {

	public OpenFolderBox(string defaultValue) : base(defaultValue) {
		
		Value = defaultValue;
	}

	public override string Value { get; set; }

}