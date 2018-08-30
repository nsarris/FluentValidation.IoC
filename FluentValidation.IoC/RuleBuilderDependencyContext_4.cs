using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    public sealed class RuleBuilderDependencyContext<T, TChild, T1, T2, T3, T4>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, T1, T2, T3, T4, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
                    var dependency1 = context.ResolveDependency<T1>();
                    var dependency2 = context.ResolveDependency<T2>();
                    var dependency3 = context.ResolveDependency<T3>();
                    var dependency4 = context.ResolveDependency<T4>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, T1, T2, T3, T4, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
                    var dependency1 = context.ResolveDependency<T1>();
                    var dependency2 = context.ResolveDependency<T2>();
                    var dependency3 = context.ResolveDependency<T3>();
                    var dependency4 = context.ResolveDependency<T4>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, T1, T2, T3, T4, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
                    var dependency1 = context.ResolveDependency<T1>();
                    var dependency2 = context.ResolveDependency<T2>();
                    var dependency3 = context.ResolveDependency<T3>();
                    var dependency4 = context.ResolveDependency<T4>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2, dependency3, dependency4));
                });
        }
    }
}
