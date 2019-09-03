﻿using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    internal sealed class ValidationContextProvider<T, TValidator> : IValidationContextProvider<T, TValidator> where TValidator : IValidator<T>
    {
        private readonly IServiceProvider serviceProvider;

        internal ValidationContextProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ValidationContext<T> BuildContext(T instance)
            => new ValidationContext<T>(instance).WithServiceProvider(serviceProvider);

        public TValidator GetValidator()
            => serviceProvider.GetValidatorProvider().GetSpecificValidator<TValidator>();

        public ValidationResult Validate(T instance)
            => BuildContext(instance).Validate();

        public Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation = default)
            => BuildContext(instance).ValidateAsync(cancellation);
    }
}
