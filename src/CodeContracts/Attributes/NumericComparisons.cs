namespace CodeContracts.Attributes
{
    using System;

    public enum NumericComparisons
    {
        HigherThan,
        HigherOrEqualThan,
        LowerThan,
        LowerOrEqualThan
    }

    [Flags]
    public enum IncludingBorders
    {
        None = 0,
        Lower = 1,
        Upper = 2
    }
}