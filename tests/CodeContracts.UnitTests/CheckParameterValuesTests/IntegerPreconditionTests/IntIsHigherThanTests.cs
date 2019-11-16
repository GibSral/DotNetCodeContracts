namespace CodeContracts.UnitTests.CheckParameterValuesTests.IntegerPreconditionTests
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
        [InlineData(1)]
        [InlineData(10)]
        public void MethodCall_WithValueIsLowerOrEqual_Throws(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.SetInt(value, 100)).Should().Throw<PreconditionViolatedException>();
        }
        
        [Theory]
        [InlineData(11)]
        [InlineData(12)]
        public void MethodCall_WithValueIsHigher_DoesNotThrow(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.SetInt(value, 100)).Should().NotThrow<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(-200)]
        [InlineData(-100)]
        public void PropertyCall_WithValueIsLowerOrEqual_Throws(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.IntHigherThanMinus100 = value).Should().Throw<PreconditionViolatedException>();
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void PropertyCall_WithValueIsHigher_DoesNotThrow(int value)
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.IntHigherThanMinus100 = value).Should().NotThrow<PreconditionViolatedException>();
        }
    }
}