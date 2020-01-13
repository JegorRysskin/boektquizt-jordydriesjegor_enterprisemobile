using Newtonsoft.Json;

namespace BoektQuiz.Models
{
    public class TeamModel
    {
        [JsonProperty("teamId")]
        public int TeamId { get; set; }
    }
}
