namespace FluentValidation.IoC
{
    public static class ValidatorProviderExtensions
    {
        public static TValidator GetSpecificValidator<TValidator>(this IValidatorProvider validatorFactory)
        {
            return (TValidator)validatorFactory.GetSpecificValidator(typeof(TValidator));
        }
    }
}
