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
        public bool AlreadyOneEnabledQuiz { get; set; }
        private readonly QuizService _quizService;

        public EditQuizViewModel()
        {
            _quizService = new QuizService();

            SelectedQuiz = MessengerCache.CachedSelectedQuiz;
            Messenger.Default.Register<Quiz>(this, (quiz) => { SelectedQuiz = quiz; });
            AlreadyOneEnabledQuiz = MessengerCache.CachedAlreadyOneEnabledQuiz;
            Messenger.Default.Register<bool>(this, (b) => { AlreadyOneEnabledQuiz = b; });
        }


        public RelayCommand DeleteQuizCommand => new RelayCommand(DeleteQuiz);
        public RelayCommand EditQuizCommand => new RelayCommand(EditQuiz);
        public RelayCommand<Round> NavigateToRoundCommand => new RelayCommand<Round>(NavigateToRound);

        private void NavigateToRound(Round selectedRound)
        {
            if (selectedRound == null) return;

            Messenger.Default.Send(selectedRound);
            NavigationService.Navigate(typeof(RoundViewModel).FullName);
        }

        private async void EditQuiz()
        {
            if (AlreadyOneEnabledQuiz)
                SelectedQuiz.QuizEnabled = false;
            
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
                NavigationService.GoBack();
            }
        }
    }
}
