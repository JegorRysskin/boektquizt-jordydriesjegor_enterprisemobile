using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace JuryApp.Core.Models
{
    public class Quiz : ObservableObject
    {
        private int _quizId;
        private string _quizName;

        [JsonProperty("id")]
        public int QuizId
        {
            get => _quizId;
            set => Set(() => QuizId, ref _quizId, value);
        }

        [JsonProperty("name")]
        public string QuizName
        {
            get => _quizName;
            set => Set(() => QuizName, ref _quizName, value);
        }

    }
}
