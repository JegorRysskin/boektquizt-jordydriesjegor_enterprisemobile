using BoektQuiz.Models;
using BoektQuiz.Repositories;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BoektQuiz.Tests
{
    [TestFixture]
    public class RoundEndViewModelTests
    {
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IRoundRepository> _roundRepositoryMock;
        private Mock<ITeamRepository> _teamRepositoryMock;
        private Mock<IBackendService> _backendServiceMock;
        private Round _round;
        private Team _team;
        private CrossConnectivityFake _crossConnectivityFake;
        private RoundEndViewModel _sut;
        private QuestionViewModel _receiver;
        private RoundStartViewModel _sender;

        [SetUp]
        public void SetUp()
        {
            _round = GenerateRound();
            _team = GenerateTeam();

            _crossConnectivityFake = new CrossConnectivityFake();
            _navigationServiceMock = new Mock<INavigationService>();
            _roundRepositoryMock = new Mock<IRoundRepository>();
            _teamRepositoryMock = new Mock<ITeamRepository>();
            _backendServiceMock = new Mock<IBackendService>();
            _backendServiceMock.Setup(backend => backend.GetTeamByToken(It.IsAny<String>())).ReturnsAsync(_team);
            _backendServiceMock.Setup(backend => backend.GetRoundById(It.IsAny<Int32>(), It.IsAny<String>())).ReturnsAsync(_round);

            _receiver = new QuestionViewModel(_navigationServiceMock.Object);
            _sender = new RoundStartViewModel(_navigationServiceMock.Object, _backendServiceMock.Object, 1);
            _sender.StartRoundCommand.Execute(null);
            _sut = new RoundEndViewModel(_navigationServiceMock.Object, _roundRepositoryMock.Object, _teamRepositoryMock.Object, _backendServiceMock.Object);

            FillInAnswers();
            Connectivity.Instance = _crossConnectivityFake;
        }

        [Test]
        public void Constructor_ShouldLoadRoundAndTeam()
        {
            //Act
            var sut = new RoundEndViewModel(_navigationServiceMock.Object, _roundRepositoryMock.Object, _teamRepositoryMock.Object, _backendServiceMock.Object);
            FillInAnswers();

            //Assert
            Assert.That(sut.Round, Is.Not.Null);
            Assert.That(sut.Team, Is.Not.Null);
        }

        [Test]
        public void EndRoundCommand_IfNoConnectionIsEstablishedAndRoundDisabled_ShouldntBeAbleToExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = false;
            _sut.Round.Enabled = false;

            //Act
            var canExecuteEndRoundCommand = _sut.EndRoundCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteEndRoundCommand, Is.EqualTo(false));
        }

        [Test]
        public void EndRoundCommand_IfConnectionIsEstablishedAndRoundEnabled_ShouldntBeAbleToExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = false;
            _sut.Round.Enabled = true;

            //Act
            var canExecuteEndRoundCommand = _sut.EndRoundCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteEndRoundCommand, Is.EqualTo(false));
        }

        [Test]
        public void EndRoundCommand_IfConnectionIsEstablishedAndRoundDisabled_ShouldBeAbleToExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;
            _sut.Round.Enabled = false;

            //Act
            var canExecuteEndRoundCommand = _sut.EndRoundCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteEndRoundCommand, Is.EqualTo(true));
        }

        [Test]
        public void EndRoundCommand_ChangeInConnectionShouldReEvaluateCanExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = false;
            _sut.Round.Enabled = false;

            //Act
            var canExecuteEndRoundCommandBeforeConnectionChange = _sut.EndRoundCommand.CanExecute(null);
            _crossConnectivityFake.ConnectionValue = true;
            var canExecuteEndRoundCommandAfterConnectionChange = _sut.EndRoundCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteEndRoundCommandBeforeConnectionChange, Is.Not.EqualTo(canExecuteEndRoundCommandAfterConnectionChange));
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

        [Test]
        public void ReloadRoundCommand_ShouldReloadRoundAndReEvaluateEndRoundCommandCanExecute()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            var roundBeforeReload = new Round() { Id = _sut.Round.Id, Name = _sut.Round.Name, Enabled = _sut.Round.Enabled, Questions = _sut.Round.Questions };
            var canExecuteEndRoundCommandBeforeReload = _sut.EndRoundCommand.CanExecute(null);

            /*To simulate Round state change from JuryApp */
            _round.Enabled = false;
            /*To simulate Round state change from JuryApp */

            //Act
            _sut.ReloadRoundCommand.Execute(null);
            var canExecuteEndRoundCommandAfterReload = _sut.EndRoundCommand.CanExecute(null);

            //Assert
            Assert.That(roundBeforeReload, Is.Not.EqualTo(_sut.Round));
            Assert.That(canExecuteEndRoundCommandBeforeReload, Is.Not.EqualTo(canExecuteEndRoundCommandAfterReload));
            _backendServiceMock.Verify(backend => backend.GetRoundById(It.IsAny<Int32>(), It.IsAny<String>()), Times.AtLeastOnce); //Normally it should be triggered once but because of the _sender constructor in the SetUp, it's triggered twice.
        }

        private void FillInAnswers()
        {
            foreach(Answer answer in _receiver.Team.Answers)
            {
                answer.AnswerString = Guid.NewGuid().ToString();
                _receiver.SendAnswerCommand.Execute(null);
            }
        }

        private Round GenerateRound()
        {
            var questions = GenerateQuestionsList();

            return new Round() { Id = 1, Name = "Ronde 1", Enabled = true, Questions = questions };
        }

        private Team GenerateTeam()
        {
            var answers = GenerateAnswersList();

            return new Team() { Id = 1, Name = "Team 1", Answers = answers, Enabled = true, Scores = 0 };
        }

        private List<Question> GenerateQuestionsList()
        {
            return new List<Question>()
            {
                new Question { Id = 1, QuestionString = "Vraag 1"  },
                new Question { Id = 2, QuestionString = "Vraag 2"  },
                new Question { Id = 3, QuestionString = "Vraag 3"  },
                new Question { Id = 4, QuestionString = "Vraag 4"  },
                new Question { Id = 5, QuestionString = "Vraag 5"  },
                new Question { Id = 6, QuestionString = "Vraag 6"  },
                new Question { Id = 7, QuestionString = "Vraag 7"  },
                new Question { Id = 8, QuestionString = "Vraag 8"  },
                new Question { Id = 9, QuestionString = "Vraag 9" },
                new Question { Id = 10, QuestionString = "Vraag 10" },
            };
        }

        private List<Answer> GenerateAnswersList()
        {
            return new List<Answer>()
            {
                new Answer { Id = 1, AnswerString = String.Empty, QuestionId = 1 },
                new Answer { Id = 1, AnswerString = String.Empty, QuestionId = 2 },
                new Answer { Id = 1, AnswerString = String.Empty, QuestionId = 3 },
                new Answer { Id = 1, AnswerString = String.Empty, QuestionId = 4 },
                new Answer { Id = 1, AnswerString = String.Empty, QuestionId = 5 },
                new Answer { Id = 1, AnswerString = String.Empty, QuestionId = 6 },
                new Answer { Id = 1, AnswerString = String.Empty, QuestionId = 7 },
                new Answer { Id = 1, AnswerString = String.Empty, QuestionId = 8 },
                new Answer { Id = 1, AnswerString = String.Empty, QuestionId = 9 },
                new Answer { Id = 1, AnswerString = String.Empty, QuestionId = 10 }
            };
        }
    }
}
