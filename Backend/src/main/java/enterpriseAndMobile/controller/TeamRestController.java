package enterpriseAndMobile.controller;

import enterpriseAndMobile.Exception.NotFoundException;
import enterpriseAndMobile.annotation.LogExecutionTime;
import enterpriseAndMobile.dto.AnswerDto;
import enterpriseAndMobile.dto.TeamPatchAnswersDto;
import enterpriseAndMobile.dto.TeamPatchDto;
import enterpriseAndMobile.dto.TeamPatchScoreDto;
import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.service.TeamService;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@CrossOrigin("*")
@RestController
@RequestMapping(value = "/team")
public class TeamRestController {
    protected final Log logger = LogFactory.getLog(getClass());

    private final TeamService teamService;

    public TeamRestController(TeamService teamService) {
        this.teamService = teamService;
    }

    @LogExecutionTime
    @PreAuthorize("hasRole('ADMIN')")
    @GetMapping(produces = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<List<Team>> getAllTeams() {
        List<Team> teams = teamService.getAllTeams();
        if (teams.isEmpty()) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(teams, HttpStatus.OK);
    }

    @LogExecutionTime
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    @GetMapping(value = "/id/{id}", produces = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Object> getTeamById(@PathVariable("id") int id) {
        Optional<Team> team = teamService.getTeamById(id);

        if (team.isEmpty()) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(team.get(), HttpStatus.OK);
    }

    @LogExecutionTime
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    @GetMapping(value = "/name/{name}", produces = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Object> getTeamByName(@PathVariable("name") String name) {
        Team team = teamService.getItemByName(name);

        if (team == null) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(team, HttpStatus.OK);
    }

    @LogExecutionTime
    @PreAuthorize("hasRole('ADMIN')")
    @PatchMapping(value = "{id}")
    public ResponseEntity<Team> patchTeam(@PathVariable("id") int id, @RequestBody TeamPatchDto patchDto) {
        try {
            Team team = teamService.patchTeam(id, patchDto);
            return new ResponseEntity<>(team, HttpStatus.OK);
        } catch (NotFoundException e) {
            logger.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @LogExecutionTime
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    @PatchMapping(value = "/answer/{id}")
    public ResponseEntity<Team> patchTeamAnswers(@PathVariable("id") int id, @RequestBody AnswerDto patchDto) {
        try {
            Team team = teamService.patchTeamAnswers(id, patchDto);
             return new ResponseEntity<>(team, HttpStatus.OK);
        } catch (NotFoundException e) {
            logger.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @LogExecutionTime
    @PreAuthorize("hasAnyRole('ADMIN')")
    @PatchMapping(value = "/score/{id}")
    public ResponseEntity<Team> patchTeamScore(@PathVariable("id") int id, @RequestBody TeamPatchScoreDto patchDto) {
        try {
            Team team = teamService.patchScoreTeam(id, patchDto);
            return new ResponseEntity<>(team, HttpStatus.OK);
        } catch (NotFoundException e) {
            logger.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }
}
