namespace RedRiverApp.Core.Domain.Users
{
    public class User(string Username, string Password)
    {
        private readonly string Username = Username;
        private readonly string Password = Password;

        public bool IsCorrectUsernameAndPassword(string UsernameInput, string PasswordInput)
        {
            return (UsernameInput.Equals(Username) && PasswordInput.Equals(Password));
        }

        public string GetUsername()
        {
            return Username;
        }

        public string GetPassword()
        {
            return Password;
        }

    }
}