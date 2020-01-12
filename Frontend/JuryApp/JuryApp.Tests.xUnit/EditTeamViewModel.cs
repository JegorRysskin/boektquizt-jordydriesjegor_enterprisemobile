using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Helpers;
using JuryApp.Services;
using JuryApp.ViewModels;
using Moq;
using System;
using Windows.UI.Xaml.Media.Animation;
using Xunit;

namespace JuryApp.Tests.XUnit
{
    public class EditTeamViewModelTest
    {
        private Teams _teams;
        private Mock<ITeamService> _teamServiceMock;
        private Mock<INavigationServiceEx> _navigationServiceExMock;
        private Mock<IMessengerCache> _messengerCacheMock;
        private TeamsViewModel _sender;
        private int _selectedIndex;
        private Team _selectedTeam;
        private EditTeamViewModel _sut;

        public EditTeamViewModelTest()
        {
            _teams = GenerateTeamList();
            _selectedIndex = new Random().Next(_teams.Count);
            _selectedTeam = _teams[_selectedIndex];

            _teamServiceMock = new Mock<ITeamService>();
            _navigationServiceExMock = new Mock<INavigationServiceEx>();
            _messengerCacheMock = new Mock<IMessengerCache>();
            _teamServiceMock.Setup(tS => tS.GetAllTeams(It.IsAny<bool>())).ReturnsAsync(_teams);
            _teamServiceMock.Setup(tS => tS.DeleteTeam(It.IsAny<int>())).ReturnsAsync(true);
            _teamServiceMock.Setup(tS => tS.EditTeam(It.IsAny<int>(), It.IsAny<Team>())).ReturnsAsync(true);
            _navigationServiceExMock.Setup(nS => nS.Navigate(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<NavigationTransitionInfo>())).Returns(true);
            _navigationServiceExMock.Setup(nS => nS.GoBack()).Returns(true);
            _messengerCacheMock.Setup(mC => mC.CachedSelectedTeam).Returns(_selectedTeam);

            _sender = new TeamsViewModel(_teamServiceMock.Object, _navigationServiceExMock.Object);
            _sender.EditTeamCommand.Execute(_selectedIndex);
            _sut = new EditTeamViewModel(_teamServiceMock.Object, _navigationServiceExMock.Object, _messengerCacheMock.Object);
        }

        [Fact]
        public void Constructor_ShouldLoadTeam()
        {
            //Act
            var sut = new EditTeamViewModel(_teamServiceMock.Object, _navigationServiceExMock.Object, _messengerCacheMock.Object);

            //Assert
            Assert.Equal(_selectedTeam, sut.SelectedTeam);
            _teamServiceMock.Verify(tS => tS.GetAllTeams(It.IsAny<bool>()), Times.AtLeastOnce); //Normally it's once but since the ViewModel is created in the Constructor of this test it's twice
        }

        [Fact]
        public void EditTeamCommand_ShouldEditTeamAndGoBack()
        {
            //Act
            _sut.EditTeamCommand.Execute(null);

            //Assert
            _teamServiceMock.Verify(tS => tS.EditTeam(It.IsAny<int>(), It.IsAny<Team>()), Times.Once);
            _navigationServiceExMock.Verify(nS => nS.GoBack(), Times.Once);
        }

        [Fact]
        public void DeleteTeamCommand_ShouldDeleteTeamAndGoBack()
        {
            //Act
            _sut.DeleteTeamCommand.Execute(null);

            //Assert
            _teamServiceMock.Verify(tS => tS.DeleteTeam(It.IsAny<int>()), Times.Once);
            _navigationServiceExMock.Verify(nS => nS.GoBack(), Times.Once);
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
                new Answer() { AnswerId = 1 + (10 * multiplier), AnswerText = "Antwoord 1", AnswerQuestion = new Question() { QuestionId = 1 + (10 * multiplier), QuestionText = "Vraag 1", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 1 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 1" } } } },
                new Answer() { AnswerId = 2 + (10 * multiplier), AnswerText = "Antwoord 2", AnswerQuestion = new Question() { QuestionId = 2 + (10 * multiplier), QuestionText = "Vraag 2", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 2 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 2" } } } },
                new Answer() { AnswerId = 3 + (10 * multiplier), AnswerText = "Antwoord 3", AnswerQuestion = new Question() { QuestionId = 3 + (10 * multiplier), QuestionText = "Vraag 3", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 3 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 3" } } } },
                new Answer() { AnswerId = 4 + (10 * multiplier), AnswerText = "Antwoord 4", AnswerQuestion = new Question() { QuestionId = 4 + (10 * multiplier), QuestionText = "Vraag 4", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 4 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 4" } } } },
                new Answer() { AnswerId = 5 + (10 * multiplier), AnswerText = "Antwoord 5", AnswerQuestion = new Question() { QuestionId = 5 + (10 * multiplier), QuestionText = "Vraag 5", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 5 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 5" } } } },
                new Answer() { AnswerId = 6 + (10 * multiplier), AnswerText = "Antwoord 6", AnswerQuestion = new Question() { QuestionId = 6 + (10 * multiplier), QuestionText = "Vraag 6", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 6 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 6" } } } },
                new Answer() { AnswerId = 7 + (10 * multiplier), AnswerText = "Antwoord 7", AnswerQuestion = new Question() { QuestionId = 7 + (10 * multiplier), QuestionText = "Vraag 7", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 7 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 7" } } } },
                new Answer() { AnswerId = 8 + (10 * multiplier), AnswerText = "Antwoord 8", AnswerQuestion = new Question() { QuestionId = 8 + (10 * multiplier), QuestionText = "Vraag 8", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 8 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 8" } } } },
                new Answer() { AnswerId = 9 + (10 * multiplier), AnswerText = "Antwoord 9", AnswerQuestion = new Question() { QuestionId = 9 + (10 * multiplier), QuestionText = "Vraag 9", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 9 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 9" } } } },
                new Answer() { AnswerId = 10 + (10 * multiplier), AnswerText = "Antwoord 10", AnswerQuestion = new Question() { QuestionId = 10 + (10 * multiplier), QuestionText = "Vraag 10", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 10 + (10 * multiplier), CorrectAnswerText = "Voorbeeldantwoord 10" } } } }
            };
        }
    }
}
