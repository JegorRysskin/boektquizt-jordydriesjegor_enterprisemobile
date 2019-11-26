package enterpriseAndMobile.service;

import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.repositories.QuizRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class QuizService {
    private final QuizRepository quizRepository;

    public QuizService(@Autowired QuizRepository quizRepository) {
        this.quizRepository = quizRepository;
    }

    public List<Quiz> getAllQuizzes(){
        return quizRepository.getAllQuizzes();
    }
}
