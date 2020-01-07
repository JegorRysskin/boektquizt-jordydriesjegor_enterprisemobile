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
            var bC = ((Entry) sender).BindingContext;
            if (bC is QuestionViewModel qVM)
            {
                qVM.SendAnswerCommand.ChangeCanExecute();
            }

            if (bC is RegisterViewModel rVM)
            {
                rVM.RegisterTeamCommand.ChangeCanExecute();
            }

            if (bC is LoginViewModel lVM)
            {
                lVM.LoginCommand.ChangeCanExecute();
            }
        }
    }
}