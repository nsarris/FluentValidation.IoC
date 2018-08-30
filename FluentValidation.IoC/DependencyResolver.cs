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

        public T Resolve<T>()
        {
            return (T)serviceProvider.GetService(typeof(T));
        }

        public object Resolve(Type type)
        {
            return serviceProvider.GetService(type);
        }
    }
}
