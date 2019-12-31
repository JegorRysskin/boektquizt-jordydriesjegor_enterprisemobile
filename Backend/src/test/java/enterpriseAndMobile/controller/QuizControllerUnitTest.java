package enterpriseAndMobile.controller;

import enterpriseAndMobile.converter.JsonStringConverter;
import enterpriseAndMobile.dto.QuizDto;
import enterpriseAndMobile.dto.QuizPatchDto;
import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.service.QuizService;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mock;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.boot.web.server.LocalServerPort;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.security.test.context.support.WithMockUser;
import org.springframework.test.context.junit.jupiter.SpringExtension;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.collection.IsCollectionWithSize.hasSize;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.ArgumentMatchers.anyInt;
import static org.mockito.BDDMockito.given;
import static org.mockito.Mockito.mock;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@ExtendWith(SpringExtension.class)
@WebMvcTest(QuizRestController.class)
@WithMockUser(roles = "ADMIN")
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
    public void addQuiz_ToDatabase() throws Exception {
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

    @Test
    public void getItemById() throws Exception {
        Quiz quiz = new Quiz("test", false);

        given(quizService.getQuizById(anyInt())).willReturn(Optional.of(quiz));

        mockMvc.perform(get("/quiz/1")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.name", is("test")));
    }

    @Test
    public void deleteQuizById() throws Exception {
        Quiz quiz = new Quiz();

        mockMvc.perform(MockMvcRequestBuilders
                .delete("/quiz/1")
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk());
    }

    @Test
    public void patchQuizById() throws Exception {
        Quiz patchedQuiz = new Quiz("test", true);
        QuizPatchDto quizPatchDto = new QuizPatchDto("test", true);

        given(quizService.patchQuiz(anyInt(), any())).willReturn(patchedQuiz);

        mockMvc.perform(MockMvcRequestBuilders
                .patch("/quiz/1")
                .content(JsonStringConverter.asJsonString(quizPatchDto))
                .contentType(MediaType.APPLICATION_JSON)
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.name", is("test")))
                .andExpect(jsonPath("$.enabled", is(true)));
    }
}


