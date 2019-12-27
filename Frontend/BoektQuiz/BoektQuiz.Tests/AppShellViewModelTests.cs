using BoektQuiz.Models;
using BoektQuiz.Services;
using BoektQuiz.Util;
using BoektQuiz.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoektQuiz.Tests
{
    [TestFixture]
    public class AppShellViewModelTests
    {
        private Mock<IDataStore<Round>> _dataStoreMock;
        private AppShell _shell;

        [SetUp]
        public void SetUp()
        {
            AppContainer.RegisterDependencies();
            _dataStoreMock = new Mock<IDataStore<Round>>();
            _shell = new AppShell();
        }

        [Test]
        public void Constructor_ShouldLoadAllRounds()
        {
            //Arrange
            IList<Round> allRounds = GenerateRoundsList();
            _dataStoreMock.Setup(ds => ds.GetItemsAsync()).ReturnsAsync(allRounds);

            //Act
            var sut = new AppShellViewModel(_shell, _dataStoreMock.Object);

            //Assert
            Assert.That(sut.Rounds, Is.EqualTo(allRounds));
            _dataStoreMock.Verify(ds => ds.GetItemsAsync(), Times.Once);
        }

        private IList<Round> GenerateRoundsList()
        {
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
                new Round { Id = 1, Text = "Ronde 1", Questions = questions1 },
                new Round { Id = 2, Text = "Ronde 2", Questions = questions2 },
                new Round { Id = 3, Text = "Ronde 3", Questions = questions3 },
                new Round { Id = 4, Text = "Ronde 4", Questions = questions4 },
                new Round { Id = 5, Text = "Ronde 5", Questions = questions5 },
                new Round { Id = 6, Text = "Ronde 6", Questions = questions6 },
                new Round { Id = 7, Text = "Ronde 7", Questions = questions7 },
                new Round { Id = 8, Text = "Ronde 8", Questions = questions8 },
            };
        }
        
        private List<Question> GenerateQuestionsList(int index)
        {
            return new List<Question>()
            {
                new Question { Id = 1 + (index * 10), Text = "Vraag 1", Answer = new Answer() { Id = 1 + (index * 10), AnswerString = "", QuestionId = 1 + (index * 10), TeamId = 1 }  },
                new Question { Id = 2 + (index * 10), Text = "Vraag 2", Answer = new Answer() { Id = 2 + (index * 10), AnswerString = "", QuestionId = 2 + (index * 10), TeamId = 1 }  },
                new Question { Id = 3 + (index * 10), Text = "Vraag 3", Answer = new Answer() { Id = 3 + (index * 10), AnswerString = "", QuestionId = 3 + (index * 10), TeamId = 1 }  },
                new Question { Id = 4 + (index * 10), Text = "Vraag 4", Answer = new Answer() { Id = 4 + (index * 10), AnswerString = "", QuestionId = 4 + (index * 10), TeamId = 1 }  },
                new Question { Id = 5 + (index * 10), Text = "Vraag 5", Answer = new Answer() { Id = 5 + (index * 10), AnswerString = "", QuestionId = 5 + (index * 10), TeamId = 1 }  },
                new Question { Id = 6 + (index * 10), Text = "Vraag 6", Answer = new Answer() { Id = 6 + (index * 10), AnswerString = "", QuestionId = 6 + (index * 10), TeamId = 1 }  },
                new Question { Id = 7 + (index * 10), Text = "Vraag 7", Answer = new Answer() { Id = 7 + (index * 10), AnswerString = "", QuestionId = 7 + (index * 10), TeamId = 1 }  },
                new Question { Id = 8 + (index * 10), Text = "Vraag 8", Answer = new Answer() { Id = 8 + (index * 10), AnswerString = "", QuestionId = 8 + (index * 10), TeamId = 1 }  },
                new Question { Id = 9 + (index * 10), Text = "Vraag 9", Answer = new Answer() { Id = 9 + (index * 10), AnswerString = "", QuestionId = 9 + (index * 10), TeamId = 1 } },
                new Question { Id = 10 + (index * 10), Text = "Vraag 10", Answer = new Answer() { Id = 10 + (index * 10), AnswerString = "", QuestionId = 10 + (index * 10), TeamId = 1 } },
            };
        }
    }
}
