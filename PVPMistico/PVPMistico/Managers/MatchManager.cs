using Models.Classes;
using PVPMistico.Constants;
using PVPMistico.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PVPMistico.Managers
{
    public class MatchManager : IMatchManager
    {
        private readonly IHttpManager _httpManager;

        public MatchManager(IHttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public async Task<List<MatchModel>> GetLeagueMatchesAsync(int leagueID)
        {
            return await _httpManager.GetAsync<List<MatchModel>>(ApiConstants.MatchesURL, extension: ApiConstants.LeagueExtension, parameter: leagueID.ToString());
        }

        public async Task<List<MatchModel>> GetMatchesAsync()
        {
            return await _httpManager.GetAsync<List<MatchModel>>(ApiConstants.MatchesURL);
        }

        public async Task<List<MatchModel>> GetPlayerLeagueMatchesAsync(int leagueid, string username)
        {
            var url = ApiConstants.MatchesURL + ApiConstants.LeagueExtension + leagueid + "/" + ApiConstants.UsernameExtension + username;
            return await _httpManager.GetAsync<List<MatchModel>>(url);
        }

        public async Task<List<MatchModel>> GetPlayerMatchesAsync(string username)
        {
            return await _httpManager.GetAsync<List<MatchModel>>(ApiConstants.MatchesURL, extension: ApiConstants.UsernameExtension, parameter: username);
        }
    }
}
