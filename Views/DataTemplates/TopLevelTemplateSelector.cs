using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using Guify.Models.Components;
using Guify.Models;

namespace Guify.Views.DataTemplates;

class TopLevelTemplateSelector : IDataTemplate
{
	private const string SINGLETON_TEMPLATE = "SingletonTemplate";
	private const string VERBS_TEMPLATE = "VerbsTemplate";
	
	[Content]
	public Dictionary<string, IDataTemplate> Templates { get; }
		= new Dictionary<string, IDataTemplate>();

	public Control Build(object param)
		=> param switch
		{
			ComponentBase[] => Templates[SINGLETON_TEMPLATE].Build(param),
			Verb[] => Templates[VERBS_TEMPLATE].Build(param),
			_ => throw new ArgumentException()
		};

	public bool Match(object data) => data is ComponentBase[] or Verb[];
}