using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace bytePassion.Library.Essentials.WpfTools.Adorner
{
	public class AdornedControl : ContentControl
	{
		private enum AdornerShowState
		{
			Visible,
			Hidden,
			FadingIn,
			FadingOut,
		}

		#region Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///////////                                                                                            //////////
		///////////                              dependy property definitions                                  //////////
		///////////                                                                                            //////////
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static readonly DependencyProperty IsAdornerVisibleProperty =
			DependencyProperty.Register(nameof(IsAdornerVisible), 
										typeof(bool), 
										typeof(AdornedControl),
										new FrameworkPropertyMetadata(IsAdornerVisible_PropertyChanged));

		public static readonly DependencyProperty AdornerContentProperty =
			DependencyProperty.Register(nameof(AdornerContent), 
										typeof(FrameworkElement), 
										typeof(AdornedControl),
										new FrameworkPropertyMetadata(AdornerContent_PropertyChanged));

		public static readonly DependencyProperty HorizontalAdornerPlacementProperty =
			DependencyProperty.Register(nameof(HorizontalAdornerPlacement), 
										typeof(AdornerPlacement), 
										typeof(AdornedControl),
										new FrameworkPropertyMetadata(AdornerPlacement.Inside));

		public static readonly DependencyProperty VerticalAdornerPlacementProperty =
			DependencyProperty.Register(nameof(VerticalAdornerPlacement), 
										typeof(AdornerPlacement), 
										typeof(AdornedControl),
										new FrameworkPropertyMetadata(AdornerPlacement.Inside));

		public static readonly DependencyProperty AdornerOffsetXProperty =
			DependencyProperty.Register(nameof(AdornerOffsetX), 
										typeof(double), 
										typeof(AdornedControl));

		public static readonly DependencyProperty AdornerOffsetYProperty =
			DependencyProperty.Register(nameof(AdornerOffsetY), 
										typeof(double), 
										typeof(AdornedControl));

		public static readonly DependencyProperty IsMouseOverShowEnabledProperty =
			DependencyProperty.Register(nameof(IsMouseOverShowEnabled), 
										typeof(bool), 
										typeof(AdornedControl),
										new FrameworkPropertyMetadata(true, IsMouseOverShowEnabled_PropertyChanged));

		public static readonly DependencyProperty FadeInTimeProperty =
			DependencyProperty.Register(nameof(FadeInTime), 
										typeof(double), 
										typeof(AdornedControl),
										new FrameworkPropertyMetadata(0.25));

		public static readonly DependencyProperty FadeOutTimeProperty =
			DependencyProperty.Register(nameof(FadeOutTime), 
										typeof(double), 
										typeof(AdornedControl),
										new FrameworkPropertyMetadata(1.0));

		public static readonly DependencyProperty CloseAdornerTimeOutProperty =
			DependencyProperty.Register(nameof(CloseAdornerTimeOut), 
										typeof(double), 
										typeof(AdornedControl),
										new FrameworkPropertyMetadata(2.0, CloseAdornerTimeOut_PropertyChanged));

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///////////                                                                                            //////////
		///////////                          dependy property wrapping properties                              //////////
		///////////                                                                                            //////////
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public bool IsAdornerVisible
		{
			get { return (bool)GetValue(IsAdornerVisibleProperty); }
			set { SetValue(IsAdornerVisibleProperty, value); }
		}

		public FrameworkElement AdornerContent
		{
			get { return (FrameworkElement)GetValue(AdornerContentProperty); }
			set { SetValue(AdornerContentProperty, value); }
		}

		public AdornerPlacement HorizontalAdornerPlacement
		{
			get { return (AdornerPlacement)GetValue(HorizontalAdornerPlacementProperty); }
			set { SetValue(HorizontalAdornerPlacementProperty, value); }
		}


		public AdornerPlacement VerticalAdornerPlacement
		{
			get { return (AdornerPlacement)GetValue(VerticalAdornerPlacementProperty); }
			set { SetValue(VerticalAdornerPlacementProperty, value); }
		}


		public double AdornerOffsetX
		{
			get { return (double)GetValue(AdornerOffsetXProperty); }
			set { SetValue(AdornerOffsetXProperty, value); }
		}

		public double AdornerOffsetY
		{
			get { return (double)GetValue(AdornerOffsetYProperty); }
			set { SetValue(AdornerOffsetYProperty, value); }
		}

		public bool IsMouseOverShowEnabled
		{
			get { return (bool)GetValue(IsMouseOverShowEnabledProperty); }
			set { SetValue(IsMouseOverShowEnabledProperty, value); }
		}

		public double FadeInTime
		{
			get { return (double)GetValue(FadeInTimeProperty); }
			set { SetValue(FadeInTimeProperty, value); }
		}

		public double FadeOutTime
		{
			get { return (double)GetValue(FadeOutTimeProperty); }
			set { SetValue(FadeOutTimeProperty, value); }
		}

		public double CloseAdornerTimeOut
		{
			get { return (double)GetValue(CloseAdornerTimeOutProperty); }
			set { SetValue(CloseAdornerTimeOutProperty, value); }
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///////////                                                                                            //////////
		///////////                            dependy property changed methonds                               //////////
		///////////                                                                                            //////////
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static void IsAdornerVisible_PropertyChanged (DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			((AdornedControl)o).ShowOrHideAdornerInternal();
		}

		private static void IsMouseOverShowEnabled_PropertyChanged (DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var c = (AdornedControl)o;
			c.closeAdornerTimer.Stop();
			c.HideAdorner();
		}

		private static void CloseAdornerTimeOut_PropertyChanged (DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var c = (AdornedControl)o;
			c.closeAdornerTimer.Interval = TimeSpan.FromSeconds(c.CloseAdornerTimeOut);
		}

		private static void AdornerContent_PropertyChanged (DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var c = (AdornedControl)o;
			c.ShowOrHideAdornerInternal();

			var oldAdornerContent = (FrameworkElement)e.OldValue;
			if (oldAdornerContent != null)
			{
				oldAdornerContent.MouseEnter -= c.OnAdornerContentMouseEnter;
				oldAdornerContent.MouseLeave -= c.OnAdornerContentMouseLeave;
			}

			var newAdornerContent = (FrameworkElement)e.NewValue;
			if (newAdornerContent != null)
			{
				newAdornerContent.MouseEnter += c.OnAdornerContentMouseEnter;
				newAdornerContent.MouseLeave += c.OnAdornerContentMouseLeave;
			}
		}

		private void OnAdornerContentMouseEnter (object sender, MouseEventArgs e) { MouseEnterLogic(); }
		private void OnAdornerContentMouseLeave (object sender, MouseEventArgs e) { MouseLeaveLogic(); }

		#endregion

		#region Commands

		public static readonly RoutedCommand ShowAdornerCommand    = new RoutedCommand("ShowAdorner",    typeof(AdornedControl));
		public static readonly RoutedCommand FadeInAdornerCommand  = new RoutedCommand("FadeInAdorner",  typeof(AdornedControl));
		public static readonly RoutedCommand HideAdornerCommand    = new RoutedCommand("HideAdorner",    typeof(AdornedControl));
		public static readonly RoutedCommand FadeOutAdornerCommand = new RoutedCommand("FadeOutAdorner", typeof(AdornedControl));

		private static readonly CommandBinding ShowAdornerCommandBinding    = new CommandBinding(ShowAdornerCommand,   ExecuteShowAdorner);
		private static readonly CommandBinding FadeInAdornerCommandBinding  = new CommandBinding(FadeInAdornerCommand, ExecuteFadeInAdorner);
		private static readonly CommandBinding HideAdornerCommandBinding    = new CommandBinding(HideAdornerCommand,   ExecuteHideAdorner);
		private static readonly CommandBinding FadeOutAdornerCommandBinding = new CommandBinding(FadeInAdornerCommand, ExecuteFadeOutAdorner);

		private static void ExecuteShowAdorner    (object target, ExecutedRoutedEventArgs e) { ((AdornedControl)target).ShowAdorner();    }		
		private static void ExecuteFadeInAdorner  (object target, ExecutedRoutedEventArgs e) { ((AdornedControl)target).FadeOutAdorner(); }		
		private static void ExecuteHideAdorner    (object target, ExecutedRoutedEventArgs e) { ((AdornedControl)target).HideAdorner();    }		
		private static void ExecuteFadeOutAdorner (object target, ExecutedRoutedEventArgs e) { ((AdornedControl)target).FadeOutAdorner(); }

		static AdornedControl ()
		{
			CommandManager.RegisterClassCommandBinding(typeof(AdornedControl), ShowAdornerCommandBinding);
			CommandManager.RegisterClassCommandBinding(typeof(AdornedControl), FadeOutAdornerCommandBinding);
			CommandManager.RegisterClassCommandBinding(typeof(AdornedControl), HideAdornerCommandBinding);
			CommandManager.RegisterClassCommandBinding(typeof(AdornedControl), FadeInAdornerCommandBinding);
		}

		#endregion Commands

		private readonly DispatcherTimer closeAdornerTimer = new DispatcherTimer();

		private AdornerShowState adornerShowState = AdornerShowState.Hidden;
		private AdornerLayer adornerLayer = null;
		private FrameworkElementAdorner currentDisplayedAdorner = null;		

		public AdornedControl ()
		{			
			DataContextChanged += (sender, args) => UpdateAdornerDataContext();

			closeAdornerTimer.Tick += (sender, args) =>
			                          {
										  closeAdornerTimer.Stop();
										  FadeOutAdorner();
									  };

			closeAdornerTimer.Interval = TimeSpan.FromSeconds(CloseAdornerTimeOut);
		}
				
		private void ShowAdorner ()
		{
			IsAdornerVisible = true;
		}
		
		private void HideAdorner ()
		{
			IsAdornerVisible = false;
		}
		
		private void FadeInAdorner ()
		{
			if (adornerShowState == AdornerShowState.Visible ||
				adornerShowState == AdornerShowState.FadingIn)
			{
				// Already visible or fading in.
				return;
			}

			ShowAdorner();

			if (adornerShowState != AdornerShowState.FadingOut)
			{
				currentDisplayedAdorner.Opacity = 0.0;
			}

			var doubleAnimation = new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(FadeInTime)));
			doubleAnimation.Completed += (sender, args) => adornerShowState = AdornerShowState.Visible;
			doubleAnimation.Freeze();

			currentDisplayedAdorner.BeginAnimation(OpacityProperty, doubleAnimation);

			adornerShowState = AdornerShowState.FadingIn;
		}
		
		private void FadeOutAdorner ()
		{
			if (adornerShowState == AdornerShowState.FadingOut ||
				adornerShowState == AdornerShowState.Hidden)
			{
				
				return;
			}
			
			var fadeOutAnimation = new DoubleAnimation(0.0, new Duration(TimeSpan.FromSeconds(FadeOutTime)));
			fadeOutAnimation.Completed += (sender, args) =>
			                              {
				                              if (adornerShowState == AdornerShowState.FadingOut)
					                              HideAdorner();
			                              };
			fadeOutAnimation.Freeze();

			currentDisplayedAdorner.BeginAnimation(OpacityProperty, fadeOutAnimation);

			adornerShowState = AdornerShowState.FadingOut;
		}

		
	
		#region Private/Internal Functions
	
		
		

		
		private void ShowOrHideAdornerInternal ()
		{
			if (IsAdornerVisible)			
				ShowAdornerInternal();			
			else			
				HideAdornerInternal();			
		}

				
		private void ShowAdornerInternal ()
		{
			if (currentDisplayedAdorner != null)							
				return;
			
			if (AdornerContent != null)
			{
				if (adornerLayer == null)				
					adornerLayer = AdornerLayer.GetAdornerLayer(this);
				
				if (adornerLayer != null)
				{										
					currentDisplayedAdorner = new FrameworkElementAdorner(AdornerContent, this,
																		  HorizontalAdornerPlacement, 
																		  VerticalAdornerPlacement,
																		  AdornerOffsetX, AdornerOffsetY);
					adornerLayer.Add(currentDisplayedAdorner);
					UpdateAdornerDataContext();
				}
			}

			adornerShowState = AdornerShowState.Visible;
		}
		
		private void HideAdornerInternal ()
		{
			if (adornerLayer == null || currentDisplayedAdorner == null)						
				return;			
			
			closeAdornerTimer.Stop();
			adornerLayer.Remove(currentDisplayedAdorner);
			currentDisplayedAdorner.DisconnectChild();

			currentDisplayedAdorner = null;
			adornerLayer = null;
			
			adornerShowState = AdornerShowState.Hidden;
		}

		
		public override void OnApplyTemplate ()
		{
			base.OnApplyTemplate();
			ShowOrHideAdornerInternal();
		}
		
		protected override void OnMouseEnter (MouseEventArgs e)
		{
			base.OnMouseEnter(e);
			MouseEnterLogic();
		}
		
		protected override void OnMouseLeave (MouseEventArgs e)
		{
			base.OnMouseLeave(e);
			MouseLeaveLogic();
		}

		
		private void MouseEnterLogic ()
		{
			if (!IsMouseOverShowEnabled)			
				return;
			
			closeAdornerTimer.Stop();
			FadeInAdorner();
		}
		
		private void MouseLeaveLogic ()
		{
			if (!IsMouseOverShowEnabled)			
				return;
			
			closeAdornerTimer.Start();
		}

		private void UpdateAdornerDataContext ()
		{
			if (AdornerContent != null)
			{
				AdornerContent.DataContext = DataContext;
			}
		}

		#endregion
	}
}