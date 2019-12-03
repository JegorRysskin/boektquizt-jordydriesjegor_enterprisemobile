package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Team;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

@Repository
public interface TeamRepository extends JpaRepository<Team, Integer> {
    @Transactional(readOnly = true)
    default List<Team> getAllTeams() {
        return findAll();
    }
}
