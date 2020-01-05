using BoektQuiz.Models;
using BoektQuiz.Repositories;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.Views;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class RoundEndViewModel : BaseViewModel
    {
        public Round _round;
        public Round Round {
            get => _round; 
            set
            {
                if (_round == value) return;
                _round = value;
                OnPropertyChanged();
            } 
        }

        private INavigationService _navigationService;

        private IRoundRepository _roundRepository;

        private IBackendService _backendService;

        private Command _endRoundCommand;

        public Command EndRoundCommand =>
            _endRoundCommand ?? (_endRoundCommand = new Command(OnEndRound, CanEndRound));

        public RoundEndViewModel(INavigationService navigationService, IRoundRepository roundRepository, IBackendService backendService)
        {
            MessagingCenter.Instance.Subscribe<QuestionViewModel, Round>(this, "Round", (sender, round) => { Round = round; });
            _navigationService = navigationService;
            _roundRepository = roundRepository;
            _backendService = backendService;
            Connectivity.Instance.ConnectivityChanged += HandleConnectivityChanged;
        }

        private async void OnEndRound()
        {
            if (Round != null)
            {
                MessagingCenter.Instance.Unsubscribe<QuestionViewModel, Round>(this, "Round");
                await _navigationService.ReturnToRoot();
                await _roundRepository.UpdateRoundAsync(Round);
                string token = Application.Current.Properties["token"].ToString();
                await _backendService.PatchRound(Round, token);
            }
        }

        public bool CanEndRound()
        {
            return Connectivity.Instance.IsConnected;
        }

        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            EndRoundCommand.ChangeCanExecute();
        }
    }
}
