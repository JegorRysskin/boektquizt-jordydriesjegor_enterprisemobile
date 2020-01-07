using BoektQuiz.Models;
using BoektQuiz.Repositories;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BoektQuiz.Tests
{
    [TestFixture]
    public class RoundOverviewViewModelTests
    {
        private Mock<IRoundRepository> _roundRepositoryMock;
        private Mock<IQuestionRepository> _questionRepositoryMock;
        private Mock<INavigationService> _navigationServiceMock;
        private RoundOverviewViewModel _sut;
        private QuestionOverviewViewModel _receiver;
        private IList<Round> _allRounds;

        [SetUp]
        public void SetUp()
        {
            _roundRepositoryMock = new Mock<IRoundRepository>();
            _navigationServiceMock = new Mock<INavigationService>();
            _allRounds = GenerateRoundsList();
            _roundRepositoryMock.Setup(repo => repo.GetAllRoundsAsync()).ReturnsAsync(_allRounds);
            _sut = new RoundOverviewViewModel(_navigationServiceMock.Object, _roundRepositoryMock.Object);
            _questionRepositoryMock = new Mock<IQuestionRepository>();
            _receiver = new QuestionOverviewViewModel(_questionRepositoryMock.Object);
        }

        [Test]
        public void Constructor_ShouldLoadAllRounds()
        {
            //Act
            var sut = new RoundOverviewViewModel(_navigationServiceMock.Object, _roundRepositoryMock.Object);

            //Assert
            Assert.That(sut.Rounds, Is.EqualTo(_allRounds));
            _roundRepositoryMock.Verify(repo => repo.GetAllRoundsAsync(), Times.AtLeastOnce); //Normally it should be triggered once but because of the _sut constructor in the SetUp, it's triggered twice.
        }

        [Test]
        public void RoundWithNoQuestionListShouldDisableItemSelectCommand()
        {
            //Arrange
            var emptyRound = new Round() { Id = 0, Name = "Empty Round" };

            //Act
            var canExecuteItemSelectCommand = _sut.ItemSelectCommand.CanExecute(emptyRound);

            //Assert
            Assert.That(canExecuteItemSelectCommand, Is.EqualTo(false));
        }

        [Test]
        public void RoundWithQuestionListShouldEnableItemSelectCommand()
        {
            //Arrange
            var round = new Round() { Id = 0, Name = "Round", Questions = GenerateQuestionsList(0) };

            //Act
            var canExecuteItemSelectCommand = _sut.ItemSelectCommand.CanExecute(round);

            //Assert
            Assert.That(canExecuteItemSelectCommand, Is.EqualTo(true));
        }

        [Test]
        public void ItemSelectCommand_ShouldTriggerNavigationAndSendRound()
        {
            //Arrange
            var round = new Round() { Id = 0, Name = "Round", Questions = GenerateQuestionsList(0) };

            //Act
            _sut.ItemSelectCommand.Execute(round);
            Round receivedRound = _receiver.Round;

            //Assert
            Assert.That(receivedRound, Is.Not.Null);
            Assert.That(receivedRound, Is.EqualTo(round));
            _navigationServiceMock.Verify(nav => nav.NavigateToAsync(RoutingConstants.QuestionOverviewRoute), Times.Once);
        }

        [Test]
        public void LoadItemsCommand_ShouldReloadAllRounds()
        {
            //Act
            var roundsBeforeReload = new List<Round>(_sut.Rounds);
            var allRoundsModified = new List<Round>(_allRounds);
            allRoundsModified.Add(new Round() { Id = 9, Name = "Ronde 9", Questions = GenerateQuestionsList(8) });
            _roundRepositoryMock.Setup(repo => repo.GetAllRoundsAsync()).ReturnsAsync(allRoundsModified);
            _sut.LoadItemsCommand.Execute(null);
            var roundsAfterReload = _sut.Rounds;

            //Assert
            Assert.That(roundsBeforeReload, Is.Not.Null);
            Assert.That(roundsAfterReload, Is.Not.Null);
            Assert.That(roundsBeforeReload, Is.Not.EqualTo(roundsAfterReload));
            Assert.That(roundsAfterReload.Count, Is.EqualTo(roundsBeforeReload.Count + 1));
            _roundRepositoryMock.Verify(repo => repo.GetAllRoundsAsync(), Times.Exactly(2));
        }

        private IList<Round> GenerateRoundsList()
        {
            var dummyquestions = GenerateDummyQuestionsList();

            var questions1 = GenerateQuestionsList(0);

            var questions2 = GenerateQuestionsList(1);

            var questions3 = GenerateQuestionsList(2);

            var questions4 = GenerateQuestionsList(3);

            var questions5 = GenerateQuestionsList(4);

            var questions6 = GenerateQuestionsList(5);

            var questions7 = GenerateQuestionsList(6);

            var questions8 = GenerateQuestionsList(7);

            return new List<Round>()
            {
                new Round { Id = -1, Name = "Ronde 0", Questions = dummyquestions },
                new Round { Id = 1, Name = "Ronde 1", Questions = questions1 },
                new Round { Id = 2, Name = "Ronde 2", Questions = questions2 },
                new Round { Id = 3, Name = "Ronde 3", Questions = questions3 },
                new Round { Id = 4, Name = "Ronde 4", Questions = questions4 },
                new Round { Id = 5, Name = "Ronde 5", Questions = questions5 },
                new Round { Id = 6, Name = "Ronde 6", Questions = questions6 },
                new Round { Id = 7, Name = "Ronde 7", Questions = questions7 },
                new Round { Id = 8, Name = "Ronde 8", Questions = questions8 },
            };
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

        private List<Question> GenerateDummyQuestionsList()
        {
            return new List<Question>()
            {
                new Question { Id = -1, QuestionString = "Vraag 1", Answer = new Answer() { Id = -1, AnswerString = "Dummy Antwoord 1", QuestionId = -1 }  },
                new Question { Id = -2, QuestionString = "Vraag 2", Answer = new Answer() { Id = -2, AnswerString = "Dummy Antwoord 2", QuestionId = -2 }  },
                new Question { Id = -3, QuestionString = "Vraag 3", Answer = new Answer() { Id = -3, AnswerString = "Dummy Antwoord 3", QuestionId = -3 }  },
                new Question { Id = -4, QuestionString = "Vraag 4", Answer = new Answer() { Id = -4, AnswerString = "Dummy Antwoord 4", QuestionId = -4 }  },
                new Question { Id = -5, QuestionString = "Vraag 5", Answer = new Answer() { Id = -5, AnswerString = "Dummy Antwoord 5", QuestionId = -5 }  },
                new Question { Id = -6, QuestionString = "Vraag 6", Answer = new Answer() { Id = -6, AnswerString = "Dummy Antwoord 6", QuestionId = -6 }  },
                new Question { Id = -7, QuestionString = "Vraag 7", Answer = new Answer() { Id = -7, AnswerString = "Dummy Antwoord 7", QuestionId = -7 }  },
                new Question { Id = -8, QuestionString = "Vraag 8", Answer = new Answer() { Id = -8, AnswerString = "Dummy Antwoord 8", QuestionId = -8 }  },
                new Question { Id = -9, QuestionString = "Vraag 9", Answer = new Answer() { Id = -9, AnswerString = "Dummy Antwoord 9", QuestionId = -9 } },
                new Question { Id = -10, QuestionString = "Vraag 10", Answer = new Answer() { Id = -10, AnswerString = "Dummy Antwoord 10", QuestionId = -10 } },
            };
        }
    }
}
