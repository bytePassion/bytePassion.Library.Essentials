using System.Collections;
using System.Windows;
using System.Windows.Media;

namespace bytePassion.Library.Essentials.WpfTools.Adorner
{

	public class FrameworkElementAdorner : System.Windows.Documents.Adorner
	{
		
		private readonly FrameworkElement adorner;
		
		private readonly AdornerPlacement horizontalAdornerPlacement = AdornerPlacement.Inside;
		private readonly AdornerPlacement verticalAdornerPlacement   = AdornerPlacement.Inside;
		
		private readonly double offsetX;
		private readonly double offsetY;
		
		public FrameworkElementAdorner (FrameworkElement adorner, FrameworkElement adornedElement)
			: base(adornedElement)
		{
			this.adorner = adorner;

			AddLogicalChild(adorner);
			AddVisualChild(adorner);
		}

		public FrameworkElementAdorner (FrameworkElement adorner, 
										FrameworkElement adornedElement,
									    AdornerPlacement horizontalAdornerPlacement, 
									    AdornerPlacement verticalAdornerPlacement,
									    double offsetX, 
										double offsetY)
			: this(adorner, adornedElement)
		{			
			this.horizontalAdornerPlacement = horizontalAdornerPlacement;
			this.verticalAdornerPlacement = verticalAdornerPlacement;
			this.offsetX = offsetX;
			this.offsetY = offsetY; 

			adornedElement.SizeChanged += (sender, args) => InvalidateMeasure();			
		}
				
		public double PositionX { get; set; } = double.NaN;
		public double PositionY { get; set; } = double.NaN;

		protected override Size MeasureOverride (Size constraint)
		{
			adorner.Measure(constraint);
			return adorner.DesiredSize;
		}

		protected override Size ArrangeOverride (Size finalSize)
		{
			var x = PositionX;
			var y = PositionY;

			if (double.IsNaN(x)) x = DetermineX();			
			if (double.IsNaN(y)) y = DetermineY();

			var adornerWidth = DetermineWidth();
			var adornerHeight = DetermineHeight();

			adorner.Arrange(new Rect(x, y, adornerWidth, adornerHeight));

			return finalSize;
		}

		private double DetermineX ()
		{
			switch (adorner.HorizontalAlignment)
			{
				case HorizontalAlignment.Left:
				{
					if (horizontalAdornerPlacement == AdornerPlacement.Outside)					
						return -adorner.DesiredSize.Width + offsetX;					
					else					
						return offsetX;					
				}
				case HorizontalAlignment.Right:
				{
					if (horizontalAdornerPlacement == AdornerPlacement.Outside)
					{
						var adornedWidth = AdornedElement.ActualWidth;
						return adornedWidth + offsetX;
					}
					else
					{
						var adornerWidth = adorner.DesiredSize.Width;
						var adornedWidth = AdornedElement.ActualWidth;
						var x = adornedWidth - adornerWidth;
						return x + offsetX;
					}
				}
				case HorizontalAlignment.Center:
				{
					var adornerWidth = adorner.DesiredSize.Width;
					var adornedWidth = AdornedElement.ActualWidth;
					var x = (adornedWidth / 2) - (adornerWidth / 2);
					return x + offsetX;
				}
				case HorizontalAlignment.Stretch:
				{
					return 0.0;
				}
			}
			return 0.0;
		}
		
		private double DetermineY ()
		{
			switch (adorner.VerticalAlignment)
			{
				case VerticalAlignment.Top:
				{
					if (verticalAdornerPlacement == AdornerPlacement.Outside)					
						return -adorner.DesiredSize.Height + offsetY;					
					else					
						return offsetY;
					
				}
				case VerticalAlignment.Bottom:
				{
					if (verticalAdornerPlacement == AdornerPlacement.Outside)
					{
						var adornedHeight = AdornedElement.ActualHeight;
						return adornedHeight + offsetY;
					}
					else
					{
						var adornerHeight = adorner.DesiredSize.Height;
						var adornedHeight = AdornedElement.ActualHeight;
						var x = adornedHeight - adornerHeight;
						return x + offsetY;
					}
				}
				case VerticalAlignment.Center:
				{
					var adornerHeight = adorner.DesiredSize.Height;
					var adornedHeight = AdornedElement.ActualHeight;
					var x = (adornedHeight / 2) - (adornerHeight / 2);
					return x + offsetY;
				}
				case VerticalAlignment.Stretch:
				{
					return 0.0;
				}
			}
			return 0.0;
		}
		
		private double DetermineWidth ()
		{
			if (!double.IsNaN(PositionX))			
				return adorner.DesiredSize.Width;
			
			switch (adorner.HorizontalAlignment)
			{
				case HorizontalAlignment.Left:    { return adorner.DesiredSize.Width;  }
				case HorizontalAlignment.Right:   { return adorner.DesiredSize.Width;  }
				case HorizontalAlignment.Center:  { return adorner.DesiredSize.Width;  }
				case HorizontalAlignment.Stretch: { return AdornedElement.ActualWidth; }
			}

			return 0.0;
		}
		
		private double DetermineHeight ()
		{
			if (!double.IsNaN(PositionY))			
				return adorner.DesiredSize.Height;
			
			switch (adorner.VerticalAlignment)
			{
				case VerticalAlignment.Top:     { return adorner.DesiredSize.Height;  }
				case VerticalAlignment.Bottom:  { return adorner.DesiredSize.Height;  }
				case VerticalAlignment.Center:  { return adorner.DesiredSize.Height;  }
				case VerticalAlignment.Stretch: { return AdornedElement.ActualHeight; }
			}
			return 0.0;
		}

		
		protected override int VisualChildrenCount => 1;
		protected override Visual GetVisualChild (int index) => adorner;

		public new FrameworkElement AdornedElement => (FrameworkElement)base.AdornedElement;

		protected override IEnumerator LogicalChildren
		{
			get
			{
				var list = new ArrayList
				           {
					           adorner
				           };
				return list.GetEnumerator();
			}
		}
		
		public void DisconnectChild ()
		{
			RemoveLogicalChild(adorner);
			RemoveVisualChild(adorner);
		}				
	}
}