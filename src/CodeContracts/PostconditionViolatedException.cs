namespace CodeContracts
{
    using System;
    using static ExceptionMessages;

    public class PostconditionViolatedException : Exception
    {
        public PostconditionViolatedException(string message)
            : base(ErrorMessage(message))
        {
        }

        public PostconditionViolatedException(string message, Exception exception)
            : base(ErrorMessage(message), exception)
        {
        }

        public static PostconditionViolatedException UnexpectedException(Exception exception) => new PostconditionViolatedException(UnexpectedExceptionMessage, exception);

        private static string ErrorMessage(string message) => $"Postcondition violated: {message}";
    }
}