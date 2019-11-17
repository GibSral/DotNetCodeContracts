namespace CodeContracts.UnitTests.RequireAllArgumentsAspectTests
{
    using Attributes;

    public class TestModel
    {
        public TestModel()
        {
        }

        [RequireAllArguments(ObjectTypes.Reference)]
        public TestModel(ReferenceArgument arg1, ReferenceArgument arg2, ValueArgument arg3)
        {
        }

        [RequireAllArguments(ObjectTypes.Struct)]
        public TestModel(ValueArgument arg1, ValueArgument arg2, ReferenceArgument arg3)
        {
        }

        [RequireAllArguments(ObjectTypes.Reference | ObjectTypes.Struct)]
        public TestModel(ReferenceArgument referenceArgument, ValueArgument valueArgument)
        {
        }

        [RequireAllArguments(ObjectTypes.Reference)]
        public void NullCheckOnMethod(ReferenceArgument referenceArgument, ReferenceArgument referenceArgument1, ValueArgument arg3)
        {
        }

        [RequireAllArguments(ObjectTypes.Struct)]
        public void DefaultStructCheckOnMethod(ValueArgument valueArgument, ValueArgument valueArgument1, ReferenceArgument arg3)
        {
        }

        [RequireAllArguments(ObjectTypes.Reference | ObjectTypes.Struct)]
        public void NullOrDefaultCheckOnMethod(ReferenceArgument referenceArgument, ValueArgument referenceArgument1)
        {
        }
    }
}