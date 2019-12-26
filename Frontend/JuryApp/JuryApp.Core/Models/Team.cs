using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace JuryApp.Core.Models
{
    public class Team : ObservableObject
    {
        private int _teamId;
        private string _teamName;
        private bool _teamEnabled;

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
    }
}