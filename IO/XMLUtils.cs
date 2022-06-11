using Guify.Models.Components;
using Guify.Models;
using System.Xml.Linq;
using System.Linq;

namespace Guify.IO;

static class XMLUtils {

	public const string OPEN_FOLDER_BOX = "SelectFolder";
	public const string TEXT_BOX = "String";
	public const string NAME = "Name";
	public const string DESCRIPTION = "Description";
	public const string DEFAULT = "Default";
	public const string REQUIRED = "Required";
	public const string LONG_NAME = "LongName";
	public const string SHORT_NAME = "ShortName";

	public static Verb[] LoadFile(string path) {

		var doc = XDocument.Load(path);
		if (doc == null) throw new ArgumentNullException("The file doesn't exist");

		var ui = doc.Elements().First();
		var es = ui.Elements().ToArray();

		return ui.Elements().Select(e => LoadVerb(e)).ToArray();
	}

	private static Verb LoadVerb(XElement element) {

		var name = element.Attribute(NAME)?.Value;
		var comment = element.GetDescription();
		var controls = element.Elements().Select(e => LoadElements(e)).ToArray();

		if (name == null || comment == null) {

			throw new ArgumentNullException("verb or description is null");
		}

		return new Verb(name, comment, controls);
	}

	private static ControlBase LoadElements(XElement xml)

		=> xml.Name.LocalName switch {
			OPEN_FOLDER_BOX => LoadOpenFolderBox(xml),
			TEXT_BOX => LoadTextBox(xml),
			var any => throw new ArgumentException($"{any} is not a valid component")
		};

	private static TextBox LoadTextBox(XElement node) {

		var comment = node.GetDescription();
		var defaultValue = node.GetDefaultValue();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();

		return new TextBox(defaultValue, comment, isRequired, longName, shortName);
	}

	private static OpenFolderBox LoadOpenFolderBox(XElement node) {

		var defaultValue = node.GetDefaultValue();
		var desc = node.GetDescription();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();

		return new OpenFolderBox(defaultValue, isRequired, desc, longName, shortName);
	}

	private static bool GetIsRequired(this XElement node)
		=> bool.Parse(node.Attribute(REQUIRED)?.Value ?? "false");

	private static string GetDescription(this XElement node) {

		var desc = node.Attribute(DESCRIPTION)?.Value;
		if (desc == null ) throw new ArgumentNullException("description is not given");
		else return desc;
	}

	private static string GetDefaultValue(this XElement node)
		=> node.Attribute(DEFAULT)?.Value ?? string.Empty;

	private static string? GetLongName(this XElement node)
		=> node.Attribute(LONG_NAME)?.Value;

	private static string? GetShortName(this XElement node)
		=> node.Attribute(SHORT_NAME)?.Value;
}
