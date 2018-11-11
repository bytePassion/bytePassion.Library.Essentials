using System.Windows;
using System.Windows.Interactivity;

namespace bytePassion.Library.Essentials.WpfTools.Behaviors
{
    public class GetFocusOnLoadedBehavior : Behavior<FrameworkElement>
    {
	    protected override void OnAttached()
	    {
		    base.OnAttached();
			AssociatedObject.Loaded += OnLoaded;
	    }

	    protected override void OnDetaching()
	    {
		    base.OnDetaching();
			AssociatedObject.Loaded -= OnLoaded;
	    }

	    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
	    {
		    AssociatedObject.Focus();
	    }
    }
}
