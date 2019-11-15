namespace CodeContracts.Aspects
{
    using System;
    using System.Reflection;
    using AspectInjector.Broker;
    using JetBrains.Annotations;

    [Aspect(Scope.Global)]
    [Injection(typeof(CheckParametersAttribute))]
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
    public class CheckParametersAttribute : Attribute
    {
        [Advice(Kind.Before)]
        public void CheckPrecondition([Argument(Source.Arguments)] object[] arguments, [Argument(Source.Metadata)] MethodBase method)
        {
            var parameterInfos = method.GetParameters();
            for (var i = 0; i < arguments.Length; i++)
            {
                var parameterInfo = parameterInfos[i];
                var notNullAttribute = parameterInfo.GetCustomAttribute<NotNullAttribute>();
                if (notNullAttribute != null)
                {
                    var argument = arguments[0];
                    if (argument == null)
                    {
                        throw new PreconditionViolatedException($"Precondition violated: Argument {parameterInfo.Name} was null");
                    }
                }
            }
        }
    }
}