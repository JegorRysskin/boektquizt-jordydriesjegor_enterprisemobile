package enterpriseAndMobile.controller;

import enterpriseAndMobile.Exception.NotFoundException;
import enterpriseAndMobile.annotation.LogExecutionTime;
import enterpriseAndMobile.dto.QuizDto;
import enterpriseAndMobile.dto.QuizPatchDto;
import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.service.QuizService;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

import static enterpriseAndMobile.util.HttpStatusUtils.notFound;
import static enterpriseAndMobile.util.HttpStatusUtils.ok;

@CrossOrigin("*")
@RestController
@RequestMapping(value = "/quiz")
public class QuizRestController {
    protected final Log logger = LogFactory.getLog(getClass());

    @Autowired
    private QuizService quizService;

    @LogExecutionTime
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    @GetMapping(produces = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<List<Quiz>> getAllQuizzes() {
        List<Quiz> quizzes = quizService.getAllQuizzes();
        if (quizzes.isEmpty()) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(quizzes, HttpStatus.OK);
    }

    @LogExecutionTime
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    @GetMapping(value = "{id}", produces = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Quiz> getQuizById(@PathVariable("id") int id) {
        return quizService.getQuizById(id)
                .map(ok())
                .orElseGet(notFound());
    }

    @LogExecutionTime
    @PreAuthorize("hasRole('ADMIN')")
    @PostMapping
    public ResponseEntity<Quiz> addQuiz(@RequestBody QuizDto quizDto) {
        Quiz quiz = quizService.addQuiz(quizDto);
        return new ResponseEntity<>(quiz, HttpStatus.CREATED);
    }

    @LogExecutionTime
    @PreAuthorize("hasRole('ADMIN')")
    @DeleteMapping(value = "{id}")
    public ResponseEntity removeQuiz(@PathVariable("id") int id) {
        try {
            quizService.removeQuiz(id);
            return new ResponseEntity(HttpStatus.OK);
        } catch (NotFoundException e) {
            logger.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @LogExecutionTime
    @PreAuthorize("hasRole('ADMIN')")
    @PatchMapping(value = "{id}")
    public ResponseEntity<Quiz> patchQuiz(@PathVariable("id") int id, @RequestBody QuizPatchDto patchDto) {
        try {
            Quiz quiz = quizService.patchQuiz(id, patchDto);
            return new ResponseEntity<>(quiz, HttpStatus.OK);
        } catch (NotFoundException e) {
            logger.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }
}
