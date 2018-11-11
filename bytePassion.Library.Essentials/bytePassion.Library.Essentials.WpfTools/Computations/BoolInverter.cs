using System.Globalization;
using bytePassion.Library.Essentials.WpfTools.ConverterBase;

namespace bytePassion.Library.Essentials.WpfTools.Computations
{
	public class BoolInverter : GenericValueConverter<bool, bool>
	{
		protected override bool Convert    (bool value, CultureInfo culture) => !value;
		protected override bool ConvertBack(bool value, CultureInfo culture) => !value;
	}
}
