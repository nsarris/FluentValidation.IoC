using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace FluentValidation.IoC
{
    public interface IValidationContextProvider
    {
        ValidationContext<T> BuildContext<T>(T instance);

        ValidationResult Validate<T>(T instance, Action<ValidationContext<T>> configureContext = null);
        Task<ValidationResult> ValidateAsync<T>(T instance, CancellationToken cancellation, Action<ValidationContext<T>> configureContext = null);
        Task<ValidationResult> ValidateAsync<T>(T instance, Action<ValidationContext<T>> configureContext = null);
        
        IValidationContextProvider<T> For<T>();
    }
}