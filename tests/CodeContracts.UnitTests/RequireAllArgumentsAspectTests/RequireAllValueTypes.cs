namespace CodeContracts.UnitTests.RequireAllArgumentsAspectTests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class RequireAllValueTypes
    {
        [Theory]
        [MemberData(nameof(AtLeastOneArgumentNull))]
        public void ConstructorCallWithOnlyValueTypes_WithAnyArgumentIsDefault_Throws(ValueArgument arg1, ValueArgument arg2, ReferenceArgument arg3)
        {
            this.Invoking(_ => new TestModel(arg1, arg2, arg3)).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [MemberData(nameof(AtLeastOneArgumentNull))]
        public void MethodCallWithOnlyValueTypes_WithAnyArgumentNull_Throws(ValueArgument arg1, ValueArgument arg2, ReferenceArgument arg3)
        {
            var testModel = new TestModel();
            testModel.Invoking(it => it.DefaultStructCheckOnMethod(arg1, arg2, arg3)).Should().Throw<PreconditionViolatedException>();
        }

        public static IEnumerable<object[]> AtLeastOneArgumentNull()
        {
            var allData = new List<object[]>
            {
                new object[] { ValueArgument.Default(), ValueArgument.Default(), new ReferenceArgument() },
                new object[] { ValueArgument.Filled(), ValueArgument.Default(), new ReferenceArgument(), },
                new object[] { ValueArgument.Default(), ValueArgument.Filled(), new ReferenceArgument(), },
            };

            return allData;
        }

        [Fact]
        public void ConstructorCallWithOnlyValueTypes_WithAllArgumentsHaveValue_DoesNotThrow()
        {
            this.Invoking(_ => new TestModel(ValueArgument.Filled(), ValueArgument.Filled(), null)).Should().NotThrow();
        }

        [Fact]
        public void MethodCallWithOnlyValueTypes_WithAllArgumentsHaveValue_DoesNotThrow()
        {
            var testModel = new TestModel();
            testModel.Invoking(it => it.DefaultStructCheckOnMethod(ValueArgument.Filled(), ValueArgument.Filled(), null)).Should().NotThrow();
        }
    }
}