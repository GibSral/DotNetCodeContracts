namespace CodeContracts.Attributes
{
    using System;
    using AspectInjector.Broker;
    using Aspects;

    [Flags]
    public enum ObjectTypes
    {
        Reference = 0,
        Value = 1
    }
    
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
    [Injection(typeof(RequireAllArgumentsAspect))]
    public class RequireAllArgumentsAttribute : Attribute
    {
        private readonly ObjectTypes objectType;

        public RequireAllArgumentsAttribute(ObjectTypes objectType = ObjectTypes.Reference)
        {
            this.objectType = objectType;
        }   
    }
}