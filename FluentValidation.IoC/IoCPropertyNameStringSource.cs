using FluentValidation.Validators;
using System;

namespace FluentValidation.IoC
{
    internal class IoCPropertyNameStringSource : Resources.IStringSource
    {
        private readonly Type entityType;
        private readonly string propertyName;

        public IoCPropertyNameStringSource(Type entityType, string propertyName)
        {
            this.entityType = entityType;
            this.propertyName = propertyName;
        }

        public string GetString(IValidationContext context)
        {
            if (context == null)
                return null;
            else
            {
                if (context is ValidationContext validationContext)
                {
                    return validationContext.GetLiteralService().GetPropertyName(entityType, propertyName);
                }
                else if (context is PropertyValidatorContext propertyValidationContext)
                {
                    return propertyValidationContext.ParentContext.GetLiteralService().GetPropertyName(entityType, propertyName);
                }
                else
                    return null;
            }
        }

        public string ResourceName => null;

        public Type ResourceType => null;
    }

    internal class IoCErrorMessageStringSource : Resources.IStringSource
    {
        private readonly string code;

        public IoCErrorMessageStringSource(string code)
        {
            this.code = code;
        }

        public string GetString(IValidationContext context)
        {
            if (context == null)
                return null;
            else
            {
                if (context is ValidationContext validationContext)
                {
                    return validationContext.GetLiteralService().GetValidationErrorMessage(code);
                }
                else if(context is PropertyValidatorContext propertyValidationContext)
                {
                    return propertyValidationContext.ParentContext.GetLiteralService().GetValidationErrorMessage(code);
                }
                else
                    return code;
            }
        }

        public string ResourceName => null;

        public Type ResourceType => null;
    }
}
