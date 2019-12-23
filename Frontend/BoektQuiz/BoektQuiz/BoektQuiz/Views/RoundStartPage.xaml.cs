using System;
using BoektQuiz.Models;
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

            viewModel = new RoundStartViewModel(AppContainer.Resolve<INavigationService>(), id);

            BindingContext = viewModel;
        }
    }
}