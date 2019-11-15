namespace CodeContracts.UnitTests
{
    using Aspects;
    using JetBrains.Annotations;

    public class Model
    {
        [CheckParameters]
        public Model(string param0, [NotNull] Parameter param1)
        {
            Param1 = param1;
        }

        [NotNull]
        public Parameter Param1 { get; }

        [CheckParameters]
        public void DoSomethingWithNull([NotNull] object parameter)
        {
        }
    }
}