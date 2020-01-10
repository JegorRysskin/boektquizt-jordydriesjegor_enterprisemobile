using BoektQuiz.Models;
using BoektQuiz.Repositories;
using BoektQuiz.Services;
using BoektQuiz.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BoektQuiz.Tests
{
    [TestFixture]
    public class QuestionOverviewViewModelTests
    {
        private Mock<IQuestionRepository> _questionRepositoryMock;
        private Mock<IAnswerRepository> _teamRepositoryMock;
        private Mock<IRoundRepository> _roundRepositoryMock;
        private Mock<IBackendService> _backendServiceMock;
        private Team _team;
        private Mock<INavigationService> _navigationServiceMock;
        private QuestionOverviewViewModel _sut;
        private RoundOverviewViewModel _sender;


        [SetUp]
        public void SetUp()
        {
            _questionRepositoryMock = new Mock<IQuestionRepository>();
            _teamRepositoryMock = new Mock<IAnswerRepository>();
            _backendServiceMock = new Mock<IBackendService>();
            _team = GenerateTeam();
            _backendServiceMock.Setup(backend => backend.GetTeamByToken(It.IsAny<String>())).ReturnsAsync(_team);
            _sut = new QuestionOverviewViewModel(_questionRepositoryMock.Object, _teamRepositoryMock.Object, _backendServiceMock.Object);
            _roundRepositoryMock = new Mock<IRoundRepository>();
            _navigationServiceMock = new Mock<INavigationService>();
            _sender = new RoundOverviewViewModel(_navigationServiceMock.Object, _roundRepositoryMock.Object);
        }

        [Test]
        public void Constructor_ShouldReceiveRoundAndLoadAllQuestions()
        {
            //Arrange
            var round = GenerateRound();

            //Act
            var sut = new QuestionOverviewViewModel(_questionRepositoryMock.Object, _teamRepositoryMock.Object, _backendServiceMock.Object);
            _sender.ItemSelectCommand.Execute(round);

            //Assert
            Assert.That(sut.Questions, Is.Not.Null);
            Assert.That(sut.Questions, Is.EqualTo(round.Questions));
        }

        [Test]
        public void LoadItemsCommand_ShouldReloadAllQuestions()
        {
            //Arrange
            var round = GenerateRound();
            _sender.ItemSelectCommand.Execute(round);

            //Act
            var questionsBeforeReload = new List<Question>(_sut.Questions);
            var questionsModified = new List<Question>(_sut.Questions);
            questionsModified.Add(new Question() { Id = 11, QuestionString = "Vraag 11" });
            _questionRepositoryMock.Setup(repo => repo.GetQuestionsFromRound(It.IsAny<Int32>())).ReturnsAsync(questionsModified);
            _sut.LoadItemsCommand.Execute(null);
            var questionsAfterReload = _sut.Questions;

            //Assert
            Assert.That(questionsBeforeReload, Is.Not.Null);
            Assert.That(questionsAfterReload, Is.Not.Null);
            Assert.That(questionsBeforeReload, Is.Not.EqualTo(questionsAfterReload));
            Assert.That(questionsAfterReload.Count, Is.EqualTo(questionsBeforeReload.Count + 1));
            _questionRepositoryMock.Verify(repo => repo.GetQuestionsFromRound(It.IsAny<Int32>()), Times.Once);
        }

        private Round GenerateRound()
        {
            var questions = GenerateQuestionsList();

            return new Round() { Id = 1, Name = "Ronde 1", Questions = questions };
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
