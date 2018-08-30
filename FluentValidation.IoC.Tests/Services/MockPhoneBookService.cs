using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC.Tests.Services
{
    class MockPhoneBookService : IPhoneBookService
    {
        public bool IsExistingNumber(string phoneNumber)
        {
            return true;
        }

        public bool IsLandline(string phoneNumber)
        {
            return true;
        }

        public bool IsMobile(string phoneNumber)
        {
            return phoneNumber.StartsWith("69");
        }
    }
}
