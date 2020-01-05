using GalaSoft.MvvmLight;
using JuryApp.Core.Models.Collections;
using Newtonsoft.Json;

namespace JuryApp.Core.Models
{
    public class Team : ObservableObject
    {
        private int _teamId;
        private string _teamName;
        private bool _teamEnabled;
        private double _teamScore;
        private Answers _teamAnswers;

        [JsonProperty("id")]
        public int TeamId
        {
            get => _teamId;
            set => Set(() => TeamId, ref _teamId, value);
        }

        [JsonProperty("name")]
        public string TeamName
        {
            get => _teamName;
            set => Set(() => TeamName, ref _teamName, value);
        }


        [JsonProperty("enabled")]
        public bool TeamEnabled
        {
            get => _teamEnabled;
            set => Set(() => TeamEnabled, ref _teamEnabled, value);
        }

        [JsonProperty("scores")]
        public double TeamScore
        {
            get => _teamScore;
            set => Set(() => TeamScore, ref _teamScore, value);
        }

        [JsonProperty("answers")]
        public Answers TeamAnswers
        {
            get => _teamAnswers;
            set => Set(() => TeamAnswers, ref _teamAnswers, value);
        }
    }
}