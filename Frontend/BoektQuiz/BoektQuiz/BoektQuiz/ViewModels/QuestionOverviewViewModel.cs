using BoektQuiz.Models;
using BoektQuiz.Repositories;
using BoektQuiz.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class QuestionOverviewViewModel : BaseViewModel
    {
        private IList<Question> _questions;
        public IList<Question> Questions { get => _questions; set { if (_questions == value) return; _questions = value; OnPropertyChanged(); } }
        public Command LoadItemsCommand { get; set; }

        private IQuestionRepository _questionRepository;

        public Round Round { get; set; }

        public QuestionOverviewViewModel(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
            MessagingCenter.Subscribe<RoundOverviewViewModel, Round>(this, "Round", (sender, round) => { Round = round; Questions = round.Questions; });
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
    }
}
