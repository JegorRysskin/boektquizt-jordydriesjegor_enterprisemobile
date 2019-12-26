using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using BoektQuiz.Views;
using Xamarin.Forms;

namespace BoektQuiz.Services
{
    public class NavigationService : INavigationService
    {

        private static AppShell _currentShell;

        public void RegisterRoutes()
        {
            Routing.RegisterRoute(RoutingConstants.RegisterRoute, typeof(RegisterPage));
            Routing.RegisterRoute(RoutingConstants.HomeRoute, typeof(HomePage));
            Routing.RegisterRoute(RoutingConstants.LoginRoute, typeof(LoginPage));
            Routing.RegisterRoute(RoutingConstants.QuestionRoute, typeof(QuestionPage));
            Routing.RegisterRoute(RoutingConstants.RoundEndRoute, typeof(RoundEndPage));
            Routing.RegisterRoute(RoutingConstants.RoundStartRoute, typeof(RoundStartPage));
            Routing.RegisterRoute(RoutingConstants.QuestionOverviewRoute, typeof(QuestionOverviewPage));
        }

        public async Task InitializeAsync()
        {
            await Task.Run(() =>
            {
                if (App.Current.MainPage is AppShell shell)
                {
                    _currentShell = shell;
                    RegisterRoutes();
                }
                else
                {
                    throw new InvalidOperationException("The MainPage should be the Shell when navigating");
                }
            });
        }

        public async Task NavigateToAsync(string route)
        {
            await _currentShell.GoToAsync(route);
        }

        public async Task ReturnToRoot()
        {
            await _currentShell.Navigation.PopToRootAsync();
        }
    }
}
