﻿using BoektQuiz.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoektQuiz.Models
{
    [JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
    public class Round
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public List<Question> Questions { get; set; }
    }
}
