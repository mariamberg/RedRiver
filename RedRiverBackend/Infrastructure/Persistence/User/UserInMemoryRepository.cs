using System.Collections.Concurrent;

using RedRiverApp.Core.Domain.Users;
using RedRiverApp.Core.Domain.LogIns;
using RedRiverApp.Core.Domain.Users.Port;

namespace RedRiverApp.Infrastructure.Persistence.Users
{
    public class UserInMemoryRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<string, User> repository;

        public UserInMemoryRepository()
        {
            repository = new ConcurrentDictionary<string, User>();
        }

        public bool LogIn(LogIn login)
        {
            if (repository.TryGetValue(login.Username, out var user))
            {
                if (user.IsCorrectUsernameAndPassword(user.GetUsername(), user.GetPassword()))
                {
                    return true;
                }
                return false;

            }
            System.Console.WriteLine("Okänd användare: " + login.Username);
            return false;
        }

        public User Create(User user)
        {
            
            repository.TryAdd(user.GetUsername(), user);
            return user;
        }


        public User Get(string username)
        {
            if (repository.TryGetValue(username, out User? value))
            {
                return value;
            }

            throw new KeyNotFoundException($"Användare med användarnamn '{username}' finns inte");
        }

        public Boolean IsExistingUser(string username)
        {
            return repository.ContainsKey(username);
        }

        public User Update(User user)
        {
            // Säkerställer att användare med användarnamn finns innan uppdatering.
            Get(user.GetUsername());
            repository[user.GetUsername()] = user;
            return user;
        }
    }
}