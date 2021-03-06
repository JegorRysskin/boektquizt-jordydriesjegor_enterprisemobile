﻿using BoektQuiz.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace BoektQuiz.Models
{
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

        public override bool Equals(object obj)
        {
            return obj is Answer answer &&
                   Id == answer.Id &&
                   QuestionId == answer.QuestionId;
        }
    }
}
