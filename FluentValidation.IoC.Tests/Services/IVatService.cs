namespace FluentValidation.IoC.Tests
{
    public interface IVatService
    {
        bool IsValid(string vatNumber);
    }
}