using JuryApp.ViewModels;
using Windows.UI.Xaml.Controls;


namespace JuryApp.Views
{
    public sealed partial class CorrectPage : Page
    {
        private CorrectViewModel ViewModel => ViewModelLocator.Current.CorrectViewModel;

        public CorrectPage()
        {
            InitializeComponent();
        }
    }
}
