namespace CodeContracts
{
    using System;
    using System.Reflection;

    public static class Checks
    {
        private const string UnknownException = "unknown exception";

        public static void ForeachParameter(object[] arguments, ParameterInfo[] parameterInfos, Action<object, ParameterInfo> assertion)
        {
            for (var i = 0; i < arguments.Length; i++)
            {
                var parameterInfo = parameterInfos[i];
                var argument = arguments[i];

                assertion(argument, parameterInfo);
            }
        }
        
        public static void ExecutePreconditionCheck(Func<bool> check, Func<string> errorMessage) =>
            ExecuteSafe(() => { ExecuteCheck(check, () => new PreconditionViolatedException(errorMessage())); },
                        it => new PreconditionViolatedException(UnknownException, it));

        public static void ExecutePostconditionCheck(Func<bool> check, Func<string> errorMessage) =>
            ExecuteSafe(() => { ExecuteCheck(check, () => new PostconditionViolatedException(errorMessage())); },
                        it => new PostconditionViolatedException(UnknownException, it));

        private static void ExecuteCheck(Func<bool> check, Func<Exception> onValidationError)
        {
            var result = check();
            if (!result)
            {
                throw onValidationError();
            }
        }

        public static Action<ParameterInfo, object, Attribute> WithoutAttribute(Action<ParameterInfo, object> check) => (parameterInfo, value, _) => check(parameterInfo, value);

        private static void ExecuteSafe(Action action, Func<Exception, Exception> wrapException)
        {
            try
            {
                action();
            }
            catch (Exception exception) when(ExceptionIsNoCodeContractException(exception))
            {
                throw wrapException(exception);
            }
        }

        private static bool ExceptionIsNoCodeContractException(Exception exception) => !(exception is PreconditionViolatedException) && !(exception is PostconditionViolatedException);
    }
}