using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace FluentValidation.IoC
{
	
	public sealed class RuleBuilderDependencyContext<T, TProperty, TDependency1>
    {
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

		public IRuleBuilderInitial<T, TProperty> Custom(Action<T, TProperty, TDependency1, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, context);
                });
        }

		public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, CustomContext, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, context);
                });
        }
		
        public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, CustomContext, CancellationToken, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, context, cancellation);
                });
        }

        public IRuleBuilderOptions<T, TProperty> Must(Func<T, TProperty, TDependency1, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
                    return validatorFunction(parent, child, dependency1);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
                    return validatorFunction(parent, child, dependency1);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
                    return validatorFunction(parent, child, dependency1, cancellation);
                });
        }

        public RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2> Inject<TDependency2>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2>(ruleBuilder);
        }
    }
	
	public sealed class RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2>
    {
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

		public IRuleBuilderInitial<T, TProperty> Custom(Action<T, TProperty, TDependency1, TDependency2, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, context);
                });
        }

		public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, CustomContext, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, context);
                });
        }
		
        public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, CustomContext, CancellationToken, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, context, cancellation);
                });
        }

        public IRuleBuilderOptions<T, TProperty> Must(Func<T, TProperty, TDependency1, TDependency2, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
                    return validatorFunction(parent, child, dependency1, dependency2);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
                    return validatorFunction(parent, child, dependency1, dependency2);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
                    return validatorFunction(parent, child, dependency1, dependency2, cancellation);
                });
        }

        public RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3> Inject<TDependency3>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3>(ruleBuilder);
        }
    }
	
	public sealed class RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3>
    {
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

		public IRuleBuilderInitial<T, TProperty> Custom(Action<T, TProperty, TDependency1, TDependency2, TDependency3, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, context);
                });
        }

		public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, CustomContext, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, context);
                });
        }
		
        public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, CustomContext, CancellationToken, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, context, cancellation);
                });
        }

        public IRuleBuilderOptions<T, TProperty> Must(Func<T, TProperty, TDependency1, TDependency2, TDependency3, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, cancellation);
                });
        }

        public RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4> Inject<TDependency4>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4>(ruleBuilder);
        }
    }
	
	public sealed class RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4>
    {
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

		public IRuleBuilderInitial<T, TProperty> Custom(Action<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, context);
                });
        }

		public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, CustomContext, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, context);
                });
        }
		
        public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, CustomContext, CancellationToken, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, context, cancellation);
                });
        }

        public IRuleBuilderOptions<T, TProperty> Must(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, cancellation);
                });
        }

        public RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5> Inject<TDependency5>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5>(ruleBuilder);
        }
    }
	
	public sealed class RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5>
    {
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

		public IRuleBuilderInitial<T, TProperty> Custom(Action<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, context);
                });
        }

		public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, CustomContext, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, context);
                });
        }
		
        public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, CustomContext, CancellationToken, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, context, cancellation);
                });
        }

        public IRuleBuilderOptions<T, TProperty> Must(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, cancellation);
                });
        }

        public RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6> Inject<TDependency6>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6>(ruleBuilder);
        }
    }
	
	public sealed class RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6>
    {
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

		public IRuleBuilderInitial<T, TProperty> Custom(Action<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, context);
                });
        }

		public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, CustomContext, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, context);
                });
        }
		
        public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, CustomContext, CancellationToken, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, context, cancellation);
                });
        }

        public IRuleBuilderOptions<T, TProperty> Must(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, cancellation);
                });
        }

        public RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7> Inject<TDependency7>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7>(ruleBuilder);
        }
    }
	
	public sealed class RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7>
    {
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

		public IRuleBuilderInitial<T, TProperty> Custom(Action<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, context);
                });
        }

		public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, CustomContext, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, context);
                });
        }
		
        public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, CustomContext, CancellationToken, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, context, cancellation);
                });
        }

        public IRuleBuilderOptions<T, TProperty> Must(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, cancellation);
                });
        }

        public RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8> Inject<TDependency8>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8>(ruleBuilder);
        }
    }
	
	public sealed class RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8>
    {
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

		public IRuleBuilderInitial<T, TProperty> Custom(Action<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, context);
                });
        }

		public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, CustomContext, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, context);
                });
        }
		
        public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, CustomContext, CancellationToken, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, context, cancellation);
                });
        }

        public IRuleBuilderOptions<T, TProperty> Must(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, cancellation);
                });
        }

        public RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9> Inject<TDependency9>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9>(ruleBuilder);
        }
    }
	
	public sealed class RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9>
    {
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

		public IRuleBuilderInitial<T, TProperty> Custom(Action<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, context);
                });
        }

		public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, CustomContext, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, context);
                });
        }
		
        public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, CustomContext, CancellationToken, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, context, cancellation);
                });
        }

        public IRuleBuilderOptions<T, TProperty> Must(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, cancellation);
                });
        }

        public RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10> Inject<TDependency10>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10>(ruleBuilder);
        }
    }
	
	public sealed class RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10>
    {
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

		public IRuleBuilderInitial<T, TProperty> Custom(Action<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, context);
                });
        }

		public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, CustomContext, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, context);
                });
        }
		
        public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, CustomContext, CancellationToken, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, context, cancellation);
                });
        }

        public IRuleBuilderOptions<T, TProperty> Must(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, cancellation);
                });
        }

        public RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11> Inject<TDependency11>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11>(ruleBuilder);
        }
    }
	
	public sealed class RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11>
    {
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

		public IRuleBuilderInitial<T, TProperty> Custom(Action<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
					var dependency11 = context.GetServiceProvider().GetRequiredService<TDependency11>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, context);
                });
        }

		public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, CustomContext, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
					var dependency11 = context.GetServiceProvider().GetRequiredService<TDependency11>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, context);
                });
        }
		
        public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, CustomContext, CancellationToken, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
					var dependency11 = context.GetServiceProvider().GetRequiredService<TDependency11>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, context, cancellation);
                });
        }

        public IRuleBuilderOptions<T, TProperty> Must(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
					var dependency11 = context.GetServiceProvider().GetRequiredService<TDependency11>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
					var dependency11 = context.GetServiceProvider().GetRequiredService<TDependency11>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
					var dependency11 = context.GetServiceProvider().GetRequiredService<TDependency11>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, cancellation);
                });
        }

        public RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12> Inject<TDependency12>()
        {
            return new RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12>(ruleBuilder);
        }
    }
	
	public sealed class RuleBuilderDependencyContext<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12>
    {
        readonly IRuleBuilder<T, TProperty> ruleBuilder;

        public RuleBuilderDependencyContext(IRuleBuilder<T, TProperty> ruleBuilder)
        {
            this.ruleBuilder = ruleBuilder;
        }

		public IRuleBuilderInitial<T, TProperty> Custom(Action<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, CustomContext> validationAction)
        {
            return ruleBuilder
                .Custom((x, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
					var dependency11 = context.GetServiceProvider().GetRequiredService<TDependency11>();
					var dependency12 = context.GetServiceProvider().GetRequiredService<TDependency12>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, dependency12, context);
                });
        }

		public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, CustomContext, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
					var dependency11 = context.GetServiceProvider().GetRequiredService<TDependency11>();
					var dependency12 = context.GetServiceProvider().GetRequiredService<TDependency12>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, dependency12, context);
                });
        }
		
        public IRuleBuilderInitial<T, TProperty> CustomAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, CustomContext, CancellationToken, Task> validationAction)
        {
            return ruleBuilder
                .CustomAsync((x, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
					var dependency11 = context.GetServiceProvider().GetRequiredService<TDependency11>();
					var dependency12 = context.GetServiceProvider().GetRequiredService<TDependency12>();
                    var parent = (T)context.ParentContext.InstanceToValidate;
                    return validationAction(parent, x, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, dependency12, context, cancellation);
                });
        }

        public IRuleBuilderOptions<T, TProperty> Must(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, bool> validatorFunction)
        {
            return ruleBuilder
                .Must((parent, child, context) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
					var dependency11 = context.GetServiceProvider().GetRequiredService<TDependency11>();
					var dependency12 = context.GetServiceProvider().GetRequiredService<TDependency12>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, dependency12);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, _) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
					var dependency11 = context.GetServiceProvider().GetRequiredService<TDependency11>();
					var dependency12 = context.GetServiceProvider().GetRequiredService<TDependency12>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, dependency12);
                });
        }

		public IRuleBuilderOptions<T, TProperty> MustAsync(Func<T, TProperty, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, CancellationToken, Task<bool>> validatorFunction)
        {
            return ruleBuilder
                .MustAsync((parent, child, context, cancellation) =>
                {
					var dependency1 = context.GetServiceProvider().GetRequiredService<TDependency1>();
					var dependency2 = context.GetServiceProvider().GetRequiredService<TDependency2>();
					var dependency3 = context.GetServiceProvider().GetRequiredService<TDependency3>();
					var dependency4 = context.GetServiceProvider().GetRequiredService<TDependency4>();
					var dependency5 = context.GetServiceProvider().GetRequiredService<TDependency5>();
					var dependency6 = context.GetServiceProvider().GetRequiredService<TDependency6>();
					var dependency7 = context.GetServiceProvider().GetRequiredService<TDependency7>();
					var dependency8 = context.GetServiceProvider().GetRequiredService<TDependency8>();
					var dependency9 = context.GetServiceProvider().GetRequiredService<TDependency9>();
					var dependency10 = context.GetServiceProvider().GetRequiredService<TDependency10>();
					var dependency11 = context.GetServiceProvider().GetRequiredService<TDependency11>();
					var dependency12 = context.GetServiceProvider().GetRequiredService<TDependency12>();
                    return validatorFunction(parent, child, dependency1, dependency2, dependency3, dependency4, dependency5, dependency6, dependency7, dependency8, dependency9, dependency10, dependency11, dependency12, cancellation);
                });
        }

    }
}
