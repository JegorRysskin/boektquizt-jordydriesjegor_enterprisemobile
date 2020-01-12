using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Services;
using System.Linq;

namespace JuryApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationServiceEx _navigationService;
        private readonly IRoundService _roundService;

        public Rounds Rounds { get; set; }
        public string SelectionMode { get; set; } = "Single";

        public MainViewModel(IRoundService roundService, INavigationServiceEx navigationService)
        {
            _roundService = roundService;
            _navigationService = navigationService;
            GetRoundsFromEnabledQuiz(true);

            _navigationService.Navigated += NavigationService_Navigated;
        }

        public RelayCommand<Round> EnableRoundCommand => new RelayCommand<Round>(EnableRounds);
        public RelayCommand DisableAllRoundsCommand => new RelayCommand(DisableAllRounds);

        private void DisableAllRounds()
        {
            if (!Rounds.Any(r => r.RoundEnabled)) return;

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

        private void EnableRounds(Round selectedRound)
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
            GetRoundsFromEnabledQuiz(true);
        }

        private async void GetRoundsFromEnabledQuiz(bool forceRefresh)
        {
            Rounds = await _roundService.GetAllRoundsByEnabledQuiz(forceRefresh);
            RaisePropertyChanged(() => Rounds);

        }
    }
}
