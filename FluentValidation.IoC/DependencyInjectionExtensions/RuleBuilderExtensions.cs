using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Linq.Expressions;

namespace FluentValidation.IoC
{
    public static partial class RuleBuilderExtensions
    {
        #region ExecuteChild  

        internal static ValidationResult ExecuteChild<TChild>(this CustomContext context, TChild instance, IValidator<TChild> childValidator)
        {
            var childContext =
                context.ParentContext.IsChildCollectionContext ?
                context.ParentContext.CloneForChildCollectionValidator(instance, true) :
                context.ParentContext.CloneForChildValidator(instance, true, context.ParentContext.Selector);

            return childValidator.Validate(childContext);
        }

        internal static ValidationResult ExecuteChild<TChild>(this CustomContext context, TChild instance, Type validatorType)
        {
            var childValidator = context.ResolveValidator<TChild>(validatorType);
            return ExecuteChild(context, instance, childValidator);
        }

        #endregion

        #region ResolveValidator - Resolve Validator from TChild

        public static IRuleBuilderInitial<T, TChild> InjectValidator<T, TChild>(this IRuleBuilder<T, TChild> ruleBuilder)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
                    if (x != null)
                    {
                        var validator = context.ResolveValidator<TChild>();
                        context.Append(ExecuteChild(context, x, validator));
                    }
                });
        }

        public static IRuleBuilderOptions<T, TChild> InjectValidator<T, TChild>(
            this IRuleBuilder<T, TChild> ruleBuilder, 
            Func<T, TChild, IValidator<TChild>, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
                    var validator = context.ResolveValidator<TChild>();
                    return validatorFunction(parent, child, validator);
                });
        }

        public static IRuleBuilderInitial<T, TChild> InjectValidator<T, TChild>(
            this IRuleBuilder<T, TChild> ruleBuilder, 
            Func<T, TChild, IValidator<TChild>, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
                    var validator = context.ResolveValidator<TChild>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, validator));
                });
        }

        #endregion

        #region ResolveValidator - Resolve a specific Validator

        private static void AssertValidatorType<TChild>(Type validatorType)
        {
            if (!typeof(IValidator<TChild>).IsAssignableFrom(validatorType))
                throw new InvalidOperationException($"Type {validatorType.Name} does not implement IValidator<{typeof(TChild).Name}>");
        }

        public static IRuleBuilderInitial<T, TChild> InjectValidator<T, TChild>(
            this IRuleBuilder<T, TChild> ruleBuilder, 
            Type validatorType)
        {
            AssertValidatorType<TChild>(validatorType);

            return ruleBuilder
                .Custom((x, context) =>
                {
                    if (x != null)
                    {
                        var validator = context.ResolveValidator<TChild>(validatorType);
                        context.Append(ExecuteChild(context, x, validator));
                    }
                });
        }

        public static IRuleBuilderOptions<T, TChild> InjectValidator<T, TChild>(
            this IRuleBuilder<T, TChild> ruleBuilder,
            Type validatorType, 
            Func<T, TChild, IValidator<TChild>, bool> validatorFunction)
        {
            AssertValidatorType<TChild>(validatorType);

            return ruleBuilder
                .Must((parent, child, context) =>
                {
                    var validator = context.ResolveValidator<TChild>(validatorType);
                    return validatorFunction(parent, child, validator);
                });
        }

        public static IRuleBuilderInitial<T, TChild> InjectValidator<T, TChild>(
            this IRuleBuilder<T, TChild> ruleBuilder,
            Type validatorType,
            Func<T, TChild, IValidator<TChild>, ValidationResult> validatorFunction)
        {
            AssertValidatorType<TChild>(validatorType);

            return ruleBuilder
                .Custom((x, context) =>
                {
                    var validator = context.ResolveValidator<TChild>(validatorType);
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, validator));
                });
        }

        #endregion

        #region Must and Custom - Inject service provider and implement custom logic

        public static IRuleBuilderOptions<T, TChild> Must<T, TChild>(
            this IRuleBuilder<T, TChild> ruleBuilder, 
            Func<T, TChild, IServiceProvider, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
                    return validatorFunction(parent, child, context.GetServiceProvider());
                });
        }

        public static IRuleBuilderOptions<T, TChild> Must<T, TChild>(
            this IRuleBuilder<T, TChild> ruleBuilder,
            Func<T, TChild, IServiceProvider, PropertyValidatorContext, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
                    return validatorFunction(parent, child, context.GetServiceProvider(), context);
                });
        }

        public static IRuleBuilderInitial<T, TChild> Custom<T, TChild>(
            this IRuleBuilder<T, TChild> ruleBuilder, 
            Func<T, TChild, IServiceProvider, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
                    var resolver = context.GetServiceProvider();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, resolver));
                });
        }

        public static IRuleBuilderInitial<T, TChild> Custom<T, TChild>(
            this IRuleBuilder<T, TChild> ruleBuilder, 
            Action<T, TChild, IServiceProvider, CustomContext> validatorAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validatorAction(parent, x, context.GetServiceProvider(), context);
                });
        }

        #endregion

        #region Public API

        public static ResolverRuleBuilder<T, TProperty> WithDependencies<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return new ResolverRuleBuilder<T, TProperty>(ruleBuilder);
        }

        #endregion

        #region Helpers

        internal static RuleBuilder<T,TProperty> GetRuleBuilder<T, TProperty>(this IRuleBuilder<T,TProperty> ruleBuilder)
        {
            return (ruleBuilder as RuleBuilder<T, TProperty>);
        }

        #endregion
    }
}
