namespace CodeContracts.Attributes
{
    using System;
    using AspectInjector.Broker;
    using Aspects;

    [Flags]
    public enum ObjectTypes
    {
        Reference = 1,
        Struct = 2
    }

    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
    [Injection(typeof(RequireAllArgumentsAspect), Priority = 20000)]
    public class RequireAllArgumentsAttribute : Attribute
    {
        public RequireAllArgumentsAttribute(ObjectTypes objectType = ObjectTypes.Reference)
        {
            ObjectType = objectType;
        }

        public ObjectTypes ObjectType { get; }
    }
}