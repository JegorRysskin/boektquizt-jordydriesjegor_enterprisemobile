package enterpriseAndMobile.service;

import enterpriseAndMobile.Exception.NotFoundException;
import enterpriseAndMobile.model.Answer;
import enterpriseAndMobile.model.Question;
import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.repository.TeamRepository;
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

import static org.mockito.ArgumentMatchers.anyInt;
import static org.mockito.BDDMockito.given;

@ExtendWith(SpringExtension.class)
@SpringBootTest(classes = AnswerService.class)
public class AnswerServiceUnitTest {

    @MockBean
    private TeamRepository teamRepository;

    @Autowired
    private AnswerService answerService;

    @Test
    public void getAnswerByQuestionIdAndTeamId_FromAnswerService() throws NotFoundException {
        Team team = new Team();
        List<Answer> answers = new ArrayList<>();
        Answer answer = new Answer();
        Question question = new Question();
        answer.setQuestion(question);
        answers.add(answer);
        team.setAnswers(answers);
        given(teamRepository.getTeamById(anyInt())).willReturn(Optional.of(team));
        Assertions.assertEquals(answer.getId(), answerService.getAnswerByQuestionIdAndTeamId(team.getId(), question.getId()).getId());
    }
}
