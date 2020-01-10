using BoektQuiz.Annotations;
using BoektQuiz.Util;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace BoektQuiz.Models
{
    [JsonObject(NamingStrategyType = typeof(LowercaseNamingStrategy))]
    public class Answer : INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        private string _answer;
        public string AnswerString { get => _answer; set { if (_answer == value) return; _answer = value; OnPropertyChanged(); } }

        public int QuestionId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
