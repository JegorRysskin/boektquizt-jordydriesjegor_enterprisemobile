using System.Collections.ObjectModel;
using System.Threading.Tasks;
using JuryApp.Core.Models;
using JuryApp.Core.Services.Interfaces;

namespace JuryApp.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly HttpDataService _httpDataService;

        public TeamService()
        {
            _httpDataService = new HttpDataService();
        }

        public async Task<Teams> GetAllTeams(bool forceRefresh)
        {
            var result = await _httpDataService.GetAsync<Teams>("team", LoginService.AccessToken, forceRefresh);
            return result;
        }
        public async Task<bool> DeleteTeam(int id)
        {
            var result = await _httpDataService.DeleteAsync($"users/{id}", LoginService.AccessToken);
            return result;
        }
    }
}