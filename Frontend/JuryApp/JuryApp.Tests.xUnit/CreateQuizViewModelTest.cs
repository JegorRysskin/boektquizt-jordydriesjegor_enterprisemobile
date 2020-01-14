using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Services;
using JuryApp.ViewModels;
using Moq;
using System;
using System.Linq;
using Windows.UI.Xaml.Media.Animation;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace JuryApp.Tests.XUnit
{
    public class CreateQuizViewModelTest
    {
        private Quizzes _quizzes;
        private Mock<IQuizService> _quizServiceMock;
        private Mock<INavigationServiceEx> _navigationServiceExMock;
        private QuizzenViewModel _sender;
        private CreateQuizViewModel _sut;
        private Random _random = new Random();

        public CreateQuizViewModelTest()
        {
            _quizzes = GenerateQuizzesList();

            _quizServiceMock = new Mock<IQuizService>();
            _quizServiceMock.Setup(qS => qS.GetAllQuizzes()).ReturnsAsync(_quizzes);
            _quizServiceMock.Setup(qS => qS.AddQuiz(It.IsAny<Quiz>())).ReturnsAsync(true);
            _navigationServiceExMock = new Mock<INavigationServiceEx>();
            _navigationServiceExMock.Setup(nS => nS.Navigate(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<NavigationTransitionInfo>())).Returns(true);

            _sender = new QuizzenViewModel(_quizServiceMock.Object, _navigationServiceExMock.Object);
            _sender.CreateQuizCommand.Execute(null);
            _sut = new CreateQuizViewModel(_quizServiceMock.Object, _navigationServiceExMock.Object);
        }

        [Fact]
        public void CreateNewQuizCommand_ShouldAddNewQuizAndNavigateToQuizzenViewModel()
        {
            //Act
            _sut.CreateNewQuizCommand.Execute(null);

            //Assert
            _quizServiceMock.Verify(qS => qS.AddQuiz(It.IsAny<Quiz>()), Times.Once);
            _navigationServiceExMock.Verify(nS => nS.Navigate(typeof(QuizzenViewModel).FullName, It.IsAny<object>(), It.IsAny<NavigationTransitionInfo>()), Times.Once);
        }

        [Fact]
        public void CreateRoundsCommand_ShouldAddNumberOfRoundsWithEachTenQuestionsProvidedByComboBoxToNewQuiz()
        {
            //Arrange
            var amountOfRoundsToCreate = _sut.ListOfTenForComboBox[_random.Next(_sut.ListOfTenForComboBox.Count)];

            //Act
            _sut.CreateRoundsCommand.Execute(amountOfRoundsToCreate);

            //Assert
            Assert.Equal(amountOfRoundsToCreate, _sut.NewQuiz.QuizRounds.Count);
            Assert.True(_sut.NewQuiz.QuizRounds.All(qR => qR.RoundQuestions.Count == 10));
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
