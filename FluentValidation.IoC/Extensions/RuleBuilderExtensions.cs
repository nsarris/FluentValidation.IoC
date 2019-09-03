using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    public static partial class RuleBuilderExtensions
    {
        #region ResolveValidator - Resolve Validator from TProperty

        public static IRuleBuilderOptions<T, TProperty> InjectValidator<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IValidator<TProperty>, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
                    var validator = context.ResolveValidator<TProperty>();
                    return validatorFunction(parent, child, validator);
                });
        }

        public static IRuleBuilderInitial<T, TProperty> InjectValidator<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IValidator<TProperty>, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
                    var validator = context.ResolveValidator<TProperty>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, validator));
                });
        }

        #endregion

        #region ResolveValidator - Resolve a specific Validator

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
            return ruleBuilder.InjectValidator((sp, ctx) => (IValidator<TProperty>)sp.GetValidatorProvider().GetSpecificValidator(validatorType), ruleSets);
        }

        public static IRuleBuilderOptions<T, TProperty> InjectValidator<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Type validatorType,
            Func<T, TProperty, IValidator<TProperty>, bool> validatorFunction)
        {
            AssertValidatorType<TProperty>(validatorType);

            return ruleBuilder
                .Must((parent, child, context) =>
                {
                    var validator = context.ResolveValidator<TProperty>(validatorType);
                    return validatorFunction(parent, child, validator);
                });
        }

        public static IRuleBuilderInitial<T, TProperty> InjectValidator<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Type validatorType,
            Func<T, TProperty, IValidator<TProperty>, ValidationResult> validatorFunction)
        {
            AssertValidatorType<TProperty>(validatorType);

            return ruleBuilder
                .Custom((x, context) =>
                {
                    var validator = context.ResolveValidator<TProperty>(validatorType);
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, validator));
                });
        }

        #endregion

        #region Must and Custom Async - Inject service provider and implement custom logic

        public static IRuleBuilderOptions<T, TProperty> Must<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IServiceProvider, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) => validatorFunction(parent, child, context.GetServiceProvider()));
        }

        public static IRuleBuilderOptions<T, TProperty> Must<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IServiceProvider, PropertyValidatorContext, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) => validatorFunction(parent, child, context.GetServiceProvider(), context));
        }

        public static IRuleBuilderInitial<T, TProperty> Custom<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IServiceProvider, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
                    var resolver = context.GetServiceProvider();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, resolver));
                });
        }

        public static IRuleBuilderInitial<T, TProperty> Custom<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Action<T, TProperty, IServiceProvider, CustomContext> validatorAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validatorAction(parent, x, context.GetServiceProvider(), context);
                });
        }

        #endregion

        #region Must and Custom Async

        public static IRuleBuilderOptions<T, TProperty> MustAsync<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IServiceProvider, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) => validatorFunction(parent, child, context.GetServiceProvider(), cancellation));
        }

        public static IRuleBuilderOptions<T, TProperty> MustAsync<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IServiceProvider, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) => validatorFunction(parent, child, context.GetServiceProvider()));
        }

        public static IRuleBuilderOptions<T, TProperty> MustAsync<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IServiceProvider, PropertyValidatorContext, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) => validatorFunction(parent, child, context.GetServiceProvider(), context, cancellation));
        }

        public static IRuleBuilderOptions<T, TProperty> MustAsync<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IServiceProvider, PropertyValidatorContext, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) => validatorFunction(parent, child, context.GetServiceProvider(), context));
        }

        public static IRuleBuilderInitial<T, TProperty> CustomAsync<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IServiceProvider, CancellationToken, Task<ValidationResult>> validatorFunction)
        {
            return ruleBuilder
                .CustomAsync(async (x, context, cancellation) =>
                {
                    var resolver = context.GetServiceProvider();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(await validatorFunction(parent, x, resolver, cancellation));
                });
        }

        public static IRuleBuilderInitial<T, TProperty> CustomAsync<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IServiceProvider, Task<ValidationResult>> validatorFunction)
        {
            return ruleBuilder
                .CustomAsync(async (x, context, cancellation) =>
                {
                    var resolver = context.GetServiceProvider();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(await validatorFunction(parent, x, resolver));
                });
        }

        public static IRuleBuilderInitial<T, TProperty> CustomAsync<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IServiceProvider, CustomContext, CancellationToken, Task> validatorAction)
        {
            return ruleBuilder
                .CustomAsync(async (x, context, cancellation) =>
                {
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    await validatorAction(parent, x, context.GetServiceProvider(), context, cancellation);
                });
        }

        public static IRuleBuilderInitial<T, TProperty> CustomAsync<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder,
            Func<T, TProperty, IServiceProvider, CustomContext, Task> validatorAction)
        {
            return ruleBuilder
                .CustomAsync(async (x, context, cancellation) =>
                {
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    await validatorAction(parent, x, context.GetServiceProvider(), context);
                });
        }

        #endregion

        #region WithDependencies Builder

        public static InjectionRuleBuilder<T, TProperty> WithDependencies<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return new InjectionRuleBuilder<T, TProperty>(ruleBuilder);
        }

        #endregion
    }
}
