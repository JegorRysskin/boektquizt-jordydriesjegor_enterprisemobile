
using GalaSoft.MvvmLight.Ioc;
using JuryApp.Core.Services;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Helpers;
using JuryApp.Services;

namespace JuryApp.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        private static ViewModelLocator _current;

        public static ViewModelLocator Current => _current ?? (_current = new ViewModelLocator());

        private ViewModelLocator()
        {
            SimpleIoc.Default.Register<INavigationServiceEx, NavigationServiceEx>();

            SimpleIoc.Default.Register<ShellViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<QuizzenViewModel>();
            SimpleIoc.Default.Register<TeamsViewModel>();
            SimpleIoc.Default.Register<EditQuizViewModel>();
            SimpleIoc.Default.Register<CreateQuizViewModel>();
            SimpleIoc.Default.Register<EditTeamViewModel>();
            SimpleIoc.Default.Register<ScoresViewModel>();
            SimpleIoc.Default.Register<RoundViewModel>();
            SimpleIoc.Default.Register<CorrectViewModel>();
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

    }
}
