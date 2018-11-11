using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace bytePassion.Library.Essentials.WpfTools.Adorner
{
	public class UIElementAdorner : System.Windows.Documents.Adorner
    {
        private readonly AdornerLayer layer;

        private readonly UIElement adornerView;
        private double leftOffset;
        private double topOffset;

        public UIElementAdorner(UIElement adornedElement, UIElement adornerView, AdornerLayer layer) : base(adornedElement)
        {
            this.layer = layer;
			this.adornerView = adornerView;

			Focusable = false;
            IsHitTestVisible = false;	        
            layer.Add(this);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            adornerView.Measure(constraint);
            return adornerView.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            adornerView.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return adornerView;
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        public void UpdatePosition(double left, double top)
        {
            leftOffset = left;
            topOffset = top;
	        layer?.Update(AdornedElement);
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            var result = new GeneralTransformGroup();
	        // ReSharper disable once AssignNullToNotNullAttribute
            result.Children.Add(base.GetDesiredTransform(transform));
            result.Children.Add(new TranslateTransform(leftOffset, topOffset));
            return result;
        }

        public void Destroy()
        {
            layer.Remove(this);
        }

    }
}