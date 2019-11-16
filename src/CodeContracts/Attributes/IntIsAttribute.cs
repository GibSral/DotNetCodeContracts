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

        public IntIsAttribute(NumericComparisons numericComparison, int value, int value2)
        {
            NumericComparison = numericComparison;
            Value = value;
            Value2 = value2;
        }

        public NumericComparisons NumericComparison { get; }

        public int Value { get; }

        public int Value2 { get; }
    }
}