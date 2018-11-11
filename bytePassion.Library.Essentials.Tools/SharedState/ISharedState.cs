using System;

namespace bytePassion.Library.Essentials.Tools.SharedState
{
	public interface ISharedState<T>
	{
		event Action<T> StateChanged;

		T Value { get; set; }
	}
}