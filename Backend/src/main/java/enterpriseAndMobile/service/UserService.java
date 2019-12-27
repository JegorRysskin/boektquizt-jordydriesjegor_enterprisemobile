package enterpriseAndMobile.service;


import enterpriseAndMobile.dto.UserDto;
import enterpriseAndMobile.model.User;

import java.util.List;

public interface UserService {
    User save(UserDto user);
    List<User> findAll();
    void delete(long id);
    User findOne(String username);

    User findById(Long id);

    boolean deleteByTeamId(int id);
}
