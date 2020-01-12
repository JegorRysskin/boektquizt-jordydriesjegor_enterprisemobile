using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Services;
using System.Linq;

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
            FetchListOfTeams(true);

            _navigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
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
