package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Round;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.orm.jpa.DataJpaTest;
import org.springframework.boot.test.autoconfigure.orm.jpa.TestEntityManager;
import org.springframework.test.context.junit.jupiter.SpringExtension;

import java.util.Optional;

@ExtendWith(SpringExtension.class)
@DataJpaTest
public class RoundRepositoryIntegrationTest {
    @Autowired
    private TestEntityManager entityManager;

    @Autowired
    private RoundRepository roundRepository;

    @Test
    public void getRoundById_FromRoundRepository() {
        Round round = new Round();
        entityManager.persist(round);
        entityManager.flush();

        Optional<Round> found = roundRepository.findById(round.getId());

        Assertions.assertEquals(round.getId(), found.get().getId());
    }

    @Test
    public void patchRound_fromRoundRepository(){
        Round round = new Round();
        entityManager.persist(round);
        entityManager.flush();
        round.setName("test");
        Round returnedRound = roundRepository.patchRound(round);
        Assertions.assertEquals(returnedRound.getId(), round.getId());
        Assertions.assertEquals(returnedRound.getName(), round.getName());
    }
}
