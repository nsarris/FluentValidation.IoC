using Microsoft.Extensions.DependencyInjection;
using System;

namespace FluentValidation.IoC
{
    internal class ValidatorServiceDescriptor
    {
        public ValidatorServiceDescriptor(Type validatorType, ServiceLifetime lifetime, bool mapInterfaces)
        {
            ValidatorType = validatorType;
            Lifetime = lifetime;
            MapInterfaces = mapInterfaces;
        }

        public Type ValidatorType { get; }
        public ServiceLifetime Lifetime { get; }
        public bool MapInterfaces { get; }
    }
}
