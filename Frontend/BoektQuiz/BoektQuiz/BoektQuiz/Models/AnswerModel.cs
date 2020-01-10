using BoektQuiz.Util;
using Newtonsoft.Json;
using System;

namespace BoektQuiz.Models
{
    [JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
    public class AnswerModel
    {
        public int Id { get; set; }
        public String AnswerString { get; set; }
        public Question Question { get; set; }
    }
}
