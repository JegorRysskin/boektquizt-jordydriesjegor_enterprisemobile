package enterpriseAndMobile.service;

import enterpriseAndMobile.Exception.NotFoundException;
import enterpriseAndMobile.dto.QuizPatchDto;
import enterpriseAndMobile.dto.TeamPatchDto;
import enterpriseAndMobile.model.Quiz;
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

    @Transactional
    public Team patchTeam(int id, TeamPatchDto patch) throws NotFoundException {
        Optional<Team> team = teamRepository.getTeamById(id);
        if (team.isPresent()) {
            if (patch.getName() != "" || !patch.getName().equals(team.get().getName())) {
                team.get().setName(patch.getName());
            }
            team.get().setEnabled(patch.isEnabled());
            return teamRepository.patchTeam(team.get());
        }
        throw new NotFoundException("The team you tried to patch wasn't found.");
    }
}
