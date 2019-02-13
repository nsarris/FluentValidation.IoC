using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC
{
    internal sealed class DependencyResolver : IDependencyResolver, IValidatorFactory
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

        public IValidator<T> GetValidator<T>()
        {
            return GetService<IValidator<T>>();
        }

        public IValidator GetValidator(Type type)
        {
            if (!typeof(IValidator).IsAssignableFrom(type))
                throw new InvalidOperationException($"Type {type.Name} does not implement IValidator");

            return (IValidator)GetService(type);
        }

        public TValidator GetValidator<T, TValidator>()
            where TValidator : IValidator<T>
        {
            return GetService<TValidator>();
        }

        public IValidator<T> GetValidator<T>(Type validatorType)
        {
            if (!typeof(IValidator<T>).IsAssignableFrom(validatorType))
                throw new InvalidOperationException($"Type {validatorType.Name} does not implement IValidator<{typeof(T).Name}>");

            return (IValidator<T>)GetService(validatorType);
        }
    }
}
