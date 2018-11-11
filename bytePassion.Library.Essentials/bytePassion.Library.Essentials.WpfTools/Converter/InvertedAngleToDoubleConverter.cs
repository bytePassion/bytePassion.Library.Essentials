using System.Globalization;
using bytePassion.Library.Essentials.WpfTools.ConverterBase;
using bytePassion.Library.Essentials.WpfTools.Positioning;

namespace bytePassion.Library.Essentials.WpfTools.Converter
{
	public class InvertedAngleToDoubleConverter : GenericValueConverter<Angle, double>
    {
        protected override double Convert(Angle angle, CultureInfo culture)
        {
            return angle.Inverted.Value;
        }

        protected override Angle ConvertBack(double value, CultureInfo culture)
        {
            return new Angle(new Degree(value)).Inverted;
        }
    }
}