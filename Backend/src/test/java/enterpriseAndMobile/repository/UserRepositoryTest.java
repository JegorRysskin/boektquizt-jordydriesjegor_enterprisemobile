package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.model.User;
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
public class UserRepositoryTest {
    @Autowired
    private TestEntityManager entityManager;

    @Autowired
    private UserRepository userRepository;

    @Test
    public void deleteUserByTeamId_FromUserRepository(){
        Team team = new Team();
        Team team1 = new Team();

        User user = new User(team);
        User user1= new User(team1);

        entityManager.persist(user);
        entityManager.persist(user1);
        entityManager.flush();

        List<User> found = userRepository.getAllUsers();

        Assertions.assertEquals(2, found.size());

        userRepository.deleteUserByTeam(team);

        found = userRepository.getAllUsers();

        Assertions.assertEquals(1, found.size());
    }
}
