using GalaSoft.MvvmLight;
using JuryApp.Core.Models.Collections;
using Newtonsoft.Json;

namespace JuryApp.Core.Models
{
    public class Quiz : ObservableObject
    {
        private int _quizId;
        private string _quizName;
        private bool _quizEnabled;
        private Rounds _quizRounds;

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

        [JsonProperty("enabled")]
        public bool QuizEnabled
        {
            get => _quizEnabled;
            set => Set(() => QuizEnabled, ref _quizEnabled, value);
        }

        [JsonProperty("rounds")]
        public Rounds QuizRounds
        {
            get => _quizRounds;
            set => Set(() => QuizRounds, ref _quizRounds, value);
        }
    }
}
