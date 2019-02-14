using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC
{
    public static class ContextExtensions
    {
        public static CustomContext Append(this CustomContext validationContext, ValidationResult result)
        {
            foreach (var failure in result.Errors)
                validationContext.AddFailure(failure);
            return validationContext;
        }
    }
}
