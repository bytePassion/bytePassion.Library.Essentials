using System;

namespace bytePassion.Library.Essentials.Tools.SemanticTypeBase
{
    public abstract class SimpleDoubleSemanticType : SemanticType<double>
    {        
        protected SimpleDoubleSemanticType(double value, string unit = "")
            : base(value, unit)
        {            
        }
        
        protected override Func<SemanticType<double>, SemanticType<double>, bool> EqualsFunc
        {
            get { return (st1, st2) => DoubleUtils.DoubleEquals(st1.Value, st2.Value); }
        }
        
        protected override string StringRepresentation => $"{DoubleUtils.DoubleFormat(Value)}";        

        public static implicit operator double(SimpleDoubleSemanticType doubleType)
        {
            return doubleType.Value;
        }
    }
}
