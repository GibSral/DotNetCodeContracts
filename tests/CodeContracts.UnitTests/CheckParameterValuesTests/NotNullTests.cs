namespace CodeContracts.UnitTests.CheckParameterValuesTests
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class NotNullTests
    {
        [Fact]
        public void ConstructorCall_WithNotNullMarkedParameterIsNotNull_DoesNotThrow()
        {
            this.Invoking(_ => new Model("SomeValue", new Parameter("SomeValue"))).Should().NotThrow<PreconditionViolatedException>();
        }

        [Fact]
        public void ConstructorCall_WithNotNullMarkedParameterIsNull_Throws()
        {
            this.Invoking(_ => new Model("SomeValue", null)).Should().Throw<PreconditionViolatedException>();
        }

        [Fact]
        public void MethodCall_WithNotNullMarkedParameterIsNull_Throws()
        {
            var model = new Model("SomeValue", new Parameter("SomeValue"));
            model.Invoking(it => it.DoSomethingWithNull(null)).Should().Throw<PreconditionViolatedException>();
        }
    }
}