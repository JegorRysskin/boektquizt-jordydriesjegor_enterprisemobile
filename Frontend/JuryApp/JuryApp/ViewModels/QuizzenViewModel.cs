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
    public class QuizzenViewModel : ViewModelBase
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private readonly QuizService _quizService;

        public Quizzes Quizzes { get; set; } = new Quizzes();

        public RelayCommand CreateQuizCommand => new RelayCommand(NavigateToCreateQuizPage);
        public RelayCommand<int> EditQuizCommand => new RelayCommand<int>(NavigateToEditQuizPage);


        public QuizzenViewModel()
        {
            _quizService = new QuizService();
            FetchListOfQuizzes(false);

            NavigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            FetchListOfQuizzes(true);
        }

        private void NavigateToCreateQuizPage()
        {
            NavigationService.Navigate(typeof(CreateQuizViewModel).FullName);
        }

        private void NavigateToEditQuizPage(int selectedIndex)
        {
            if (selectedIndex == -1) return;

            Messenger.Default.Send(Quizzes[selectedIndex]);
            Messenger.Default.Send(Quizzes);
            NavigationService.Navigate(typeof(EditQuizViewModel).FullName);
        }

        private async void FetchListOfQuizzes(bool forceRefresh)
        {
            var quizzes = await _quizService.GetAllQuizzes(forceRefresh);

            Quizzes.Clear();
            foreach (var quiz in quizzes)
            {
                Quizzes.Add(quiz);
            }
        }
    }
}
