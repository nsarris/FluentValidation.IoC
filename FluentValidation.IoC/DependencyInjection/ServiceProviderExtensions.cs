using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC
{
    public static class ServiceProviderExtensions
    {
        public static IValidatorFactory GetValidatorFactory(this IServiceProvider serviceProvider)
            => (IValidatorFactory)serviceProvider.GetRequiredService(typeof(IValidatorFactory));
    }
}
