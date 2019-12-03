using System;
using System.Collections.Generic;
using System.Text;
using BoektQuiz.ViewModels;
using Xamarin.Forms;

namespace BoektQuiz.Behavior
{
    public class EntryValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool isEntryFilledIn = ((Entry) sender).Text.Length > 0;
            var bC = ((Entry) sender).BindingContext;
            if (bC is QuestionViewModel vM)
            {
                vM.IsEntryFilledIn = isEntryFilledIn;
                vM.SendAnswerCommand.ChangeCanExecute();
            }
        }
    }
}