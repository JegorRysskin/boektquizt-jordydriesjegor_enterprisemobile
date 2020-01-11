using BoektQuiz.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoektQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel viewModel;

        public HomePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new HomeViewModel();
        }

        private void HomePage_Appearing(object sender, System.EventArgs e) //Auto-Update Status Text when HomePage is displayed
        {
            viewModel.UpdateStatusCommand.Execute(null);
        }
    }
}