using BoektQuiz.Models;
using BoektQuiz.Repositories;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoektQuiz.Tests
{
    [TestFixture]
    public class RoundEndViewModelTests
    {
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IRoundRepository> _roundRepositoryMock;
        private Mock<IBackendService> _backendServiceMock;
        private Round _round;
        private CrossConnectivityFake _crossConnectivityFake;
        private RoundEndViewModel _sut;
        private QuestionViewModel _receiver;
        private RoundStartViewModel _sender;

        [SetUp]
        public void SetUp()
        {
            _crossConnectivityFake = new CrossConnectivityFake();
            _navigationServiceMock = new Mock<INavigationService>();
            _roundRepositoryMock = new Mock<IRoundRepository>();
            _backendServiceMock = new Mock<IBackendService>();
            _sut = new RoundEndViewModel(_navigationServiceMock.Object, _roundRepositoryMock.Object, _backendServiceMock.Object);
            _receiver = new QuestionViewModel(_navigationServiceMock.Object);
            _round = GenerateRound();
            _backendServiceMock.Setup(backend => backend.GetRoundById(It.IsAny<Int32>(), It.IsAny<String>())).ReturnsAsync(_round);
            _sender = new RoundStartViewModel(_navigationServiceMock.Object, _backendServiceMock.Object, 1);
            _sender.StartRoundCommand.Execute(null);
            Connectivity.Instance = _crossConnectivityFake;
        }

        [Test]
        public void Constructor_ShouldLoadRound()
        {
            //Act
            var sut = new RoundEndViewModel(_navigationServiceMock.Object, _roundRepositoryMock.Object, _backendServiceMock.Object);
            FillInAnswers();

            //Assert
            Assert.That(sut.Round, Is.Not.Null);
        }

        [Test]
        public void EndRoundCommand_IfNoConnectionIsEstablishedShouldntBeAbleToExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = false;

            //Act
            var canExecuteStartRoundCommand = _sut.EndRoundCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteStartRoundCommand, Is.EqualTo(false));
        }

        [Test]
        public void EndRoundCommand_IfConnectionIsEstablishedShouldBeAbleToExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            //Act
            var canExecuteStartRoundCommand = _sut.EndRoundCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteStartRoundCommand, Is.EqualTo(true));
        }

        [Test]
        public void EndRoundCommand_ChangeInConnectionShouldReEvaluateCanExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = false;

            //Act
            var canExecuteStartRoundCommandBeforeConnectionChange = _sut.EndRoundCommand.CanExecute(null);
            _crossConnectivityFake.ConnectionValue = true;
            var canExecuteStartRoundCommandAfterConnectionChange = _sut.EndRoundCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteStartRoundCommandBeforeConnectionChange, Is.Not.EqualTo(canExecuteStartRoundCommandAfterConnectionChange));
        }

        [Test]
        public void EndRoundCommand_ShouldUpdateRoundAndReturnToRoundStartView()
        {
            //Act
            FillInAnswers();
            _sut.EndRoundCommand.Execute(null);

            //Assert
            _roundRepositoryMock.Verify(repo => repo.UpdateRoundAsync(_sut.Round), Times.Once);
            _navigationServiceMock.Verify(nav => nav.ReturnToRoot(), Times.Once);
        }

        private void FillInAnswers()
        {
            foreach(Question question in _receiver.Round.Questions)
            {
                question.Answer.AnswerString = Guid.NewGuid().ToString();
                _receiver.SendAnswerCommand.Execute(null);
            }
        }

        private Round GenerateRound()
        {
            var questions = GenerateQuestionsList();

            return new Round() { Id = 1, Name = "Ronde 1", Questions = questions };
        }

        private List<Question> GenerateQuestionsList()
        {
            return new List<Question>()
            {
                new Question { Id = 1, Text = "Vraag 1", Answer = new Answer() { Id = 1, AnswerString = "", QuestionId = 1, TeamId = 1 }  },
                new Question { Id = 2, Text = "Vraag 2", Answer = new Answer() { Id = 2, AnswerString = "", QuestionId = 2, TeamId = 1 }  },
                new Question { Id = 3, Text = "Vraag 3", Answer = new Answer() { Id = 3, AnswerString = "", QuestionId = 3, TeamId = 1 }  },
                new Question { Id = 4, Text = "Vraag 4", Answer = new Answer() { Id = 4, AnswerString = "", QuestionId = 4, TeamId = 1 }  },
                new Question { Id = 5, Text = "Vraag 5", Answer = new Answer() { Id = 5, AnswerString = "", QuestionId = 5, TeamId = 1 }  },
                new Question { Id = 6, Text = "Vraag 6", Answer = new Answer() { Id = 6, AnswerString = "", QuestionId = 6, TeamId = 1 }  },
                new Question { Id = 7, Text = "Vraag 7", Answer = new Answer() { Id = 7, AnswerString = "", QuestionId = 7, TeamId = 1 }  },
                new Question { Id = 8, Text = "Vraag 8", Answer = new Answer() { Id = 8, AnswerString = "", QuestionId = 8, TeamId = 1 }  },
                new Question { Id = 9, Text = "Vraag 9", Answer = new Answer() { Id = 9, AnswerString = "", QuestionId = 9, TeamId = 1 } },
                new Question { Id = 10, Text = "Vraag 10", Answer = new Answer() { Id = 10, AnswerString = "", QuestionId = 10, TeamId = 1 } },
            };
        }
    }
}
