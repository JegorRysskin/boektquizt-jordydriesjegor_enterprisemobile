package enterpriseAndMobile.service;

import enterpriseAndMobile.dto.QuizDto;
import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.repository.QuizRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

@Service
public class QuizService {
    private final QuizRepository quizRepository;

    public QuizService(@Autowired QuizRepository quizRepository) {
        this.quizRepository = quizRepository;
    }

    @Transactional(readOnly = true)
    public List<Quiz> getAllQuizzes() {
        return quizRepository.getAllQuizzes();
    }

    @Transactional
    public Quiz addQuiz(QuizDto quizDto) {
        Quiz quiz = new Quiz(quizDto.getName(), quizDto.isEnabled());
        return quizRepository.addQuiz(quiz);
    }
}
