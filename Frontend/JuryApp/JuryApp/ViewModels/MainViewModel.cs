
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

        private readonly QuizService _quizService;

        public Quiz EnabledQuiz { get; set; }

        public MainViewModel()
        {
            _quizService = new QuizService();
            GetEnabledQuiz(true);

            NavigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            GetEnabledQuiz(true);
        }

        private async void GetEnabledQuiz(bool forceRefresh)
        {
            var quizzes = await _quizService.GetAllQuizzes(forceRefresh);

            EnabledQuiz = quizzes.FirstOrDefault(q => q.QuizEnabled);
            RaisePropertyChanged(() => EnabledQuiz);
        }
    }
}
