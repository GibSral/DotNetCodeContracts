namespace CodeContracts.UnitTests.CheckParameterValuesTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class NotNullTests
    {
        [Fact]
        public void ConstructorCall_WithNotNullMarkedParameterIsNull_Throws()
        {
            throw new InvalidCastException();
        } 
    }
}