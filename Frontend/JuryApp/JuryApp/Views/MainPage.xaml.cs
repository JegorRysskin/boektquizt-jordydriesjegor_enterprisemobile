
using JuryApp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace JuryApp.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel => ViewModelLocator.Current.MainViewModel;

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
