namespace CodeContracts.UnitTests.CheckParameterValuesTests.IntegerPreconditionTests
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class IntIsHigherOrEqualThanTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(99)]
        public void MethodCall_WithValueIsLower_Throws(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.SetInt(100, value)).Should().Throw<PreconditionViolatedException>();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(101)]
        public void MethodCall_WithValueIsHigherOrEqual_DoesNotThrow(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.SetInt(100, value)).Should().NotThrow<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(-11)]
        [InlineData(-12)]
        public void PropertyCall_WithValueIsLower_Throws(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.IntHigherOrEqualThanMinus10 = value).Should().Throw<PreconditionViolatedException>();
        }
        
        [Theory]
        [InlineData(-10)]
        [InlineData(1)]
        [InlineData(2)]
        public void PropertyCall_WithValueIsHigherOrEqual_DoesNotThrow(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.IntHigherOrEqualThanMinus10 = value).Should().NotThrow<PreconditionViolatedException>();
        }
    }
}