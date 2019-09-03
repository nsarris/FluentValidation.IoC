using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Linq;
using System.Linq.Expressions;


namespace FluentValidation.IoC
{
    public sealed class InjectionRuleBuilder<T, TProperty>
    {
        #region Private Fields

        readonly IRuleBuilder<T, TProperty> ruleBuilder;
        
        #endregion

        #region Ctor

        public InjectionRuleBuilder(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        #endregion

        #region Specify validator to resolve

        public IRuleBuilderOptions<T, TProperty> InjectValidator<TValidator>(params string[] ruleSets)
            where TValidator : IValidator<TProperty>
        {
            return ruleBuilder.InjectValidator((s, ctx) => s.GetValidatorProvider().GetSpecificValidator<TValidator>(), ruleSets);
        }

        public IRuleBuilderOptions<T, TProperty> InjectValidator<TValidator>(Func<T, TProperty, TValidator, bool> validatorFunction)
            where TValidator : IValidator<TProperty>
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
                    var validator = context.ResolveValidator<TProperty, TValidator>();
                    return validatorFunction(parent, child, validator);
                });
        }

        public IRuleBuilderOptions<T, TProperty> InjectValidator<TValidator>(Func<T, TProperty, TValidator, ValidationResult> validatorFunction)
            where TValidator : IValidator<TProperty>
        {
            return
                (IRuleBuilderOptions<T, TProperty>)ruleBuilder
                .Custom((x, context) =>
                {
                    var validator = context.ResolveValidator<TProperty, TValidator>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, validator));
                });
        }

        #endregion

        #region Using Dependency

        public RuleBuilderDependencyContext<T, TProperty, TDependency> Inject<TDependency>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency>(ruleBuilder);
        }

        #endregion
    }
}
