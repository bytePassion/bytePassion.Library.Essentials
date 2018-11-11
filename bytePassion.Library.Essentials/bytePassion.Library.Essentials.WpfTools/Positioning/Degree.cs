using bytePassion.Library.Essentials.Tools.SemanticTypeBase;

namespace bytePassion.Library.Essentials.WpfTools.Positioning
{
    public class Degree : SimpleDoubleSemanticType
    {        
        public Degree(double value)
            : base(value, "deg")
        {            
        }        
    }
}
