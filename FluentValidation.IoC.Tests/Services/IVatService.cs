namespace FluentValidation.IoC.Tests.Services
{
    public interface IVatService
    {
        bool IsValid(string vatNumber);
    }
}