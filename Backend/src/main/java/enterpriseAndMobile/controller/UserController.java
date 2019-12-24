package enterpriseAndMobile.controller;


import enterpriseAndMobile.annotation.LogExecutionTime;
import enterpriseAndMobile.dto.UserDto;
import enterpriseAndMobile.model.User;
import enterpriseAndMobile.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
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
        return userService.save(user);
    }



}