namespace CodeContracts
{
    using System;
    using System.Reflection;
    using Aspects;
    using UnitTests;

    public static class Checks
    {
        public static void ExecutePreconditionCheck(Func<bool> check, Func<string> errorMessage)
        {
            var result = check();
            if (!result)
            {
                var message = ErrorMessage(Messages.PreconditionViolated, errorMessage);
                throw new PreconditionViolatedException(message);
            }
        }
        
        public static void ExecutePostconditionCheck(Func<bool> check, Func<string> errorMessage)
        {
            var result = check();
            if (!result)
            {
                var message = ErrorMessage(Messages.PostconditionViolated, errorMessage);
                throw new PostconditionViolatedException(message);
            }
        }

        private static string ErrorMessage(string messageHeader, Func<string> errorMessage) => $"{messageHeader} {errorMessage()}";

        public static Action<ParameterInfo, object, Attribute> WithoutAttribute(Action<ParameterInfo, object> check) => (parameterInfo, value, _) => check(parameterInfo, value);
    }
}