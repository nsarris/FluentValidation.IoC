using Unity;
using FluentValidation.IoC.Tests.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using FluentValidation.TestHelper;


namespace FluentValidation.IoC.Tests
{
    [TestFixture]
    class Tests
    {
        [Test]
        public void Test()
        {
            var invalidCustomer = Data.GetInvalidCustomer();
            
            ValidationResult result;
            ILiteralService literalService;

            //Normally the ValidationContextProvider would be injected in the caller's constructor
            using (var scope = Setup.Container.CreateChildContainer())
            {
                var validationContextProvider = Setup.Container.Resolve<IValidationContextProvider>();

                validationContextProvider.ShouldHaveValidationErrorFor(c => c.VatNumber, invalidCustomer)
                   .WithErrorCode("VatValidationServiceFailure");

                result = validationContextProvider.Validate(invalidCustomer);
                literalService = scope.Resolve<ILiteralService>();
            }

            Assert.IsTrue(result.Errors.Count == 2);

            Assert.IsTrue(result.Errors[0].ErrorCode == "VatValidationServiceFailure"
                && result.Errors[0].ErrorMessage == literalService.GetValidationErrorMessage(result.Errors[0].ErrorCode, result.Errors[0].FormattedMessagePlaceholderValues));

            //Assert.IsTrue(result.Errors[1].ErrorMessage.EndsWith("is not a valid mobile phone number"));

            //Assert.IsTrue(result.Errors[2].ErrorMessage.Contains("'Telephone Number'"));

            Assert.IsTrue(result.Errors[1].ErrorMessage.Contains("'Post Code'"));
        }
    }
}
