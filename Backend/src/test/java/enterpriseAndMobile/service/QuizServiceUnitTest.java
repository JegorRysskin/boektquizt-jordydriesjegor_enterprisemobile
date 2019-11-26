package enterpriseAndMobile.service;

import enterpriseAndMobile.model.Quiz;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.test.context.junit.jupiter.SpringExtension;

import java.util.ArrayList;
import java.util.List;

import static org.mockito.BDDMockito.given;

@ExtendWith(SpringExtension.class)
public class QuizServiceUnitTest {
    @MockBean
    private QuizService quizService;

    @Test
    public void getAllQuizzes(){
        Quiz quiz1 = new Quiz();
        Quiz quiz2 = new Quiz();

        List<Quiz> quizzes = new ArrayList<>();

        quizzes.add(quiz1);
        quizzes.add(quiz2);

        given(quizService.getAllQuizzes()).willReturn(quizzes);

        Assertions.assertEquals(2, quizService.getAllQuizzes().size());
    }
}
