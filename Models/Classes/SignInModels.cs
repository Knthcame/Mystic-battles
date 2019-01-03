namespace Models.Classes
{
    public class SignInModels
    {
        public AccountModel Account { get; set; }

        public TrainerModel Trainer { get; set; }

        public SignInModels() { }

        public SignInModels(AccountModel account, TrainerModel trainer)
        {
            Account = account;
            Trainer = trainer;
        }
    }
}
