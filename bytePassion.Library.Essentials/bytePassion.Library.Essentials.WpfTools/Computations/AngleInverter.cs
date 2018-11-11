using System.Globalization;
using bytePassion.Library.Essentials.WpfTools.ConverterBase;
using bytePassion.Library.Essentials.WpfTools.Positioning;

namespace bytePassion.Library.Essentials.WpfTools.Computations
{
    public class AngleInverter : GenericValueConverter<Angle, double>
    {
	    protected override double Convert(Angle value, CultureInfo culture)
	    {
		    return 360.0 - value.PosValue.Value;
	    }	    
    }
}
