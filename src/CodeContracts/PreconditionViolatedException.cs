namespace CodeContracts
{
    using System;
    using static ExceptionMessages;

    public class PreconditionViolatedException : Exception
    {
        public PreconditionViolatedException(string message)
            : base(ErrorMessage(message))
        {
        }

        public PreconditionViolatedException(string message, Exception exception)
            : base(ErrorMessage(message), exception)
        {
        }
        
        public static PreconditionViolatedException UnexpectedException(Exception exception) => new PreconditionViolatedException(UnexpectedExceptionMessage, exception);
        
        private static string ErrorMessage(string message) => $"Precondition violated: {message}";
    }
}