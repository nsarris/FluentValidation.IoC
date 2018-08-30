using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC.Tests
{
    public class MockLiteralService : ILiteralService
    {
        public string GetPropertyName(Type entityType, string propertyName)
        {
            return propertyName;
        }

        public string GetPropertyName<T>(string propertyName)
        {
            return propertyName;
        }

        public string GetValidationErrorMessage(string code)
        {
            if (code == "VatValidationServiceFailure")
                return "VAT Service failed to validate VAT for customer";

            return code;
        }
    }
}
