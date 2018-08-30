using System;
using Unity;

namespace FluentValidation.IoC.Unity
{
    public class UnityValidatorDisposableResolver : UnityValidatorResolverBase
    {
        public UnityValidatorDisposableResolver(IUnityContainer container) 
            : base(container, true)
        {
            
        }
    }
}
