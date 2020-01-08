package enterpriseAndMobile.controller;

import enterpriseAndMobile.model.Answer;
import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.service.AnswerService;
import enterpriseAndMobile.service.TeamService;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.security.test.context.support.WithMockUser;
import org.springframework.test.context.junit.jupiter.SpringExtension;
import org.springframework.test.web.servlet.MockMvc;

import java.util.Optional;

import static org.hamcrest.CoreMatchers.is;
import static org.mockito.ArgumentMatchers.anyInt;
import static org.mockito.BDDMockito.given;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@ExtendWith(SpringExtension.class)
@WebMvcTest(AnswerRestController.class)
@WithMockUser(roles = "ADMIN")
public class AnswerControllerUnitTest {

    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private AnswerService answerService;

    @Test
    public void GetAnswerByQuestionAndTeamId_FromAnswerController() throws Exception {
        Answer answer = new Answer("test");
        given(answerService.getAnswerByQuestionIdAndTeamId(anyInt(), anyInt())).willReturn(answer);

        mockMvc.perform(get("/answer/1/1")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.answerString", is(answer.getAnswerString())));
    }

}
