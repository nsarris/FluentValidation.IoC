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

        public static IRuleBuilderInitial<T, TChild> SetResolvedValidator<T, TChild>(this IRuleBuilder<T, TChild> ruleBuilder)
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

        public static IRuleBuilderOptions<T, TChild> SetResolvedValidator<T, TChild>(
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

        public static IRuleBuilderInitial<T, TChild> SetResolvedValidator<T, TChild>(
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

        public static IRuleBuilderInitial<T, TChild> SetResolvedValidator<T, TChild>(
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

        public static IRuleBuilderOptions<T, TChild> SetResolvedValidator<T, TChild>(
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

        public static IRuleBuilderInitial<T, TChild> SetResolvedValidator<T, TChild>(
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

        #region String literals

        private static (Type entityType, string propertyName) ExtractSelector<T>(Expression<Func<T, object>> selector)
        {
            if (selector.Body is MemberExpression memberExpression)
                return (memberExpression.Expression.Type, memberExpression.Member.Name);

            throw new ArgumentException("Expresssion provided is not a valid member expression.", nameof(selector));
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveName<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder)
        {
            var propertyName = 
                GetRuleBuilder(ruleBuilder)?.Rule?.DisplayName.ResourceName
                ?? GetRuleBuilder(ruleBuilder)?.Rule?.PropertyName;

            if (string.IsNullOrEmpty(propertyName))
                return ruleBuilder;

            return ResolveName(ruleBuilder, typeof(T), propertyName);
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveName<T, TProperty, TEntity>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, Expression<Func<TEntity, object>> selector)
        {
            var (entityType, propertyName) = ExtractSelector(selector);

            return ResolveName(ruleBuilder, entityType, propertyName);
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveName<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, Type entityType, string propertyName)
        {
            return ruleBuilder
                .Configure(x => { x.DisplayName = new IoCPropertyNameStringSource(typeof(T), propertyName); });
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, string code)
        {
            return ruleBuilder
                .Configure(x =>
                {
                    x.CurrentValidator.Options.ErrorCodeSource = new Resources.StaticStringSource(code);
                    x.CurrentValidator.Options.ErrorMessageSource = new IoCErrorMessageStringSource(code);
                });
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder)
        {
            var errorCodeSource = GetRuleBuilder(ruleBuilder)?.Rule?.CurrentValidator?.Options?.ErrorCodeSource;
            if (!PropertyAccessor.TryGetValue<string>(errorCodeSource, "_message", out var code)
                || string.IsNullOrEmpty(code))
                throw new InvalidOperationException("Could not get code from validator. Please call WithErrorCode prior to ResolveMessage. Custom sources are not supported.");

            return ruleBuilder.ResolveMessage(code);
        }

        #endregion

        #region Public API

        public static ResolverRuleBuilder<T, TProperty> WithIoC<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return new ResolverRuleBuilder<T, TProperty>(ruleBuilder);
        }

        #endregion

        #region Helpers

        private static RuleBuilder<T,TProperty> GetRuleBuilder<T, TProperty>(IRuleBuilder<T,TProperty> ruleBuilder)
        {
            return (ruleBuilder as RuleBuilder<T, TProperty>);
        }

        #endregion
    }
}
