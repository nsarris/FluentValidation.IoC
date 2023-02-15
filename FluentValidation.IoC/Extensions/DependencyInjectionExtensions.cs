using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.Internal;

namespace FluentValidation.IoC
{
    /// <summary>
    /// Extension methods for working with a Service Provider.
    /// </summary>
    internal static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Gets the service provider associated with the validation context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IServiceProvider GetServiceProvider(this IValidationContext context)
            => Get(context.RootContextData);

        /// <summary>
        /// Gets the service provider associated with the validation context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IServiceProvider GetServiceProvider<T, TProperty>(this MessageBuilderContext<T, TProperty> context)
            => Get(context.ParentContext.RootContextData);

        private static IServiceProvider Get(IDictionary<string, object> rootContextData)
        {
            if (rootContextData.TryGetValue("_FV_ServiceProvider", out var sp))
            {
                if (sp is IServiceProvider serviceProvider)
                {
                    return serviceProvider;
                }
            }

            throw new InvalidOperationException("The service provider has not been configured to work with FluentValidation. Making use of InjectValidator or GetServiceProvider is only supported when using the automatic MVC integration.");
        }

        /// <summary>
        /// Sets the service provider associated with the validation context.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceProvider"></param>
        public static void SetServiceProvider(this IValidationContext context, IServiceProvider serviceProvider)
        {
            context.RootContextData["_FV_ServiceProvider"] = serviceProvider;
        }

        /// <summary>
        /// Uses the Service Provider to inject the default validator for the property type.
        /// </summary>
        /// <param name="ruleBuilder"></param>
        /// <param name="callback"></param>
        /// <param name="ruleSets"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> InjectValidator<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Func<IServiceProvider, ValidationContext<T>, IValidator<TProperty>> callback, params string[] ruleSets)
        {
            var adaptor = new ChildValidatorAdaptor<T, TProperty>((context, _) => {
                var serviceProvider = context.GetServiceProvider();
                var validator = callback(serviceProvider, context);
                return validator;
            }, typeof(IValidator<TProperty>));

            adaptor.RuleSets = ruleSets;
            return ruleBuilder.SetAsyncValidator(adaptor);
        }
    }
}
