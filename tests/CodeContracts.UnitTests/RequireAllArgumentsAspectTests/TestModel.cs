namespace CodeContracts.UnitTests.RequireAllArgumentsAspectTests
{
    using Attributes;

    public class TestModel
    {
        public TestModel()
        {
        }
        
        [RequireAllArguments(ObjectTypes.Reference)]
        public TestModel(ReferenceArgument arg1, ReferenceArgument arg2, ReferenceArgument arg3)
        {
        }

        [RequireAllArguments(ObjectTypes.Reference)]
        public void NullInMethodSignature(ReferenceArgument referenceArgument, ReferenceArgument referenceArgument1, ReferenceArgument arg3)
        {
        }
    }
}