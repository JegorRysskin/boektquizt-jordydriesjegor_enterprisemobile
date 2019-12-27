using BoektQuiz.Models;
using BoektQuiz.Repositories;
using BoektQuiz.Services;
using BoektQuiz.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoektQuiz.Tests
{
    [TestFixture]
    public class QuestionOverviewViewModelTests
    {
        private Mock<IQuestionRepository> _questionRepositoryMock;
        private Mock<IRoundRepository> _roundRepositoryMock;
        private Mock<INavigationService> _navigationServiceMock;
        private QuestionOverviewViewModel _sut;
        private RoundOverviewViewModel _sender;


        [SetUp]
        public void SetUp()
        {
            _questionRepositoryMock = new Mock<IQuestionRepository>();
            _sut = new QuestionOverviewViewModel(_questionRepositoryMock.Object);
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
            var sut = new QuestionOverviewViewModel(_questionRepositoryMock.Object);
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
            questionsModified.Add(new Question() { Id = 11, Text = "Vraag 11", Answer = new Answer() { Id = 11, AnswerString = "", QuestionId = 11, TeamId = 1 } });
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

            return new Round() { Id = 1, Text = "Ronde 1", Questions = questions };
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
