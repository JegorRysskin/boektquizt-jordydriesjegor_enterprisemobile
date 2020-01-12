
using GalaSoft.MvvmLight.Ioc;
using JuryApp.Core.Services;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Helpers;
using JuryApp.Services;
using JuryApp.Views;

namespace JuryApp.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        private static ViewModelLocator _current;

        public static ViewModelLocator Current => _current ?? (_current = new ViewModelLocator());

        private ViewModelLocator()
        {
            SimpleIoc.Default.Register<IMessengerCache, MessengerCache>();
            MessengerCache = SimpleIoc.Default.GetInstance<IMessengerCache>();

            SimpleIoc.Default.Register<INavigationServiceEx, NavigationServiceEx>();

            SimpleIoc.Default.Register<IQuizService, QuizService>();
            SimpleIoc.Default.Register<IRoundService, RoundService>();
            SimpleIoc.Default.Register<ITeamService, TeamService>();

            SimpleIoc.Default.Register<ShellViewModel>();
            Register<MainViewModel, MainPage>();
            Register<QuizzenViewModel, QuizzenPage>();
            Register<TeamsViewModel, TeamsPage>();
            Register<EditQuizViewModel, EditQuizPage>();
            Register<CreateQuizViewModel, CreateQuizPage>();
            Register<EditTeamViewModel, EditTeamPage>();
            Register<ScoresViewModel, ScoresPage>();
            Register<RoundViewModel, RoundPage>();
            Register<CorrectViewModel, CorrectPage>();
        }

        public CorrectViewModel CorrectViewModel => SimpleIoc.Default.GetInstance<CorrectViewModel>();

        public RoundViewModel RoundViewModel => SimpleIoc.Default.GetInstance<RoundViewModel>();

        public ScoresViewModel ScoresViewModel => SimpleIoc.Default.GetInstance<ScoresViewModel>();

        public CreateQuizViewModel CreateQuizViewModel => SimpleIoc.Default.GetInstance<CreateQuizViewModel>();

        public EditQuizViewModel EditQuizViewModel => SimpleIoc.Default.GetInstance<EditQuizViewModel>();

        public TeamsViewModel TeamsViewModel => SimpleIoc.Default.GetInstance<TeamsViewModel>();

        public EditTeamViewModel EditTeamViewModel => SimpleIoc.Default.GetInstance<EditTeamViewModel>();

        public QuizzenViewModel QuizzenViewModel => SimpleIoc.Default.GetInstance<QuizzenViewModel>();

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public ShellViewModel ShellViewModel => SimpleIoc.Default.GetInstance<ShellViewModel>();

        public INavigationServiceEx NavigationService => SimpleIoc.Default.GetInstance<INavigationServiceEx>();

        public IMessengerCache MessengerCache;

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
