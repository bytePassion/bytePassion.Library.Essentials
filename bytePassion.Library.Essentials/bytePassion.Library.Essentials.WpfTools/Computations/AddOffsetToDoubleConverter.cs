using System.Globalization;
using bytePassion.Library.Essentials.WpfTools.ConverterBase;

namespace bytePassion.Library.Essentials.WpfTools.Computations
{
	public class AddOffsetToDoubleConverter : GenericParameterizedValueConverter<double, double, string>
	{
		protected override double Convert(double value, string parameter, CultureInfo culture)
		{
			return value + double.Parse(parameter);
		}		
	}
}
