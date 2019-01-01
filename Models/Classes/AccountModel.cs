namespace Models.Classes
{
    public class AccountModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public AccountModel() { }

        public AccountModel(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
