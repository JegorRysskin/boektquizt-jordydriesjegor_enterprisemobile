using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.Views;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class RoundStartViewModel
    {
        private INavigationService _navigationService;
        public Round Round { get; set; }

        private Command _startRoundCommand;

        private MockDataStore dataStore = new MockDataStore();

        public Command StartRoundCommand =>
            _startRoundCommand ?? (_startRoundCommand = new Command(OnStartRound, CanStartRound));

        public RoundStartViewModel(INavigationService navigationService, int id)
        {
            Round = dataStore.GetRoundAsync(id).Result;
            _navigationService = navigationService;
            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

        private async void OnStartRound()
        {
            //Start Round
            await _navigationService.NavigateToAsync(RoutingConstants.QuestionRoute);
            MessagingCenter.Instance.Send(this, "Round", Round);
        }

        public bool CanStartRound()
        {
            return !CrossConnectivity.Current.IsConnected;
        }

        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            StartRoundCommand.ChangeCanExecute();
        }
    }
}
