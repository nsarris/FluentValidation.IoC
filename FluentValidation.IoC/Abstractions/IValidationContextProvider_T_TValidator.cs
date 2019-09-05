using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace FluentValidation.IoC
{
    public interface IValidationContextProvider<T, TValidator> where TValidator : IValidator<T>
    {
        ValidationContext<T> BuildContext(T instance);
        ValidationResult Validate(T instance, Action<ValidationContext<T>> configureContext);
        Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation, Action<ValidationContext<T>> configureContext);
        Task<ValidationResult> ValidateAsync(T instance, Action<ValidationContext<T>> configureContext);
    }
}