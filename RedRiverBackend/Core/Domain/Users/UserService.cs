using RedRiverApp.Core.Domain.Users.Port;
using RedRiverApp.Core.Domain.LogIns;

namespace RedRiverApp.Core.Domain.Users
{
    public class UserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public User Get(string username)
        {
            return repository.Get(username);
        }

        public User Create(NewUserRequest newUser)
        {
            User user = new User(newUser.Username, newUser.Password);
            return repository.Create(user);
        }

        public Boolean LogIn(LogIn logIn)
        {
            return repository.LogIn(logIn);
        }
    }
}