using BoektQuiz.Repositories;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoektQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionOverviewPage : ContentPage
    {
        QuestionOverviewViewModel viewModel;

        public QuestionOverviewPage()
        {
            InitializeComponent();

            viewModel = new QuestionOverviewViewModel(AppContainer.Resolve<IQuestionRepository>(), AppContainer.Resolve<IAnswerRepository>(), AppContainer.Resolve<IBackendService>());

            BindingContext = viewModel;
        }
    }
}