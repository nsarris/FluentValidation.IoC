using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    public static class ServiceLocator
    {
        private static IServiceProvider serviceProvider;
        private static IValidatorFactory validatorFactory;

        public static IServiceProvider GetServiceProvider()
        {
            return serviceProvider;
        }

        public static void SetServiceProvider(IServiceProvider value)
        {
            serviceProvider = value;
        }

        public static IValidatorFactory GetValidatorFactory()
        {
            return validatorFactory ?? serviceProvider.GetValidatorFactory();
        }

        public static void SetValidatorFactory(IValidatorFactory value)
        {
            validatorFactory = value;
        }

        public static IoCValidationContext CreateIoCValidationContext()
        {
            if (GetServiceProvider() is null 
                || GetValidatorFactory() is null)
                throw new InvalidOperationException("Cannot build an IoC Validation context if the DependencyResolver or ValidatorFactory are not set in ServiceLocator.");

            return new IoCValidationContext(GetServiceProvider());
        }
    }
}
