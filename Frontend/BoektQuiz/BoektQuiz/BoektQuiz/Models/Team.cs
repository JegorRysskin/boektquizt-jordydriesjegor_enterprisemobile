using BoektQuiz.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoektQuiz.Models
{
    [JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Answer> Answers { get; set; }
        public Double Scores { get; set; }
        public bool Enabled { get; set; }
    }
}
