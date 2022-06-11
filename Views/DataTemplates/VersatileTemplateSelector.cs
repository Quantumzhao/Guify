using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using Guify.Models.Components;

namespace Guify.Views.DataTemplates;

class VersatileTemplateSelector : IDataTemplate {
	
	[Content]
	public Dictionary<string, IDataTemplate> Templates { get; } 
		= new Dictionary<string, IDataTemplate>();

	public IControl Build(object param)
	{
		var key = param.ToString();
		
		if (key == null) throw new ArgumentNullException();
		else return Templates[key].Build(param);
	}

	public bool Match(object data) => data is ComponentBase;
}