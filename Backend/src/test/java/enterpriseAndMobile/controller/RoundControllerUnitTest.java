package enterpriseAndMobile.controller;

import enterpriseAndMobile.model.Round;
import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.service.RoundService;
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
@WebMvcTest(RoundRestController.class)
@WithMockUser(roles = "ADMIN")
public class RoundControllerUnitTest {
    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private RoundService roundService;

    @Test
    public void GetRoundById_FromTeamController() throws Exception {
        Round round = new Round("test");

        given(roundService.getRoundById(anyInt())).willReturn(round);

        mockMvc.perform(get("/round/1")
                .contentType(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.name", is(round.getName())));
    }

}
