using System.Linq;

namespace FluentValidation.IoC.Tests.Services
{
    //This is service mocks an external dependency
    public class MockVatService : IVatService
    {
        public bool IsValid(string vatNumber)
        {
            if (string.IsNullOrEmpty(vatNumber))
                return false;

            return vatNumber.Length >= 10
                && vatNumber.Length <= 20 
                && vatNumber.Take(2).All(x => char.IsLetter(x))
                && vatNumber.Skip(2).All(x => char.IsDigit(x));
        }
    }
}
