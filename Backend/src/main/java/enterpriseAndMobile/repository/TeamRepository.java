package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Team;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface TeamRepository extends JpaRepository<Team, Integer> {
    default List<Team> getAllTeams() {
        return findAll();
    }

    default Optional<Team> getTeamById(int id) {
        return findById(id);
    }

    Team getTeamByName(String name);
}
