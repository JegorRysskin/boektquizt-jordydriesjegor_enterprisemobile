package enterpriseAndMobile.service;

import enterpriseAndMobile.Exception.NotFoundException;
import enterpriseAndMobile.model.Answer;
import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.repository.TeamRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Service
public class AnswerService {
    private final TeamRepository teamRepository;

    public AnswerService(TeamRepository teamRepository) {
        this.teamRepository = teamRepository;
    }

    @Transactional(readOnly = true)
    public Answer getAnswerByQuestionIdAndTeamId(int teamId, int questionId) throws NotFoundException {
        Optional<Team> team = teamRepository.getTeamById(teamId);
        if (team.isPresent()) {
            List<Answer> answers = team.get().getAnswers();
            for (Answer answer : answers) {
                if (answer.getQuestion().getId() == questionId) {
                    return answer;
                }
            }
        }
        throw new NotFoundException("Team you tried to get wasn't found.");
    }
}
