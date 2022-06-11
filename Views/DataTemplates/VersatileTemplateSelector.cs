using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using Guify.Models.Components;
using Guify.IO;

namespace Guify.Views.DataTemplates;

class VersatileTemplateSelector : IDataTemplate
{
	[Content]
	public Dictionary<string, IDataTemplate> Templates { get; }
		= new Dictionary<string, IDataTemplate>();

	public IControl Build(object param)
	{
		if (param == null) throw new ArgumentNullException();
		else if (param is StringField) return Templates[XMLUtils.STRING_FIELD].Build(param);
		else if (param is SelectFolderField) return Templates[XMLUtils.SELECT_FOLDER_FIELD].Build(param);
		else throw new ArgumentException();
	}

	public bool Match(object data) => data is ComponentBase;
}