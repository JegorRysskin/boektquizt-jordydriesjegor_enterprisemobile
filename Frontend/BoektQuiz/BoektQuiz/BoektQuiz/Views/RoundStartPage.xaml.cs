using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoektQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundStartPage : ContentPage
    {
        RoundStartViewModel viewModel;

        public RoundStartPage(int id = 0)
        {
            InitializeComponent();

            viewModel = new RoundStartViewModel(AppContainer.Resolve<INavigationService>(), AppContainer.Resolve<IBackendService>(), id);

            BindingContext = viewModel;
        }

        private void RoundStartPage_Appearing(object sender, System.EventArgs e)
        {
            viewModel.ReloadRoundCommand.Execute(null);
            viewModel.StartRoundCommand.ChangeCanExecute();
        }
    }
}