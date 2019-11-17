namespace CodeContracts.UnitTests.RequireArgumentsToSatisfyAspectTests.IntegerPreconditionTests
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class IntIsLowerThanTests
    {
        [Theory]
        [InlineData(200)]
        [InlineData(201)]
        [InlineData(300)]
        public void MethodCall_WithValueIsHigherOrEqual_Throws(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.SetInt(lowerThan200: value)).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(199)]
        [InlineData(0)]
        public void MethodCall_WithValueIsHigher_DoesNotThrow(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.SetInt(lowerThan200: value)).Should().NotThrow<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-9)]
        public void PropertyCall_WithValueIsHigherOrEqual_Throws(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.IntLowerThanMinus10 = value).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(-11)]
        [InlineData(-100)]
        public void PropertyCall_WithValueIsLower_DoesNotThrow(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.IntLowerThanMinus10 = value).Should().NotThrow<PreconditionViolatedException>();
        }
    }
}