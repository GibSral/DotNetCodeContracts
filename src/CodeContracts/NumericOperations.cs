namespace CodeContracts
{
    using System;
    using System.Reflection;
    using Aspects;
    using Attributes;

    public static class NumericOperations
    {
        public static void CheckIntIs(ParameterInfo parameterInfo, object argument, Attribute attribute)
        {
            var intIsAttribute = (IntIsAttribute)attribute;
            var value = ConvertToInt(argument, parameterInfo.Name);
            switch (intIsAttribute.NumericComparison)
            {
                case NumericComparisons.HigherThan:
                    Checks.ExecutePreconditionCheck(() => value > intIsAttribute.Number, () => $"Int must be higher than {intIsAttribute.Number} but found {value}");
                    break;
                case NumericComparisons.HigherOrEqualThan: 
                    Checks.ExecutePreconditionCheck(() => value >= intIsAttribute.Number, () => $"Int must be higher or equal than {intIsAttribute.Number} but found {value}");
                    break;
                case NumericComparisons.LowerThan: break;
                case NumericComparisons.LowerOrEqualThan: break;
                default: throw new PreconditionViolatedException("unknown numeric comparison");
            }
        }

        private static int ConvertToInt(object argument, string parameterName)
        {
            var intValue = (int)argument;
            return intValue;
        }
    }
}