using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace FluentValidation.IoC.Unity
{
    public abstract class UnityValidatorResolverBase : DefaultDependencyResolver
    {
        private readonly bool disposeContainer;

        public IUnityContainer Container { get; }

        protected UnityValidatorResolverBase(IUnityContainer container, bool disposeContainer)
            : base(new UnityServiceProvider(container))
        {
            Container = container;
            this.disposeContainer = disposeContainer;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposeContainer)
                base.Dispose(disposing);
        }
    }
}
