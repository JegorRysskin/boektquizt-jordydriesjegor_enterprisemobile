using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoektQuiz.Tests
{
    [TestFixture]
    public class RoundStartViewModelTests
    {
        private Mock<IBackendService> _backendServiceMock;
        private Mock<INavigationService> _navigationServiceMock;
        private CrossConnectivityFake _crossConnectivityFake;
        private Round _round;
        private RoundStartViewModel _sut;

        [SetUp]
        public void SetUp()
        {
            _crossConnectivityFake = new CrossConnectivityFake();
            _backendServiceMock = new Mock<IBackendService>();
            _navigationServiceMock = new Mock<INavigationService>();
            _round = GenerateRound();
            _backendServiceMock.Setup(backend => backend.GetRoundById(It.IsAny<Int32>(), It.IsAny<String>())).ReturnsAsync(_round);
            _sut = new RoundStartViewModel(_navigationServiceMock.Object, _backendServiceMock.Object, 1);
            Connectivity.Instance = _crossConnectivityFake;
        }

        [Test]
        public void Constructor_ShouldSetRound()
        {
            //Act
            var sut = new RoundStartViewModel(_navigationServiceMock.Object, _backendServiceMock.Object, 1);

            //Assert
            Assert.That(sut.Round, Is.EqualTo(_round));
            _backendServiceMock.Verify(backend => backend.GetRoundById(It.IsAny<Int32>(), It.IsAny<String>()), Times.AtLeastOnce); //Normally it should be triggered once but because of the _sut constructor in the SetUp, it's triggered twice.
        }

        [Test]
        public void StartRoundCommand_IfConnectionIsEstablishedShouldntBeAbleToExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            //Act
            var canExecuteStartRoundCommand = _sut.StartRoundCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteStartRoundCommand, Is.EqualTo(false));
        }

        [Test]
        public void StartRoundCommand_IfNoConnectionIsEstablishedShouldBeAbleToExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = false;

            //Act
            var canExecuteStartRoundCommand = _sut.StartRoundCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteStartRoundCommand, Is.EqualTo(true));
        }

        [Test]
        public void StartRoundCommand_ChangeInConnectionShouldReEvaluateCanExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = false;

            //Act
            var canExecuteStartRoundCommandBeforeConnectionChange = _sut.StartRoundCommand.CanExecute(null);
            _crossConnectivityFake.ConnectionValue = true;
            var canExecuteStartRoundCommandAfterConnectionChange = _sut.StartRoundCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteStartRoundCommandBeforeConnectionChange, Is.Not.EqualTo(canExecuteStartRoundCommandAfterConnectionChange));
        }

        [Test]
        public void StartRoundCommand_ShouldStartRound()
        {
            //Arrange
            _navigationServiceMock.Setup(nav => nav.NavigateToAsync(It.IsAny<string>())).Returns(() => Task.CompletedTask);

            //Act
            _sut.StartRoundCommand.Execute(null);

            //Assert
            _navigationServiceMock.Verify(nav => nav.NavigateToAsync(RoutingConstants.QuestionRoute), Times.Once);
        }

        private Round GenerateRound()
        {
            return new Round { Id = 1, Name = "Ronde 1", Questions = GenerateQuestionsList(1) };
        }

        private List<Question> GenerateQuestionsList(int index)
        {
            return new List<Question>()
            {
                new Question { Id = 1 + (index * 10), QuestionString = "Vraag 1", Answer = new Answer() { Id = 1 + (index * 10), AnswerString = "", QuestionId = 1 + (index * 10) }  },
                new Question { Id = 2 + (index * 10), QuestionString = "Vraag 2", Answer = new Answer() { Id = 2 + (index * 10), AnswerString = "", QuestionId = 2 + (index * 10) }  },
                new Question { Id = 3 + (index * 10), QuestionString = "Vraag 3", Answer = new Answer() { Id = 3 + (index * 10), AnswerString = "", QuestionId = 3 + (index * 10) }  },
                new Question { Id = 4 + (index * 10), QuestionString = "Vraag 4", Answer = new Answer() { Id = 4 + (index * 10), AnswerString = "", QuestionId = 4 + (index * 10) }  },
                new Question { Id = 5 + (index * 10), QuestionString = "Vraag 5", Answer = new Answer() { Id = 5 + (index * 10), AnswerString = "", QuestionId = 5 + (index * 10) }  },
                new Question { Id = 6 + (index * 10), QuestionString = "Vraag 6", Answer = new Answer() { Id = 6 + (index * 10), AnswerString = "", QuestionId = 6 + (index * 10) }  },
                new Question { Id = 7 + (index * 10), QuestionString = "Vraag 7", Answer = new Answer() { Id = 7 + (index * 10), AnswerString = "", QuestionId = 7 + (index * 10) }  },
                new Question { Id = 8 + (index * 10), QuestionString = "Vraag 8", Answer = new Answer() { Id = 8 + (index * 10), AnswerString = "", QuestionId = 8 + (index * 10) }  },
                new Question { Id = 9 + (index * 10), QuestionString = "Vraag 9", Answer = new Answer() { Id = 9 + (index * 10), AnswerString = "", QuestionId = 9 + (index * 10) } },
                new Question { Id = 10 + (index * 10), QuestionString = "Vraag 10", Answer = new Answer() { Id = 10 + (index * 10), AnswerString = "", QuestionId = 10 + (index * 10) } },
            };
        }
    }
}
