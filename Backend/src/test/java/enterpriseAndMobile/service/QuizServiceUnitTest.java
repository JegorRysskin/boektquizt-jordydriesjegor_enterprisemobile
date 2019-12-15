package enterpriseAndMobile.service;

import enterpriseAndMobile.QuizApplication;
import enterpriseAndMobile.dto.QuizDto;
import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.repository.QuizRepository;
import javassist.NotFoundException;
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

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.ArgumentMatchers.anyInt;
import static org.mockito.BDDMockito.given;

@ExtendWith(SpringExtension.class)
@SpringBootTest(classes = QuizService.class)
public class QuizServiceUnitTest {

    @MockBean
    private QuizRepository quizRepository;

    @Autowired
    private QuizService quizService;

    @Test
    public void getAllQuizzes() {
        Quiz quiz1 = new Quiz();
        Quiz quiz2 = new Quiz();

        List<Quiz> quizzes = new ArrayList<>();

        quizzes.add(quiz1);
        quizzes.add(quiz2);
        given(quizRepository.getAllQuizzes()).willReturn(quizzes);
        Assertions.assertEquals(2, quizService.getAllQuizzes().size());
    }

    @Test
    public void addQuiz() {
        QuizDto quizDto = new QuizDto("Test", false);
        Quiz quiz = new Quiz();
        given(quizRepository.addQuiz(any())).willReturn(quiz);
        Assertions.assertEquals(quiz.getId(), quizService.addQuiz(quizDto).getId());
    }

    @Test
    public void getQuizById() {
        Quiz quiz = new Quiz();
        given(quizRepository.getQuizById(anyInt())).willReturn(Optional.of(quiz));
        quizService.getQuizById(0);
        Assertions.assertEquals(quiz.getId(), quizService.getQuizById(0).get().getId());
    }

    @Test
    public void deleteQuizById(){
        Quiz quiz = new Quiz();
        given(quizRepository.getQuizById(quiz.getId())).willReturn(Optional.of(quiz));
        quizService.removeQuiz(quiz.getId());
        Assertions.assertEquals(0, quizService.getAllQuizzes().size());
    }

    @Test
    public void ThrowNotFoundWhenDeletingQuizThatDoesNotExist(){
        given(quizRepository.getQuizById(anyInt())).willReturn(Optional.empty());
        Assertions.assertThrows(Exception.class, () -> quizService.removeQuiz(1));
    }
}
