using JuryApp.ViewModels;
using Windows.UI.Xaml.Controls;


namespace JuryApp.Views
{
    public sealed partial class CreateQuizPage : Page
    {
        private CreateQuizViewModel ViewModel
        {
            get { return ViewModelLocator.Current.CreateQuizViewModel; }
        }
        public CreateQuizPage()
        {
            InitializeComponent();
        }
    }
}
