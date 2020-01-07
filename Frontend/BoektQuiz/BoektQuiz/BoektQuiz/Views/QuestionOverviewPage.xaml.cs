using BoektQuiz.Repositories;
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

            viewModel = new QuestionOverviewViewModel(AppContainer.Resolve<IQuestionRepository>());

            BindingContext = viewModel;
        }
    }
}