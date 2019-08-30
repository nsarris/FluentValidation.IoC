using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Linq.Expressions;

namespace FluentValidation.IoC
{
    public static partial class LiteralRuleBuilderExtensions
    {
        private static (Type entityType, string propertyName) ExtractSelector<T>(Expression<Func<T, object>> selector)
        {
            if (selector.Body is MemberExpression memberExpression)
                return (memberExpression.Expression.Type, memberExpression.Member.Name);

            throw new ArgumentException("Expresssion provided is not a valid member expression.", nameof(selector));
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveName<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder)
        {
            var propertyName =
                ruleBuilder.GetRuleBuilder()?.Rule?.GetDisplayName()
                ?? ruleBuilder.GetRuleBuilder()?.Rule?.PropertyName;

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
                .Configure(x => { x.DisplayName = new InjectedPropertyNameStringSource(typeof(T), propertyName); });
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, string code)
        {
            return ruleBuilder
                .Configure(x =>
                {
                    x.CurrentValidator.Options.ErrorCodeSource = new Resources.StaticStringSource(code);
                    x.CurrentValidator.Options.ErrorMessageSource = new InjectedErrorMessageStringSource(code);
                });
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder)
        {
            var errorCodeSource = ruleBuilder.GetRuleBuilder()?.Rule?.CurrentValidator?.Options?.ErrorCodeSource;

            if (!PropertyAccessor.TryGetValue<string>(errorCodeSource, "_message", out var code)
                || string.IsNullOrEmpty(code))
                throw new InvalidOperationException("Could not get code from validator. Please call WithErrorCode prior to ResolveMessage. Custom sources are not supported.");

            return ruleBuilder.ResolveMessage(code);
        }
    }
}
