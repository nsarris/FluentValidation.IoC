using FluentValidation.Validators;
using System;

namespace FluentValidation.IoC
{
    internal class InjectedErrorMessageStringSource : Resources.IStringSource
    {
        private readonly string code;

        public InjectedErrorMessageStringSource(string code)
        {
            this.code = code;
        }

        public string GetString(IValidationContext context)
        {
            if (context == null)
                return code;
            else
                return context.GetLiteralService().GetValidationErrorMessage(code);
        }

        public string ResourceName => null;

        public Type ResourceType => null;
    }
}
