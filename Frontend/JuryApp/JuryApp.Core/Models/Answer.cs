using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace JuryApp.Core.Models
{
    public class Answer : ObservableObject
    {
        private int _answerId;
        private string _answerText;

        [JsonProperty("id")]
        public int AnswerId
        {
            get => _answerId;
            set => Set(() => AnswerId, ref _answerId, value);
        }

        [JsonProperty]
        public string AnswerText
        {
            get => _answerText;
            set => Set(() => AnswerText, ref _answerText, value);
        }
    }
}
