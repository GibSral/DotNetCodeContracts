namespace CodeContracts.Aspects
{
    using System;
    using AspectInjector.Broker;

    [Aspect(Scope.Global)]
    public class RequireAllArgumentsAspect
    {
        [Advice(Kind.Before)]
        public void AssertThatAllArgumentsHaveValues([Argument(Source.Arguments)] object[] arguments)
        {
        }
    }
}