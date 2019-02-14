using FluentValidation;
using FluentValidation.Results;
using System;

namespace FluentValidation.IoC
{
    public sealed class IoCValidationContext : IDisposable
    {
        public IDependencyResolver DependencyResolver { get; }
        
        public IoCValidationContext(IDependencyResolver resolver)
        {
            this.DependencyResolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public IoCValidationContext(IServiceProvider serviceProvider)
            : this(new DefaultDependencyResolver(serviceProvider))
        {
            
        }

        internal static ValidationContext<T> BuildContext<T>(T instance, IDependencyResolver resolver)
        {
            var context = new ValidationContext<T>(instance);
            context.RootContextData.Add(Constants.DependencyResolverKeyLiteral, resolver);
            return context;
        }

        internal static ValidationContext<T> SetupContext<T>(ValidationContext<T> context, IDependencyResolver resolver)
        {
            if (context.RootContextData.ContainsKey(Constants.DependencyResolverKeyLiteral))
                throw new InvalidOperationException("RootContextData already contains a dependency resolver");

            context.RootContextData.Add(Constants.DependencyResolverKeyLiteral, resolver);
            return context;
        }

        public ValidationResult Validate<T>(T instance)
        {
            var validator = DependencyResolver.GetValidatorFactory().GetValidator<T>();
            return validator.Validate(BuildContext(instance, DependencyResolver));
        }

        public ValidationResult Validate<T>(ValidationContext<T> context)
        {
            var validator = DependencyResolver.GetValidatorFactory().GetValidator<T>();
            return validator.Validate(SetupContext(context, DependencyResolver));
        }

        public IoCValidationContext<T> For<T>()
        {
            return new IoCValidationContext<T>(DependencyResolver);
        }

        public IoCValidationInstanceContext<T> For<T>(T instance)
        {
            return new IoCValidationInstanceContext<T>(instance, DependencyResolver);
        }

        public void Dispose()
        {
            DependencyResolver.Dispose();
        }
    }
}
