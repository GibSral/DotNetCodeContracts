namespace CodeContracts.UnitTests
{
    using Aspects;
    using Attributes;
    using JetBrains.Annotations;
    using static Attributes.NumericComparisons;

    public class TestModel
    {
        [CheckParameters]
        public TestModel([StringNotEmpty] string param0, [NotNull] Parameter param1)
        {
            Param1 = param1;
        }

        public static TestModel MakeDefault() => new TestModel("SomeString", new Parameter("someValue"));

        public Parameter Param1 { get; }

        [CheckParameters]
        public string NotEmptyStringProperty
        {
            get;
            [StringNotEmpty]
            set;
        }

        [CheckParameters]
        public object NotNullProperty
        {
            get;
            [NotNull]
            set;
        }

        [CheckParameters]
        public void DoSomethingWithNull([NotNull] object parameter)
        {
        }

        [CheckParameters]
        public void DoSomethingWithEmptyString([StringNotEmpty] string emptyString)
        {
        }

        [CheckParameters]
        public void DoSomethingWithNotEmptyOnNonStringParameter([StringNotEmpty] int someInteger)
        {
        }

        [CheckParameters]
        public void SetInt([IntIs(HigherThan, 10)] int higherThan10 = 11,
                           [IntIs(HigherOrEqualThan, 100)] int higherOrEqualThan100 = 100,
                           [IntIs(LowerThan, 200)] int lowerThan200 = 199,
                           [IntIs(LowerOrEqualThan, 1000)] int lowerOrEqualThan1000 = 1000)
        {
        }

        [CheckParameters]
        public int IntHigherThanMinus100
        {
            get;
            [IntIs(HigherThan, -100)]
            set;
        }

        [CheckParameters]
        public int IntHigherOrEqualThanMinus10
        {
            get;
            [IntIs(HigherOrEqualThan, -10)]
            set;
        }

        [CheckParameters]
        public int IntLowerThanMinus10
        {
            get;
            [IntIs(LowerThan, -10)]
            set;
        }
        
        [CheckParameters]
        public int IntLowerOrEqualThanMinus10
        {
            get;
            [IntIs(LowerOrEqualThan, -10)]
            set;
        }
    }
}