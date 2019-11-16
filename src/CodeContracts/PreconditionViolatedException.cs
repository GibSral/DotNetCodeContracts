namespace CodeContracts
{
    using System;

    public class PreconditionViolatedException : Exception
    {
        public PreconditionViolatedException(string message)
            : base(message)
        {
        }
        
        public PreconditionViolatedException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}