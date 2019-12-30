
using JuryApp.ViewModels;

using Xunit;

namespace JuryApp.Tests.XUnit
{
    // TODO WTS: Add appropriate tests
    public class Tests
    {
        [Fact]
        public void TestMethod1()
        {
        }

        // TODO WTS: Add tests for functionality you add to MainViewModel.
        [Fact]
        public void TestMainViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new MainViewModel();
            Assert.NotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to QuizzenViewModel.
        [Fact]
        public void TestQuizzenViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new QuizzenViewModel();
            Assert.NotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to TeamsViewModel.
        [Fact]
        public void TestTeamsViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new TeamsViewModel();
            Assert.NotNull(vm);
        }
    }
}
