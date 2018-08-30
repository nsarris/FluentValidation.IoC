using FluentValidation.Results;

namespace FluentValidation.IoC
{
    public sealed class IoCValidationContext<T, TValidator>
        where TValidator : IValidator<T>
    {
        private readonly TValidator validator;
        internal IoCValidationContext(TValidator validator)
        {
            this.validator = validator;
        }

        public ValidationResult Validate(T instance)
        {
            return validator.Validate(instance);
        }

        public ValidationResult Validate(ValidationContext<T> context)
        {
            return validator.Validate(context);
        }
    }
}
