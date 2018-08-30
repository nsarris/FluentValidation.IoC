using System;
using Unity;

namespace FluentValidation.IoC.Unity
{
    public class UnityValidatorHierarchicalResolver : UnityValidatorResolverBase
    {
        public UnityValidatorHierarchicalResolver(IUnityContainer container)
            :base(container.CreateChildContainer(), true)
        {
            
        }
    }
}
