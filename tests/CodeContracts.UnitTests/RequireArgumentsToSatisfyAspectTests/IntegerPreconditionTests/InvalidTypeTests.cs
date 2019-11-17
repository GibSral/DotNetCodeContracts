namespace CodeContracts.UnitTests.RequireArgumentsToSatisfyAspectTests.IntegerPreconditionTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class InvalidTypeTests
    {
        [Fact]
        public void IntIs_OnWrongParameterType_Throws()
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.SetValueWithIntIsOnString("SomeString")).Should().Throw<PreconditionViolatedException>().WithInnerException<InvalidCastException>();
        }
        
        [Fact]
        public void IntInRange_OnWrongParameterType_Throws()
        {
            var model = TestModel.MakeDefault();
            model.Invoking(it => it.SetValueWithIntIsOnString(valueWithRangeCheck: "SomeString")).Should().Throw<PreconditionViolatedException>().WithInnerException<InvalidCastException>();
        }
    }
}