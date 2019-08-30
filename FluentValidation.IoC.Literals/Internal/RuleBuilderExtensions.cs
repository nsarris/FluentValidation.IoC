using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    internal static partial class RuleBuilderExtensions
    {
        internal static RuleBuilder<T,TProperty> GetRuleBuilder<T, TProperty>(this IRuleBuilder<T,TProperty> ruleBuilder)
        {
            return (ruleBuilder as RuleBuilder<T, TProperty>);
        }
    }
}
