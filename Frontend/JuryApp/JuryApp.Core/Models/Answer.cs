using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace JuryApp.Core.Models
{
    public class Answer : ObservableObject
    {
        private int _answerId;
        private string _answerText;
        private Question _answerQuestion;

        [JsonProperty("id")]
        public int AnswerId
        {
            get => _answerId;
            set => Set(() => AnswerId, ref _answerId, value);
        }

        [JsonProperty("answerString")]
        public string AnswerText
        {
            get => _answerText;
            set => Set(() => AnswerText, ref _answerText, value);
        }

        [JsonProperty("question")]
        public Question AnswerQuestion
        {
            get => _answerQuestion;
            set => Set(() => AnswerQuestion, ref _answerQuestion, value);
        }
    }
}
