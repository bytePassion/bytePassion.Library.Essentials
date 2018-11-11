using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace bytePassion.Library.Essentials.WpfTools.Behaviors
{
	public class GridViewSplitColumnsEqualyBehavior : Behavior<ListView>
	{
		protected override void OnAttached ()
		{
			base.OnAttached();

			AssociatedObject.SizeChanged += OnSizeChanged;
			((INotifyCollectionChanged)AssociatedObject.Items).CollectionChanged += OnCollectionChanged;

			initIsDeregistered = false;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();

			AssociatedObject.SizeChanged -= OnSizeChanged;			
		}

		private bool initIsDeregistered; 

		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			OnSizeChanged(AssociatedObject, null);			
		}

		private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
		{
			var listView = (ListView) sender;

			if (!(listView.View is GridView))
				return;

			var listViewWidth = listView.ActualWidth;
			var gridView = (GridView) listView.View;
			var gridViewColumnCount = gridView.Columns.Count;

			var widthPerColumn = (listViewWidth / gridViewColumnCount) * 0.985;

			for (int i = 0; i < gridViewColumnCount; i++)			
				gridView.Columns[i].Width = widthPerColumn;	
			
			if (gridViewColumnCount > 0)
				if (!initIsDeregistered)
				{
					((INotifyCollectionChanged) AssociatedObject.Items).CollectionChanged -= OnCollectionChanged;
					initIsDeregistered = true;
				}
		}		
	}
}
