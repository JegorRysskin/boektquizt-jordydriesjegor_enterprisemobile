using JuryApp.ViewModels;
using Windows.UI.Xaml.Controls;


namespace JuryApp.Views
{
    public sealed partial class RoundsPage : Page
    {
        private RoundsViewModel ViewModel => ViewModelLocator.Current.RoundsViewModel;
        public RoundsPage()
        {
            InitializeComponent();
        }
    }
}
