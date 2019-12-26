using Windows.UI.Xaml.Controls;
using JuryApp.ViewModels;

namespace JuryApp.Views
{
    public sealed partial class EditTeamPage : Page
    {
        private EditTeamViewModel ViewModel => ViewModelLocator.Current.EditTeamViewModel;

        public EditTeamPage()
        {
            InitializeComponent();
        }
    }
}
