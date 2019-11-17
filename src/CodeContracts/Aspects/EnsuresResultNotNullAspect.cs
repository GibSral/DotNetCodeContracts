namespace CodeContracts.Aspects
{
    using System;
    using AspectInjector.Broker;

    [Aspect(Scope.Global)]
    public class EnsuresResultNotNullAspect
    {
        [Advice(Kind.After)]
        public void CheckResultForNull([Argument(Source.ReturnValue)] object returnValue, [Argument(Source.ReturnType)] Type returnType)
        {
            if (returnType.IsClass)
            {
                if (returnValue == null)
                {
                    throw new PostconditionViolatedException("return value was null");
                }
            }
        }
    }
}