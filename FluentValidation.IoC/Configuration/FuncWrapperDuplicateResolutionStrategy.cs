using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FluentValidation.IoC
{
    internal class FuncWrapperDuplicateResolutionStrategy : IDuplicateValidatorResolutionStrategy
    {
        private readonly Func<IEnumerable<ServiceDescriptor>, ServiceDescriptor> resolver;

        public FuncWrapperDuplicateResolutionStrategy(Func<IEnumerable<ServiceDescriptor>, ServiceDescriptor> duplicateResolver)
        {
            this.resolver = duplicateResolver;
        }

        public ServiceDescriptor Resolve(IEnumerable<ServiceDescriptor> duplicates)
         => resolver(duplicates);
    }
}
