package enterpriseAndMobile.controller;

import enterpriseAndMobile.dto.QuizDto;
import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.service.QuizService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin("*")
@RestController
@RequestMapping(value = "/quiz")
public class QuizRestController {

    @Autowired
    private QuizService quizService;

    @GetMapping(produces = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<List<Quiz>> getAllQuizzes() {
        List<Quiz> quizzes = quizService.getAllQuizzes();
        if (quizzes.isEmpty()) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<> (quizzes, HttpStatus.OK);
    }

    @PostMapping
    public ResponseEntity<Quiz> addQuiz(@RequestBody QuizDto quizDto){
        Quiz quiz = quizService.addQuiz(quizDto);
        return new ResponseEntity<>(quiz, HttpStatus.CREATED);
    }
}
