namespace CodeContracts.UnitTests
{
    using Attributes;

    public class ValidatableModel
    {
        private const string ExpectedValue = "ExpectedValue";
        private readonly int value1;
        private readonly string value2;

        [ValidateInstance(nameof(IsInstanceValid))]
        public ValidatableModel(int value1, string value2)
        {
            this.value1 = value1;
            this.value2 = value2;
        }

        [ValidateInstance("NonExistingValidationMethod")]
        public ValidatableModel(int value1)
        {
            this.value1 = value1;
            value2 = ExpectedValue;
        }

        public static ValidatableModel Valid() => new ValidatableModel(200, ExpectedValue);

        public static ValidatableModel InValid() => new ValidatableModel(200, "SomeOtherValue");

        public static ValidatableModel WithNonExistingValidationMethod() => new ValidatableModel(101);

        private bool IsInstanceValid() => value1 > 100 && value2 == ExpectedValue;

        [ValidateInstance(nameof(ValidateWithNoReturnValue))]
        public void TriggerValidationWithValidationMethodIsNoPredicate()
        {
        }

        private void ValidateWithNoReturnValue()
        {
        }
    }
}