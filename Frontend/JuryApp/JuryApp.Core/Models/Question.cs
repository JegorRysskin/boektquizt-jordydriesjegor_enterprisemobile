using System.Collections.Generic;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models.Collections;
using Newtonsoft.Json;

namespace JuryApp.Core.Models
{
    public class Question : ObservableObject
    {
        private int _questionId;
        private string _questionText;
        private List<string> _questionCorrectAnswers;

        [JsonProperty("id")]
        public int QuestionId
        {
            get => _questionId;
            set => Set(() => QuestionId, ref _questionId, value);
        }

        [JsonProperty("question")]
        public string QuestionText
        {
            get => _questionText;
            set => Set(() => QuestionText, ref _questionText, value);
        }

        [JsonProperty("correctAnswerToQuestion")]
        public List<string> QuestionCorrectAnswers
        {
            get => _questionCorrectAnswers;
            set => Set(() => QuestionCorrectAnswers, ref _questionCorrectAnswers, value);
        }
    }
}
