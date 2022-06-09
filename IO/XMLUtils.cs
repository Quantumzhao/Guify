using Guify.Models.Containers;
using Guify.Models.Components;
using Guify.Models;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace Guify.IO;

class XMLUtils {

	public Page[] LoadFile(string path) {

		var doc = new XDocument();
		// doc.LoadXml
		if (doc == null) throw new ArgumentNullException("The file doesn't exist");

		return doc.Elements().Select(e => LoadPage(e)).ToArray();
	}

	private Page LoadPage(XElement element) {

		var label = element.Attribute("Label")?.Value;
		var comment = element.Attribute("Comment")?.Value;
		var controls = element.Elements().Select(e => LoadElements(e)).ToArray();

		if (label == null || comment == null) {

			throw new ArgumentNullException("label or comment is null");
		}

		return new Page(label, comment, controls);
	}

	private ElementBase LoadElements(XElement xml) {
		
		return xml.Name.LocalName switch {
			nameof(Group) => throw new NotImplementedException(),
			nameof(OpenFolderBox) => throw new NotImplementedException(),
			nameof(TextBox) => throw new NotImplementedException(),
			var any => throw new ArgumentException($"{any} is not a valid component")
		};
	}

	private TextBox LoadTextBox(XmlNode node) {
		throw new NotImplementedException();
	}
}
