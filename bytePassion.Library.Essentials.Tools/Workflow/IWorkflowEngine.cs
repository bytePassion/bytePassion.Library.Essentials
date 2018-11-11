using System;

namespace bytePassion.Library.Essentials.Tools.Workflow
{
	public interface IWorkflowEngine<out TState, in TEvent>
    {
        event Action<TState> StateChanged;
		
        TState CurrentState { get; }
		        
        void ApplyEvent(TEvent transitionEvent);
    }
}