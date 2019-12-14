using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models;

namespace JuryApp.Core.Services.Interfaces
{
    public interface IQuizService
    {
        Task<ObservableCollection<Quiz>> GetAllQuizzes();
        Task<Quiz> GetQuizById(int id);
    }

}
