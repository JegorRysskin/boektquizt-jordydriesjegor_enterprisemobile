using BoektQuiz.Models;
using BoektQuiz.Repositories;
using BoektQuiz.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class QuestionOverviewViewModel : BaseViewModel
    {
        private IList<Question> _questions;
        public IList<Question> Questions { get => _questions; set { if (_questions == value) return; _questions = value; OnPropertyChanged(); } }

        private IList<QuestionAnswer> _questionAnswers;
        public IList<QuestionAnswer> QuestionAnswers { get => _questionAnswers; set { if (_questionAnswers == value) return; _questionAnswers = value; OnPropertyChanged(); } }

        public Command LoadItemsCommand { get; set; }

        private IQuestionRepository _questionRepository;

        private IAnswerRepository _answerRepository;

        private IBackendService _backendService;

        public Team Team { get; set; }

        public Round Round { get; set; }

        public QuestionOverviewViewModel(IQuestionRepository questionRepository, IAnswerRepository answerRepository, IBackendService backendService)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _backendService = backendService;
            QuestionAnswers = new List<QuestionAnswer>();
            MessagingCenter.Subscribe<RoundOverviewViewModel, Round>(this, "Round", (sender, round) => { 
                Round = round; 
                Questions = round.Questions;
                MatchQuestionWithAnswer();
            });
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Questions = await _questionRepository.GetQuestionsFromRound(Round.Id);
                QuestionAnswers.Clear();
                MatchQuestionWithAnswer();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void MatchQuestionWithAnswer()
        {
            if (Application.Current != null)
            {
                if (Application.Current.Properties.ContainsKey("token"))
                {
                    string token = Application.Current.Properties["token"].ToString();

                    if (token != String.Empty)
                    {
                        Team = _backendService.GetTeamByToken(token).Result;
                    }
                }
            }

            foreach (Question question in Questions)
            {
                Answer answer = _answerRepository.GetAnswerFromQuestion(question.Id).Result;
                QuestionAnswers.Add(new QuestionAnswer() { Question = question, Answer = answer });
            }
        }
    }
}
