namespace CodeContracts.Attributes
{
    using System;
    using System.Reflection;
    using static NumericOperations;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public class IntIsAttribute : Attribute
    {
        public IntIsAttribute(NumericComparisons numericComparison, int value)
        {
            NumericComparison = numericComparison;
            Value = value;
        }

        public NumericComparisons NumericComparison { get; }

        public int Value { get; }

        public static void CheckIntIs(ParameterInfo parameterInfo, object argument, Attribute attribute)
        {
            var intIsAttribute = (IntIsAttribute)attribute;
            var value = ConvertToInt(argument, parameterInfo);
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
    }
}