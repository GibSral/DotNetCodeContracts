namespace CodeContracts.UnitTests.CheckParameterValuesTests
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class IntIsHigherThanTests
    {
        [Theory]
        [InlineData(0)]
        public void MethodCall_WithValueIsLower_Throws(int value)
        {
            var model = Model.MakeDefault();
            model.Invoking(it => it.SetIntThatIsTooLow(value)).Should().Throw<PreconditionViolatedException>();
        }
    }
}