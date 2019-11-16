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
                    Checks.ExecutePreconditionCheck(() => value > intIsAttribute.Value, () => $"Int must be higher than {intIsAttribute.Value} but found {value}");
                    break;
                case NumericComparisons.HigherOrEqualThan: 
                    Checks.ExecutePreconditionCheck(() => value >= intIsAttribute.Value, () => $"Int must be higher or equal than {intIsAttribute.Value} but found {value}");
                    break;
                case NumericComparisons.LowerThan: 
                    Checks.ExecutePreconditionCheck(() => value < intIsAttribute.Value, () => $"Int must be lower than {intIsAttribute.Value} but found {value}");
                    break;
                case NumericComparisons.LowerOrEqualThan:  
                    Checks.ExecutePreconditionCheck(() => value <= intIsAttribute.Value, () => $"Int must be lower or equal than {intIsAttribute.Value} but found {value}");
                    break;
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