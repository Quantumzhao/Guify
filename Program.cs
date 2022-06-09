using Avalonia;
using Avalonia.ReactiveUI;

namespace Guify
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) {
            if (args.Length == 0) {
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
            } else {
                Guify.CLI.CLIMain.EntryPoint(args);
            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}
