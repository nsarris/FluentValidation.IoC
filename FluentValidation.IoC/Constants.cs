using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    internal static class Constants
    {
        public static readonly string ValidatorFactoryKeyLiteral = "_" + nameof(FluentValidation.IoC) + "__validator_factory_key_literal_";
        public static readonly string DependencyResolverKeyLiteral = "_" + nameof(FluentValidation.IoC) + "_dependency_resolver_key_literal_";
    }
}
