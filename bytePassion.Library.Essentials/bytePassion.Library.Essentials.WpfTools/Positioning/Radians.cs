using bytePassion.Library.Essentials.Tools.SemanticTypeBase;

namespace bytePassion.Library.Essentials.WpfTools.Positioning
{

    public class Radians : SimpleDoubleSemanticType
    {
        public Radians(double value)
            : base(value, "rad")
        {            
        }       
    }
}