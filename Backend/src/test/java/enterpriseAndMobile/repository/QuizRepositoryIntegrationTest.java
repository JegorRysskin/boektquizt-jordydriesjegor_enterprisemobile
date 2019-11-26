package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.repository.QuizRepository;
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
public class QuizRepositoryIntegrationTest {

    @Autowired
    private TestEntityManager entityManager;

    @Autowired
    private QuizRepository quizRepository;

    @Test
    public void getAllQuizes_FromQuizRepository(){
        Quiz quiz1 = new Quiz();
        Quiz quiz2 = new Quiz();

        entityManager.persist(quiz1);
        entityManager.persist(quiz2);
        entityManager.flush();

        List<Quiz> found = quizRepository.getAllQuizzes();

        Assertions.assertEquals(2, found.size());
    }
}
