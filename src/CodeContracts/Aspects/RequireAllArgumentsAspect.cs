namespace CodeContracts.Aspects
{
    using System;
    using System.Linq;
    using System.Reflection;
    using AspectInjector.Broker;
    using Attributes;
    using static Checks;

    [Aspect(Scope.Global)]
    public class RequireAllArgumentsAspect
    {
        [Advice(Kind.Before)]
        public void AssertThatAllArgumentsHaveValues([Argument(Source.Arguments)] object[] arguments, [Argument(Source.Metadata)] MethodBase method, [Argument(Source.Triggers)] Attribute[] triggers)
        {
            var requireAllArgumentsAttribute = (RequireAllArgumentsAttribute)triggers[0];
            var parameterInfos = method.GetParameters();
            var (check, errorMessage) = Assertion(requireAllArgumentsAttribute);

            ForeachParameter(arguments, parameterInfos, (argument, parameterInfo) => ExecutePreconditionCheck(() => check(argument, parameterInfo), () => errorMessage(parameterInfo)));
        }

        private static (Func<object, ParameterInfo, bool> check, Func<ParameterInfo, string> errorMessage) Assertion(RequireAllArgumentsAttribute requireAllArgumentsAttribute)
        {
            Func<object, ParameterInfo, bool> check;
            Func<ParameterInfo, string> errorMessage;
            switch (requireAllArgumentsAttribute.ObjectType)
            {
                case ObjectTypes.Reference:
                    check = CheckReferenceTypeNotNull;
                    errorMessage = ClassErrorMessage;
                    break;
                case ObjectTypes.Struct:
                    check = CheckStructNotDefault;
                    errorMessage = StructErrorMessage;
                    break;

                case ObjectTypes.Reference | ObjectTypes.Struct:
                    check = (argument, parameterInfo) => CheckReferenceTypeNotNull(argument, parameterInfo) && CheckStructNotDefault(argument, parameterInfo);
                    errorMessage = parameterInfo => IsStruct(parameterInfo.ParameterType) ? StructErrorMessage(parameterInfo) : ClassErrorMessage(parameterInfo);
                    break;
                
                default: 
                    check = (argument, parameterInfo) => false;
                    errorMessage = info => "unknown error";
                    break;
            }

            return (check, errorMessage);
        }

        private static bool CheckStructNotDefault(object argument, ParameterInfo parameterInfo)
        {
            var parameterType = parameterInfo.ParameterType;
            if (IsStruct(parameterType))
            {
                var defaultInstance = Activator.CreateInstance(parameterType);
                var isDefault = argument.Equals(defaultInstance);
                return !isDefault;
            }

            return true;
        }

        private static bool CheckReferenceTypeNotNull(object argument, ParameterInfo parameterInfo)
        {
            if (parameterInfo.ParameterType.IsClass)
            {
                return argument != null;    
            }

            return true;
        }

        private static string ClassErrorMessage(ParameterInfo parameterInfo) => $"ReferenceType parameter was null. Name: {parameterInfo.Name}";

        private static string StructErrorMessage(ParameterInfo parameterInfo) => $"ValueType parameter was default. Name: {parameterInfo.Name}";

        private static bool IsStruct(Type parameterType) => parameterType.IsValueType && !parameterType.IsPrimitive && !parameterType.IsEnum;
    }
}