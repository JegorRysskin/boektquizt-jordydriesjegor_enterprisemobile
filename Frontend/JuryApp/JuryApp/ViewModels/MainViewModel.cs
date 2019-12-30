
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private readonly QuizService _quizService;

        public Quizzes EnabledQuizzes { get; set; } = new Quizzes();

        public MainViewModel()
        {
            _quizService = new QuizService();
            FetchListOfEnabledQuizzes(false);

            NavigationService.Navigated += NavigationService_Navigated;
        }

        public RelayCommand<int> RoundControlCommand => new RelayCommand<int>(NavigateToRoundPage);

        private void NavigateToRoundPage(int selectedIndex)
        {
            if (selectedIndex != -1)
            {
                Messenger.Default.Send(EnabledQuizzes[selectedIndex]);
                NavigationService.Navigate(typeof(RoundsViewModel).FullName);
            }
        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            FetchListOfEnabledQuizzes(true);
        }

        private async void FetchListOfEnabledQuizzes(bool forceRefresh)
        {
            var quizzes = await _quizService.GetAllQuizzes(forceRefresh);

            EnabledQuizzes.Clear();
            foreach (var quiz in quizzes)
            {
                if (quiz.QuizEnabled)
                {
                    EnabledQuizzes.Add(quiz);
                }
            }
        }
    }
}
