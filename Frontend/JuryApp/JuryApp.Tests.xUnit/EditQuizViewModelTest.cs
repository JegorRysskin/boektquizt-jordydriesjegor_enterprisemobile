using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Helpers;
using JuryApp.Services;
using JuryApp.ViewModels;
using Moq;
using System;
using System.Linq;
using Windows.UI.Xaml.Media.Animation;
using Xunit;

namespace JuryApp.Tests.XUnit
{
    public class EditQuizViewModelTest
    {
        private Quizzes _quizzes;
        private int _selectedIndex;
        private Quiz _selectedQuiz;
        private Mock<IQuizService> _quizServiceMock;
        private Mock<INavigationServiceEx> _navigationServiceExMock;
        private Mock<IMessengerCache> _messengerCacheMock;
        private QuizzenViewModel _sender;
        private EditQuizViewModel _sut;
        private Random _random = new Random();

        public EditQuizViewModelTest()
        {
            _quizzes = GenerateQuizzesList();
            _selectedIndex = _random.Next(_quizzes.Count);
            _selectedQuiz = _quizzes[_selectedIndex];

            _quizServiceMock = new Mock<IQuizService>();
            _quizServiceMock.Setup(qS => qS.GetAllQuizzes(It.IsAny<bool>())).ReturnsAsync(_quizzes);
            _quizServiceMock.Setup(qS => qS.DeleteQuiz(It.IsAny<int>())).ReturnsAsync(true);
            _quizServiceMock.Setup(qS => qS.EditQuiz(It.IsAny<int>(), It.IsAny<Quiz>())).ReturnsAsync(true);
            _navigationServiceExMock = new Mock<INavigationServiceEx>();
            _navigationServiceExMock.Setup(nS => nS.GoBack()).Returns(true);
            _navigationServiceExMock.Setup(nS => nS.Navigate(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<NavigationTransitionInfo>())).Returns(true);
            _messengerCacheMock = new Mock<IMessengerCache>();
            _messengerCacheMock.Setup(mC => mC.CachedSelectedQuiz).Returns(_selectedQuiz);

            _sender = new QuizzenViewModel(_quizServiceMock.Object, _navigationServiceExMock.Object);
            _sender.EditQuizCommand.Execute(_selectedIndex);
            _sut = new EditQuizViewModel(_quizServiceMock.Object, _navigationServiceExMock.Object, _messengerCacheMock.Object);
        }

        [Fact]
        public void Constructor_ShouldLoadSelectedQuizAndAllQuizzes()
        {
            //Act
            var sut = new EditQuizViewModel(_quizServiceMock.Object, _navigationServiceExMock.Object, _messengerCacheMock.Object);

            //Assert
            Assert.Equal(_selectedQuiz, sut.SelectedQuiz);
            Assert.Equal(_quizzes, sut.AllQuizzes);
            _quizServiceMock.Verify(qS => qS.GetAllQuizzes(It.IsAny<bool>()), Times.Exactly(3)); /*Normally it should be once but the _sender constructor in this test's constructor calls the function one time, 
                                                                                                 the _sut constructor in this test's constructor calls it a second time and this test calls it a third time*/
        }

        [Fact]
        public void DeleteQuizCommand_ShouldDeleteSelectedQuizAndGoBack()
        {
            //Act
            _sut.DeleteQuizCommand.Execute(null);

            //Assert
            _quizServiceMock.Verify(qS => qS.DeleteQuiz(_selectedQuiz.QuizId), Times.Once);
            _navigationServiceExMock.Verify(nS => nS.GoBack(), Times.Once);
        }

        [Fact]
        public void EditQuizCommand_ShouldEditSelectedQuizDisableAllOtherQuizzesAndGoBack()
        {
            //Arrange
            _sut.SelectedQuiz.QuizEnabled = true;
            var otherQuizzes = _sender.Quizzes.Except(new Quizzes() { _sut.SelectedQuiz });

            /*In the unlikely event that the other quizzes should be enabled, the EditQuizCommand should still disable these quizzes */
            foreach(Quiz otherQuiz in otherQuizzes)
            {
                otherQuiz.QuizEnabled = true;
            }

            //Act
            _sut.EditQuizCommand.Execute(null);

            //Assert
            Assert.Equal(_sender.Quizzes[_selectedIndex], _sut.SelectedQuiz);
            Assert.True(otherQuizzes.All(oQ => !oQ.QuizEnabled));
            _quizServiceMock.Verify(qS => qS.EditQuiz(_sut.SelectedQuiz.QuizId, _sut.SelectedQuiz), Times.Once); //To check if the SelectedQuiz is updated
            _quizServiceMock.Verify(qS => qS.EditQuiz(It.IsAny<int>(), It.IsAny<Quiz>()), Times.Exactly(_quizzes.Count)); //To check if the otherQuizzes are disabled
            _navigationServiceExMock.Verify(nS => nS.GoBack(), Times.Once);
        }

