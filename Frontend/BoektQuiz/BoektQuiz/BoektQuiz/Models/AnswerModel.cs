using BoektQuiz.Util;
using Newtonsoft.Json;
using System;

namespace BoektQuiz.Models
{
    public class AnswerModel
    {
        [JsonProperty("answerString")]
        public String AnswerString { get; set; }
        [JsonProperty("questionId")]
        public int questionId { get; set; }
    }
}
