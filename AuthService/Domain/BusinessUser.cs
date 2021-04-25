namespace MDC.AuthService.Domain
{
    public class BusinessUser
    {
        public BusinessUser(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; }
        public string Password { get; }

        public bool PasswordMatches(string passwordToTest)
        {
            return Password == passwordToTest;
        }
    }
}