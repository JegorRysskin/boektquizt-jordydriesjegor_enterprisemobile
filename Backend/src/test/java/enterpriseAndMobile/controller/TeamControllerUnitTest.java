package enterpriseAndMobile.controller;

import enterpriseAndMobile.converter.JsonStringConverter;
import enterpriseAndMobile.dto.QuizPatchDto;
import enterpriseAndMobile.dto.TeamPatchAnswersDto;
import enterpriseAndMobile.dto.TeamPatchDto;
import enterpriseAndMobile.model.Answer;
import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.service.TeamService;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.security.test.context.support.WithMockUser;
import org.springframework.test.context.junit.jupiter.SpringExtension;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.collection.IsCollectionWithSize.hasSize;
import static org.mockito.ArgumentMatchers.*;
import static org.mockito.BDDMockito.given;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@ExtendWith(SpringExtension.class)
@WebMvcTest(TeamRestController.class)
@WithMockUser(roles = "ADMIN")
public class TeamControllerUnitTest {

    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private TeamService teamService;

    @Test
    public void getAllTeams_FromTeamController() throws Exception {
        Team team1 = new Team("test1");
        Team team2 = new Team("test2");

        List<Team> teams = new ArrayList<>();

        mockMvc.perform(get("/team")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isNotFound());

        teams.add(team1);
        teams.add(team2);

        given(teamService.getAllTeams()).willReturn(teams);

        mockMvc.perform(get("/team")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$", hasSize(2)))
                .andExpect(jsonPath("$[0].name", is(team1.getName())));
    }

    @Test
    public void GetTeamById_FromTeamController() throws Exception {
        Team team = new Team("test1");

        mockMvc.perform(get("/team/id/1")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isNotFound());

        given(teamService.getTeamById(anyInt())).willReturn(Optional.of(team));

        mockMvc.perform(get("/team/id/1")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.name", is(team.getName())));
    }

    @Test
    public void GetTeamByName_FromTeamController() throws Exception {
        Team team = new Team("test1");

        mockMvc.perform(get("/team/name/test1")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isNotFound());

        given(teamService.getItemByName(anyString())).willReturn(team);

        mockMvc.perform(get("/team/name/test1")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.name", is(team.getName())));
    }

    @Test
    public void patchTeamById() throws Exception {
        Team patchedTeam = new Team("test", false);
        TeamPatchDto teamPatchDto = new TeamPatchDto("test", true);

        given(teamService.patchTeam(anyInt(), any())).willReturn(patchedTeam);

        mockMvc.perform(MockMvcRequestBuilders
                .patch("/team/1")
                .content(JsonStringConverter.asJsonString(teamPatchDto))
                .contentType(MediaType.APPLICATION_JSON)
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.name", is("test")))
                .andExpect(jsonPath("$.enabled", is(false)));
    }

    @Test
    public void patchTeamAnswersById() throws Exception {
        Answer answer = new Answer();
        List<Answer> answers = new ArrayList<>();
        answers.add(answer);
        Team returnedTeam = new Team(answers);

        TeamPatchAnswersDto teamPatchAnswersDto = new TeamPatchAnswersDto(answer);

        given(teamService.patchTeamAnswers(anyInt(), any())).willReturn(returnedTeam);

        mockMvc.perform(MockMvcRequestBuilders
                .patch("/team/answer/1")
                .content(JsonStringConverter.asJsonString(teamPatchAnswersDto))
                .contentType(MediaType.APPLICATION_JSON)
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk());
    }
}



