using BoektQuiz.Models;
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
    public class QuestionViewModelTests
    {
        private Mock<IBackendService> _backendServiceMock;
        private Mock<INavigationService> _navigationServiceMock;
        private Round _round;
        private QuestionViewModel _sut;
        private RoundStartViewModel _sender;

        [SetUp]
        public void SetUp()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _sut = new QuestionViewModel(_navigationServiceMock.Object);
            _backendServiceMock = new Mock<IBackendService>();
            _round = GenerateRound();
            _backendServiceMock.Setup(backend => backend.GetRoundById(It.IsAny<Int32>(), It.IsAny<String>())).ReturnsAsync(_round);
            _sender = new RoundStartViewModel(_navigationServiceMock.Object, _backendServiceMock.Object, 1);
            _sender.StartRoundCommand.Execute(null);
        }

        [Test]
        public void Constructor_ShouldLoadQuestion()
        {
            //Act
            var sut = new QuestionViewModel(_navigationServiceMock.Object);
            _sender.StartRoundCommand.Execute(null);

            //Assert
            Assert.That(sut.Question, Is.Not.Null);
        }

        [Test]
        public void SendAnswerCommand_ShouldDisableIfAnswerIsEmpty()
        {
            //Act
            var canExecuteSendAnswerCommand = _sut.SendAnswerCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteSendAnswerCommand, Is.EqualTo(false));
        }

        [Test]
        public void SendAnswerCommand_ShouldEnableIfAnswerIsNotEmpty()
        {
            //Act
            _sut.Question.Answer.AnswerString = Guid.NewGuid().ToString();
            var canExecuteSendAnswerCommand = _sut.SendAnswerCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteSendAnswerCommand, Is.EqualTo(true));
        }

        [Test]
        public void SendAnswerCommand_ShouldReEvaluateWhenAnswerIsBeingTyped()
        {
            //Act
            var canExecuteSendAnswerCommandBeforeTyping = _sut.SendAnswerCommand.CanExecute(null);
            _sut.Question.Answer.AnswerString = Guid.NewGuid().ToString();
            var canExecuteSendAnswerCommandAfterTyping = _sut.SendAnswerCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteSendAnswerCommandBeforeTyping, Is.Not.EqualTo(canExecuteSendAnswerCommandAfterTyping));
        }

        [Test]
        public void SendAnswerCommand_ShouldReEvaluateWhenAnswerIsBeingCleared()
        {
            //Act
            _sut.Question.Answer.AnswerString = Guid.NewGuid().ToString();
            var canExecuteSendAnswerCommandBeforeClearing = _sut.SendAnswerCommand.CanExecute(null);
            _sut.Question.Answer.AnswerString = String.Empty;
            var canExecuteSendAnswerCommandAfterClearing = _sut.SendAnswerCommand.CanExecute(null);

            //Assert
            Assert.That(canExecuteSendAnswerCommandBeforeClearing, Is.Not.EqualTo(canExecuteSendAnswerCommandAfterClearing));
        }

        [Test]
        public void SendAnswerCommand_ShouldLoadNextQuestion()
        {
            //Act
            var question = _sut.Question;
            _sut.SendAnswerCommand.Execute(null);
            var nextQuestion = _sut.Question;

            //Assert
            Assert.That(question, Is.Not.EqualTo(nextQuestion));
            Assert.That(nextQuestion.Id, Is.EqualTo(question.Id + 1));
        }

        [Test]
        public void SendAnswerCommand_ShouldTriggerNavigationToRoundEndViewOnLastQuestion()
        {
            //Act
            foreach(Question question in _round.Questions)
            {
                _sut.SendAnswerCommand.Execute(null);
            }

            //Assert
            _navigationServiceMock.Verify(nav => nav.NavigateToAsync(RoutingConstants.RoundEndRoute), Times.Once);
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
                new Question { Id = 1, QuestionString = "Vraag 1", Answer = new Answer() { Id = 1, AnswerString = "", QuestionId = 1 }  },
                new Question { Id = 2, QuestionString = "Vraag 2", Answer = new Answer() { Id = 2, AnswerString = "", QuestionId = 2 }  },
                new Question { Id = 3, QuestionString = "Vraag 3", Answer = new Answer() { Id = 3, AnswerString = "", QuestionId = 3 }  },
                new Question { Id = 4, QuestionString = "Vraag 4", Answer = new Answer() { Id = 4, AnswerString = "", QuestionId = 4 }  },
                new Question { Id = 5, QuestionString = "Vraag 5", Answer = new Answer() { Id = 5, AnswerString = "", QuestionId = 5 }  },
                new Question { Id = 6, QuestionString = "Vraag 6", Answer = new Answer() { Id = 6, AnswerString = "", QuestionId = 6 }  },
                new Question { Id = 7, QuestionString = "Vraag 7", Answer = new Answer() { Id = 7, AnswerString = "", QuestionId = 7 }  },
                new Question { Id = 8, QuestionString = "Vraag 8", Answer = new Answer() { Id = 8, AnswerString = "", QuestionId = 8 }  },
                new Question { Id = 9, QuestionString = "Vraag 9", Answer = new Answer() { Id = 9, AnswerString = "", QuestionId = 9 } },
                new Question { Id = 10, QuestionString = "Vraag 10", Answer = new Answer() { Id = 10, AnswerString = "", QuestionId = 10 } },
            };
        }
    }
}
