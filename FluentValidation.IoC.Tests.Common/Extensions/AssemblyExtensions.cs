using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace FluentValidation.IoC.Tests
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Assembly> FilterFramework(this IEnumerable<Assembly> assemblies)
        {
            return assemblies
                    .Where(x => !x.IsDynamic)
                    .Where(x => !x.FullName.StartsWith("System.")
                        && !x.FullName.StartsWith("Microsoft."))
                    .Where(x => !x.FullName.Contains("PublicKeyToken=b77a5c561934e089"))
                    .Where(x => !x.FullName.StartsWith("Unity.")
                        && !x.FullName.StartsWith("nunit."))
                    ;
        }
    }
}
