using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User()
            {
                FirstName = "Read Only", LastName = "User", Email = "readonly@user.com",
                Id = Guid.NewGuid(), Username = "readonly@user.com", Password = "readonly@user.com",
                Roles = new List<string> { "reader" }
            },
            new User()
            {
                FirstName = "Read Write", LastName = "User", Email = "readwrite@user.com",
                Id = Guid.NewGuid(), Username = "readwrite@user.com", Password = "readwrite@user.com",
                Roles = new List<string> { "reader", "writer" }
            }
        };
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var user = Users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && 
            x.Password == password);

            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}
