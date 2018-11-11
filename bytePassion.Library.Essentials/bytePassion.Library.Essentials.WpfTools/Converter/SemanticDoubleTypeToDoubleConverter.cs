using System.Globalization;
using bytePassion.Library.Essentials.Tools.SemanticTypeBase;
using bytePassion.Library.Essentials.WpfTools.ConverterBase;

namespace bytePassion.Library.Essentials.WpfTools.Converter
{
	public class SemanticDoubleTypeToDoubleConverter : GenericValueConverter<SemanticType<double>, double>
    {
        protected override double Convert(SemanticType<double> value, CultureInfo culture)
        {
            return value.Value;
        }
    }
}
