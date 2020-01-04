using System;
using System.IO;
using System.Threading.Tasks;
using BoektQuiz.Context;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.Views;
using Microsoft.EntityFrameworkCore;

namespace BoektQuiz
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitializeDatabase();

            InitializeApp();

            MainPage = new AppShell();

            InitializeNavigation();
        }

        private void InitializeDatabase()
        {
            using (BoektQuizContext context = BoektQuizContextFactory.Create())
            {
                context.Database.Migrate();
            };
        }

        private void InitializeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            navigationService.InitializeAsync();
        }
        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
