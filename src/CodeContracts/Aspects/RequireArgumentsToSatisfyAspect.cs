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

    [Aspect(Scope.Global)]
    public class RequireArgumentsToSatisfyAspect
    {
        private static readonly Dictionary<Type, Action<ParameterInfo, object, Attribute>> ParameterCheckers = new Dictionary<Type, Action<ParameterInfo, object, Attribute>>
        {
            { typeof(NotNullAttribute), WithoutAttribute(CheckNotNull) },
            { typeof(NotEmptyAttribute), WithoutAttribute(NotEmptyAttribute.CheckNotEmpty) },
            { typeof(IntIsAttribute), IntIsAttribute.CheckIntIs },
            { typeof(IntIsInRangeAttribute), IntIsInRangeAttribute.CheckIntIsInRange }
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
                ForeachParameter(arguments,
                                 parameterInfos,
                                 (argument, parameterInfo) =>
                                 {
                                     var customAttributes = parameterInfo.GetCustomAttributes();
                                     ExecuteChecks(customAttributes, parameterInfo, argument);
                                 });
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
    }
}