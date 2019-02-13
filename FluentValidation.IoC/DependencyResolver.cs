using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC
{
    public sealed class DependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider serviceProvider;

        public DependencyResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            if (serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }

        public T GetService<T>()
        {
            return (T)serviceProvider.GetService(typeof(T));
        }

        public object GetService(Type serviceType)
        {
            return serviceProvider.GetService(serviceType);
        }
    }
}
