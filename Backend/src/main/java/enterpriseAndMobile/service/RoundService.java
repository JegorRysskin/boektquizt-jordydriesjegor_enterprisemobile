package enterpriseAndMobile.service;

import enterpriseAndMobile.Exception.NotFoundException;
import enterpriseAndMobile.model.Round;
import enterpriseAndMobile.repository.RoundRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Optional;

@Service
public class RoundService {

    private final RoundRepository roundRepository;

    public RoundService(RoundRepository roundRepository) {
        this.roundRepository = roundRepository;
    }

    @Transactional(readOnly = true)
    public Round getRoundById(int id) throws NotFoundException {
        Optional<Round> foundRound =  roundRepository.findById(id);
        if(foundRound.isPresent()){
            return foundRound.get();
        }
        throw new NotFoundException("Round was't found.");
    }
}
