using JuryApp.ViewModels;
using Windows.UI.Xaml.Controls;

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
