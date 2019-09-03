using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentValidation.IoC
{
    public interface IAssemblyScanner
    {
        IEnumerable<Type> FindValidatorsInAssemblies(IEnumerable<Assembly> assemblies);
    }
}
