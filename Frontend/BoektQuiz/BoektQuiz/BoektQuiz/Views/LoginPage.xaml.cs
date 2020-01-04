using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoektQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this.viewModel = new LoginViewModel(AppContainer.Resolve<IBackendService>());
        }
    }
}