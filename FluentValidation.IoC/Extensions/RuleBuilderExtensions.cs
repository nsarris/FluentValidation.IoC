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
        //#region Custom with parent

        //public static IRuleBuilderInitial<T, TProperty> Custom<T, TProperty>(
        //    this IRuleBuilder<T, TProperty> ruleBuilder,
        //    Action<T, TProperty, CustomContext> validate)
        //{
        //    return ruleBuilder
        //        .Custom((x, context) =>
        //        {
        //            var parent = (T)context.ParentContext.InstanceToValidate;
        //            validate(parent, x, context);
        //        });
        //}

        //public static IRuleBuilderInitial<T, TProperty> CustomAsync<T, TProperty>(
        //    this IRuleBuilder<T, TProperty> ruleBuilder,
        //    Func<T, TProperty, CustomContext, CancellationToken, Task> validate)
        //{
        //    return ruleBuilder
        //        .CustomAsync((x, context, cancellationToken) =>
        //        {
        //            var parent = (T)context.ParentContext.InstanceToValidate;
        //            return validate(parent, x, context, cancellationToken);
        //        });
        //}

        //#endregion

        //#region MustAsync and CustomAsync without cancellation token

        //public static IRuleBuilderOptions<T, TProperty> MustAsync<T, TProperty>(
        //    this IRuleBuilder<T, TProperty> ruleBuilder,
        //    Func<T, TProperty, Task<bool>> validate)
        //{
        //    return ruleBuilder
        //        .MustAsync((parent, x, _) =>
        //        {
        //            return validate(parent, x);
        //        });
        //}

        //public static IRuleBuilderOptions<T, TProperty> MustAsync<T, TProperty>(
        //    this IRuleBuilder<T, TProperty> ruleBuilder,
        //    Func<TProperty, Task<bool>> validate)
        //{
        //    return ruleBuilder
        //        .MustAsync((x, _) =>
        //        {
        //            return validate(x);
        //        });
        //}

        //public static IRuleBuilderOptions<T, TProperty> MustAsync<T, TProperty>(
        //    this IRuleBuilder<T, TProperty> ruleBuilder,
        //    Func<T, TProperty, PropertyValidatorContext, Task<bool>> validate)
        //{
        //    return ruleBuilder
        //        .MustAsync((parent, x, context,_) =>
        //        {
        //            return validate(parent, x, context);
        //        });
        //}

        //public static IRuleBuilderInitial<T, TProperty> CustomAsync<T, TProperty>(
        //    this IRuleBuilder<T, TProperty> ruleBuilder,
        //    Func<TProperty, CustomContext, Task> validate)
        //{
        //    return ruleBuilder
        //        .CustomAsync((x, context, _) =>
        //        {
        //            return validate(x, context);
        //        });
        //}

        //public static IRuleBuilderInitial<T, TProperty> CustomAsync<T, TProperty>(
        //    this IRuleBuilder<T, TProperty> ruleBuilder,
        //    Func<T, TProperty, CustomContext, Task> validate)
        //{
        //    return ruleBuilder
        //        .CustomAsync((x, context, _) =>
        //        {
        //            var parent = (T)context.ParentContext.InstanceToValidate;
        //            return validate(parent, x, context);
        //        });
        //}

        //#endregion

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
