using FluentValidation;
using FluentValidation.Results;
using System;

namespace FluentValidation.IoC
{
    public sealed class IoCValidationContext<T>
    {
        private readonly IServiceProvider serviceProvider;

        internal IoCValidationContext(IServiceProvider dependencyResolver)
        {
            this.serviceProvider = dependencyResolver;
        }

        public ValidationContext<T> BuildContext(T instance)
            => IoCValidationContext.BuildContext<T>(instance, serviceProvider);

        public ValidationContext<T> SetupContext(ValidationContext<T> context)
            => IoCValidationContext.SetupContext<T>(context, serviceProvider);

        public IoCValidationContext<T, TValidator> Using<TValidator>()
            where TValidator : IValidator<T>
        {
            return new IoCValidationContext<T, TValidator>(serviceProvider);
        }

        public TValidator GetSpecificValidator<TValidator>() => serviceProvider.GetValidatorFactory().GetSpecificValidator<TValidator>();

        public ValidationResult ValidateUsing<TValidator>(T instance) 
            where TValidator : IValidator<T>
            => GetSpecificValidator<TValidator>().Validate(instance);
        

        public ValidationResult ValidateUsing<TValidator>(ValidationContext<T> context)
            where TValidator : IValidator<T>
            => GetSpecificValidator<TValidator>().Validate(context);

        public ValidationResult Validate(T instance)
            => serviceProvider.GetValidatorFactory().GetValidator<T>().Validate(instance);
    }
}
