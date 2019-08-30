using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC
{
    internal class DefaultValidatorProvider : IValidatorProvider
    {
        private readonly IServiceProvider serviceProvider;

        public DefaultValidatorProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected virtual IValidator GetValidatorInternal(Type type, bool typeIsChecked)
        {
            if (!typeIsChecked && !typeof(IValidator).IsAssignableFrom(type))
                throw new InvalidOperationException($"Type {type.Name} does not implement IValidator");

            return (IValidator)serviceProvider.GetRequiredService(type);
        }

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)GetValidatorInternal(typeof(IValidator<T>), true);
        }

        public IValidator GetValidator(Type type)
        {
            return GetValidatorInternal(typeof(IValidator<>).MakeGenericType(type), false);
        }

        public IValidator GetSpecificValidator(Type validatorType)
        {
            return GetValidatorInternal(validatorType, true);
        }
    }
}
