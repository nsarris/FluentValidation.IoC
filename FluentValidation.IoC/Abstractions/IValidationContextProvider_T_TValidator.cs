using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace FluentValidation.IoC
{
    public interface IValidationContextProvider<T, TValidator> where TValidator : IValidator<T>
    {
        ValidationContext<T> BuildContext(T instance);
        TValidator GetValidator();
        ValidationResult Validate(T instance);
        Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation = default);
    }
}