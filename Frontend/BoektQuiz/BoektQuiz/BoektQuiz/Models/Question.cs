using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BoektQuiz.Annotations;

namespace BoektQuiz.Models
{
    public class Question : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Text { get; set; }
        private string _answer;

        public string Answer
        {
            get => _answer;
            set
            {
                if (_answer == value) return;
                _answer = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}