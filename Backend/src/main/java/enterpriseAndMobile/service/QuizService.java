package enterpriseAndMobile.service;

import enterpriseAndMobile.dto.QuizDto;
import enterpriseAndMobile.dto.QuizPatchDto;
import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.Exception.NotFoundException;
import enterpriseAndMobile.model.Round;
import enterpriseAndMobile.repository.QuizRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

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
    public List<Round> getListOfRoundsByQuizById(int id) throws NotFoundException {
        Optional<Quiz> quiz = quizRepository.getQuizById(id);
        if(quiz.isPresent()){
            return quiz.get().getRounds();
        } else {
            throw new NotFoundException("The quiz you tried to get wasn't found.");
        }
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
    public void removeQuiz(int id) throws NotFoundException {
        Optional<Quiz> quiz = quizRepository.getQuizById(id);
        if(quiz.isPresent()){
            quizRepository.deleteQuiz(quiz.get());
        } else {
            throw new NotFoundException("The quiz you tried to delete wasn't found.");
        }
    }

    @Transactional
    public Quiz patchQuiz(int id, QuizPatchDto patch) throws NotFoundException {
        Optional<Quiz> quiz = quizRepository.getQuizById(id);
        if (quiz.isPresent()) {
            if (patch.getName() != "" || !patch.getName().equals(quiz.get().getName())) {
                quiz.get().setName(patch.getName());
            }
            quiz.get().setEnabled(patch.isEnabled());
            return quizRepository.patchQuiz(quiz.get());
        }
        throw new NotFoundException("The quiz you tried to patch wasn't found.");
    }
}
