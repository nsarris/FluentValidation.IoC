using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace FluentValidation.IoC
{
    public static class LiteralContextExtensions
    {
        public static ILiteralService GetLiteralService(this IValidationContext context)
        {
            return context.GetServiceProvider().GetRequiredService<ILiteralService>();
        }
    }
}
