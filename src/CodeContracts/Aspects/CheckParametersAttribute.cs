namespace CodeContracts.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AspectInjector.Broker;
    using Attributes;
    using JetBrains.Annotations;
    using static Checks;
    using static NumericOperations;

    [Aspect(Scope.PerInstance)]
    [Injection(typeof(CheckParametersAttribute), Priority = 10000)]
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
    public class CheckParametersAttribute : Attribute
    {
        private static readonly Dictionary<Type, Action<ParameterInfo, object, Attribute>> ParameterCheckers = new Dictionary<Type, Action<ParameterInfo, object, Attribute>>
        {
            { typeof(NotNullAttribute), WithoutAttribute(CheckNotNull) },
            { typeof(StringNotEmptyAttribute), WithoutAttribute(CheckStringNotEmpty) },
            { typeof(IntIsAttribute), CheckIntIs },
            { typeof(IntIsInRangeAttribute), CheckIntIsInRange }
        };

        [Advice(Kind.Before)]
        public void CheckPrecondition([Argument(Source.Arguments)] object[] arguments, [Argument(Source.Metadata)] MethodBase method)
        {
            if (IsProperty(method))
            {
                var parameterInfo = method.GetParameters().First();
                var attributes = method.GetCustomAttributes();
                ExecuteChecks(attributes, parameterInfo, arguments.First());
            }
            else
            {
                var parameterInfos = method.GetParameters();
                for (var i = 0; i < arguments.Length; i++)
                {
                    var parameterInfo = parameterInfos[i];
                    var argument = arguments[i];
                    var customAttributes = parameterInfo.GetCustomAttributes();
                    ExecuteChecks(customAttributes, parameterInfo, argument);
                }
            }
        }

        private static void ExecuteChecks(IEnumerable<Attribute> customAttributes, ParameterInfo parameterInfo, object argument)
        {
            foreach (var customAttribute in customAttributes)
            {
                if (ParameterCheckers.TryGetValue(customAttribute.GetType(), out var check))
                {
                    check(parameterInfo, argument, customAttribute);
                }
            }
        }

        private static bool IsProperty(MethodBase method) => method.IsSpecialName && !method.IsConstructor;

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