using System;

namespace bytePassion.Library.Essentials.WpfTools.Command.Updater
{
	public interface ICommandUpdater : IDisposable
    {
        event EventHandler UpdateOfCanExecuteChangedRequired;
    }
}