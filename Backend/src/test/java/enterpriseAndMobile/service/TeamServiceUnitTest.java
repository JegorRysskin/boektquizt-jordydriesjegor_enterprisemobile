package enterpriseAndMobile.service;

import enterpriseAndMobile.Exception.NotFoundException;
import enterpriseAndMobile.dto.*;
import enterpriseAndMobile.model.Answer;
import enterpriseAndMobile.model.Question;
import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.repository.QuestionRepository;
import enterpriseAndMobile.repository.TeamRepository;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.test.context.junit.jupiter.SpringExtension;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import static org.mockito.ArgumentMatchers.*;
import static org.mockito.BDDMockito.given;

@ExtendWith(SpringExtension.class)
@SpringBootTest(classes = TeamService.class)
public class TeamServiceUnitTest {

    @MockBean
    private TeamRepository teamRepository;

    @MockBean
    private QuestionRepository questionRepository;

    @Autowired
    private TeamService teamService;

    @Test
    public void getAllTeams_FromTeamService() {
        Team team1 = new Team();
        Team team2 = new Team();

        List<Team> teams = new ArrayList<>();

        teams.add(team1);
        teams.add(team2);

        given(teamRepository.getAllTeams()).willReturn(teams);
        Assertions.assertEquals(2, teamService.getAllTeams().size());
    }

    @Test
    public void getTeamById_FromTeamService(){
        Team team = new Team();

        given(teamRepository.getTeamById(anyInt())).willReturn(Optional.of(team));

        Assertions.assertTrue(teamService.getTeamById(1).isPresent());
    }

    @Test
    public void getTeamByName_FromTeamService(){
        Team team = new Team("test");

        given(teamRepository.getTeamByName(anyString())).willReturn(team);

        Assertions.assertEquals("test", teamService.getItemByName("test").getName());
    }

    @Test
    public void patchTeamById() throws NotFoundException {
        Team team = new Team();
        Team returnedTeam = new Team("test");
        TeamPatchDto patchDto = new TeamPatchDto("test", true);

        given(teamRepository.getTeamById(anyInt())).willReturn(Optional.of(team));
        given(teamRepository.patchTeam(any())).willReturn(returnedTeam);
        Team found = teamService.patchTeam(0, patchDto);
        Assertions.assertEquals(patchDto.getName(), found.getName());
        Assertions.assertEquals(team.getId(), found.getId());
    }

    @Test
    public void patchTeamAnswersById() throws NotFoundException {
        Answer answer = new Answer();
        List<Answer> answers = new ArrayList<>();
        answers.add(answer);
        Team team = new Team();
        Team returnedTeam = new Team(answers);

        AnswerDto answerDto = new AnswerDto("");
        Question question = new Question();

        given(teamRepository.getTeamById(anyInt())).willReturn(Optional.of(team));
        given(teamRepository.patchTeam(any())).willReturn(returnedTeam);
        given(questionRepository.findById(anyInt())).willReturn(Optional.of(question));
        Team found = teamService.patchTeamAnswers(0, answerDto);
        Assertions.assertEquals(1, found.getAnswers().size());
        Assertions.assertEquals(team.getId(), found.getId());
    }

   @Test
    public void patchTeamScoreById() throws NotFoundException {
        Team team = new Team();
        Team returnedTeam = new Team(20.0);
        TeamPatchScoreDto teamPatchScoreDto = new TeamPatchScoreDto(20.0);

        given(teamRepository.getTeamById(anyInt())).willReturn(Optional.of(team));
        given(teamRepository.patchTeam(any())).willReturn(returnedTeam);
        Team found = teamService.patchScoreTeam(0, teamPatchScoreDto);
        Assertions.assertEquals(teamPatchScoreDto.getScores(), found.getScores());
        Assertions.assertEquals(team.getId(), found.getId());
    }
}
