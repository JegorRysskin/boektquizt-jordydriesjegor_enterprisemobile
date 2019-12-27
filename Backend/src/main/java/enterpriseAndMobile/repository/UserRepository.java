package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Team;
import enterpriseAndMobile.model.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Repository
public interface UserRepository extends JpaRepository<User, Long> {

    @Transactional(readOnly = true)
    default Optional<User> getUserById(Long id) {
        return findById(id);
    }

    @Transactional(readOnly = true)
    default List<User> getAllUsers() {
        return findAll();
    }

    @Transactional
    default void deleteUserById(User user) {
        delete(user);
    }

    @Transactional
    default User updateUser(User user) {
        return save(user);
    }

    @Transactional
    default Optional<User> addNewUser(User user){
        return Optional.of(save(user));
    }

    @Transactional
    void deleteUserByTeam(Team team);

}
