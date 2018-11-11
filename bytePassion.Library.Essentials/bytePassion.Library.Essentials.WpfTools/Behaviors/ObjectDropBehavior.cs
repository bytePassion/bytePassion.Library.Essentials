using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace bytePassion.Library.Essentials.WpfTools.Behaviors
{
	public class ObjectDropBehavior : Behavior<FrameworkElement>
    {	    	
		public static readonly DependencyProperty OnDropCommandProperty =
			DependencyProperty.Register(nameof(OnDropCommand),
										typeof(ICommand),
										typeof(ObjectDropBehavior));

	    public ICommand OnDropCommand
	    {
			get { return (ICommand) GetValue(OnDropCommandProperty); }
			set { SetValue(OnDropCommandProperty, value);}
	    }	    
		
		protected override void OnAttached ()
		{
			base.OnAttached();
			AssociatedObject.Drop += AssociatedObjectOnDrop;				
		}

		protected override void OnDetaching ()
		{
			base.OnDetaching();
			AssociatedObject.Drop -= AssociatedObjectOnDrop;
		}

	    private void AssociatedObjectOnDrop(object sender, DragEventArgs e)
	    {
			if (OnDropCommand != null)
			{
				var formats = e.Data.GetFormats();
				var dropedObject = e.Data.GetData(formats[0]);				

				if (OnDropCommand.CanExecute(dropedObject))
					OnDropCommand.Execute(dropedObject);			    
		    }
	    }	   
    }
}
