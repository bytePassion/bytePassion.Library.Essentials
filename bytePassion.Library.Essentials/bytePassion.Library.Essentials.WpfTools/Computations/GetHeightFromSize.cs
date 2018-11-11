using System.Globalization;
using bytePassion.Library.Essentials.WpfTools.ConverterBase;
using bytePassion.Library.Essentials.WpfTools.Positioning;

namespace bytePassion.Library.Essentials.WpfTools.Computations
{
	public class GetHeightFromSize : GenericValueConverter<Size, double>
	{
		protected override double Convert(Size value, CultureInfo culture)
		{
			return value.Height.Value;
		}
	}
}
