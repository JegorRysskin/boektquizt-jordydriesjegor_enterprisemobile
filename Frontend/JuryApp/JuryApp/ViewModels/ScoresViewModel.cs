using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services;
using System.Linq;

namespace JuryApp.ViewModels
{
    public class ScoresViewModel
    {
        public Teams Teams { get; set; } = new Teams();
        private readonly TeamService _teamService;

        public ScoresViewModel()
        {
            _teamService = new TeamService();
            FetchListOfTeams(true);
        }

        private async void FetchListOfTeams(bool forceRefresh)
        {
            var teams = await _teamService.GetAllTeams(forceRefresh);

            Teams.Clear();
            foreach (var team in teams.OrderByDescending(team => team.TeamScore))
            {
                if (team.TeamEnabled)
                    Teams.Add(team);
            }
        }

    }
}
