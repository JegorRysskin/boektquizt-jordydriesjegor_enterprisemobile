using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationServiceEx _navigationService;
        private readonly IRoundService _roundService;
        private readonly ITeamService _teamService;

        public Rounds Rounds { get; set; } = new Rounds();
        public string SelectionMode { get; set; } = "Single";
        public Teams TeamsBySelectedRound { get; set; } = new Teams();

        public MainViewModel(ITeamService teamService, IRoundService roundService, INavigationServiceEx navigationService)
        {
            _teamService = teamService;
            _roundService = roundService;
            _navigationService = navigationService;
            GetRoundsFromEnabledQuiz();

            _navigationService.Navigated += NavigationService_Navigated;
        }

        public RelayCommand<Round> EnableRoundCommand => new RelayCommand<Round>(EnableRound);
        public RelayCommand DisableAllRoundsCommand => new RelayCommand(DisableAllRounds);
        public RelayCommand<Round> GetTeamsOfSelectedRoundCommand => new RelayCommand<Round>(FetchListOfTeamsByRound);
        public RelayCommand<Round> RefreshTeamsCommand => new RelayCommand<Round>(RefreshTeams);

        private void RefreshTeams(Round selectedRound)
        {
            GetRoundsFromEnabledQuiz();
        }

        private void DisableAllRounds()
        {
            if (!Rounds.Any(r => r.RoundEnabled)) return;

            TeamsBySelectedRound.Clear();

            SelectionMode = "None";
            RaisePropertyChanged(() => SelectionMode);

            Rounds.ToList().ForEach(async r =>
            {
                r.RoundEnabled = false;
                await _roundService.EditRound(r.RoundId, r);
            });

            SelectionMode = "Single";
            RaisePropertyChanged(() => SelectionMode);
        }

        private void EnableRound(Round selectedRound)
        {
            if (selectedRound == null) return;

            Rounds.ToList().ForEach(async r =>
            {
                r.RoundEnabled = r.RoundId == selectedRound.RoundId;
                await _roundService.EditRound(r.RoundId, r);
            });

        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            GetRoundsFromEnabledQuiz();
        }

        private async void GetRoundsFromEnabledQuiz()
        {
            Rounds = await _roundService.GetAllRoundsByEnabledQuiz();
            RaisePropertyChanged(() => Rounds);
        }

        private async void FetchListOfTeamsByRound(Round selectedRound)
        {
            if (selectedRound == null) return;

            var teams = await _teamService.GetAllTeams();

            TeamsBySelectedRound.Clear();

            teams.ToList().Where(t => selectedRound.RoundTeamsIndication.Contains(t.TeamId)).ToList().ForEach(t => TeamsBySelectedRound.Add(t));

        }
    }
}
