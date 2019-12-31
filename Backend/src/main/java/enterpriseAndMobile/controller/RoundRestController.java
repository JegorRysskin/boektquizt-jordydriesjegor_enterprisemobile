package enterpriseAndMobile.controller;

import enterpriseAndMobile.Exception.NotFoundException;
import enterpriseAndMobile.annotation.LogExecutionTime;
import enterpriseAndMobile.model.Round;
import enterpriseAndMobile.service.RoundService;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

@CrossOrigin("*")
@RestController
@RequestMapping(value = "/round")
public class RoundRestController {
    protected final Log logger = LogFactory.getLog(getClass());
    private final RoundService roundService;

    public RoundRestController(RoundService roundService) {
        this.roundService = roundService;
    }

    @LogExecutionTime
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    @GetMapping(value = "/{id}", produces = MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Round> getRoundById(@PathVariable("id") int id) {
        try {
            Round found  = roundService.getRoundById(id);
            return new ResponseEntity<>(found, HttpStatus.OK);
        } catch (NotFoundException e) {
            logger.error(e.getMessage(), e);
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }
}
