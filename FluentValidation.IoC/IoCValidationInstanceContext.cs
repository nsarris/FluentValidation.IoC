using FluentValidation;
using FluentValidation.Results;

namespace FluentValidation.IoC
{
    public sealed class IoCValidationInstanceContext<T>
    {
        private readonly IDependencyResolver resolver;
        private readonly IValidatorFactory validatorFactory;
        private readonly T instance;

        internal IoCValidationInstanceContext(IDependencyResolver resolver, IValidatorFactory validatorFactory,T instance)
        {
            this.resolver = resolver;
            this.instance = instance;
            this.validatorFactory = validatorFactory;
        }

        public ValidationResult ValidateUsing<TValidator>()
            where TValidator : IValidator<T>
        {
            var validator = validatorFactory.GetValidator<T, TValidator>();
            return validator.Validate(IoCValidationContext.BuildContext(instance, resolver, validatorFactory));
        }

        public ValidationResult Validate()
        {
            var validator = validatorFactory.GetValidator<T>();
            return validator.Validate(IoCValidationContext.BuildContext(instance, resolver, validatorFactory));
        }

        public ValidationResult ValidateUsing<TValidator>(ValidationContext<T> context)
            where TValidator : IValidator<T>
        {
            var validator = validatorFactory.GetValidator<T, TValidator>();
            return validator.Validate(IoCValidationContext.SetupContext(context, resolver, validatorFactory));
        }

        public ValidationResult Validate(ValidationContext<T> context)
        {
            var validator = validatorFactory.GetValidator<T>();
            return validator.Validate(IoCValidationContext.SetupContext(context, resolver, validatorFactory));
        }
    }
}
