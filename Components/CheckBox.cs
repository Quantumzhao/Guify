namespace Guify.Components
{
    class CheckBox : ComponentBase
    {
        public bool Value { get; set; }

        public string Text {get; set; } = string.Empty;

        public Action<ComponentBase, bool> OnValueChanged = (s, e) => {};
    }
}