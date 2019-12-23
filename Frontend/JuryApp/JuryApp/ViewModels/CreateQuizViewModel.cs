using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection.Metadata;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    public class CreateQuizViewModel : ViewModelBase
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public Quiz NewQuiz { get; set; }

        private readonly QuizService _quizService;

        public RelayCommand CreateNewQuizCommand => new RelayCommand(CreateNewQuiz);

        public CreateQuizViewModel()
        {
            _quizService = new QuizService();
            NewQuiz = new Quiz();
        }

        private async void CreateNewQuiz()
        {
            var result = await _quizService.AddQuiz(NewQuiz);

            if (result)
            {
                NavigationService.Navigate(typeof(QuizzenViewModel).FullName);
            }
        }
    }
}
