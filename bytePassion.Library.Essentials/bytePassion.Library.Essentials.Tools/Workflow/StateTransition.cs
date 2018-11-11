using bytePassion.Library.Essentials.Tools.FrameworkExtensions;

namespace bytePassion.Library.Essentials.Tools.Workflow
{
	public class StateTransition<TState, TEvent>        
    {               
        public StateTransition(TState stateBefore, TEvent transitionEvent, TState stateAfter)
        {
            StateBefore = stateBefore;
            TransitionEvent = transitionEvent;
            StateAfter = stateAfter;
        }

        public TState StateBefore     { get; }
        public TEvent TransitionEvent { get; }
        public TState StateAfter      { get; }

        public override int GetHashCode()
        {
            return StateBefore.GetHashCode() ^ 
                   TransitionEvent.GetHashCode() ^ 
                   StateAfter.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj, (st1, st2) => st1.StateBefore.Equals(st2) && 
                                                  st1.TransitionEvent.Equals(st2) &&
                                                  st1.StateAfter.Equals(st2.StateAfter));
        }

        public override string ToString()
        {
            return $"[{StateBefore}] --- {TransitionEvent} ---> [{StateAfter}]";
        }
    }
}