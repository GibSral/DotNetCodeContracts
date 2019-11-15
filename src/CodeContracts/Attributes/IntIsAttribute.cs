﻿namespace CodeContracts.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter)]
    public class IntIsAttribute : Attribute
    {
        public IntIsAttribute(NumericComparisons numericComparison, int number)
        {
            NumericComparison = numericComparison;
            Number = number;
        }

        public NumericComparisons NumericComparison { get; }
        
        public int Number { get; }
    }
}