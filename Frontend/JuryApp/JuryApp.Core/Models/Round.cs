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

        [JsonProperty]
        public bool RoundEnabled
        {
            get => _roundEnabled;
            set => Set(() => RoundEnabled, ref _roundEnabled, value);
        }
    }
}
