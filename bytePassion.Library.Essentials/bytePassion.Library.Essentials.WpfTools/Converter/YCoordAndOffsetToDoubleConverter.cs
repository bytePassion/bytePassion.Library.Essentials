using System.Globalization;
using bytePassion.Library.Essentials.WpfTools.ConverterBase;
using bytePassion.Library.Essentials.WpfTools.Positioning;

namespace bytePassion.Library.Essentials.WpfTools.Converter
{
	public class YCoordAndOffsetToDoubleConverter : GenericParameterizedValueConverter<YCoord, double, double>
	{
		protected override double Convert     (YCoord value, double offset, CultureInfo culture) => value.Value + offset;
		protected override YCoord ConvertBack (double value, double offset, CultureInfo culture) => new YCoord(value - offset);
	}
}