using BoektQuiz.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoektQuiz.Models
{
    [JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
