using System.Globalization;
using System.Windows;
using bytePassion.Library.Essentials.WpfTools.ConverterBase;

namespace bytePassion.Library.Essentials.WpfTools.Converter
{
	public class InvertedBoolToCollapsedVisibilityConverter : GenericValueConverter<bool, Visibility>
    {
	    protected override Visibility Convert(bool value, CultureInfo culture)
	    {
			return value ? Visibility.Collapsed : Visibility.Visible;
	    }	    
    }
}
