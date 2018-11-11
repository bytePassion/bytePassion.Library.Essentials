using System;
using bytePassion.Library.Essentials.Tools.FrameworkExtensions;
using bytePassion.Library.Essentials.Tools.SharedState;

namespace bytePassion.Library.Essentials.WpfTools.Command.Updater
{
	public class SharedStateCommandUpdate<T> : DisposingObject, ICommandUpdater
    {
        public event EventHandler UpdateOfCanExecuteChangedRequired;

        private readonly ISharedState<T> sharedState;
        
        public SharedStateCommandUpdate(ISharedState<T> sharedState)
        {
            this.sharedState = sharedState;

            sharedState.StateChanged += OnGlobalStateChanged;
        }

        private void OnGlobalStateChanged(T newValue)
        {
            UpdateOfCanExecuteChangedRequired?.Invoke(this, new EventArgs());
        }

        protected override void CleanUp()
        {
            sharedState.StateChanged -= OnGlobalStateChanged;
        }        
    }
}