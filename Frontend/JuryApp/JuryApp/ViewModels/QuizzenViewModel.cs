using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class QuizzenViewModel : ViewModelBase
    {
        private readonly INavigationServiceEx _navigationService;

        private readonly IQuizService _quizService;

        public Quizzes Quizzes { get; set; } = new Quizzes();

        public RelayCommand CreateQuizCommand => new RelayCommand(NavigateToCreateQuizPage);
        public RelayCommand<int> EditQuizCommand => new RelayCommand<int>(NavigateToEditQuizPage);


        public QuizzenViewModel(IQuizService quizService, INavigationServiceEx navigationService)
        {
            _quizService = quizService;
            _navigationService = navigationService;
            FetchListOfQuizzes();

            _navigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            FetchListOfQuizzes();
        }

        private void NavigateToCreateQuizPage()
        {
            _navigationService.Navigate(typeof(CreateQuizViewModel).FullName);
        }

        private void NavigateToEditQuizPage(int selectedIndex)
        {
            if (selectedIndex == -1) return;

            Messenger.Default.Send(Quizzes[selectedIndex]);
            Messenger.Default.Send(Quizzes);
            _navigationService.Navigate(typeof(EditQuizViewModel).FullName);
        }

        private async void FetchListOfQuizzes()
        {
            var quizzes = await _quizService.GetAllQuizzes();

            Quizzes.Clear();
            foreach (var quiz in quizzes)
            {
                Quizzes.Add(quiz);
            }
        }
    }
}
