package enterpriseAndMobile.service;


import enterpriseAndMobile.dao.UserDao;
import enterpriseAndMobile.dto.UserDto;
import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.model.User;
import enterpriseAndMobile.repository.TeamRepository;
import enterpriseAndMobile.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.sql.Date;
import java.time.LocalDate;
import java.util.*;

@Service(value = "userService")
public class UserServiceImpl implements UserDetailsService, UserService {

    private final UserDao userDao;

    private final BCryptPasswordEncoder bcryptEncoder;

    private final RoleService roleService;

    private final UserRepository userRepository;

    private final TeamRepository teamRepository;

    public UserServiceImpl(UserDao userDao, BCryptPasswordEncoder bcryptEncoder, RoleService roleService, UserRepository userRepository, TeamRepository teamRepository) {
        this.userDao = userDao;
        this.bcryptEncoder = bcryptEncoder;
        this.roleService = roleService;
        this.userRepository = userRepository;
        this.teamRepository = teamRepository;
    }

    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        User user = userDao.findByUsername(username);
        if(user == null){
            throw new UsernameNotFoundException("Invalid username or password.");
        }
        return new org.springframework.security.core.userdetails.User(user.getUsername(), user.getPassword(), getAuthority(user));
    }

    private Set<SimpleGrantedAuthority> getAuthority(User user) {
        Set<SimpleGrantedAuthority> authorities = new HashSet<>();
        user.getRoles().forEach(role -> {
            authorities.add(new SimpleGrantedAuthority("ROLE_" + role.getName()));
        });
        return authorities;
    }



    public List<User> findAll() {
        List<User> list = new ArrayList<>();
        userDao.findAll().iterator().forEachRemaining(list::add);
        return list;
    }

    @Override
    public void delete(long id) {
        userDao.deleteById(id);
    }

    @Override
    public User findOne(String username) {
        return userDao.findByUsername(username);
    }

    @Override
    public User findById(Long id) {
        return userDao.findById(id).get();
    }

    @Override
    public User save(UserDto user) {
        User newUser = new User();
        newUser.setUsername(user.getUsername());
        newUser.setPassword(bcryptEncoder.encode(user.getPassword()));
        user.setRole(user.getRole());
        newUser.setRoles(roleService.findRoleByName(user.getRole()));
        newUser.setTeam(new Team(user.getUsername()));
        return userDao.save(newUser);
    }

    @Override
    public boolean deleteByTeamId(int id) {
        Optional<Team> team = teamRepository.getTeamById(id);
        if(team.isPresent()) {
            userRepository.deleteUserByTeam(team.get());
            return true;
        } else{
            return false;
        }
    }

}