package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Quiz;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import java.util.List;
import java.util.Optional;

@Repository
public interface QuizRepository extends JpaRepository<Quiz, Integer> {

    default Optional<Quiz> getQuizById(int id) {
        return findById(id);
    }

    default List<Quiz> getAllQuizzes() {
        return findAll();
    }

    default Quiz addQuiz(Quiz quiz){
        return save(quiz);
    }
}
