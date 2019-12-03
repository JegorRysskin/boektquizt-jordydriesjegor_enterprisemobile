using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using JuryApp.Core.Models;

namespace JuryApp.Core.Services
{
    public interface IQuizService
    {
        Task<ObservableCollection<Quiz>> GetAllQuizzes();
    }
}
