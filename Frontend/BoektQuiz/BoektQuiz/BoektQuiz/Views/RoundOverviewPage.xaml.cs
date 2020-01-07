using BoektQuiz.Repositories;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoektQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundOverviewPage : ContentPage
    {
        RoundOverviewViewModel viewModel;

        public RoundOverviewPage()
        {
            InitializeComponent();

            viewModel = new RoundOverviewViewModel(AppContainer.Resolve<INavigationService>(), AppContainer.Resolve<IRoundRepository>());

            BindingContext = viewModel;
        }
    }
}