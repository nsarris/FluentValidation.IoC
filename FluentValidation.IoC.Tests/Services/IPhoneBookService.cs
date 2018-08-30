using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC.Tests.Services
{
    interface IPhoneBookService
    {
        bool IsExistingNumber(string phoneNumber);
        bool IsLandline(string phoneNumber);
        bool IsMobile(string phoneNumber);
    }
}
