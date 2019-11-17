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
        public void ConstructorCallWithOnlyReferenceTypes_WithAnyArgumentNull_Throws(ReferenceArgument arg1, ReferenceArgument arg2, ReferenceArgument arg3)
        {
            this.Invoking(_ => new TestModel(arg1, arg2, arg3)).Should().Throw<PreconditionViolatedException>();
        }
        
        [Theory]
        [MemberData(nameof(AtLeastOneArgumentNull))]
        public void MethodCallWithOnlyReferenceTypes_WithAnyArgumentNull_Throws(ReferenceArgument arg1, ReferenceArgument arg2, ReferenceArgument arg3)
        {
            var testModel = new TestModel();
            testModel.Invoking(it => it.NullInMethodSignature(arg1, arg2, arg3)).Should().Throw<PreconditionViolatedException>();
        }
        
        public static IEnumerable<object[]> AtLeastOneArgumentNull()
        {
            var allData = new List<object[]>
            {
                new object[] { null, null, null },
                new object[] { new ReferenceArgument(), null, null },
                new object[] { new ReferenceArgument(), new ReferenceArgument(), null },
                new object[] { new ReferenceArgument(), null, new ReferenceArgument() },
                new object[] { null, null, new ReferenceArgument() },
                new object[] { null, new ReferenceArgument(), null },
            };

            return allData;
        }

        [Fact]
        public void ConstructorCllWithOnlyReferenceTypes_WithNoArgumentNull_DoesNotThrow()
        {
            this.Invoking(_ => new TestModel(new ReferenceArgument(), new ReferenceArgument(), new ReferenceArgument())).Should().NotThrow();
        }
    }
}