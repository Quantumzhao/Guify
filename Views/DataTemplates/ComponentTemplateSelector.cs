using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using Guify.Models.Components;
using Separator = Guify.Models.Components.Separator;

namespace Guify.Views.DataTemplates;

public class ComponentTemplateSelector : IDataTemplate
{
    private const string INFIX_TEMPLATE = "InfixTemplate";
    private const string SEPARATOR_TEMPLATE = "SeparatorTemplate";
    private const string FIELD_TEMPLATE = "FieldTemplate";

    [Content]
    public Dictionary<string, IDataTemplate> Templates { get; } = new();

    public IControl Build(object param)
        => param switch
        {
            Infix => Templates[INFIX_TEMPLATE].Build(param),
            Separator => Templates[SEPARATOR_TEMPLATE].Build(param),
            _ => Templates[FIELD_TEMPLATE].Build(param)
        };

    public bool Match(object data) => data is ComponentBase;
}