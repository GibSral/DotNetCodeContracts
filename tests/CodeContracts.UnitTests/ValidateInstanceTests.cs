namespace CodeContracts.UnitTests
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class ValidateInstanceTests
    {
        [Fact]
        public void ConstructorCall_WithInstanceIsValid_DoesNotThrow()
        {
            this.Invoking(_ => ValidatableModel.Valid()).Should().NotThrow();
        }

        [Fact]
        public void ConstructorCall_WithInstanceIsNotValid_Throws()
        {
            this.Invoking(_ => ValidatableModel.InValid()).Should().Throw<PostconditionViolatedException>();
        }

        [Fact]
        public void ConstructorCall_WithNonExistingValidationMethod_Throws()
        {
            this.Invoking(_ => ValidatableModel.WithNonExistingValidationMethod()).Should().Throw<PostconditionViolatedException>();
        }

        [Fact]
        public void ValidateObject_WithReturnValueIsNotBool_Throws()
        {
            var validatableModel = ValidatableModel.Valid();
            validatableModel.Invoking(it => it.TriggerValidationWithValidationMethodIsNoPredicate()).Should().Throw<PostconditionViolatedException>();
        }
    }
}