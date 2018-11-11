using bytePassion.Library.Essentials.Tools.FrameworkExtensions;

namespace bytePassion.Library.Essentials.WpfTools.Positioning
{
	public class AngleInterval
    {
	    public AngleInterval(Angle from, Angle to)
	    {
		    From = from;
		    To = to;
	    }

		public Angle From { get; }
	    public Angle To   { get; }

	    public Angle AbsolutAngleValue => (From < To) ? To - From : new Angle(new Degree(360 - From.Value + To.Value));

	    public bool IsAngleWithin(Angle a)
	    {
		    return To > From 
					? a < To && a > From 
					: a < To || a > From;
	    }

		public bool IsOverlappingWith(AngleInterval ai)
	    {			
			if (IsAngleWithin(ai.From)) return true;
			if (IsAngleWithin(ai.To))   return true;
			if (ai.IsAngleWithin(From)) return true;
			if (ai.IsAngleWithin(To))   return true;
			
			if (this == ai) return true;

			return false;
	    }
		
		/// <summary>
		/// if referenceInterval covers this interval completely, the result is 1 (equals 100%)
		/// if referenceInterval lies within this interval, the result is between 0 and 1
		/// </summary>
		/// <param name="referenceInterval"></param>
		/// <returns></returns>
	    public double GetPercentageOverlappingTo(AngleInterval referenceInterval)
	    {
			if (referenceInterval.IsAngleWithin(From) && referenceInterval.IsAngleWithin(To))
				return 1.0;

			if (!referenceInterval.IsOverlappingWith(this))
				return 0.0;

			var fromInsideThis = IsAngleWithin(referenceInterval.From);
			var toInsideThis = IsAngleWithin(referenceInterval.To);

			Angle overlapping;
			if (fromInsideThis && toInsideThis)
				overlapping = referenceInterval.AbsolutAngleValue;
			else if (fromInsideThis)
				overlapping = new AngleInterval(referenceInterval.From, To).AbsolutAngleValue;
			else
				overlapping = new AngleInterval(From, referenceInterval.To).AbsolutAngleValue; 

			return overlapping / AbsolutAngleValue;
	    }

	    public override bool   Equals(object obj) => this.Equals(obj, (ai1, ai2) => true); // TODO !!!!!!!!!!!
	    public override int    GetHashCode()      => From.GetHashCode() ^ To.GetHashCode();
	    public override string ToString()         => $"[{From};{To}]";

	    public static bool operator == (AngleInterval a1, AngleInterval a2) => EqualsExtension.EqualsForEqualityOperator(a1,a2);
	    public static bool operator != (AngleInterval a1, AngleInterval a2) => !(a1 == a2);
    }
}
