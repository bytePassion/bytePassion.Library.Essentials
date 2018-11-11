using System.Globalization;
using bytePassion.Library.Essentials.WpfTools.ConverterBase;
using bytePassion.Library.Essentials.WpfTools.Positioning;

namespace bytePassion.Library.Essentials.WpfTools.Converter
{
	public class AngleToDoubleConverter : GenericValueConverter<Angle, double>
    {
	    protected override double Convert    (Angle  angle, CultureInfo culture) => angle.Value;
	    protected override Angle  ConvertBack(double value, CultureInfo culture) => new Angle(new Degree(value));
    }
}
