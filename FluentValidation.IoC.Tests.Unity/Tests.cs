using Unity;
using FluentValidation.IoC.Tests.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;


namespace FluentValidation.IoC.Tests
{
    [TestFixture]
    class Tests
    {
        [Test]
        public void Test()
        {
            var validCustomer = Data.GetValidCustomer();
            
            ValidationResult result;
            ILiteralService literalService;

            //Normally the ValidationContextProvider would be injected in the caller's constructor
            using (var scope = Setup.Container.CreateChildContainer())
            {
                var validationContext = Setup.Container.Resolve<ValidationContextProvider>();
                result = validationContext.Validate(validCustomer);
                literalService = (ILiteralService)validationContext.ServiceProvider.GetService(typeof(ILiteralService));
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
