namespace CodeContracts.Attributes
{
    using System;
    using AspectInjector.Broker;
    using Aspects;

    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
    [Injection(typeof(RequireArgumentsToSatisfyAspect), Priority = 10000)]
    public class RequireArgumentsToSatisfyAttribute : Attribute
    {
    }
}