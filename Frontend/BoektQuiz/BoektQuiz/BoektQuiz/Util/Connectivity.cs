using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace BoektQuiz.Util
{
    public static class Connectivity
    {
        static IConnectivity connectivity;
        public static IConnectivity Instance
        {
            get
            {
                return connectivity ?? (connectivity = CrossConnectivity.Current);
            }
            set
            {
                connectivity = value;
            }
        }
    }
}
