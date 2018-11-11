using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace bytePassion.Library.Essentials.WpfTools.Behaviors
{
    public class DisableRightClickBehavior : Behavior<FrameworkElement>
    {
	    protected override void OnAttached()
	    {
		    AssociatedObject.PreviewMouseRightButtonDown += OnPreviewMouseRightButtonDown;
	    }

	    protected override void OnDetaching()
	    {
		    base.OnDetaching();
			AssociatedObject.PreviewMouseRightButtonDown -= OnPreviewMouseRightButtonDown;
	    }

	    private static void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
	    {
			mouseButtonEventArgs.Handled = true;
	    }
    }
}
