namespace CodeContracts.Attributes
{
    using System;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public class IntIsInRangeAttribute : Attribute
    {
        public IntIsInRangeAttribute(int lower, int upper, IncludingBorders includingBorders = IncludingBorders.None)
        {
            Lower = lower;
            Upper = upper;
            IncludingBorders = includingBorders;
        }

        public int Lower { get; }

        public int Upper { get; }

        public IncludingBorders IncludingBorders { get; }

        public static void CheckIntIsInRange(ParameterInfo parameterInfo, object argument, Attribute attribute)
        {
            var intIsAttribute = (IntIsInRangeAttribute)attribute;
            var value = NumericOperations.ConvertToInt(argument, parameterInfo);
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
    }
}