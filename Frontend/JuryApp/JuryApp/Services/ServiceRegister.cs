using GalaSoft.MvvmLight.Ioc;
using JuryApp.Core.Services;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Helpers;

namespace JuryApp.Services
{
    public class ServiceRegister
    {
        public ServiceRegister()
        {
            SimpleIoc.Default.Register<IMessengerCache, MessengerCache>();
            MessengerCache = SimpleIoc.Default.GetInstance<IMessengerCache>();
            SimpleIoc.Default.Register<IQuizService, QuizService>();
            SimpleIoc.Default.Register<IRoundService, RoundService>();
            SimpleIoc.Default.Register<ITeamService, TeamService>();
        }

        public IMessengerCache MessengerCache;

    }
}
