using FluentValidation;
using FluentValidation.Results;
using System;

namespace FluentValidation.IoC
{
    public sealed class IoCValidationContext
    {
        public IServiceProvider ServiceProvider { get; }
        
        public IoCValidationContext(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public ValidationContext<T> BuildContext<T>(T instance)
            => BuildContext<T>(instance, ServiceProvider);

        public ValidationContext BuildContext(object instance)
            => BuildContext(instance, ServiceProvider);

        public ValidationContext<T> SetupContext<T>(ValidationContext<T> context)
            => SetupContext<T>(context, ServiceProvider);

        public ValidationContext SetupContext(ValidationContext context)
            => SetupContext(context, ServiceProvider);

        internal static ValidationContext<T> BuildContext<T>(T instance, IServiceProvider serviceProvider)
        {
            var context = new ValidationContext<T>(instance);
            context.RootContextData.Add(Constants.ServiceProviderKeyLiteral, serviceProvider);
            return context;
        }

        internal static ValidationContext BuildContext(object instance, IServiceProvider serviceProvider)
        {
            var context = new ValidationContext(instance);
            context.RootContextData.Add(Constants.ServiceProviderKeyLiteral, serviceProvider);
            return context;
        }

        internal static ValidationContext<T> SetupContext<T>(ValidationContext<T> context, IServiceProvider serviceProvider)
            => SetupContext(context, serviceProvider);
        
        internal static ValidationContext SetupContext(ValidationContext context, IServiceProvider serviceProvider)
        {
            if (context.RootContextData.ContainsKey(Constants.ServiceProviderKeyLiteral))
                throw new InvalidOperationException("RootContextData already contains a dependency serviceProvider");

            context.RootContextData.Add(Constants.ServiceProviderKeyLiteral, serviceProvider);
            return context;
        }

        public IValidator<T> GetValidator<T>()
            => ServiceProvider.GetValidatorFactory().GetValidator<T>();

        public TValidator GetSpecificValidator<TValidator>()
            where TValidator : IValidator
            => ServiceProvider.GetValidatorFactory().GetSpecificValidator<TValidator>();

        public ValidationResult Validate<T>(T instance)
            => GetValidator<T>().Validate(BuildContext(instance, ServiceProvider));
        
        public ValidationResult Validate<T>(ValidationContext<T> context)
            => GetValidator<T>().Validate(SetupContext(context, ServiceProvider));
        
        public ValidationResult ValidateUsing<TValidator>(object instance)
            where TValidator : IValidator
            => GetSpecificValidator<TValidator>().Validate(BuildContext(instance, ServiceProvider));

        public ValidationResult ValidateUsing<TValidator>(ValidationContext context)
            where TValidator : IValidator
            => GetSpecificValidator<TValidator>().Validate(SetupContext(context, ServiceProvider));

        public IoCValidationContext<T> For<T>()
            => new IoCValidationContext<T>(ServiceProvider);
        
        public IoCValidationInstanceContext<T> For<T>(T instance) 
            => new IoCValidationInstanceContext<T>(instance, ServiceProvider);
    }
}
