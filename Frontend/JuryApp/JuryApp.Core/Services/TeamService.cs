using System.Collections.ObjectModel;
using System.Threading.Tasks;
using JuryApp.Core.Models;
using JuryApp.Core.Services.Interfaces;

namespace JuryApp.Core.Services
{
    public class TeamService : ITeamService
    {
        private HttpDataService _httpDataService;

        public TeamService()
        {
            _httpDataService = new HttpDataService();
        }

        public async Task<ObservableCollection<Team>> GetAllTeams()
        {
            var result = await _httpDataService.GetAsync<ObservableCollection<Team>>("team", LoginService.AccessToken);
            return result;
        }
    }
}