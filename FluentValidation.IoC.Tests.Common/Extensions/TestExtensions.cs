using FluentValidation.Internal;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentValidation.IoC.Tests
{
    public static class TestExtensions
    {
        public static IEnumerable<ValidationFailure> ShouldHaveValidationErrorFor<T, TProperty>(this IValidationContextProvider validationContextProvider, Expression<Func<T, TProperty>> memberAccessor, T value)
        {
            string propertyName = ValidatorOptions.PropertyNameResolver(typeof(T), memberAccessor.GetMember(), memberAccessor);
            var result = validationContextProvider.Validate(value);
            return result.Errors.Where(x => x.PropertyName == propertyName).ToList();
        }

        public static IEnumerable<ValidationFailure> ShouldHaveValidationErrorFor<T, TProperty>(this ValidationContext<T> context, IValidator<T> validator, Expression<Func<T,TProperty>> memberAccessor)
        {
            string propertyName = ValidatorOptions.PropertyNameResolver(typeof(T), memberAccessor.GetMember(), memberAccessor);
            var result = validator.Validate(context);
            return result.Errors.Where(x => x.PropertyName == propertyName).ToList();
        }
    }
}