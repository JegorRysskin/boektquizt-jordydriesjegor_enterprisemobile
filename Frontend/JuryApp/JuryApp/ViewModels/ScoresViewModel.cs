using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services;
using JuryApp.Core.Services.Interfaces;
using System.Linq;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class ScoresViewModel
    {
        private readonly INavigationServiceEx _navigationService;

        public Teams Teams { get; set; } = new Teams();
        private readonly ITeamService _teamService;

        public ScoresViewModel(ITeamService teamService, INavigationServiceEx navigationService)
        {
            _navigationService = navigationService;
            _teamService = teamService;
            FetchListOfTeams();

            _navigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            FetchListOfTeams();
        }

        private async void FetchListOfTeams()
        {
            var teams = await _teamService.GetAllTeams();

            Teams.Clear();
            foreach (var team in teams.OrderByDescending(team => team.TeamScore))
            {
                if (team.TeamEnabled)
                    Teams.Add(team);
            }
        }

    }
}
