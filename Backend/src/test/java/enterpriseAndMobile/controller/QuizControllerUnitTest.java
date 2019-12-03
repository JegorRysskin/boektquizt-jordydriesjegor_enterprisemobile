package enterpriseAndMobile.controller;

import enterpriseAndMobile.converter.JsonStringConverter;
import enterpriseAndMobile.dto.QuizDto;
import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.service.QuizService;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit.jupiter.SpringExtension;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;

import java.util.ArrayList;
import java.util.List;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.collection.IsCollectionWithSize.hasSize;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.BDDMockito.given;
import static org.mockito.Mockito.mock;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@ExtendWith(SpringExtension.class)
@WebMvcTest(QuizRestController.class)
public class QuizControllerUnitTest {

    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private QuizService quizService;

    @Test
    public void getAllQuizes_FromQuizController() throws Exception {
        Quiz quiz1 = new Quiz("test1");
        Quiz quiz2 = new Quiz("test2");

        List<Quiz> quizzes = new ArrayList<>();

        mockMvc.perform(get("/quiz")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isNotFound());

        quizzes.add(quiz1);
        quizzes.add(quiz2);

        given(quizService.getAllQuizzes()).willReturn(quizzes);

        mockMvc.perform(get("/quiz")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$", hasSize(2)))
                .andExpect(jsonPath("$[0].name", is(quiz1.getName())));
    }

    @Test
    public void addQuiz_ToDatabase() throws Exception{
        Quiz quiz = new Quiz("test", false);
        QuizDto quizDto = new QuizDto("test", false);

        given(quizService.addQuiz(any())).willReturn(quiz);

        mockMvc.perform(MockMvcRequestBuilders
                .post("/quiz")
                .content(JsonStringConverter.asJsonString(quizDto))
                .contentType(MediaType.APPLICATION_JSON)
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(status().isCreated())
                .andExpect(jsonPath("$.name", is("test")));
    }
}


