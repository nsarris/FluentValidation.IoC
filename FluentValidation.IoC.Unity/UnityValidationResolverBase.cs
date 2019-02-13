using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace FluentValidation.IoC.Unity
{
    public abstract class UnityValidatorResolverBase : IDependencyResolver, IValidatorFactory
    {
        protected readonly IUnityContainer container;
        private readonly bool disposeContainer;

        protected UnityValidatorResolverBase(IUnityContainer container, bool disposeContainer)
        {
            this.container = container;
            this.disposeContainer = disposeContainer;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposeContainer)
                container.Dispose();
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

        public T GetService<T>()
        {
            return container.Resolve<T>();
        }

        public object GetService(Type serviceType)
        {
            return container.Resolve(serviceType);
        }
    }
}
