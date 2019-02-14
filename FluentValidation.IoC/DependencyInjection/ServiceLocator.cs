using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    public static class ServiceLocator
    {
        private static IDependencyResolver dependencyResolver;
        private static IValidatorFactory validatorFactory;

        public static IDependencyResolver GetDependencyResolver()
        {
            return dependencyResolver;
        }

        public static void SetDependencyResolver(IDependencyResolver value)
        {
            dependencyResolver = value;
        }

        public static void SetDependencyResolver(IServiceProvider value)
        {
            dependencyResolver = new DefaultDependencyResolver(value);
        }

        public static IValidatorFactory GetValidatorFactory()
        {
            return validatorFactory;
        }

        public static void SetValidatorFactory(IValidatorFactory value)
        {
            validatorFactory = value;
        }

        public static IoCValidationContext CreateIoCValidationContext()
        {
            if (GetDependencyResolver() is null 
                || GetValidatorFactory() is null)
                throw new InvalidOperationException("Cannot build an IoC Validation context if the DependencyResolver or ValidatorFactory are not set in ServiceLocator.");

            return new IoCValidationContext(GetDependencyResolver());
        }
    }
}
