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

        private string _token;

        private Command _startRoundCommand;

        public Command StartRoundCommand =>
            _startRoundCommand ?? (_startRoundCommand = new Command(OnStartRound, CanStartRound));

        public RoundStartViewModel(INavigationService navigationService, IBackendService backendService, int id)
        {
            if (Application.Current != null)
            {
                if (Application.Current.Properties.ContainsKey("token"))
                {
                    _token = Application.Current.Properties["token"].ToString();
                } 
            }
            Round = backendService.GetRoundById(id, _token).Result;
            _navigationService = navigationService;
            Connectivity.Instance.ConnectivityChanged += HandleConnectivityChanged;
        }

        private async void OnStartRound()
        {
            //Start Round
            await _navigationService.NavigateToAsync(RoutingConstants.QuestionRoute);
            MessagingCenter.Instance.Send(this, "Round", Round);
        }

        public bool CanStartRound()
        {
            return !Connectivity.Instance.IsConnected;
        }

        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            StartRoundCommand.ChangeCanExecute();
        }
    }
}
