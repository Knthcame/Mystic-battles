using Models.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PVPMistico.Managers.Interfaces
{
    public interface IMatchManager
    {
        Task<List<MatchModel>> GetMatchesAsync();

        Task<List<MatchModel>> GetLeagueMatchesAsync(int leagueID);

        Task<List<MatchModel>> GetPlayerMatchesAsync(string username);

        Task<List<MatchModel>> GetPlayerLeagueMatchesAsync(int leagueid, string username);
    }
}
