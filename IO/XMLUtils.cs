using Guify.Models.Components;
using Guify.Models;
using static Guify.Models.Misc;
using System.Xml.Linq;
using System.Linq;

namespace Guify.IO;

static class XMLUtils
{

#region XMLLabels
	private const string SELECT_FOLDER_FIELD = "selectFolder";
	private const string STRING_FIELD = "string";
	private const string YESNO_FIELD = "yesNo";
	private const string NUMBER_FIELD = "number";
	private const string PICK_VALUE_FIELD = "pickValue";
	private const string SAVE_FILE_FIELD = "saveFile";
	private const string OPEN_FILE_FIELD = "openFile";
	private const string INFIX = "infix";
	private const string SEPARATOR = "separator";
#endregion

#region XMLAttributes
	private const string NAME = "name";
	private const string DESCRIPTION = "description";
	private const string CUSTOM_DEFAULT = "customDefault";
	private const string REQUIRED = "required";
	private const string LONG_NAME = "longName";
	private const string SHORT_NAME = "shortName";
	private const string SHOW_HELP = "showHelp";
	private const string COMMAND = "command";
	private const string VERB = "verb";
	private const string CANDIDATE = "candidate";
	private const string VALUE = "value";
	private const string CODE = "code";
	private const string GROUP = "group";
	private const string MAX = "max";
	private const string MIN = "min";
	private const string MULTIPLE = "multiple";
	private const string CUSTOM_DEFAULT_FOLDER = "customDefaultFolder";
	private const string CUSTOM_DEFAULT_FILE_NAME = "customDefaultFileName";
	private const string IS_FLAG = "isFlag";
	private const string LABEL = "label";
	private const string REUSABLE = "reusable";
#endregion

	public static Root LoadRoot(string path)
	{
		var doc = XDocument.Load(path);
		if (doc == null) throw new ArgumentNullException("The file doesn't exist");

		var root = doc.Elements().First();
		var es = root.Elements().ToArray();
		var help = root.Attribute(SHOW_HELP)?.Value;
		var cmd = root.Attribute(COMMAND)?.Value;
		var reusable = root.Attribute(REUSABLE)?.Value?.ToBool();

		Verb[]? verbs = null;
		ComponentBase[]? components = null;

		if (root.Elements().All(e => e.Name != VERB))
			verbs = root.Elements().Select(LoadVerb).ToArray();
		else if (root.Elements().Any(e => e.Name == VERB))
			throw new InvalidOperationException(
				"Normal components can't be at the same level as verbs");
		else
			components = root.Elements().Select(LoadElements).ToArray();

		if (cmd == null) throw new InvalidOperationException(
			$"The .gui file {path} is not configured properly");

		return new Root(cmd, help, reusable, verbs, components);
	}

	private static Verb LoadVerb(XElement element)
	{
		var name = element.Attribute(NAME)?.Value;
		var comment = element.GetDescription();
		var controls = element.Elements().Select(LoadElements).ToArray();

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
			INFIX => LoadInfix(xml),
			SEPARATOR => LoadSeparator(xml),
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
		var desc = node.GetDescription();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();
		var allowMultiple = node.Attribute(MULTIPLE)?.Value.ToBool() ?? false;
		var customDefaultFolder = node.Attribute(CUSTOM_DEFAULT_FOLDER)?.Value;
		var customDefaultFileName = node.Attribute(CUSTOM_DEFAULT_FILE_NAME)?.Value?.Expand();

		return new OpenFileField(customDefaultFileName, customDefaultFolder, allowMultiple, isRequired, desc, longName, shortName);
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
		var isFlag = node.Attribute(IS_FLAG)?.Value?.ToBool();

		var group = node.Attribute(GROUP)?.Value;
		if (group == null)
			return new CheckBoxField(defaultValue, isFlag, isRequired, longName, shortName, desc);
		else
			return new RadioButtonField(defaultValue, isFlag, isRequired, longName, shortName, desc
				, group);
	}

	private static ComponentBase LoadNumberField(XElement node)
	{
		var defaultValue = node.GetDefaultValue();
		var desc = node.GetDescription();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();
		var max = node.Attribute(MAX)?.Value.ToFloat();
		var min = node.Attribute(MIN)?.Value.ToFloat();
		
		return new NumberField(
			defaultValue.ToFloat(), max, min, isRequired, longName, shortName, desc);
	}

	private static SaveFileField LoadSaveFileField(XElement node)
	{
		var desc = node.GetDescription();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();
		var customDefaultFileName = node.Attribute(CUSTOM_DEFAULT_FILE_NAME)?.Value;
		var customDefaultFolder = node.Attribute(CUSTOM_DEFAULT_FOLDER)?.Value;

		return new SaveFileField(customDefaultFileName, customDefaultFolder, desc, isRequired, longName, shortName);
	}

	private static PickValueField LoadPickValueField(XElement node)
	{
		var defaultValue = node.GetDefaultValue();
		var desc = node.GetDescription();
		var isRequired = node.GetIsRequired();
		var longName = node.GetLongName();
		var shortName = node.GetShortName();
		var candidates = node.Elements()
							 .Where  (e => e.Name.LocalName == CANDIDATE)
							 .Select (c => c.Attribute(VALUE)?.Value)
							 .Where  (c => !string.IsNullOrEmpty(c))
							 .Select (c => c.FuckingToString())
							 .ToArray();

		return new PickValueField(
			defaultValue, isRequired, longName, shortName, desc, candidates);
	}

	private static Infix LoadInfix(XElement node)
	{
		var value = node.Attribute(CODE)?.Value;
		if (value == null) throw new ArgumentNullException();
		else return new Infix(value);
	}

	private static Separator LoadSeparator(XElement node)
	{
		var label = node.Attribute(LABEL)?.Value ?? string.Empty;
		return new Separator(label);
	}

	private static bool GetIsRequired(this XElement node)
		=> bool.Parse(node.Attribute(REQUIRED)?.Value ?? "false");

	private static string GetDescription(this XElement node)
	{
		var desc = node.Attribute(DESCRIPTION)?.Value;
		if (desc == null) throw new ArgumentNullException("description is not given");
		else return desc;
	}

	private static string? GetDefaultValue(this XElement node)
		=> node.Attribute(CUSTOM_DEFAULT)?.Value;

	private static string? GetLongName(this XElement node)
		=> node.Attribute(LONG_NAME)?.Value;

	private static string? GetShortName(this XElement node)
		=> node.Attribute(SHORT_NAME)?.Value;
}
