using Guify.Models.Components;
using Guify.Models;
using System.Xml.Linq;
using System.Linq;

namespace Guify.IO;

static class XMLUtils
{
	public const string SELECT_FOLDER_FIELD = "selectFolder";
	public const string STRING_FIELD = "string";
	public const string YESNO_FIELD = "yesNo";
	public const string NUMBER_FIELD = "number";
	public const string PICK_VALUE_FIELD = "pickValue";
	public const string SAVE_FILE_FIELD = "saveFile";
	public const string OPEN_FILE_FIELD = "openFile";
	public const string NAME = "name";
	public const string DESCRIPTION = "description";
	public const string CUSTOM_DEFAULT = "customDefault";
	public const string REQUIRED = "required";
	public const string LONG_NAME = "longName";
	public const string SHORT_NAME = "shortName";
	public const string SHOW_HELP = "showHelp";
	public const string COMMAND = "command";
	public const string VERB = "verb";
	public const string CANDIDATE = "candidate";
	public const string VALUE = "value";
	public const string IS_FLOAT_NUMBER = "isFloatNumber";
	public const string GROUP = "group";

	public static Root LoadRoot(string path)
	{
		var doc = XDocument.Load(path);
		if (doc == null) throw new ArgumentNullException("The file doesn't exist");

		var root = doc.Elements().First();
		var es = root.Elements().ToArray();
		var help = root.Attribute(SHOW_HELP)?.Value;
		var cmd = root.Attribute(COMMAND)?.Value;

		Verb[]? verbs = null;
		ComponentBase[]? components = null;

		if (root.Elements().All(e => e.Name != VERB))
			verbs = root.Elements().Select(e => LoadVerb(e)).ToArray();
		else if (root.Elements().Any(e => e.Name == VERB))
			throw new InvalidOperationException(
				"Normal components can't be at the same level as verbs");
		else
			components = root.Elements().Select(e => LoadElements(e)).ToArray();

		if (cmd == null) throw new InvalidOperationException(
			$"The .gui file {path} is not configured properly");

		return new Root(cmd, help, verbs, components);
	}

	private static Verb LoadVerb(XElement element)
	{
		var name = element.Attribute(NAME)?.Value;
		var comment = element.GetDescription();
		var controls = element.Elements().Select(e => LoadElements(e)).ToArray();

		if (name == null || comment == null)
		{
			throw new ArgumentNullException("verb or description is null");
		}

		return new Verb(name, comment, controls);
	}

	private static ComponentBase LoadElements(XElement xml)
		=> xml.Name.LocalName switch
		{
			YESNO_FIELD => LoadYesNoField(xml),
			SELECT_FOLDER_FIELD => LoadSelectFolderField(xml),
			STRING_FIELD => LoadStringField(xml),
			NUMBER_FIELD => LoadNumberField(xml),
			OPEN_FILE_FIELD => LoadOpenFileField(xml),
			SAVE_FILE_FIELD => LoadSaveFileField(xml),
			PICK_VALUE_FIELD => LoadPickValueField(xml),
			var any => throw new ArgumentException($"{any} is not a valid component")
		};

	private static StringField LoadStringField(XElement node)
	{
		var comment = node.GetDescription();
		var defaultValue = node.GetDefaultValue();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();

		return new StringField(defaultValue, comment, isRequired, longName, shortName);
	}

	private static OpenFileField LoadOpenFileField(XElement node)
	{
		var defaultValue = node.GetDefaultValue();
		var desc = node.GetDescription();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();

		return new OpenFileField(defaultValue, isRequired, desc, longName, shortName);
	}

	private static SelectFolderField LoadSelectFolderField(XElement node)
	{
		var defaultValue = node.GetDefaultValue();
		var desc = node.GetDescription();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();

		return new SelectFolderField(defaultValue, desc, isRequired, longName, shortName);
	}

	private static ComponentBase LoadYesNoField(XElement node)
	{
		var defaultValue = bool.Parse(node.GetDefaultValue() ?? "false");
		var desc = node.GetDescription();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();

		var group = node.Attribute(GROUP)?.Value;
		if (group == null)
			return new CheckBoxField(defaultValue, isRequired, longName, shortName, desc);
		else
			return new RadioButtonField(defaultValue, isRequired, longName, shortName, desc
				, group);
	}

	private static ComponentBase LoadNumberField(XElement node)
	{
		var defaultValue = node.GetDefaultValue();
		var desc = node.GetDescription();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();
		
		var isFloat = bool.Parse(node.Attribute(IS_FLOAT_NUMBER)?.Value ?? "false");
		if (isFloat)
			return new IntField(int.Parse(defaultValue), isRequired, longName, shortName, desc);
		else
			return new FloatField(float.Parse(defaultValue), isRequired, longName, shortName, desc);
	}

	private static SaveFileField LoadSaveFileField(XElement node)
	{
		var defaultValue = node.GetDefaultValue();
		var desc = node.GetDescription();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();

		return new SaveFileField(defaultValue, desc, isRequired, longName, shortName);
	}

	private static PickValueField LoadPickValueField(XElement node)
	{
		var defaultValue = node.GetDefaultValue();
		var desc = node.GetDescription();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();
		var candidates = node.Elements()
							 .Where  (e => e.Name == CANDIDATE)
							 .Select (c => c.Attribute(VALUE)?.Value)
							 .Where  (c => !string.IsNullOrEmpty(c))
							 .Select (c => c.FuckingToString())
							 .ToArray();

		return new PickValueField(
			defaultValue, isRequired, longName, shortName, desc, candidates);
	}

	private static bool GetIsRequired(this XElement node)
		=> bool.Parse(node.Attribute(REQUIRED)?.Value ?? "false");

	private static string GetDescription(this XElement node)
	{
		var desc = node.Attribute(DESCRIPTION)?.Value;
		if (desc == null) throw new ArgumentNullException("description is not given");
		else return desc;
	}

	private static string GetDefaultValue(this XElement node)
		=> node.Attribute(CUSTOM_DEFAULT)?.Value ?? string.Empty;

	private static string? GetLongName(this XElement node)
		=> node.Attribute(LONG_NAME)?.Value;

	private static string? GetShortName(this XElement node)
		=> node.Attribute(SHORT_NAME)?.Value;
}
