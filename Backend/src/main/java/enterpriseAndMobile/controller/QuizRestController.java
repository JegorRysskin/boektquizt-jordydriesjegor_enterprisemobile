package enterpriseAndMobile.controller;

import enterpriseAndMobile.dto.QuizDto;
import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.service.QuizService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import javax.ws.rs.QueryParam;
import javax.ws.rs.core.Response;
import java.util.List;

import static enterpriseAndMobile.util.HttpStatusUtils.notFound;
import static enterpriseAndMobile.util.HttpStatusUtils.ok;

@CrossOrigin("*")
@RestController
@RequestMapping(value = "/quiz")
public class QuizRestController {

    @Autowired
    private QuizService quizService;

    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    @GetMapping(produces = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<List<Quiz>> getAllQuizzes() {
        List<Quiz> quizzes = quizService.getAllQuizzes();
        if (quizzes.isEmpty()) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(quizzes, HttpStatus.OK);
    }

    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    @GetMapping(value = "{id}", produces = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Quiz> getQuizById(@PathVariable("id") int id) {
        return quizService.getQuizById(id)
                .map(ok())
                .orElseGet(notFound());
    }

    @PreAuthorize("hasRole('ADMIN')")
    @PostMapping
    public ResponseEntity<Quiz> addQuiz(@RequestBody QuizDto quizDto) {
        Quiz quiz = quizService.addQuiz(quizDto);
        return new ResponseEntity<>(quiz, HttpStatus.CREATED);
    }
}
