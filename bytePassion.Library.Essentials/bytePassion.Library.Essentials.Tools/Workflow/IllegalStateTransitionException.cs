using System;

namespace bytePassion.Library.Essentials.Tools.Workflow
{
	public class IllegalStateTransitionException : Exception
    {
        public IllegalStateTransitionException()
        {
        }

        public IllegalStateTransitionException(string message)
			: base(message)
		{
        }

        public IllegalStateTransitionException(string format, params object[] args)
			: base(string.Format(format, args))
		{
        }

        public IllegalStateTransitionException(string message, Exception innerException)
			: base(message, innerException)
		{
        }

        public IllegalStateTransitionException(string format, Exception innerException, params object[] args)
			: base(string.Format(format, args), innerException)
		{
        }
    }
}
