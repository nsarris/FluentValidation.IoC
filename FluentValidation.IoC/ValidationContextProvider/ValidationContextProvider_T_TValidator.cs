using FluentValidation.Results;
using System;

namespace FluentValidation.IoC
{
    public sealed class ValidationContextProvider<T, TValidator>
        where TValidator : IValidator<T>
    {
        private readonly IServiceProvider serviceProvider;

        internal ValidationContextProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ValidationContext<T> BuildContext(T instance)
            => ValidationContextProvider.BuildContext(instance, serviceProvider);

        public TValidator GetValidator() 
            => serviceProvider.GetValidatorFactory().GetSpecificValidator<TValidator>();

        public ValidationResult Validate(T instance) 
            => BuildContext(instance).Validate();
    }
}
