using BoektQuiz.Models;
using BoektQuiz.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BoektQuiz.ViewModels
{
    public class QuestionOverviewViewModel : BaseViewModel
    {
        private IList<Question> _questions;
        public IList<Question> Questions { get => _questions; set { if (_questions == value) return; _questions = value; OnPropertyChanged(); } }

        public QuestionOverviewViewModel()
        {
            MessagingCenter.Subscribe<RoundOverviewViewModel, Round>(this, "Round", (sender, round) => { Questions = round.Questions; Console.WriteLine(Questions); });
        }
    }
}
