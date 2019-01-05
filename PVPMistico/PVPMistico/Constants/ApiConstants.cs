namespace PVPMistico.Constants
{
    public abstract class ApiConstants
    {
        public const string BaseURL = "https://pvpservice20190105011315.azurewebsites.net/";

        public const string LogInURL = BaseURL + "api/LogIn/";

        public const string SignInURL = BaseURL + "api/SignIn/";

        public const string LeaderboardsURL = BaseURL + "api/Leaderboards/";

        public const string TrainersURL = BaseURL + "api/Trainers/";

        public const string MatchesURL = BaseURL + "api/Match/";

        public const string UsernameExtension = "username/";

        public const string IdExtension = "id/";

        public const string MatchExtension = "match/";

        public const string TrainerExtension = "trainer/";

        public const string LeagueExtension = "league/";
    }
}
