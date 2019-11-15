namespace CodeContracts
{
    using System;

    public class PreconditionViolatedException : Exception
    {
        public PreconditionViolatedException(string message)
            : base(message)
        {
        }
    }
}