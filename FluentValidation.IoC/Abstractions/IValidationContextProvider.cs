using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace FluentValidation.IoC
{
    public interface IValidationContextProvider
    {
        IServiceProvider ServiceProvider { get; }

        ValidationContext BuildContext(object instance);
        ValidationContext<T> BuildContext<T>(T instance);
        ValidationContext SetupContext(ValidationContext context);
        ValidationContext<T> SetupContext<T>(ValidationContext<T> context);
        
        TValidator GetSpecificValidator<TValidator>() where TValidator : IValidator;
        IValidator<T> GetValidator<T>();
        
        ValidationResult Validate<T>(T instance);
        ValidationResult Validate<T>(ValidationContext<T> context);
        Task<ValidationResult> ValidateAsync<T>(T instance, CancellationToken cancellation = default);
        Task<ValidationResult> ValidateAsync<T>(ValidationContext<T> context, CancellationToken cancellation = default);
        ValidationResult ValidateUsing<TValidator>(object instance) where TValidator : IValidator;
        ValidationResult ValidateUsing<TValidator>(ValidationContext context) where TValidator : IValidator;
        Task<ValidationResult> ValidateUsingAsync<TValidator>(object instance, CancellationToken cancellation = default) where TValidator : IValidator;
        Task<ValidationResult> ValidateUsingAsync<TValidator>(ValidationContext context, CancellationToken cancellation = default) where TValidator : IValidator;

        IValidationContextProvider<T> For<T>();
    }
}