namespace bytePassion.Library.Essentials.Tools.SharedState
{
	public interface ISharedStateWriteOnly<in T>
    {
        T Value { set; }
    }
}