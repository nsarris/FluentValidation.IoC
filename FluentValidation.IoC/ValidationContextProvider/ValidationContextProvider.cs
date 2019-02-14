using FluentValidation;
using FluentValidation.Results;
using System;

namespace FluentValidation.IoC
{
    public sealed class ValidationContextProvider
    {
        #region Properties

        public IServiceProvider ServiceProvider { get; }

        #endregion

        #region Ctor

        public ValidationContextProvider(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        #endregion

        #region Context builders

        public ValidationContext<T> BuildContext<T>(T instance)
            => BuildContext<T>(instance, ServiceProvider);

        public ValidationContext BuildContext(object instance)
            => BuildContext(instance, ServiceProvider);

        public ValidationContext<T> SetupContext<T>(ValidationContext<T> context)
            => SetupContext<T>(context, ServiceProvider);

        public ValidationContext SetupContext(ValidationContext context)
            => SetupContext(context, ServiceProvider);

        #endregion

        #region Static context builders

        internal static ValidationContext<T> BuildContext<T>(T instance, IServiceProvider serviceProvider)
        {
            var context = new ValidationContext<T>(instance);
            context.SetServiceProvider(serviceProvider);
            return context;
        }

        internal static ValidationContext BuildContext(object instance, IServiceProvider serviceProvider)
        {
            var context = new ValidationContext(instance);
            context.SetServiceProvider(serviceProvider);
            return context;
        }

        internal static ValidationContext<T> SetupContext<T>(ValidationContext<T> context, IServiceProvider serviceProvider)
            => (ValidationContext<T>)SetupContext((ValidationContext)context, serviceProvider);
        
        internal static ValidationContext SetupContext(ValidationContext context, IServiceProvider serviceProvider)
        {
            context.RootContextData[Constants.ServiceProviderKeyLiteral] = serviceProvider;
            return context;
        }

        #endregion

        #region Validator builders

        public IValidator<T> GetValidator<T>()
            => ServiceProvider.GetValidatorFactory().GetValidator<T>();

        public TValidator GetSpecificValidator<TValidator>()
            where TValidator : IValidator
            => ServiceProvider.GetValidatorFactory().GetSpecificValidator<TValidator>();

        #endregion

        #region Validations

        public ValidationResult Validate<T>(T instance)
            => BuildContext(instance, ServiceProvider).Validate();
        
        public ValidationResult Validate<T>(ValidationContext<T> context)
            => SetupContext(context, ServiceProvider).Validate();
        
        public ValidationResult ValidateUsing<TValidator>(object instance)
            where TValidator : IValidator
            => BuildContext(instance, ServiceProvider).ValidateUsing<TValidator>();

        public ValidationResult ValidateUsing<TValidator>(ValidationContext context)
            where TValidator : IValidator
            => SetupContext(context, ServiceProvider).ValidateUsing<TValidator>();

        #endregion

        #region Define validated Type

        public ValidationContextProvider<T> For<T>()
            => new ValidationContextProvider<T>(ServiceProvider);
        
        #endregion
    }
}
