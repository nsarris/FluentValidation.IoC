using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    public static partial class RuleBuilderExtensions
    {
        private static void AssertValidatorType<TProperty>(Type validatorType)
        {
            if (!typeof(IValidator<TProperty>).IsAssignableFrom(validatorType))
                throw new InvalidOperationException($"Type {validatorType.Name} does not implement IValidator<{typeof(TProperty).Name}>");
        }

        public static IRuleBuilder<T, TProperty> InjectValidator<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Type validatorType,
            params string[] ruleSets)
        {
            return ruleBuilder.InjectValidator((sp, _) =>
            {
                AssertValidatorType<TProperty>(validatorType);

                return (IValidator<TProperty>)sp.GetRequiredService(validatorType);
            }, ruleSets);
        }

        public static InjectionRuleBuilder<T, TProperty> WithDependencies<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return new InjectionRuleBuilder<T, TProperty>(ruleBuilder);
        }
    }
}
