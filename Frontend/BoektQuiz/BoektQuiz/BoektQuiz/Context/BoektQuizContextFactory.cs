using System.IO;

namespace BoektQuiz.Context
{
    public class BoektQuizContextFactory
    {
        public static BoektQuizContext Create()
        {
            BoektQuizContext context = new BoektQuizContext();

            string dbName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "boektquiz.db");
            context.ConnectionString = $"Data Source = {dbName}";

            return context;
        }
    }
}
