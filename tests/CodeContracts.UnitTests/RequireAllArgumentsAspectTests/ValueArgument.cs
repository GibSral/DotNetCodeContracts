namespace CodeContracts.UnitTests.RequireAllArgumentsAspectTests
{
    public struct ValueArgument
    {
        public ValueArgument(string value1, int value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public string Value1 { get; }

        public int Value2 { get; }

        public static ValueArgument Filled() => new ValueArgument("SomeString", 1);
        
        public static ValueArgument Default() => new ValueArgument();
    }
}