namespace CodeContracts.UnitTests
{
    using Aspects;
    using Attributes;
    using JetBrains.Annotations;
    using static Attributes.NumericComparisons;

    public class Model
    {
        [CheckParameters]
        public Model([StringNotEmpty] string param0, [NotNull]Parameter param1)
        {
            Param1 = param1;
        }

        public static Model MakeDefault() => new Model("SomeString", new Parameter("someValue"));

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
        public void SetIntThatIsTooLow([IntIs(HigherThan,  10)] int value)
        {
        }
    }
}