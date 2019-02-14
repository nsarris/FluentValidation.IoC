using FluentValidation;
using FluentValidation.Results;

namespace FluentValidation.IoC
{
    public sealed class IoCValidationContext<T>
    {
        private readonly IDependencyResolver dependencyResolver;

        internal IoCValidationContext(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        public IoCValidationContext<T, TValidator> Using<TValidator>()
            where TValidator : IValidator<T>
        {
            return new IoCValidationContext<T, TValidator>(dependencyResolver.GetValidatorFactory().GetSpecificValidator<TValidator>());
        }

        public ValidationResult ValidateUsing<TValidator>(T instance)
            where TValidator : IValidator<T>
        {
            var validator = dependencyResolver.GetValidatorFactory().GetSpecificValidator<TValidator>();
            return validator.Validate(instance);
        }

        public ValidationResult ValidateUsing<TValidator>(ValidationContext<T> context)
            where TValidator : IValidator<T>
        {
            var validator = dependencyResolver.GetValidatorFactory().GetSpecificValidator<TValidator>();
            return validator.Validate(context);
        }
    }
}
