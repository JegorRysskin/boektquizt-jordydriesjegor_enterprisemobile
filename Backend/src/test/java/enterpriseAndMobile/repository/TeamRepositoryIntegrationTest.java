package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Team;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.boot.test.autoconfigure.orm.jpa.TestEntityManager;
import org.springframework.test.context.junit.jupiter.SpringExtension;

import java.util.List;
import java.util.Optional;

@ExtendWith(SpringExtension.class)
@DataJpaTest
public class TeamRepositoryIntegrationTest {

    @Autowired
    private TestEntityManager entityManager;

    @Autowired
    private TeamRepository teamRepository;

    @Test
    public void getAllTeams_FromTeamRepository(){
        Team team1 = new Team();
        Team team2 = new Team();

        entityManager.persist(team1);
        entityManager.persist(team2);
        entityManager.flush();

        List<Team> found = teamRepository.getAllTeams();

        Assertions.assertEquals(2, found.size());
    }

    @Test
    public void getTeamById_FromTeamRepository(){
        Team team1 = new Team("test");

        entityManager.persist(team1);
        entityManager.flush();

        Optional<Team> team = teamRepository.getTeamById(team1.getId());

        Assertions.assertTrue(team.isPresent());
    }

    @Test
    public void getTeamByName_FromTeamRepository(){
        Team team1 = new Team("test");

        entityManager.persist(team1);
        entityManager.flush();

        Team team = teamRepository.getTeamByName("test");
        Assertions.assertEquals("test", team.getName());
    }
}
