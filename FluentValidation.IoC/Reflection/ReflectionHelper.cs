using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FluentValidation.IoC
{
    public static class ReflectionHelper
    {
        private static bool IsSubclassOfGeneric(Type type, Type openGenericType)
        {
            return IsOrSubclassOfGeneric(type.BaseType, openGenericType);
        }

        private static bool IsOrSubclassOfGeneric(Type type, Type genericType)
        {
            var openGenericType = genericType.IsGenericType ? genericType.GetGenericTypeDefinition() : genericType;

            while (type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == openGenericType)
                    return true;

                type = type.BaseType;
            }

            return false;
        }

        public static IEnumerable<Type> AutoDiscoverValidatorTypes(IEnumerable<Assembly> assemblies)
        {
            return AutoDiscoverValidatorTypes(assemblies.SelectMany(a => a.GetTypes()));
        }

        public static IEnumerable<Type> AutoDiscoverValidatorTypes(IEnumerable<Type> types)
        {
            var validatorTypes = types
                    .Where(x =>
                        x.Assembly != typeof(AbstractValidator<>).Assembly
                        && !x.IsGenericType
                        && IsSubclassOfGeneric(x, typeof(AbstractValidator<>)))
                     .GroupBy(x => x.BaseType.GetGenericArguments().First(), x => new
                     {
                         Type = x,
                         IsDefault = x.CustomAttributes.Any(a => a.AttributeType == typeof(DefaultValidatorAttribute))
                     })
                     .Select(x => new
                     {
                         ElementType = x.Key,
                         DefaultValidators = x.Count() == 1 ? new[] { x.First() }.ToList() : x.Where(a => a.IsDefault).ToList(),
                         Validators = x.ToList()
                     })
                     .ToList();

            var multipleDefaultValidators = validatorTypes.Where(x => x.DefaultValidators.Count > 1).ToList();

            if (multipleDefaultValidators.Any())
                throw new InvalidOperationException("The DefaultValidatorAttribute can only be used once per IValidator<T> implementation. The following violations where found: "
                    + Environment.NewLine +
                    string.Join(Environment.NewLine,
                        multipleDefaultValidators
                            .Select(v => $"Validated type: {v.ElementType} , ValidatorTypes : {string.Join(",", v.Validators.Select(vv => vv.Type.Name))}")));

            var duplicateValidatorTypes = validatorTypes.Where(x => x.Validators.Count > 1 && !x.DefaultValidators.Any()).ToList();

            if (duplicateValidatorTypes.Any())
                throw new InvalidOperationException("Duplicate validators found for the same validater type. Please use the DefaultValidationAttribute to select the default validator. The following violations where found: "
                    + Environment.NewLine +
                    string.Join(Environment.NewLine,
                        duplicateValidatorTypes
                            .Select(v => $"Validated type: {v.ElementType} , ValidatorTypes : {string.Join(",", v.Validators.Select(vv => vv.Type.Name))}")));

            return validatorTypes.Select(x => x.Validators.OrderByDescending(v => v.IsDefault).Select(v => v.Type).First());
        }

    }
}
