using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;
using JuryApp.Core.Services.Interfaces;
using JuryApp.Services;
using JuryApp.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JuryApp.Tests.XUnit
{
    public class CorrectViewModelTest
    {
        private Teams _teams;
        private Teams _enabledTeams;
        private Quizzes _quizzes;
        private Rounds _rounds;
        private Mock<ITeamService> _teamServiceMock;
        private Mock<IRoundService> _roundServiceMock;
        private Mock<INavigationServiceEx> _navigationServiceExMock;
        private CorrectViewModel _sut;
        private Random _random = new Random();

        public CorrectViewModelTest()
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

            _quizzes = GenerateQuizzesList();
            _rounds = _quizzes.Where(q => q.QuizEnabled).FirstOrDefault().QuizRounds;

            _teamServiceMock = new Mock<ITeamService>();
            _teamServiceMock.Setup(tS => tS.GetAllTeams()).ReturnsAsync(_teams);
            _teamServiceMock.Setup(tS => tS.PatchTeamScore(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
            _roundServiceMock = new Mock<IRoundService>();
            _roundServiceMock.Setup(rS => rS.GetAllRoundsByEnabledQuiz()).ReturnsAsync(_rounds);
            _navigationServiceExMock = new Mock<INavigationServiceEx>();

            _sut = new CorrectViewModel(_teamServiceMock.Object, _roundServiceMock.Object, _navigationServiceExMock.Object);
        }

        [Fact]
        public void Constructor_ShouldLoadEnabledTeamsAndRoundsOfEnabledQuiz()
        {
            //Act
            var sut = new CorrectViewModel(_teamServiceMock.Object, _roundServiceMock.Object, _navigationServiceExMock.Object);

            //Assert
            Assert.Equal(_enabledTeams, sut.EnabledTeams);
            Assert.Equal(_rounds, sut.QuizRounds);
            _teamServiceMock.Verify(tS => tS.GetAllTeams(), Times.AtLeastOnce); //Normally it's once but since the ViewModel is created in the Constructor of this test it's twice
            _roundServiceMock.Verify(rS => rS.GetAllRoundsByEnabledQuiz(), Times.AtLeastOnce); //Normally it's once but since the ViewModel is created in the Constructor of this test it's twice
        }

        [Fact]
        public void GetAnswersSelectedTeamCommand_ShouldGetAnswersFromSelectedTeam_IfSelectedTeamIsNotNull()
        {
            //Arrange
            var selectedTeam = _enabledTeams[_random.Next(_enabledTeams.Count)];

            //Act
            _sut.GetAnswersSelectedTeamCommand.Execute(selectedTeam);

            //Assert
            Assert.Equal(selectedTeam.TeamAnswers, _sut.TeamAnswers);
        }

        [Fact]
        public void GetAnswersSelectedTeamCommand_ShouldDoNothing_IfSelectedTeamIsNull()
        {
            //Act
            _sut.GetAnswersSelectedTeamCommand.Execute(null);

            //Assert
            Assert.Equal(new Answers(), _sut.TeamAnswers);
        }

        [Fact]
        public void GetSelectedRoundCommand_ShouldGetAnswersFromSelectedTeamForThatSpecificRound_IfSelectedRoundIsNotNull()
        {
            //Arrange
            var selectedTeam = _enabledTeams[_random.Next(_enabledTeams.Count)];
            var selectedRound = _rounds[_random.Next(_rounds.Count)];
            var selectedRoundQuestions = selectedTeam.TeamAnswers.Join(selectedRound.RoundQuestions, answer => answer.AnswerQuestion.QuestionId, question => question.QuestionId, (answer, question) => new { Answer = answer, Question = question }).ToList();
            var selectedRoundAnswers = new Answers();

            foreach(var answerQuestion in selectedRoundQuestions)
            {
                selectedRoundAnswers.Add(answerQuestion.Answer);
            }

            //Act
            _sut.GetAnswersSelectedTeamCommand.Execute(selectedTeam);
            _sut.GetSelectedRoundCommand.Execute(selectedRound);

            //Assert
            Assert.Equal(selectedRoundAnswers, _sut.TeamAnswersPerRound);
        }

        [Fact]
        public void GetSelectedRoundCommand_ShouldDoNothing_IfSelectedRoundIsNull()
        {
            //Arrange
            var selectedTeam = _enabledTeams[_random.Next(_enabledTeams.Count)];

            //Act
            _sut.GetAnswersSelectedTeamCommand.Execute(selectedTeam);
            _sut.GetSelectedRoundCommand.Execute(null);

            //Assert
            Assert.Equal(new Answers(), _sut.TeamAnswersPerRound);
        }

        [Fact]
        public void SendScoreCommand_ShouldPatchTeamScore()
        {
            //Arrange
            var selectedTeam = _enabledTeams[_random.Next(_enabledTeams.Count)];
            var teamScoreBefore = selectedTeam.TeamScore;
            var selectedRound = _rounds[_random.Next(_rounds.Count)];
            _sut.GetAnswersSelectedTeamCommand.Execute(selectedTeam);
            _sut.GetSelectedRoundCommand.Execute(selectedRound);
            var listItems = new List<object>();

            for (int i = 0; i < _random.Next(10); i++)
            {
                listItems.Add(new ListItem() { QuestionText = _sut.TeamAnswersPerRound[i].AnswerQuestion.QuestionText, AnswerText = _sut.TeamAnswersPerRound[i].AnswerText, CorrectAnswerText = _sut.TeamAnswersPerRound[i].AnswerQuestion.QuestionCorrectAnswers.First().CorrectAnswerText });
            }

            //Act
            _sut.SendScoreCommand.Execute(listItems);

            //Assert
            Assert.Equal(teamScoreBefore + listItems.Count, selectedTeam.TeamScore);
            _teamServiceMock.Verify(tS => tS.PatchTeamScore(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
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
                new Answer() { AnswerId = 1 + (80 * multiplier), AnswerText = "Antwoord 1", AnswerQuestion = new Question() { QuestionId = 1, QuestionText = "Vraag 1", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 1, CorrectAnswerText = "Voorbeeldantwoord 1" } } } },
                new Answer() { AnswerId = 2 + (80 * multiplier), AnswerText = "Antwoord 2", AnswerQuestion = new Question() { QuestionId = 2, QuestionText = "Vraag 2", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 2, CorrectAnswerText = "Voorbeeldantwoord 2" } } } },
                new Answer() { AnswerId = 3 + (80 * multiplier), AnswerText = "Antwoord 3", AnswerQuestion = new Question() { QuestionId = 3, QuestionText = "Vraag 3", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 3, CorrectAnswerText = "Voorbeeldantwoord 3" } } } },
                new Answer() { AnswerId = 4 + (80 * multiplier), AnswerText = "Antwoord 4", AnswerQuestion = new Question() { QuestionId = 4, QuestionText = "Vraag 4", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 4, CorrectAnswerText = "Voorbeeldantwoord 4" } } } },
                new Answer() { AnswerId = 5 + (80 * multiplier), AnswerText = "Antwoord 5", AnswerQuestion = new Question() { QuestionId = 5, QuestionText = "Vraag 5", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 5, CorrectAnswerText = "Voorbeeldantwoord 5" } } } },
                new Answer() { AnswerId = 6 + (80 * multiplier), AnswerText = "Antwoord 6", AnswerQuestion = new Question() { QuestionId = 6, QuestionText = "Vraag 6", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 6, CorrectAnswerText = "Voorbeeldantwoord 6" } } } },
                new Answer() { AnswerId = 7 + (80 * multiplier), AnswerText = "Antwoord 7", AnswerQuestion = new Question() { QuestionId = 7, QuestionText = "Vraag 7", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 7, CorrectAnswerText = "Voorbeeldantwoord 7" } } } },
                new Answer() { AnswerId = 8 + (80 * multiplier), AnswerText = "Antwoord 8", AnswerQuestion = new Question() { QuestionId = 8, QuestionText = "Vraag 8", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 8, CorrectAnswerText = "Voorbeeldantwoord 8" } } } },
                new Answer() { AnswerId = 9 + (80 * multiplier), AnswerText = "Antwoord 9", AnswerQuestion = new Question() { QuestionId = 9, QuestionText = "Vraag 9", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 9, CorrectAnswerText = "Voorbeeldantwoord 9" } } } },
                new Answer() { AnswerId = 10 + (80 * multiplier), AnswerText = "Antwoord 10", AnswerQuestion = new Question() { QuestionId = 10, QuestionText = "Vraag 10", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 10, CorrectAnswerText = "Voorbeeldantwoord 10" } } } },
                new Answer() { AnswerId = 11 + (80 * multiplier), AnswerText = "Antwoord 11", AnswerQuestion = new Question() { QuestionId = 11, QuestionText = "Vraag 1", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 11, CorrectAnswerText = "Voorbeeldantwoord 11" } } } },
                new Answer() { AnswerId = 12 + (80 * multiplier), AnswerText = "Antwoord 12", AnswerQuestion = new Question() { QuestionId = 12, QuestionText = "Vraag 2", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 12, CorrectAnswerText = "Voorbeeldantwoord 12" } } } },
                new Answer() { AnswerId = 13 + (80 * multiplier), AnswerText = "Antwoord 13", AnswerQuestion = new Question() { QuestionId = 13, QuestionText = "Vraag 3", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 13, CorrectAnswerText = "Voorbeeldantwoord 13" } } } },
                new Answer() { AnswerId = 14 + (80 * multiplier), AnswerText = "Antwoord 14", AnswerQuestion = new Question() { QuestionId = 14, QuestionText = "Vraag 4", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 14, CorrectAnswerText = "Voorbeeldantwoord 14" } } } },
                new Answer() { AnswerId = 15 + (80 * multiplier), AnswerText = "Antwoord 15", AnswerQuestion = new Question() { QuestionId = 15, QuestionText = "Vraag 5", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 15, CorrectAnswerText = "Voorbeeldantwoord 15" } } } },
                new Answer() { AnswerId = 16 + (80 * multiplier), AnswerText = "Antwoord 16", AnswerQuestion = new Question() { QuestionId = 16, QuestionText = "Vraag 6", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 16, CorrectAnswerText = "Voorbeeldantwoord 16" } } } },
                new Answer() { AnswerId = 17 + (80 * multiplier), AnswerText = "Antwoord 17", AnswerQuestion = new Question() { QuestionId = 17, QuestionText = "Vraag 7", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 17, CorrectAnswerText = "Voorbeeldantwoord 17" } } } },
                new Answer() { AnswerId = 18 + (80 * multiplier), AnswerText = "Antwoord 18", AnswerQuestion = new Question() { QuestionId = 18, QuestionText = "Vraag 8", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 18, CorrectAnswerText = "Voorbeeldantwoord 18" } } } },
                new Answer() { AnswerId = 19 + (80 * multiplier), AnswerText = "Antwoord 19", AnswerQuestion = new Question() { QuestionId = 19, QuestionText = "Vraag 9", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 19, CorrectAnswerText = "Voorbeeldantwoord 19" } } } },
                new Answer() { AnswerId = 20 + (80 * multiplier), AnswerText = "Antwoord 20", AnswerQuestion = new Question() { QuestionId = 20, QuestionText = "Vraag 10", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 20, CorrectAnswerText = "Voorbeeldantwoord 20" } } } },
                new Answer() { AnswerId = 21 + (80 * multiplier), AnswerText = "Antwoord 21", AnswerQuestion = new Question() { QuestionId = 21, QuestionText = "Vraag 1", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 21, CorrectAnswerText = "Voorbeeldantwoord 21" } } } },
                new Answer() { AnswerId = 22 + (80 * multiplier), AnswerText = "Antwoord 22", AnswerQuestion = new Question() { QuestionId = 22, QuestionText = "Vraag 2", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 22, CorrectAnswerText = "Voorbeeldantwoord 22" } } } },
                new Answer() { AnswerId = 23 + (80 * multiplier), AnswerText = "Antwoord 23", AnswerQuestion = new Question() { QuestionId = 23, QuestionText = "Vraag 3", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 23, CorrectAnswerText = "Voorbeeldantwoord 23" } } } },
                new Answer() { AnswerId = 24 + (80 * multiplier), AnswerText = "Antwoord 24", AnswerQuestion = new Question() { QuestionId = 24, QuestionText = "Vraag 4", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 24, CorrectAnswerText = "Voorbeeldantwoord 24" } } } },
                new Answer() { AnswerId = 25 + (80 * multiplier), AnswerText = "Antwoord 25", AnswerQuestion = new Question() { QuestionId = 25, QuestionText = "Vraag 5", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 25, CorrectAnswerText = "Voorbeeldantwoord 25" } } } },
                new Answer() { AnswerId = 26 + (80 * multiplier), AnswerText = "Antwoord 26", AnswerQuestion = new Question() { QuestionId = 26, QuestionText = "Vraag 6", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 26, CorrectAnswerText = "Voorbeeldantwoord 26" } } } },
                new Answer() { AnswerId = 27 + (80 * multiplier), AnswerText = "Antwoord 27", AnswerQuestion = new Question() { QuestionId = 27, QuestionText = "Vraag 7", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 27, CorrectAnswerText = "Voorbeeldantwoord 27" } } } },
                new Answer() { AnswerId = 28 + (80 * multiplier), AnswerText = "Antwoord 28", AnswerQuestion = new Question() { QuestionId = 28, QuestionText = "Vraag 8", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 28, CorrectAnswerText = "Voorbeeldantwoord 28" } } } },
                new Answer() { AnswerId = 29 + (80 * multiplier), AnswerText = "Antwoord 29", AnswerQuestion = new Question() { QuestionId = 29, QuestionText = "Vraag 9", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 29, CorrectAnswerText = "Voorbeeldantwoord 29" } } } },
                new Answer() { AnswerId = 30 + (80 * multiplier), AnswerText = "Antwoord 30", AnswerQuestion = new Question() { QuestionId = 30, QuestionText = "Vraag 10", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 30, CorrectAnswerText = "Voorbeeldantwoord 30" } } } },
                new Answer() { AnswerId = 31 + (80 * multiplier), AnswerText = "Antwoord 31", AnswerQuestion = new Question() { QuestionId = 31, QuestionText = "Vraag 1", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 31, CorrectAnswerText = "Voorbeeldantwoord 31" } } } },
                new Answer() { AnswerId = 32 + (80 * multiplier), AnswerText = "Antwoord 32", AnswerQuestion = new Question() { QuestionId = 32, QuestionText = "Vraag 2", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 32, CorrectAnswerText = "Voorbeeldantwoord 32" } } } },
                new Answer() { AnswerId = 33 + (80 * multiplier), AnswerText = "Antwoord 33", AnswerQuestion = new Question() { QuestionId = 33, QuestionText = "Vraag 3", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 33, CorrectAnswerText = "Voorbeeldantwoord 33" } } } },
                new Answer() { AnswerId = 34 + (80 * multiplier), AnswerText = "Antwoord 34", AnswerQuestion = new Question() { QuestionId = 34, QuestionText = "Vraag 4", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 34, CorrectAnswerText = "Voorbeeldantwoord 34" } } } },
                new Answer() { AnswerId = 35 + (80 * multiplier), AnswerText = "Antwoord 35", AnswerQuestion = new Question() { QuestionId = 35, QuestionText = "Vraag 5", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 35, CorrectAnswerText = "Voorbeeldantwoord 35" } } } },
                new Answer() { AnswerId = 36 + (80 * multiplier), AnswerText = "Antwoord 36", AnswerQuestion = new Question() { QuestionId = 36, QuestionText = "Vraag 6", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 36, CorrectAnswerText = "Voorbeeldantwoord 36" } } } },
                new Answer() { AnswerId = 37 + (80 * multiplier), AnswerText = "Antwoord 37", AnswerQuestion = new Question() { QuestionId = 37, QuestionText = "Vraag 7", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 37, CorrectAnswerText = "Voorbeeldantwoord 37" } } } },
                new Answer() { AnswerId = 38 + (80 * multiplier), AnswerText = "Antwoord 38", AnswerQuestion = new Question() { QuestionId = 38, QuestionText = "Vraag 8", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 38, CorrectAnswerText = "Voorbeeldantwoord 38" } } } },
                new Answer() { AnswerId = 39 + (80 * multiplier), AnswerText = "Antwoord 39", AnswerQuestion = new Question() { QuestionId = 39, QuestionText = "Vraag 9", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 39, CorrectAnswerText = "Voorbeeldantwoord 39" } } } },
                new Answer() { AnswerId = 40 + (80 * multiplier), AnswerText = "Antwoord 40", AnswerQuestion = new Question() { QuestionId = 40, QuestionText = "Vraag 10", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 40, CorrectAnswerText = "Voorbeeldantwoord 40" } } } },
                new Answer() { AnswerId = 41 + (80 * multiplier), AnswerText = "Antwoord 41", AnswerQuestion = new Question() { QuestionId = 41, QuestionText = "Vraag 1", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 41, CorrectAnswerText = "Voorbeeldantwoord 41" } } } },
                new Answer() { AnswerId = 42 + (80 * multiplier), AnswerText = "Antwoord 42", AnswerQuestion = new Question() { QuestionId = 42, QuestionText = "Vraag 2", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 42, CorrectAnswerText = "Voorbeeldantwoord 42" } } } },
                new Answer() { AnswerId = 43 + (80 * multiplier), AnswerText = "Antwoord 43", AnswerQuestion = new Question() { QuestionId = 43, QuestionText = "Vraag 3", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 43, CorrectAnswerText = "Voorbeeldantwoord 43" } } } },
                new Answer() { AnswerId = 44 + (80 * multiplier), AnswerText = "Antwoord 44", AnswerQuestion = new Question() { QuestionId = 44, QuestionText = "Vraag 4", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 44, CorrectAnswerText = "Voorbeeldantwoord 44" } } } },
                new Answer() { AnswerId = 45 + (80 * multiplier), AnswerText = "Antwoord 45", AnswerQuestion = new Question() { QuestionId = 45, QuestionText = "Vraag 5", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 45, CorrectAnswerText = "Voorbeeldantwoord 45" } } } },
                new Answer() { AnswerId = 46 + (80 * multiplier), AnswerText = "Antwoord 46", AnswerQuestion = new Question() { QuestionId = 46, QuestionText = "Vraag 6", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 46, CorrectAnswerText = "Voorbeeldantwoord 46" } } } },
                new Answer() { AnswerId = 47 + (80 * multiplier), AnswerText = "Antwoord 47", AnswerQuestion = new Question() { QuestionId = 47, QuestionText = "Vraag 7", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 47, CorrectAnswerText = "Voorbeeldantwoord 47" } } } },
                new Answer() { AnswerId = 48 + (80 * multiplier), AnswerText = "Antwoord 48", AnswerQuestion = new Question() { QuestionId = 48, QuestionText = "Vraag 8", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 48, CorrectAnswerText = "Voorbeeldantwoord 48" } } } },
                new Answer() { AnswerId = 49 + (80 * multiplier), AnswerText = "Antwoord 49", AnswerQuestion = new Question() { QuestionId = 49, QuestionText = "Vraag 9", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 49, CorrectAnswerText = "Voorbeeldantwoord 49" } } } },
                new Answer() { AnswerId = 50 + (80 * multiplier), AnswerText = "Antwoord 50", AnswerQuestion = new Question() { QuestionId = 50, QuestionText = "Vraag 10", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 50, CorrectAnswerText = "Voorbeeldantwoord 50" } } } },
                new Answer() { AnswerId = 51 + (80 * multiplier), AnswerText = "Antwoord 51", AnswerQuestion = new Question() { QuestionId = 51, QuestionText = "Vraag 1", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 51, CorrectAnswerText = "Voorbeeldantwoord 51" } } } },
                new Answer() { AnswerId = 52 + (80 * multiplier), AnswerText = "Antwoord 52", AnswerQuestion = new Question() { QuestionId = 52, QuestionText = "Vraag 2", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 52, CorrectAnswerText = "Voorbeeldantwoord 52" } } } },
                new Answer() { AnswerId = 53 + (80 * multiplier), AnswerText = "Antwoord 53", AnswerQuestion = new Question() { QuestionId = 53, QuestionText = "Vraag 3", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 53, CorrectAnswerText = "Voorbeeldantwoord 53" } } } },
                new Answer() { AnswerId = 54 + (80 * multiplier), AnswerText = "Antwoord 54", AnswerQuestion = new Question() { QuestionId = 54, QuestionText = "Vraag 4", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 54, CorrectAnswerText = "Voorbeeldantwoord 54" } } } },
                new Answer() { AnswerId = 55 + (80 * multiplier), AnswerText = "Antwoord 55", AnswerQuestion = new Question() { QuestionId = 55, QuestionText = "Vraag 5", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 55, CorrectAnswerText = "Voorbeeldantwoord 55" } } } },
                new Answer() { AnswerId = 56 + (80 * multiplier), AnswerText = "Antwoord 56", AnswerQuestion = new Question() { QuestionId = 56, QuestionText = "Vraag 6", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 56, CorrectAnswerText = "Voorbeeldantwoord 56" } } } },
                new Answer() { AnswerId = 57 + (80 * multiplier), AnswerText = "Antwoord 57", AnswerQuestion = new Question() { QuestionId = 57, QuestionText = "Vraag 7", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 57, CorrectAnswerText = "Voorbeeldantwoord 57" } } } },
                new Answer() { AnswerId = 58 + (80 * multiplier), AnswerText = "Antwoord 58", AnswerQuestion = new Question() { QuestionId = 58, QuestionText = "Vraag 8", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 58, CorrectAnswerText = "Voorbeeldantwoord 58" } } } },
                new Answer() { AnswerId = 59 + (80 * multiplier), AnswerText = "Antwoord 59", AnswerQuestion = new Question() { QuestionId = 59, QuestionText = "Vraag 9", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 59, CorrectAnswerText = "Voorbeeldantwoord 59" } } } },
                new Answer() { AnswerId = 60 + (80 * multiplier), AnswerText = "Antwoord 60", AnswerQuestion = new Question() { QuestionId = 60, QuestionText = "Vraag 10", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 60, CorrectAnswerText = "Voorbeeldantwoord 60" } } } },
                new Answer() { AnswerId = 61 + (80 * multiplier), AnswerText = "Antwoord 61", AnswerQuestion = new Question() { QuestionId = 61, QuestionText = "Vraag 1", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 61, CorrectAnswerText = "Voorbeeldantwoord 61" } } } },
                new Answer() { AnswerId = 62 + (80 * multiplier), AnswerText = "Antwoord 62", AnswerQuestion = new Question() { QuestionId = 62, QuestionText = "Vraag 2", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 62, CorrectAnswerText = "Voorbeeldantwoord 62" } } } },
                new Answer() { AnswerId = 63 + (80 * multiplier), AnswerText = "Antwoord 63", AnswerQuestion = new Question() { QuestionId = 63, QuestionText = "Vraag 3", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 63, CorrectAnswerText = "Voorbeeldantwoord 63" } } } },
                new Answer() { AnswerId = 64 + (80 * multiplier), AnswerText = "Antwoord 64", AnswerQuestion = new Question() { QuestionId = 64, QuestionText = "Vraag 4", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 64, CorrectAnswerText = "Voorbeeldantwoord 64" } } } },
                new Answer() { AnswerId = 65 + (80 * multiplier), AnswerText = "Antwoord 65", AnswerQuestion = new Question() { QuestionId = 65, QuestionText = "Vraag 5", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 65, CorrectAnswerText = "Voorbeeldantwoord 65" } } } },
                new Answer() { AnswerId = 66 + (80 * multiplier), AnswerText = "Antwoord 66", AnswerQuestion = new Question() { QuestionId = 66, QuestionText = "Vraag 6", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 66, CorrectAnswerText = "Voorbeeldantwoord 66" } } } },
                new Answer() { AnswerId = 67 + (80 * multiplier), AnswerText = "Antwoord 67", AnswerQuestion = new Question() { QuestionId = 67, QuestionText = "Vraag 7", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 67, CorrectAnswerText = "Voorbeeldantwoord 67" } } } },
                new Answer() { AnswerId = 68 + (80 * multiplier), AnswerText = "Antwoord 68", AnswerQuestion = new Question() { QuestionId = 68, QuestionText = "Vraag 8", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 68, CorrectAnswerText = "Voorbeeldantwoord 68" } } } },
                new Answer() { AnswerId = 69 + (80 * multiplier), AnswerText = "Antwoord 69", AnswerQuestion = new Question() { QuestionId = 69, QuestionText = "Vraag 9", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 69, CorrectAnswerText = "Voorbeeldantwoord 69" } } } },
                new Answer() { AnswerId = 70 + (80 * multiplier), AnswerText = "Antwoord 70", AnswerQuestion = new Question() { QuestionId = 70, QuestionText = "Vraag 10", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 70, CorrectAnswerText = "Voorbeeldantwoord 70" } } } },
                new Answer() { AnswerId = 71 + (80 * multiplier), AnswerText = "Antwoord 71", AnswerQuestion = new Question() { QuestionId = 71, QuestionText = "Vraag 1", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 71, CorrectAnswerText = "Voorbeeldantwoord 71" } } } },
                new Answer() { AnswerId = 72 + (80 * multiplier), AnswerText = "Antwoord 72", AnswerQuestion = new Question() { QuestionId = 72, QuestionText = "Vraag 2", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 72, CorrectAnswerText = "Voorbeeldantwoord 72" } } } },
                new Answer() { AnswerId = 73 + (80 * multiplier), AnswerText = "Antwoord 73", AnswerQuestion = new Question() { QuestionId = 73, QuestionText = "Vraag 3", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 73, CorrectAnswerText = "Voorbeeldantwoord 73" } } } },
                new Answer() { AnswerId = 74 + (80 * multiplier), AnswerText = "Antwoord 74", AnswerQuestion = new Question() { QuestionId = 74, QuestionText = "Vraag 4", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 74, CorrectAnswerText = "Voorbeeldantwoord 74" } } } },
                new Answer() { AnswerId = 75 + (80 * multiplier), AnswerText = "Antwoord 75", AnswerQuestion = new Question() { QuestionId = 75, QuestionText = "Vraag 5", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 75, CorrectAnswerText = "Voorbeeldantwoord 75" } } } },
                new Answer() { AnswerId = 76 + (80 * multiplier), AnswerText = "Antwoord 76", AnswerQuestion = new Question() { QuestionId = 76, QuestionText = "Vraag 6", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 76, CorrectAnswerText = "Voorbeeldantwoord 76" } } } },
                new Answer() { AnswerId = 77 + (80 * multiplier), AnswerText = "Antwoord 77", AnswerQuestion = new Question() { QuestionId = 77, QuestionText = "Vraag 7", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 77, CorrectAnswerText = "Voorbeeldantwoord 77" } } } },
                new Answer() { AnswerId = 78 + (80 * multiplier), AnswerText = "Antwoord 78", AnswerQuestion = new Question() { QuestionId = 78, QuestionText = "Vraag 8", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 78, CorrectAnswerText = "Voorbeeldantwoord 78" } } } },
                new Answer() { AnswerId = 79 + (80 * multiplier), AnswerText = "Antwoord 79", AnswerQuestion = new Question() { QuestionId = 79, QuestionText = "Vraag 9", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 79, CorrectAnswerText = "Voorbeeldantwoord 79" } } } },
                new Answer() { AnswerId = 80 + (80 * multiplier), AnswerText = "Antwoord 80", AnswerQuestion = new Question() { QuestionId = 80, QuestionText = "Vraag 10", QuestionCorrectAnswers = new CorrectAnswers { new CorrectAnswer() { CorrectAnswerId = 80, CorrectAnswerText = "Voorbeeldantwoord 80" } } } }
            };
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

    internal class ListItem
    {
        public ListItem()
        {
        }

        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public string CorrectAnswerText { get; set; }
        public bool IsChecked { get; set; }
    }
}
