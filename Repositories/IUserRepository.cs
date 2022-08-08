using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
