using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.ViewModels;

namespace BoektQuiz.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        public Vraag Vraag { get; set; }

        private MockDataStore dataStore = new MockDataStore();

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            Vraag = dataStore.GetItemAsync(0).Result;

            viewModel = new ItemDetailViewModel(Navigation, Vraag);
            BindingContext = viewModel;
        }

        public ItemDetailPage(int id = 0)
        {
            InitializeComponent();

            Vraag = dataStore.GetItemAsync(id).Result;

            viewModel = new ItemDetailViewModel(Navigation, Vraag);
            BindingContext = viewModel;
        }
    }
}