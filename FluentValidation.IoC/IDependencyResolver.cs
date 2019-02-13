using System;

namespace FluentValidation.IoC
{
    public interface IDependencyResolver : IServiceProvider, IDisposable
    {
        T GetService<T>();
    }
}
