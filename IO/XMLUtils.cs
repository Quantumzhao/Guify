using Guify.Models.Containers;
using Guify.Models.Components;
using Guify.Models;
using System.Xml.Linq;
using System.Linq;

namespace Guify.IO;

static class XMLUtils {

	public static Page[] LoadFile(string path) {

		var doc = new XDocument();
		// doc.LoadXml
		if (doc == null) throw new ArgumentNullException("The file doesn't exist");

		return doc.Elements().Select(e => LoadPage(e)).ToArray();
	}

	private static Page LoadPage(XElement element) {

		var label = element.Attribute("Label")?.Value;
		var comment = element.GetComment();
		var controls = element.Elements().Select(e => LoadElements(e)).ToArray();

		if (label == null || comment == null) {

			throw new ArgumentNullException("label or comment is null");
		}

		return new Page(label, comment, controls);
	}

	private static ElementBase LoadElements(XElement xml)

		=> xml.Name.LocalName switch {
			nameof(Group) => throw new NotImplementedException(),
			nameof(OpenFolderBox) => LoadOpenFolderBox(xml),
			nameof(TextBox) => LoadTextBox(xml),
			var any => throw new ArgumentException($"{any} is not a valid component")
		};

	private static TextBox LoadTextBox(XElement node) {

		var comment = node.GetComment();
		var defaultValue = node.GetDefaultValue();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();

		return new TextBox(defaultValue, comment, isRequired, longName, shortName);
	}

	private static OpenFolderBox LoadOpenFolderBox(XElement node) {

		var defaultValue = node.GetDefaultValue();
		var comment = node.GetComment();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();

		return new OpenFolderBox(defaultValue, isRequired, comment, longName, shortName);
	}

	private static bool GetIsRequired(this XElement node)
		=> bool.Parse(node.Attribute("Required")?.Value ?? "false");

	private static string GetComment(this XElement node) {

		var comment = node.Attribute("Comment")?.Value;
		if (comment == null ) throw new ArgumentNullException("comment is not given");
		else return comment;
	}

	private static string GetDefaultValue(this XElement node)
		=> node.Attribute("Default")?.Value ?? string.Empty;

	private static string? GetLongName(this XElement node)
		=> node.Attribute("LongName")?.Value;

	private static string? GetShortName(this XElement node)
		=> node.Attribute("ShortName")?.Value;
}
