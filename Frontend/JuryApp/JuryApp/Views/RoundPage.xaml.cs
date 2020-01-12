using JuryApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace JuryApp.Views
{
    public sealed partial class RoundPage : Page
    {
        private RoundViewModel ViewModel => ViewModelLocator.Current.RoundViewModel;
        public RoundPage()
        {
            InitializeComponent();
        }
    }
}
