using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services;
using JuryApp.Core.Services.Interfaces;
using System.Linq;

namespace JuryApp.ViewModels
{
    public class ScoresViewModel
    {
        public Teams Teams { get; set; } = new Teams();
        private readonly ITeamService _teamService;

        public ScoresViewModel(ITeamService teamService)
        {
            _teamService = teamService;
            FetchListOfTeams(true);
        }

        private async void FetchListOfTeams(bool forceRefresh)
        {
            var teams = await _teamService.GetAllTeams(forceRefresh);

            Teams.Clear();
            foreach (var team in teams.OrderByDescending(team => team.TeamScore))
            {
                Teams.Add(team);
            }
        }

    }
}
