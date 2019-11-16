namespace CodeContracts.Attributes
{
    using System;
    using AspectInjector.Broker;
    using Aspects;

    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
    [Injection(typeof(IsValidAspect))]
    public class ValidateInstanceAttribute : Attribute
    {
        public ValidateInstanceAttribute(string isValidMethod)
        {
            IsValidMethod = isValidMethod;
        }

        public string IsValidMethod { get; }
    }
}