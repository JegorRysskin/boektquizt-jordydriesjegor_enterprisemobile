using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoektQuiz.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddQuestionAsync(T item);
        Task<bool> UpdateQuestionAsync(T item);
        Task<bool> DeleteQuestionAsync(int id);
        Task<T> GetQuestionAsync(int id);
        Task<IEnumerable<T>> GetQuestionsAsync(bool forceRefresh = false);
    }
}
