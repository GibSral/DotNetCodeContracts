namespace CodeContracts
{
    using System;
    using System.Reflection;
    using Attributes;

    public static class NumericOperations
    {
        public static int ConvertToInt(object argument, ParameterInfo parameterInfo)
        {
            try
            {
                return (int)argument;
            }
            catch (InvalidCastException exception)
            {
                throw new PreconditionViolatedException($"{parameterInfo} should be of type int but found {parameterInfo.ParameterType.FullName}", exception);
            }
        }
    }
}