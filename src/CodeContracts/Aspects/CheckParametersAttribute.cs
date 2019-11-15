namespace CodeContracts.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AspectInjector.Broker;
    using Attributes;
    using JetBrains.Annotations;

    [Aspect(Scope.PerInstance)]
    [Injection(typeof(CheckParametersAttribute))]
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
    public class CheckParametersAttribute : Attribute
    {
        private static readonly Dictionary<Type, Action<ParameterInfo, object>> ParameterCheckers =
            new Dictionary<Type, Action<ParameterInfo, object>> { { typeof(NotNullAttribute), CheckNotNull }, { typeof(NotEmptyAttribute), CheckStringNotEmpty }, };

        [Advice(Kind.Before)]
        public void CheckPrecondition([Argument(Source.Arguments)] object[] arguments, [Argument(Source.Metadata)] MethodBase method)
        {
            var parameterInfos = method.GetParameters();
            for (var i = 0; i < arguments.Length; i++)
            {
                var parameterInfo = parameterInfos[i];
                var argument = arguments[i];
                var customAttributes = parameterInfo.GetCustomAttributes();
                foreach (var customAttribute in customAttributes)
                {
                    if (ParameterCheckers.TryGetValue(customAttribute.GetType(), out var check))
                    {
                        check(parameterInfo, argument);
                    }
                }
            }
        }

        private static void CheckNotNull(ParameterInfo parameterInfo, object argument)
        {
            if (argument == null)
            {
                throw new PreconditionViolatedException($"Precondition violated: Argument {parameterInfo.Name} was null");
            }
        }

        private static void CheckStringNotEmpty(ParameterInfo parameterInfo, object argument)
        {
            switch (argument)
            {
                case null: throw new PreconditionViolatedException($"Precondition violated: Argument {parameterInfo.Name} was null");
                case string str:
                    if (string.IsNullOrWhiteSpace(str))
                    {
                        throw new PreconditionViolatedException($"Precondition violated: Argument {parameterInfo.Name} was empty");
                    }

                    break;
                
                default: throw new PreconditionViolatedException($"Precondition violated: Argument {parameterInfo.Name} is not a string");
            }
        }
    }
}