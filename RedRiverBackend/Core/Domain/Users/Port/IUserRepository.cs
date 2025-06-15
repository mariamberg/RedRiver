using RedRiverApp.Core.Domain.LogIns; 

namespace RedRiverApp.Core.Domain.Users.Port
{
    public interface IUserRepository
    {
        User Get(string username);
        User Create(User user);
        User Update(User user);
        bool LogIn(LogIn logIn);

    }

}