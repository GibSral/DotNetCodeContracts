namespace CodeContracts.UnitTests.RequireAllArgumentsAspectTests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class RequireAllMixedTypes
    {
        [Theory]
        [MemberData(nameof(AtLeastOneArgumentNullOrDefault))]
        public void ConstructorCallWithMixedTypes_WithAnyArgumentNullOrDefault_Throws(ReferenceArgument arg1, ValueArgument arg2)
        {
            this.Invoking(_ => new TestModel(arg1, arg2)).Should().Throw<PreconditionViolatedException>();
        }

        [Theory]
        [MemberData(nameof(AtLeastOneArgumentNullOrDefault))]
        public void MethodCallWithMixedTypes_WithAnyArgumentNullOrDefault_Throws(ReferenceArgument arg1, ValueArgument arg2)
        {
            var testModel = new TestModel();
            testModel.Invoking(it => it.NullOrDefaultCheckOnMethod(arg1, arg2)).Should().Throw<PreconditionViolatedException>();
        }

        public static IEnumerable<object[]> AtLeastOneArgumentNullOrDefault()
        {
            var allData = new List<object[]>
            {
                new object[] { null, ValueArgument.Filled() }, new object[] { new ReferenceArgument(), ValueArgument.Default() }, new object[] { null, ValueArgument.Default() },
            };

            return allData;
        }
    }
}