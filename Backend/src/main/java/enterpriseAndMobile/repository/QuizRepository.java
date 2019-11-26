package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Quiz;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

@Repository
public interface QuizRepository extends JpaRepository<Quiz, Integer> {
    @Transactional(readOnly = true)
    default List<Quiz> getAllQuizzes() {
        return findAll();
    }
}
