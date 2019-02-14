using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace FluentValidation.IoC
{
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
                    return validatorFunction(parent, child, dependency1);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1));
                });
        }
        public RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2> Using<TDependency2>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2>(ruleBuilder);
        }
    }
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
                    return validatorFunction(parent, child, dependency1, dependency2);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, TDependency2, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2));
                });
        }
        public RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3> Using<TDependency3>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3>(ruleBuilder);
        }
    }
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, TDependency2, TDependency3, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2, dependency3));
                });
        }
        public RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4> Using<TDependency4>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4>(ruleBuilder);
        }
    }
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2, dependency3, dependency4));
                });
        }
        public RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5> Using<TDependency5>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5>(ruleBuilder);
        }
    }
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5));
                });
        }
        public RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6> Using<TDependency6>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6>(ruleBuilder);
        }
    }
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6));
                });
        }
        public RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7> Using<TDependency7>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7>(ruleBuilder);
        }
    }
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7));
                });
        }
        public RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8> Using<TDependency8>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8>(ruleBuilder);
        }
    }
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8));
                });
        }
        public RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9> Using<TDependency9>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9>(ruleBuilder);
        }
    }
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9));
                });
        }
        public RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10> Using<TDependency10>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10>(ruleBuilder);
        }
    }
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
					var dependency10 = context.ResolveDependency<TDependency10>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
					var dependency10 = context.ResolveDependency<TDependency10>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
					var dependency10 = context.ResolveDependency<TDependency10>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10));
                });
        }
        public RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11> Using<TDependency11>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11>(ruleBuilder);
        }
    }
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
					var dependency10 = context.ResolveDependency<TDependency10>();
					var dependency11 = context.ResolveDependency<TDependency11>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
					var dependency10 = context.ResolveDependency<TDependency10>();
					var dependency11 = context.ResolveDependency<TDependency11>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
					var dependency10 = context.ResolveDependency<TDependency10>();
					var dependency11 = context.ResolveDependency<TDependency11>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11));
                });
        }
        public RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12> Using<TDependency12>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12>(ruleBuilder);
        }
    }
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
					var dependency10 = context.ResolveDependency<TDependency10>();
					var dependency11 = context.ResolveDependency<TDependency11>();
					var dependency12 = context.ResolveDependency<TDependency12>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, dependency12);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
					var dependency10 = context.ResolveDependency<TDependency10>();
					var dependency11 = context.ResolveDependency<TDependency11>();
					var dependency12 = context.ResolveDependency<TDependency12>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, dependency12, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
					var dependency10 = context.ResolveDependency<TDependency10>();
					var dependency11 = context.ResolveDependency<TDependency11>();
					var dependency12 = context.ResolveDependency<TDependency12>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, dependency12));
                });
        }
        public RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, TDependency13> Using<TDependency13>()
        {
            return new RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, TDependency13>(ruleBuilder);
        }
    }
	public sealed class RuleBuilderDependencyContext<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, TDependency13>
    {
        readonly IRuleBuilder<T, TChild> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TChild> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

        public IRuleBuilderOptions<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, TDependency13, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
					var dependency10 = context.ResolveDependency<TDependency10>();
					var dependency11 = context.ResolveDependency<TDependency11>();
					var dependency12 = context.ResolveDependency<TDependency12>();
					var dependency13 = context.ResolveDependency<TDependency13>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, dependency12, dependency13);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Action<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, TDependency13, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
					var dependency10 = context.ResolveDependency<TDependency10>();
					var dependency11 = context.ResolveDependency<TDependency11>();
					var dependency12 = context.ResolveDependency<TDependency12>();
					var dependency13 = context.ResolveDependency<TDependency13>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, dependency12, dependency13, context);
                });
        }

        public IRuleBuilderInitial<T, TChild> Custom(Func<T, TChild, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, TDependency13, ValidationResult> validatorFunction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.ResolveDependency<TDependency1>();
					var dependency2 = context.ResolveDependency<TDependency2>();
					var dependency3 = context.ResolveDependency<TDependency3>();
					var dependency4 = context.ResolveDependency<TDependency4>();
					var dependency5 = context.ResolveDependency<TDependency5>();
					var dependency6 = context.ResolveDependency<TDependency6>();
					var dependency7 = context.ResolveDependency<TDependency7>();
					var dependency8 = context.ResolveDependency<TDependency8>();
					var dependency9 = context.ResolveDependency<TDependency9>();
					var dependency10 = context.ResolveDependency<TDependency10>();
					var dependency11 = context.ResolveDependency<TDependency11>();
					var dependency12 = context.ResolveDependency<TDependency12>();
					var dependency13 = context.ResolveDependency<TDependency13>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    context.Append(validatorFunction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, dependency12, dependency13));
                });
        }
    }
}
