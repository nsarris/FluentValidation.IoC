using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC
{
    internal class ServiceProviderValidatorFactory : IValidatorFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceProviderValidatorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IValidator<T> GetValidator<T>()
            => serviceProvider.GetRequiredService<IValidator<T>>();
        
        public IValidator GetValidator(Type type)
            => (IValidator)serviceProvider.GetRequiredService(typeof(IValidator<>).MakeGenericType(type));
    }
}
