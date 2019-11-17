namespace CodeContracts.UnitTests
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class EnsureResultIsNotNullTests
    {
        [Fact]
        public void MethodCall_WithResultIsNull_Throws()
        {
            var testModel = TestModel.MakeValidInstance();
            testModel.Invoking(it => it.CheckForResultNull(true)).Should().Throw<PostconditionViolatedException>();
        }
        
        [Fact]
        public void MethodCall_WithResultStringIsNull_Throws()
        {
            var testModel = TestModel.MakeValidInstance();
            testModel.Invoking(it => it.CheckForResultStringNull()).Should().Throw<PostconditionViolatedException>();
        }
        
        [Fact]
        public void MethodCall_WithResultIsNotNull_DoesNotThrow()
        {
            var testModel = TestModel.MakeValidInstance();
            testModel.Invoking(it => it.CheckForResultNull(false)).Should().NotThrow();
        }
        
        [Fact]
        public void MethodCall_WithResultStruct_DoesNotThrow()
        {
            var testModel = TestModel.MakeValidInstance();
            testModel.Invoking(it => it.CheckForResultNullButIsValueType()).Should().NotThrow();
        }
    }
}