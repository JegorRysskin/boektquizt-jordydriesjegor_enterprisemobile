using System.Collections.Generic;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models.Collections;
using Newtonsoft.Json;

namespace JuryApp.Core.Models
{
    public class Round : ObservableObject
    {
        private int _roundId;
        private Questions _roundQuestions;
        private bool _roundEnabled;
        private string _roundName;
        private List<int> _roundTeamsIndication;

        [JsonProperty("id")]
        public int RoundId
        {
            get => _roundId;
            set => Set(() => RoundId, ref _roundId, value);
        }

        [JsonProperty("questions")]
        public Questions RoundQuestions
        {
            get => _roundQuestions;
            set => Set(() => RoundQuestions, ref _roundQuestions, value);
        }

        [JsonProperty("enabled")]
        public bool RoundEnabled
        {
            get => _roundEnabled;
            set => Set(() => RoundEnabled, ref _roundEnabled, value);
        }

        [JsonProperty("name")]
        public string RoundName
        {
            get => _roundName;
            set => Set(() => RoundName, ref _roundName, value);
        }

        [JsonProperty("teamIdOpenedRound")]
        public List<int> RoundTeamsIndication
        {
            get => _roundTeamsIndication;
            set => Set(() => RoundTeamsIndication, ref _roundTeamsIndication, value);
        }
    }
}
