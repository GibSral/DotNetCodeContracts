namespace CodeContracts.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public class NotEmptyAttribute : Attribute
    {
    }
}