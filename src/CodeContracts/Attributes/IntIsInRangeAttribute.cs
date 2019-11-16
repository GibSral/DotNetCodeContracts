namespace CodeContracts.Attributes
{
    using System;

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
    }
}