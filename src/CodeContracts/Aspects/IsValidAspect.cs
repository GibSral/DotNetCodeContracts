namespace CodeContracts.Aspects
{
    using System;
    using System.Linq;
    using System.Reflection;
    using AspectInjector.Broker;
    using Attributes;
    using UnitTests;
    using static Checks;

    [Aspect(Scope.PerInstance)]
    public class IsValidAspect
    {
        [Advice(Kind.After)]
        public void IsValid([Argument(Source.Instance)] object instance, [Argument(Source.Triggers)] Attribute[] triggers)
        {
            ValidateByReflection(instance, triggers);
        }

        private static void ValidateByReflection(object instance, Attribute[] triggers)
        {
            var validateInstanceAttribute = (ValidateInstanceAttribute)triggers.First(it => it is ValidateInstanceAttribute);
            var validationMethod = instance.GetType()
                                           .GetMethod(validateInstanceAttribute.IsValidMethod,
                                                      BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            ExecutePostconditionCheck(() => validationMethod != null, () => $"ValidationMethod not found: {validateInstanceAttribute.IsValidMethod} - Type: {instance.GetType()}");
            ExecutePostconditionCheck(() => validationMethod.ReturnType == typeof(bool), () => $"ValidationMethod has wrong return type: {validateInstanceAttribute.IsValidMethod}");
            ExecutePostconditionCheck(() => InvokeValidation(instance, validationMethod), () => $"{instance.GetType().Name} is not valid");
        }

        private static bool InvokeValidation(object instance, MethodBase validationMethod) => (bool)validationMethod.Invoke(instance, Array.Empty<object>());
    }
}