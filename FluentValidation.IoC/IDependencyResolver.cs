using System;

namespace FluentValidation.IoC
{
    public interface IDependencyResolver : IDisposable
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}
