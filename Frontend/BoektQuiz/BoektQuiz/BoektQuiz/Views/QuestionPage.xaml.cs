using System.ComponentModel;
using Xamarin.Forms;
using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace BoektQuiz.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class QuestionPage : ContentPage
    {
        QuestionViewModel viewModel;
        public bool IsACEnabled { get; private set; } // AC stands for Anti-Cheat in this context and not Air-Conditioning

        public QuestionPage()
        {
            InitializeComponent();

            viewModel = new QuestionViewModel(AppContainer.Resolve<INavigationService>());
            BindingContext = viewModel;

            IsACEnabled = true;

            MessagingCenter.Instance.Subscribe<QuestionViewModel, bool>(this, "IsACEnabled", (sender, isACEnabled) => { IsACEnabled = isACEnabled; });

            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected && IsACEnabled)
            {
                var alert = DisplayAlert("Valsspeler", "U wordt nu gediskwalificeerd",
                    "Ik aanvaard de diskwalificatie");
            }
        }
    }
}