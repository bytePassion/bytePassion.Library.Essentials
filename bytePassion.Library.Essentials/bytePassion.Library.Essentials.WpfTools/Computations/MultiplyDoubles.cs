using System.Globalization;
using bytePassion.Library.Essentials.WpfTools.ConverterBase;

namespace bytePassion.Library.Essentials.WpfTools.Computations
{
	public class MultiplyDoubles : GenericTwoToOneValueConverter<double, double, double>
    {
	    protected override double Convert(double value1, double value2, CultureInfo culture)
	    {
		    return value1 * value2;
	    }	   
    }
}
