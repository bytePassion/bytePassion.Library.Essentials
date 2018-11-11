using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace bytePassion.Library.Essentials.WpfTools.Behaviors
{
	public class ListViewAutoScollDownBehavior : Behavior<ListView>
	{
		protected override void OnAttached ()
		{
			base.OnAttached();
			
			 ((INotifyCollectionChanged)AssociatedObject.Items).CollectionChanged += OnCollectionChanged;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			((INotifyCollectionChanged)AssociatedObject.Items).CollectionChanged -= OnCollectionChanged;
		}

		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			var listView  = AssociatedObject;
			var items     = listView.Items;
			var itemCount = items.Count;

			if (itemCount == 0) return;

			var lastItem = listView.Items[itemCount -1];
			listView.ScrollIntoView(lastItem);
		}		
	}
}
