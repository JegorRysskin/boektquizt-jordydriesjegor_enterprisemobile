using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models.Collections;
using Newtonsoft.Json;

namespace JuryApp.Core.Models
{
    public class Question : ObservableObject
    {
        private int _questionId;
        private Answers _questionAnswers;

        [JsonProperty("id")]
        public int QuestionId
        {
            get => _questionId;
            set => Set(() => QuestionId, ref _questionId, value);
        }

        [JsonProperty]
        public Answers QuestionAnswers
        {
            get => _questionAnswers;
            set => Set(() => QuestionAnswers, ref _questionAnswers, value);

        }
    }
}
