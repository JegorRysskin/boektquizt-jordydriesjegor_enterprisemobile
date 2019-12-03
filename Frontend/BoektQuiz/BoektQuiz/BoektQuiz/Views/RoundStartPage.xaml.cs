using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoektQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundStartPage : ContentPage
    {
        RoundStartViewModel viewModel;
        public Round Round { get; set; }

        private MockDataStore dataStore = new MockDataStore();

        public RoundStartPage()
        {
            InitializeComponent();

            Round = dataStore.GetRoundAsync(0).Result;

            BindingContext = this.viewModel = new RoundStartViewModel(Navigation, Round);
        }

        public RoundStartPage(RoundStartViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public RoundStartPage(int id = 0)
        {
            InitializeComponent();

            Round = dataStore.GetRoundAsync(id).Result;

            viewModel = new RoundStartViewModel(Navigation, Round);
            BindingContext = viewModel;
        }
    }
}