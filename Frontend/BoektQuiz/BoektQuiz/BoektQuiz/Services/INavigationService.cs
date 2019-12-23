using System;
using System.Threading.Tasks;
using BoektQuiz.ViewModels;
using Xamarin.Forms;

namespace BoektQuiz.Services
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task NavigateToAsync(string route);

        Task ReturnToRoot();
    }
}