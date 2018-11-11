using System;

namespace bytePassion.Library.Essentials.Tools.SharedState
{
	public interface ISharedStateReadOnly<out T>
    {
        event Action<T> StateChanged;

        T Value { get; }
    }
}