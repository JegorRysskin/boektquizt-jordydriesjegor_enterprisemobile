package enterpriseAndMobile.service;

import enterpriseAndMobile.Exception.NotFoundException;
import enterpriseAndMobile.dto.RoundPatchDto;
import enterpriseAndMobile.model.Round;
import enterpriseAndMobile.repository.RoundRepository;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.test.context.junit.jupiter.SpringExtension;

import java.util.Optional;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.ArgumentMatchers.anyInt;
import static org.mockito.BDDMockito.given;

@ExtendWith(SpringExtension.class)
@SpringBootTest(classes = RoundService.class)
public class RoundServiceUnitTest {
    @MockBean
    private RoundRepository roundRepository;

    @Autowired
    private RoundService roundService;

    @Test
    public void getRoundById_FromTeamService() throws NotFoundException {
        Round round = new Round();

        given(roundRepository.findById(anyInt())).willReturn(Optional.of(round));
        Assertions.assertEquals(round.getId(), roundService.getRoundById(1).getId());
    }

    @Test
    public void patchRoundById() throws NotFoundException {
        Round round = new Round();
        Round returnedQuiz = new Round("test");
        RoundPatchDto patch = new RoundPatchDto("test");

        given(roundRepository.findById(anyInt())).willReturn(Optional.of(round));
        given(roundRepository.patchRound(any())).willReturn(returnedQuiz);
        Round patchedRound = roundService.patchRound(0, patch);
        Assertions.assertEquals(patch.getName(), patchedRound.getName());
        Assertions.assertEquals(round.getId(), patchedRound.getId());
    }
}
