using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace bytePassion.Library.Essentials.WpfTools.Behaviors
{
    public class MoveHorizontalBehavior : Behavior<FrameworkElement>
	{
		public static readonly DependencyProperty HorizontalMovementDeltaProperty 
			= DependencyProperty.Register(nameof(HorizontalMovementDelta), 
										  typeof (double), 
										  typeof (MoveHorizontalBehavior));

		public static readonly DependencyProperty ExecuteOnMovementEndProperty 
			= DependencyProperty.Register(nameof(ExecuteOnMovementEnd), 
										  typeof (ICommand), 
										  typeof (MoveHorizontalBehavior));

		public ICommand ExecuteOnMovementEnd
		{
			get { return (ICommand) GetValue(ExecuteOnMovementEndProperty); }
			set { SetValue(ExecuteOnMovementEndProperty, value); }
		}

		public double HorizontalMovementDelta
		{
			get { return (double) GetValue(HorizontalMovementDeltaProperty); }
			set { SetValue(HorizontalMovementDeltaProperty, value); }
		}

		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.PreviewMouseLeftButtonDown += OnAssociatedObjectMouseLeftButtonDown;
			AssociatedObject.MouseLeftButtonUp          += OnAssociatedObjectMouseLeftButtonUp;
			AssociatedObject.MouseMove                  += OnAssociatedObjectMouseMove;

			container = System.Windows.Application.Current.MainWindow;
			mouseIsDown = false;
		}

		protected override void OnDetaching ()
		{
			base.OnDetaching();
			AssociatedObject.PreviewMouseLeftButtonDown -= OnAssociatedObjectMouseLeftButtonDown;
			AssociatedObject.MouseLeftButtonUp          -= OnAssociatedObjectMouseLeftButtonUp;
			AssociatedObject.MouseMove                  -= OnAssociatedObjectMouseMove;
		}

		private FrameworkElement container;
		
		private bool  mouseIsDown;		
		private Point referencePoint;

		private void OnAssociatedObjectMouseMove(object sender, MouseEventArgs mouseEventArgs)
		{
			if (mouseIsDown)
			{
				var displacement = mouseEventArgs.GetPosition(container) - referencePoint;

				HorizontalMovementDelta = displacement.X;				
			}
		}

		private void OnAssociatedObjectMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			EndDrag();
		}

		private void OnAssociatedObjectMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			if (ExecuteOnMovementEnd != null)
				if (ExecuteOnMovementEnd.CanExecute(null))
					InitDrag(mouseButtonEventArgs.GetPosition(container));

			mouseButtonEventArgs.Handled = true;
		}

		private void InitDrag (Point startinPoint)
		{
			mouseIsDown = true;
			referencePoint = startinPoint;			

			AssociatedObject.CaptureMouse();
		}

		private void EndDrag ()
		{
			if (mouseIsDown)
				if (ExecuteOnMovementEnd != null)
					if (ExecuteOnMovementEnd.CanExecute(null))
						ExecuteOnMovementEnd.Execute(null);

			mouseIsDown = false;
			Mouse.Capture(null);
		}
	}
}
