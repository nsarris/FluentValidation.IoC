using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentValidation.IoC
{
    internal class AttributeDuplicateResolutionStrategy : IDuplicateValidatorResolutionStrategy
    {
        public AttributeDuplicateResolutionStrategy(Type attributeType)
        {
            AttributeType = attributeType ?? throw new ArgumentNullException(nameof(attributeType));
        }

        public Type AttributeType { get; }

        public ServiceDescriptor Resolve(IEnumerable<ServiceDescriptor> duplicates)
        {
            return duplicates.OrderByDescending(y => y.ImplementationType.GetCustomAttributes(false).Any(a => a.GetType() == AttributeType)).First();
        }
    }
}
