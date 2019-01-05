using SQLite;

namespace Models.Classes
{
    [Table(nameof(AccountModel))]
    public class AccountModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int DatabaseID { get; set; }

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

        public AccountModel(string username, string password, string email, string name)
        {
            Username = username;
            Password = password;
            Email = email;
            Name = name;
        }
    }
}
