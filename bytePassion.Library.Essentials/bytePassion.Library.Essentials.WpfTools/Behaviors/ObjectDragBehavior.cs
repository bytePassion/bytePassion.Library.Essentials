using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace bytePassion.Library.Essentials.WpfTools.Behaviors
{

	public class ObjectDragBehavior : Behavior<FrameworkElement>
	{		
		public static readonly DependencyProperty DragItemProperty = 
			DependencyProperty.Register(nameof(DragItem), 
										typeof (object), 
										typeof (ObjectDragBehavior));

		public object DragItem
		{
			get { return GetValue(DragItemProperty); }
			set { SetValue(DragItemProperty, value); }
		}			

		protected override void OnAttached()
		{
			base.OnAttached();			
			
			AssociatedObject.PreviewMouseLeftButtonDown += OnMouseLeftDown;
			AssociatedObject.PreviewMouseLeftButtonUp   += OMouseLeftButtonUp;
			AssociatedObject.PreviewMouseMove           += OnMouseMove;	
			AssociatedObject.PreviewQueryContinueDrag   += OnQueryContinueDrag;
			AssociatedObject.MouseLeave					+= OnMouseLeave;
		}		

		protected override void OnDetaching()
		{
			base.OnDetaching();

			AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseLeftDown;
			AssociatedObject.PreviewMouseLeftButtonUp   -= OMouseLeftButtonUp;
			AssociatedObject.PreviewMouseMove           -= OnMouseMove;
			AssociatedObject.PreviewQueryContinueDrag   -= OnQueryContinueDrag;
			AssociatedObject.MouseLeave					-= OnMouseLeave;
		}

		private bool  IsLeftMouseButtonDown    { get; set; }
		private Point MousePositionOnDragStart { get; set; }

		private void OnMouseLeave (object sender, MouseEventArgs mouseEventArgs)
		{
			IsLeftMouseButtonDown = false;
		}

		private void OnQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		{
			if (e.EscapePressed)
			{
				e.Action = DragAction.Cancel;
				e.Handled = true;
			}
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (IsLeftMouseButtonDown)
			{
				var displacement = MousePositionOnDragStart - e.GetPosition(AssociatedObject);

				if (displacement.Length > 2)
					if (DragItem != null)					
						DragDrop.DoDragDrop((DependencyObject) sender, DragItem, DragDropEffects.Link);					
			}
		}

		private void OMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			IsLeftMouseButtonDown = false;
		}

		private void OnMouseLeftDown(object sender, MouseButtonEventArgs e)
		{
			IsLeftMouseButtonDown = true;
			MousePositionOnDragStart = e.GetPosition(AssociatedObject);
		}
	}
}
