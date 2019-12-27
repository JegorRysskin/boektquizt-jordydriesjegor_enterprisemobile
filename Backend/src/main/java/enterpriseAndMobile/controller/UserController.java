package enterpriseAndMobile.controller;


import enterpriseAndMobile.annotation.LogExecutionTime;
import enterpriseAndMobile.dto.UserDto;
import enterpriseAndMobile.model.User;
import enterpriseAndMobile.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin(origins = "*", maxAge = 3600)
@RestController
public class UserController {

    @Autowired
    private UserService userService;

    @LogExecutionTime
    @PreAuthorize("hasRole('ADMIN')")
    @GetMapping(value="/users")
    public List<User> listUser(){
        return userService.findAll();
    }

    @LogExecutionTime
    @PreAuthorize("hasAnyRole('ADMIN', 'USER')")
    @GetMapping(value = "/users/{id}")
    public User getOne(@PathVariable(value = "id") Long id){
        return userService.findById(id);
    }

    @LogExecutionTime
    @PostMapping(value = "/signup")
    public User saveUser(@RequestBody UserDto user){
        user.setRole(new String[]{"USER"});
        return userService.save(user);
    }

    @PreAuthorize("hasRole('ENGINEER')")
    @PostMapping(value = "/addAdmin")
    public User addAdmin(@RequestBody UserDto user){
        user.setRole(new String[]{"ADMIN"});
        return userService.save(user);
    }

    @LogExecutionTime
    @PreAuthorize("hasRole('ADMIN')")
    @DeleteMapping(value="/users/{id}")
    public ResponseEntity removeUserByTeamId(@PathVariable("id") int id){
        boolean success = userService.deleteByTeamId(id);
        if(success == true){
            return new ResponseEntity(HttpStatus.OK);
        } else{
            return new ResponseEntity(HttpStatus.NOT_FOUND);
        }
    }



}