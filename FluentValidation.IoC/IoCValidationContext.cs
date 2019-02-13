using FluentValidation;
using FluentValidation.Results;
using System;

namespace FluentValidation.IoC
{
    public sealed class IoCValidationContext : IDisposable
    {
        public IDependencyResolver DependencyResolver { get; }
        public IValidatorFactory ValidatorFactory { get; }

        public IoCValidationContext(IDependencyResolver resolver, IValidatorFactory validatorFactory)
        {
            if (resolver is null) throw new ArgumentNullException(nameof(resolver));

            this.DependencyResolver = resolver;
            this.ValidatorFactory = validatorFactory ?? resolver.GetService<IValidatorFactory>();
        }

        public IoCValidationContext(IServiceProvider serviceProvider)
            : this(new DependencyResolver(serviceProvider), null)
        {

        }

        public IoCValidationContext(IServiceProvider serviceProvider, IValidatorFactory validatorFactory)
            :this(new DependencyResolver(serviceProvider), validatorFactory)
        {
            
        }

        public IoCValidationContext(IDependencyResolver resolver)
            :this(resolver, null)
        {
            
        }


        internal static ValidationContext<T> BuildContext<T>(T instance, IDependencyResolver resolver, IValidatorFactory validatorFactory)
        {
            var context = new ValidationContext<T>(instance);
            context.RootContextData.Add(Constants.DependencyResolverKeyLiteral, resolver);
            context.RootContextData.Add(Constants.ValidatorFactoryKeyLiteral, validatorFactory);
            return context;
        }

        internal static ValidationContext<T> SetupContext<T>(ValidationContext<T> context, IDependencyResolver resolver, IValidatorFactory validatorFactory)
        {
            if (context.RootContextData.ContainsKey(Constants.DependencyResolverKeyLiteral))
                throw new InvalidOperationException("RootCotextData already contains a container");

            if (context.RootContextData.ContainsKey(Constants.ValidatorFactoryKeyLiteral))
                throw new InvalidOperationException("RootCotextData already contains a validator factory");

            context.RootContextData.Add(Constants.DependencyResolverKeyLiteral, resolver);
            context.RootContextData.Add(Constants.ValidatorFactoryKeyLiteral, validatorFactory);
            return context;
        }

        public ValidationResult Validate<T>(T instance)
        {
            var validator = ValidatorFactory.GetValidator<T>();
            return validator.Validate(BuildContext(instance, DependencyResolver, ValidatorFactory));
        }

        public ValidationResult Validate<T>(ValidationContext<T> context)
        {
            var validator = ValidatorFactory.GetValidator<T>();
            return validator.Validate(SetupContext(context, DependencyResolver, ValidatorFactory));
        }

        public IoCValidationContext<T> For<T>()
        {
            return new IoCValidationContext<T>(ValidatorFactory);
        }

        public IoCValidationInstanceContext<T> For<T>(T instance)
        {
            return new IoCValidationInstanceContext<T>(instance, DependencyResolver, ValidatorFactory);
        }

        public void Dispose()
        {
            DependencyResolver.Dispose();
        }
    }
}
