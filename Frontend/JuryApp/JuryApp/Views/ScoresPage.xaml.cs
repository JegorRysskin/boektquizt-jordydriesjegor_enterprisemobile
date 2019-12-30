using Windows.UI.Xaml.Controls;
using JuryApp.ViewModels;

namespace JuryApp.Views
{
    public sealed partial class ScoresPage : Page
    {
        private ScoresViewModel ViewModel => ViewModelLocator.Current.ScoresViewModel;

        public ScoresPage()
        {
            InitializeComponent();
        }
    }
}
