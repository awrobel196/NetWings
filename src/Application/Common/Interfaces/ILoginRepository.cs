namespace Application.Common.Interfaces
{
    public interface ILoginRepository 
    {
        Task<bool> TryLogin(string name, string password);
        Task Logout();
    }
}
