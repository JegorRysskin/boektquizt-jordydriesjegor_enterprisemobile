using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models.Collections;
using Newtonsoft.Json;

namespace JuryApp.Core.Models
{
    public class Round : ObservableObject
    {
        private int _roundId;
        private Questions _roundQuestions;

        [JsonProperty("id")]
        public int RoundId
        {
            get => _roundId;
            set => Set(() => RoundId, ref _roundId, value);
        }

        [JsonProperty]
        public Questions RoundQuestions
        {
            get => _roundQuestions;
            set => Set(() => RoundQuestions, ref _roundQuestions, value);
        }
    }
}
