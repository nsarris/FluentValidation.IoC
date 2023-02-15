using FluentValidation.IoC;
using FluentValidation.IoC.Tests;
using FluentValidation.IoC.Tests.Core;
using FluentValidation.IoC.Tests.Model;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentValidation.IoC.Tests.Core
{
    public class Tests : IClassFixture<SetupFixture>
    {
        private readonly SetupFixture fixture;

        public Tests(SetupFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Test1()
        {
            var invalidCustomer = Data.GetInvalidCustomer();

            ValidationResult result;
            ILiteralService literalService;

            //Normally the ValidationContextProvider would be injected in the caller's constructor
            using (var scope = fixture.ServiceProvider.CreateScope())
            {
                var validationContextProvider = fixture.ServiceProvider.GetRequiredService<IValidationContextProvider>();

                validationContextProvider.ShouldHaveValidationErrorFor(c => c.VatNumber, invalidCustomer);
                    //.WithErrorCode("VatValidationServiceFailure");

                result = validationContextProvider.Validate(invalidCustomer);
                literalService = scope.ServiceProvider.GetRequiredService<ILiteralService>();
            }
            
            Assert.True(result.Errors.Count == 2);

            Assert.True(result.Errors[0].ErrorCode == "VatValidationServiceFailure"
                && result.Errors[0].ErrorMessage == literalService.GetValidationErrorMessage(
                    result.Errors[0].ErrorCode, 
                    result.Errors[0].FormattedMessagePlaceholderValues["PropertyName"].ToString(), 
                    result.Errors[0].FormattedMessagePlaceholderValues["PropertyValue"]));

            //Assert.EndsWith("is not a valid mobile phone number", result.Errors[1].ErrorMessage);

            //Assert.Contains("'Telephone Number'", result.Errors[2].ErrorMessage);

            Assert.Contains("'Post Code'", result.Errors[1].ErrorMessage);
        }
    }
}