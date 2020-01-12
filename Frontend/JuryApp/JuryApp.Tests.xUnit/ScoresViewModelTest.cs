using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Services;
using JuryApp.ViewModels;
using Moq;
using System.Linq;
using Xunit;

namespace JuryApp.Tests.XUnit
{
    public class ScoresViewModelTest
    {
        private Teams _teams;
        private Teams _enabledTeams;
        private Mock<ITeamService> _teamServiceMock;
        private Mock<INavigationServiceEx> _navigationServiceExMock;

        public ScoresViewModelTest()
        {
            _teams = GenerateTeamList();
            _enabledTeams = new Teams();

            foreach(Team team in _teams)
            {
                if (team.TeamEnabled)
                {
                    _enabledTeams.Add(team);
                }
            }

            _navigationServiceExMock = new Mock<INavigationServiceEx>();
            _teamServiceMock = new Mock<ITeamService>();
            _teamServiceMock.Setup(tS => tS.GetAllTeams(It.IsAny<bool>())).ReturnsAsync(_teams);
            _navigationServiceExMock = new Mock<INavigationServiceEx>();
        }

        [Fact]
        public void Constructor_ShouldLoadEnabledTeams()
        {
            //Act
            var sut = new ScoresViewModel(_teamServiceMock.Object, _navigationServiceExMock.Object);

            //Assert
            Assert.Equal(_enabledTeams, sut.Teams);
            _teamServiceMock.Verify(tS => tS.GetAllTeams(It.IsAny<bool>()), Times.Once);
        }

        private Teams GenerateTeamList()
        {
            return new Teams()
            {
                new Team() { TeamId = 1, TeamName = "Team 1", TeamEnabled = true, TeamScore = 30, TeamAnswers = GenerateAnswerList(0)  },
                new Team() { TeamId = 2, TeamName = "Team 2", TeamEnabled = true, TeamScore = 20, TeamAnswers = GenerateAnswerList(1)  },
                new Team() { TeamId = 3, TeamName = "Team 3", TeamEnabled = true, TeamScore = 10, TeamAnswers = GenerateAnswerList(2)  },
                new Team() { TeamId = 4, TeamName = "Team 4", TeamEnabled = false, TeamScore = 0, TeamAnswers = GenerateAnswerList(3)  }
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
