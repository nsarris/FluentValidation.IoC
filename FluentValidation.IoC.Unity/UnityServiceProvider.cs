using System;
using System.Collections;
using System.Linq;
using Unity;
using Unity.Extension;
using Unity.Lifetime;

namespace FluentValidation.IoC.Unity
{
    internal sealed class UnityServiceProvider : IServiceProvider
    {
        public IUnityContainer Container { get; }

        public UnityServiceProvider(IUnityContainer unityContainer)
        {
            this.Container = unityContainer ?? throw new ArgumentNullException(nameof(unityContainer));
        }

        public object GetService(Type serviceType) => Container.Resolve(serviceType);
        public object GetService<T>() => Container.Resolve<T>();
    }
}
