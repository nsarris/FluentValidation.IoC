using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace FluentValidation.IoC
{
    public interface IValidationContextProvider<T>
    {
        ValidationContext<T> BuildContext(T instance);
        ValidationContext<T> SetupContext(ValidationContext<T> context);

        IValidator<T> GetValidator();
        TValidator GetSpecificValidator<TValidator>() where TValidator : IValidator<T>;
        
        ValidationResult Validate(T instance);
        Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation = default);

        ValidationResult ValidateUsing(Type validatorType, T instance);
        ValidationResult ValidateUsing(Type validatorType, ValidationContext<T> context);
        Task<ValidationResult> ValidateUsingAsync(Type validatorType, T instance, CancellationToken cancellation = default);
        Task<ValidationResult> ValidateUsingAsync(Type validatorType, ValidationContext<T> context, CancellationToken cancellation = default);

        ValidationResult ValidateUsing<TValidator>(T instance) where TValidator : IValidator<T>;
        ValidationResult ValidateUsing<TValidator>(ValidationContext<T> context) where TValidator : IValidator<T>;
        Task<ValidationResult> ValidateUsingAsync<TValidator>(T instance, CancellationToken cancellation = default) where TValidator : IValidator<T>;
        Task<ValidationResult> ValidateUsingAsync<TValidator>(ValidationContext<T> context, CancellationToken cancellation = default) where TValidator : IValidator<T>;

        IValidationContextProvider<T, TValidator> Using<TValidator>() where TValidator : IValidator<T>;
    }
}