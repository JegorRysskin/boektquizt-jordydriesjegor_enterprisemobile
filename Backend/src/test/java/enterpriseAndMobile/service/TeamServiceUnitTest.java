package enterpriseAndMobile.service;

import enterpriseAndMobile.model.Team;
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

import static org.mockito.BDDMockito.given;

@ExtendWith(SpringExtension.class)
@SpringBootTest(classes = TeamService.class)
public class TeamServiceUnitTest {

    @MockBean
    private TeamRepository teamRepository;

    @Autowired
    private TeamService teamService;

    @Test
    public void getAllQuizzes() {
        Team team1 = new Team();
        Team team2 = new Team();

        List<Team> teams = new ArrayList<>();

        teams.add(team1);
        teams.add(team2);

        given(teamRepository.getAllTeams()).willReturn(teams);
        Assertions.assertEquals(2, teamService.getAllTeams().size());
    }
}
