﻿using Unity;
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

            var validCustomer = new Customer()
            {
                Id = 1,
                Name = "Someone",
                VatNumber = "12321211",
                MainAddress = new Address()
                {
                    City = "Athens",
                    Street = "Some street",
                    Number = "11",
                    PrimaryPhone = new Phone("+3012321321", PhoneKind.Home),
                    OtherPhones = new List<Phone>()
                    {
                        new Phone("693232142332", PhoneKind.Mobile),
                        new Phone("432432323223", PhoneKind.Mobile),
                        new Phone("34234322", PhoneKind.Home),
                    }
                },
                OtherAddresses = new List<Address>()
                {
                    new Address()
                    {
                        City = "Patra",
                        Street = "Some other street",
                        Number = "121",
                        PrimaryPhone = new Phone("+3012321321", PhoneKind.Home),
                        OtherPhones = new List<Phone>()
                        {
                            new Phone("+30231212332423432", PhoneKind.Home),
                        }
                    },
                    new Address()
                    {
                        City = "Xanthi",
                        Street = "Another street",
                        Number = "111",
                        PrimaryPhone = new Phone("+3012321321", PhoneKind.Home),
                        OtherPhones = new List<Phone>()
                        {
                            new Phone("+30231214324322", PhoneKind.Home),
                            new Phone("3413211342343232", PhoneKind.Home),
                        }
                    }
                }
            };

            ValidationResult result;
            using (var validationContext = Setup.Container.Resolve<IoCValidationContext>())
            {
                result = validationContext.Validate(validCustomer);
            }

            Assert.IsTrue(result.Errors.Count == 4);

            Assert.IsTrue(result.Errors[0].ErrorCode == "VatValidationServiceFailure"
                && result.Errors[0].ErrorMessage == ServiceLocator.LiteralService.GetValidationErrorMessage(result.Errors[0].ErrorCode));

            Assert.IsTrue(result.Errors[1].ErrorMessage.EndsWith("is not a valid mobile phone number"));

            Assert.IsTrue(result.Errors[2].ErrorMessage.StartsWith("'Telephone Number'"));

            Assert.IsTrue(result.Errors[3].ErrorMessage == "'Post Code' should not be empty.");
        }
    }
}