using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentValidation.IoC
{
    public static class LiteralServiceExtensions
    {
        private readonly static IReadOnlyDictionary<string, object> emptyMessageValues = new ReadOnlyDictionary<string,object>(new Dictionary<string,object>());

        public static string GetPropertyName<T>(this ILiteralService literalService, string propertyName)
        {
            return literalService.GetPropertyName(typeof(T), propertyName);
        }

        public static string GetValidationErrorMessage(this ILiteralService literalService, string code)
        {
            return literalService.GetValidationErrorMessage(code, emptyMessageValues);
        }
    }
}
