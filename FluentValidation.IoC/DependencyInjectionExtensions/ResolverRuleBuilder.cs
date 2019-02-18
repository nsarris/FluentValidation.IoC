using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Linq;
using System.Linq.Expressions;


namespace FluentValidation.IoC
{
    public sealed class ResolverRuleBuilder<T, TChild>
    {
        #region Private Fields

        readonly IRuleBuilder<T, TChild> ruleBuilder;
        
        #endregion

        #region Ctor

        public ResolverRuleBuilder(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        #endregion

        #region Specify validator to resolve

        public IRuleBuilderOptions<T, TChild> InjectValidator<TValidator>(params string[] ruleSets)
            where TValidator : IValidator<TChild>
        {
            var adaptor = new ChildValidatorAdaptor(context =>
            {
                var actualContext = (PropertyValidatorContext)context;
                return actualContext.ResolveValidator<TChild, TValidator>();
            }, typeof(IValidator<TChild>))
            {
                RuleSets = ruleSets
            };

            return ruleBuilder.SetValidator(adaptor);
        }

        public IRuleBuilderOptions<T, TChild> InjectValidator<TValidator>(Func<T, TChild, TValidator, bool> validatorFunction)
            where TValidator : IValidator<TChild>
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
                    var validator = context.ResolveValidator<TChild, TValidator>();
                    return validatorFunction(parent, child, validator);
                });
        }

        public IRuleBuilderOptions<T, TChild> InjectValidator<TValidator>(Func<T, TChild, TValidator, ValidationResult> validatorFunction)
            where TValidator : IValidator<TChild>
        {
            return
                (IRuleBuilderOptions<T, TChild>)ruleBuilder
                .Custom((x, context) =>
                {
                    var validator = context.ResolveValidator<TChild, TValidator>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, validator));
                });
        }

        #endregion

        #region Using Dependency

        public RuleBuilderDependencyContext<T, TChild, TDependency> Inject<TDependency>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency>(ruleBuilder);
        }

        #endregion
    }
}
