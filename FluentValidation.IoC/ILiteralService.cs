using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    public interface ILiteralService
    {
        string GetPropertyName(Type entityType, string propertyName);
        string GetPropertyName<T>(string propertyName);
        string GetValidationErrorMessage(string code, IReadOnlyDictionary<string,object> messageValues);
        string GetValidationErrorMessage(string code);
    }
}
