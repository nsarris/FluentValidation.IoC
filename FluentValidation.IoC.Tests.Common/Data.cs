using FluentValidation.IoC.Tests.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC.Tests
{
    public static class Data
    {
        public static Customer GetInValidCustomer()
        {
            return new Customer()
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
        }
    }
}
