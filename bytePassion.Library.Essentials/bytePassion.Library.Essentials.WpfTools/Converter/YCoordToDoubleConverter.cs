using System.Globalization;
using bytePassion.Library.Essentials.WpfTools.ConverterBase;
using bytePassion.Library.Essentials.WpfTools.Positioning;

namespace bytePassion.Library.Essentials.WpfTools.Converter
{
	public class YCoordToDoubleConverter : GenericValueConverter<YCoord, double>
    {
        protected override double Convert    (YCoord value, CultureInfo culture) => value.Value;
	    protected override YCoord ConvertBack(double value, CultureInfo culture) => new YCoord(value);
    }
}