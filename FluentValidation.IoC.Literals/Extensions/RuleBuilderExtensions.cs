using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentValidation.IoC
{
    public static partial class LiteralRuleBuilderExtensions
    {
        private static (Type type, string propertyName) ExtractSelector<T>(Expression<Func<T, object>> selector)
        {
            if (selector.Body is MemberExpression memberExpression)
                return (memberExpression.Expression.Type, memberExpression.Member.Name);

            throw new ArgumentException("Expresssion provided is not a valid member expression.", nameof(selector));
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveName<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder)
        {
            return ruleBuilder
                .Configure(x =>
                {
                    x.SetDisplayName(d =>
                    {
                        return d.GetLiteralService().GetPropertyName(typeof(T), x.PropertyName);
                    });
                });
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveName<T, TProperty, TEntity>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, Expression<Func<TEntity, object>> selector)
        {
            var (entityType, propertyName) = ExtractSelector(selector);

            return ResolveName(ruleBuilder, entityType, propertyName);
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveName<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, Type entityType, string propertyName)
        {
            return ruleBuilder
                .Configure(x =>
                {
                    x.SetDisplayName(d =>
                    {
                        return d.GetLiteralService().GetPropertyName(entityType, propertyName);
                    });
                });
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, string code)
        {
            return ruleBuilder
                .Configure(x =>
                {
                    x.Current.ErrorCode = code;
                })
                .ResolveMessage()
                ;
        }

        public static IRuleBuilderOptions<T, TProperty> ResolveMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder)
        {
            return ruleBuilder
                .Configure(x =>
                {
                    x.Current.SetErrorMessage((c, p) => c.GetLiteralService().GetValidationErrorMessage(x.Current.ErrorCode, x.GetDisplayName(c), p));
                });
        }
    }
}
