namespace NZWalks.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AuthenticateAsync(string username, string password);
    }
}
