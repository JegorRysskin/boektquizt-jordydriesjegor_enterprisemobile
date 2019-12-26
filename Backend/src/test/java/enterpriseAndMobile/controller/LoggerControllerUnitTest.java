package enterpriseAndMobile.controller;

import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.service.LoggerService;
import enterpriseAndMobile.service.QuizService;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.security.test.context.support.WithMockUser;
import org.springframework.test.context.junit.jupiter.SpringExtension;
import org.springframework.test.web.servlet.MockMvc;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import static org.hamcrest.CoreMatchers.is;
import static org.hamcrest.collection.IsCollectionWithSize.hasSize;
import static org.mockito.BDDMockito.given;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@ExtendWith(SpringExtension.class)
@WebMvcTest(LoggerController.class)
@WithMockUser(roles = "ENGINEER")
public class LoggerControllerUnitTest {
    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private LoggerService loggerService;

    @Test
    public void getAllQuizes_FromQuizController() throws Exception {
        List<String> messages = new ArrayList<>();

        messages.add("test");
        messages.add("test2");

        given(loggerService.getAllLogMessages()).willReturn(Optional.of(messages));

        mockMvc.perform(get("/logger")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$", hasSize(2)))
                .andExpect(jsonPath("$[0]", is(messages.get(0))));
    }
}
