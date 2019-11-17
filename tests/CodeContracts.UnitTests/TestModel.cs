namespace CodeContracts.UnitTests
{
    using Aspects;
    using Attributes;
    using JetBrains.Annotations;
    using static Attributes.NumericComparisons;

    public class TestModel
    {
        [RequireArgumentsToSatisfy]
        public TestModel([NotEmpty] string param0, [NotNull] Parameter param1)
        {
            Param1 = param1;
        }

        public static TestModel MakeDefault() => new TestModel("SomeString", new Parameter("someValue"));

        public Parameter Param1 { get; }

        [RequireArgumentsToSatisfy]
        public string NotEmptyStringProperty
        {
            get;
            [NotEmpty]
            set;
        }

        [RequireArgumentsToSatisfy]
        public object NotNullProperty
        {
            get;
            [NotNull]
            set;
        }

        [RequireArgumentsToSatisfy]
        public void DoSomethingWithNull([NotNull] object parameter)
        {
        }

        [RequireArgumentsToSatisfy]
        public void DoSomethingWithEmptyString([NotEmpty] string emptyString)
        {
        }

        [RequireArgumentsToSatisfy]
        public void DoSomethingWithNotEmptyOnNonStringParameter([NotEmpty] int someInteger)
        {
        }

        [RequireArgumentsToSatisfy]
        public void SetInt([IntIs(HigherThan, 10)] int higherThan10 = 11,
                           [IntIs(HigherOrEqualThan, 100)] int higherOrEqualThan100 = 100,
                           [IntIs(LowerThan, 200)] int lowerThan200 = 199,
                           [IntIs(LowerOrEqualThan, 1000)] int lowerOrEqualThan1000 = 1000,
                           [IntIsInRange(0, 100)]
                           int inRange0And100ExcludingBorder = 50,
                           [IntIsInRange(-200, 200, IncludingBorders.Lower)]
                           int inRangeMinus200And200IncludingLowerBorder = 50,
                           [IntIsInRange(-10, 10, IncludingBorders.Upper)]
                           int inRangeMinus10And20IncludingUpperBorder = 0,
                           [IntIsInRange(-10, 10, IncludingBorders.Upper | IncludingBorders.Lower)]
                           int inRangeMinus10And20IncludingBothBorders = 0)
        {
        }

        [RequireArgumentsToSatisfy]
        public void SetValueWithIntIsOnString([IntIs(HigherThan, 10)] string valueWithIntIsCheck = "SomeString", [IntIsInRange(0, 100)] string valueWithRangeCheck = "SomeString")
        {
        }

        [RequireArgumentsToSatisfy]
        public int IntHigherThanMinus100
        {
            get;
            [IntIs(HigherThan, -100)]
            set;
        }

        [RequireArgumentsToSatisfy]
        public int IntHigherOrEqualThanMinus10
        {
            get;
            [IntIs(HigherOrEqualThan, -10)]
            set;
        }

        [RequireArgumentsToSatisfy]
        public int IntLowerThanMinus10
        {
            get;
            [IntIs(LowerThan, -10)]
            set;
        }

        [RequireArgumentsToSatisfy]
        public int IntLowerOrEqualThanMinus10
        {
            get;
            [IntIs(LowerOrEqualThan, -10)]
            set;
        }
        
        [RequireArgumentsToSatisfy]
        public int IntLowerWithRangeCheck
        {
            get;
            [IntIsInRange(-100, 200)]
            set;
        }
    }
}