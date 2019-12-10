package enterpriseAndMobile.repository;

import enterpriseAndMobile.model.Role;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface RoleRepository extends JpaRepository<Role, Integer> {
    @Query("select role from Role as role where role.name =?1")
    Role getRoleByName(String name);

    default Role createRole(Role role){
        return save(role);
    }
}
