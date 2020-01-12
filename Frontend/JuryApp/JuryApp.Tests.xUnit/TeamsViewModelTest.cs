using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Services;
using JuryApp.ViewModels;
using Moq;
using System;
using Windows.UI.Xaml.Media.Animation;
using Xunit;

namespace JuryApp.Tests.XUnit
{
    public class TeamsViewModelTest
    {
        private Teams _teams;
        private Mock<ITeamService> _teamServiceMock;
        private Mock<INavigationServiceEx> _navigationServiceExMock;
        private TeamsViewModel _sut;

        public TeamsViewModelTest()
        {
            _teams = GenerateTeamList();

            _teamServiceMock = new Mock<ITeamService>();
            _navigationServiceExMock = new Mock<INavigationServiceEx>();
            _teamServiceMock.Setup(tS => tS.GetAllTeams(It.IsAny<bool>())).ReturnsAsync(_teams);
            _navigationServiceExMock.Setup(nS => nS.Navigate(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<NavigationTransitionInfo>())).Returns(true);
            
            _sut = new TeamsViewModel(_teamServiceMock.Object, _navigationServiceExMock.Object);
        }

        [Fact]
        public void Constructor_ShouldLoadTeams()
        {
            //Act
            var sut = new TeamsViewModel(_teamServiceMock.Object, _navigationServiceExMock.Object);

            //Assert
            Assert.Equal(_teams, sut.Teams);
            _teamServiceMock.Verify(tS => tS.GetAllTeams(It.IsAny<bool>()), Times.AtLeastOnce); //Normally it's once but since the ViewModel is created in the Constructor of this test it's twice
        }

        [Fact]
        public void EditTeamCommand_ShouldNavigateToEditTeamPage_IfSelectedIndexIsNotMinusOne()
        {
            //Act
            _sut.EditTeamCommand.Execute(new Random().Next(_sut.Teams.Count));

            //Assert
            _navigationServiceExMock.Verify(nS => nS.Navigate(typeof(EditTeamViewModel).FullName, It.IsAny<object>(), It.IsAny<NavigationTransitionInfo>()), Times.Once);
        }

        [Fact]
        public void EditTeamCommand_ShouldDoNothing_IfSelectedIndexIsMinusOne()
        {
            //Act
            _sut.EditTeamCommand.Execute(-1);

            //Assert
            _navigationServiceExMock.Verify(nS => nS.Navigate(typeof(EditTeamViewModel).FullName, It.IsAny<object>(), It.IsAny<NavigationTransitionInfo>()), Times.Never);
        }

        private Teams GenerateTeamList()
        {
            return new Teams()
            {
                new Team() { TeamId = 1, TeamName = "Team 1", TeamEnabled = true, TeamScore = 30, TeamAnswers = GenerateAnswerList(0)  },
                new Team() { TeamId = 2, TeamName = "Team 2", TeamEnabled = true, TeamScore = 20, TeamAnswers = GenerateAnswerList(1)  },
                new Team() { TeamId = 3, TeamName = "Team 3", TeamEnabled = true, TeamScore = 10, TeamAnswers = GenerateAnswerList(2)  },
                new Team() { TeamId = 4, TeamName = "Team 4", TeamEnabled = true, TeamScore = 0, TeamAnswers = GenerateAnswerList(3)  }
            };
        }

        private Answers GenerateAnswerList(int multiplier)
        {
            return new Answers()
            {
                new Answer() { AnswerId = 1 + (10 * multiplier), AnswerText = "Antwoord 1", AnswerQuestion = new Question() { QuestionId = 1, QuestionText = "Vraag 1", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 1, CorrectAnswerText = "Voorbeeldantwoord 1" } } } },
                new Answer() { AnswerId = 2 + (10 * multiplier), AnswerText = "Antwoord 2", AnswerQuestion = new Question() { QuestionId = 2, QuestionText = "Vraag 2", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 2, CorrectAnswerText = "Voorbeeldantwoord 2" } } } },
                new Answer() { AnswerId = 3 + (10 * multiplier), AnswerText = "Antwoord 3", AnswerQuestion = new Question() { QuestionId = 3, QuestionText = "Vraag 3", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 3, CorrectAnswerText = "Voorbeeldantwoord 3" } } } },
                new Answer() { AnswerId = 4 + (10 * multiplier), AnswerText = "Antwoord 4", AnswerQuestion = new Question() { QuestionId = 4, QuestionText = "Vraag 4", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 4, CorrectAnswerText = "Voorbeeldantwoord 4" } } } },
                new Answer() { AnswerId = 5 + (10 * multiplier), AnswerText = "Antwoord 5", AnswerQuestion = new Question() { QuestionId = 5, QuestionText = "Vraag 5", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 5, CorrectAnswerText = "Voorbeeldantwoord 5" } } } },
                new Answer() { AnswerId = 6 + (10 * multiplier), AnswerText = "Antwoord 6", AnswerQuestion = new Question() { QuestionId = 6, QuestionText = "Vraag 6", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 6, CorrectAnswerText = "Voorbeeldantwoord 6" } } } },
                new Answer() { AnswerId = 7 + (10 * multiplier), AnswerText = "Antwoord 7", AnswerQuestion = new Question() { QuestionId = 7, QuestionText = "Vraag 7", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 7, CorrectAnswerText = "Voorbeeldantwoord 7" } } } },
                new Answer() { AnswerId = 8 + (10 * multiplier), AnswerText = "Antwoord 8", AnswerQuestion = new Question() { QuestionId = 8, QuestionText = "Vraag 8", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 8, CorrectAnswerText = "Voorbeeldantwoord 8" } } } },
                new Answer() { AnswerId = 9 + (10 * multiplier), AnswerText = "Antwoord 9", AnswerQuestion = new Question() { QuestionId = 9, QuestionText = "Vraag 9", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 9, CorrectAnswerText = "Voorbeeldantwoord 9" } } } },
                new Answer() { AnswerId = 10 + (10 * multiplier), AnswerText = "Antwoord 10", AnswerQuestion = new Question() { QuestionId = 10, QuestionText = "Vraag 10", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 10, CorrectAnswerText = "Voorbeeldantwoord 10" } } } }
            };
        }
    }
}
