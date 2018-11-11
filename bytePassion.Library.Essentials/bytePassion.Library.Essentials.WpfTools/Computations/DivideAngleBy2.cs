using System.Globalization;
using bytePassion.Library.Essentials.WpfTools.ConverterBase;
using bytePassion.Library.Essentials.WpfTools.Positioning;

namespace bytePassion.Library.Essentials.WpfTools.Computations
{
	public class DivideAngleBy2 : GenericParameterizedValueConverter<Angle, double, bool>
    {	   
	    protected override double Convert(Angle value, bool invert, CultureInfo culture)
	    {
			return (invert ? -1 : 1) * value.Value / 2.0;
	    }	   
    }
}
