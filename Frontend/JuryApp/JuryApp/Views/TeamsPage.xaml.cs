using JuryApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace JuryApp.Views
{
    public sealed partial class TeamsPage : Page
    {
        private TeamsViewModel ViewModel => ViewModelLocator.Current.TeamsViewModel;

        public TeamsPage()
        {
            InitializeComponent();
        }
    }
}
