using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC
{
    public class DefaultValidatorFactory : IValidatorFactory
    {
        private readonly IServiceProvider serviceProvider;

        public DefaultValidatorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected virtual IValidator GetValidatorInternal(Type type, bool typeIsChecked)
        {
            if (!typeIsChecked && !typeof(IValidator).IsAssignableFrom(type))
                throw new InvalidOperationException($"Type {type.Name} does not implement IValidator");

            return (IValidator)serviceProvider.GetService(type);
        }

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)GetValidatorInternal(typeof(IValidator<T>), true);
        }

        public IValidator GetValidator(Type type)
        {
            return GetValidatorInternal(typeof(IValidator<>).MakeGenericType(type), false);
        }

        public TValidator GetSpecificValidator<TValidator>()
        {
            return (TValidator)GetValidatorInternal(typeof(TValidator), true);

        }

        public IValidator GetSpecificValidator(Type validatorType)
        {
            //if (!typeof(IValidator<T>).IsAssignableFrom(validatorType))
            //    throw new InvalidOperationException($"Type {validatorType.Name} does not implement IValidator<{typeof(T).Name}>");

            //return (IValidator<T>)GetValidatorInternal(validatorType, true);
            return (IValidator)GetValidatorInternal(validatorType, true);
        }
    }
}
