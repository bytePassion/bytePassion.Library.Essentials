using System.ComponentModel;

namespace bytePassion.Library.Essentials.Tools.UndoRedo
{
	public interface IUndoRedo : INotifyPropertyChanged
	{
		void Undo();
		void Redo();

		bool UndoPossible { get; }
		bool RedoPossible { get; }
	}
}
