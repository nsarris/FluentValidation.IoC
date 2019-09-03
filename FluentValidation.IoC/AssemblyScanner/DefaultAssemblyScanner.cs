using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentValidation.IoC
{
    internal class DefaultAssemblyScanner : IAssemblyScanner
    {
        public IEnumerable<Type> FindValidatorsInAssemblies(IEnumerable<Assembly> assemblies)
        {
            return AssemblyScanner
                .FindValidatorsInAssemblies(assemblies)
                .Select(x => x.ValidatorType)
                .ToList();
        }
    }
}
