
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
    // TODO WTS: Add appropriate tests
    public class RoundViewModelTest
    {
        private Rounds _rounds;
        private Quizzes _quizzes;
        private int _selectedQuizIndex;
        private Quiz _selectedQuiz;
        private int _selectedRoundIndex;
        private Round _selectedRound;
        private Mock<IQuizService> _quizServiceMock;
        private Mock<IRoundService> _roundServiceMock;
        private Mock<INavigationServiceEx> _navigationServiceExMock;
        private Mock<IMessengerCache> _messengerCacheMock;
        private QuizzenViewModel _sender;
        private EditQuizViewModel _intermediate;
        private RoundViewModel _sut;
        private Random _random = new Random();

        public RoundViewModelTest()
        {
            _quizzes = GenerateQuizzesList();
            _selectedQuizIndex = _random.Next(_quizzes.Count);
            _selectedQuiz = _quizzes[_selectedQuizIndex];
            _selectedRoundIndex = _random.Next(_selectedQuiz.QuizRounds.Count);
            _selectedRound = _selectedQuiz.QuizRounds[_selectedRoundIndex];

            _quizServiceMock = new Mock<IQuizService>();
            _quizServiceMock.Setup(qS => qS.GetAllQuizzes()).ReturnsAsync(_quizzes);
            _quizServiceMock.Setup(qS => qS.DeleteQuiz(It.IsAny<int>())).ReturnsAsync(true);
            _quizServiceMock.Setup(qS => qS.EditQuiz(It.IsAny<int>(), It.IsAny<Quiz>())).ReturnsAsync(true);
            _roundServiceMock = new Mock<IRoundService>();
            _roundServiceMock.Setup(rS => rS.EditRound(It.IsAny<int>(), It.IsAny<Round>())).ReturnsAsync(true);
            _navigationServiceExMock = new Mock<INavigationServiceEx>();
            _navigationServiceExMock.Setup(nS => nS.GoBack()).Returns(true);
            _navigationServiceExMock.Setup(nS => nS.Navigate(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<NavigationTransitionInfo>())).Returns(true);
            _messengerCacheMock = new Mock<IMessengerCache>();
            _messengerCacheMock.Setup(mC => mC.CachedSelectedQuiz).Returns(_selectedQuiz);
            _messengerCacheMock.Setup(mC => mC.CachedSelectedRound).Returns(_selectedRound);

            _sender = new QuizzenViewModel(_quizServiceMock.Object, _navigationServiceExMock.Object);
            _sender.EditQuizCommand.Execute(_selectedQuizIndex);
            _intermediate = new EditQuizViewModel(_quizServiceMock.Object, _navigationServiceExMock.Object, _messengerCacheMock.Object);
            _intermediate.NavigateToRoundCommand.Execute(_selectedRound);
            _sut = new RoundViewModel(_roundServiceMock.Object, _navigationServiceExMock.Object, _messengerCacheMock.Object);
        }

        [Fact]
        public void Constructor_ShouldLoadSelectedRound()
        {
            //Act
            var sut = new RoundViewModel(_roundServiceMock.Object, _navigationServiceExMock.Object, _messengerCacheMock.Object);

            //Assert
            Assert.Equal(_selectedRound, sut.SelectedRound);
        }

        [Fact]
        public void AddNewAnswerCommand_ShouldAddANewCorrectAnswerWithEmptyCorrectAnswerText_IfSelectedQuestionIndexIsNotMinusOne()
        {
            //Arrange
            var selectedQuestionIndex = _random.Next(_sut.SelectedRound.RoundQuestions.Count);
            var numberOfCorrectAnswersBefore = _sut.SelectedRound.RoundQuestions[selectedQuestionIndex].QuestionCorrectAnswers.Count;

            //Act
            _sut.AddNewAnswerCommand.Execute(selectedQuestionIndex);
            var numberOfCorrectAnswersAfter = _sut.SelectedRound.RoundQuestions[selectedQuestionIndex].QuestionCorrectAnswers.Count;

            //Assert
            Assert.Equal(numberOfCorrectAnswersBefore + 1, numberOfCorrectAnswersAfter);
            Assert.Equal(_sut.SelectedRound.RoundQuestions[selectedQuestionIndex].QuestionCorrectAnswers.Last().CorrectAnswerText, String.Empty);
        }

        [Fact]
        public void AddNewAnswerCommand_ShouldDoNothing_IfSelectedQuestionIndexIsMinusOne()
        {
            //Arrange
            var selectedQuestionIndex = -1;
            var totalOfCorrectAnswersBefore = _sut.SelectedRound.RoundQuestions.Sum(rQ => rQ.QuestionCorrectAnswers.Count);

            //Act
            _sut.AddNewAnswerCommand.Execute(selectedQuestionIndex);
            var totalOfCorrectAnswersAfter = _sut.SelectedRound.RoundQuestions.Sum(rQ => rQ.QuestionCorrectAnswers.Count);

            //Assert
            Assert.Equal(totalOfCorrectAnswersBefore, totalOfCorrectAnswersAfter);
        }

        [Fact]
        public void SaveRoundCommand_ShouldEditRoundAndGoBack()
        {
            //Arrange
            var selectedQuestionIndex = _random.Next(_sut.SelectedRound.RoundQuestions.Count);
            var correctAnswer = Guid.NewGuid().ToString();
            _sut.SelectedRound.RoundQuestions[selectedQuestionIndex].QuestionCorrectAnswers.Last().CorrectAnswerText = correctAnswer;

            //Act
            _sut.SaveRoundCommand.Execute(null);

            //Assert
            Assert.Equal(correctAnswer, _intermediate.SelectedQuiz.QuizRounds[_selectedRoundIndex].RoundQuestions[selectedQuestionIndex].QuestionCorrectAnswers.Last().CorrectAnswerText); //Check if the CorrectAnswerText is edited in the previous viewmodel (EditQuizViewModel)
            _roundServiceMock.Verify(rS => rS.EditRound(_sut.SelectedRound.RoundId, _sut.SelectedRound), Times.Once);
        }

        private Quizzes GenerateQuizzesList()
        {
            return new Quizzes()
            {
                new Quiz() { QuizId = 1, QuizName = "Quiz 1", QuizEnabled = true, QuizRounds = GenerateRoundsList(0) },
                new Quiz() { QuizId = 2, QuizName = "Quiz 2", QuizEnabled = false, QuizRounds = GenerateRoundsList(1) },
                new Quiz() { QuizId = 3, QuizName = "Quiz 3", QuizEnabled = false, QuizRounds = GenerateRoundsList(2) },
            };
        }

        private Rounds GenerateRoundsList(int multiplier)
        {
            return new Rounds()
            {
                new Round() { RoundId = 1 + (8 * multiplier), RoundName = "Ronde 1", RoundEnabled = true, RoundQuestions = GenerateQuestionsList(0 + (8 * multiplier)) },
                new Round() { RoundId = 2 + (8 * multiplier), RoundName = "Ronde 2", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(1 + (8 * multiplier)) },
                new Round() { RoundId = 3 + (8 * multiplier), RoundName = "Ronde 3", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(2 + (8 * multiplier)) },
                new Round() { RoundId = 4 + (8 * multiplier), RoundName = "Ronde 4", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(3 + (8 * multiplier)) },
                new Round() { RoundId = 5 + (8 * multiplier), RoundName = "Ronde 5", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(4 + (8 * multiplier)) },
                new Round() { RoundId = 6 + (8 * multiplier), RoundName = "Ronde 6", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(5 + (8 * multiplier)) },
                new Round() { RoundId = 7 + (8 * multiplier), RoundName = "Ronde 7", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(6 + (8 * multiplier)) },
                new Round() { RoundId = 8 + (8 * multiplier), RoundName = "Ronde 8", RoundEnabled = false, RoundQuestions = GenerateQuestionsList(7 + (8 * multiplier)) }
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
