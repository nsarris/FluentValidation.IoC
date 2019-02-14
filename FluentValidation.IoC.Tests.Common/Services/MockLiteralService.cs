using FluentValidation.IoC.Tests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC.Tests.Services
{
    public class MockLiteralService : ILiteralService
    {
        public string GetPropertyName(Type entityType, string propertyName)
        {
            if (entityType == typeof(Phone) && propertyName == nameof(Phone.Number))
                return "Telephone Number";

            return propertyName;
        }

        public string GetPropertyName<T>(string propertyName)
        {
            return GetPropertyName(typeof(T), propertyName);
        }

        public string GetValidationErrorMessage(string code, IReadOnlyDictionary<string, object> messageValues)
        {
            if (code == "VatValidationServiceFailure")
            {
                return $"VAT Service failed to validate VAT number '{messageValues?["PropertyValue"] ?? "{PropertyValue}"}' for customer";
            }

            return code;
        }

        public string GetValidationErrorMessage(string code)
        {
            return GetValidationErrorMessage(code, null);
        }
    }
}
