package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Quiz;
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
public class QuizRepositoryIntegrationTest {

    @Autowired
    private TestEntityManager entityManager;

    @Autowired
    private QuizRepository quizRepository;

    @Test
    public void getAllQuizes_FromQuizRepository() {
        Quiz quiz1 = new Quiz();
        Quiz quiz2 = new Quiz();

        entityManager.persist(quiz1);
        entityManager.persist(quiz2);
        entityManager.flush();

        List<Quiz> found = quizRepository.getAllQuizzes();

        Assertions.assertEquals(2, found.size());
    }

    @Test
    public void addQuiz_ToQuizRepository() {
        Quiz quiz = new Quiz();

        Quiz result = quizRepository.addQuiz(quiz);

        Assertions.assertEquals(1, quizRepository.getAllQuizzes().size());
    }

    @Test
    public void getQuizById_FromQuizRepository() {
        Quiz quiz = new Quiz();

        entityManager.persist(quiz);
        entityManager.flush();

        Optional<Quiz> quiz1 = quizRepository.getQuizById(quiz.getId());

        Assertions.assertTrue(quiz1.isPresent());
    }

    @Test
    public void deleteQuizById_ToQuizRepository(){
        Quiz quiz = new Quiz();

        entityManager.persist(quiz);
        entityManager.flush();

        quizRepository.deleteQuiz(quiz);
        Assertions.assertTrue(quizRepository.getQuizById(quiz.getId()).isEmpty());
    }

    @Test
    public void patchQuiz_FromQuizRepository() {
        Quiz quiz = new Quiz();
        quiz.setName("test");
        entityManager.persist(quiz);
        entityManager.flush();

        Quiz patched = quizRepository.patchQuiz(quiz);

        Assertions.assertEquals("test", patched.getName());
        Assertions.assertEquals(quiz.getId(), patched.getId());
    }
}
