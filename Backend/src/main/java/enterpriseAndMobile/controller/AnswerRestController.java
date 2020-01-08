package enterpriseAndMobile.controller;

import enterpriseAndMobile.Exception.NotFoundException;
import enterpriseAndMobile.annotation.LogExecutionTime;
import enterpriseAndMobile.model.Answer;
import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.service.AnswerService;
import enterpriseAndMobile.service.TeamService;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin("*")
@RestController
@RequestMapping(value = "/answer")
public class AnswerRestController {

    protected final Log logger = LogFactory.getLog(getClass());

    private final AnswerService answerService;

    public AnswerRestController(AnswerService answerService) {
        this.answerService = answerService;
    }

    @LogExecutionTime
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    @GetMapping(value = "/{team}/{question}", produces = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Answer> getAnswerByQuestionIdAndTeamId(@PathVariable("team") int teamId,@PathVariable("question") int questionId) {
        try {
            Answer answer = answerService.getAnswerByQuestionIdAndTeamId(teamId, questionId);
            return new ResponseEntity<>(answer, HttpStatus.OK);
        } catch (NotFoundException e) {
            logger.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }
}
