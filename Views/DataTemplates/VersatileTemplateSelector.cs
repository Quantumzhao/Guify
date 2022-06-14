using System.ComponentModel.Design.Serialization;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using Guify.Models.Components;
using Guify.IO;
using static Guify.Models.Misc;
using Separator = Guify.Models.Components.Separator;

namespace Guify.Views.DataTemplates;

class VersatileTemplateSelector : IDataTemplate
{
	private const string STRING_TEMPLATE = "StringTemplate";
	private const string SELECT_FOLDER_TEMPLATE = "SelectFolderTemplate";
	private const string CHECK_BOX_TEMPLATE = "CheckBoxTemplate";
	private const string RADIO_BUTTON_TEMPLATE = "RadioButtonTemplate";
	private const string FLOAT_TEMPLATE = "NumberTemplate";
	private const string OPEN_FILE_TEMPLATE = "OpenFileTemplate";
	private const string PICK_VALUE_TEMPLATE = "PickValueTemplate";
	private const string SAVE_FILE_TEMPLATE = "SaveFileTemplate";
	private const string SEPARATOR_TEMPLATE = "SeparatorTemplate";
	private const string INFIX_TEMPLATE = "InfixTemplate";


	[Content]
	public Dictionary<string, IDataTemplate> Templates { get; }
		= new Dictionary<string, IDataTemplate>();

	public IControl Build(object param)
		=> param switch
		{
			StringField => Templates[STRING_TEMPLATE].Build(param),
			SelectFolderField => Templates[SELECT_FOLDER_TEMPLATE].Build(param),
			CheckBoxField => Templates[CHECK_BOX_TEMPLATE].Build(param),
			RadioButtonField => Templates[RADIO_BUTTON_TEMPLATE].Build(param),
			NumberField => Templates[FLOAT_TEMPLATE].Build(param),
			OpenFileField => Templates[OPEN_FILE_TEMPLATE].Build(param),
			PickValueField => Templates[PICK_VALUE_TEMPLATE].Build(param),
			SaveFileField => Templates[SAVE_FILE_TEMPLATE].Build(param),
			Separator => Templates[SEPARATOR_TEMPLATE].Build(param),
			Infix => Templates[INFIX_TEMPLATE].Build(param),
			_ => throw new ArgumentException()
		};

	public bool Match(object data) => data is ComponentBase;
}