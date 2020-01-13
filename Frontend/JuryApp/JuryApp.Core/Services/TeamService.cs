using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using System.Threading.Tasks;
using JuryApp.Core.Models;

namespace JuryApp.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly HttpDataService _httpDataService;

        public TeamService()
        {
            _httpDataService = new HttpDataService();
        }

        public async Task<Teams> GetAllTeams()
        {
            var result = await _httpDataService.GetAsync<Teams>("team", await LoginService.Login());
            return result;
        }

        public async Task<Team> GetTeamById(int id)
        {
            var result = await _httpDataService.GetAsync<Team>($"team/id/{id}");

            return result;
        }

        public async Task<bool> DeleteTeam(int id)
        {
            var result = await _httpDataService.DeleteAsync($"users/{id}", await LoginService.Login());
            return result;
        }

        public async Task<bool> PatchTeamScore(int id, int score)
        {
            var result = await _httpDataService.PatchAsJsonAsync($"team/score/{id}", new Team{ TeamScore = score }, await LoginService.Login());
            return result;
        }

        public async Task<bool> EditTeam(int id, Team editedTeam)
        {
            var result = await _httpDataService.PatchAsJsonAsync($"team/{id}", editedTeam, await LoginService.Login());

            return result;
        }
    }
}