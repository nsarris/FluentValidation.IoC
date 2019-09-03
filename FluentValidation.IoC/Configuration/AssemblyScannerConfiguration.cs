using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentValidation.IoC
{
    public class AssemblyScannerConfiguration
    {
        private readonly IAssemblyScanner assemblyScanner;

        internal List<Type> ValidatorTypes { get; } = new List<Type>();
        internal ServiceLifetime LifeTime { get; private set; } = ServiceLifetime.Transient;
        internal bool MapInterfaces { get; private set; } = true;

        internal AssemblyScannerConfiguration(IAssemblyScanner assemblyScanner)
        {
            this.assemblyScanner = assemblyScanner;
        }

        public AssemblyScannerConfiguration Scan(IEnumerable<Assembly> assemblies)
        {
            ValidatorTypes.AddRange(assemblyScanner.FindValidatorsInAssemblies(assemblies));
            return this;
        }

        public AssemblyScannerConfiguration WithLifeTime(ServiceLifetime lifetime)
        {
            LifeTime = lifetime;
            return this;
        }

        public AssemblyScannerConfiguration WithInterfaces(bool mapInterfaces)
        {
            MapInterfaces = mapInterfaces;
            return this;
        }
    }
}
