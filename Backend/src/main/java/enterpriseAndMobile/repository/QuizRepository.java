package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Quiz;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import java.util.List;

@Repository
public interface QuizRepository extends JpaRepository<Quiz, Integer> {

    default List<Quiz> getAllQuizzes() {
        return findAll();
    }

    default Quiz addQuiz(Quiz quiz){
        return save(quiz);
    }
}
