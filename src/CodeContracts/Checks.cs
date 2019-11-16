namespace CodeContracts
{
    using System;
    using System.Reflection;
    using Aspects;

    public static class Checks
    {
        public static void ExecutePreconditionCheck(Func<bool> check, Func<string> errorMessage)
        {
            var result = check();
            if (!result)
            {
                var message = $"{Messages.PreconditionViolated} {errorMessage()}";
                throw new PreconditionViolatedException(message);
            }
        }

        public static Action<ParameterInfo, object, Attribute> WithoutAttribute(Action<ParameterInfo, object> check) => (parameterInfo, value, _) => check(parameterInfo, value);
    }
}