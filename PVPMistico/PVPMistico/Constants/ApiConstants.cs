namespace PVPMistico.Constants
{
    public abstract class ApiConstants
    {
        public const string BaseURL = "http://192.168.1.37:5001/";

        public const string LogInURL = BaseURL + "api/LogIn/";

        public const string SignInURL = BaseURL + "api/SignIn/";

        public const string LeaderboardsURL = BaseURL + "api/Leaderboards/";

        public const string UsernameExtension = "username/";

        public const string IdExtension = "id/";

        public const string MatchExtension = "match/";

        public const string TrainerExtension = "trainer/";
    }
}