        [Fact]
        public void NavigateToRoundCommand_ShouldNavigateToRoundViewModel_IfSelectedRoundIsNotNull()
        {
            //Arrange
            var selectedRoundIndex = _random.Next(_sut.SelectedQuiz.QuizRounds.Count);

            //Act
            _sut.NavigateToRoundCommand.Execute(_sut.SelectedQuiz.QuizRounds[selectedRoundIndex]);

            //Assert
            _navigationServiceExMock.Verify(nS => nS.Navigate(typeof(RoundViewModel).FullName, It.IsAny<object>(), It.IsAny<NavigationTransitionInfo>()), Times.Once);
        }

        [Fact]
        public void NavigateToRoundCommand_ShouldDoNothing_IfSelectedRoundIsNull()
        {
            //Act
            _sut.NavigateToRoundCommand.Execute(null);

            //Assert
            _navigationServiceExMock.Verify(nS => nS.Navigate(typeof(RoundViewModel).FullName, It.IsAny<object>(), It.IsAny<NavigationTransitionInfo>()), Times.Never);
        }

        private Quizzes GenerateQuizzesList()
        {
            return new Quizzes()
            {
                new Quiz() { QuizId = 1, QuizName = "Quiz 1", QuizEnabled = false, QuizRounds = GenerateRoundsList(0) },
                new Quiz() { QuizId = 2, QuizName = "Quiz 2", QuizEnabled = false, QuizRounds = GenerateRoundsList(1) },
                new Quiz() { QuizId = 3, QuizName = "Quiz 3", QuizEnabled = false, QuizRounds = GenerateRoundsList(2) },
            };
        }

        private Rounds GenerateRoundsList(int multiplier)
        {
            return new Rounds()
            {
                new Round() { RoundId = 1 + (10 * multiplier), RoundName = "Ronde 1", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(0 + (8 * multiplier)) },
                new Round() { RoundId = 2 + (10 * multiplier), RoundName = "Ronde 2", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(1 + (8 * multiplier)) },
                new Round() { RoundId = 3 + (10 * multiplier), RoundName = "Ronde 3", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(2 + (8 * multiplier)) },
                new Round() { RoundId = 4 + (10 * multiplier), RoundName = "Ronde 4", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(3 + (8 * multiplier)) },
                new Round() { RoundId = 5 + (10 * multiplier), RoundName = "Ronde 5", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(4 + (8 * multiplier)) },
                new Round() { RoundId = 6 + (10 * multiplier), RoundName = "Ronde 6", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(5 + (8 * multiplier)) },
                new Round() { RoundId = 7 + (10 * multiplier), RoundName = "Ronde 7", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(6 + (8 * multiplier)) },
                new Round() { RoundId = 8 + (10 * multiplier), RoundName = "Ronde 8", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(7 + (8 * multiplier)) },
            };
        }

        private Questions GenerateQuestionsList(int multiplier)
        {
            return new Questions()
            {
                new Question() { QuestionId = 1 + (10 * multiplier), QuestionText = "Vraag 1", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 1 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 1" } } },
                new Question() { QuestionId = 2 + (10 * multiplier), QuestionText = "Vraag 2", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 2 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 2" } } },
                new Question() { QuestionId = 3 + (10 * multiplier), QuestionText = "Vraag 3", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 3 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 3" } } },
                new Question() { QuestionId = 4 + (10 * multiplier), QuestionText = "Vraag 4", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 4 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 4" } } },
                new Question() { QuestionId = 5 + (10 * multiplier), QuestionText = "Vraag 5", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 5 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 5" } } },
                new Question() { QuestionId = 6 + (10 * multiplier), QuestionText = "Vraag 6", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 6 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 6" } } },
                new Question() { QuestionId = 7 + (10 * multiplier), QuestionText = "Vraag 7", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 7 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 7" } } },
                new Question() { QuestionId = 8 + (10 * multiplier), QuestionText = "Vraag 8", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 8 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 8" } } },
                new Question() { QuestionId = 9 + (10 * multiplier), QuestionText = "Vraag 9", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 9 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 9" } } },
                new Question() { QuestionId = 10 + (10 * multiplier), QuestionText = "Vraag 10", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 10 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 10" } } }
            };
        }
    }
}
