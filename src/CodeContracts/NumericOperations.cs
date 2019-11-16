namespace CodeContracts
{
    using System;
    using System.Reflection;
    using Attributes;

    public static class NumericOperations
    {
        public static void CheckIntIsInRange(ParameterInfo parameterInfo, object argument, Attribute attribute)
        {
            var intIsAttribute = (IntIsInRangeAttribute)attribute;
            var value = ConvertToInt(argument, parameterInfo.Name);
            switch (intIsAttribute.IncludingBorders)
            {
                case IncludingBorders.None:
                    Checks.ExecutePreconditionCheck(() => value > intIsAttribute.Lower && value < intIsAttribute.Upper,
                                                    () => $"{parameterInfo.Name} must be in Range ]{intIsAttribute.Lower}..{intIsAttribute.Upper}[ but found {value}");
                    break;
                case IncludingBorders.Lower:
                    Checks.ExecutePreconditionCheck(() => value >= intIsAttribute.Lower && value < intIsAttribute.Upper,
                                                    () => $"{parameterInfo.Name} must be in Range [{intIsAttribute.Lower}..{intIsAttribute.Upper}[ but found {value}");
                    break;
                case IncludingBorders.Upper:
                    Checks.ExecutePreconditionCheck(() => value > intIsAttribute.Lower && value <= intIsAttribute.Upper,
                                                    () => $"{parameterInfo.Name} must be in Range ]{intIsAttribute.Lower}..{intIsAttribute.Upper}] but found {value}");
                    break;
                case IncludingBorders.Lower | IncludingBorders.Upper:
                    Checks.ExecutePreconditionCheck(() => value >= intIsAttribute.Lower && value <= intIsAttribute.Upper,
                                                    () => $"{parameterInfo.Name} must be in Range [{intIsAttribute.Lower}..{intIsAttribute.Upper}] but found {value}");
                    break;
                default: throw new PreconditionViolatedException("unknown range border definition");
            }
        }

        public static void CheckIntIs(ParameterInfo parameterInfo, object argument, Attribute attribute)
        {
            var intIsAttribute = (IntIsAttribute)attribute;
            var value = ConvertToInt(argument, parameterInfo.Name);
            switch (intIsAttribute.NumericComparison)
            {
                case NumericComparisons.HigherThan:
                    Checks.ExecutePreconditionCheck(() => value > intIsAttribute.Value, () => $"{parameterInfo.Name} must be higher than {intIsAttribute.Value} but found {value}");
                    break;
                case NumericComparisons.HigherOrEqualThan:
                    Checks.ExecutePreconditionCheck(() => value >= intIsAttribute.Value, () => $"{parameterInfo.Name} must be higher or equal than {intIsAttribute.Value} but found {value}");
                    break;
                case NumericComparisons.LowerThan:
                    Checks.ExecutePreconditionCheck(() => value < intIsAttribute.Value, () => $"{parameterInfo.Name} must be lower than {intIsAttribute.Value} but found {value}");
                    break;
                case NumericComparisons.LowerOrEqualThan:
                    Checks.ExecutePreconditionCheck(() => value <= intIsAttribute.Value, () => $"{parameterInfo.Name} must be lower or equal than {intIsAttribute.Value} but found {value}");
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