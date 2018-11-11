using System;
using System.Collections.Generic;
using System.Linq;

namespace bytePassion.Library.Essentials.Tools.Workflow
{

	public class WorkflowEngine<TState, TEvent> : IWorkflowEngine<TState, TEvent>
    {
        public event Action<TState> StateChanged;

        private readonly IReadOnlyList<StateTransition<TState, TEvent>> transitions;

        private TState currentState;

        public TState CurrentState
        {
            get { return currentState; }
            private set
            {
                if (!currentState.Equals(value))
                {
                    currentState = value;
                    StateChanged?.Invoke(CurrentState);
                }
            }
        }

        public WorkflowEngine(IReadOnlyList<StateTransition<TState, TEvent>> transitions, 
                              TState initialState)
        {
            this.transitions = transitions;
            CurrentState = initialState;            
        } 
		
        public bool IsEventApplyable(TEvent transitionEvent)
        {
            return transitions.Any(transition => transition.TransitionEvent.Equals(transitionEvent) &&
                                                 transition.StateBefore.Equals(CurrentState));
        }
        
        public void ApplyEvent(TEvent transitionEvent)
        {
            var currentTransition = transitions.FirstOrDefault(transition => transition.TransitionEvent.Equals(transitionEvent) &&
                                                                             transition.StateBefore.Equals(CurrentState));
            if (currentTransition == null)
                throw new IllegalStateTransitionException($"there is no transition from >>{CurrentState}<< with the Event >>{transitionEvent}<<");
            
            CurrentState = currentTransition.StateAfter;            
        }
    }
}
