namespace CodeContracts.UnitTests.CheckParameterValuesTests
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class StringNotEmptyTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ConstructorCall_WithEmptyString_Throws(string emptyString)
        {
            this.Invoking(_ => new TestModel(emptyString, new Parameter("SomeValue"))).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void MethodCall_WithEmptyString_Throws(string emptyString)
        {
            var model = new TestModel("SomeString", new Parameter("SomeValue"));
            model.Invoking(it => it.DoSomethingWithEmptyString(emptyString)).Should().Throw<PreconditionViolatedException>();
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void SetProperty_WithEmptyString_Throws(string emptyString)
        {
            var model = new TestModel("SomeString", new Parameter("SomeValue"));
            model.Invoking(it => it.NotEmptyStringProperty = emptyString).Should().Throw<PreconditionViolatedException>();
        }

        [Fact]
        public void MethodCall_WithNotEmptyOnOtherTypeParameterThanString_Throws()
        {
            var model = new TestModel("SomeString", new Parameter("SomeValue"));
            model.Invoking(it => it.DoSomethingWithNotEmptyOnNonStringParameter(1)).Should().Throw<PreconditionViolatedException>();
        }
    }
}