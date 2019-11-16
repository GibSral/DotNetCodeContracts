namespace CodeContracts.UnitTests.CheckParameterValuesTests.IntegerPreconditionTests
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class IntIsLowerOrEqualThanTests
    {
        [Theory]
        [InlineData(1001)]
        [InlineData(1002)]
        public void MethodCall_WithValueIsHigher_Throws(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.SetInt(lowerOrEqualThan1000: value)).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(999)]
        [InlineData(0)]
        public void MethodCall_WithValueIsLowerOrEqual_DoesNotThrow(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.SetInt(lowerOrEqualThan1000: value)).Should().NotThrow<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(-9)]
        [InlineData(-8)]
        public void PropertyCall_WithValueIsHigher_Throws(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.IntLowerOrEqualThanMinus10 = value).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-100)]
        public void PropertyCall_WithValueIsLowerOrEqual_DoesNotThrow(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.IntLowerOrEqualThanMinus10 = value).Should().NotThrow<PreconditionViolatedException>();
        }
    }
}