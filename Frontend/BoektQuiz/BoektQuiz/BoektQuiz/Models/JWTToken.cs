using System;
using System.Collections.Generic;
using System.Text;

namespace BoektQuiz.Models
{
    public class JWTToken
    {
        public String Sub { get; set; }
        public String Scopes { get; set; }
        public int Iat { get; set; }
        public int Exp { get; set; }
    }
}
