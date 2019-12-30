using JuryApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace JuryApp.Views
{
    public sealed partial class EditQuizPage : Page
    {
        private EditQuizViewModel ViewModel => ViewModelLocator.Current.EditQuizViewModel;

        public EditQuizPage()
        {
            InitializeComponent();
        }
    }
}
