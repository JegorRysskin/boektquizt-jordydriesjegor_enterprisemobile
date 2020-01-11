using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Xamarin.Forms;

namespace BoektQuiz
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        AppShellViewModel viewModel;

        public AppShell()
        {
            InitializeComponent();

            Shell.SetBackgroundColor(this, Color.FromHex("ED028C"));
            Shell.SetForegroundColor(this, Color.White);
            Shell.SetTabBarIsVisible(this, false);

            viewModel = new AppShellViewModel(AppContainer.Resolve<IBackendService>());
            BindingContext = viewModel;
        }

        private void Shell_Navigated(object sender, ShellNavigatedEventArgs e)
        {
            viewModel.LoadRounds();
        }
    }
}
