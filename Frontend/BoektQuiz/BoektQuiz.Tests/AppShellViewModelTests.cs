﻿using BoektQuiz.Models;
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
        private CrossConnectivityFake _crossConnectivityFake;
        private Mock<IBackendService> _backendServiceMock;
        private List<Round> _allRounds;
        private AppShell _shell;

        [SetUp]
        public void SetUp()
        {
            _crossConnectivityFake = new CrossConnectivityFake();

            _backendServiceMock = new Mock<IBackendService>();
            _allRounds = GenerateRoundsList();
            _backendServiceMock.Setup(backend => backend.GetAllRounds(It.IsAny<String>())).ReturnsAsync(_allRounds);

            Connectivity.Instance = _crossConnectivityFake;
        }

        [Test]
        public void Constructor_ShouldLoadAllRounds_IfConnectionIsEstablished()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = true;

            //Act
            var sut = new AppShellViewModel(_backendServiceMock.Object);

            //Assert
            Assert.That(sut.Rounds, Is.EqualTo(_allRounds));
            _backendServiceMock.Verify(backend => backend.GetAllRounds(It.IsAny<String>()), Times.Once);
        }

        [Test]
        public void Constructor_ShouldLoadNothing_IfNoConnectionIsEstablished()
        {
            //Arrange
            _crossConnectivityFake.ConnectionValue = false;

            //Act
            var sut = new AppShellViewModel(_backendServiceMock.Object);

            //Assert
            Assert.Null(sut.Rounds);
            _backendServiceMock.Verify(backend => backend.GetAllRounds(It.IsAny<String>()), Times.Never);
        }

        private List<Round> GenerateRoundsList()
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
                new Question { Id = 1 + (index * 10), QuestionString = "Vraag 1"  },
                new Question { Id = 2 + (index * 10), QuestionString = "Vraag 2"  },
                new Question { Id = 3 + (index * 10), QuestionString = "Vraag 3"  },
                new Question { Id = 4 + (index * 10), QuestionString = "Vraag 4"  },
                new Question { Id = 5 + (index * 10), QuestionString = "Vraag 5"  },
                new Question { Id = 6 + (index * 10), QuestionString = "Vraag 6"  },
                new Question { Id = 7 + (index * 10), QuestionString = "Vraag 7"  },
                new Question { Id = 8 + (index * 10), QuestionString = "Vraag 8"  },
                new Question { Id = 9 + (index * 10), QuestionString = "Vraag 9" },
                new Question { Id = 10 + (index * 10), QuestionString = "Vraag 10" },
            };
        }
    }
}
