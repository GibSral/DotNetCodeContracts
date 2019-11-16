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
        private const string PreconditionViolated = "Precondition violated:";

        private static readonly Dictionary<Type, Action<ParameterInfo, object, Attribute>> ParameterCheckers = new Dictionary<Type, Action<ParameterInfo, object, Attribute>>
        {
            { typeof(NotNullAttribute), WithoutAttribute(CheckNotNull) }, { typeof(StringNotEmptyAttribute), WithoutAttribute(CheckStringNotEmpty) }, { typeof(IntIsAttribute), CheckIntIs }
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

        private static void CheckIntIs(ParameterInfo parameterInfo, object argument, Attribute attribute)
        {
            var intIsAttribute = (IntIsAttribute)attribute;
            var value = ConvertToInt(argument, parameterInfo.Name);
            switch (intIsAttribute.NumericComparison)
            {
                case NumericComparisons.HigherThan:
                    ExecutePreconditionCheck(() => value > intIsAttribute.Number, () => $"Int must be higher than {intIsAttribute.Number} but found {value}");
                    break;
                case NumericComparisons.HigherOrEqualThan: break;
                case NumericComparisons.LowerThan: break;
                case NumericComparisons.LowerOrEqualThan: break;
                default: throw new PreconditionViolatedException("unknown numeric comparison");
            }
        }

        private static int ConvertToInt(object argument, string parameterName)
        {
            var intValue = (int)argument;
            return intValue;
        }

        private static void ExecutePreconditionCheck(Func<bool> check, Func<string> errorMessage)
        {
            var result = check();
            if (!result)
            {
                var message = $"{PreconditionViolated} {errorMessage()}";
                throw new PreconditionViolatedException(message);
            }
        }

        private static Action<ParameterInfo, object, Attribute> WithoutAttribute(Action<ParameterInfo, object> check) => (parameterInfo, value, _) => check(parameterInfo, value);
    }
}