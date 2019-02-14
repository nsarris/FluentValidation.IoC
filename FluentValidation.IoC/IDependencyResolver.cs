using System;

namespace FluentValidation.IoC
{
    public interface IDependencyResolver : IServiceProvider, IDisposable
    {
        IValidatorFactory GetValidatorFactory();
        T GetService<T>();
    }
}
