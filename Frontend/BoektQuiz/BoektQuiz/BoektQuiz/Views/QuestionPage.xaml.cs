using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BoektQuiz.Models;
using BoektQuiz.Services;
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
        public Question Question { get; set; }

        private MockDataStore dataStore = new MockDataStore();

        public QuestionPage(QuestionViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

        public QuestionPage()
        {
            //InitializeComponent();

            for (int id = 9; id >= 0; id--)
            {
                Navigation.PushAsync(new QuestionPage(id));
            }

            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

        public QuestionPage(int id = 0)
        {
            InitializeComponent();

            Question = dataStore.GetQuestionAsync(id).Result;

            viewModel = new QuestionViewModel(Navigation, Question);
            BindingContext = viewModel;

                        CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var alert = DisplayAlert("Valsspeler", "U wordt nu gediskwalificeerd",
                    "Ik aanvaard de diskwalificatie");
            }
        }
    }
}