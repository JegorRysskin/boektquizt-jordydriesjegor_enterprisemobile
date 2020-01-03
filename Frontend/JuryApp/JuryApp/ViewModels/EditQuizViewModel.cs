using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using JuryApp.Services;
using System;
using Windows.UI.Xaml.Media;
using JuryApp.Helpers;

namespace JuryApp.ViewModels
{
    public class EditQuizViewModel : ViewModelBase
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;
        private MessengerCache MessengerCache => ViewModelLocator.Current.MessengerCache;

        public Quiz SelectedQuiz { get; set; }
        private readonly QuizService _quizService;

        public EditQuizViewModel()
        {
            _quizService = new QuizService();

            SelectedQuiz = MessengerCache.CachedSelectedQuiz;
            Messenger.Default.Register<Quiz>(this, (quiz) => { SelectedQuiz = quiz; });
        }

        public RelayCommand DeleteQuizCommand => new RelayCommand(DeleteQuiz);
        public RelayCommand EditQuizCommand => new RelayCommand(EditQuiz);

        private async void EditQuiz()
        {
            var result = await _quizService.EditQuiz(SelectedQuiz.QuizId, SelectedQuiz);

            if (result)
            {
                NavigationService.GoBack();
            }
        }

        private async void DeleteQuiz()
        {
            var result = await _quizService.DeleteQuiz(SelectedQuiz.QuizId);

            if (result)
            {
                NavigationService.Navigate(typeof(QuizzenViewModel).FullName);
            }
        }
    }
}
