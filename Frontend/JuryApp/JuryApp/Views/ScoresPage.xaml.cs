using JuryApp.ViewModels;
using Windows.UI.Xaml.Controls;

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
