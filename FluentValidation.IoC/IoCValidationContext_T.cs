using FluentValidation;
using FluentValidation.Results;

namespace FluentValidation.IoC
{
    public sealed class IoCValidationContext<T>
    {
        private readonly IValidatorFactory validatorFactory;
        internal IoCValidationContext(IValidatorFactory validatorFactory)
        {
            this.validatorFactory = validatorFactory;
        }

        public IoCValidationContext<T, TValidator> Using<TValidator>()
            where TValidator : IValidator<T>
        {
            return new IoCValidationContext<T, TValidator>(validatorFactory.GetValidator<T, TValidator>());
        }

        public ValidationResult ValidateUsing<TValidator>(T instance)
            where TValidator : IValidator<T>
        {
            var validator = validatorFactory.GetValidator<T, TValidator>();
            return validator.Validate(instance);
        }

        public ValidationResult ValidateUsing<TValidator>(ValidationContext<T> context)
            where TValidator : IValidator<T>
        {
            var validator = validatorFactory.GetValidator<T, TValidator>();
            return validator.Validate(context);
        }
    }
}
