using FluentValidation.Results;
using System;

namespace FluentValidation.IoC
{
    public sealed class IoCValidationContext<T, TValidator>
        where TValidator : IValidator<T>
    {
        private readonly IServiceProvider serviceProvider;
        internal IoCValidationContext(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TValidator GetValidator() => serviceProvider.GetValidatorFactory().GetSpecificValidator<TValidator>();
        public ValidationResult Validate(T instance) => GetValidator().Validate(instance);
        public ValidationResult Validate(ValidationContext<T> context) => GetValidator().Validate(context);
    }
}
