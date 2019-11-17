namespace CodeContracts.UnitTests.RequireAllArgumentsAspectTests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class RequireAllReferenceTypes
    {
        [Theory]
        [MemberData(nameof(AtLeastOneArgumentNull))]
        public void ConstructorCallWithOnlyReferenceTypes_WithAnyArgumentNull_Throws(ReferenceArgument arg1, ReferenceArgument arg2, ValueArgument arg3)
        {
            this.Invoking(_ => new TestModel(arg1, arg2, arg3)).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [MemberData(nameof(AtLeastOneArgumentNull))]
        public void MethodCallWithOnlyReferenceTypes_WithAnyArgumentNull_Throws(ReferenceArgument arg1, ReferenceArgument arg2, ValueArgument arg3)
        {
            var testModel = new TestModel();
            testModel.Invoking(it => it.NullCheckOnMethod(arg1, arg2, arg3)).Should().Throw<PreconditionViolatedException>();
        }

        public static IEnumerable<object[]> AtLeastOneArgumentNull()
        {
            var allData = new List<object[]>
            {
                new object[] { null, null, ValueArgument.Filled() },
                new object[] { new ReferenceArgument(), null, ValueArgument.Filled() },
                new object[] { null, new ReferenceArgument(), ValueArgument.Filled() },
            };

            return allData;
        }

        [Fact]
        public void ConstructorCllWithOnlyReferenceTypes_WithNoArgumentNull_DoesNotThrow()
        {
            this.Invoking(_ => new TestModel(new ReferenceArgument(), new ReferenceArgument(), ValueArgument.Default())).Should().NotThrow();
        }

        [Fact]
        public void MethodCallWithOnlyReferenceTypes_WithNoArgumentNull_DoesNotThrow()
        {
            var testModel = new TestModel();
            testModel.Invoking(it => it.NullCheckOnMethod(new ReferenceArgument(), new ReferenceArgument(), ValueArgument.Default())).Should().NotThrow();
        }
    }
}