namespace CodeContracts.UnitTests
{
    using Aspects;
    using Attributes;
    using JetBrains.Annotations;

    public class Model
    {
        [CheckParameters]
        public Model([NotEmpty] string param0, [NotNull] Parameter param1)
        {
            Param1 = param1;
        }

        [NotNull]
        public Parameter Param1 { get; }

        [CheckParameters]
        public string NotEmptyStringProperty
        {
            get;
            
            [NotEmpty]
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
        public void DoSomethingWithEmptyString([NotEmpty] string emptyString)
        {
        }

        [CheckParameters]
        public void DoSomethingWithNotEmptyOnNonStringParameter([NotEmpty] int someInteger)
        {
        }
    }
}