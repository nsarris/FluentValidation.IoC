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
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public InjectionRuleBuilder(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilder<T, TProperty> InjectValidator<TValidator>(params string[] ruleSets)
            where TValidator : IValidator<TProperty>
        {
            return ruleBuilder.InjectValidator(typeof(TValidator), ruleSets);
        }

        public RuleBuilderDependencyContext<T, TProperty, TDependency> Inject<TDependency>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency>(ruleBuilder);
        }
    }
}
