using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using JuryApp.Services;
using System;
using System.Linq;
using Windows.UI.Xaml.Media;
using JuryApp.Core.Models.Collections;
using JuryApp.Helpers;

namespace JuryApp.ViewModels
{
    public class EditQuizViewModel : ViewModelBase
    {
        private NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;
        private MessengerCache MessengerCache => ViewModelLocator.Current.MessengerCache;

        public Quiz SelectedQuiz { get; set; }
        public Quizzes AllQuizzes { get; set; } = new Quizzes();
        private readonly QuizService _quizService;

        public EditQuizViewModel()
        {
            _quizService = new QuizService();

            SelectedQuiz = MessengerCache.CachedSelectedQuiz;
            Messenger.Default.Register<Quiz>(this, (quiz) => { SelectedQuiz = quiz; });
            FetchListOfQuizzes(true);
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
            var result = await _quizService.EditQuiz(SelectedQuiz.QuizId, SelectedQuiz);
            DisableOtherQuizzes();

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

        private void DisableOtherQuizzes()
        {
            AllQuizzes.Where(q => q.QuizId != SelectedQuiz.QuizId).ToList().ForEach(q => q.QuizEnabled = false);

            AllQuizzes.ToList().ForEach(async q =>
            {
                if (q.QuizId != SelectedQuiz.QuizId)
                {
                    await _quizService.EditQuiz(q.QuizId, q);
                }
            });
        }

        private async void FetchListOfQuizzes(bool forceRefresh)
        {
            var quizzes = await _quizService.GetAllQuizzes(forceRefresh);

            AllQuizzes.Clear();
            foreach (var quiz in quizzes)
            {
                AllQuizzes.Add(quiz);
            }
        }
    }
}
