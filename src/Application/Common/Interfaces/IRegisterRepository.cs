namespace Application.Common.Interfaces
{
    public interface IRegisterRepository
    {
        Task<bool> TryRegister(string email, string password, string name, string phone);
        Task<bool> TryRegisterInExistLicense(string email, string password, string name, string phone, string license);
    }
}
