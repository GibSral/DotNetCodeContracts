namespace CodeContracts.Attributes
{
    using System;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public class NotEmptyAttribute : Attribute
    {
        public static void CheckNotEmpty(ParameterInfo parameterInfo, object argument)
        {
            switch (argument)
            {
                case null: throw new PreconditionViolatedException($"Precondition violated: Argument {parameterInfo.Name} was null");
                case string str:
                    if (string.IsNullOrWhiteSpace(str))
                    {
                        throw new PreconditionViolatedException($"Precondition violated: Argument {parameterInfo.Name} was empty");
                    }

                    break;

                default: throw new PreconditionViolatedException($"Precondition violated: Argument {parameterInfo.Name} is not a string");
            }
        }
    }
}