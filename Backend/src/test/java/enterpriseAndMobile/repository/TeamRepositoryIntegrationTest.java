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
}
