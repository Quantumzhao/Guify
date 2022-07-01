using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Guify.Models.Components;
using Guify.Models;
using Guify.IO;
using Guify.CLI;
using System.Linq;
using System.Threading;
using Avalonia.Media;

namespace Guify.Views
{
	public partial class MainView : UserControl
	{
		public MainView()
		{
			InitializeComponent();
		}

		// private bool flag;
		// private IBrush b;
		
		private void UpdateVerbs(object? sender, SelectionChangedEventArgs e)
		{
			if (sender is not TabControl tab) return;

			if (e.AddedItems.Count != 0 && e.AddedItems[0] is Verb newVerb) 
				newVerb.IsUsingThis = true;
			if (e.RemovedItems.Count != 0 && e.RemovedItems[0] is Verb oldVerb) 
				oldVerb.IsUsingThis = false;
		}

		private void PART_ScrollViewer_OnScrollChanged(object? sender, ScrollChangedEventArgs e)
		{
			// if (e.OffsetDelta.Y == 0) return;
			//
			// (sender as ScrollViewer).OpacityMask = new SolidColorBrush(Colors.White);
			//
			// if (flag)
			// {
			// 	(sender as ScrollViewer).OpacityMask = new SolidColorBrush(Colors.White);
			// }
			// else
			// {
			// 	(sender as ScrollViewer).OpacityMask = b;
			// }
			//
			// flag = !flag;
		}
	}
}