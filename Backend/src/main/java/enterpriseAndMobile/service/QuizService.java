package enterpriseAndMobile.service;

import enterpriseAndMobile.dto.QuizDto;
import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.repository.QuizRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import javax.ws.rs.NotFoundException;
import java.util.List;
import java.util.Optional;

@Service
public class QuizService {
    private final QuizRepository quizRepository;

    public QuizService(QuizRepository quizRepository) {
        this.quizRepository = quizRepository;
    }

    @Transactional(readOnly = true)
    public Optional<Quiz> getQuizById(int id) {
        return quizRepository.getQuizById(id);
    }

    @Transactional(readOnly = true)
    public List<Quiz> getAllQuizzes() {
        return quizRepository.getAllQuizzes();
    }

    @Transactional
    public Quiz addQuiz(QuizDto quizDto) {
        Quiz quiz = new Quiz(quizDto.getName(), quizDto.isEnabled(), quizDto.getRounds());
        return quizRepository.addQuiz(quiz);
    }

    @Transactional
    public void removeQuiz(int id){
        Optional<Quiz> quiz = quizRepository.getQuizById(id);
        if(quiz.isPresent()){
            quizRepository.deleteQuiz(quiz.get());
        } else{
            throw new NotFoundException("The quiz you tried to delete wasn't found.");
        }
    }
}
