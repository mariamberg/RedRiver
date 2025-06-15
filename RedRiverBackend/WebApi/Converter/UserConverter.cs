using RedRiverApp.Core.Domain.Users;

namespace RedRiverApp.WebApi.Converter
{
    public class UserConverter
    {
        public LogInResponse ConvertToResponse(User user)
        {
            return new LogInResponse(user.GetUsername(), user.GetPassword());
        }
    }
}