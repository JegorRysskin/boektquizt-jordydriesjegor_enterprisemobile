package enterpriseAndMobile.controller;

import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.service.TeamService;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit.jupiter.SpringExtension;
import org.springframework.test.web.servlet.MockMvc;

import java.util.ArrayList;
import java.util.List;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.collection.IsCollectionWithSize.hasSize;
import static org.mockito.BDDMockito.given;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@ExtendWith(SpringExtension.class)
@WebMvcTest(TeamRestController.class)
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
}


