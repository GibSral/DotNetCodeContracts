namespace CodeContracts.UnitTests
{
    using System;

    public class PostconditionViolatedException : Exception
    {
        public PostconditionViolatedException(string message)
            : base(message)
        {
        }
    }
}