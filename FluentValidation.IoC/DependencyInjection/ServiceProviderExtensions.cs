using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC
{
    public static class ServiceProviderExtensions
    {
        public static IValidatorFactory GetValidatorFactory(this IServiceProvider serviceProvider)
            => (IValidatorFactory)serviceProvider.GetService(typeof(IValidatorFactory));

        public static T GetService<T>(this IServiceProvider serviceProvider)
            => (T)serviceProvider.GetService(typeof(T));

    }
}
