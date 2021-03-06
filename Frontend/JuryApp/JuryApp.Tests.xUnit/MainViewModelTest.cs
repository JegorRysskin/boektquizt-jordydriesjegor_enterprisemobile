﻿using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Services;
using JuryApp.ViewModels;
using Moq;
using System;
using System.Linq;
using Windows.UI.Xaml.Media.Animation;
using Xunit;

namespace JuryApp.Tests.XUnit
{
    public class MainViewModelTest
    {
        private Rounds _rounds;
        private Quizzes _quizzes;
        private Mock<IRoundService> _roundServiceMock;
        private Mock<INavigationServiceEx> _navigationServiceExMock;
        private Mock<ITeamService> _teamServiceMock;
        private Round _selectedRound;
        private MainViewModel _sut;

        public MainViewModelTest()
        {
            _quizzes = GenerateQuizzesList();
            _rounds = _quizzes.Where(q => q.QuizEnabled).FirstOrDefault().QuizRounds;
            _selectedRound = _rounds[new Random().Next(_rounds.Count)];

            _teamServiceMock = new Mock<ITeamService>();
            _roundServiceMock = new Mock<IRoundService>();
            _roundServiceMock.Setup(rS => rS.GetAllRoundsByEnabledQuiz()).ReturnsAsync(_rounds);
            _roundServiceMock.Setup(rS => rS.EditRound(It.IsAny<int>(), It.IsAny<Round>())).ReturnsAsync(true);
            _navigationServiceExMock = new Mock<INavigationServiceEx>();
            _navigationServiceExMock.Setup(nS => nS.Navigate(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<NavigationTransitionInfo>())).Returns(true);

            _sut = new MainViewModel(_teamServiceMock.Object,_roundServiceMock.Object, _navigationServiceExMock.Object);
        }

        [Fact]
        public void Constructor_ShouldLoadRoundsFromEnabledQuiz()
        {
            //Act
            var sut = new MainViewModel(_teamServiceMock.Object,_roundServiceMock.Object, _navigationServiceExMock.Object);

            //Assert
            Assert.Equal(_rounds, sut.Rounds);
            _roundServiceMock.Verify(rS => rS.GetAllRoundsByEnabledQuiz(), Times.AtLeastOnce); //Normally it's once but since the ViewModel is created in the Constructor of this test it's twice
        }

        [Fact]
        public void EnableRoundCommand_ShouldEnableSelectedRoundAndDisableOtherRounds_IfSelectedRoundIsNotNull()
        {
            //Act
            _sut.EnableRoundCommand.Execute(_selectedRound);
            var otherRounds = _sut.Rounds.Except(new Rounds() { _selectedRound });

            //Assert
            Assert.True(_selectedRound.RoundEnabled);
            Assert.True(otherRounds.All(r => !r.RoundEnabled));
            _roundServiceMock.Verify(rS => rS.EditRound(It.IsAny<int>(), It.IsAny<Round>()), Times.Exactly(_sut.Rounds.Count));
        }

        [Fact]
        public void EnableRoundCommand_ShouldDoNothing_IfSelectedRoundIsNull()
        {
            //Act
            _sut.EnableRoundCommand.Execute(null);

            //Assert
            _roundServiceMock.Verify(rS => rS.EditRound(It.IsAny<int>(), It.IsAny<Round>()), Times.Never);
        }

        [Fact]
        public void DisableAllRoundsCommand_ShouldDisableAllRounds()
        {
            //Act
            _sut.DisableAllRoundsCommand.Execute(null);

            //Assert
            Assert.True(_sut.Rounds.All(r => !r.RoundEnabled));
            _roundServiceMock.Verify(rS => rS.EditRound(It.IsAny<int>(), It.IsAny<Round>()), Times.Exactly(_sut.Rounds.Count));
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
