package enterpriseAndMobile.service;

import enterpriseAndMobile.dao.UserDao;
import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.repository.TeamRepository;
import enterpriseAndMobile.repository.UserRepository;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.test.context.junit.jupiter.SpringExtension;

import java.util.Optional;

import static org.mockito.ArgumentMatchers.anyInt;
import static org.mockito.BDDMockito.given;

@ExtendWith(SpringExtension.class)
@SpringBootTest(classes = UserServiceImpl.class)
public class UserServiceUnitTest {
    @MockBean
    private UserRepository userRepository;

    @MockBean
    private TeamRepository teamRepository;

    @MockBean
    private UserDao userDao;

    @MockBean
    private BCryptPasswordEncoder bcryptEncoder;

    @MockBean
    private RoleService roleService;

    @Autowired
    private UserServiceImpl userService;

    @Test
    public void deleteUserByTeamId_FromUserService() {
        Team team = new Team();

        given(teamRepository.getTeamById(anyInt())).willReturn(Optional.of(team));
        Assertions.assertTrue(userService.deleteByTeamId(anyInt()));
    }
}
