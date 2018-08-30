using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    public static class ServiceLocator
    {
        public static IDependencyResolver DependencyResolver { get; set; }
        public static IValidatorFactory ValidatorFactory { get; set; }
        public static ILiteralService LiteralService { get; set; }

        public static IoCValidationContext GetIoCValidationContext()
        {
            if (DependencyResolver is null 
                || ValidatorFactory is null)
                throw new InvalidOperationException("Cannot build an IoC Validation context if the DependencyResolver or ValidatorFactory are not set in ServiceLocator.");

            return new IoCValidationContext(DependencyResolver, ValidatorFactory);
        }

        internal static void AssertLiteralService()
        {
            if (LiteralService is null && DependencyResolver is null)
                throw new InvalidOperationException("Literal resolution is only supported by providing a LiteralSerice or a DependencyResolver in ServiceLocator.");
        }
    }
}
