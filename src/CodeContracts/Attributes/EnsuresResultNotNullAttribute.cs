namespace CodeContracts.Attributes
{
    using System;
    using AspectInjector.Broker;
    using Aspects;

    [AttributeUsage(AttributeTargets.Method)]
    [Injection(typeof(EnsuresResultNotNullAspect), Priority = 10000)]
    public class EnsuresResultNotNullAttribute : Attribute
    {
    }
}