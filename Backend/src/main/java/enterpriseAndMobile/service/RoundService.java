package enterpriseAndMobile.service;

import enterpriseAndMobile.Exception.NotFoundException;
import enterpriseAndMobile.dto.RoundPatchDto;
import enterpriseAndMobile.dto.RoundPatchTeamIdDto;
import enterpriseAndMobile.model.Quiz;
import enterpriseAndMobile.model.Round;
import enterpriseAndMobile.repository.QuizRepository;
import enterpriseAndMobile.repository.RoundRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Service
public class RoundService {

    private final RoundRepository roundRepository;
    private final QuizRepository quizRepository;

    public RoundService(RoundRepository roundRepository, QuizRepository quizRepository) {
        this.roundRepository = roundRepository;
        this.quizRepository = quizRepository;
    }

    @Transactional(readOnly = true)
    public Round getRoundById(int id) throws NotFoundException {
        Optional<Round> foundRound = roundRepository.findById(id);
        if (foundRound.isPresent()) {
            return foundRound.get();
        }
        throw new NotFoundException("Round was't found.");
    }

    @Transactional
    public Round patchRound(int id, RoundPatchDto patchRound) throws NotFoundException {
        Optional<Round> foundRound = roundRepository.findById(id);
        if (foundRound.isPresent()) {
            if (!patchRound.getName().equals("") || patchRound.getName() != null || !patchRound.getName().equals(foundRound.get().getName())) {
                foundRound.get().setName(patchRound.getName());
            }
            if (patchRound.getQuestions() != null) {
                foundRound.get().setQuestions(patchRound.getQuestions());
            }
            foundRound.get().setEnabled(patchRound.isEnabled());
            return roundRepository.patchRound(foundRound.get());
        }
        throw new NotFoundException("Round was't found.");
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

    public List<Round> getListOfRoundsByEnabled() throws NotFoundException {
        List<Quiz> quiz = quizRepository.getAllQuizzes();
        int count = 0;
        while(!quiz.get(count).isEnabled() && quiz.size() -1 > count){
            count++;
        }
        if(quiz.size() < count){
            throw new NotFoundException("No quiz is enabled.");
        }
        return quiz.get(count).getRounds();
    }

    @Transactional
    public Round patchTeamIdRound(int id, RoundPatchTeamIdDto patch) throws NotFoundException {
        Optional<Round> round = roundRepository.findById(id);
        if (round.isPresent()) {
            if (patch.getTeamId() != 0) {
                List<Integer> teamIdList = round.get().getTeamIdOpenedRound();
                teamIdList.add(patch.getTeamId());
                round.get().setTeamIdOpenedRound(teamIdList);
            }
            return roundRepository.patchRound(round.get());
        }
        throw new NotFoundException("The round you tried to patch wasn't found.");
    }
}
