using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace JuryApp.Core.Models
{
    public class CorrectAnswer : ObservableObject
    {
        private int _correctAnswerId;
        private string _correctAnswerText;

        [JsonProperty("id")]
        public int CorrectAnswerId
        {
            get => _correctAnswerId;
            set => Set(() => CorrectAnswerId, ref _correctAnswerId, value);
        }

        [JsonProperty("answer")]
        public string CorrectAnswerText
        {
            get => _correctAnswerText;
            set => Set(() => CorrectAnswerText, ref _correctAnswerText, value);
        }
    }
}
