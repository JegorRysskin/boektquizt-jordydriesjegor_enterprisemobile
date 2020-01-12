using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class TeamsViewModel : ViewModelBase
    {
        private INavigationServiceEx _navigationService;

        private readonly ITeamService _teamService;

        public Teams Teams { get; set; } = new Teams();

        public RelayCommand<int> EditTeamCommand => new RelayCommand<int>(NavigateToEditTeamPage);

        public TeamsViewModel(ITeamService teamService, INavigationServiceEx navigationService)
        {
            _teamService = teamService;
            _navigationService = navigationService;
            FetchListOfTeams(false);

            _navigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            FetchListOfTeams(true);
        }

        private void NavigateToEditTeamPage(int selectedIndex)
        {
            if (selectedIndex != -1)
            {
                Messenger.Default.Send(Teams[selectedIndex]);
                _navigationService.Navigate(typeof(EditTeamViewModel).FullName);
            }

        }

        private async void FetchListOfTeams(bool forceRefresh)
        {
            var teams = await _teamService.GetAllTeams(forceRefresh);

            Teams.Clear();
            foreach (var team in teams)
            {
                Teams.Add(team);
            }
        }
    }
}
