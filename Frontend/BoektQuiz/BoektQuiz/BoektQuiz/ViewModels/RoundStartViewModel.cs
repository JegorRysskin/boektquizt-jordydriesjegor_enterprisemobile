using BoektQuiz.Models;
using BoektQuiz.Views;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class RoundStartViewModel
    {
        public string Title { get; set; }

        public INavigation Navigation;
        public Round Round { get; set; }

        private Command _startRoundCommand;

        public Command StartRoundCommand =>
            _startRoundCommand ?? (_startRoundCommand = new Command(OnStartRound, CanStartRound));

        public RoundStartViewModel(INavigation navigation, Round round = null)
        {
            Title = round?.Text;
            Round = round;
            Navigation = navigation;
            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

        private async void OnStartRound()
        {
            //Start Round
            await Navigation.PushAsync(new QuestionPage());
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
