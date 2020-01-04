
using System;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;
        private readonly RoundService _roundService;

        public Rounds Rounds { get; set; }

        public MainViewModel()
        {
            _roundService = new RoundService();
            GetRoundsFromEnabledQuiz(true);

            NavigationService.Navigated += NavigationService_Navigated;
        }

        public RelayCommand<Round> EnableRoundCommand => new RelayCommand<Round>(EnableRounds);

        private void EnableRounds(Round selectedRound)
        {
            foreach (var round in Rounds)
            {
                round.RoundEnabled = round.RoundId == selectedRound.RoundId;
                UpdateRound(round);
            }

        }

        private async void UpdateRound(Round toBeUpdatedRound)
        {
            await _roundService.EditRound(toBeUpdatedRound.RoundId, toBeUpdatedRound);
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
