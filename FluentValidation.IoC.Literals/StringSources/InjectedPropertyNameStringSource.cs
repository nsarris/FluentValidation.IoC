using FluentValidation.Validators;
using System;

namespace FluentValidation.IoC
{
    internal class InjectedPropertyNameStringSource : Resources.IStringSource
    {
        private readonly Type entityType;
        private readonly string propertyName;

        public InjectedPropertyNameStringSource(Type entityType, string propertyName)
        {
            this.entityType = entityType;
            this.propertyName = propertyName;
        }

        public string GetString(IValidationContext context)
        {
            if (context == null)
                return propertyName;
            else
                return context.GetLiteralService().GetPropertyName(entityType, propertyName);
        }

        public string ResourceName => null;

        public Type ResourceType => null;
    }
}
