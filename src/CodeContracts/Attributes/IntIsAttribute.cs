namespace CodeContracts.Attributes
{
    using System;

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
    }
}