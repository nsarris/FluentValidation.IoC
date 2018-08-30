using System;
using Unity;

namespace FluentValidation.IoC.Unity
{
    public class UnityValidatorResolver : UnityValidatorResolverBase
    {
        public UnityValidatorResolver(IUnityContainer container) 
            : base(container, false)
        {
            
        }
    }
}
