
using JuryApp.ViewModels;

using Windows.UI.Xaml.Controls;

namespace JuryApp.Views
{
    public sealed partial class ShellPage : Page
    {
        private ShellViewModel ViewModel => ViewModelLocator.Current.ShellViewModel;

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
        }
    }
}
