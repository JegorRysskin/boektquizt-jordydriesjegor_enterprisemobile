using BoektQuiz.Models;
using BoektQuiz.Repositories;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoektQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundEndPage : ContentPage
    {
        RoundEndViewModel viewModel;

        public RoundEndPage()
        {
            InitializeComponent();

            viewModel = new RoundEndViewModel(AppContainer.Resolve<INavigationService>(), AppContainer.Resolve<IRoundRepository>(), AppContainer.Resolve<IBackendService>());
            
            BindingContext = viewModel;
        }
    }
}