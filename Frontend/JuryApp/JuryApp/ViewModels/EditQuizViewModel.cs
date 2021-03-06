﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using JuryApp.Core.Models;
using JuryApp.Core.Services;
using JuryApp.Services;
using System;
using System.Linq;
using JuryApp.Core.Models.Collections;
using JuryApp.Helpers;
using JuryApp.Core.Services.Interfaces;

namespace JuryApp.ViewModels
{
    public class EditQuizViewModel : ViewModelBase
    {
        private INavigationServiceEx _navigationService;
        private IMessengerCache _messengerCache;

        public Quiz SelectedQuiz { get; set; }
        public Quizzes AllQuizzes { get; set; } = new Quizzes();
        private readonly IQuizService _quizService;

        public EditQuizViewModel(IQuizService quizService, INavigationServiceEx navigationServiceEx, IMessengerCache messengerCache)
        {
            _quizService = quizService;
            _navigationService = navigationServiceEx;
            _messengerCache = messengerCache;

            SelectedQuiz = _messengerCache.CachedSelectedQuiz;
            Messenger.Default.Register<Quiz>(this, (quiz) => { SelectedQuiz = quiz; });
            FetchListOfQuizzes();
        }


        public RelayCommand DeleteQuizCommand => new RelayCommand(DeleteQuiz);
        public RelayCommand EditQuizCommand => new RelayCommand(EditQuiz);
        public RelayCommand<Round> NavigateToRoundCommand => new RelayCommand<Round>(NavigateToRound);

        private void NavigateToRound(Round selectedRound)
        {
            if (selectedRound == null) return;

            Messenger.Default.Send(selectedRound);
            _navigationService.Navigate(typeof(RoundViewModel).FullName);
        }

        private async void EditQuiz()
        {
            var result = await _quizService.EditQuiz(SelectedQuiz.QuizId, SelectedQuiz);
            DisableOtherQuizzes();

            if (result)
            {
                _navigationService.GoBack();
            }
        }

        private async void DeleteQuiz()
        {
            var result = await _quizService.DeleteQuiz(SelectedQuiz.QuizId);

            if (result)
            {
                _navigationService.GoBack();
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

        private async void FetchListOfQuizzes()
        {
            var quizzes = await _quizService.GetAllQuizzes();

            AllQuizzes.Clear();
            foreach (var quiz in quizzes)
            {
                AllQuizzes.Add(quiz);
            }
        }
    }
}
