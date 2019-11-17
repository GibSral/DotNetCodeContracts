namespace CodeContracts.UnitTests.RequireArgumentsToSatisfyAspectTests.IntegerPreconditionTests
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class IntIsInRangeTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(-1)]
        [InlineData(101)]
        public void MethodCall_WithValueIsNotInRangeExcludingBorders_Throws(int value)
        {
            var model = TestModel.MakeValidInstance();
            model.Invoking(it => it.SetInt(inRange0And100ExcludingBorder: value)).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(11)]
        [InlineData(12)]
        public void MethodCall_WithValueIsInRangeExcludingBorders_DoesNotThrow(int value)
        {
            var model = TestModel.MakeValidInstance();
            model.Invoking(it => it.SetInt(inRange0And100ExcludingBorder: value)).Should().NotThrow<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(-201)]
        [InlineData(-300)]
        [InlineData(200)]
        [InlineData(201)]
        public void MethodCall_WithValueIsNotInRangeIncludingLowerBorder_Throws(int value)
        {
            var model = TestModel.MakeValidInstance();
            model.Invoking(it => it.SetInt(inRangeMinus200And200IncludingLowerBorder: value)).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(-200)]
        [InlineData(0)]
        public void MethodCall_WithValueIsInRangeIncludingLowerBorder_DoesNotThrow(int value)
        {
            var model = TestModel.MakeValidInstance();
            model.Invoking(it => it.SetInt(inRangeMinus200And200IncludingLowerBorder: value)).Should().NotThrow<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-11)]
        [InlineData(11)]
        [InlineData(12)]
        public void MethodCall_WithValueIsNotInRangeIncludingUpperBorder_Throws(int value)
        {
            var model = TestModel.MakeValidInstance();
            model.Invoking(it => it.SetInt(inRangeMinus10And20IncludingUpperBorder: value)).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        public void MethodCall_WithValueIsInRangeIncludingUpperBorder_DoesNotThrow(int value)
        {
            var model = TestModel.MakeValidInstance();
            model.Invoking(it => it.SetInt(inRangeMinus10And20IncludingUpperBorder: value)).Should().NotThrow<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(-11)]
        [InlineData(-12)]
        [InlineData(11)]
        [InlineData(12)]
        public void MethodCall_WithValueIsNotInRangeIncludingBothBorders_Throws(int value)
        {
            var model = TestModel.MakeValidInstance();
            model.Invoking(it => it.SetInt(inRangeMinus10And20IncludingBothBorders: value)).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(1)]
        [InlineData(10)]
        public void MethodCall_WithValueIsInRangeIncludingBothBorders_DoesNotThrow(int value)
        {
            var model = TestModel.MakeValidInstance();
            model.Invoking(it => it.SetInt(inRangeMinus10And20IncludingBothBorders: value)).Should().NotThrow<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(-200)]
        [InlineData(-100)]
        public void PropertyCall_WithValueIsNotInRange_Throws(int value)
        {
            var model = TestModel.MakeValidInstance();
            model.Invoking(it => it.IntHigherThanMinus100 = value).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void PropertyCall_WithValueIsHigher_DoesNotThrow(int value)
        {
            var model = TestModel.MakeValidInstance();
            model.Invoking(it => it.IntHigherThanMinus100 = value).Should().NotThrow<PreconditionViolatedException>();
        }
    }
}