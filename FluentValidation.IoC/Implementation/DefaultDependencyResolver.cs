using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        public IServiceProvider ServiceProvider { get; }
        
        public DefaultDependencyResolver(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (ServiceProvider is IDisposable disposable)
                disposable.Dispose();
        }

        public T GetService<T>()
        {
            return (T)ServiceProvider.GetService(typeof(T));
        }

        public virtual object GetService(Type serviceType)
        {
            return ServiceProvider.GetService(serviceType);
        }

        public virtual IValidatorFactory GetValidatorFactory()
        {
            return this.GetService<IValidatorFactory>();
        }
    }
}
