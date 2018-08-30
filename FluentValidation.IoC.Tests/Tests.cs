using Unity;
using FluentValidation.IoC.Tests.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                VatNumber = "EL12321211",
                MainAddress = new Address()
                {
                    City = "Athens",
                    Street = "Some street",
                    Number = "11",
                    PrimaryPhone = new Phone("+3012321321", PhoneKind.Home),
                    OtherPhones = new List<Phone>()
                    {
                        new Phone("+3023121254354", PhoneKind.Home),
                        new Phone("34132113254353", PhoneKind.Home),
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

            using (var validationContext = Setup.Container.Resolve<IoCValidationContext>())
            {
                var result = validationContext.Validate(validCustomer);
            }
        }
    }
}
