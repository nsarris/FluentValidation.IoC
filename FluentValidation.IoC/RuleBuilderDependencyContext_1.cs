using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    public sealed class RuleBuilderDependencyContext<T, TChild, TDependency>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;
        
        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
                    var dependency = context.ResolveDependency<TDependency>();
                    return validatorFunction(parent, child, dependency);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
                    var dependency = context.ResolveDependency<TDependency>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
                    var dependency = context.ResolveDependency<TDependency>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency));
                });
        }

        public RuleBuilderDependencyContext<T, TChild, TDependency, T2> Using<T2>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency ,T2>(ruleBuilder);
        }
    }
}
