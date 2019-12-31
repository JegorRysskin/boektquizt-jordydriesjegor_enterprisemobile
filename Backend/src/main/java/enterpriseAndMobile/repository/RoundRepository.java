package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Round;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface RoundRepository extends JpaRepository<Round, Integer> {
    @Override
    Optional<Round> findById(Integer integer);
}
