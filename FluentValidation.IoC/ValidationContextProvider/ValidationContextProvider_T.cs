using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    public sealed class ValidationContextProvider<T>
    {
        #region Properties

        private readonly IServiceProvider serviceProvider;

        #endregion

        #region Ctor

        internal ValidationContextProvider(IServiceProvider dependencyResolver)
        {
            this.serviceProvider = dependencyResolver;
        }

        #endregion

        #region Context builders

        public ValidationContext<T> BuildContext(T instance)
            => ValidationContextProvider.BuildContext(instance, serviceProvider);

        public ValidationContext<T> SetupContext(ValidationContext<T> context)
            => ValidationContextProvider.SetupContext(context, serviceProvider);

        #endregion

        #region Validator builders

        public TValidator GetSpecificValidator<TValidator>() => serviceProvider.GetValidatorProvider().GetSpecificValidator<TValidator>();

        #endregion

        #region Validations

        public ValidationResult ValidateUsing<TValidator>(T instance) 
            where TValidator : IValidator<T>
            => BuildContext(instance).ValidateUsing<TValidator>();
        
        public ValidationResult ValidateUsing<TValidator>(ValidationContext<T> context)
            where TValidator : IValidator<T>
            => SetupContext(context).ValidateUsing<TValidator>();

        public ValidationResult Validate(T instance)
            => BuildContext(instance).Validate();

        public Task<ValidationResult> ValidateUsingAsync<TValidator>(T instance, CancellationToken cancellation = default)
            where TValidator : IValidator<T>
            => BuildContext(instance).ValidateUsingAsync<TValidator>(cancellation);

        public Task<ValidationResult> ValidateUsingAsync<TValidator>(ValidationContext<T> context, CancellationToken cancellation = default)
            where TValidator : IValidator<T>
            => SetupContext(context).ValidateUsingAsync<TValidator>(cancellation);

        public Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation = default)
            => BuildContext(instance).ValidateAsync();

        #endregion

        #region Define Validator type

        public ValidationContextProvider<T, TValidator> Using<TValidator>()
            where TValidator : IValidator<T>
        {
            return new ValidationContextProvider<T, TValidator>(serviceProvider);
        }

        #endregion
    }
}
