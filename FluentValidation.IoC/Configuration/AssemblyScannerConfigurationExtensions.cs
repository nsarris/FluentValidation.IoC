using System;
using System.Reflection;

namespace FluentValidation.IoC
{
    public static class AssemblyScannerConfigurationExtensions
    {
        public static AssemblyScannerConfiguration Scan(this AssemblyScannerConfiguration assemblyScannerConfiguration, Assembly assembly)
            => assemblyScannerConfiguration.Scan(new[] { assembly });

        public static AssemblyScannerConfiguration Scan<T>(this AssemblyScannerConfiguration assemblyScannerConfiguration)
            => assemblyScannerConfiguration.Scan(typeof(T));

        public static AssemblyScannerConfiguration Scan(this AssemblyScannerConfiguration assemblyScannerConfiguration, Type typeContainedInAssemblyToScan)
            => assemblyScannerConfiguration.Scan(typeContainedInAssemblyToScan.Assembly);
    }
}
