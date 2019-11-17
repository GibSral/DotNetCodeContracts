namespace CodeContracts.Aspects
{
    using System;
    using System.Linq;
    using AspectInjector.Broker;
    using static Checks;

    [Aspect(Scope.Global)]
    public class RequireAllArgumentsAspect
    {
        [Advice(Kind.Before)]
        public void AssertThatAllArgumentsHaveValues([Argument(Source.Arguments)] object[] arguments)
        {
            ExecutePreconditionCheck(() => !arguments.Any(it => it is null), () => "at least one parameter was null");
        }
    }
}