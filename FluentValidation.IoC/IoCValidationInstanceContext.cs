using FluentValidation;
using FluentValidation.Results;

namespace FluentValidation.IoC
{
    public sealed class IoCValidationInstanceContext<T>
    {
        private readonly IDependencyResolver resolver;
        private readonly T instance;

        internal IoCValidationInstanceContext(T instance, IDependencyResolver resolver)
        {
            this.resolver = resolver;
            this.instance = instance;
        }

        public ValidationResult ValidateUsing<TValidator>()
            where TValidator : IValidator<T>
        {
            var validator = resolver.GetValidatorFactory().GetSpecificValidator<TValidator>();
            return validator.Validate(IoCValidationContext.BuildContext(instance, resolver));
        }

        public ValidationResult Validate()
        {
            var validator = resolver.GetValidatorFactory().GetValidator<T>();
            return validator.Validate(IoCValidationContext.BuildContext(instance, resolver));
        }

        public ValidationResult ValidateUsing<TValidator>(ValidationContext<T> context)
            where TValidator : IValidator<T>
        {
            var validator = resolver.GetValidatorFactory().GetSpecificValidator<TValidator>();
            return validator.Validate(IoCValidationContext.SetupContext(context, resolver));
        }

        public ValidationResult Validate(ValidationContext<T> context)
        {
            var validator = resolver.GetValidatorFactory().GetValidator<T>();
            return validator.Validate(IoCValidationContext.SetupContext(context, resolver));
        }
    }
}
