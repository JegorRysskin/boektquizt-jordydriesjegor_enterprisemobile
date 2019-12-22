package enterpriseAndMobile.service;

import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.repository.TeamRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Service
public class TeamService {
    private final TeamRepository teamRepository;

    public TeamService(TeamRepository teamRepository) {
        this.teamRepository = teamRepository;
    }

    @Transactional(readOnly = true)
    public List<Team> getAllTeams() {
        return teamRepository.getAllTeams();
    }

    @Transactional(readOnly = true)
    public Optional<Team> getTeamById(int id) {
        return teamRepository.getTeamById(id);
    }

    @Transactional(readOnly = true)
    public Team getItemByName(String name) {
        return teamRepository.getTeamByName(name);
    }
}
