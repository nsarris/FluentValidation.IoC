using FluentValidation;
using FluentValidation.Results;
using System;

namespace FluentValidation.IoC
{
    public sealed class IoCValidationInstanceContext<T>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly T instance;

        internal IoCValidationInstanceContext(T instance, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.instance = instance;
        }

        public ValidationContext<T> BuildContext()
            => IoCValidationContext.BuildContext<T>(instance, serviceProvider);

        public ValidationResult ValidateUsing<TValidator>()
            where TValidator : IValidator<T>
        {
            var validator = serviceProvider.GetValidatorFactory().GetSpecificValidator<TValidator>();
            return validator.Validate(IoCValidationContext.BuildContext(instance, serviceProvider));
        }

        public ValidationResult Validate()
        {
            var validator = serviceProvider.GetValidatorFactory().GetValidator<T>();
            return validator.Validate(IoCValidationContext.BuildContext(instance, serviceProvider));
        }

        public ValidationResult ValidateUsing<TValidator>(ValidationContext<T> context)
            where TValidator : IValidator<T>
        {
            var validator = serviceProvider.GetValidatorFactory().GetSpecificValidator<TValidator>();
            return validator.Validate(IoCValidationContext.SetupContext(context, serviceProvider));
        }

        public ValidationResult Validate(ValidationContext<T> context)
        {
            var validator = serviceProvider.GetValidatorFactory().GetValidator<T>();
            return validator.Validate(IoCValidationContext.SetupContext(context, serviceProvider));
        }
    }
}
