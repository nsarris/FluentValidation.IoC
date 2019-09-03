using FluentValidation.IoC;
using FluentValidation.IoC.Tests;
using FluentValidation.IoC.Tests.Core;
using FluentValidation.IoC.Tests.Model;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FluentValidation.IoC.Tests.Core
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var invalidCustomer = Data.GetInvalidCustomer();

            ValidationResult result;
            ILiteralService literalService;

            //Normally the ValidationContextProvider would be injected in the caller's constructor
            using (var scope = Setup.ServiceProvider.CreateScope())
            {
                var validationContextProvider = Setup.ServiceProvider.GetRequiredService<IValidationContextProvider>();
                
                validationContextProvider.ShouldHaveValidationErrorFor(c => c.VatNumber, invalidCustomer)
                    .WithErrorCode("VatValidationServiceFailure");

                result = validationContextProvider.Validate(invalidCustomer);
                literalService = (ILiteralService)validationContextProvider.ServiceProvider.GetService(typeof(ILiteralService));
            }

            Assert.IsTrue(result.Errors.Count == 4);

            Assert.IsTrue(result.Errors[0].ErrorCode == "VatValidationServiceFailure"
                && result.Errors[0].ErrorMessage == literalService.GetValidationErrorMessage(result.Errors[0].ErrorCode, result.Errors[0].FormattedMessagePlaceholderValues));

            Assert.IsTrue(result.Errors[1].ErrorMessage.EndsWith("is not a valid mobile phone number"));

            Assert.IsTrue(result.Errors[2].ErrorMessage.Contains("'Telephone Number'"));

            Assert.IsTrue(result.Errors[3].ErrorMessage.Contains("'Post Code'"));
        }
    }
}