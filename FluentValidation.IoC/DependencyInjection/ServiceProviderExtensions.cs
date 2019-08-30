using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC
{
    public static class ServiceProviderExtensions
    {
        public static IValidatorProvider GetValidatorProvider(this IServiceProvider serviceProvider)
            => (IValidatorProvider)serviceProvider.GetRequiredService(typeof(IValidatorProvider));
    }
}
